using System;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
namespace QuintrixHomeworkPlayerMVP.Models
{
    public class Player : IdentityUser
    {
        public string Name{get;set;}
        public uint Currency{get;set;}
        #pragma warning disable CS8618
        public Player(){}
        #pragma warning restore CS8618
    }
}