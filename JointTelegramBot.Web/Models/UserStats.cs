using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JointTelegramBot.Web.Models
{
    public class UserStats
    {
        [Key]
        public int StatsId { get; set; }
        public int UserId { get; set; }
        public int RefLink { get; set; }
        public Statu Statu { get; set; }
        public User User { get; set; }
    }
    public enum Statu
    {
        New,
        Left,
        ComeBack
    }
}
