using QuintrixHomeworkPlayerMVP.Authorization;
using QuintrixHomeworkPlayerMVP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace QuintrixHomeworkPlayerMVP.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw="")
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                var adminID=await EnsureUser(serviceProvider,testUserPw,"adminTest@player.bot");
                await EnsureRole(serviceProvider,adminID,Constants.BotAdministratorRole);
                SeedDB(context, testUserPw);
            }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                                    string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<Player>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new Player
                {
                    UserName = UserName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            IdentityResult IR;
            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<Player>>();

            if (userManager == null)
            {
                throw new Exception("userManager is null");
            }

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
        public static void SeedDB(ApplicationDbContext context, string adminID)
        {
            if (context.Bot.Any()) return;
            context.Bot.AddRange(
                new Bot{
                    Id=Guid.NewGuid(),
                    Name="Debera Garcia",
                    Status=BotStatus.Untarnished,
                    OwnerID=adminID
                },
                new Bot{
                    Id=Guid.NewGuid(),
                    Name="Thorsten Weinrich",
                    Status=BotStatus.Worn,
                    OwnerID=adminID
                },
                new Bot{
                    Id=Guid.NewGuid(),
                    Name="Yuhong Li",
                    Status=BotStatus.Broken,
                    OwnerID=adminID
                },
                new Bot{
                    Id=Guid.NewGuid(),
                    Name="Jon Orton",
                    Status=BotStatus.Broken,
                    OwnerID=adminID
                },
                new Bot{
                    Id=Guid.NewGuid(),
                    Name="Diliana Alexieve-Bosseva",
                    Status=BotStatus.Worn,
                    OwnerID=adminID
                }
            );
            context.SaveChanges();
        }
    }
}