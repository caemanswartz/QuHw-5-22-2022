using QuintrixHomeworkPlayerMVP.Data;
using QuintrixHomeworkPlayerMVP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace QuintrixHomeworkPlayerMVP.Pages.Bots
{
    public class DI_BasePageModel:PageModel
    {
        protected ApplicationDbContext Context{get;}
        protected IAuthorizationService AuthorizationService{get;}
        protected UserManager<Player> UserManager{get;}
        public DI_BasePageModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<Player> userManager) : base()
            {
                Context=context;
                UserManager=userManager;
                AuthorizationService=authorizationService;
            }
    }
}