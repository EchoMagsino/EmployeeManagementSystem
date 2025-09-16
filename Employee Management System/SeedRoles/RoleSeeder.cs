using Microsoft.AspNetCore.Identity;

namespace Employee_Management_System.SeedRoles
{
    public class RoleSeeder
    {

        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roles = { "User", "Admin", "HR", "Manager" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

            }

            var managerEmail = "jerichomagsino28@gmail.com";
            var managerUser = await userManager.FindByEmailAsync(managerEmail);

            if (managerUser != null && !await userManager.IsInRoleAsync(managerUser, "Manager"))
            {
                await userManager.AddToRolesAsync(managerUser, new List<string> { "Manager" });
            }


            }

        }
    }
