using QuintrixHomeworkPlayerMVP.Authorization;
using QuintrixHomeworkPlayerMVP.Data;
using QuintrixHomeworkPlayerMVP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace QuintrixHomeworkPlayerMVP.Pages.Bots
{
    public class DeleteModel:DI_BasePageModel
    {
        public DeleteModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<Player> userManager)
            :base(context,authorizationService,userManager){}
        [BindProperty]
        public Bot Bot{get;set;}
        public async Task<IActionResult> onGetAsync(Guid id)
        {
            Bot? _bot=await Context.Bot.FirstOrDefaultAsync(
                m=>m.Id==id);
            if(_bot==null)return NotFound();
            Bot=_bot;
            var isAuthorized=await AuthorizationService
                .AuthorizeAsync(User,Bot,BotOperations.Delete);
            if(!isAuthorized.Succeeded)return Forbid();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var bot=await Context.Bot.AsNoTracking()
                .FirstOrDefaultAsync(m=>m.Id==id);
            if(bot==null)return NotFound();
            var isAuthorized=await AuthorizationService
                .AuthorizeAsync(User,Bot,BotOperations.Delete);
            Context.Bot.Remove(bot);
            await Context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}