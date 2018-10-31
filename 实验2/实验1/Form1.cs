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
            ((FormEdit)this.ActiveMdiChild).richTextBox3.Text = "递归下降语法分析:\n";
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
                int index = ((FormEdit)this.ActiveMdiChild).dataGridView1.Rows.Add();
                ((FormEdit)this.ActiveMdiChild).dataGridView1.Rows[index].Cells[0].Value = line + 1;
                ((FormEdit)this.ActiveMdiChild).dataGridView1.Rows[index].Cells[1].Value = take;
                ((FormEdit)this.ActiveMdiChild).dataGridView1.Rows[index].Cells[2].Value = text;
                
                value.Add(index, text);
                type.Add(index,take);
                text = "";
                //cnt++;
            }
        }
        /************************************************************/
        /*******************递归下降分析*****************************/
        /************************************************************/
        bool check = true;
        private void error()
        {
            check = false;
            ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += "出现错误，终止分析！\n";
        }
        private void match(string t,int ty,bool a)
        {
            if (a)
            {
                if (value[pos] != t)
                {
                    check = false;
                    return;
                }
            }
            else
            {
                if(type[pos]!=ty)
                {
                    check = false;
                    return;
                }
            }
            pos++;
        }
        private void program()
        {
            pos = 0;
            check = true;
            ((FormEdit)this.ActiveMdiChild).richTextBox3.Text = "递归下降语法分析:\n";
            ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += "program-->block\n";
            block();
            if (check == false)
            {
                error();
                return;
            }
        }
        private void block()
        {
            if (check == false)
                return;
            ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += "bolck-->{stmts}\n";
            match("{",0,true);
            stmts();
            match("}",0,true);
        }
        private void stmts()
        {
            if (check == false)
                return;
            if(value[pos]=="}")
            {
                ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += "stmts-->null\n";
                return;
            }
            ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += "stmts-->stmt stmts\n";
            stmt();
            stmts();
        }
        private void stmt()
        {
            if (check == false)
                return;
            if (type[pos] == 2)
            {
                ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += "stmt-->id = expr;\n";
                match("", 2, false);
                match("=", 0, true);
                expr();
                match(";", 0, true);
            }
            else if (value[pos] == "if")
            {
                match("if", 0, true);
                match("(", 0, true);
                Bool();
                match(")", 0, true);
                stmt();
                if(value[pos]=="else")
                {
                    ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += "stmt-->if(bool) stmt else stmt\n";
                    match("else", 0, true);
                    stmt();
                }
                else
                {
                    ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += "stmt-->if(bool) stmt\n";
                }
            }
            else if (value[pos] == "while")
            {
                ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += "stmt-->while(bool) stmt\n";
                match("while", 0, true);
                match("(", 0, true);
                Bool();
                match(")", 0, true);
                stmt();
            }
            else if (value[pos] == "do")
            {
                ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += "stmt-->do stmt while(bool)\n";
                match("do", 0, true);
                stmt();
                match("while", 0, true);
                match("(", 0, true);
                Bool();
                match(")", 0, true);
            }
            else if (value[pos] == "break")
            {
                ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += "stmt-->break\n";
                match("break", 0, true);
            }
            else
            {
                ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += "stmt-->block\n";
                block();
            }
        }
        private void Bool()
        {
            if (check == false)
                return;
            expr();
            string temp = value[pos];
            if (temp == "<" || temp == ">")
            {
                ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += "bool-->expr " + temp;
                pos++;
                Bool1();
            }
            else
            {
                ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += "bool-->expr\n";
                expr();
            }
        }
        private void Bool1()
        {
            if (value[pos] == "=")
            {
                ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += "= expr\n";
                pos++;
                expr();
            }
            else
            {
                expr();
                ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += " expr\n";
            }
        }
        private void expr()
        {
            if (check == false)
                return;
            ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += "expr-->term expr1\n";
            term();
            expr1();
        }
        private void expr1()
        {
            if (check == false)
                return;
            if (value[pos] == "+" || value[pos] == "-")
            {
                ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += "expr1-->"+value[pos]+"term1\n";
                pos++;
                term();
                expr1();
            }
            else
                ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += "expr1-->null\n";
        }
        private void term()
        {
            if (check == false)
                return;
            ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += "term-->factor term1\n";
            factor();
            term1();
        }
        private void term1()
        {
            if (check == false)
                return;
            if (value[pos] == "*" || value[pos] == "/")
            {
                ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += "term-->"+value[pos]+"factor term1\n";
                pos++;
                factor();
                term1();
            }
            else ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += "term1-->null\n";
        }
        private void factor()
        {
            if (check == false)
                return;
            if (value[pos] == "(")
            {
                ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += "factor-->(expr)\n";
                match("(", 0, true);
                expr();
                match(")", 0, true);
            }
            else if (type[pos] == 1 || type[pos] == 2)
            {
                if(type[pos]==1)
                {
                    ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += "factor-->num\n";
                    match("", 1, false);
                }
                else
                {
                    ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += "factor-->id\n";
                    match("", 2, false);
                }
            }
            else check = false;
        }
        /************************************************************/
        /************************************************************/
        /************************************************************/
        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            words_analysis_init();
            program();
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