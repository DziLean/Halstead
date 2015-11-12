using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;


namespace LeanenkaHolsted
{
    public partial class Form1 : Form
    {
        #region Function definition
        public static int AnalyzeUniqueOperands(List<MatchCollection> L)
        {
            List<string> ListStringResult = new List<string>();
            int unique = 0;
            foreach (var I in L)
            {
                List<string> ListHelp = ListFromMatch(I);
                for (int i = 0; i < ListHelp.Count; ++i)
                    ListStringResult.Add(ListHelp[i]);
            }
            unique = ListStringResult.Select(w => w).Distinct().ToList().Count;
            return unique;
        }
        public static int AnalyzeTotalOperands(List<MatchCollection> L)
        {
            List<string> ListStringResult = new List<string>();
            int unique = 0;
            foreach (var I in L)
            {
                List<string> ListHelp = ListFromMatch(I);
                for (int i = 0; i < ListHelp.Count; ++i)
                    ListStringResult.Add(ListHelp[i]);
            }
            unique = ListStringResult.Count;
            return unique;
        }
        public static int AnalyzeUniqueOperators(List<MatchCollection> L, List<List<string>> LS)
        {
            int unique = 0;
            foreach ( var I in L)
            {
                if (I.Count!=0)
                    unique += 1;
            }
            for( int i = 0; i < LS.Count ;++i)
                unique += LS[i].Count;
            return unique;
        }
        public static int AnalyzeTotalOperators(List<MatchCollection> L)
        {
            int unique = 0;
            foreach (var I in L)
            {
                 unique += I.Count;
            }           
            return unique;
        }
        public static List<string> ListFromMatch(MatchCollection MatchCollect)
        {
            List<string> ListString = new List<string>();
            for (int i = 0; i < MatchCollect.Count; ++i)
                ListString.Add(MatchCollect[i].Value);
            return ListString;
        }
        #endregion
        #region Catching errors
        public static void CatchFunction(Exception ex)
        {
            StreamWriter Strw;
            FileInfo Errors = new FileInfo(Directory.GetCurrentDirectory() + @"\Errors.txt");
            if (!Errors.Exists)
                Strw = new StreamWriter(Errors.Create(), System.Text.Encoding.UTF8);
            else
                Strw = Errors.AppendText();
            Strw.WriteLine("\r\n*Errors logging at: {0}*", DateTime.Now);
            Strw.WriteLine("Message: " + ex.Message + "; source: " + ex.Source + "; data: " + ex.Data + "; stacktrace: " + ex.StackTrace);
            Strw.Flush();
            MessageBox.Show("An error has occured: " + ex.Message + ". For more information see the Errors.txt logs.");
            Application.Exit();
        }
        #endregion
        #region Events handler
        public Form1()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            openFileDialog1.Filter = " JS files(*.js, *.JS) | *.js; *.JS | Text files(*.txt) | *.txt  | All files(*.*) | *.*";

            this.richTextBox1.Enabled = true;
            //this.richTextBox1.ReadOnly = true;

            this.richTextBox2.Enabled = true;
            this.richTextBox2.ReadOnly = true;

            LinkLabel.Link link = new LinkLabel.Link();
	        link.LinkData = "http://javascript.ru/manual/operator";
	        linkLabel1.Links.Add(link);

            DataGridViewRow row1 = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            row1.Cells[0].Value = "Unique operators";
            row1.Cells[1].Value = "n1";
            dataGridView1.Rows.Add(row1);

            DataGridViewRow row2 = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            row2.Cells[0].Value = "Unique operands";
            row2.Cells[1].Value = "n2";
            dataGridView1.Rows.Add(row2);

            DataGridViewRow row3 = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            row3.Cells[0].Value = "Total quantity of operators";
            row3.Cells[1].Value = "N1";
            dataGridView1.Rows.Add(row3);

            DataGridViewRow row4 = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            row4.Cells[0].Value = "Total quantity of operands";
            row4.Cells[1].Value = "N2";
            dataGridView1.Rows.Add(row4);

            DataGridViewRow row5 = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            row5.Cells[0].Value = "Map of the program";
            row5.Cells[1].Value = "n = n1 + n2";
            dataGridView1.Rows.Add(row5);

