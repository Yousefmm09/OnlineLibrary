using Microsoft.AspNetCore.Identity;
using OnlineLibrary.Model;

namespace OnlineLibrary.Data.Seeder
{
    public static class AdminUserSeeder
    {
        public static async Task SeedAdminUser(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, OBDbcontext dbcontext)
        {
            const string adminEmail = "admin@example.com";
            const string adminUserName = "admin";
            const string adminPassword = "StrongAdminPassword123!";

            // Ensure ADMIN role exists
            if (!await roleManager.RoleExistsAsync("ADMIN"))
            {
                await roleManager.CreateAsync(new IdentityRole("ADMIN"));
            }

            // Ensure admin user exists
            var adminUser = await userManager.FindByNameAsync(adminUserName);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminUserName,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    PhoneNumber = "010679493048"
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (!result.Succeeded)
                {
                    // optionally log or throw if you want to fail startup
                    return;
                }

                await userManager.AddToRoleAsync(adminUser, "ADMIN");
            }
            else
            {
                // Ensure user has ADMIN role
                if (!await userManager.IsInRoleAsync(adminUser, "ADMIN"))
                {
                    await userManager.AddToRoleAsync(adminUser, "ADMIN");
                }
            }

            // Ensure a Customer record exists for this user (so Login and token creation work)
            var existingCustomer = dbcontext.Customers.FirstOrDefault(c => c.UserId == adminUser.Id);
            if (existingCustomer == null)
            {
                var customer = new Customer
                {
                    Name = "Administrator",
                    EmailAddress = adminEmail,
                    Adress = "Admin Address",
                    PhoneNumber = adminUser.PhoneNumber,
                    UserId = adminUser.Id
                };
                dbcontext.Customers.Add(customer);
                await dbcontext.SaveChangesAsync();
            }
        }
    }
}
