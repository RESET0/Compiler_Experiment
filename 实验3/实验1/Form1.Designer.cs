namespace 实验1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.File_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewFile_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFile_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Save_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseFile_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Exit_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Edit_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Cut_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aToolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.FindAndReplace_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File_ToolStripMenuItem,
            this.Edit_ToolStripMenuItem,
            this.compileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(814, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // File_ToolStripMenuItem
            // 
            this.File_ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewFile_ToolStripMenuItem,
            this.OpenFile_ToolStripMenuItem,
            this.Save_ToolStripMenuItem,
            this.CloseFile_ToolStripMenuItem,
            this.Exit_ToolStripMenuItem});
            this.File_ToolStripMenuItem.Name = "File_ToolStripMenuItem";
            this.File_ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.File_ToolStripMenuItem.Text = "文件";
            // 
            // NewFile_ToolStripMenuItem
            // 
            this.NewFile_ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("NewFile_ToolStripMenuItem.Image")));
            this.NewFile_ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.NewFile_ToolStripMenuItem.Name = "NewFile_ToolStripMenuItem";
            this.NewFile_ToolStripMenuItem.Size = new System.Drawing.Size(126, 28);
            this.NewFile_ToolStripMenuItem.Text = "新建文件";
            this.NewFile_ToolStripMenuItem.Click += new System.EventHandler(this.NewFile_ToolStripMenuItem_Click);
            // 
            // OpenFile_ToolStripMenuItem
            // 
            this.OpenFile_ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("OpenFile_ToolStripMenuItem.Image")));
            this.OpenFile_ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.OpenFile_ToolStripMenuItem.Name = "OpenFile_ToolStripMenuItem";
            this.OpenFile_ToolStripMenuItem.Size = new System.Drawing.Size(126, 28);
            this.OpenFile_ToolStripMenuItem.Text = "打开文件";
            this.OpenFile_ToolStripMenuItem.Click += new System.EventHandler(this.OpenFile_ToolStripMenuItem_Click);
            // 
            // Save_ToolStripMenuItem
            // 
            this.Save_ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("Save_ToolStripMenuItem.Image")));
            this.Save_ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Save_ToolStripMenuItem.Name = "Save_ToolStripMenuItem";
            this.Save_ToolStripMenuItem.Size = new System.Drawing.Size(126, 28);
            this.Save_ToolStripMenuItem.Text = "保存文件";
            this.Save_ToolStripMenuItem.Click += new System.EventHandler(this.Save_ToolStripMenuItem_Click);
            // 
            // CloseFile_ToolStripMenuItem
            // 
            this.CloseFile_ToolStripMenuItem.Name = "CloseFile_ToolStripMenuItem";
            this.CloseFile_ToolStripMenuItem.Size = new System.Drawing.Size(126, 28);
            this.CloseFile_ToolStripMenuItem.Text = "关闭";
            this.CloseFile_ToolStripMenuItem.Click += new System.EventHandler(this.CloseFile_ToolStripMenuItem_Click);
            // 
            // Exit_ToolStripMenuItem
            // 
            this.Exit_ToolStripMenuItem.Name = "Exit_ToolStripMenuItem";
            this.Exit_ToolStripMenuItem.Size = new System.Drawing.Size(126, 28);
            this.Exit_ToolStripMenuItem.Text = "退出";
            this.Exit_ToolStripMenuItem.Click += new System.EventHandler(this.Exit_ToolStripMenuItem_Click);
            // 
            // Edit_ToolStripMenuItem
            // 
            this.Edit_ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.aToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.Cut_ToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.aToolStripMenuItem1,
            this.FindAndReplace_ToolStripMenuItem});
            this.Edit_ToolStripMenuItem.Name = "Edit_ToolStripMenuItem";
            this.Edit_ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.Edit_ToolStripMenuItem.Text = "编辑";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // aToolStripMenuItem
            // 
            this.aToolStripMenuItem.Name = "aToolStripMenuItem";
            this.aToolStripMenuItem.Size = new System.Drawing.Size(173, 6);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // Cut_ToolStripMenuItem
            // 
            this.Cut_ToolStripMenuItem.Name = "Cut_ToolStripMenuItem";
            this.Cut_ToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.Cut_ToolStripMenuItem.Text = "Cut";
            this.Cut_ToolStripMenuItem.Click += new System.EventHandler(this.Cut_ToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // aToolStripMenuItem1
            // 
            this.aToolStripMenuItem1.Name = "aToolStripMenuItem1";
            this.aToolStripMenuItem1.Size = new System.Drawing.Size(173, 6);
            // 
            // FindAndReplace_ToolStripMenuItem
            // 
            this.FindAndReplace_ToolStripMenuItem.Name = "FindAndReplace_ToolStripMenuItem";
            this.FindAndReplace_ToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.FindAndReplace_ToolStripMenuItem.Text = "Find and Replace";
            this.FindAndReplace_ToolStripMenuItem.Click += new System.EventHandler(this.FindAndReplace_ToolStripMenuItem_Click);
            // 
            // compileToolStripMenuItem
            // 
            this.compileToolStripMenuItem.Name = "compileToolStripMenuItem";
            this.compileToolStripMenuItem.Size = new System.Drawing.Size(82, 21);
            this.compileToolStripMenuItem.Text = "LR语法分析";
            this.compileToolStripMenuItem.Click += new System.EventHandler(this.compileToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 502);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem File_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewFile_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenFile_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Save_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CloseFile_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Exit_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Edit_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FindAndReplace_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Cut_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator aToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator aToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem compileToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}

