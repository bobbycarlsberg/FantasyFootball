
using System;
using System.Collections.Generic;
using System.Linq;
using FantasyFootball.Model;
using FantasyFootball.Model.Interfaces;
using FantasyFootball.Ranking.MatchDetailsForm;

namespace FantasyFootball.Ranking
{
    public class RankController : RankControllerBase
    {
        public List<ITeamRank> TeamRanks { get; set; }
        public int CurrentGameWeek { get; set; }
        public int FormWeeks { get; set; }
        public int PredictionWeeks { get; set; }

        private MatchDetailName selectedAttribute;

        private List<MatchDetailForm> awayGKTeamAverages;
        public List<MatchDetailForm> AwayGKTeamAverages
        {
            get { return awayGKTeamAverages; }
            set { awayGKTeamAverages = value; }
        }

        private List<MatchDetailForm> awayDefTeamAverages;
        public List<MatchDetailForm> AwayDefTeamAverages
        {
            get { return awayDefTeamAverages; }
            set { awayDefTeamAverages = value; }
        }

        private List<MatchDetailForm> awayMidTeamAverages;
        public List<MatchDetailForm> AwayMidTeamAverages
        {
            get { return awayMidTeamAverages; }
            set { awayMidTeamAverages = value; }
        }

        private List<MatchDetailForm> awayFwdTeamAverages;
        public List<MatchDetailForm> AwayFwdTeamAverages
        {
            get { return awayFwdTeamAverages; }
            set { awayFwdTeamAverages = value; }
        }

        private List<MatchDetailForm> homeGKTeamAverages;
        public List<MatchDetailForm> HomeGKTeamAverages
        {
            get { return homeGKTeamAverages; }
            set { homeGKTeamAverages = value; }
        }

        private List<MatchDetailForm> homeDefTeamAverages;
        public List<MatchDetailForm> HomeDefTeamAverages
        {
            get { return homeDefTeamAverages; }
            set { homeDefTeamAverages = value; }
        }

        private List<MatchDetailForm> homeMidTeamAverages;
        public List<MatchDetailForm> HomeMidTeamAverages
        {
            get { return homeMidTeamAverages; }
            set { homeMidTeamAverages = value; }
        }

        private List<MatchDetailForm> homeFwdTeamAverages;
        public List<MatchDetailForm> HomeFwdTeamAverages
        {
            get { return homeFwdTeamAverages; }
            set { homeFwdTeamAverages = value; }
        }

        private List<MatchDetailForm> homeTeamAverages;
        public List<MatchDetailForm> HomeTeamAverages
        {
            get { return homeTeamAverages; }
            set { homeTeamAverages = value; }
        }

        private List<MatchDetailForm> awayTeamAverages;
        public List<MatchDetailForm> AwayTeamAverages
        {
            get { return awayTeamAverages; }
            set { awayTeamAverages = value; }
        }
        
        public override List<ITeamRank> RankTeams(List<Team> Teams, int form, int currentGameweek, int predictions, MatchDetailName matchDetailName)
        {
            selectedAttribute = matchDetailName;
            CurrentGameWeek = currentGameweek;
            FormWeeks = form;
            PredictionWeeks = predictions;
            
            return RankTeams(Teams);
        }

        private List<ITeamRank> RankTeams(List<Team> Teams)
        {
            TeamRanks = new List<ITeamRank>();
            Teams.ForEach(x =>
            {
                var tr = new TeamRank(x);
                TeamRanks.Add(tr);
                tr.SetPoints(CurrentGameWeek, FormWeeks, selectedAttribute);
            });

            awayGKTeamAverages = new List<MatchDetailForm>();
            awayDefTeamAverages = new List<MatchDetailForm>();
            awayMidTeamAverages = new List<MatchDetailForm>();
            awayFwdTeamAverages = new List<MatchDetailForm>();
            homeGKTeamAverages = new List<MatchDetailForm>();
            homeDefTeamAverages = new List<MatchDetailForm>();
            homeMidTeamAverages = new List<MatchDetailForm>();
            homeFwdTeamAverages = new List<MatchDetailForm>();

            awayTeamAverages = new List<MatchDetailForm>();
            homeTeamAverages = new List<MatchDetailForm>();

            for (var i = 1; i < (int)MatchDetailName.TP; i++)
            {
                awayTeamAverages.Add(new MatchDetailForm((MatchDetailName)i, TeamRanks.SelectMany(x => ((TeamRank)x).AwayMatchDetailForms.SelectMany(y => y.MatchDetails))));
                awayGKTeamAverages.Add(new MatchDetailForm((MatchDetailName)i, TeamRanks.SelectMany(x => ((TeamRank)x).AwayGKMatchDetailForms.SelectMany(y => y.MatchDetails))));
                awayDefTeamAverages.Add(new MatchDetailForm((MatchDetailName)i, TeamRanks.SelectMany(x => ((TeamRank)x).AwayDefMatchDetailForms.SelectMany(y => y.MatchDetails))));
                awayMidTeamAverages.Add(new MatchDetailForm((MatchDetailName)i, TeamRanks.SelectMany(x => ((TeamRank)x).AwayMidMatchDetailForms.SelectMany(y => y.MatchDetails))));
                awayFwdTeamAverages.Add(new MatchDetailForm((MatchDetailName)i, TeamRanks.SelectMany(x => ((TeamRank)x).AwayFwdMatchDetailForms.SelectMany(y => y.MatchDetails))));

                homeTeamAverages.Add(new MatchDetailForm((MatchDetailName)i, TeamRanks.SelectMany(x => ((TeamRank)x).HomeMatchDetailForms.SelectMany(y => y.MatchDetails))));
                homeGKTeamAverages.Add(new MatchDetailForm((MatchDetailName)i, TeamRanks.SelectMany(x => ((TeamRank)x).HomeGKMatchDetailForms.SelectMany(y => y.MatchDetails))));
                homeDefTeamAverages.Add(new MatchDetailForm((MatchDetailName)i, TeamRanks.SelectMany(x => ((TeamRank)x).HomeDefMatchDetailForms.SelectMany(y => y.MatchDetails))));
                homeMidTeamAverages.Add(new MatchDetailForm((MatchDetailName)i, TeamRanks.SelectMany(x => ((TeamRank)x).HomeMidMatchDetailForms.SelectMany(y => y.MatchDetails))));
                homeFwdTeamAverages.Add(new MatchDetailForm((MatchDetailName)i, TeamRanks.SelectMany(x => ((TeamRank)x).HomeFwdMatchDetailForms.SelectMany(y => y.MatchDetails))));
            }

            return TeamRanks.OrderBy(x => x.Team.LongName).ToList();
        }

        public override List<IPlayerRank> RankPlayers(List<Player> players, int NoFormWeeks, int currentGameweek,
                                                          int Gameweeks, string Position)
        {
            var tempPlayerRanks = new List<IPlayerRank>();

            players.Where(x => x.Position != null && x.Position.Contains(Position) && x.Availability != null).ToList().ForEach(x =>
            {
                var tr = new PlayerRank(x);
                tempPlayerRanks.Add(tr);
                tr.SetPoints(currentGameweek, NoFormWeeks, Gameweeks);
                tr.Predict(currentGameweek, Gameweeks, this);
            });
            
            return tempPlayerRanks.OrderByDescending(x => x.FuturePoints).ToList();
        }
    }
}