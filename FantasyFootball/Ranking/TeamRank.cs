using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyFootball.Model;
using FantasyFootball.Model.Interfaces;
using FantasyFootball.Ranking.MatchDetailsForm;

namespace FantasyFootball.Ranking
{
    public class TeamRank : ITeamRank
    {
        public TeamRank(Team team)
        {
            Team = team;
            AwayGKMatchDetailForms = new List<MatchDetailForm>();
            AwayDefMatchDetailForms = new List<MatchDetailForm>();
            awayMidMatchDetailForms = new List<MatchDetailForm>();
            awayFwdMatchDetailForms = new List<MatchDetailForm>();
            HomeGKMatchDetailForms = new List<MatchDetailForm>();
            HomeDefMatchDetailForms = new List<MatchDetailForm>();
            HomeMidMatchDetailForms = new List<MatchDetailForm>();
            HomeFwdMatchDetailForms = new List<MatchDetailForm>();

            homeMatchDetailForms = new List<MatchDetailForm>();
            awayMatchDetailForms = new List<MatchDetailForm>();
        }

        public Team Team { get; set; }

        private List<MatchDetailForm> awayGKMatchDetailForms;
        public List<MatchDetailForm> AwayGKMatchDetailForms
        {
            get { return awayGKMatchDetailForms; }
            set { awayGKMatchDetailForms = value; }
        }

        private List<MatchDetailForm> awayDefMatchDetailForms;
        public List<MatchDetailForm> AwayDefMatchDetailForms
        {
            get { return awayDefMatchDetailForms; }
            set { awayDefMatchDetailForms = value; }
        }

        private List<MatchDetailForm> awayMidMatchDetailForms;
        public List<MatchDetailForm> AwayMidMatchDetailForms
        {
            get { return awayMidMatchDetailForms; }
            set { awayMidMatchDetailForms = value; }
        }

        private List<MatchDetailForm> awayFwdMatchDetailForms;
        public List<MatchDetailForm> AwayFwdMatchDetailForms
        {
            get { return awayFwdMatchDetailForms; }
            set { awayFwdMatchDetailForms = value; }
        }

        private List<MatchDetailForm> homeGKMatchDetailForms;
        public List<MatchDetailForm> HomeGKMatchDetailForms
        {
            get { return homeGKMatchDetailForms; }
            set { homeGKMatchDetailForms = value; }
        }

        private List<MatchDetailForm> homeDefMatchDetailForms;
        public List<MatchDetailForm> HomeDefMatchDetailForms
        {
            get { return homeDefMatchDetailForms; }
            set { homeDefMatchDetailForms = value; }
        }

        private List<MatchDetailForm> homeMidMatchDetailForms;
        public List<MatchDetailForm> HomeMidMatchDetailForms
        {
            get { return homeMidMatchDetailForms; }
            set { homeMidMatchDetailForms = value; }
        }

        private List<MatchDetailForm> homeFwdMatchDetailForms;
        public List<MatchDetailForm> HomeFwdMatchDetailForms
        {
            get { return homeFwdMatchDetailForms; }
            set { homeFwdMatchDetailForms = value; }
        }

        private List<MatchDetailForm> homeMatchDetailForms;
        public List<MatchDetailForm> HomeMatchDetailForms
        {
            get { return homeMatchDetailForms; }
            set { homeMatchDetailForms = value; }
        }

        private List<MatchDetailForm> awayMatchDetailForms;
        public List<MatchDetailForm> AwayMatchDetailForms
        {
            get { return awayMatchDetailForms; }
            set { awayMatchDetailForms = value; }
        }

        public double GKHomePoints { get; set; }
        public double DefHomePoints { get; set; }
        public double MidHomePoints { get; set; }
        public double FwdHomePoints { get; set; }

        public double GKAwayPoints { get; set; }
        public double DefAwayPoints { get; set; }
        public double MidAwayPoints { get; set; }
        public double FwdAwayPoints { get; set; }

