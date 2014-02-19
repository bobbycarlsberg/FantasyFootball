using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyFootball.Model;

namespace FantasyFootball.Model
{
    public interface IPlayerRank
    {
        Player Player { get; }
        double FuturePoints { get; }
        double WillPlay { get; }
        List<IMatchPlayerPrediction> Predictions { get; }
        double ActualFuturePoints(int firstGameWeek, int lastGameWeek);
        double ActualFuturePoints(int gameWeek);
    }
}
