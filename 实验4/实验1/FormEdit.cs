using System;
using System.Collections;
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
    public partial class FormEdit : Form
    {
        public FormEdit()
        {
            InitializeComponent();
        }
        
        private void FormEdit_Load(object sender, EventArgs e)
        {

            richTextBox2.ScrollBars = 0;
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void updateLabelRowIndex()
        {
            //we get index of first visible char and number of first visible line
            Point pos = new Point(0, 0);
            int firstIndex = this.richTextBox1.GetCharIndexFromPosition(pos);
            int firstLine = this.richTextBox1.GetLineFromCharIndex(firstIndex);

            //now we get index of last visible char and number of last visible line
            pos.X += this.richTextBox1.ClientRectangle.Width;
            pos.Y += this.richTextBox1.ClientRectangle.Height;
            int lastIndex = this.richTextBox1.GetCharIndexFromPosition(pos);
            int lastLine = this.richTextBox1.GetLineFromCharIndex(lastIndex);

            //this is point position of last visible char, 
            //we'll use its Y value for calculating numberLabel size
            pos = this.richTextBox1.GetPositionFromCharIndex(lastIndex);

            richTextBox2.Text = "";
            for (int i = firstLine; i <= lastLine + 1; i++)
            {
                richTextBox2.Text += i + 1 + "\r\n";
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            updateLabelRowIndex();
        }

        private void richTextBox1_Resize(object sender, EventArgs e)
        {
            richTextBox1_VScroll(null, null);
        }

        private void richTextBox1_FontChanged(object sender, EventArgs e)
        {
            updateLabelRowIndex();
            richTextBox1_VScroll(null, null);
        }

        private void richTextBox1_VScroll(object sender, EventArgs e)
        {
            int p = richTextBox1.GetPositionFromCharIndex(0).Y % (richTextBox1.Font.Height + 1);
            richTextBox2.Location = new Point(0, p);
            updateLabelRowIndex();
        }

    }
}
