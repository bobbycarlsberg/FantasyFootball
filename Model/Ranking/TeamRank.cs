using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyFootball.Model;

namespace FantasyFootball.Ranking
{
    public class TeamRank
    {
        public Team Team { get; set; }

        private double avgAwayGKPPoints;
        public double AvgAwayGKPPoints
        {
            get { return avgAwayGKPPoints; }
            set { avgAwayGKPPoints = value; }
        }

        private double avghomeGKPPoints;
        public double AvghomeGKPPoints
        {
            get { return avghomeGKPPoints; }
            set { avghomeGKPPoints = value; }
        }

        private double avgAwayDefPoints;
        public double AvgAwayDefPoints
        {
            get { return avgAwayDefPoints; }
            set { avgAwayDefPoints = value; }
        }

        private double avgAwayMidPoints;
        public double AvgAwayMidPoints
        {
            get { return avgAwayMidPoints; }
            set { avgAwayMidPoints = value; }
        }

        private double avgAwayFWDPoints;
        public double AvgAwayFWDPoints
        {
            get { return avgAwayFWDPoints; }
            set { avgAwayFWDPoints = value; }
        }

        private double avgHomeDefPoints;
        public double AvgHomeDefPoints
        {
            get { return avgHomeDefPoints; }
            set { avgHomeDefPoints = value; }
        }

        private double avgHomeMidPoints;
        public double AvgHomeMidPoints
        {
            get { return avgHomeMidPoints; }
            set { avgHomeMidPoints = value; }
        }

        private double avgHomeFWDPoints;
        public double AvgHomeFWDPoints
        {
            get { return avgHomeFWDPoints; }
            set { avgHomeFWDPoints = value; }
        }

        public void SetPoints(int gameWeek, int form)
        {
            var awayMatches = Team.Fixtures.Where(x => x.AwayTeam == Team && x.GameWeek.No <= gameWeek && x.GameWeek.No >= gameWeek - form);
            var gkps = awayMatches.SelectMany(x => x.MatchPlayerDetails.Where(y => y.Player.IsGoalKeeper && y.Player.Team != Team));
            var defenders = awayMatches.SelectMany(x => x.MatchPlayerDetails.Where(y => y.Player.IsDefensive && y.Player.Team != Team));
            var midfielders = awayMatches.SelectMany(x => x.MatchPlayerDetails.Where(y => y.Player.IsMidfield && y.Player.Team != Team));
            var attackers = awayMatches.SelectMany(x => x.MatchPlayerDetails.Where(y => y.Player.IsAttack && y.Player.Team != Team));

            var awayGKPminutes = gkps.Sum(x => x.MP);
            var awayDefMinutes = defenders.Sum(x => x.MP);
            var awayMidMinutes = midfielders.Sum(x => x.MP);
            var awayFwdMinutes = attackers.Sum(x => x.MP);

            avgAwayGKPPoints = Math.Round((double)gkps.Sum(y => y.TP) / awayGKPminutes * 90, 5); 
            avgAwayDefPoints = Math.Round((double)defenders.Sum(y => y.TP) / awayDefMinutes * 90, 5);
            avgAwayMidPoints = Math.Round((double)midfielders.Sum(y => y.TP) / awayMidMinutes * 90, 5); 
            avgAwayFWDPoints = Math.Round((double)attackers.Sum(y => y.TP) / awayFwdMinutes * 90, 5); 

            var homeMatches = Team.Fixtures.Where(x => x.HomeTeam == Team && x.GameWeek.No <= gameWeek && x.GameWeek.No >= gameWeek - form);
            gkps = homeMatches.SelectMany(x => x.MatchPlayerDetails.Where(y => y.Player.IsGoalKeeper && y.Player.Team != Team));
            defenders = homeMatches.SelectMany(x => x.MatchPlayerDetails.Where(y => y.Player.IsDefensive && y.Player.Team != Team));
            midfielders = homeMatches.SelectMany(x => x.MatchPlayerDetails.Where(y => y.Player.IsMidfield && y.Player.Team != Team));
            attackers = homeMatches.SelectMany(x => x.MatchPlayerDetails.Where(y => y.Player.IsAttack && y.Player.Team != Team));

            var homeGKPMinutes = gkps.Sum(x => x.MP);
            var homeDefMinutes = defenders.Sum(x => x.MP);
            var homeMidMinutes = midfielders.Sum(x => x.MP);
            var homeFwdMinutes = attackers.Sum(x => x.MP);

            avghomeGKPPoints = Math.Round((double)gkps.Sum(y => y.TP) / homeGKPMinutes * 90, 5);
            avgHomeDefPoints = Math.Round((double)defenders.Sum(y => y.TP) / homeDefMinutes * 90, 5);
            avgHomeMidPoints = Math.Round((double)midfielders.Sum(y => y.TP) / homeMidMinutes * 90, 5); ;
            avgHomeFWDPoints = Math.Round((double)attackers.Sum(y => y.TP) / homeFwdMinutes * 90, 5); ;
        }

        public TeamRank(Team team)
        {
            Team = team;
        }
    }
}
