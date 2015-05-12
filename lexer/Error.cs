using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lexer
{
    class Error
    {
        public Error()
        {
            this.message = "";
            this.row = 0;
            this.pos = 0;
        }
        public Error(string message, int row, int pos)
        {
            this.message = message;
            this.row = row;
            this.pos = pos;
        }
        public string message;
        public int row;
        public int pos; // symbol position
    }
}
