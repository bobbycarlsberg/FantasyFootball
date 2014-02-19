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
    public class PlayerRank : PlayerRankBase
    {
        public PlayerRank(Player player):base(player)
        {
            awayMatchDetailForms = new List<MatchDetailForm>();
            homeMatchDetailForms = new List<MatchDetailForm>();
        }

        public void SetPoints(int gameWeek, int form, int futureGames)
        {
            WillPlay = 0;

            var awayGames =
                           Player.MatchPlayerDetails.Where(
                               x => x.Match.GameWeek.No >= gameWeek - form && x.Match.GameWeek.No < gameWeek && x.Match.AwayTeam == Player.Team).ToList();

            var homeGames =
                Player.MatchPlayerDetails.Where(
                    x => x.Match.GameWeek.No >= gameWeek - form && x.Match.GameWeek.No < gameWeek && x.Match.HomeTeam == Player.Team).ToList();

            for (var i = 1; i <= (int)MatchDetailName.TP; i++)
            {
                awayMatchDetailForms.Add(new MatchDetailForm((MatchDetailName)i, awayGames.OrderByDescending(x => x.Match.GameWeek.No).SelectMany(x => x.MatchDetails)));
            }

            for (var i = 1; i <= (int)MatchDetailName.TP; i++)
            {
                homeMatchDetailForms.Add(new MatchDetailForm((MatchDetailName)i, homeGames.OrderByDescending(x => x.Match.GameWeek.No).SelectMany(x => x.MatchDetails)));
            }

            for (int i = gameWeek; i > gameWeek - 5; i--)
            {
                if (Player.MatchPlayerDetails.Any(x => x.Match.GameWeek.No == i))
                {
                    WillPlay += ((double)Player.MatchPlayerDetails.FirstOrDefault(x => x.Match.GameWeek.No == i).MP / 90) *( i - (gameWeek - 5));
                }
            }

            WillPlay /= 15;
        }

        public void Predict(int gameWeek, int futureGames, RankController rankController)
        {
            var fixtures =
                Player.Team.Fixtures.Where(x => x.GameWeek.No <= gameWeek + futureGames && x.GameWeek.No >= gameWeek).ToList();

            Predictions = new List<IMatchPlayerPrediction>();

            fixtures.ForEach(x => Predictions.Add(new MatchPlayerPrediction(x, this, rankController)));

            if (Predictions.Any())
                FuturePoints = Predictions.Sum(x => x.Points);
            else
            {
                FuturePoints = 0;
            }
        }

        public double ActualFuturePoints(int firstGameWeek, int lastGameWeek)
        {
            return Player.MatchPlayerDetails.Where(x => x.Match.GameWeek.No >= firstGameWeek
                && x.Match.GameWeek.No <= lastGameWeek).Sum(x => x.TP);
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
    }
}
