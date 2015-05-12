using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace lexer.SyntaxTree
{
    public class XMLNode
    {
        private static int id = 0;
        public XMLNode()
        {
            name = nodesTypes.node;
            value = "";
            nodes = new List<XMLNode>();
            Id = id++.ToString();
        }

        public XMLNode(nodesTypes name)
        {
            this.name = name;
            value = "";
            nodes = new List<XMLNode>();
            Id = id++.ToString();
        }
        [XmlAttribute]
        public string Id;
        [XmlAttribute]
        public nodesTypes name;
        [XmlAttribute]
        public string value;
        public List<XMLNode> nodes;

        public XMLNode AddNode(XMLNode node)
        {
            nodes.Add(node);
            return node;
        }
    }
}
