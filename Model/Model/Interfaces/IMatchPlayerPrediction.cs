using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyFootball.Model;

namespace FantasyFootball.Model
{
    public interface IMatchPlayerPrediction
    {
        Fixture Fixture { get; }
        double Points { get; }
    }
}
