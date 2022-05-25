using System;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
namespace QuintrixHomeworkPlayerMVP.Models
{
    public class Bot
    {
        [Key]
        public Guid Id{get;set;}
        public string Name{get;set;}
        private string Personality{get;set;}
        #pragma warning disable CS8618
        public Bot(){}
        #pragma warning restore CS8618
    }
}