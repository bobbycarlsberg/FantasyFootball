using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyFootball.Model;
using FantasyFootball.Ranking.MatchDetailsForm;

namespace FantasyFootball.Ranking.MatchDetailsPredictors
{
    public class AssistPredictor : IMatchDetailPredictor
    {
        private double prediction;
        public double Prediction
        {
            get { return prediction; }
            set { prediction = value; }
        }

        private MatchDetailName name = MatchDetailName.A;
        public MatchDetailName Name
        {
            get { return name; }
        }

        public void SetPoints(RankController rankController, TeamRank teamRank, List<MatchDetailForm> teamMatchDetailForms, PlayerRank playerRank, List<MatchDetailForm> playerMatchDetailForms)
        {
            var average = MatchPlayerPrediction.SDProportionate(rankController, teamMatchDetailForms,
                                                                playerMatchDetailForms, name);

            prediction = average * 3;
        }
    }
}
