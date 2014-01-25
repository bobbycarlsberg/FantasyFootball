using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyFootball.Model;
using FantasyFootball.Ranking.MatchDetailsForm;
using FantasyFootball.Ranking.MatchDetailsPredictors;

namespace FantasyFootball.Ranking
{
    public class PlayerRank
    {
        public PlayerRank(Player player)
        {
            Player = player;
            awayMatchDetailForms = new List<MatchDetailForm>();
            homeMatchDetailForms = new List<MatchDetailForm>();
            Predictions = new List<MatchPlayerPrediction>();
        }

        public void SetPoints(int gameWeek, int form, int futureGames)
        {
            var awayGames =
                           Player.MatchPlayerDetails.Where(
                               x => x.Match.GameWeek.No >= gameWeek - form && x.Match.AwayTeam == Player.Team).ToList();
            var homeGames =
                Player.MatchPlayerDetails.Where(
                    x => x.Match.GameWeek.No >= gameWeek - form && x.Match.HomeTeam == Player.Team).ToList();

            for (var i = 1; i <= (int)MatchDetailName.TP; i++)
            {
                awayMatchDetailForms.Add(new MatchDetailForm((MatchDetailName)i, awayGames.SelectMany(x => x.MatchDetails)));
            }

            for (var i = 1; i <= (int)MatchDetailName.TP; i++)
            {
                homeMatchDetailForms.Add(new MatchDetailForm((MatchDetailName)i, homeGames.SelectMany(x => x.MatchDetails)));
            }

        }

        public void Predict(int gameWeek, int futureGames, RankController rankController)
        {
            var fixtures =
                player.Team.Fixtures.Where(x => x.GameWeek.No <= gameWeek + futureGames && x.GameWeek.No > gameWeek);

            Predictions =
                fixtures.Select(x => new MatchPlayerPrediction(x, this, rankController)).ToList();

            FuturePoints = predictions.Sum(x => x.Prediction);
        }

        private List<MatchPlayerPrediction> predictions;
        public List<MatchPlayerPrediction> Predictions
        {
            get { return predictions; }
            set { predictions = value; }
        }

        private List<MatchDetailForm> awayMatchDetailForms;
        public List<MatchDetailForm> AwayMatchDetailForms
        {
            get { return awayMatchDetailForms; }
            set { awayMatchDetailForms = value; }
        }

        private List<MatchDetailForm> homeMatchDetailForms;
        public List<MatchDetailForm> HomeMatchDetailForms
        {
            get { return homeMatchDetailForms; }
            set { homeMatchDetailForms = value; }
        }

        private Player player;
        public Player Player
        {
            get { return player; }
            set { player = value; }
        }

        public double FuturePoints { get; set; }
        public double WillPlay { get; set; }
    }
}
