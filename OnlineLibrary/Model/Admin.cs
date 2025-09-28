namespace OnlineLibrary.Model
{
    public class Admin
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
