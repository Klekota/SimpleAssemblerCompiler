using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace lexer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SerializeTables.SerializeAttributes();
            SerializeTables.SeriaizeKeyWords();
            SerializeTables.SeriaizeIdentifiers();

            SerializeTables.DeserializeAttributes();
            SerializeTables.DeserializeKeyWords();
            SerializeTables.DeserializeIdentifiers();

            this.numberedRTBCode.RichTextBox.TextChanged += new System.EventHandler(this.numberedRTBCodeRichTextBoxTextChanged);
            this.openToolStripMenuItem.Click += (s, e) => this.openFileDialog1.ShowDialog();
            this.saveToolStripMenuItem.Click += (s, e) => this.saveFileDialog1.ShowDialog();
            this.openFileDialog1.FileOk += (s, e) =>
            {
                if (openFileDialog1.FileName.Length > 0)
                    numberedRTBCode.RichTextBox.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
            };
            this.saveFileDialog1.FileOk += (s, e) =>
            {
                if (saveFileDialog1.FileName.Length > 0)
                    numberedRTBCode.RichTextBox.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
            };


            lexer = new LexicalAnalizer(numberedRTBCode.RichTextBox.Lines);
        }

        LexicalAnalizer lexer;
        bool programBuilded;
        bool errorsFound;

        private void ClearScreens()
        {
            Invoke((MethodInvoker)(() =>
            {
                richTextBoxOutput.Text = string.Empty;
                richTextBoxErrorList.Text = string.Empty;
            }));
        }


        private void LexerWorkDone(List<LexicalAnalizerOutput> output, List<Error> errors, List<Constant> constants, List<Identifier> identifiers)
        {
            ClearScreens();
             // add space to call TextChanged and highlight syntax
            Invoke((MethodInvoker)delegate { numberedRTBCode.RichTextBox.Text += " "; });
            

            foreach (var item in output)
            {
                string message = String.Format("Lexem: {0}\t\tCode: {1}\n", item.lexem, item.code);
                Debug.Print(message);
                Invoke((MethodInvoker)delegate { richTextBoxOutput.Text += message; });
            }
            Debug.Print("\n");

            if (errors.Count() > 0)
            {
                foreach (var item in errors)
                {
                    string message = String.Format(item.message + " in row {0}, position {1}\n", item.row.ToString(), item.pos.ToString());
                    Debug.Print(message);
                    Invoke((MethodInvoker)delegate { richTextBoxErrorList.Text += message; }); 
                }
            }
            else
            {
                Invoke((MethodInvoker)delegate { richTextBoxErrorList.Text += "Build succeeded"; }); 
            }

            Invoke((MethodInvoker)delegate { richTextBoxOutput.Text += "\n\nConstants:\n"; });
            foreach (var item in constants)
            {
                string message = String.Format("value: {0}\t\tid: {1}\n", item.value, item.id);
                Invoke((MethodInvoker)delegate { richTextBoxOutput.Text += message; });
            }

            Invoke((MethodInvoker)delegate { richTextBoxOutput.Text += "\n\nIdentifiers:\n"; });
            foreach (var item in identifiers)
            {
                string message = String.Format("name: {0}\t\tid: {1}\n", item.name, item.id);
                Invoke((MethodInvoker)delegate { richTextBoxOutput.Text += message; });
            }

            //
            // CALL SYNTAX_ANALIZEER WHEN LEXER FINISES
            //
            SyntaxAnalizer syntaxer = new SyntaxAnalizer(output, constants, identifiers, lexer.keyWords);
            syntaxer.WorkDone += new SyntaxAnalizer.WorkDoneHandler(SyntaxerWorkDone);
            Thread syntaxerThread = new Thread(new ThreadStart(syntaxer.Analize));
            syntaxerThread.Start();
        }

        private void SyntaxerWorkDone(List<Error> errors, List<IdentifierExt> identifiersExt)
        {
            if (identifiersExt.Count() > 0)
            {
                Invoke((MethodInvoker)delegate { this.richTextBoxOutput.Text += "\nIdentifiers extended table:\n"; });
                foreach (var item in identifiersExt)
                {
                    string message = String.Format(String.Format("name: {0}\t\ttype: {1}\n", item.name, item.typeAttribute));
                    Debug.Print(message);
                    Invoke((MethodInvoker)delegate { this.richTextBoxOutput.Text += message; });
                }
            }

            if (errors.Count() > 0)
            {
                Invoke((MethodInvoker)delegate { richTextBoxErrorList.Text += "\n\nSyntax errors:\n"; });
                foreach (var item in errors)
                {
                    string message = String.Format(item.message + " in row {0}\n", item.row.ToString());
                    Debug.Print(message);
                    Invoke((MethodInvoker)delegate { richTextBoxErrorList.Text += message; });
                }
                errorsFound = true;
            }
            programBuilded = true;

            //if (!errorsFound)
            //{
            //    AssemblerCodeGenerator codeGenerator = new AssemblerCodeGenerator();
            //    codeGenerator.WorkDone += codeGeneratorWorkDone;
            //    Thread generatorThread = new Thread(new ThreadStart(codeGenerator.GenerateCode));

            //    generatorThread.Start();
            //}
        }

        private void buildSolutionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string path = System.IO.Directory.GetCurrentDirectory() + @"\test.txt";
            lexer = new LexicalAnalizer(numberedRTBCode.RichTextBox.Lines);
            lexer.WorkDone += LexerWorkDone;
            Thread lexerThread = new Thread(new ThreadStart(lexer.Analize));
            lexerThread.Start();

            //this.backgroundWorker1.DoWork += (s, ee) => 
            //{
            //    lexer = new LexicalAnalizer(numberedRTBCode.RichTextBox.Lines);
            //    lexer.Analize();
            //};
            //this.backgroundWorker1.RunWorkerCompleted += (s, ee) => { };
        }

        private void ColorPattern(string pattern, Color color, RegexOptions options = RegexOptions.IgnoreCase)
        {
            foreach (Match m in Regex.Matches(numberedRTBCode.RichTextBox.Text, pattern, options))
                SetColor(m.Index, m.Length, color);
        }

        private void SetColor(int startIdx, int length, Color color)
        {
            numberedRTBCode.RichTextBox.SelectionStart = startIdx;
            numberedRTBCode.RichTextBox.SelectionLength = length;
            numberedRTBCode.RichTextBox.SelectionColor = color;
        }

        //
        //numberedRTB
        //
        private void numberedRTBCodeRichTextBoxTextChanged(object sender, EventArgs e) // highlight syntax
        {
             // avoid blinking
            labelOutput.Focus();
            
            // saving original caret position + forecolor
            int originalIndex = numberedRTBCode.RichTextBox.SelectionStart; 
            int originalLength = numberedRTBCode.RichTextBox.SelectionLength; 
            Color originalColor = numberedRTBCode.RichTextBox.SelectionColor;

            // removes any previous highlighting (so modified words won't remain highlighted)
            SetColor(0, numberedRTBCode.RichTextBox.Text.Length, Color.Black);
            
            //  Color code
            ColorPattern(string.Join("|", MyTables.Keywords), Color.Blue);
            ColorPattern(string.Join("|", MyTables.Identifiers), Color.CadetBlue);
            ColorPattern(@"(?<!\w+)-?\d+(?:\.\d+)?", Color.MediumVioletRed);
            ColorPattern(@";.*", Color.Green);

            // restoring the original colors, for further writing
            SetColor(originalIndex, originalLength, originalColor);

            // restore focus
            numberedRTBCode.RichTextBox.Focus();
        }
    }
}
