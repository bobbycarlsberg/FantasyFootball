using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyFootball.Model;

namespace FantasyFootball.Ranking.MatchDetailsPredictors
{
    public class MinutesPlayedPredictor : IMatchDetails
    {
        private List<MatchDetail> matchDetails;
        public List<MatchDetail> MatchDetails
        {
            get { return matchDetails; }
            set { matchDetails = value; }
        }
        
        private double average;
        public double Average
        {
            get { return average; }
        }

        private double sd;
        public double SD
        {
            get { return sd; }
        }

        private double prediction;
        public double Prediction
        {
            get { return prediction; }
        }

        private string name;
        public string Name
        {
            get { return name; }
        }

        public void SetPoints()
        {
            average = matchDetails.Sum(x => (double)x.Value) / matchDetails.Count;
            sd = RankController.CalculateStdDev(matchDetails.Select(x => (double) x.Value));
        }

        public void SetPoints(string name, List<MatchDetail> matchDetails)
        {
            this.name = name;
            this.matchDetails = matchDetails.Where(x => x.Name == MatchDetailName.MP).ToList();

            SetPoints();
        }
    }
}
