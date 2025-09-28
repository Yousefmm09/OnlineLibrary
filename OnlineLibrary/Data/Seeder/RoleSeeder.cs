using Microsoft.AspNetCore.Identity;

namespace OnlineLibrary.Data.Seeder
{
    public class RoleSeeder
    {
        public static async Task Roleseeder(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "USER", "ADMIN", "SUPERADMIN" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}
