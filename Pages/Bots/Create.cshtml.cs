using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuintrixHomeworkPlayerMVP.Authorization;
using QuintrixHomeworkPlayerMVP.Data;
using QuintrixHomeworkPlayerMVP.Models;
namespace QuintrixHomeworkPlayerMVP.Pages.Bots
{
    public class CreateModel:DI_BasePageModel
    {
        #pragma warning disable CS8618
        public CreateModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<Player> usermanager) : base(
                context, authorizationService, usermanager){}
        #pragma warning restore CS8618
        public IActionResult OnGet()
        {
            return Page();
        }
        [BindProperty]
        public Bot Bot{get;set;}
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
                return Page();
            Bot.OwnerID=UserManager.GetUserId(User);
            var isAuthorized= await AuthorizationService
                .AuthorizeAsync(User,Bot,BotOperations.Create);
            if (!isAuthorized.Succeeded)
                return Forbid();
            Context.Bot.Add(Bot);
            await Context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}