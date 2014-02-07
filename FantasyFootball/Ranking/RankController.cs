
using System;
using System.Collections.Generic;
using System.Linq;
using FantasyFootball.Model;
using FantasyFootball.Ranking.MatchDetailsForm;

namespace FantasyFootball.Ranking
{
    public class RankController
    {
        public List<TeamRank> TeamRanks { get; set; }
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
        
        private List<PlayerRank> playerRanks;
        public List<PlayerRank> PlayerRanks
        {
            get { return playerRanks; }
            set { playerRanks = value; }
        }
        
        public List<PlayerRank> goalies;
        public List<PlayerRank> defenders;
        public List<PlayerRank> midfielders;
        public List<PlayerRank> forwards;

        public List<TeamRank> RankTeams(List<Team> Teams, int NoFormWeeks, int currentGameweek, int Gameweeks, MatchDetailName matchDetailName)
        {
            selectedAttribute = matchDetailName;
            CurrentGameWeek = currentGameweek;
            FormWeeks = NoFormWeeks;
            PredictionWeeks = Gameweeks;
            
            return RankTeams(Teams);
        }

        private List<TeamRank> RankTeams(List<Team> Teams)
        {
            TeamRanks = new List<TeamRank>();
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
                awayTeamAverages.Add(new MatchDetailForm((MatchDetailName)i, TeamRanks.SelectMany(x => x.AwayMatchDetailForms.SelectMany(y => y.MatchDetails))));
                awayGKTeamAverages.Add(new MatchDetailForm((MatchDetailName)i, TeamRanks.SelectMany(x => x.AwayGKMatchDetailForms.SelectMany(y => y.MatchDetails))));
                awayDefTeamAverages.Add(new MatchDetailForm((MatchDetailName)i, TeamRanks.SelectMany(x => x.AwayDefMatchDetailForms.SelectMany(y => y.MatchDetails))));
                awayMidTeamAverages.Add(new MatchDetailForm((MatchDetailName)i, TeamRanks.SelectMany(x => x.AwayMidMatchDetailForms.SelectMany(y => y.MatchDetails))));
                awayFwdTeamAverages.Add(new MatchDetailForm((MatchDetailName)i, TeamRanks.SelectMany(x => x.AwayFwdMatchDetailForms.SelectMany(y => y.MatchDetails))));

                homeTeamAverages.Add(new MatchDetailForm((MatchDetailName)i, TeamRanks.SelectMany(x => x.HomeMatchDetailForms.SelectMany(y => y.MatchDetails))));
                homeGKTeamAverages.Add(new MatchDetailForm((MatchDetailName)i, TeamRanks.SelectMany(x => x.HomeGKMatchDetailForms.SelectMany(y => y.MatchDetails))));
                homeDefTeamAverages.Add(new MatchDetailForm((MatchDetailName)i, TeamRanks.SelectMany(x => x.HomeDefMatchDetailForms.SelectMany(y => y.MatchDetails))));
                homeMidTeamAverages.Add(new MatchDetailForm((MatchDetailName)i, TeamRanks.SelectMany(x => x.HomeMidMatchDetailForms.SelectMany(y => y.MatchDetails))));
                homeFwdTeamAverages.Add(new MatchDetailForm((MatchDetailName)i, TeamRanks.SelectMany(x => x.HomeFwdMatchDetailForms.SelectMany(y => y.MatchDetails))));
            }

            return TeamRanks.OrderBy(x => x.Team.LongName).ToList();
        }

        public List<PlayerRank> RankPlayers(List<Player> players, int NoFormWeeks, int currentGameweek,
                                                          int Gameweeks, string Position)
        {
            var rankedPlayers = new List<PlayerRank>();

            players.Where(x => x.Position != null && x.Position.Contains(Position) && x.Availability >= 50).ToList().ForEach(x =>
            {
                var tr = new PlayerRank(x);
                rankedPlayers.Add(tr);
                tr.SetPoints(currentGameweek, NoFormWeeks, Gameweeks);
                tr.Predict(currentGameweek, Gameweeks, this);
            });

            return rankedPlayers.OrderByDescending(x => x.FuturePoints).ToList();
        }

        public PlayerRank GetCaptain(List<PlayerRank> players, int currentGameweek)
        {
            return players.OrderByDescending(x => x.Predictions.FirstOrDefault(y => y.Fixture.GameWeek.No == currentGameweek).Prediction).FirstOrDefault();
        }

        public FFTeam DreamTeam(List<Player> players, int NoFormWeeks, int currentGameweek,
                                                          int Gameweeks)
        {
            var squad = new FFTeam();

            goalies = RankPlayers(players, NoFormWeeks, currentGameweek, Gameweeks, "GKP").OrderByDescending(x => x.FuturePoints).ToList();
            defenders = RankPlayers(players, NoFormWeeks, currentGameweek, Gameweeks, "DEF").OrderByDescending(x => x.FuturePoints).ToList();
            midfielders = RankPlayers(players, NoFormWeeks, currentGameweek, Gameweeks, "MID").OrderByDescending(x => x.FuturePoints).ToList();
            forwards = RankPlayers(players, NoFormWeeks, currentGameweek, Gameweeks, "FWD").OrderByDescending(x => x.FuturePoints).ToList();

            PlayerRanks = RankPlayers(players, NoFormWeeks, currentGameweek, Gameweeks, "").ToList();

            for (int i = 0; squad.Squad.Count < 15; i++)
            {
                if (playerRanks[i].WillPlay > 0.5)
                {
                    squad.Squad.Add(PlayerRanks[i]);
                }
            }

            return squad;
        }

