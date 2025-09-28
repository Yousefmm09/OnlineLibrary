using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary.Dto
{
    public class GetAllBookinCategoryDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
