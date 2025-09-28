using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLibrary.Model
{
    public class Order
    {
        public int Id { get; set; }

        public Customer Customer { get; set; }
        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public List<Book> Books { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending";
        public List<OrderItem> Items { get; set; }
    }
}
