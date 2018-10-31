using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 实验1
{
    public partial class findreplace : Form
    {
        public Form1 mainform;
        public string str;
        public int start = 0;
        public int index;
        public findreplace()
        {
            InitializeComponent();
        }
        public findreplace(Form1 form)
        {
            InitializeComponent();
            mainform = form;
        }
        private void Find_Click(object sender, EventArgs e)
        {
            str = textBox1.Text;
            if (textBox1.Text.Length != 0)//如果查找字符串不为空,调用主窗体查找方法
                mainform.FindRichTextBoxString(textBox1.Text);
            else
                MessageBox.Show("Find can not be empty", "Hint", MessageBoxButtons.OK);
        }

        private void Replace_Click(object sender, EventArgs e)
        {
            /*if (textBox2.Text.Length != 0)//如果替换字符串不为空,调用主窗体替换方法
                mainform.ReplaceRichTextBoxString(textBox2.Text);
            else
                MessageBox.Show("Replace can not be empty", "Hint", MessageBoxButtons.OK);*/
        }
    }
}