            DataGridViewRow row6 = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            row6.Cells[0].Value = "Total program length";
            row6.Cells[1].Value = "N = N1 + N2";
            dataGridView1.Rows.Add(row6);

            DataGridViewRow row7 = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            row7.Cells[0].Value = "Volume of the program";
            row7.Cells[1].Value = "V = N*log2(n)";
            dataGridView1.Rows.Add(row7);

            DataGridViewRow row8 = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            row8.Cells[0].Value = "Potential volume of the program";
            row8.Cells[1].Value = "V* = n*log2(n)";
            dataGridView1.Rows.Add(row8);

            DataGridViewRow row9 = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            row9.Cells[0].Value = "Theoritical program length";
            row9.Cells[1].Value = "N^ = n1*log2(n1) + n2*log2(n2)";
            dataGridView1.Rows.Add(row9);

            DataGridViewRow row10 = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            row10.Cells[0].Value = "Program level";
            row10.Cells[1].Value = "L = V*/V";
            dataGridView1.Rows.Add(row10);

            DataGridViewRow row11 = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            row11.Cells[0].Value = "Program level[2]";
            row11.Cells[1].Value = "L^ = 2 * n2/(n1 * N2)";
            dataGridView1.Rows.Add(row11);

            DataGridViewRow row12 = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            row12.Cells[0].Value = "Intellectual content of the algorithm";
            row12.Cells[1].Value = "I = L^ * V";
            dataGridView1.Rows.Add(row12);

            DataGridViewRow row13 = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            row13.Cells[0].Value = "Quantity of demanded intellectual decisions";
            row13.Cells[1].Value = "E = N^ * log2(n/L)";
            dataGridView1.Rows.Add(row13);

