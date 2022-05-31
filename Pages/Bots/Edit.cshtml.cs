using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuintrixHomeworkPlayerMVP.Authorization;
using QuintrixHomeworkPlayerMVP.Data;
using QuintrixHomeworkPlayerMVP.Models;
namespace QuintrixHomeworkPlayerMVP.Pages.Bots
{
    public class EditModel:DI_BasePageModel
    {
        private readonly ApplicationDbContext _context;
        #pragma warning disable CS8618
        public EditModel(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<Player> userManager)
            :base(context,authorizationService,userManager){}
        #pragma warning restore CS8618
        [BindProperty]
        public Bot Bot{get;set;}
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Bot? bot=await Context.Bot.FirstOrDefaultAsync(
                m=>m.Id==id);
            if(bot==null)return NotFound();
            Bot=bot;
            var isAuthorized= await AuthorizationService
                .AuthorizeAsync(User,Bot,BotOperations.Update);
            if (!isAuthorized.Succeeded)return Forbid();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if(!ModelState.IsValid)return Page();
            var bot=await Context.Bot.AsNoTracking()
                .FirstOrDefaultAsync(m=>m.Id==id);
            if(bot==null)return NotFound();
            var isAuthorized=await AuthorizationService
                .AuthorizeAsync(User,bot,BotOperations.Update);
            if(!isAuthorized.Succeeded)return Forbid();
            Bot.OwnerID=bot.OwnerID;
            Context.Attach(Bot).State=EntityState.Modified;
            if(Bot.Status==BotStatus.Broken)
            {
                var canRepair=await AuthorizationService
                    .AuthorizeAsync(User,Bot,BotOperations.Repair);
                if(canRepair.Succeeded)Bot.Status=BotStatus.Worn;
            }
            await Context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
        private bool BotExists(Guid id)
        {
            return _context.Bot.Any(e=>e.Id==id);
        }
    }
}