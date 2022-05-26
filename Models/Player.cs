using Microsoft.AspNetCore.Identity;
namespace QuintrixHomeworkPlayerMVP.Models
{
    public class Player : IdentityUser
    {
        [PersonalData]
        public string Name{get;set;}
        [PersonalData]
        public uint Currency{get;set;}
        #pragma warning disable CS8618
        public Player(){}
        #pragma warning restore CS8618
    }
}