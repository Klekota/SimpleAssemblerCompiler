using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lexer
{
    public enum nodesTypes
    {
        node,
        token,
        procedure_idn,
        var_declar,
        declar_list,
        attribute,
        var_idn,
        conditional_statement,
        incomplete_conditional_statement,
        alternative_part,
        conditional_expression,
        expression,
        ///////////////////////////
        program,
        segment_list,
        segment_declar,
        block,
        declaration_list,
        statement_list,
        declaration,
        format_part,
        statement,
        fsub_statement,
        base_index_adress,
        register32,
        register16,
        fmul_statement,
        identifier_st,
        fcomp_statement,
        right_formatted_constant,
        segment_identifier,
        end_program,
        entry_point,
        entry_point_identifier,
        st_argument,
        fldz_statement
    }
    class SyntaxAnalizer
    {
        public SyntaxAnalizer(List<LexicalAnalizerOutput> lexems, List<Constant> constants, List<Identifier> identifiers, List<KeyWord> keyWords)
        {
            errors = new List<Error>();
            this.lexems = lexems;
            this.constants = constants;
            this.identifiers = identifiers;
            this.identifiersExtended = new List<IdentifierExt>();
            this.keyWords = keyWords;
            graphNodes = new List<SyntaxTree.Node>();
            links = new List<SyntaxTree.Link>();

            program = new SyntaxTree.XMLNode(nodesTypes.program);
            //graphNodes.Add(new SyntaxTree.Node(nodesTypes.program));
            positionInLexems = -1;

            
        }

        private List<Error> errors;
        private List<LexicalAnalizerOutput> lexems;
        private List<Constant> constants;
        private List<Identifier> identifiers;
        private List<KeyWord> keyWords;
        private SyntaxTree.XMLNode program;
        private int positionInLexems; // current pos in lexems
        private List<IdentifierExt> identifiersExtended;

        public delegate void WorkDoneHandler(List<Error> errors, List<IdentifierExt> identifiersExt);
        public event WorkDoneHandler WorkDone;

        private List<SyntaxTree.Node> graphNodes;
        private List<SyntaxTree.Link> links;

        private void AddGraphNode(SyntaxTree.Node g)
        {
            graphNodes.Add(g);
        }

        private void AddLink(SyntaxTree.Link l)
        {
            links.Add(l);
        }

        private void CreateGraphLabels()
        {
            foreach (var item in graphNodes)
            {
                if (item.Value != "")
                    item.Label = item.Id.ToString() + " " + item.Value;
                else
                    item.Label = item.Id.ToString();
            }
        }

        private LexicalAnalizerOutput GetNextToken()
        {
            positionInLexems++;
            if (positionInLexems < lexems.Count)
                return lexems[positionInLexems];
            else return new LexicalAnalizerOutput() { code = -1, row = -1, lexem = ""}; // end of program
        }

        private bool ParseProgram()
        {
            SyntaxTree.XMLNode currentNode = program;

            if (ParseSegmentDeclar(currentNode))
            {
                if (ParseSegmentList())
                {
                    if (ParseEndProgram())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool ParseSegmentDeclar(SyntaxTree.XMLNode curr)
        {
            SyntaxTree.XMLNode currentNode = curr.AddNode(new SyntaxTree.XMLNode() { name = nodesTypes.segment_declar });
            if (ParseSegmentIdentifier(currentNode))
            {
                LexicalAnalizerOutput currentToken = GetNextToken();
                if (currentToken.lexem == "SEGMENT")
                {
                    currentNode.AddNode(new SyntaxTree.XMLNode() { name = nodesTypes.token, value = currentToken.lexem });
                    if (ParseBlock(currentNode))
                    {
                        if (ParseSegmentIdentifier(currentNode))
                        {
                            currentToken = GetNextToken();
                            if (currentToken.lexem == "ENDS")
                            {
                                currentNode.AddNode(new SyntaxTree.XMLNode() { name = nodesTypes.token, value = currentToken.lexem });
                            }
                            else
                                errors.Add(new Error { message = "**Error** Expected 'ENDS' keyword", row = currentToken.row });
                        }
                    }
                }
                else
                    errors.Add(new Error { message = "**Error** Expected 'SEGMENT' keyword", row = currentToken.row });
            }
            curr.nodes.Remove(currentNode);
            return false;
        }

        private bool ParseBlock(SyntaxTree.XMLNode curr)
        {
            SyntaxTree.XMLNode currentNode = curr.AddNode(new SyntaxTree.XMLNode() { name = nodesTypes.block });

            if (ParseStatementList(currentNode))
            { 
                
            }

            return false;
        }

        private bool ParseSegmentIdentifier(SyntaxTree.XMLNode curr)
        {
            LexicalAnalizerOutput expectedIdentfier = ParseIdentifier();
            if (expectedIdentfier.lexem != "")
            {
                SyntaxTree.XMLNode currentNode = curr.AddNode(new SyntaxTree.XMLNode() { name = nodesTypes.segment_identifier })
                                                     .AddNode(new SyntaxTree.XMLNode() { name = nodesTypes.token, value = expectedIdentfier.lexem });
                return true;
            }
            else
                errors.Add(new Error { message = "**Error** Expected identifier", row = expectedIdentfier.row });
            return false;
        }


        private bool ParseSegmentList()
        {
            return false;
        }

        private bool ParseEndProgram()
        {
            return false;
        }

        private bool ParseProcedureIdn()
        {
            LexicalAnalizerOutput identifier = ParseIdentifier();
            if (identifier.lexem != "")
            {
                program.AddNode(new SyntaxTree.XMLNode() { name = nodesTypes.procedure_idn, value = identifier.lexem });
                //AddGraphNode(new SyntaxTree.Node(nodesTypes.procedure_idn, identifier.lexem));
                //AddLink(new SyntaxTree.Link(nodesTypes.program, nodesTypes.procedure_idn));
                return true;
            }
            else
            {
                errors.Add(new Error { message = "**Error** Expected user identifier", row = identifier.row });
                return false;
            }
        }

        private LexicalAnalizerOutput ParseIdentifier() // return empty string if not parsed else return value
        {
            LexicalAnalizerOutput currentToken = GetNextToken();
            if (identifiers.Find(x => x.id == currentToken.code && x.type != identifierType.system) != null)
                return currentToken;
            else
            {
                //errors.Add(new Error { message = "**Error** Expected user identifier", row = currentToken.row });
                return new LexicalAnalizerOutput() { lexem = "" };
            }
        }

        

        private bool ParseStatementList(SyntaxTree.XMLNode curr)
        {
            SyntaxTree.XMLNode currentNode = curr.AddNode(new SyntaxTree.XMLNode() { name = nodesTypes.statement_list });

            if (ParseStatement(currentNode))
                ParseStatementList(currentNode);
            else
            {
                positionInLexems--;
                curr.nodes.Remove(currentNode);
            }
                

            //positionInLexems--;  
            return true;
        }

        private bool ParseStatement(SyntaxTree.XMLNode curr)
        {
            SyntaxTree.XMLNode currentNode = curr.AddNode(new SyntaxTree.XMLNode() { name = nodesTypes.statement });

            if (ParseEntryPoint(currentNode))
            {
                return true;
            }

            return false;
        }

        private bool ParseEntryPoint(SyntaxTree.XMLNode curr)
        {
            LexicalAnalizerOutput expectedIdentifier = ParseIdentifier();

            if (expectedIdentifier.lexem != "")
            {
                LexicalAnalizerOutput currentToken = GetNextToken();
                if (currentToken.lexem == ":")
                {
                    SyntaxTree.XMLNode currentNode = curr.AddNode(new SyntaxTree.XMLNode() { name = nodesTypes.entry_point })
                                                         .AddNode(new SyntaxTree.XMLNode() { name = nodesTypes.entry_point_identifier })
                                                         .AddNode(new SyntaxTree.XMLNode() { name = nodesTypes.token, value = expectedIdentifier.lexem });
                    return true;
                }
            }
            return false;
        }

        private bool parseConditionStatement(SyntaxTree.XMLNode curr)
        {
            SyntaxTree.XMLNode currentNode = curr.AddNode(new SyntaxTree.XMLNode() { name = nodesTypes.conditional_statement});
            if (parseIncompleteConditionStatement(currentNode))
            {
                if (parseAlternativePart(currentNode))
                    return true;
            }
            return false;
        }

        private bool parseIncompleteConditionStatement(SyntaxTree.XMLNode curr)
        {
            SyntaxTree.XMLNode currentNode = curr.AddNode(new SyntaxTree.XMLNode() { name = nodesTypes.incomplete_conditional_statement });
            // IF conditionalExpression THEN statement_list
            LexicalAnalizerOutput currentToken = GetNextToken();

            if (currentToken.lexem == "IF")
            {
                currentNode.AddNode(new SyntaxTree.XMLNode() { name = nodesTypes.token, value = currentToken.lexem });
                if (parseconditionalExpression(currentNode))
                {
                    currentToken = GetNextToken();
                    if (currentToken.lexem == "THEN")
                    {
                        currentNode.AddNode(new SyntaxTree.XMLNode() { name = nodesTypes.token, value = currentToken.lexem });
                        if (parseStatementList(currentNode))
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    //errors.Add(new Error { message = "**Error** conditional statement expected ", row = currentToken.row });
                    return false;
                }
            }

            return false;
        }

        private bool parseconditionalExpression(SyntaxTree.XMLNode curr)
        {
            SyntaxTree.XMLNode currentNode = curr.AddNode(new SyntaxTree.XMLNode() { name = nodesTypes.conditional_expression });
            if (parseExpression(currentNode))
            {
                LexicalAnalizerOutput currentToken = GetNextToken();
                if (currentToken.lexem == "=")
                {
                    currentNode.AddNode(new SyntaxTree.XMLNode() { name = nodesTypes.token, value = currentToken.lexem });
                    if (parseExpression(currentNode))
                        return true;
                }
                else
                    errors.Add(new Error { message = "**Error** Expected '=' ", row = currentToken.row });
            }
            return false;
        }

        private bool parseExpression(SyntaxTree.XMLNode curr)
        {
            SyntaxTree.XMLNode currentNode = curr.AddNode(new SyntaxTree.XMLNode() { name = nodesTypes.expression });
            LexicalAnalizerOutput currentToken = GetNextToken();

            if (constants.Find(x => x.id == currentToken.code) != null)
            {
                currentNode.value = currentToken.lexem;
                return true;
            }
                

            if (identifiersExtended.Find(x => x.name == currentToken.lexem) != null)
            {
                currentNode.value = currentToken.lexem;
                return true;
            }
            else
                errors.Add(new Error { message = "**Error** Undeclared identifier", row = currentToken.row });
            
            errors.Add(new Error { message = "**Error** Expected identifier or constant", row = currentToken.row });
            return false;   
        }

        private bool parseAlternativePart(SyntaxTree.XMLNode curr)
        {
            SyntaxTree.XMLNode currentNode = curr.AddNode(new SyntaxTree.XMLNode() { name = nodesTypes.alternative_part });
            LexicalAnalizerOutput currentToken = GetNextToken();
            if (currentToken.lexem == "ELSE")
            {
                currentNode.AddNode(new SyntaxTree.XMLNode() { name = nodesTypes.token, value = currentToken.lexem });
                if (parseStatementList(currentNode))
                    return true;
                else
                {
                    errors.Add(new Error { message = "**Error** Expected statement", row = currentToken.row });
                }
            }
            positionInLexems--; // no alternative part
            curr.nodes.Remove(currentNode);
            return true;
        }

        private bool parseVarDeclarations()
        {
            SyntaxTree.XMLNode currentNode = program.nodes.Find(x => x.name == nodesTypes.block).AddNode(new SyntaxTree.XMLNode() { name = nodesTypes.var_declar });
            //AddGraphNode(new SyntaxTree.Node(nodesTypes.var_declar));
            //AddLink(new SyntaxTree.Link(nodesTypes.block, nodesTypes.var_declar));

            LexicalAnalizerOutput currentToken = GetNextToken();
            if (currentToken.lexem == "VAR" && keyWords.Find(x => x.id == currentToken.code) != null)
            {
                //AddGraphNode(new SyntaxTree.Node(nodesTypes.token, currentToken.lexem));
                //AddLink(new SyntaxTree.Link(nodesTypes.var_declar, nodesTypes.token));
                currentNode.AddNode(new SyntaxTree.XMLNode() { name = nodesTypes.token, value = currentToken.lexem });

                currentNode.AddNode(new SyntaxTree.XMLNode() { name = nodesTypes.declar_list });

                if (parseDeclarationList())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }   
            return true;    
        }

        private bool parseDeclarationList()
        {
            SyntaxTree.XMLNode currentNode = program.nodes.Find(x => x.name == nodesTypes.block)
                                                 .nodes.Find(x => x.name == nodesTypes.var_declar);

            if (parseDeclaration())
                parseDeclarationList();
            if (currentNode.nodes.Find(x => x.name == nodesTypes.declar_list)
                           .nodes.Count > 0)
                return true;
            else // no declarations found
            {
                currentNode.nodes.Clear();
                return false;
            }
        }

        private bool parseDeclaration()
        {
            LexicalAnalizerOutput currentToken = new LexicalAnalizerOutput();
            SyntaxTree.XMLNode currentNode = ParseVarIdn();
            if (currentNode != null)//identifiers.Find(x => x.type == identifierType.user && x.id == currentToken.code) != null)
            {
                currentToken = GetNextToken();
                if (currentToken.lexem == ":")
                {
                    currentNode.AddNode(new SyntaxTree.XMLNode() { name = nodesTypes.token, value = currentToken.lexem });
                    string expectedDeclarationType = ParseAttribute();
                    if (expectedDeclarationType != "")
                    {
                        currentNode.AddNode(new SyntaxTree.XMLNode() { name = nodesTypes.attribute, value = expectedDeclarationType });
                        if (!identifiersExtended.Exists(x => x.name == currentNode.nodes.Find(y => y.name == nodesTypes.var_idn).value))
                        {
                            identifiersExtended.Add(new IdentifierExt()
                            {
                                name = currentNode.nodes.Find(x => x.name == nodesTypes.var_idn).value,
                                typeAttribute = expectedDeclarationType,
                                type = identifierType.user
                            });
                        }
                        else
                        {
                            errors.Add(new Error { message = "**Error** Variable is already declared ", row = currentToken.row });
                            return false;
                        }
                        currentToken = GetNextToken();
                        if (currentToken.lexem == ";")
                        {
                            currentNode.AddNode(new SyntaxTree.XMLNode() { name = nodesTypes.token, value = currentToken.lexem });
                            return true;
                        }
                        else 
                            errors.Add(new Error { message = "**Error** ';' expected", row = currentToken.row });
                    }
                    else
                        errors.Add(new Error { message = "**Error** Type expected", row = currentToken.row });
                }
                else
                {
                    errors.Add(new Error { message = "**Error** ':' expected", row = currentToken.row });
                }
            }
            return false;
        }

        private string ParseAttribute()
        {
            LexicalAnalizerOutput currentToken = GetNextToken();
            if (identifiers.Find(x => x.type == identifierType.system && x.id == currentToken.code) != null)
                return currentToken.lexem;
            else return "";
        }

        private SyntaxTree.XMLNode ParseVarIdn()
        {
            LexicalAnalizerOutput identifier = ParseIdentifier();
            if (identifier.lexem != "")
            {
                SyntaxTree.XMLNode currentNode = program.nodes.Find(x => x.name == nodesTypes.block)
                                                 .nodes.Find(x => x.name == nodesTypes.var_declar)
                                                 .nodes.Find(x => x.name == nodesTypes.declar_list)
                                                 .AddNode(new SyntaxTree.XMLNode() { name = nodesTypes.declaration });

                currentNode.AddNode(new SyntaxTree.XMLNode() { name = nodesTypes.var_idn, value = identifier.lexem});
                return currentNode;
            }
            else
            {
                return null;
            }
        }

        public void Analize()
        {
            ParseProgram();
            SerializeTables.SeriaizeNode(program);

            SyntaxTree.XMLNodeToDGMLParser parser = new SyntaxTree.XMLNodeToDGMLParser();
            SyntaxTree.Graph graph = parser.GetGraph();
            
            SerializeTables.SeriaizeNodeGraph(graph);
            if (WorkDone != null) WorkDone(errors, identifiersExtended);
        }
    }
}
