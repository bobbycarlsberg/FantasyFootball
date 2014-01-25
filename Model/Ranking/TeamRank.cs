using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyFootball.Model;
using FantasyFootball.Ranking.MatchDetailsForm;

namespace FantasyFootball.Ranking
{
    public class TeamRank
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

        public void SetPoints(int gameWeek, int form)
        {
            var details = Team.Fixtures.Where(y => y.GameWeek.No <= gameWeek && y.GameWeek.No > gameWeek - form).SelectMany(x => x.MatchPlayerDetails).ToList();

            var homeDetails =
                details.Where(x => x.Match.HomeTeam == Team && x.Player.Team != Team).ToList();
            var awayDetails =
                details.Where(x => x.Match.AwayTeam == Team && x.Player.Team != Team).ToList();

            for (var i = 1; i < (int)MatchDetailName.TP; i++)
            {
                awayGKMatchDetailForms.Add(new MatchDetailForm((MatchDetailName)i, homeDetails.Where(x => x.Player.IsGoalKeeper).SelectMany(x => x.MatchDetails)));
                awayDefMatchDetailForms.Add(new MatchDetailForm((MatchDetailName)i, homeDetails.Where(x => x.Player.IsDefensive).SelectMany(x => x.MatchDetails)));
                awayMidMatchDetailForms.Add(new MatchDetailForm((MatchDetailName)i, homeDetails.Where(x => x.Player.IsMidfield).SelectMany(x => x.MatchDetails)));
                awayFwdMatchDetailForms.Add(new MatchDetailForm((MatchDetailName)i, homeDetails.Where(x => x.Player.IsAttack).SelectMany(x => x.MatchDetails)));
            }

            for (var i = 1; i < (int)MatchDetailName.TP; i++)
            {
                homeGKMatchDetailForms.Add(new MatchDetailForm((MatchDetailName)i, awayDetails.Where(x => x.Player.IsGoalKeeper).SelectMany(x => x.MatchDetails)));
                homeDefMatchDetailForms.Add(new MatchDetailForm((MatchDetailName)i, awayDetails.Where(x => x.Player.IsDefensive).SelectMany(x => x.MatchDetails)));
                homeMidMatchDetailForms.Add(new MatchDetailForm((MatchDetailName)i, awayDetails.Where(x => x.Player.IsMidfield).SelectMany(x => x.MatchDetails)));
                homeFwdMatchDetailForms.Add(new MatchDetailForm((MatchDetailName)i, awayDetails.Where(x => x.Player.IsAttack).SelectMany(x => x.MatchDetails)));
            }
        }
    }
}
