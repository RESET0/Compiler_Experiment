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
                                if (c==">"||c=="<")
                                {
                                    char cc=GetNextChar();
                                    if (cc == '=')
                                    {
                                        c += "=";
                                        text = c;save = false;
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
                                /*if (IsAlpha(temp))
                                    take = ERROR;*/
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
                                /*if (IsNumber(temp))
                                    take = ERROR;*/
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
                /*int index = ((FormEdit)this.ActiveMdiChild).dataGridView1.Rows.Add();
                ((FormEdit)this.ActiveMdiChild).dataGridView1.Rows[index].Cells[0].Value = line + 1;
                ((FormEdit)this.ActiveMdiChild).dataGridView1.Rows[index].Cells[1].Value = take;
                ((FormEdit)this.ActiveMdiChild).dataGridView1.Rows[index].Cells[2].Value = text;
                */
                int index = value.Count ;
                value.Add(index, text);
                type.Add(index,take);
                text = "";
                //cnt++;
            }
        }
        /************************************************************/
        /*******************LR分析*****************************/
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
        struct rl {
            public int num;
            public string r;
            public rl(string a,int b)
            {
                this.num = b;
                this.r = a;
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
        Stack <int> State = new Stack<int>() ;
        Stack<string> str = new Stack<string>();
        int sz;
        bool error_flag;
        Stack<int> tempstate = new Stack<int>();
        Stack<string> tempst = new Stack<string>();
        void show_state()
        {
            string cl1 = "";
            string cl2 = "";
            string cl3 = "";
            while (State.Count!=0)
            {
                
                tempstate.Push(State.Peek());
                State.Pop();
            }
            while(tempstate.Count!=0)
            {
                State.Push(tempstate.Peek());
                tempstate.Pop();

                cl1 += State.Peek().ToString();
            }
            while(str.Count!=0)
            {
                tempst.Push(str.Peek());
                str.Pop();
            }
            while (tempst.Count != 0)
            {
                str.Push(tempst.Peek());
                tempst.Pop();
                cl2 += str.Peek();
            }
            for (int i=pos;i<sz;i++)
            {
                cl3 += value[i]+" ";
            }
            int index = ((FormEdit)this.ActiveMdiChild).dataGridView1.Rows.Add();
            ((FormEdit)this.ActiveMdiChild).dataGridView1.Rows[index].Cells[0].Value = cl1;
            ((FormEdit)this.ActiveMdiChild).dataGridView1.Rows[index].Cells[1].Value = cl2;
            ((FormEdit)this.ActiveMdiChild).dataGridView1.Rows[index].Cells[2].Value = cl3;
        }
        void reget()
        {
            pos--;
        }
        void init_lr()
        {
            State.Clear();
            str.Clear();
            State.Push(0);
            str.Push("#");
            value.Add(value.Count, "#");
            type.Add(type.Count, -1);
            pos = 0;
            error_flag = true;
            sz = value.Count;
            show_state();
        }
        KeyValuePair<int,string> get_next()
        {
            KeyValuePair<int, string> temp = new KeyValuePair<int, string>(type[pos], value[pos]);
            pos++;
            return temp;
        }
        void LR_analysis()
        {
            KeyValuePair<int, string> temp = get_next();
            while (State.Count() != 0 )
            {
                int pres = State.Peek();
                string prest;
                if (temp.Key == 2)
                    prest = "id";
                else if (temp.Key == 1)
                    prest = "num";
                else prest = temp.Value;
                string t = "";
                int wtf = act_goto[prest];
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
                    str.Push(R[0].r);
                    break;
                }
                else if (cur < 30 && cur >= 0)
                {
                    State.Push(cur);
                    str.Push(prest);
                    show_state();
                    temp = get_next();
                }
                else if (cur >= 30)
                {
                    if (cur == 34)
                    {
                        t = "stmts";
                        temp = new KeyValuePair<int, string>(-1, t);
                        reget();
                    }
                    else
                    {
                        cur -= 31;
                        for (int i = 0; i < R[cur].num; i++)
                        {
                            State.Pop();
                            str.Pop();
                        }
                        temp = new KeyValuePair<int, string>(-1, R[cur].r);
                        reget();
                    }
                }
            }
            show_state();
            if(!error_flag)
            {
                ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += "Error:语法错误\n";
            }
        }
        /************************************************************/
        /************************************************************/
        /************************************************************/
        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            words_analysis_init();
            init_lr();
            LR_analysis();
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