        public void SetPoints(int gameWeek, int form, MatchDetailName selectedAttribute)
        {
            var details = Team.Fixtures
                .Where(y => y.GameWeek.No <= gameWeek && y.GameWeek.No > gameWeek - form)
                .SelectMany(x => x.MatchPlayerDetails)
                .Where(x => x.MatchDetails.Any(z => z.Name == MatchDetailName.MP && int.Parse(z.Value.ToString()) > 0)).ToList();

            var homeDetails =
                details.Where(x => x.Match.HomeTeam == Team && x.Player.Team != Team).ToList();
            var awayDetails =
                details.Where(x => x.Match.AwayTeam == Team && x.Player.Team != Team).ToList();

            for (var i = 1; i <= (int)MatchDetailName.TP; i++)
            {
                homeGKMatchDetailForms.Add(new MatchDetailForm((MatchDetailName)i, homeDetails.Where(x => x.Player.IsGoalKeeper).SelectMany(x => x.MatchDetails)));
                homeDefMatchDetailForms.Add(new MatchDetailForm((MatchDetailName)i, homeDetails.Where(x => x.Player.IsDefender).SelectMany(x => x.MatchDetails)));
                homeMidMatchDetailForms.Add(new MatchDetailForm((MatchDetailName)i, homeDetails.Where(x => x.Player.IsMidfield).SelectMany(x => x.MatchDetails)));
                homeFwdMatchDetailForms.Add(new MatchDetailForm((MatchDetailName)i, homeDetails.Where(x => x.Player.IsAttack).SelectMany(x => x.MatchDetails)));
                homeMatchDetailForms.Add(new MatchDetailForm((MatchDetailName)i, homeDetails.SelectMany(x => x.MatchDetails)));
            }

            for (var i = 1; i <= (int)MatchDetailName.TP; i++)
            {
                awayGKMatchDetailForms.Add(new MatchDetailForm((MatchDetailName)i, awayDetails.Where(x => x.Player.IsGoalKeeper).SelectMany(x => x.MatchDetails)));
                awayDefMatchDetailForms.Add(new MatchDetailForm((MatchDetailName)i, awayDetails.Where(x => x.Player.IsDefender).SelectMany(x => x.MatchDetails)));
                awayMidMatchDetailForms.Add(new MatchDetailForm((MatchDetailName)i, awayDetails.Where(x => x.Player.IsMidfield).SelectMany(x => x.MatchDetails)));
                awayFwdMatchDetailForms.Add(new MatchDetailForm((MatchDetailName)i, awayDetails.Where(x => x.Player.IsAttack).SelectMany(x => x.MatchDetails)));
                awayMatchDetailForms.Add(new MatchDetailForm((MatchDetailName)i, awayDetails.SelectMany(x => x.MatchDetails)));
            }

            matches = details.Select(x => x.Match.GameWeek.No).Distinct().Count();

            GKAwayPoints = (double)awayGKMatchDetailForms.Where(x => x.Name == selectedAttribute).Sum(x => x.Total) / awayGKMatchDetailForms.FirstOrDefault(x => x.Name == selectedAttribute).MatchDetails.Count;
            DefAwayPoints = (double)awayDefMatchDetailForms.Where(x => x.Name == selectedAttribute).Sum(x => x.Total) / awayDefMatchDetailForms.FirstOrDefault(x => x.Name == selectedAttribute).MatchDetails.Count;
            MidAwayPoints = (double)awayMidMatchDetailForms.Where(x => x.Name == selectedAttribute).Sum(x => x.Total) / awayMidMatchDetailForms.FirstOrDefault(x => x.Name == selectedAttribute).MatchDetails.Count;
            FwdAwayPoints = (double)awayFwdMatchDetailForms.Where(x => x.Name == selectedAttribute).Sum(x => x.Total) / awayFwdMatchDetailForms.FirstOrDefault(x => x.Name == selectedAttribute).MatchDetails.Count;

            GKHomePoints = (double)homeGKMatchDetailForms.Where(x => x.Name == selectedAttribute).Sum(x => x.Total) / homeGKMatchDetailForms.FirstOrDefault(x => x.Name == selectedAttribute).MatchDetails.Count;
            DefHomePoints = (double)homeDefMatchDetailForms.Where(x => x.Name == selectedAttribute).Sum(x => x.Total) / homeDefMatchDetailForms.FirstOrDefault(x => x.Name == selectedAttribute).MatchDetails.Count;
            MidHomePoints = (double)homeMidMatchDetailForms.Where(x => x.Name == selectedAttribute).Sum(x => x.Total) / homeMidMatchDetailForms.FirstOrDefault(x => x.Name == selectedAttribute).MatchDetails.Count;
            FwdHomePoints = (double)homeFwdMatchDetailForms.Where(x => x.Name == selectedAttribute).Sum(x => x.Total) / homeFwdMatchDetailForms.FirstOrDefault(x => x.Name == selectedAttribute).MatchDetails.Count;
        }

        private int matches;
        public int Matches
        {
            get { return matches; }
        }
    }
}
