using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyFootball.Ranking;
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
            matchDetailPredictors.Add(new GoalsScoredPredictor());
            matchDetailPredictors.Add(new GoalsConcededPredictor());
            matchDetailPredictors.Add(new BonusPredictor());
            matchDetailPredictors.Add(new SavesPredictor());
            matchDetailPredictors.Add(new CleanSheetPredictor());

            if (teamRank.Team == fixture.AwayTeam)
            {
                matchDetailPredictors.ForEach(x => x.SetPoints(rankController, teamRank, teamRank.AwayGKMatchDetailForms, playerRank, playerRank.HomeMatchDetailForms));
            }
            else
            {
                matchDetailPredictors.ForEach(x => x.SetPoints(rankController, teamRank, teamRank.HomeGKMatchDetailForms, playerRank, playerRank.AwayMatchDetailForms));
            }

            Prediction = matchDetailPredictors.Sum(x => x.Prediction);
        }

        private List<IMatchDetailPredictor> matchDetailPredictors; 

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
    }
}
