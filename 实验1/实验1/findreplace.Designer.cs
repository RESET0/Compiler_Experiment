namespace 实验1
{
    partial class findreplace
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
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Replace = new System.Windows.Forms.Button();
            this.Find = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(65, 40);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(216, 21);
            this.textBox2.TabIndex = 12;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(65, 10);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(216, 21);
            this.textBox1.TabIndex = 11;
            // 
            // Replace
            // 
            this.Replace.Location = new System.Drawing.Point(187, 79);
            this.Replace.Name = "Replace";
            this.Replace.Size = new System.Drawing.Size(62, 23);
            this.Replace.TabIndex = 10;
            this.Replace.Text = "Replace";
            this.Replace.UseVisualStyleBackColor = true;
            this.Replace.Click += new System.EventHandler(this.Replace_Click);
            // 
            // Find
            // 
            this.Find.Cursor = System.Windows.Forms.Cursors.Default;
            this.Find.Location = new System.Drawing.Point(41, 79);
            this.Find.Name = "Find";
            this.Find.Size = new System.Drawing.Size(85, 23);
            this.Find.TabIndex = 9;
            this.Find.Text = "Find";
            this.Find.UseVisualStyleBackColor = true;
            this.Find.Click += new System.EventHandler(this.Find_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "Replace";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "Find";
            // 
            // findreplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 113);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Replace);
            this.Controls.Add(this.Find);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "findreplace";
            this.Text = "Find and Replace";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button Replace;
        private System.Windows.Forms.Button Find;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}