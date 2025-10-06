namespace OnlineLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OBDbcontext _dbcontext;
        private readonly UserManager<ApplicationUser> _userManager;
        public OrderController(OBDbcontext dbcontext,UserManager<ApplicationUser> userManager)
        {
            _dbcontext = dbcontext;
            _userManager = userManager;
        }
        [HttpGet("get-orders")]
        [Authorize(Roles ="USER,ADMIN")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = _dbcontext.Orders
                .Join(
                    _dbcontext.Customers,
                    o => o.CustomerId,
                    c => c.Id,
                    (o, c) => new
                    {
                        o.Id,
                        CustomerName = c.Name,
                        o.OrderDate,
                        o.TotalAmount,
                        o.Status,
                        Item = o.Items.Select(ot => new
                        {
                           Title= ot.Book.Title,
                           Quantity=ot.Quantity,
                           
                        })
                    });
                
            return Ok(orders);
        }
        [HttpPost("create-order")]
        [Authorize(Roles ="USER")]
        public async Task<IActionResult> CreateOrder([FromBody] CreatOrderDto createOrder)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user= await _userManager.GetUserAsync(User);
            var customer =  _dbcontext.Customers.FirstOrDefault(c=>c.UserId==user.Id);
            if (customer == null)
                return NotFound($"The customer is not found");

            if (createOrder.Items == null || !createOrder.Items.Any())
                return BadRequest("Order must have at least one item");

            // 3️⃣ Create order entity
            var order = new Order
            {
                CustomerId=customer.Id,
                OrderDate = DateTime.UtcNow,
                Status = "Pending",
                TotalAmount = 0, // will be calculated below
                Items = new List<OrderItem>()
            };

            foreach (var itemDto in createOrder.Items)
            {
                var book = await _dbcontext.Books.FindAsync(itemDto.BookId);
                if (book == null)
                    return NotFound($"Book with ID {itemDto.BookId} not found");

                if (book.Stock < itemDto.Quantity)
                    return BadRequest($"Not enough stock for book {book.Title}");

                // Update stock
                book.Stock -= itemDto.Quantity;

                // Add order item
                var orderItem = new OrderItem
                {
                    BookId = book.Id,
                    Quantity = itemDto.Quantity,
                    Price = book.Price * itemDto.Quantity
                };

                order.Items.Add(orderItem);
                order.TotalAmount += orderItem.Price;
            }

            _dbcontext.Orders.Add(order);
            await _dbcontext.SaveChangesAsync();

            return Ok(new
            {
                Message = "Order created successfully",
                OrderId = order.Id,
                TotalAmount = order.TotalAmount
            });
        }
        [HttpDelete("deleteOrder")]
        [Authorize(Roles ="USER")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var order = await _dbcontext.Orders.FindAsync(orderId);
            if (order == null)
                return NotFound($"Order with ID {orderId} not found");
            // Restore stock for each item in the order
            var orderItems = _dbcontext.OrderItems.Where(oi => oi.OrderId == orderId).ToList();
            foreach (var item in orderItems)
            {
                var book = await _dbcontext.Books.FindAsync(item.BookId);
                if (book != null)
                {
                    book.Stock += item.Quantity;
                }
            }
            _dbcontext.OrderItems.RemoveRange(orderItems);
            _dbcontext.Orders.Remove(order);
            await _dbcontext.SaveChangesAsync();
            return Ok(new { Message = "Order deleted successfully" });
        }   

    }
}
