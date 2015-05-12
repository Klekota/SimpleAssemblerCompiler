

namespace lexer
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fILEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nEWToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bUILDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildSolutionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelErrorList = new System.Windows.Forms.Label();
            this.labelOutput = new System.Windows.Forms.Label();
            this.richTextBoxOutput = new System.Windows.Forms.RichTextBox();
            this.richTextBoxErrorList = new System.Windows.Forms.RichTextBox();
            this.numberedRTBCode = new LineNumbers.NumberedRTB();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fILEToolStripMenuItem,
            this.bUILDToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(1021, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fILEToolStripMenuItem
            // 
            this.fILEToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nEWToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fILEToolStripMenuItem.Name = "fILEToolStripMenuItem";
            this.fILEToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.fILEToolStripMenuItem.Text = "FILE";
            // 
            // nEWToolStripMenuItem
            // 
            this.nEWToolStripMenuItem.Name = "nEWToolStripMenuItem";
            this.nEWToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.nEWToolStripMenuItem.Text = "New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // bUILDToolStripMenuItem
            // 
            this.bUILDToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buildSolutionToolStripMenuItem});
            this.bUILDToolStripMenuItem.Name = "bUILDToolStripMenuItem";
            this.bUILDToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.bUILDToolStripMenuItem.Text = "BUILD";
            // 
            // buildSolutionToolStripMenuItem
            // 
            this.buildSolutionToolStripMenuItem.Name = "buildSolutionToolStripMenuItem";
            this.buildSolutionToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.buildSolutionToolStripMenuItem.Text = "Build solution";
            this.buildSolutionToolStripMenuItem.Click += new System.EventHandler(this.buildSolutionToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "files|*.asm";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.labelErrorList, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelOutput, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.richTextBoxOutput, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.richTextBoxErrorList, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.numberedRTBCode, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(9, 24);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1003, 491);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // labelErrorList
            // 
            this.labelErrorList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelErrorList.AutoSize = true;
            this.labelErrorList.Location = new System.Drawing.Point(501, 321);
            this.labelErrorList.Margin = new System.Windows.Forms.Padding(0);
            this.labelErrorList.Name = "labelErrorList";
            this.labelErrorList.Size = new System.Drawing.Size(502, 20);
            this.labelErrorList.TabIndex = 4;
            this.labelErrorList.Text = "Error List";
            this.labelErrorList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelOutput
            // 
            this.labelOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelOutput.AutoSize = true;
            this.labelOutput.Location = new System.Drawing.Point(501, 0);
            this.labelOutput.Margin = new System.Windows.Forms.Padding(0);
            this.labelOutput.Name = "labelOutput";
            this.labelOutput.Size = new System.Drawing.Size(502, 20);
            this.labelOutput.TabIndex = 2;
            this.labelOutput.Text = "Output";
            this.labelOutput.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // richTextBoxOutput
            // 
            this.richTextBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxOutput.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxOutput.Location = new System.Drawing.Point(504, 23);
            this.richTextBoxOutput.Name = "richTextBoxOutput";
            this.richTextBoxOutput.Size = new System.Drawing.Size(496, 295);
            this.richTextBoxOutput.TabIndex = 3;
            this.richTextBoxOutput.Text = "";
            // 
            // richTextBoxErrorList
            // 
            this.richTextBoxErrorList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxErrorList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxErrorList.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBoxErrorList.Location = new System.Drawing.Point(504, 344);
            this.richTextBoxErrorList.Name = "richTextBoxErrorList";
            this.richTextBoxErrorList.Size = new System.Drawing.Size(496, 144);
            this.richTextBoxErrorList.TabIndex = 5;
            this.richTextBoxErrorList.Text = "";
            // 
            // numberedRTBCode
            // 
            this.numberedRTBCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numberedRTBCode.BackColor = System.Drawing.SystemColors.Window;
            this.numberedRTBCode.Font = new System.Drawing.Font("Consolas", 10.75F);
            this.numberedRTBCode.Location = new System.Drawing.Point(4, 4);
            this.numberedRTBCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numberedRTBCode.Name = "numberedRTBCode";
            this.tableLayoutPanel1.SetRowSpan(this.numberedRTBCode, 4);
            this.numberedRTBCode.Size = new System.Drawing.Size(493, 483);
            this.numberedRTBCode.TabIndex = 6;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "files *.asm|*.asm";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1021, 521);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fILEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nEWToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem bUILDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildSolutionToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label labelOutput;
        private System.Windows.Forms.RichTextBox richTextBoxOutput;
        private System.Windows.Forms.Label labelErrorList;
        private System.Windows.Forms.RichTextBox richTextBoxErrorList;
        private LineNumbers.NumberedRTB numberedRTBCode;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

