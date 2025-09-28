using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLibrary.Model
{
    public class BookReview
    {
        public int Id { get; set; }
        [ForeignKey("BookId")]
        public int BookId {  get; set; }
        public Book book { get; set; }
        [ForeignKey("UserId")]
        public string UserId {  get; set; }
        public ApplicationUser User { get; set; }
        public int Rating {  get; set; }
        [MaxLength(250)]
        public string Comment { get; set; }
        public DateTime DateTime { get; set; }
    }
}
