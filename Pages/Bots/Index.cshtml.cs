using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuintrixHomeworkPlayerMVP.Authorization;
using QuintrixHomeworkPlayerMVP.Data;
using QuintrixHomeworkPlayerMVP.Models;
namespace QuintrixHomeworkPlayerMVP.Pages.Bots
{
    public class IndexModel:DI_BasePageModel
    {
        #pragma warning disable CS8618
        public IndexModel(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<Player> userManager) : base(
                context, authorizationService, userManager){}
        #pragma warning restore CS8618
        public IList<Bot> Bots{get;set;}
        public async Task OnGetAsync()
        {
            var bots = from b in Context.Bot
                select b;
            var isAuthorized=User.IsInRole(Constants.BotAdministratorRole);
            var currentUserId=UserManager.GetUserId(User);
            if (!isAuthorized)
                bots=bots.Where(b=>b.OwnerID==currentUserId
                    || b.Status!=BotStatus.Broken);
            Bots=await bots.ToListAsync();
        }
    }
}