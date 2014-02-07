using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyFootball;
using FantasyFootball.Model;

namespace FantasyFootballTP.Ranking
{
    public class PlayerRank
    {
        public PlayerRank(Player player)
        {
            Player = player;
        }

        public void SetPoints(int gameWeek, int form, int futureGames)
        {
            var awayGames =
                Player.MatchPlayerDetails.Where(
                    x => x.Match.GameWeek.No >= gameWeek - form && x.Match.AwayTeam == Player.Team);
            var homeGames =
                Player.MatchPlayerDetails.Where(
                    x => x.Match.GameWeek.No >= gameWeek - form && x.Match.HomeTeam == Player.Team);

            if (awayGames.Any())
            {
                var awaypointsarray = awayGames.Where(x => int.Parse(x.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.MP).Value.ToString()) >= 60).Select(x => double.Parse(x.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.TP).Value.ToString()) - 2).Concat(awayGames.Where(x => int.Parse(x.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.MP).Value.ToString()) < 60).Select(x => double.Parse(x.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.TP).Value.ToString()) - 1));
                AwayPoints = awaypointsarray.Sum(x => (int)x);

                awayPointsSD = RankController.CalculateStdDev(awaypointsarray);

                AwayMinutes = awayGames.Sum(x => int.Parse(x.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.MP).Value.ToString()));

                AwayPointsPerGame = (((double)AwayPoints / AwayMinutes) * 90) + 2;
            }

            if (homeGames.Any())
            {
                HomePoints = (homeGames.Where(x => int.Parse(x.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.MP).Value.ToString()) >= 60).Sum(x => int.Parse(x.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.TP).Value.ToString()) - 2)) +
                             (homeGames.Where(x => int.Parse(x.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.MP).Value.ToString()) < 60).Sum(x => int.Parse(x.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.TP).Value.ToString()) - 1));

                homePointsSD = RankController.CalculateStdDev(homeGames.Where(x => int.Parse(x.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.MP).Value.ToString()) >= 60).Select(x => double.Parse(x.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.TP).Value.ToString()) - 2).Concat(homeGames.Where(x => int.Parse(x.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.MP).Value.ToString()) < 60).Select(x => double.Parse(x.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.TP).Value.ToString()) - 1)));

                HomeMinutes = homeGames.Sum(x => int.Parse(x.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.MP).Value.ToString()));

                HomePointsPerGame = (((double)HomePoints / HomeMinutes) * 90) + 2;
            }

            if (player.MatchPlayerDetails.Any(x => x.Match.GameWeek.No == gameWeek - 0))
                WillPlay = (double)player.MatchPlayerDetails.Where(x => x.Match.GameWeek.No == gameWeek - 0).Sum(x => int.Parse(x.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.MP).Value.ToString())) /
                           (player.MatchPlayerDetails.Count(x => x.Match.GameWeek.No == gameWeek - 0) * 90) * 0.5;

            if (player.MatchPlayerDetails.Any(x => x.Match.GameWeek.No == gameWeek - 1))
                WillPlay += (double)player.MatchPlayerDetails.Where(x => x.Match.GameWeek.No == gameWeek - 1).Sum(x => int.Parse(x.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.MP).Value.ToString())) /
                           (player.MatchPlayerDetails.Count(x => x.Match.GameWeek.No == gameWeek - 1) * 90) * 0.35;

            if (player.MatchPlayerDetails.Any(x => x.Match.GameWeek.No == gameWeek - 2))
                WillPlay += (double)player.MatchPlayerDetails.Where(x => x.Match.GameWeek.No == gameWeek - 2).Sum(x =>int.Parse(x.MatchDetails.FirstOrDefault(w => w.Name == MatchDetailName.MP).Value.ToString())) /
                       (player.MatchPlayerDetails.Count(x => x.Match.GameWeek.No == gameWeek - 2) * 90) * 0.15;

            WillPlay *= (double)player.Availability / 100;

            if (player.Name == "Agüero")
                WillPlay = 1;

            pointsPerPound = ((awayPointsPerGame + homePointsPerGame) / 2) / Player.Price;

            FormFixtures = player.MatchPlayerDetails.Where(x => x.Match.GameWeek.No >= gameWeek - form).ToList();

        }

        public void Predict(int gameWeek, int futureGames, RankController rankController)
        {
            var fixtures =
                player.Team.Fixtures.Where(x => x.GameWeek.No <= gameWeek + futureGames && x.GameWeek.No > gameWeek);

            Predictions =
                fixtures.Select(x => new MatchPlayerPrediction(x, this, rankController)).ToList();

            FuturePoints = predictions.Sum(x => x.Points);
        }

        private List<MatchPlayerPrediction> predictions;
        public List<MatchPlayerPrediction> Predictions
        {
            get { return predictions; }
            set { predictions = value; }
        }

        private List<MatchPlayerDetails> formFixtures;
        public List<MatchPlayerDetails> FormFixtures
        {
            get { return formFixtures; }
            set { formFixtures = value; }
        }

        private double homePointsSD;
        public double HomePointsSD
        {
            get { return homePointsSD; }
            set { homePointsSD = value; }
        }

        private double awayPointsSD;
        public double AwayPointsSD
        {
            get { return awayPointsSD; }
            set { awayPointsSD = value; }
        }

        private double futurePoints;
        public double FuturePoints
        {
            get { return futurePoints; }
            set { futurePoints = value; }
        }

        public double FuturePointsPerPound
        {
            get { return FuturePoints / player.Price; }
        }

        private int homeMinutes;
        public int HomeMinutes
        {
            get { return homeMinutes; }
            set { homeMinutes = value; }
        }

        private int awayMinutes;
        public int AwayMinutes
        {
            get { return awayMinutes; }
            set { awayMinutes = value; }
        }

        public int TotalMinutes
        {
            get { return awayMinutes + homeMinutes; }
        }

        private int homePoints;
        public int HomePoints
        {
            get { return homePoints; }
            set { homePoints = value; }
        }

        private int awayPoints;
        public int AwayPoints
        {
            get { return awayPoints; }
            set { awayPoints = value; }
        }

        private double pointsPerPound;
        public double PointsPerPound
        {
            get { return pointsPerPound; }
            set { pointsPerPound = value; }
        }

        private double homePointsPerGame;
        public double HomePointsPerGame
        {
            get { return homePointsPerGame; }
            set { homePointsPerGame = value; }
        }

        private double awayPointsPerGame;
        public double AwayPointsPerGame
        {
            get { return awayPointsPerGame; }
            set { awayPointsPerGame = value; }
        }

        public double PointsPerGame
        {
            get { return homeMinutes + awayMinutes > 360 ? (((double)(homePoints + awayPoints) / (homeMinutes + awayMinutes)) * 90) + 2 : 0; }
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
    }
}
