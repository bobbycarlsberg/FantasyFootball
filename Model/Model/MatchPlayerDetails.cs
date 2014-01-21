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

        private List<MatchDetail> matchDetails;
        public List<MatchDetail> MatchDetails
        {
            get { return matchDetails; }
            set { matchDetails = value; }
        } 

        public int MP { get; set; }
        public int GS { get; set; }
        public int A { get; set; }
        public int CS { get; set; }
        public int GC { get; set; }
        public int OG { get; set; }
        public int PS { get; set; }
        public int PM { get; set; }
        public int YC { get; set; }
        public int RC { get; set; }
        public int B { get; set; }
        public int S { get; set; }
        public int ESP { get; set; }
        public int BPS { get; set; }
        public int TP { get; set; }

        public MatchPlayerDetails(Player player, Fixture match)
        {
            this.player = player;
            this.match = match;
            match.MatchPlayerDetails.Add(this);
            matchDetails = new List<MatchDetail>();
        }
    }
}
