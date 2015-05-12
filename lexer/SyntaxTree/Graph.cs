using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace lexer.SyntaxTree
{
    public class Graph
    {  
        public Graph()
        { }

        public Node[] Nodes;
        public Link[] Links;
    }
}
