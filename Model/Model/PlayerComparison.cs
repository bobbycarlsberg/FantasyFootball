using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootball.Model
{
    public struct PlayerComparison
    {
        public IPlayerRank player;
        public IPlayerRank alternative;
        public double difference;
        public double value;

        public PlayerComparison(IPlayerRank p, IPlayerRank alt)
        {
            player = p;
            alternative = alt;
            if (alt != null && alt.Player != null)
            {
                difference = (alt.FuturePoints - p.FuturePoints);
                if (difference < 0)
                {
                    value = -999999;
                }
                else
                {
                    value = (alt.FuturePoints / alt.Player.Price) - (p.FuturePoints / p.Player.Price);
                }
            }
            else
            {
                difference = 0;
                value = 0;
            }
        }

        public static PlayerComparison CreatePlayerComparison(IPlayerRank playerRank, IRankController rankController, List<IPlayerRank> squad, double budget)
        {
            IPlayerRank nextBest;

            if (playerRank.Player.IsGoalKeeper)
            {
                nextBest =
                    rankController.goalies.OrderByDescending(x => x.FuturePoints)
                           .FirstOrDefault(
                               x =>
                               !squad.Select(y => y.Player.ID).Contains(x.Player.ID) &&
                               x.Player.Price < playerRank.Player.Price + budget && x.WillPlay > 0.5);
            }
            else if (playerRank.Player.IsDefender)
            {
                nextBest =
                    rankController.defenders.OrderByDescending(x => x.FuturePoints)
                           .FirstOrDefault(
                               x =>
                               !squad.Select(y => y.Player.ID).Contains(x.Player.ID) &&
                               x.Player.Price < playerRank.Player.Price + budget && x.WillPlay > 0.5);
            }
            else if (playerRank.Player.IsMidfield)
            {
                nextBest =
                    rankController.midfielders.OrderByDescending(x => x.FuturePoints)
                           .FirstOrDefault(
                               x =>
                               !squad.Select(y => y.Player.ID).Contains(x.Player.ID) &&
                               x.Player.Price < playerRank.Player.Price + budget && x.WillPlay > 0.5);
            }
            else
            {
                nextBest =
                    rankController.forwards.OrderByDescending(x => x.FuturePoints)
                           .FirstOrDefault(
                               x =>
                               !squad.Select(y => y.Player.ID).Contains(x.Player.ID) &&
                               x.Player.Price < playerRank.Player.Price + budget && x.WillPlay > 0.5);
            }

            return new PlayerComparison(playerRank, nextBest);
        }
    }
}
