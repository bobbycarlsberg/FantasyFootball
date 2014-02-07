using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyFootball;
using FantasyFootball.Model;

namespace FantasyFootballTP.Ranking
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

            var awayGKPminutes = gkps.Sum(x => int.Parse(x.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.MP).Value.ToString()));
            var awayDefMinutes = defenders.Sum(x => int.Parse(x.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.MP).Value.ToString()));
            var awayMidMinutes = midfielders.Sum(x => int.Parse(x.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.MP).Value.ToString()));
            var awayFwdMinutes = attackers.Sum(x => int.Parse(x.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.MP).Value.ToString()));

            avgAwayGKPPoints = Math.Round((double)gkps.Sum(y => int.Parse(y.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.TP).Value.ToString())) / awayGKPminutes * 90, 5);
            avgAwayDefPoints = Math.Round((double)defenders.Sum(y => int.Parse(y.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.TP).Value.ToString())) / awayDefMinutes * 90, 5);
            avgAwayMidPoints = Math.Round((double)midfielders.Sum(y => int.Parse(y.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.TP).Value.ToString())) / awayMidMinutes * 90, 5);
            avgAwayFWDPoints = Math.Round((double)attackers.Sum(y => int.Parse(y.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.TP).Value.ToString())) / awayFwdMinutes * 90, 5); 

            var homeMatches = Team.Fixtures.Where(x => x.HomeTeam == Team && x.GameWeek.No <= gameWeek && x.GameWeek.No >= gameWeek - form);
            gkps = homeMatches.SelectMany(x => x.MatchPlayerDetails.Where(y => y.Player.IsGoalKeeper && y.Player.Team != Team));
            defenders = homeMatches.SelectMany(x => x.MatchPlayerDetails.Where(y => y.Player.IsDefensive && y.Player.Team != Team));
            midfielders = homeMatches.SelectMany(x => x.MatchPlayerDetails.Where(y => y.Player.IsMidfield && y.Player.Team != Team));
            attackers = homeMatches.SelectMany(x => x.MatchPlayerDetails.Where(y => y.Player.IsAttack && y.Player.Team != Team));

            var homeGKPMinutes = gkps.Sum(x => int.Parse(x.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.MP).Value.ToString()));
            var homeDefMinutes = defenders.Sum(x => int.Parse(x.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.MP).Value.ToString()));
            var homeMidMinutes = midfielders.Sum(x => int.Parse(x.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.MP).Value.ToString()));
            var homeFwdMinutes = attackers.Sum(x => int.Parse(x.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.MP).Value.ToString()));

            avghomeGKPPoints = Math.Round((double)gkps.Sum(y => int.Parse(y.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.TP).Value.ToString())) / homeGKPMinutes * 90, 5);
            avgHomeDefPoints = Math.Round((double)defenders.Sum(y => int.Parse(y.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.TP).Value.ToString())) / homeDefMinutes * 90, 5);
            avgHomeMidPoints = Math.Round((double)midfielders.Sum(y => int.Parse(y.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.TP).Value.ToString())) / homeMidMinutes * 90, 5); ;
            avgHomeFWDPoints = Math.Round((double)attackers.Sum(y => int.Parse(y.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.TP).Value.ToString())) / homeFwdMinutes * 90, 5); ;
        }

        public TeamRank(Team team)
        {
            Team = team;
        }
    }
}
