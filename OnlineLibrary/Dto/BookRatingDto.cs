using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary.Dto
{
    public class BookRatingDto
    {
        public int BookId { get; set; }
        public int Rating { get; set; }
        [MaxLength(250)]
        public string Comment { get; set; }
        public DateTime DateTime { get; set; }
    }
}
