using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lexer.SyntaxTree
{
    class XMLNodeToDGMLParser
    {
        public XMLNodeToDGMLParser()
        {
            XMLSyntaxTree = SerializeTables.DeseriaizeNode();
            nodes = new List<Node>();
            links = new List<Link>();
            nodes.Add(new Node(XMLSyntaxTree.Id, XMLSyntaxTree.value, XMLSyntaxTree.name.ToString()));
            graph = new Graph();
        }
        private XMLNode XMLSyntaxTree;
        private List<Node> nodes;
        private List<Link> links;
        private Graph graph;
        private void ParseNode(XMLNode parentNode)
        {
            foreach (var item in parentNode.nodes)
            {
                string label = item.name.ToString();
                if (item.value != "")
                {
                    label += ": ";
                    label += item.value;
                }
                nodes.Add(new Node() { Id = item.Id, Label = label, Value = item.value });
                links.Add(new Link() { Source = parentNode.Id, Target = item.Id });
                ParseNode(item);
            }
        }
        public Graph GetGraph()
        {
            ParseNode(XMLSyntaxTree);
            graph.Nodes = nodes.ToArray();
            graph.Links = links.ToArray();
            return graph;
        }
    }
}
