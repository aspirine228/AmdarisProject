using GameTracker.Domain.Auth;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace GameTracker.Rep.Seed
{
    public class UserSeed
    {
        public static async Task Seed(UserManager<User> userManager , RoleManager<Role> roleManager)
        {

            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new Role() {Name="admin"});
            }
            if (await roleManager.FindByNameAsync("company") == null)
            {
                await roleManager.CreateAsync(new Role() { Name = "company" });
            }
            if (await roleManager.FindByNameAsync("gamer") == null)
            {
                await roleManager.CreateAsync(new Role() { Name = "gamer" });
            }
            if (!userManager.Users.Any())
            {
                var user = new User()
                {
                    UserName = "admin",
                    Email = "admin@gamerhistory.com",
                };
                
                await userManager.CreateAsync(user, "Qwerty1!");
                await userManager.AddToRoleAsync(user, "admin");
            }

        }
    }
}
