using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace lexer
{
    enum attrType { whitespace = 0, constant, identifier, oneSymbDelimiter, manySymbDelimiter, begCom, invalid };

    class LexicalAnalizer
    {
        public LexicalAnalizer(string[] lines, string filepath = "")
        {
            attributes = SerializeTables.DeserializeAttributes();
            identifiers = SerializeTables.DeserializeIdentifiers();
            keyWords = SerializeTables.DeserializeKeyWords();
            constants = new List<Constant>();
            errors = new List<Error>();
            this.filepath = filepath;
            this.lines = lines;
        }

        private string[] lines;
        private string filepath;
        private List<Attributes> attributes;
        private List<Identifier> identifiers;
        public List<KeyWord> keyWords;
        private List<Constant> constants;

        public static Dictionary<attrType, int> attributesTypes = new Dictionary<attrType, int>
        {
            { attrType.whitespace, 0 },
            { attrType.constant, 1},
            { attrType.identifier, 2},
            { attrType.oneSymbDelimiter, 3},
            { attrType.manySymbDelimiter, 4},
            { attrType.begCom, 5},
            { attrType.invalid, 6}
        };

        private List<Error> errors;
        public delegate void WorkDoneHandler(List<LexicalAnalizerOutput> output, List<Error> errors, List<Constant> constants, List<Identifier> identifiers);
        public event WorkDoneHandler WorkDone;

        public void Analize()
        {
            List<LexicalAnalizerOutput> result = new List<LexicalAnalizerOutput>();
            string[] lines = this.lines;

            if (filepath.Length > 1)
            {
                if (File.Exists(filepath))
                {
                    lines = File.ReadAllLines(filepath);
                }
                else
                {
                    throw new FileNotFoundException();
                }
            }

            int i = 0; // row number
            int j = 0; // symbol number
            bool supressedOutput = false; // true if whitespaced
            string buffer = "";
            int lexCode = 0;
            string currLexem = ""; // used only for logging

            if (attributes.Count != 0)
            {
                for (i = 0; i < lines.Count(); i++) 
                {
                    string currentLine = lines[i];
                    j = 0;

                    while (j < currentLine.Length)
                    {
                        supressedOutput = false;
                        char currentSymbol = currentLine[j];
                        int symbolAttr = GetSymbolAttr(currentSymbol);

                        if (symbolAttr == attributesTypes[attrType.whitespace]) // whitespace
                        {
                            while (++j < currentLine.Length)
                            {
                                currentSymbol = currentLine[j];
                                symbolAttr = GetSymbolAttr(currentSymbol);
                                if (symbolAttr != attributesTypes[attrType.whitespace])
                                    break;
                            }
                            supressedOutput = true;
                        }
                        else if (symbolAttr == attributesTypes[attrType.constant]) // constant
                        {
                            buffer = makeBufferConstant(currentLine, i, ref j);
                            currLexem = buffer;
                            lexCode = CheckConst(buffer);
                        }
                        else if (symbolAttr == attributesTypes[attrType.identifier]) // identifier
                        {
                            buffer = makeBufferIdentifier(currentLine, i , ref j);
                            currLexem = buffer;
                            lexCode = CheckIdentifier(buffer);
                        }
                        else if (symbolAttr == attributesTypes[attrType.oneSymbDelimiter]) // divider
                        {
                            lexCode = (int)currentSymbol;
                            currLexem = currentSymbol.ToString();
                            j++;
                        }
                        else if (symbolAttr == attributesTypes[attrType.begCom]) // Comment
                        {
                            break;                        
                        }
                        else
                        {
                            j++;
                            errors.Add(new Error { message = "**Error** Invalid symbol", row = i, pos = j});
                            supressedOutput = true;
                        }
                        if (!supressedOutput)
                        {
                            result.Add(new LexicalAnalizerOutput { code = lexCode, lexem = currLexem, row = i});
                        }
                    }

                }
            }
            if (WorkDone != null) WorkDone(result, errors, constants, identifiers);
        }


        private string makeBufferConstant(string currentLine, int rowNumb,  ref int j) // makes buffer for constant 
        {
            string buffer = "";
            bool decimalFound = false;
            char currentSymbol = currentLine[j];
            buffer += currentSymbol.ToString();
            while (++j < currentLine.Length)
            {
                currentSymbol = currentLine[j];
                int symbolAttr = GetSymbolAttr(currentSymbol);
                if (symbolAttr == attributesTypes[attrType.constant])
                    buffer += currentSymbol.ToString();
                if (currentSymbol == '.')
                {
                    if (!decimalFound)
                    {
                        buffer += currentSymbol.ToString();
                        decimalFound = true;
                    }
                    else
                    {
                        errors.Add(new Error { message = "**Error** Double decimal dot detected!", row = rowNumb, pos = j });
                    }
                }
                if (symbolAttr != attributesTypes[attrType.constant] && currentSymbol != '.')
                    break;
            }
            return buffer;
        }

        private string makeBufferIdentifier(string currentLine, int rowNumb, ref int j) // makes buffer for identifier 
        {
            string buffer = "";
            char currentSymbol = currentLine[j];
            bool errorTooLargeAdded = false;
            buffer += currentSymbol.ToString();
            while (++j < currentLine.Length)
            {
                currentSymbol = currentLine[j];
                int symbolAttr = GetSymbolAttr(currentSymbol);
                // if type == constant it takes only digits, if identifier it takes letters or digits starting from second symbol
                if (symbolAttr == attributesTypes[attrType.identifier] || symbolAttr == attributesTypes[attrType.constant])
                {
                    buffer += currentSymbol.ToString();
                    if (buffer.Length > 8 && !errorTooLargeAdded)
                    {
                        errors.Add(new Error { message = "**Error** Identifier too large! Розбирайтесь хлопцi", row = rowNumb, pos = j });
                        errorTooLargeAdded = true;
                    }
                }
                else break;
            }
            return buffer;
        }

        private int CheckIdentifier(string buffer) // returns lexCode of identifier in buffer
        {
            int lexCode = 0;

            if (keyWords.Count() != 0)
            {
                if (keyWords.Any(x => x.keyWord == buffer))
                {
                    lexCode = keyWords.First(x => x.keyWord == buffer).id;
                    return lexCode;
                }
            }
            if (identifiers.Count() != 0)
            {
                if (identifiers.Any(x => x.name == buffer))
                {
                    lexCode = identifiers.First(x => x.name == buffer).id;
                    return lexCode;
                }
                else
                {
                    // creates new identifier with id = maxId + 1
                    Identifier identifier = new Identifier(buffer, identifierType.user, identifiers.OrderByDescending(x => x.id).First().id + 1);
                    identifiers.Add(identifier);
                    lexCode = identifier.id;
                    return lexCode;
                }
            }
            else
            {
                Identifier identifier = new Identifier(buffer, identifierType.user);
                identifiers.Add(identifier);
                lexCode = identifier.id;
                return lexCode;
            }
        }

        private int CheckConst(string buffer) // returns lexCode, if not present in constants returns new id
        {
            // Create a NumberFormatInfo object and set some of its properties.
            System.Globalization.NumberFormatInfo provider = new System.Globalization.NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            provider.NumberGroupSeparator = ",";
            provider.NumberGroupSizes = new int[] { 3 };

            int lexCode = 0;
            if (constants.Count() == 0)
            {
                Constant constant = new Constant(Convert.ToDouble(buffer, provider));
                constants.Add(constant);
                lexCode = constant.id;
            }
            else
            {
                if (!constants.Any(x => x.value == Convert.ToDouble(buffer, provider))) // if no consts has the same value
                {
                    // creates new const with id = maxId + 1
                    Constant constant = new Constant(Convert.ToDouble(buffer, provider), constants.OrderByDescending(x => x.id).First().id + 1);
                    constants.Add(constant);
                    lexCode = constant.id;
                }
                else lexCode = constants.First(x => x.value == Convert.ToDouble(buffer, provider)).id; // if exists get id
            }
            return lexCode;
        }

        private int GetSymbolAttr(char symbol)
        {
            if (attributes.Count != 0)
                return attributes.First(x => x.symbol == symbol).type;
            else
                return attributesTypes[attrType.invalid]; 
        }
        public List<Constant> GetConstants()
        {
            return constants;
        }
        public List<Identifier> GetIdentifiers()
        {
            return identifiers;
        }
    } 
}

struct LexicalAnalizerOutput
{
    public int code;
    public string lexem;
    public int row;
}