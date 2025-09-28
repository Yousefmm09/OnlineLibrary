namespace OnlineLibrary.Dto
{
    public class ResetPassowordDto
    {
        public string Email {  get; set; }=string.Empty;
        public string token {  get; set; }=string.Empty;
        public string password { get; set; } = string.Empty;
    }
}
