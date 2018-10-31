namespace 实验1
{
    partial class FormEdit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.State_Stack = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.richTextBox2);
            this.panel2.Location = new System.Drawing.Point(4, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(26, 325);
            this.panel2.TabIndex = 1;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox2.Enabled = false;
            this.richTextBox2.Location = new System.Drawing.Point(0, 0);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(26, 325);
            this.richTextBox2.TabIndex = 6;
            this.richTextBox2.Text = "";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dataGridView1);
            this.panel4.Location = new System.Drawing.Point(287, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(515, 337);
            this.panel4.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Transparent;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.State_Stack,
            this.type,
            this.value});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(515, 337);
            this.dataGridView1.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.richTextBox1);
            this.panel5.Location = new System.Drawing.Point(36, 12);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(245, 325);
            this.panel5.TabIndex = 6;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(245, 325);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "";
            this.richTextBox1.VScroll += new System.EventHandler(this.richTextBox1_VScroll);
            this.richTextBox1.FontChanged += new System.EventHandler(this.richTextBox1_FontChanged);
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            this.richTextBox1.Resize += new System.EventHandler(this.richTextBox1_Resize);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.richTextBox3);
            this.panel1.Location = new System.Drawing.Point(8, 343);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 100);
            this.panel1.TabIndex = 7;
            // 
            // richTextBox3
            // 
            this.richTextBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox3.Location = new System.Drawing.Point(0, 0);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(784, 100);
            this.richTextBox3.TabIndex = 0;
            this.richTextBox3.Text = "";
            // 
            // State_Stack
            // 
            this.State_Stack.HeaderText = "状态栈";
            this.State_Stack.Name = "State_Stack";
            this.State_Stack.Width = 150;
            // 
            // type
            // 
            this.type.HeaderText = "符号栈";
            this.type.Name = "type";
            this.type.ReadOnly = true;
            this.type.Width = 210;
            // 
            // value
            // 
            this.value.HeaderText = "输入符号";
            this.value.Name = "value";
            this.value.ReadOnly = true;
            this.value.Width = 160;
            // 
            // FormEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel5);
            this.Name = "FormEdit";
            this.Text = "FormEdit";
            this.Load += new System.EventHandler(this.FormEdit_Load);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox richTextBox2;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        public System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.DataGridViewTextBoxColumn State_Stack;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn value;
    }
}