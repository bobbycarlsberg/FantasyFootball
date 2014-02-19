using System;
using System.Collections.Generic;
using System.Linq;
using FantasyFootball.Model;
using FantasyFootball.Model.Interfaces;

namespace FantasyFootball.Model
{
    public partial class RankControllerBase : IRankController
    {
        private List<IPlayerRank> playerRanks;
        public List<IPlayerRank> PlayerRanks
        {
            get { return playerRanks; }
            set { playerRanks = value; }
        }

        public List<IPlayerRank> goalies { get; set; }
        public List<IPlayerRank> defenders { get; set; }
        public List<IPlayerRank> midfielders { get; set; }
        public List<IPlayerRank> forwards { get; set; }

        public virtual List<IPlayerRank> RankPlayers(List<Player> players, int NoFormWeeks, int currentGameweek,
                                                          int Gameweeks, string Position)
        {
            return null;
        }

        public void RankPositions()
        {
            playerRanks = playerRanks.Where(x => x.Player != null).OrderByDescending(x => x.FuturePoints).ToList();
            goalies = playerRanks.Where(x => x.Player.IsGoalKeeper).ToList();
            defenders = playerRanks.Where(x => x.Player.IsDefender).ToList();
            midfielders = playerRanks.Where(x => x.Player.IsMidfield).ToList();
            forwards = playerRanks.Where(x => x.Player.IsAttack).ToList();
        }

        public IPlayerRank GetCaptain(List<IPlayerRank> players, int currentGameweek)
        {
            return players.OrderByDescending(x => x.Predictions.FirstOrDefault(y => y.Fixture.GameWeek.No == currentGameweek).Points).FirstOrDefault();
        }

        public FFTeam DreamTeam(List<Player> players, int form, int currentGameweek,
                                                          int predictions)
        {
            var squad = new FFTeam();
            
            for (int i = 0; squad.Squad.Count < 15; i++)
            {
                squad.Squad.Add(playerRanks[i]);
            }

            return squad;
        }

        public FFTeam BudgetDreamTeam(List<Player> players, int NoFormWeeks, int currentGameweek,
                                                          int Gameweeks, double budget)
        {
            var squad = DreamTeam(players, NoFormWeeks, currentGameweek, Gameweeks);
            var bought = new List<IPlayerRank>();

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
                squad.Team.ForEach(x => alts.Add(PlayerComparison.CreatePlayerComparison(x, this, squad.Squad.ToList(), 0)));

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


        public FFTeam PickAndSuggest(List<Player> players, int form, int currentGameweek, double budget)
        {
            var team = new List<IPlayerRank>();

            for (int i = 0; team.Count < 15; i++)
            {
                team.Add(PlayerRanks.First(x => x.Player.ID == players[i].ID));
            }

            var alts = new List<PlayerComparison>();

            foreach (var playerRank in team)
            {
                alts.Add(PlayerComparison.CreatePlayerComparison(playerRank, this, team, budget));
            }

            var chosen = alts.OrderByDescending(x => x.value).FirstOrDefault();

            if (chosen.value > 0)
            {
                team.Remove(chosen.player);
                team.Add(chosen.alternative);
            }

            team = RankPlayers(team.Select(x => x.Player).ToList(), form, currentGameweek, 1, "");

            var ffTeam = new FFTeam();

            team = team.OrderByDescending(x => x.FuturePoints).ToList();

            team.ForEach(x => ffTeam.Squad.Add(PlayerRanks.First(y => y.Player.ID == x.Player.ID)));

            return ffTeam;
        }


        public virtual List<ITeamRank> RankTeams(List<Team> Teams, int NoFormWeeks, int currentGameweek, int Gameweeks, MatchDetailName matchDetailName)
        {
            return null;
        }


        public void RankPositions(List<Player> players, int NoFormWeeks, int currentGameweek, int Gameweeks, string Position)
        {
            PlayerRanks = RankPlayers(players, NoFormWeeks, currentGameweek, Gameweeks, Position);
            RankPositions();
        }
    }
}