using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace lexer.SyntaxTree
{
    public class Link
    {
        [XmlAttribute]
        public string Source;
        [XmlAttribute]
        public string Target;
        [XmlAttribute]
        public string Label;

        public Link(string source, string target, string label = "")
        {
            this.Source = source;
            this.Target = target;
            this.Label = label;
        }
        public Link()
        {
        }
    }
}
