namespace Karaoke
{
    partial class Timestamp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Timestamp));
            this.btnForwardFifteen = new System.Windows.Forms.Button();
            this.btnBackFifteen = new System.Windows.Forms.Button();
            this.btnRedo = new System.Windows.Forms.Button();
            this.tbTimestamps = new System.Windows.Forms.RichTextBox();
            this.tbLyrics = new System.Windows.Forms.RichTextBox();
            this.labelTimeStamps = new System.Windows.Forms.Label();
            this.labelLyrics = new System.Windows.Forms.Label();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnSaveandContinue = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.tbPosition = new System.Windows.Forms.TextBox();
            this.trackbarSong = new System.Windows.Forms.TrackBar();
            this.btnAudioFile = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackbarSong)).BeginInit();
            this.SuspendLayout();
            // 
            // btnForwardFifteen
            // 
            this.btnForwardFifteen.Location = new System.Drawing.Point(172, 226);
            this.btnForwardFifteen.Name = "btnForwardFifteen";
            this.btnForwardFifteen.Size = new System.Drawing.Size(129, 23);
            this.btnForwardFifteen.TabIndex = 35;
            this.btnForwardFifteen.Text = "+15 Seconds";
            this.btnForwardFifteen.UseVisualStyleBackColor = true;
            this.btnForwardFifteen.Click += new System.EventHandler(this.btnForwardFifteen_Click);
            // 
            // btnBackFifteen
            // 
            this.btnBackFifteen.Location = new System.Drawing.Point(13, 226);
            this.btnBackFifteen.Name = "btnBackFifteen";
            this.btnBackFifteen.Size = new System.Drawing.Size(129, 23);
            this.btnBackFifteen.TabIndex = 34;
            this.btnBackFifteen.Text = "-15 Seconds";
            this.btnBackFifteen.UseVisualStyleBackColor = true;
            this.btnBackFifteen.Click += new System.EventHandler(this.btnBackFifteen_Click);
            // 
            // btnRedo
            // 
            this.btnRedo.Location = new System.Drawing.Point(14, 197);
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(287, 23);
            this.btnRedo.TabIndex = 33;
            this.btnRedo.Text = "Redo";
            this.btnRedo.UseVisualStyleBackColor = true;
            this.btnRedo.Click += new System.EventHandler(this.btnRedo_Click);
            // 
            // tbTimestamps
            // 
            this.tbTimestamps.Location = new System.Drawing.Point(610, 25);
            this.tbTimestamps.Name = "tbTimestamps";
            this.tbTimestamps.ReadOnly = true;
            this.tbTimestamps.Size = new System.Drawing.Size(178, 352);
            this.tbTimestamps.TabIndex = 32;
            this.tbTimestamps.Text = "";
            // 
            // tbLyrics
            // 
            this.tbLyrics.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLyrics.Location = new System.Drawing.Point(307, 25);
            this.tbLyrics.Name = "tbLyrics";
            this.tbLyrics.ReadOnly = true;
            this.tbLyrics.Size = new System.Drawing.Size(297, 352);
            this.tbLyrics.TabIndex = 31;
            this.tbLyrics.Text = "";
            // 
            // labelTimeStamps
            // 
            this.labelTimeStamps.AutoSize = true;
            this.labelTimeStamps.Location = new System.Drawing.Point(607, 6);
            this.labelTimeStamps.Name = "labelTimeStamps";
            this.labelTimeStamps.Size = new System.Drawing.Size(87, 16);
            this.labelTimeStamps.TabIndex = 30;
            this.labelTimeStamps.Text = "TimeStamps:";
            // 
            // labelLyrics
            // 
            this.labelLyrics.AutoSize = true;
            this.labelLyrics.Location = new System.Drawing.Point(304, 5);
            this.labelLyrics.Name = "labelLyrics";
            this.labelLyrics.Size = new System.Drawing.Size(45, 16);
            this.labelLyrics.TabIndex = 29;
            this.labelLyrics.Text = "Lyrics:";
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(13, 11);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(26, 26);
            this.btnHelp.TabIndex = 28;
            this.btnHelp.Text = "?";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnSaveandContinue
            // 
            this.btnSaveandContinue.Location = new System.Drawing.Point(623, 422);
            this.btnSaveandContinue.Name = "btnSaveandContinue";
            this.btnSaveandContinue.Size = new System.Drawing.Size(165, 23);
            this.btnSaveandContinue.TabIndex = 27;
            this.btnSaveandContinue.Text = "Save and Continue";
            this.btnSaveandContinue.UseVisualStyleBackColor = true;
            this.btnSaveandContinue.Click += new System.EventHandler(this.btnSaveandContinue_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(542, 422);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 26;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.Location = new System.Drawing.Point(14, 172);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(287, 23);
            this.btnUndo.TabIndex = 25;
            this.btnUndo.Text = "Undo";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // tbPosition
            // 
            this.tbPosition.Location = new System.Drawing.Point(14, 355);
            this.tbPosition.Name = "tbPosition";
            this.tbPosition.Size = new System.Drawing.Size(116, 22);
            this.tbPosition.TabIndex = 24;
            // 
            // trackbarSong
            // 
            this.trackbarSong.Location = new System.Drawing.Point(13, 383);
            this.trackbarSong.Name = "trackbarSong";
            this.trackbarSong.Size = new System.Drawing.Size(775, 56);
            this.trackbarSong.TabIndex = 23;
            this.trackbarSong.Scroll += new System.EventHandler(this.trackbarSong_Scroll);
            this.trackbarSong.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trackbarSong_MouseDown);
            this.trackbarSong.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackbarSong_MouseUp);
            // 
            // btnAudioFile
            // 
            this.btnAudioFile.Location = new System.Drawing.Point(14, 81);
            this.btnAudioFile.Name = "btnAudioFile";
            this.btnAudioFile.Size = new System.Drawing.Size(287, 23);
            this.btnAudioFile.TabIndex = 22;
            this.btnAudioFile.Text = "Choose Audio File";
            this.btnAudioFile.UseVisualStyleBackColor = true;
            this.btnAudioFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(176, 52);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(125, 23);
            this.btnStop.TabIndex = 21;
            this.btnStop.Text = "Start Over";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(94, 52);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 20;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(13, 52);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(75, 23);
            this.btnPlay.TabIndex = 19;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(45, 11);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(57, 26);
            this.btnBack.TabIndex = 36;
            this.btnBack.Text = "Home";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // Timestamp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnForwardFifteen);
            this.Controls.Add(this.btnBackFifteen);
            this.Controls.Add(this.btnRedo);
            this.Controls.Add(this.tbTimestamps);
            this.Controls.Add(this.tbLyrics);
            this.Controls.Add(this.labelTimeStamps);
            this.Controls.Add(this.labelLyrics);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnSaveandContinue);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.tbPosition);
            this.Controls.Add(this.trackbarSong);
            this.Controls.Add(this.btnAudioFile);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnPlay);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Timestamp";
            this.Text = "Karaoke";
            this.Load += new System.EventHandler(this.Timestamp_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Timestamp_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.trackbarSong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnForwardFifteen;
        private System.Windows.Forms.Button btnBackFifteen;
        private System.Windows.Forms.Button btnRedo;
        private System.Windows.Forms.RichTextBox tbTimestamps;
        private System.Windows.Forms.RichTextBox tbLyrics;
        private System.Windows.Forms.Label labelTimeStamps;
        private System.Windows.Forms.Label labelLyrics;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnSaveandContinue;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.TextBox tbPosition;
        private System.Windows.Forms.TrackBar trackbarSong;
        private System.Windows.Forms.Button btnAudioFile;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnBack;
    }
}