using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace lexer.SyntaxTree
{
    public class Node
    {
        [XmlAttribute]
        public string Id;
        [XmlAttribute]
        public string Value;
        [XmlAttribute]
        public string Label;

        public Node(string id, string value = "", string label = "")
        {
            this.Id = id;
            this.Value = value;
            this.Label = label;
        }
        public Node()
        {
        }
    }
}
