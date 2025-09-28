using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary.Dto
{
    public class AddBookDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public decimal Price { get; set; }
        [param: MaxLength(2048)]
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public int PublishedYear { get; set; }

    }
}
