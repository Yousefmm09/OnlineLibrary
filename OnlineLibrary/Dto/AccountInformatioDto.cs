using OnlineLibrary.Model;

namespace OnlineLibrary.Dto
{
    public class AccountInformatioDto
    {
        public string UserName { get; set; }=string.Empty;
        public string Email {  get; set; }=string.Empty;
        public List<Borrow> MyBorrow {  get; set; }= new List<Borrow>();


    }
}
