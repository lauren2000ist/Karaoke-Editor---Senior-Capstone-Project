namespace Karaoke
{
    partial class Sing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sing));
            this.pbLineLength = new System.Windows.Forms.ProgressBar();
            this.btnForwardFifteen = new System.Windows.Forms.Button();
            this.btnBackFifteen = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.tbPosition = new System.Windows.Forms.RichTextBox();
            this.trackbarSong = new System.Windows.Forms.TrackBar();
            this.tbLyrics = new System.Windows.Forms.RichTextBox();
            this.labelFocus = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnAudio = new System.Windows.Forms.Button();
            this.btnLyric = new System.Windows.Forms.Button();
            this.tbMessage = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackbarSong)).BeginInit();
            this.SuspendLayout();
            // 
            // pbLineLength
            // 
            this.pbLineLength.Location = new System.Drawing.Point(11, 71);
            this.pbLineLength.Name = "pbLineLength";
            this.pbLineLength.Size = new System.Drawing.Size(776, 10);
            this.pbLineLength.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbLineLength.TabIndex = 17;
            // 
            // btnForwardFifteen
            // 
            this.btnForwardFifteen.Location = new System.Drawing.Point(536, 370);
            this.btnForwardFifteen.Name = "btnForwardFifteen";
            this.btnForwardFifteen.Size = new System.Drawing.Size(134, 23);
            this.btnForwardFifteen.TabIndex = 16;
            this.btnForwardFifteen.Text = "+15 Seconds";
            this.btnForwardFifteen.UseVisualStyleBackColor = true;
            this.btnForwardFifteen.Click += new System.EventHandler(this.btnForwardFifteen_Click);
            // 
            // btnBackFifteen
            // 
            this.btnBackFifteen.Location = new System.Drawing.Point(129, 370);
            this.btnBackFifteen.Name = "btnBackFifteen";
            this.btnBackFifteen.Size = new System.Drawing.Size(134, 23);
            this.btnBackFifteen.TabIndex = 15;
            this.btnBackFifteen.Text = "-15 Seconds";
            this.btnBackFifteen.UseVisualStyleBackColor = true;
            this.btnBackFifteen.Click += new System.EventHandler(this.btnBackFifteen_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(536, 464);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(251, 23);
            this.btnStop.TabIndex = 14;
            this.btnStop.Text = "Start Over";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(269, 464);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(261, 23);
            this.btnPause.TabIndex = 13;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(12, 464);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(251, 23);
            this.btnPlay.TabIndex = 12;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // tbPosition
            // 
            this.tbPosition.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tbPosition.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPosition.Location = new System.Drawing.Point(269, 369);
            this.tbPosition.Name = "tbPosition";
            this.tbPosition.ReadOnly = true;
            this.tbPosition.Size = new System.Drawing.Size(261, 24);
            this.tbPosition.TabIndex = 11;
            this.tbPosition.Text = "";
            // 
            // trackbarSong
            // 
            this.trackbarSong.Location = new System.Drawing.Point(13, 402);
            this.trackbarSong.Name = "trackbarSong";
            this.trackbarSong.Size = new System.Drawing.Size(775, 56);
            this.trackbarSong.TabIndex = 10;
            this.trackbarSong.Scroll += new System.EventHandler(this.trackbarSong_Scroll);
            this.trackbarSong.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trackbarSong_MouseDown);
            this.trackbarSong.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackbarSong_MouseUp);
            // 
            // tbLyrics
            // 
            this.tbLyrics.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.tbLyrics.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbLyrics.Location = new System.Drawing.Point(12, 87);
            this.tbLyrics.Name = "tbLyrics";
            this.tbLyrics.ReadOnly = true;
            this.tbLyrics.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.tbLyrics.Size = new System.Drawing.Size(775, 261);
            this.tbLyrics.TabIndex = 9;
            this.tbLyrics.Text = "";
            // 
            // labelFocus
            // 
            this.labelFocus.AutoSize = true;
            this.labelFocus.Location = new System.Drawing.Point(746, 320);
            this.labelFocus.Name = "labelFocus";
            this.labelFocus.Size = new System.Drawing.Size(0, 16);
            this.labelFocus.TabIndex = 18;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(12, 13);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(57, 26);
            this.btnBack.TabIndex = 19;
            this.btnBack.Text = "Home";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnAudio
            // 
            this.btnAudio.Location = new System.Drawing.Point(75, 13);
            this.btnAudio.Name = "btnAudio";
            this.btnAudio.Size = new System.Drawing.Size(140, 26);
            this.btnAudio.TabIndex = 20;
            this.btnAudio.Text = "Audio File";
            this.btnAudio.UseVisualStyleBackColor = true;
            this.btnAudio.Click += new System.EventHandler(this.btnAudio_Click);
            // 
            // btnLyric
            // 
            this.btnLyric.Location = new System.Drawing.Point(221, 13);
            this.btnLyric.Name = "btnLyric";
            this.btnLyric.Size = new System.Drawing.Size(140, 26);
            this.btnLyric.TabIndex = 21;
            this.btnLyric.Text = "Lyric File";
            this.btnLyric.UseVisualStyleBackColor = true;
            this.btnLyric.Click += new System.EventHandler(this.btnLyric_Click);
            // 
            // tbMessage
            // 
            this.tbMessage.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.tbMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbMessage.Location = new System.Drawing.Point(367, 12);
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.ReadOnly = true;
            this.tbMessage.Size = new System.Drawing.Size(420, 50);
            this.tbMessage.TabIndex = 22;
            this.tbMessage.Text = "";
            // 
            // Sing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(814, 499);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.btnLyric);
            this.Controls.Add(this.btnAudio);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.labelFocus);
            this.Controls.Add(this.pbLineLength);
            this.Controls.Add(this.btnForwardFifteen);
            this.Controls.Add(this.btnBackFifteen);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.tbPosition);
            this.Controls.Add(this.trackbarSong);
            this.Controls.Add(this.tbLyrics);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Sing";
            this.Text = "Karaoke";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Sing_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.trackbarSong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pbLineLength;
        private System.Windows.Forms.Button btnForwardFifteen;
        private System.Windows.Forms.Button btnBackFifteen;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.RichTextBox tbPosition;
        private System.Windows.Forms.TrackBar trackbarSong;
        private System.Windows.Forms.RichTextBox tbLyrics;
        private System.Windows.Forms.Label labelFocus;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnAudio;
        private System.Windows.Forms.Button btnLyric;
        private System.Windows.Forms.RichTextBox tbMessage;
    }
}