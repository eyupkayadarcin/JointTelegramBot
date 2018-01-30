using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JointTelegramBot.Web.Models
{
    public class Twitter
    {
        [Key]
        public int Id { get; set; }
        public string TwitterUserName { get; set; }
        public bool Follow { get; set; }
    }
}
