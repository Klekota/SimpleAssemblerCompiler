using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lexer
{
    public enum identifierType { user, system };
    public class Identifier
    {
        public Identifier()
        {
            name = "";
            id = 401;
            type = identifierType.user;
        }
        public Identifier(string name, identifierType type, int id = 401)
        {
            this.name = name;
            this.id = id;
            this.type = type;
        }
        public string name;
        public int id;
        public identifierType type;
    }
}
