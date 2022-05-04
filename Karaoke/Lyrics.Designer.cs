namespace Karaoke
{
    partial class Lyrics
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lyrics));
            this.btnHelp = new System.Windows.Forms.Button();
            this.labelLyrics = new System.Windows.Forms.Label();
            this.labelNumLines = new System.Windows.Forms.Label();
            this.tbNumLines = new System.Windows.Forms.RichTextBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRedo = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnChooseLyrics = new System.Windows.Forms.Button();
            this.tbLyrics = new System.Windows.Forms.RichTextBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnHelp
            // 
            this.btnHelp.AutoSize = true;
            this.btnHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHelp.Location = new System.Drawing.Point(12, 11);
            this.btnHelp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(25, 28);
            this.btnHelp.TabIndex = 19;
            this.btnHelp.Text = "?";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // labelLyrics
            // 
            this.labelLyrics.AutoSize = true;
            this.labelLyrics.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLyrics.Location = new System.Drawing.Point(32, 45);
            this.labelLyrics.Name = "labelLyrics";
            this.labelLyrics.Size = new System.Drawing.Size(45, 16);
            this.labelLyrics.TabIndex = 18;
            this.labelLyrics.Text = "Lyrics:";
            // 
            // labelNumLines
            // 
            this.labelNumLines.AutoSize = true;
            this.labelNumLines.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNumLines.Location = new System.Drawing.Point(622, 27);
            this.labelNumLines.Name = "labelNumLines";
            this.labelNumLines.Size = new System.Drawing.Size(73, 16);
            this.labelNumLines.TabIndex = 17;
            this.labelNumLines.Text = "Num Lines:";
            this.labelNumLines.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbNumLines
            // 
            this.tbNumLines.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNumLines.Location = new System.Drawing.Point(718, 20);
            this.tbNumLines.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbNumLines.Name = "tbNumLines";
            this.tbNumLines.ReadOnly = true;
            this.tbNumLines.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.tbNumLines.Size = new System.Drawing.Size(75, 32);
            this.tbNumLines.TabIndex = 16;
            this.tbNumLines.Text = "";
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(549, 415);
            this.btnExport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 32);
            this.btnExport.TabIndex = 15;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = true;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(630, 416);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(163, 31);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save and Continue";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRedo
            // 
            this.btnRedo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRedo.Location = new System.Drawing.Point(113, 415);
            this.btnRedo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(75, 32);
            this.btnRedo.TabIndex = 13;
            this.btnRedo.Text = "Redo";
            this.btnRedo.UseVisualStyleBackColor = true;
            this.btnRedo.Click += new System.EventHandler(this.btnRedo_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUndo.Location = new System.Drawing.Point(32, 415);
            this.btnUndo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(75, 32);
            this.btnUndo.TabIndex = 12;
            this.btnUndo.Text = "Undo";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnChooseLyrics
            // 
            this.btnChooseLyrics.AutoSize = true;
            this.btnChooseLyrics.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChooseLyrics.Location = new System.Drawing.Point(276, 20);
            this.btnChooseLyrics.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnChooseLyrics.Name = "btnChooseLyrics";
            this.btnChooseLyrics.Size = new System.Drawing.Size(231, 31);
            this.btnChooseLyrics.TabIndex = 11;
            this.btnChooseLyrics.Text = "Choose Lyric File";
            this.btnChooseLyrics.UseVisualStyleBackColor = true;
            this.btnChooseLyrics.Click += new System.EventHandler(this.btnChooseLyrics_Click);
            // 
            // tbLyrics
            // 
            this.tbLyrics.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLyrics.Location = new System.Drawing.Point(32, 67);
            this.tbLyrics.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbLyrics.Name = "tbLyrics";
            this.tbLyrics.Size = new System.Drawing.Size(761, 344);
            this.tbLyrics.TabIndex = 10;
            this.tbLyrics.Text = "";
            this.tbLyrics.TextChanged += new System.EventHandler(this.tbLyrics_TextChanged);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(43, 11);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(57, 28);
            this.btnBack.TabIndex = 37;
            this.btnBack.Text = "Home";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // Lyrics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.labelLyrics);
            this.Controls.Add(this.labelNumLines);
            this.Controls.Add(this.tbNumLines);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnRedo);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.btnChooseLyrics);
            this.Controls.Add(this.tbLyrics);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Lyrics";
            this.Text = "Karaoke";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Label labelLyrics;
        private System.Windows.Forms.Label labelNumLines;
        private System.Windows.Forms.RichTextBox tbNumLines;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRedo;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnChooseLyrics;
        private System.Windows.Forms.RichTextBox tbLyrics;
        private System.Windows.Forms.Button btnBack;
    }
}