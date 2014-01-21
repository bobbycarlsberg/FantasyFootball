using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootball.Model
{
    public class Team
    {
        private string longName;
        public string LongName
        {
            get { return longName; }
            set { longName = value; }
        }

        private string shortName;
        public string ShortName
        {
            get { return shortName; }
            set { shortName = value; }
        }

        private string abbreviation;
        public string Abbreviation
        {
            get { return abbreviation; }
            set { abbreviation = value; }
        }

        private List<Player> players;
        public List<Player> Players
        {
            get { return players; }
            set { players = value; }
        }

        private List<Fixture> fixtures;
        public List<Fixture> Fixtures
        {
            get { return fixtures; }
            set { fixtures = value; }
        }

        public int MatchCount
        {
            get { return Fixtures.Count(x => x.MatchPlayerDetails.Any()); }
        } 

        public Team()
        {
            players = new List<Player>();
            fixtures = new List<Fixture>();
        }

        private bool CompareTo(string name)
        {
            return longName == name || shortName == name || abbreviation == name;
        }

        public static Team GetTeam(List<Team> teams, string teamName)
        {
            return teams.FirstOrDefault(x => x.CompareTo(teamName));
        }
    }
}
