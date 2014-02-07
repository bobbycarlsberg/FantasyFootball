using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootball.Model
{
    public class MatchPlayerDetails
    {
        private Player player;
        public Player Player
        {
            get { return player; }
            set { player = value; }
        }

        private Fixture match;
        public Fixture Match
        {
            get { return match; }
            set { match = value; }
        }

        public int MP
        {
            get
            {
                var y = matchDetails.FirstOrDefault(x => x.Name == MatchDetailName.MP);
                if (y.Value != null)
                    return int.Parse(y.Value.ToString());
                else
                {
                    return 0;
                }
            }
        }

        public int TP
        {
            get
            {
                var y = matchDetails.FirstOrDefault(x => x.Name == MatchDetailName.TP);
                if (y.Value != null)
                    return int.Parse(y.Value.ToString());
                else
                {
                    return 0;
                }
            }
        }

        private List<MatchDetail> matchDetails;
        public List<MatchDetail> MatchDetails
        {
            get { return matchDetails; }
            set { matchDetails = value; }
        }

        public MatchPlayerDetails(Player player, Fixture match)
        {
            this.player = player;
            this.match = match;
            match.MatchPlayerDetails.Add(this);
            matchDetails = new List<MatchDetail>();
        }
    }
}
