using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyFootball.Model;
using FantasyFootball.Ranking.MatchDetailsForm;

namespace FantasyFootball.Ranking.MatchDetailsPredictors
{
    public class GoalsConcededPredictor : IMatchDetailPredictor
    {
        private double prediction;
        public double Prediction
        {
            get { return prediction; }
            set { prediction = value; }
        }

        private MatchDetailName name = MatchDetailName.GC;
        public MatchDetailName Name
        {
            get { return name; }
        }

        public void SetPoints(RankController rankController, TeamRank teamRank, List<MatchDetailForm> teamMatchDetailForms, PlayerRank playerRank, List<MatchDetailForm> playerMatchDetailForms)
        {
            var average = MatchPlayerPrediction.SDProportionate(rankController, teamMatchDetailForms,
                                                                playerMatchDetailForms, name);

            if (playerRank.Player.IsGoalKeeper || playerRank.Player.IsDefensive)
                prediction = average / -2;
            else if (playerRank.Player.IsMidfield)
                prediction = average * -1;
        }
    }
}
