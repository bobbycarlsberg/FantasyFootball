using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyFootball;
using FantasyFootball.Model;

namespace FantasyFootball.Model
{
    public class PlayerRankBase : IPlayerRank
    {
        public PlayerRankBase(Player player)
        {
            Player = player;
            predictions = new List<IMatchPlayerPrediction>();
        }

        private List<IMatchPlayerPrediction> predictions;
        public List<IMatchPlayerPrediction> Predictions
        {
            get { return predictions; }
            set { predictions = value; }
        }

        public double ActualFuturePoints(int firstGameWeek, int lastGameWeek)
        {
            return player.MatchPlayerDetails.Where(x => x.Match.GameWeek.No >= firstGameWeek
                && x.Match.GameWeek.No <= lastGameWeek).Sum(x => x.TP);
        }

        public double ActualFuturePoints(int gameWeek)
        {
            return player.MatchPlayerDetails.Where(x => x.Match.GameWeek.No == gameWeek).Sum(x => x.TP);
        }

        private Player player;
        public Player Player
        {
            get { return player; }
            set { player = value; }
        }

        public double WillPlay { get; set; }

        public int FormPoints(int weeks, int currentGameweek)
        {
            return Player.MatchPlayerDetails.Where(x => x.Match.GameWeek.No >= currentGameweek - weeks).Sum(x => int.Parse(x.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.TP).Value.ToString()));
        }

        public int HomeFormPoints(int weeks, int currentGameweek)
        {
            return Player.MatchPlayerDetails.Where(x => x.Match.GameWeek.No >= currentGameweek - weeks && x.Match.HomeTeam == Player.Team).Sum(x => int.Parse(x.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.TP).Value.ToString()));
        }

        public int AwayFormPoints(int weeks, int currentGameweek)
        {
            return Player.MatchPlayerDetails.Where(x => x.Match.GameWeek.No >= currentGameweek - weeks && x.Match.AwayTeam == Player.Team).Sum(x => int.Parse(x.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.TP).Value.ToString()));
        }

        private double futurePoints;
        public double FuturePoints
        {
            get { return futurePoints; }
            set { futurePoints = value; }
        }
    }
}
