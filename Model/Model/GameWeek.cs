using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootball.Model
{
    public class GameWeek
    {
        public int No { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public GameWeek()
        {
            
        }

        public GameWeek(int no, DateTime start, DateTime end)
        {
            this.EndDate = end;
            this.StartDate = start;
            this.No = no;
        }

        public static GameWeek GetGameWeek(List<GameWeek> gameWeeks, DateTime date)
        {
            return gameWeeks.FirstOrDefault(x => x.StartDate <= date && x.EndDate > date);
        }
    }
}
