using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLibrary.Model
{
    public class Borrow
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [ForeignKey(nameof(Book))]
        public int? BookId { get; set; }
        public Book Book { get; set; }

        public DateTime BorrowDate { get; set; } = DateTime.Now;
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; } = "Borrowed";
    }
}
