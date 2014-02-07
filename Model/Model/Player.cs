using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootball.Model
{
    public class Player
    {
        public string Name { get; set; }
        public List<MatchPlayerDetails> MatchPlayerDetails { get; set; }
        private Team team;
        public Team Team
        {
            get { return team; }
            set
            {
                team = value;
                if (!team.Players.Contains(this))
                    team.Players.Add(this);
            }

        }
        public string Position { get; set; }
        public string PriceString { get; set; }
        public double Price { get; set; }
        public int ID { get; set; }

        public Player(string name, Team team)
        {
            MatchPlayerDetails = new List<MatchPlayerDetails>();
            Name = name;
            Team = team;
        }

        public int TotalPoints
        {
            get { return MatchPlayerDetails.Sum(x => x.MatchDetails.Where(y => y.Name == MatchDetailName.TP).Sum(y => (int)y.Value)); }
        }

        public bool IsGoalKeeper
        {
            get { return Position == "GKP"; }
        }

        public bool IsDefensive
        {
            get { return Position == "DEF" || Position == "GK"; }
        }

        public bool IsMidfield
        {
            get { return Position == "MID"; }
        }

        public bool IsAttack
        {
            get { return Position == "FWD"; }
        }

        public int Availability { get; set; }
    }
}