            DataGridViewRow row14 = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            row14.Cells[0].Value = "Real length";
            row14.Cells[1].Value = "E' = V * V / V*";
            dataGridView1.Rows.Add(row14);         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int size = 0;
            string file = string.Empty;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                file = openFileDialog1.FileName;
                try
                {
                    string text = File.ReadAllText(file);
                    size = text.Length;
                    richTextBox1.AppendText(text);
                }
                catch (Exception ex)
                {
                    CatchFunction(ex);
                }
            }
            label1.Text ="Size: "+ size+ " bytes;"; // <-- Shows file size in debugging mode.
            label2.Text = "Path: " + file + " ;"; // <-- For debugging use.
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("Thank you for the program usage");
            Application.Exit();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

         
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            string AnalysedText = richTextBox1.Text;
            Regex RegDelete = new Regex(@"delete", RegexOptions.IgnoreCase); 
            var MatchDelete = RegDelete.Matches(AnalysedText);
            Regex RegFunction = new Regex(@"function", RegexOptions.IgnoreCase);
            var MatchFunction = RegFunction.Matches(AnalysedText);
            Regex RegNew = new Regex(@"new", RegexOptions.IgnoreCase);
            var MatchNew = RegNew.Matches(AnalysedText);
            Regex RegIn = new Regex(@"in", RegexOptions.IgnoreCase);
            var MatchIn = RegIn.Matches(AnalysedText);
            Regex RegInstanceof = new Regex(@"instanceof", RegexOptions.IgnoreCase);
            var MatchInstanceof = RegInstanceof.Matches(AnalysedText);
            Regex RegThis = new Regex(@"this", RegexOptions.IgnoreCase);
            var MatchThis = RegThis.Matches(AnalysedText);
            Regex RegTypeof = new Regex(@"typeof", RegexOptions.IgnoreCase);
            var MatchTypeof = RegTypeof.Matches(AnalysedText);
            Regex RegVoid= new Regex(@"void", RegexOptions.IgnoreCase);
            var MatchVoid = RegVoid.Matches(AnalysedText);
            Regex RegAccessor1 = new Regex(@"\D\.", RegexOptions.IgnoreCase);//. object.property
            var MatchAccessor1 = RegAccessor1.Matches(AnalysedText);
            Regex RegAccessor2 = new Regex(@"\[[\'""].{0,}?['""]\]", RegexOptions.IgnoreCase);//  object["property"]
            var MatchAccessor2 = RegAccessor2.Matches(AnalysedText); 
            Regex RegArith = new Regex(@"\+(?=[^\+=])|-(?=[^-])|\*(?=[^=])|/|%|\+\+|--", RegexOptions.IgnoreCase);// arithmetical operators  +, -, *, /, %, ++, --
            var MatchArith = RegArith.Matches(AnalysedText); //  
            Regex RegComma = new Regex(@",", RegexOptions.IgnoreCase);// ,
            var MatchComma = RegComma.Matches(AnalysedText); // ,
            Regex RegLogic = new Regex(@"&&|\|\||!(?=[^(=)(==)])", RegexOptions.IgnoreCase);//logic operators &&, ||, !
            var MatchLogic = RegLogic.Matches(AnalysedText); // 
            Regex RegInit = new Regex(@"(?<=[^\+\*(==)=/(>>>)(>>)>\^&\|!(!=)<])=(?=[^=])|\+=|-=|\*=|/=|(?<=[^>])>>=|(?<=[^<])<<=|>>>=|&=|\|=|\^=", RegexOptions.IgnoreCase);//initialization =, +=, -=, *=, /=, >>=, <<=, >>>=, &=, |=, ^=
            var MatchInit = RegInit.Matches(AnalysedText); 
            Regex RegCompare = new Regex(@"(?<=[^!=])==(?=[^=])|!=(?=[^=])|===|!==|(?<=[^(>>)>])>(?=[^=>])|(?<=[^(>>)>])>=|(?<=[^(<<)<])<(?=[^=<])|(?<=[^(<<)<])<=", RegexOptions.IgnoreCase);//Compare ==, !=, ===, !==, >, >=, <, <=
            var MatchCompare = RegCompare.Matches(AnalysedText);
            Regex RegBit = new Regex(@"((?<=[^&])&(?!&)|(?<=[^\|])\|(?!\|)|\^(?=[^=])|~|(?<=[^<])<<|>>(?=[^>=])|>>>)", RegexOptions.IgnoreCase);//bit &, |, ^, ~, <<, >>, >>>
            var MatchBit = RegBit.Matches(AnalysedText); 
            Regex RegString = new Regex(@"\+(?=[^\+=])|\+=", RegexOptions.IgnoreCase);//+, +=
            var MatchString = RegString.Matches(AnalysedText); 
            Regex RegCondition = new Regex(@"(\?)[^\r\n]{1,}?(:)", RegexOptions.IgnoreCase);//condition ? ifTrue : ifFalse            
            var Matchcondition= RegCondition.Matches(AnalysedText); 
            Regex RegDotComma = new Regex(@";", RegexOptions.IgnoreCase);//;
            var MatchDotComma = RegDotComma.Matches(AnalysedText);
            Regex RegFunc = new Regex(@"(?<=function\s{1,}?)[\w\d_]{1,}?(?=[\s\r\n(])|[_\w\d]{1,}?(?=\s{0,}?=\s{0,}?function)", RegexOptions.IgnoreCase);//condition ? ifTrue : ifFalse
            var MatchFunc = RegFunc.Matches(AnalysedText);

            //start processin operands

            Regex RegOperandDeleteNew = new Regex(@"(?<=(delete|new)\s{1,}?)[\w\d]{1,}?(?=\s|;|\r|\n|\()", RegexOptions.IgnoreCase);//delete new
            var MatchOperandDeleteNew = RegOperandDeleteNew.Matches(AnalysedText);

            Regex RegOperandLookBehindInInstanceofTypeof = new Regex(@"([_\w\d]{1,}?)(?=\s{1,}?(typeof|instanceof|in(?=\s{1,}?)))", RegexOptions.IgnoreCase);//first argument instanceof typeof in
            var MatchOperandLookBehindInInstanceofTypeof = RegOperandLookBehindInInstanceofTypeof.Matches(AnalysedText);

            Regex RegOperandLookAfterInInstanceofTypeof = new Regex(@"(?<=(typeof|instanceof|in(?=\s{1,}?))\s{1,}?)[_\w\d]{1,}?(?=[;\r\n\s\)])", RegexOptions.IgnoreCase);//second argument instanceof typeof in
            var MatchOperandLookAfterInInstanceofTypeof = RegOperandLookAfterInInstanceofTypeof.Matches(AnalysedText);

