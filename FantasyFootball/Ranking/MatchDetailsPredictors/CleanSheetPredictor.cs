using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyFootball.Model;
using FantasyFootball.Ranking.MatchDetailsForm;

namespace FantasyFootball.Ranking.MatchDetailsPredictors
{
    public class CleanSheetPredictor : IMatchDetailPredictor
    {
        private double prediction;
        public double Prediction
        {
            get { return prediction; }
            set { prediction = value; }
        }

        private MatchDetailName name = MatchDetailName.CS;
        public MatchDetailName Name
        {
            get { return name; }
        }

        public void SetPoints(List<MatchDetailForm> TeamAverages, TeamRank teamRank, List<MatchDetailForm> teamMatchDetailForms, PlayerRank playerRank, List<MatchDetailForm> playerMatchDetailForms)
        {
            var average = MatchPlayerPrediction.SDProportionate(TeamAverages, teamMatchDetailForms,
                                                                playerMatchDetailForms, name);

            if (playerRank.Player.IsGoalKeeper)
                prediction = average * 4;
            else if (playerRank.Player.IsDefensive)
                prediction = average * 4;
            else if (playerRank.Player.IsMidfield)
                prediction = average;
        }
    }
}
