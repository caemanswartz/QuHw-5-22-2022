using Microsoft.AspNetCore.Authorization.Infrastructure;
namespace QuintrixHomeworkPlayerMVP.Authorization
{
    public static class BotOperations
    {
        public static OperationAuthorizationRequirement Create=
            new OperationAuthorizationRequirement
                {Name=Constants.CreateOperationName};
        public static OperationAuthorizationRequirement Read=
            new OperationAuthorizationRequirement
                {Name=Constants.ReadOperationName};
        public static OperationAuthorizationRequirement Update=
            new OperationAuthorizationRequirement
                {Name=Constants.UpdateOperationName};
        public static OperationAuthorizationRequirement Delete=
            new OperationAuthorizationRequirement
                {Name=Constants.DeleteOperationName};
        public static OperationAuthorizationRequirement Break=
            new OperationAuthorizationRequirement
                {Name=Constants.BreakOperationName};
        public static OperationAuthorizationRequirement Repair=
            new OperationAuthorizationRequirement
                {Name=Constants.RepairOperationName};
    }
    public static class Constants
    {
        public static readonly string CreateOperationName="Create";
        public static readonly string ReadOperationName="Read";
        public static readonly string UpdateOperationName="Update";
        public static readonly string DeleteOperationName="Delete";
        public static readonly string BreakOperationName="Break";
        public static readonly string RepairOperationName="Repair";
        public static readonly string BotAdministratorRole="BotAdministrator";
    }
}