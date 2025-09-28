using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary.Model
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        [StringLength(100)]
        public string Adress {  get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Borrow> Borrows { get; set; }
    }
}
