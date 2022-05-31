using QuintrixHomeworkPlayerMVP.Authorization;
using QuintrixHomeworkPlayerMVP.Data;
using QuintrixHomeworkPlayerMVP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace QuintrixHomeworkPlayerMVP.Pages.Bots
{
    public class DetailsModel : DI_BasePageModel
    {
    #pragma warning disable CS8618
        public DetailsModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<Player> userManager)
            : base(context, authorizationService, userManager){}
    #pragma warning restore CS8618

        public Bot Bot { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Bot? _bot = await Context.Bot.FirstOrDefaultAsync(m => m.Id == id);

            if (_bot == null)
            {
                return NotFound();
            }
            Bot = _bot;

            var isAuthorized = User.IsInRole(Constants.BotAdministratorRole);

            var currentUserId = UserManager.GetUserId(User);

            if (!isAuthorized
                && currentUserId != Bot.OwnerID
                && Bot.Status != BotStatus.Broken)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id, BotStatus status)
        {
            var bot = await Context.Bot.
                FirstOrDefaultAsync(m => m.Id == id);

            if (bot == null)
            {
                return NotFound();
            }

            var botOperation = (status == BotStatus.Broken)
                ? BotOperations.Repair
                : BotOperations.Break;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, bot,
                                        botOperation);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            bot.Status = status;
            Context.Bot.Update(bot);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

