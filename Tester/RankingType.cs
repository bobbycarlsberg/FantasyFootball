using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    public class RankingType
    {
        private int form;
        public int Form
        {
            get { return form; }
            set { form = value; }
        }

        private int predictions;
        public int Predictions
        {
            get { return predictions; }
            set { predictions = value; }
        }
        
        private double points;
        public double Points
        {
            get { return points; }
            set { points = value; }
        }

        public RankingType(int f, int p)
        {
            form = f;
            predictions = p;
            points = 0;
        }
    }
}
