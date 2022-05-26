using System;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
namespace QuintrixHomeworkPlayerMVP.Models
{
    public class Player : IdentityUser
    {
        private static Regex _validEmailPattern=new(
            @"^[a-zA-Z0-9_-]+@(\w+\.)+\w{2,}$",
            RegexOptions.Compiled
        );
        public new Guid Id{get;set;}
        public string Name{get;set;}
        private string _email;
        #pragma warning disable CS8618
        public override string Email
        #pragma warning restore CS8618
        {
            get=>_email;
            set=>_email=ValidatedEmail(value);
        }
        public uint Currency{get;set;}
        #pragma warning disable CS8618
        public Player(){}
        #pragma warning restore CS8618
        private static string ValidatedEmail(string input)
        {
            if(! _validEmailPattern.IsMatch(input))
                throw new Exception("Email pattern invalid");
            return input;
        }
    }
}