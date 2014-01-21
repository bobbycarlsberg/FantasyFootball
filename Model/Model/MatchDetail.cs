using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootball.Model
{
    public class MatchDetail
    {
        public MatchDetailName Name { get; set; }
        public object Value { get; set; }

        public MatchDetail(MatchDetailName name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}
