using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyFootball.Model;

namespace FantasyFootball.Ranking.MatchDetailsForm
{
    public class MatchDetailForm 
    {
        public MatchDetailForm(MatchDetailName name, IEnumerable<MatchDetail> matchDetails)
        {
            this.name = name;
            MatchDetails = matchDetails.Where(x => x.Name == Name).ToList();
            total = MatchDetails.Sum(x => int.Parse(x.Value.ToString()));
            average = (double)total / MatchDetails.Count;
            standardDeviation = RankController.CalculateStdDev(MatchDetails.Select(x => double.Parse(x.Value.ToString())));
        }

        private List<MatchDetail> matchDetails;
        public List<MatchDetail> MatchDetails
        {
            get { return matchDetails; }
            set { matchDetails = value; }
        }

        private MatchDetailName name;
        public MatchDetailName Name
        {
            get { return name; }
            set { name = value; }
        }

        private double average;
        public double Average
        {
            get { return average; }
            set { average = value; }
        }
        
        private double standardDeviation;
        public double StandardDeviation
        {
            get { return standardDeviation; }
            set { standardDeviation = value; }
        }

        private int total;
        public int Total
        {
            get { return total; }
            set { total = value; }
        }
    }
}
