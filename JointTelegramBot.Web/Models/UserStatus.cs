using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JointTelegramBot.Web.Models
{
    public class UserStatus
    {
        [Key]
        public int StatusId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string NewChatMember { get; set; }
        public string LeftChatMember { get; set; }
        public DateTime Date { get; set; }
    }
}