            Regex RegOperandAccessor1FirstOperand = new Regex(@"[_\w\d]{1,}?(?=\.)", RegexOptions.IgnoreCase);//object.property first operand
            var MatchOperandAccessor1FirstOperand = RegOperandAccessor1FirstOperand.Matches(AnalysedText);

            Regex RegOperandAccessor1SecondOperand = new Regex(@"(?<=\.)[_\w\d]{1,}?(?=[\(;\s,])", RegexOptions.IgnoreCase);//object.property second operand
            var MatchOperandAccessor1SecondOperand = RegOperandAccessor1SecondOperand.Matches(AnalysedText);

            Regex RegOperandArithFirstOperand = new Regex(@"[_\w\d]{1,}?(?=\s{0,}?(\+(?=[^\+=])|-(?=[^-])|\*(?=[^=])|/|%|\+\+|--))", RegexOptions.IgnoreCase);// arithmetical operators  +, -, *, /, %, ++, --
            var MatchOperandArithFirstOperand = RegOperandArithFirstOperand.Matches(AnalysedText);

            Regex RegOperandArithSecondOperand = new Regex(@"(?<=(\+(?=[^\+=])|-(?=[^-])|\*(?=[^=])|/|%|\+\+|--)\s{0,}?)[_\w\d]{1,}", RegexOptions.IgnoreCase);// arithmetical operators  +, -, *, /, %, ++, --
            var MatchOperandArithSecondOperand = RegOperandArithSecondOperand.Matches(AnalysedText);
            
            Regex RegOperandLogicFirstOperand = new Regex(@"[_\w\d]{1,}?(?=\s{0,}?(&&|\|\||!(?=[^(=)(==)])))", RegexOptions.IgnoreCase);// arithmetical operators  +, -, *, /, %, ++, --
            var MatchOperandLogicFirstOperand = RegOperandLogicFirstOperand.Matches(AnalysedText);

            Regex RegOperandLogicSecondOperand = new Regex(@"(?<=(&&|\|\||!(?=[^(=)(==)]))\s{0,}?)[_\w\d]{1,}", RegexOptions.IgnoreCase);// arithmetical operators  +, -, *, /, %, ++, --
            var MatchOperandLogicSecondOperand = RegOperandLogicSecondOperand.Matches(AnalysedText);

            Regex RegOperandInitializeFirstOperand = new Regex(@"[_\w\d]{1,}?(?=\s{0,}?((?<=[^\+\*(==)=/(>>>)(>>)>\^&\|!(!=)<])=(?=[^=])|\+=|-=|\*=|/=|(?<=[^>])>>=|(?<=[^<])<<=|>>>=|&=|\|=|\^=))", RegexOptions.IgnoreCase);//initialization =, +=, -=, *=, /=, >>=, <<=, >>>=, &=, |=, ^=
            var MatchOperandInitializeFirstOperand = RegOperandInitializeFirstOperand.Matches(AnalysedText);

            Regex RegOperandInitializeSecondOperand = new Regex(@"(?<=((?<=[^\+\*(==)=/(>>>)(>>)>\^&\|!(!=)<])=(?=[^=])|\+=|-=|\*=|/=|(?<=[^>])>>=|(?<=[^<])<<=|>>>=|&=|\|=|\^=)\s{0,}?)[_\w\d]{1,}", RegexOptions.IgnoreCase);//initialization =, +=, -=, *=, /=, >>=, <<=, >>>=, &=, |=, ^=
            var MatchOperandInitializeSecondOperand = RegOperandInitializeSecondOperand.Matches(AnalysedText);

            Regex RegOperandCompareFirstOperand = new Regex(@"[_\w\d]{1,}?(?=\s{0,}?((?<=[^!=])==(?=[^=])|!=(?=[^=])|===|!==|(?<=[^(>>)>])>(?=[^=>])|(?<=[^(>>)>])>=|(?<=[^(<<)<])<(?=[^=<])|(?<=[^(<<)<])<=))", RegexOptions.IgnoreCase);//Compare ==, !=, ===, !==, >, >=, <, <=
            var MatchOperandICompareFirstOperand = RegOperandCompareFirstOperand.Matches(AnalysedText);

