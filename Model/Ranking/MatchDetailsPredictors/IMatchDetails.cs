using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootball.Ranking.MatchDetailsPredictors
{
    public interface IMatchDetails
    {
        double Average { get; }
        double SD { get; }
        string Name { get; }
    }
}
