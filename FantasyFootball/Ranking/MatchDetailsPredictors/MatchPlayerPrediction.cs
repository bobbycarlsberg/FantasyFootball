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

            if (teamRank.Team == fixture.AwayTeam)
            {
                if (Player.IsGoalKeeper)
                    matchDetailPredictors.ForEach(x => x.SetPoints(rankController.AwayGKTeamAverages, teamRank, teamRank.AwayGKMatchDetailForms, playerRank, playerRank.HomeMatchDetailForms));
                if (Player.IsDefensive)
                    matchDetailPredictors.ForEach(x => x.SetPoints(rankController.AwayDefTeamAverages, teamRank, teamRank.AwayDefMatchDetailForms, playerRank, playerRank.HomeMatchDetailForms));
                if (Player.IsMidfield)
                    matchDetailPredictors.ForEach(x => x.SetPoints(rankController.AwayMidTeamAverages, teamRank, teamRank.AwayMidMatchDetailForms, playerRank, playerRank.HomeMatchDetailForms));
                if (Player.IsAttack)
                    matchDetailPredictors.ForEach(x => x.SetPoints(rankController.AwayFwdTeamAverages, teamRank, teamRank.AwayFwdMatchDetailForms, playerRank, playerRank.HomeMatchDetailForms));
            }
            else if (teamRank.Team == fixture.HomeTeam)
            {
                if (Player.IsGoalKeeper)
                    matchDetailPredictors.ForEach(x => x.SetPoints(rankController.HomeGKTeamAverages, teamRank, teamRank.HomeGKMatchDetailForms, playerRank, playerRank.AwayMatchDetailForms));
                if (Player.IsDefensive)
                    matchDetailPredictors.ForEach(x => x.SetPoints(rankController.HomeDefTeamAverages, teamRank, teamRank.HomeDefMatchDetailForms, playerRank, playerRank.AwayMatchDetailForms));
                if (Player.IsMidfield)
                    matchDetailPredictors.ForEach(x => x.SetPoints(rankController.HomeMidTeamAverages, teamRank, teamRank.HomeMidMatchDetailForms, playerRank, playerRank.AwayMatchDetailForms));
                if (Player.IsAttack)
                    matchDetailPredictors.ForEach(x => x.SetPoints(rankController.HomeFwdTeamAverages, teamRank, teamRank.HomeFwdMatchDetailForms, playerRank, playerRank.AwayMatchDetailForms));
            }

            Prediction = matchDetailPredictors.Sum(x => x.Prediction);
            if (Double.IsNaN(Prediction))
                Prediction = 0;
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

        public static double SDProportionate(List<MatchDetailForm> teamAverages, List<MatchDetailForm> teamMatchDetailForms, List<MatchDetailForm> playerMatchDetailForms, MatchDetailName name)
        {
            var average = playerMatchDetailForms.FirstOrDefault(x => x.Name == name).Average;
            var sd = playerMatchDetailForms.FirstOrDefault(x => x.Name == name).StandardDeviation;
            var teamAverage = teamMatchDetailForms.FirstOrDefault(x => x.Name == name).Average;
            var teamsAverage = teamAverages.FirstOrDefault(x => x.Name == name).Average;
            var teamSd = teamAverages.FirstOrDefault(x => x.Name == name).StandardDeviation;

            if (teamSd != 0.0)
            {
                var weight = (teamAverage - teamsAverage)/teamSd;

                average += (weight*sd);
            }
            return average;
        }
    }
}
