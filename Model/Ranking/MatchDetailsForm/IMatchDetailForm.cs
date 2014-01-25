using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyFootball.Model;

namespace FantasyFootball.Ranking.MatchDetailsForm
{
    public interface IMatchDetailForm
    {
        MatchDetailName Name { get; }
        double Average { get; }
        double StandardDeviation { get; }
        double OppositionDifficulty { get; }
        void SetPoints(MatchDetailName name, List<MatchDetail> matchDetails);
    }
}
