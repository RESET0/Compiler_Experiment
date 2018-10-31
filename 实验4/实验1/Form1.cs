using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Web;
using System.Runtime.InteropServices;

namespace 实验1
{
    public partial class Form1 : Form
    {
        private int num = 1;
        private int findpos;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InstalledFontCollection ifc = new InstalledFontCollection();
            LayoutMdi(MdiLayout.Cascade);
            Text = "Text Edit";
            NewEdit();
        }
        private void NewEdit()
        {
            FormEdit fd = new FormEdit();
            fd.MdiParent = this;
            fd.Text = "untitled" + num;
            fd.WindowState = FormWindowState.Maximized;
            fd.Show();
            fd.Activate();
            num++;
        }
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((FormEdit)this.ActiveMdiChild).richTextBox1.Redo();
        }

        private void NewFile_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewEdit();
        }

        private void OpenFile_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "C++ File（*.cpp）|*.cpp|C File（*.c）|*.c|All（*.*）|*.*";
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    NewEdit();
                    num--;

                    if (openFileDialog1.FilterIndex == 1)
                        ((FormEdit)this.ActiveMdiChild).richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.RichText);
                    else
                        ((FormEdit)this.ActiveMdiChild).richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                    ((FormEdit)this.ActiveMdiChild).Text = openFileDialog1.FileName;
                }
                catch
                {
                    MessageBox.Show("Open File Error！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            openFileDialog1.Dispose();
        }
        private void Save_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Count() > 0)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "C++ File（*.cpp）|*.cpp|C File（*.c）|*.c|All（*.*）|*.*";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (saveFileDialog1.FilterIndex == 1)
                            ((FormEdit)this.ActiveMdiChild).richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
                        else
                            ((FormEdit)this.ActiveMdiChild).richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                        MessageBox.Show("Successfully Saved", "", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    catch
                    {
                        MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                saveFileDialog1.Dispose();
            }
        }
        private void CloseFile_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Count() > 0)
            {
                if (MessageBox.Show("Close current edit？", "Hint", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    ((FormEdit)this.ActiveMdiChild).Close();
            }
        }
        private void Exit_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit？", "Hint", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                foreach (FormEdit fd in this.MdiChildren)
                    fd.Close();
                Application.Exit();
            }
        }
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((FormEdit)this.ActiveMdiChild).richTextBox1.Undo();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((FormEdit)this.ActiveMdiChild).richTextBox1.Copy();
        }

        private void Cut_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((FormEdit)this.ActiveMdiChild).richTextBox1.Cut();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((FormEdit)this.ActiveMdiChild).richTextBox1.Paste();
        }
        public void FindRichTextBoxString(string FindString)
        {
            if (findpos >= ((FormEdit)this.ActiveMdiChild).richTextBox1.Text.Length)
            {
                MessageBox.Show("All the text has been find", "提示", MessageBoxButtons.OK);
                findpos = 0;
                return;
            }
            findpos = ((FormEdit)this.ActiveMdiChild).richTextBox1.Find(FindString, findpos, RichTextBoxFinds.MatchCase);//查找string
            if (findpos == -1)
            {
                MessageBox.Show("All the text has been find", "提示", MessageBoxButtons.OK);
                findpos = 0;
            }
            else
            {
                ((FormEdit)this.ActiveMdiChild).richTextBox1.Focus();
                findpos += FindString.Length;
            }
        }
        private void FindAndReplace_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            findpos = 0;
            findreplace f = new findreplace(this);
            f.Show();
        }
        /************************************************************/
        /*****************词法分析***********************************/
        /************************************************************/
        private int line = 0, pos = -1, len = 0, state, textlen,cnt;
        string text = "", alltext;
        const int START = 1, ERROR = 0, DIGIT = 2, ALPHA = 3, NOTE = 4, FINISH = 5;
        List<string> idtable = new List<string>();
        List<string> digitable = new List<string>();
        Dictionary<int, string> value = new Dictionary<int, string>();
        Dictionary<int, int> type = new Dictionary<int, int>();
        Dictionary<string,int> dic = new Dictionary<string,int> {
            {"NUMBER1",1 },{"ALPHA2",2},{"(",3},{")",4},{">",22},{">=",23},{"<=",24},
            {"+",5 },{"-",6},{"*",7 },{"/",8},{"=",9},{"<",10},{"{",11},{"}",12},{";",13},
            { "if",14 },{"then",15},{"else",16},{"end",17},{"repeat",18},
            {"until",19},{"read",20},{ "write",21},{"while",25},{"do",26}
        };
        private void words_analysis_init()
        {
            ((FormEdit)this.ActiveMdiChild).dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            while (((FormEdit)this.ActiveMdiChild).dataGridView1.Rows.Count != 0)
                ((FormEdit)this.ActiveMdiChild).dataGridView1.Rows.RemoveAt(0);
            ((FormEdit)this.ActiveMdiChild).richTextBox3.Text = "";
            line = 0; pos = -1; len = 0; cnt = 0;
            value.Clear();
            type.Clear();
            alltext = ((FormEdit)this.ActiveMdiChild).richTextBox1.Text+'\0';
            textlen = ((FormEdit)this.ActiveMdiChild).richTextBox1.Text.Length;
            GetToken();
        }
        private bool IsAlpha(char a)
        {
            if ((a >= 'a' && a <= 'z') || (a >= 'A' && a <= 'Z'))
                return true;
            else return false;
        }
        private bool IsNumber(char a)
        {
            if (a >= '0' && a <= '9')
                return true;
            else return false;
        }
        private char GetNextChar()
        {
            if (pos == -1||(pos < textlen - 1 && alltext[pos] != '\n'))
            {
                pos++;
                return alltext[pos];
            }
            else 
            {
                line++;
                pos++;
                return alltext[pos];
            }
        }
        private void ReGetNextChar()
        {
            pos--;
            if(alltext[pos] == '\n')
            {
                line--;
            }
        }
        private void GetToken()
        {
            int take = 0;
            state = START;
            while (true)
            {
                char temp=' ';
                if(state!=FINISH)
                temp = GetNextChar();
                bool save = true;
                switch (state)
                {
                    case START:
                        {
                            if (IsAlpha(temp))
                                state = ALPHA;
                            else if (IsNumber(temp))
                                state = DIGIT;
                            else if (temp == ' ' || temp == '\t' || temp == '\n')
                            {
                                save = false;
                            }
                            else if (temp == '\0')
                            {
                                return;
                            }
                            else
                            {
                                string c = "";
                                c += temp;
                                if (c == ">" || c == "<")
                                {
                                    char cc = GetNextChar();
                                    if (cc == '=')
                                    {
                                        c += "=";
                                        text = c; save = false;
                                    }
                                    else
                                        ReGetNextChar();
                                }

                                bool test = dic.Keys.Contains(c);
                                switch (test)
                                {
                                    case true: take = dic[c]; break;
                                    case false: take = ERROR; break;
                                }
                                state = FINISH;
                            }
                        }
                        break;

                    case DIGIT:
                        {
                            if (!IsNumber(temp))
                            {
                                save = false;
                                state = FINISH;
                                take = dic["NUMBER1"];
                                ReGetNextChar();
                            }
                        }
                        break;
                    case ALPHA:
                        {
                            if (!IsAlpha(temp))
                            {
                                save = false;
                                state = FINISH;
                                if (dic.Keys.Contains(text))
                                    take = dic[text];
                                else take = dic["ALPHA2"];
                                ReGetNextChar();
                            }
                        }
                        break;
                    case FINISH:
                        {
                            save = false;
                            output(take);
                            state = START;
                        }
                        break;
                }
                if (save && temp != ' ' && temp != '\n' && temp != '\t')
                    text += temp;
            }
        }

        private void output(int take)
        {
            if (take == ERROR)
            {
                ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += "Error : Line " + (line + 1) + ".\n";
                while (pos < textlen)
                {
                    if (alltext[pos] == ' ' || alltext[pos] == '\t' || alltext[pos] == '\n')
                        break;
                    pos++;
                }
                text = "";
            }
            else
            {
                int index = ((FormEdit)this.ActiveMdiChild).dataGridView1.Rows.Add();
                ((FormEdit)this.ActiveMdiChild).dataGridView1.Rows[index].Cells[0].Value = line + 1;
                ((FormEdit)this.ActiveMdiChild).dataGridView1.Rows[index].Cells[1].Value = take;
                ((FormEdit)this.ActiveMdiChild).dataGridView1.Rows[index].Cells[2].Value = text;
                if (take == 1) digitable.Add(text);
                else if (take == 2) idtable.Add(text);
                value.Add(index, text);
                type.Add(index,take);
                text = "";
                //cnt++;
            }
        }
        /************************************************************/
        /*******************中间代码生成***********************************/
        /************************************************************/
        int[,] table ={{2,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,1,-1,-1,-1,-1,-1},
                       {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,100,-1,-1,-1,-1,-1,-1},
                       {2,34,-1,-1,-1,-1,-1,-1,-1,8,-1,12,-1,7,3,5,-1,-1,-1},
                       {-1,4,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
                       {32,32,-1,-1,-1,-1,-1,-1,-1,32,-1,32,32,-1,-1,-1,-1,-1,-1},
                       {2,34,-1,-1,-1,-1,-1,-1,-1,8,-1,12,-1,7,6,5,-1,-1,-1 },
                       {-1,33,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
                       {37,37,-1,-1,-1,-1,-1,-1,-1,37,-1,37,-1,-1,-1,-1,-1,-1,-1},
                       {-1,-1,-1,-1,-1,9,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
                       {-1,-1,-1,-1,-1,-1,-1,-1,-1,18,19,-1,-1,-1,-1,-1,-1,10,20},
                       {-1,-1,-1,-1,15,-1,-1,-1,11,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
                       {35,35,-1,-1,-1,-1,-1,-1,-1,35,-1,35,-1,-1,-1,-1,-1,-1,-1},
                       {-1,-1,13,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
                       {-1,-1,-1,-1,-1,-1,-1,-1,-1,18,19,-1,-1,-1,-1,-1,17,14,20},
                       {-1,-1,-1,-1,15,-1,23,22,11,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
                       {-1,-1,-1,-1,-1,-1,-1,-1,-1,18,19,-1,-1,-1,-1,-1,-1,-1,16},
                       {-1,-1,-1,40,40,-1,40,40,40,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
                       {-1,-1,-1,21,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
                       {-1,-1,-1,42,42,-1,42,42,42,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
                       {-1,-1,-1,43,43,-1,43,43,43,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
                       {-1,-1,-1,41,41,-1,41,41,41,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
                       {2,-1,-1,-1,-1,-1,-1,-1,-1,8,-1,12,-1,7,-1,24,-1,-1,-1 },
                       {-1,-1,-1,-1,-1,-1,-1,-1,-1,18,19,-1,-1,-1,-1,-1,-1,25,20},
                       {-1,-1,-1,-1,-1,-1,-1,-1,-1,18,19,-1,-1,-1,-1,-1,-1,26,20},
                       {36,36,-1,-1,-1,-1,-1,-1,-1,36,-1,36,-1,-1,-1,-1,-1,-1,-1},
                       {-1,-1,-1,39,15,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
                       {-1,-1,-1,38,15,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1}
            };
        struct rl
        {
            public int num;
            public string r;
            public rl(string a, int b)
            {
                this.num = b;
                this.r = a;
            }
        };
        struct feature
        {
            public string place;
            public string value;
            public string num;
            public feature(string pl, string val,string nu="")
            {
                this.place = pl;
                this.value = val;
                this.num = nu;
            }
        };
        rl[] R = {new rl( "program",1 ),new rl("block",3), new rl("stmts",2), new rl("stmts",0), new rl("stmt",4), new rl("stmt",5), new rl("stmt",1),
           new rl("bool",3),new rl("bool",3 ),new rl("E",3),new rl("E",1),new rl("T",1),new rl("T",1)
        };
        string[] r = { "program⟶block", "block⟶{ stmts }", "stmts⟶stmt stmts", "stmts⟶eps", "stmt⟶id = E ;",
                    "stmt⟶while ( bool )  stmt","stmt⟶block","bool⟶E <= E","bool⟶E >= E",
                    "E⟶E + T","E⟶T","T⟶id","T⟶num"};
        Dictionary<string, int> act_goto = new Dictionary<string, int>
        {{ "{",0 },{"}",1 },{"(",2 },{")",3 },{"+",4 },{ "=",5},{ "<=",6},
         { ">=",7 },{";",8 },{"id",9 },{"num",10 },{"while",11 },{ "#",12},
         { "block",13},{"stmts",14},{"stmt",15},{"bool",16},{"E",17},{"T",18 } };
        Stack<int> State = new Stack<int>();
        Stack<feature> str = new Stack<feature>();
        Stack<int> tempstate = new Stack<int>();
        Stack<feature> tempst = new Stack<feature>();
        Stack<int> Address = new Stack<int>();
        int sz,cntline=100,idcnt=1;
        bool error_flag;
        int getype;
        List<string> coding = new List<string>();
        Stack<int> start = new Stack<int>();
        void reget()
        {
            pos--;
        }
        feature emit(int i)
        {
            if(i==0)
            {
                str.Pop();
                State.Pop();
                feature tempft = new feature("", "program", "program");
                return tempft;
            }
            else if(i==1)
            {
                for(int j=0;j<3;j++)
                {
                    str.Pop();
                    State.Pop();
                }
                feature tempft = new feature("", "block", "block");
                return tempft;
            }
            else if(i==2)
            {
                for (int j = 0; j < 2; j++)
                {
                    str.Pop();
                    State.Pop();
                }
                feature tempft = new feature("", "stmts", "stmts");
                return tempft;
            }
            else if (i == 3)
            {
                getype = -1;
                feature tempft = new feature("", "stmts", "stmts");
                return tempft;
            }
            if (i==4)
            {
                str.Pop();
                State.Pop();
                string E = str.Peek().place;
                str.Pop();str.Pop();
                State.Pop();State.Pop();
                string id = str.Peek().value;
                str.Pop();State.Pop();
                coding.Add(id + " = " + E + "\n");
                feature tempft = new feature("", "stmt", "stmt");
                return tempft;
            }
            else if(i==5)  /*while*/
            {
                for (int j = 0; j < 5; j++)
                {
                    str.Pop();
                    State.Pop();
                }
                coding.Add("goto " + start.Peek().ToString()+"\n");
                start.Pop();
                coding[Address.Peek()] += (100+coding.Count).ToString() + "\n";
                Address.Pop();
                feature tempft = new feature("", "stmt", "stmt");
                return tempft;
            }
            else if(i==6)
            {
                str.Pop();
                State.Pop();
                feature tempft = new feature("", "stmt", "stmt");
                return tempft;
            }
            else if(i==8||i==7)
            {
                string val2 = str.Peek().place;
                str.Pop(); State.Pop();
                string op = str.Peek().value;
                State.Pop(); str.Pop();
                string val1 = str.Peek().place;
                str.Pop(); State.Pop();
                string newtemp = "T" + idcnt.ToString();
                idcnt++;
                coding.Add("if " + val1 + " " + op + " " + val2 + " goto "+(coding.Count+102).ToString()+"\n");
                coding.Add("goto ");
                Address.Push(coding.Count - 1);
                feature tempft = new feature("", "bool", "bool");
                return tempft;
            }
            else if(i==9)
            {
                string E1 = str.Peek().place;
                str.Pop(); str.Pop();
                State.Pop(); State.Pop();
                string T = str.Peek().place;
                str.Pop(); State.Pop();
                string newtemp = "T" + idcnt.ToString();
                coding.Add(newtemp + " = " + E1 + " + " + T + "\n");
                idcnt++;
                feature tempft = new feature(newtemp, "E", "E");
                return tempft;
            }
            else if(i==10)
            {
                string idname = str.Peek().place;
                str.Pop();
                State.Pop();
                feature tempft = new feature(idname, "E", "E");
                return tempft;
            }
            else 
            {
                string idname = str.Peek().value;
                str.Pop();
                State.Pop();
                feature tempft = new feature(idname, "T", "T");
                return tempft;
            }
        }
        void init_lr()
        {
            State.Clear();
            str.Clear();
            State.Push(0);
            str.Push(new feature("","#"));
            value.Add(value.Count, "#");
            type.Add(type.Count, -1);
            coding.Clear();
            idcnt = 1;pos = 0;
            error_flag = true;
            sz = value.Count;
        }
        KeyValuePair<int, string> get_next()
        {
            KeyValuePair<int, string> temp = new KeyValuePair<int, string>(type[pos], value[pos]);
            pos++;
            return temp;
        }
        bool prer = false;
        void LR_analysis()
        {
            init_lr();
            KeyValuePair<int, string> temp = get_next();
            int getype = temp.Key;
            feature pl=new feature("",temp.Value,"");
            while (State.Count() != 0)
            {
                int pres = State.Peek();
                string prest;
                if (getype == 2)
                    prest = "id";
                else if (getype == 1)
                    prest = "num";
                else prest = pl.value;
                if (prest == "while")
                    start.Push(100+coding.Count);
                int cur = table[pres, act_goto[prest]];
                if (cur == -1)
                {
                    error_flag = false;
                    break;
                }
                else if (cur == 100)
                {
                    for (int i = 0; i < R[0].num; i++)
                    {
                        State.Pop();
                        str.Pop();
                    }
                    str.Push(new feature("",R[0].r));
                    break;
                }
                else if (cur < 30 && cur >= 0)
                {
                    State.Push(cur);
                    str.Push(pl);
                    temp = get_next();
                    getype = temp.Key;
                    pl = new feature("", temp.Value, "");
                }
                else if (cur >= 30)
                {
                    cur -= 31;
                    pl = emit(cur);
                    getype = -1;
                    reget();
                }
            }
            if (!error_flag)
            {
                ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += "Error:语法错误\n";
            }
        }
        void output()
        {

            ((FormEdit)this.ActiveMdiChild).richTextBox3.Text = "中间代码:\n";
            for (int i = 0; i < coding.Count; i++)
            {
                ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += (100 + i).ToString() + ": " + coding[i];
            }
            ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += (100 + coding.Count).ToString() + ": ";
        }
        /************************************************************/
        /************************************************************/
        /************************************************************/
        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            words_analysis_init();
            LR_analysis();
            output();
        } 
    }
}
/********test*******//*
{
    i = 2;
    while(i <= 100)
    {
        sum = sum + i;
        i = i + 2;
    }
}
*/