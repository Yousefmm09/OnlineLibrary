using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]
        public List<Book> Books { get; set; }


    }
}
