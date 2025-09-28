using OnlineLibrary.Model;

namespace OnlineLibrary.Dto
{
    public class GetAllOrder
    {
        public int CustomerId { get; set; }
        public List<Book> Books { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
    }
}
