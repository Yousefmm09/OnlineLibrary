using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Model;

namespace OnlineLibrary.Data
{
    public class OBDbcontext:IdentityDbContext<ApplicationUser>
    {
     
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Borrow> Borrows { get; set; }
        public DbSet<BookReview> BookReviews { get; set; }
        public OBDbcontext(DbContextOptions<OBDbcontext> options):base(options)
        {

        }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
          
          
            base.OnModelCreating(modelBuilder);
        }
    }
}