            Regex RegOperandCompareSecondOperand = new Regex(@"(?<=((?<=[^!=])==(?=[^=])|!=(?=[^=])|===|!==|(?<=[^(>>)>])>(?=[^=>])|(?<=[^(>>)>])>=|(?<=[^(<<)<])<(?=[^=<])|(?<=[^(<<)<])<=)\s{0,}?)[_\w\d]{1,}", RegexOptions.IgnoreCase);//Compare ==, !=, ===, !==, >, >=, <, <=
            var MatchOperandCompareSecondOperand = RegOperandCompareSecondOperand.Matches(AnalysedText);

            Regex RegOperandBitFirstOperand = new Regex(@"[_\w\d]{1,}?(?=\s{0,}?((?<=[^&])&(?!&)|(?<=[^\|])\|(?!\|)|\^(?=[^=])|~|(?<=[^<])<<|>>(?=[^>=])|>>>))", RegexOptions.IgnoreCase);//bit &, |, ^, ~, <<, >>, >>>
            var MatchOperandIBitFirstOperand = RegOperandBitFirstOperand.Matches(AnalysedText);

            Regex RegOperandBitSecondOperand = new Regex(@"(?<=((?<=[^&])&(?!&)|(?<=[^\|])\|(?!\|)|\^(?=[^=])|~|(?<=[^<])<<|>>(?=[^>=])|>>>)\s{0,}?)[_\w\d]{1,}", RegexOptions.IgnoreCase);//bit &, |, ^, ~, <<, >>, >>>
            var MatchOperandBitSecondOperand = RegOperandBitSecondOperand.Matches(AnalysedText);    

            //unique lists of operators
            var Arithmetic = ListFromMatch(MatchArith).Select(w => w).Distinct().ToList();
            var Logic = ListFromMatch(MatchLogic).Select(w => w).Distinct().ToList();
            var Initialization = ListFromMatch(MatchInit).Select(w => w).Distinct().ToList();
            var Compare = ListFromMatch(MatchCompare).Select(w => w).Distinct().ToList();
            var Bit = ListFromMatch(MatchBit).Select(w => w).Distinct().ToList();
            var String = ListFromMatch(MatchString).Select(w => w).Distinct().ToList();            
            var FunctionDeclarationOrExpression = ListFromMatch(MatchFunc).Select(w => w).Distinct().ToList() ;

            List<List<string>> ListOfUniqueOperators = new List<List<string>>();
            ListOfUniqueOperators.Add(Arithmetic); ListOfUniqueOperators.Add(Logic); ListOfUniqueOperators.Add(Initialization); ListOfUniqueOperators.Add(Compare);
            ListOfUniqueOperators.Add(Bit); ListOfUniqueOperators.Add(String); ListOfUniqueOperators.Add(FunctionDeclarationOrExpression);

            //List of Matches
            List<MatchCollection> MatchCollect = new List<MatchCollection>();
            //first the addition of unique operators
            MatchCollect.Add(MatchDelete); MatchCollect.Add(MatchFunction); MatchCollect.Add(MatchNew); MatchCollect.Add(MatchIn);
            MatchCollect.Add(MatchInstanceof); MatchCollect.Add(MatchThis); MatchCollect.Add(MatchTypeof); MatchCollect.Add(MatchVoid);
            MatchCollect.Add(MatchAccessor1); MatchCollect.Add(MatchAccessor2); MatchCollect.Add(MatchComma);
            MatchCollect.Add(Matchcondition); MatchCollect.Add(MatchDotComma);

            int n1 = AnalyzeUniqueOperators(MatchCollect,ListOfUniqueOperators);
            //non-unique repetition of the operators mentioned
            MatchCollect.Add(MatchArith);MatchCollect.Add(MatchLogic); MatchCollect.Add(MatchInit);
            MatchCollect.Add(MatchCompare);MatchCollect.Add(MatchBit);MatchCollect.Add(MatchString);
            MatchCollect.Add(MatchFunc);

