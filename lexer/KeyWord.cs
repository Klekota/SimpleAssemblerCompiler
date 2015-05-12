using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lexer
{
    public class KeyWord
    {
        public KeyWord()
        {
            keyWord = "";
            id = 301;
        }
        public KeyWord(string word, int id)
        {
            keyWord = word;
            this.id = id;
        }
        public string keyWord;
        public int id;
    }
}
