using QuintrixHomeworkPlayerMVP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Threading.Tasks;

namespace QuintrixHomeworkPlayerMVP.Authorization
{
    public class BotAdministratorsAuthorizationHandler
                    : AuthorizationHandler<OperationAuthorizationRequirement, Bot>
    {
        protected override Task HandleRequirementAsync(
                                              AuthorizationHandlerContext context,
                                    OperationAuthorizationRequirement requirement, 
                                     Bot resource)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            // Administrators can do anything.
            if (context.User.IsInRole(Constants.BotAdministratorRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
