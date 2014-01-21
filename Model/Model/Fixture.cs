using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootball.Model
{
    public class Fixture
    {
        private Team homeTeam;
        public Team HomeTeam
        {
            get { return homeTeam; }
            set
            {
                homeTeam = value;
                if (!homeTeam.Fixtures.Contains(this))
                    homeTeam.Fixtures.Add(this);
            }
        }

        private Team awayTeam;
        public Team AwayTeam
        {
            get { return awayTeam; }
            set 
            { 
                awayTeam = value;
                if (!awayTeam.Fixtures.Contains(this))
                    awayTeam.Fixtures.Add(this);
            }
        }

        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        private GameWeek gameWeek;
        public GameWeek GameWeek
        {
            get { return gameWeek; }
            set { gameWeek = value; }
        }

        public Fixture()
        {
            matchPlayerDetails = new List<MatchPlayerDetails>();
        }

        private List<MatchPlayerDetails> matchPlayerDetails;
        public List<MatchPlayerDetails> MatchPlayerDetails
        {
            get { return matchPlayerDetails; }
            set { matchPlayerDetails = value; }
        }

        public override string ToString()
        {
            return GameWeek.No + "; " + HomeTeam.ShortName + " v " + AwayTeam.ShortName;
        }
    }
}
