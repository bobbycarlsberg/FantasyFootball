using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyFootball;
using FantasyFootball.Model;
using FantasyFootballTP;

namespace Tester
{
    public class RankingComparer
    {
        ModelController modelController = new ModelController();
        public List<IRankController> rankControllers;

        private int gameWeekStart = 1;
        private int form = 1;
        private int formMax = 38;
        private int predictions = 1;
        private int predictionMax = 38;

        private RankingType[] points;
        public RankingType[] Points
        {
            get { return points; }
            set { points = value; }
        }

        public RankingComparer()
        {
            modelController.LoadPlayers();
            modelController.LoadPlayerDetails();
        }

        public RankingType[] Run()
        {
            rankControllers = new List<IRankController>();

            //rankControllers.Add(new FantasyFootball.Ranking.RankController());
            rankControllers.Add(new FantasyFootballTP.Ranking.RankControllerTP());

            points = new RankingType[(formMax - form) * rankControllers.Count * (predictionMax - predictions)];
            var pointCounter = 0;

            for (int k = predictions; k < predictionMax; k++)
            {
                for (int i = form; i < formMax; i++)
                {
                    foreach (var rankController in rankControllers)
                    {
                        rankController.RankTeams(modelController.Teams, i, gameWeekStart, k,
                                                 MatchDetailName.TP);
                        rankController.RankPositions(modelController.Players, i, gameWeekStart, k, "");
                        var team = rankController.DreamTeam(modelController.Players, i, gameWeekStart, k);
                        points[pointCounter] = new RankingType(i, k);

                        for (int j = gameWeekStart; j < 39; j++)
                        {
                            modelController.Players.ForEach(
                                x => x.Availability = x.MatchPlayerDetails.Any(y => y.Match.GameWeek.No == j) ? 1 : 0);
                            rankController.RankTeams(modelController.Teams, i, j, k, MatchDetailName.TP);
                            rankController.RankPositions(modelController.Players, i, j, k, "");
                            
                            team = rankController.PickAndSuggest(team.Squad.Select(x => x.Player).ToList(), i, j, 100);
                            points[pointCounter].Points += team.Team.Sum(x => x.ActualFuturePoints(i));
                        }

                        pointCounter++;
                    }
                }
            }

            return points;
        }
    }
}
