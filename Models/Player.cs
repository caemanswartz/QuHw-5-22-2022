using System;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
namespace QuintrixHomeworkPlayerMVP.Models
{
    public class Player
    {
        private static Regex _validEmailPattern=new(
            @"^[a-zA-Z0-9_-]+@(\w+\.)+\w{2,}$",
            RegexOptions.Compiled
        );
        [Key]
        public Guid Id{get;set;}
        public string Name{get;set;}
        private string _email;
        public string Email
        {
            get=>_email;
            set=>_email=ValidatedEmail(value);
        }
        #pragma warning disable CS8618
        public Player(){}
        #pragma warning restore CS8618
        private static string ValidatedEmail(string input)
        {
            // TBD blocks invalid emails sets field to empty?
            //if(! _validEmailPattern.IsMatch(input))
            //    throw new Exception("Email pattern invalid");
            return input;
        }
    }
}