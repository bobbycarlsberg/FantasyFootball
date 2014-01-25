using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyFootball.Ranking;
using FantasyFootball.Ranking.MatchDetailsForm;
using FantasyFootball.Ranking.MatchDetailsPredictors;

namespace FantasyFootball.Model
{
    public class MatchPlayerPrediction
    {
        public MatchPlayerPrediction(Fixture fixture, PlayerRank playerRank, RankController rankController)
        {
            var teamRank = rankController.TeamRanks.FirstOrDefault(x => x.Team == (fixture.HomeTeam == playerRank.Player.Team ? fixture.AwayTeam : fixture.HomeTeam));
            Fixture = fixture;
            Player = playerRank.Player;

            matchDetailPredictors = new List<IMatchDetailPredictor>();

            matchDetailPredictors.Add(new MinutesPlayedPredictor());
            matchDetailPredictors.Add(new AssistPredictor());
            matchDetailPredictors.Add(new CleanSheetPredictor());
            matchDetailPredictors.Add(new GoalsScoredPredictor());
            matchDetailPredictors.Add(new GoalsConcededPredictor());
            matchDetailPredictors.Add(new OwnGoalPredictor());
            matchDetailPredictors.Add(new YellowCardPredictor());
            matchDetailPredictors.Add(new RedCardPredictor());
            matchDetailPredictors.Add(new SavesPredictor());
            matchDetailPredictors.Add(new BonusPredictor());

            if (teamRank.Team == fixture.HomeTeam)
            {
                matchDetailPredictors.ForEach(x => x.SetPoints(rankController, teamRank, teamRank.HomeMatchDetailForms, playerRank, playerRank.HomeMatchDetailForms));
            }
            else
            {
                matchDetailPredictors.ForEach(x => x.SetPoints(rankController, teamRank, teamRank.AwayMatchDetailForms, playerRank, playerRank.AwayMatchDetailForms));
            }

            Prediction = matchDetailPredictors.Sum(x => x.Prediction);
        }

        private List<IMatchDetailPredictor> matchDetailPredictors;
        public List<IMatchDetailPredictor> MatchDetailPredictors
        {
            get { return matchDetailPredictors; }
        } 

        private Fixture fixture;
        public Fixture Fixture
        {
            get { return fixture; }
            set { fixture = value; }
        }

        private Player player;
        public Player Player
        {
            get { return player; }
            set { player = value; }
        }

        private double prediction;
        public double Prediction
        {
            get { return prediction; }
            set { prediction = value; }
        }

        public static double Average(List<MatchDetailForm> teamMatchDetailForms, List<MatchDetailForm> playerMatchDetailForms, MatchDetailName name)
        {
            var average = playerMatchDetailForms.FirstOrDefault(x => x.Name == name).Average;
            var teamAverage = teamMatchDetailForms.FirstOrDefault(x => x.Name == name).Average;

            average += teamAverage;
            average /= 2;
            return average;
        }

        public static double SDProportionate(RankController rankController, List<MatchDetailForm> teamMatchDetailForms, List<MatchDetailForm> playerMatchDetailForms, MatchDetailName name)
        {
            var average = playerMatchDetailForms.FirstOrDefault(x => x.Name == name).Average;
            var sd = playerMatchDetailForms.FirstOrDefault(x => x.Name == name).StandardDeviation;
            var teamAverage = teamMatchDetailForms.FirstOrDefault(x => x.Name == name).Average;
            var teamsAverage = rankController.HomeTeamAverages.FirstOrDefault(x => x.Name == name).Average;

            var weight = teamAverage - teamsAverage;

            average += (weight * sd);
            return average;
        }
    }
}
