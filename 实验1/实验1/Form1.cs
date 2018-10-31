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
        private int line=0,pos=0,len=0,linelen=0,state,textlen;
        string text = "",alltext;
        const int START = 1, ERROR = 0,DIGIT = 2, ALPHA = 3, NOTE = 4, FINISH = 5;
        Dictionary<string,int> dic = new Dictionary<string,int> {
            {"NUMBER1",1 },{"ALPHA2",2},
            {"+",5 },{"-",6},{"*",7 },{"/",8},{"=",9},{"<",10},{"{",11},{"}",12},{";",13},
            { "if",3 },{"then",15},{"else",16},{"end",17},{"repeat",18},
            {"until",19},{"read",20},{ "write",21}
        };
        private void words_analysis_init()
        {
            while (((FormEdit)this.ActiveMdiChild).dataGridView1.Rows.Count != 1)
                ((FormEdit)this.ActiveMdiChild).dataGridView1.Rows.RemoveAt(0);
            ((FormEdit)this.ActiveMdiChild).richTextBox3.Text = "";
            line = 0; pos = 0; len = 0; linelen = 0;
        }
        private bool isAlpha(char a)
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
        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            words_analysis_init();
            line = 0;
            alltext = ((FormEdit)this.ActiveMdiChild).richTextBox1.Text;
            textlen = ((FormEdit)this.ActiveMdiChild).richTextBox1.Text.Length;
            GetToken();
        }
        private char GetNextChar()
        {
            if (pos < textlen)
            {
                pos++;
                if (pos < textlen - 1 && alltext[pos] == '\n')
                { line++; pos++; }
                else if (pos >= textlen)
                    return '\0';
                return alltext[pos];
            }
            else return '\0';
        }
        private void ReGetNextChar()
        {
            pos--;
            if (alltext[pos] == '\n')
            {
                line--;
                pos--;
            }
        }
        private void GetToken()
        {
            int take = 0;
            state = START;
            bool flag = true;
            while (flag)
            {
                char temp= GetNextChar(); 
                bool save = true;
                switch (state)
                {
                    case START:
                        {
                            if (isAlpha(temp))
                                state = ALPHA;
                            else if (IsNumber(temp))
                                state = DIGIT;
                            else if (temp == ' ' || temp == '\t' || temp == '\n')
                            {
                                save = false;
                            }
                            else if(temp=='\0')
                            {
                                flag = false;
                            }
                            else if (temp == '{')
                            {
                                save = false;
                                state = NOTE;
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
                        }break;
                    case NOTE:
                        {
                            save = false;
                            if (temp == '}')
                            {
                                state = START;
                                break;
                            }
                            if (pos == textlen - 1)
                            {
                                state = FINISH;
                            }
                        }
                        break;
                    case DIGIT:
                        {
                            if(!IsNumber(temp))
                            {
                                save = false;
                                state = FINISH;
                                take = dic["NUMBER1"];
                                if (isAlpha(temp))
                                    take = ERROR;
                                ReGetNextChar();
                            }
                        }break;
                    case ALPHA:
                        {
                            if(!isAlpha(temp))
                            {
                                save = false;
                                state = FINISH;
                                if (dic.Keys.Contains(text))
                                    take = dic[text];
                                else take = dic["ALPHA2"];
                                if (IsNumber(temp))
                                    take = ERROR;
                                ReGetNextChar();
                            }
                        }break;
                    case FINISH:
                        {
                            save = false;
                            output(take);
                            state = START;
                        }
                        break;
                }
                if (save&&temp!=' '&&temp!='\n'&&temp!='\t'&&temp!='\0')
                    text += temp;
            }
        }
                    
        private void output(int take)
        {
            if (take == ERROR)
            {
                ((FormEdit)this.ActiveMdiChild).richTextBox3.Text += "Error : Line " + (line+1) + ".\n";
                while(pos<textlen)
                {
                    if (alltext[pos] == ' ' || alltext[pos] == '\t' || alltext[pos] == '\n')
                        break;
                    pos++;
                }
                text = "";
                take = -1;
            }
            else
            {
                int index = ((FormEdit)this.ActiveMdiChild).dataGridView1.Rows.Add();
                ((FormEdit)this.ActiveMdiChild).dataGridView1.Rows[index].Cells[0].Value = line+1;
                ((FormEdit)this.ActiveMdiChild).dataGridView1.Rows[index].Cells[1].Value = take;
                ((FormEdit)this.ActiveMdiChild).dataGridView1.Rows[index].Cells[2].Value = text;
                text = "";
            }
            
        }
        /************************************************************/
        /************************************************************/
        /************************************************************/
        
    }
}
/*test
if check == 0
  a = 1 ;
else 
  a = 2 ;

{dfdks}
*/