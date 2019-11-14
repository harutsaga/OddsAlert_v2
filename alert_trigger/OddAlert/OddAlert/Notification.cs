using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OddAlert
{
    public class Notification
    {
        public int id{ get; set; }
        public string event_id{ get; set; }
        public string datetime{ get; set; }
        public string time_to_jump{ get; set; }
        public int degree{ get; set; }
        public string state{ get; set; }
        public string venue{ get; set; }
        public int race_number{ get; set; }
        public int horse_number{ get; set; }
        public string horse_name{ get; set; }
        public decimal previous_price{ get; set; }
        public decimal current_price{ get; set; }
        public string bookie{ get; set; }
        public decimal suggested_stake{ get; set; }
        public decimal max_profit{ get; set; }
        public string top_price_1{ get; set; }
        public string top_price_2{ get; set; }
        public string top_price_3{ get; set; }
        public string top_price_4{ get; set; }
        public string account{ get; set; }
        public decimal price_taken{ get; set; }
        public decimal bet_amount{ get; set; }
        public decimal BF_SP{ get; set; }
        public int status{ get; set; } // 0 - pending, 1 - dismissed, 2 - ended
        public string result { get; set; }
    }
}
