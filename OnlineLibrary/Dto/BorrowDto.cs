namespace OnlineLibrary.Dto
{
    public class BorrowDto
    {
        
        public int? BookId { get; set; }
        public DateTime BorrowDate { get; set; } = DateTime.Now;
        public DateTime? ReturnDate { get; set; }=DateTime.Now;
        public string Status { get; set; }
    }
}
