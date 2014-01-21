
using System;
using System.Collections.Generic;
using System.Linq;
using FantasyFootball.Model;

namespace FantasyFootball
{
    public class RankController
    {
        public List<TeamRank> TeamRanks { get; set; }
        
        private List<PlayerRank> playerRanks;
        public List<PlayerRank> PlayerRanks
        {
            get { return playerRanks; }
            set { playerRanks = value; }
        }

        private double teamGKSDHome;
        public double TeamGKSDHome
        {
            get { return teamGKSDHome; }
            set { teamGKSDHome = value; }
        }

        private double teamDEFSDHome;
        public double TeamDEFSDHome
        {
            get { return teamDEFSDHome; }
            set { teamDEFSDHome = value; }
        }

        private double teamMIDSDHome;
        public double TeamMIDSDHome
        {
            get { return teamMIDSDHome; }
            set { teamMIDSDHome = value; }
        }

        private double teamFWDSDHome;
        public double TeamFWDSDHome
        {
            get { return teamFWDSDHome; }
            set { teamFWDSDHome = value; }
        }

        private double teamGKSDAway;
        public double TeamGKSDAway
        {
            get { return teamGKSDAway; }
            set { teamGKSDAway = value; }
        }

        private double teamDEFSDAway;
        public double TeamDEFSDAway
        {
            get { return teamDEFSDAway; }
            set { teamDEFSDAway = value; }
        }

        private double teamMIDSDAway;
        public double TeamMIDSDAway
        {
            get { return teamMIDSDAway; }
            set { teamMIDSDAway = value; }
        }

        private double teamFWDSDAway;
        public double TeamFWDSDAway
        {
            get { return teamFWDSDAway; }
            set { teamFWDSDAway = value; }
        }

        private double teamGKAVGHome;
        public double TeamGKAVGHome
        {
            get { return teamGKAVGHome; }
            set { teamGKAVGHome = value; }
        }

        private double teamDEFAVGHome;
        public double TeamDEFAVGHome
        {
            get { return teamDEFAVGHome; }
            set { teamDEFAVGHome = value; }
        }

        private double teamMIDAVGHome;
        public double TeamMIDAVGHome
        {
            get { return teamMIDAVGHome; }
            set { teamMIDAVGHome = value; }
        }

        private double teamFWDAVGHome;
        public double TeamFWDAVGHome
        {
            get { return teamFWDAVGHome; }
            set { teamFWDAVGHome = value; }
        }

        private double teamGKAVGAway;
        public double TeamGKAVGAway
        {
            get { return teamGKAVGAway; }
            set { teamGKAVGAway = value; }
        }

        private double teamDEFAVGAway;
        public double TeamDEFAVGAway
        {
            get { return teamDEFAVGAway; }
            set { teamDEFAVGAway = value; }
        }

        private double teamMIDAVGAway;
        public double TeamMIDAVGAway
        {
            get { return teamMIDAVGAway; }
            set { teamMIDAVGAway = value; }
        }

        private double teamFWDAVGAway;
        public double TeamFWDAVGAway
        {
            get { return teamFWDAVGAway; }
            set { teamFWDAVGAway = value; }
        }

        public List<PlayerRank> goalies;
        public List<PlayerRank> defenders;
        public List<PlayerRank> midfielders;
        public List<PlayerRank> forwards;

        public List<TeamRank> RankTeams(List<Team> Teams, int NoFormWeeks, int currentGameweek, int Gameweeks)
        {
            TeamRanks = new List<TeamRank>();

            Teams.ForEach(x =>
                {
                    var tr = new TeamRank(x);
                    TeamRanks.Add(tr);
                    tr.SetPoints(currentGameweek, NoFormWeeks);
                });

            teamGKSDHome = CalculateStdDev(TeamRanks.Select(x => x.AvghomeGKPPoints));
            teamDEFSDHome = CalculateStdDev(TeamRanks.Select(x => x.AvgHomeDefPoints));
            teamMIDSDHome = CalculateStdDev(TeamRanks.Select(x => x.AvgHomeMidPoints));
            teamFWDSDHome = CalculateStdDev(TeamRanks.Select(x => x.AvgHomeFWDPoints));
            teamGKSDAway = CalculateStdDev(TeamRanks.Select(x => x.AvgAwayGKPPoints));
            teamDEFSDAway = CalculateStdDev(TeamRanks.Select(x => x.AvgAwayDefPoints));
            teamMIDSDAway = CalculateStdDev(TeamRanks.Select(x => x.AvgAwayMidPoints));
            TeamFWDSDAway = CalculateStdDev(TeamRanks.Select(x => x.AvgAwayFWDPoints));

            teamGKAVGAway = TeamRanks.Sum(x => x.AvghomeGKPPoints) / TeamRanks.Count;
            teamDEFAVGAway = TeamRanks.Sum(x => x.AvgHomeDefPoints) / TeamRanks.Count;
            teamMIDAVGAway = TeamRanks.Sum(x => x.AvgHomeMidPoints) / TeamRanks.Count;
            TeamFWDAVGAway = TeamRanks.Sum(x => x.AvgHomeFWDPoints) / TeamRanks.Count;
            teamGKAVGHome = TeamRanks.Sum(x => x.AvgAwayGKPPoints) / TeamRanks.Count;
            teamDEFAVGHome = TeamRanks.Sum(x => x.AvgAwayDefPoints) / TeamRanks.Count;
            teamMIDAVGHome = TeamRanks.Sum(x => x.AvgAwayMidPoints) / TeamRanks.Count;
            teamFWDAVGHome = TeamRanks.Sum(x => x.AvgAwayFWDPoints) / TeamRanks.Count;

            return TeamRanks.OrderByDescending(x => x.AvgHomeFWDPoints + x.AvgAwayFWDPoints).ToList();
        }

        public List<PlayerRank> RankPlayers(List<Player> players, int NoFormWeeks, int currentGameweek,
                                                          int Gameweeks, string Position)
        {
            var rankedPlayers = new List<PlayerRank>();

            players.Where(x => x.Position != null && x.Position.Contains(Position)).ToList().ForEach(x =>
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
            return players.OrderByDescending(x => x.Predictions.FirstOrDefault(y => y.Fixture.GameWeek.No == currentGameweek).Points).FirstOrDefault();
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
                squad.Squad.Add(PlayerRanks[i]);
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
                               x.Player.Price < playerRank.Player.Price);
            }
            else if (playerRank.Player.IsDefensive)
            {
                nextBest =
                    rankController.defenders.OrderByDescending(x => x.FuturePoints)
                           .FirstOrDefault(
                               x =>
                               !squad.defenders.Select(y => y.Player.ID).Contains(x.Player.ID) &&
                               x.Player.Price < playerRank.Player.Price);
            }
            else if (playerRank.Player.IsMidfield)
            {
                nextBest =
                    rankController.midfielders.OrderByDescending(x => x.FuturePoints)
                           .FirstOrDefault(
                               x =>
                               !squad.midfielders.Select(y => y.Player.ID).Contains(x.Player.ID) &&
                               x.Player.Price < playerRank.Player.Price);
            }
            else if (playerRank.Player.IsAttack)
            {
                nextBest =
                    rankController.forwards.OrderByDescending(x => x.FuturePoints)
                           .FirstOrDefault(
                               x =>
                               !squad.forwards.Select(y => y.Player.ID).Contains(x.Player.ID) &&
                               x.Player.Price < playerRank.Player.Price);
            }
                return new PlayerComparison(playerRank, nextBest);
        }
    }
}