            int N1 = AnalyzeTotalOperators(MatchCollect);
            //operators computed, compute the operand part
            dataGridView1.Rows[0].Cells[2].Value = n1;
            dataGridView1.Rows[2].Cells[2].Value = N1;

            //operands processing
            List<MatchCollection> MatchCollectionOfOperands = new List<MatchCollection>();
            MatchCollectionOfOperands.Add(MatchOperandDeleteNew); MatchCollectionOfOperands.Add(MatchOperandLookBehindInInstanceofTypeof); MatchCollectionOfOperands.Add(MatchOperandLookAfterInInstanceofTypeof);
            MatchCollectionOfOperands.Add(MatchOperandAccessor1FirstOperand); MatchCollectionOfOperands.Add(MatchOperandAccessor1SecondOperand); MatchCollectionOfOperands.Add(MatchOperandArithFirstOperand);
            MatchCollectionOfOperands.Add(MatchOperandArithSecondOperand); MatchCollectionOfOperands.Add(MatchOperandLogicFirstOperand);
            MatchCollectionOfOperands.Add(MatchOperandLogicSecondOperand); MatchCollectionOfOperands.Add(MatchOperandInitializeFirstOperand); MatchCollectionOfOperands.Add(MatchOperandInitializeSecondOperand);
            MatchCollectionOfOperands.Add(MatchOperandICompareFirstOperand); MatchCollectionOfOperands.Add(MatchOperandCompareSecondOperand); MatchCollectionOfOperands.Add(MatchOperandIBitFirstOperand);
            MatchCollectionOfOperands.Add(MatchOperandBitSecondOperand);

            int n2 = AnalyzeUniqueOperands(MatchCollectionOfOperands);
            int N2 = AnalyzeTotalOperands(MatchCollectionOfOperands);

            dataGridView1.Rows[1].Cells[2].Value = n2;
            dataGridView1.Rows[3].Cells[2].Value = N2;

            if (n1 == 0 || n2 == 0 || N1 == 0 || N2 == 0)
            {
                int n = 0; dataGridView1.Rows[4].Cells[2].Value = n;
                int N = 0; dataGridView1.Rows[5].Cells[2].Value = N;
                double V = 0; dataGridView1.Rows[6].Cells[2].Value = (int)V;
                double VPotential = 0; dataGridView1.Rows[7].Cells[2].Value = (int)VPotential;
                double NTheoretical = 0; dataGridView1.Rows[8].Cells[2].Value = (int)NTheoretical;
                double L = 0; dataGridView1.Rows[9].Cells[2].Value = L;
                double L2 = 0;  dataGridView1.Rows[10].Cells[2].Value = L2;
                double I = 0; dataGridView1.Rows[11].Cells[2].Value = I;
                double E = 0; dataGridView1.Rows[12].Cells[2].Value = (int)E;
                double EReal = 0; dataGridView1.Rows[13].Cells[2].Value = (int)EReal;
            }
            else
            {
                int n = n1 + n2; dataGridView1.Rows[4].Cells[2].Value = n;
                int N = N1 + N2; dataGridView1.Rows[5].Cells[2].Value = N;
                double V = N * Math.Log(n) / Math.Log(2); dataGridView1.Rows[6].Cells[2].Value = (int)V;
                double VPotential = n * Math.Log(n) / Math.Log(2); dataGridView1.Rows[7].Cells[2].Value = (int)VPotential;
                double NTheoretical = n1 * Math.Log(n1) / Math.Log(2) + n2 * Math.Log(n2) / Math.Log(2); dataGridView1.Rows[8].Cells[2].Value = (int)NTheoretical;
                double L = VPotential / V; dataGridView1.Rows[9].Cells[2].Value = L;
                double L2 = (double)(2 * n2) / (double)(n1 * N2); dataGridView1.Rows[10].Cells[2].Value = L2;
                double I = L2 * V; dataGridView1.Rows[11].Cells[2].Value = I;
                double E = NTheoretical * Math.Log(n / L) / Math.Log(2); dataGridView1.Rows[12].Cells[2].Value = (int)E;
                double EReal = V * V / VPotential; dataGridView1.Rows[13].Cells[2].Value = (int)EReal;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }
        #endregion
    }
}
