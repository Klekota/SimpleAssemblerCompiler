using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lexer
{
    public class Constant
    {
        public Constant()
        {
            value = 0.0;
            id = 501;
        }
        public Constant(double value, int id = 501)
        {
            this.value = value;
            this.id = id;
        }
        public double value;
        public int id;
    }
}