        public FFTeam BudgetDreamTeam(List<Player> players, int NoFormWeeks, int currentGameweek,
                                                          int Gameweeks, double budget)
        {
            var squad = DreamTeam(players, NoFormWeeks, currentGameweek, Gameweeks);
            var bought = new List<PlayerRank>();

            if (squad.Cost > budget)
            {
                squad.Substitutes = squad.Substitutes.OrderBy(x => x.FuturePoints).ToList();

                for (int i = 0; squad.Cost > budget && i < squad.Substitutes.Count; i++)
                {
                    var sell = squad.Substitutes.FirstOrDefault(x => !bought.Contains(x));
                    var buy =
                        PlayerRanks.Where(x => x.Player.Position == sell.Player.Position && !bought.Contains(x))
                                   .OrderBy(x => x.Player.Price).ThenByDescending(x => x.FuturePoints)
                                   .FirstOrDefault();

                    squad.Squad.Remove(sell);
                    squad.Squad.Add(buy);
                    bought.Add(buy);
                }
            }

            while (squad.Cost > budget)
            {
                var alts = new List<PlayerComparison>();
                squad.Team.ForEach(x => alts.Add(PlayerComparison.CreatePlayerComparison(x, this, squad)));

                var chosen = alts.OrderBy(x => x.difference).FirstOrDefault();

                squad.Squad.Remove(chosen.player);
                squad.Squad.Add(chosen.alternative);
            }

            return squad;
        }

        public FFTeam PickAndSuggest(FFTeam ffTeam, int NoFormWeeks, int currentGameweek,
                                     double budget)
        {
            return DreamTeam(ffTeam.Squad.Select(x => x.Player).ToList(), NoFormWeeks, currentGameweek, 1);
        }

        public static double CalculateStdDev(IEnumerable<double> values)
        {
            double ret = 0;
            if (values.Count() > 1)
            {
                //Compute the Average      
                double avg = values.Average();
                //Perform the Sum of (value-avg)_2_2      
                double sum = values.Sum(d => Math.Pow(d - avg, 2));
                //Put it all together      
                ret = Math.Sqrt((sum) / (values.Count() - 1));
            }
            return ret;
        }
    }

    public struct PlayerComparison
    {
        public PlayerRank player;
        public PlayerRank alternative;
        public double difference;

        public PlayerComparison(PlayerRank p, PlayerRank alt)
        {
            player = p;
            alternative = alt;
            if (alt != null && alt.Player != null)
                difference = (p.FuturePoints - alt.FuturePoints) / (p.Player.Price - alt.Player.Price);
            else
            {
                difference = 999;
            }
        }

        public static PlayerComparison CreatePlayerComparison(PlayerRank playerRank, RankController rankController, FFTeam squad)
        {
            PlayerRank nextBest = new PlayerRank(null);

            if (playerRank.Player.IsGoalKeeper)
            {
                nextBest =
                    rankController.goalies.OrderByDescending(x => x.FuturePoints)
                           .FirstOrDefault(
                               x =>
                               !squad.goalies.Select(y => y.Player.ID).Contains(x.Player.ID) &&
                               x.Player.Price < playerRank.Player.Price && x.WillPlay > 0.5);
            }
            else if (playerRank.Player.IsDefensive)
            {
                nextBest =
                    rankController.defenders.OrderByDescending(x => x.FuturePoints)
                           .FirstOrDefault(
                               x =>
                               !squad.defenders.Select(y => y.Player.ID).Contains(x.Player.ID) &&
                               x.Player.Price < playerRank.Player.Price && x.WillPlay > 0.5);
            }
            else if (playerRank.Player.IsMidfield)
            {
                nextBest =
                    rankController.midfielders.OrderByDescending(x => x.FuturePoints)
                           .FirstOrDefault(
                               x =>
                               !squad.midfielders.Select(y => y.Player.ID).Contains(x.Player.ID) &&
                               x.Player.Price < playerRank.Player.Price && x.WillPlay > 0.5);
            }
            else if (playerRank.Player.IsAttack)
            {
                nextBest =
                    rankController.forwards.OrderByDescending(x => x.FuturePoints)
                           .FirstOrDefault(
                               x =>
                               !squad.forwards.Select(y => y.Player.ID).Contains(x.Player.ID) &&
                               x.Player.Price < playerRank.Player.Price && x.WillPlay > 0.5);
            }
                
            return new PlayerComparison(playerRank, nextBest);
        }
    }
}