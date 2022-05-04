using NAudio.Wave;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Karaoke
{
    public partial class Sing : Form
    {
        private WaveOut outputDevice;
        private AudioFileReader audioFile;
        private OpenFileDialog audioFileDialog;
        private OpenFileDialog lyricFileDialog;
        private DialogResult audioFileResult = DialogResult.None;
        private DialogResult lyricFileResult = DialogResult.None;
        private String audioFileName;
        private String lyricFileName;
        private Timer songTimer;
        private TimeSpan[] theTimeStamps;
        private TimeSpan[] lineLengths;
        private string[] lyricLines;
        private int totalLines;
        private int lineCount;
        private string curLine;
        private string nextLine;
        private int tickCount;
        private double amountMove;
        private TimeSpan curTime;
        private int tbp; //trackbar position
        private bool firstMove;
        private bool fromEdit;
        private bool firstTime;

        public Sing() //If the user is coming from the main screen
        {
            InitializeComponent();
            fromEdit = false;
            btnPlay.Enabled = false;
            btnPause.Enabled = false;
            btnStop.Enabled = false;
            btnForwardFifteen.Enabled = false;
            btnBackFifteen.Enabled = false;
            btnLyric.Enabled = false;
            firstTime = true;
        }

        public Sing(string audioName, string[] lines, TimeSpan[] stamps) //If the user is coming from the timestamp section
        {
            InitializeComponent();
            audioFileName = audioName;
            lyricLines = lines;
            theTimeStamps = stamps;
            fromEdit = true;
            firstTime = true;
            setup();
        }

        #region Btn Clicks and Playback

        private void OnPlaybackStopped(object sender, StoppedEventArgs args)
        {
            if (audioFile != null)
            {
                audioFile.Position = 0;
            }

            songTimer.Stop();
            btnPlay.Enabled = true;
            btnPause.Enabled = false;
            btnStop.Enabled = false;
            lineCount = 0;
            tickCount = 1; //How many times the timer tick has occurred, used to help move the progress bar
            curLine = "";
            nextLine = lyricLines[lineCount];
            moveLyricLine(); //Displays the current line in Pink and the next line in gray below
            curLine = nextLine;
            nextLine = lyricLines[lineCount + 1];
            amountMove = 100 / (lineLengths[0].TotalSeconds * 2); //The amount that the progress bar needs to be increased every half-second based on the length of the line
            pbLineLength.Value = 0;
            trackbarSong.Value = 0;
            curTime = TimeSpan.Zero;
            tbPosition.Text = curTime.ToString();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            songTimer.Start();
            if(firstTime) //So that if the user changes the audio file, a new reader will be created
            {

                outputDevice = new WaveOut();
                outputDevice.PlaybackStopped += OnPlaybackStopped;
                

                audioFile = new AudioFileReader(audioFileName);
                outputDevice.Init(audioFile);
                firstTime = false;
                
            }
            outputDevice.Play();
            btnPause.Enabled = true;
            btnPlay.Enabled = false;
            btnStop.Enabled = true;
            btnAudio.Enabled = false;
            btnLyric.Enabled = false;
            btnForwardFifteen.Enabled = true;
            btnBackFifteen.Enabled = true;
            labelFocus.Focus(); //An invisible label to shift the focus off of a button
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            outputDevice.Pause();
            songTimer.Stop();
            btnPlay.Enabled = true;
            btnPause.Enabled = false;
            labelFocus.Focus(); //An invisible label to shift the focus off of a button
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (audioFile != null)
            { audioFile.Position = 0; }

            outputDevice?.Stop();
            songTimer?.Stop();

            trackbarSong.Value = 0;
            tbPosition.Text = new TimeSpan(0, 0, 0).ToString();
            lineCount = 0;
            curLine = "";
            curTime = TimeSpan.Zero;
            tickCount = 1; //How many timer ticks have occurred, used to help move the progress bar
            amountMove = 100 / (lineLengths[0].TotalSeconds * 2); //How much the progress bar needs to move every half-second based on the length of the line
            pbLineLength.Value = 0;
            nextLine = lyricLines[lineCount];
            moveLyricLine(); //Change the lyrics shown to the first line as the "next" line
            curLine = nextLine;
            nextLine = lyricLines[lineCount + 1];
            btnPause.Enabled = false;
            btnStop.Enabled = false;
            btnBackFifteen.Enabled = false;
            btnForwardFifteen.Enabled = false;
            btnAudio.Enabled = true;

            firstTime = true; //Resets the firstTime boolean so that if the user changes the audio file, a new reader will be initialized

            labelFocus.Focus(); //An invisible label to shift the focus off of a button
        }

        private void btnBackFifteen_Click(object sender, EventArgs e)
        {
            btnPause.PerformClick();

            if (audioFile.CurrentTime.TotalSeconds < 15) //If we aren't 15 seconds in, just go to the beginning
            {
                audioFile.Position = 0;
            }
            else //Otherwise go backwards 15 seconds
            {
                audioFile.CurrentTime = audioFile.CurrentTime - TimeSpan.FromSeconds(15);
            }

            curTime = audioFile.CurrentTime; //Get the current time once to be used later
            tbPosition.Text = (audioFile.CurrentTime - TimeSpan.FromMilliseconds(audioFile.CurrentTime.Milliseconds) - TimeSpan.FromHours(audioFile.CurrentTime.Hours)).ToString(); //Remove the milliseconds from display
            tickCount = 0; //Reset the tick count


            tbp = (int)curTime.TotalSeconds; //Get new value for trackbar
            if (tbp < trackbarSong.Maximum)
            {
                trackbarSong.Value = tbp;
            }
            else //If greater than maximum, set to maximum
            {
                trackbarSong.Value = trackbarSong.Maximum;
            }

            findLineNumber(); //Find where we need to be in the song
            updateLines(); //Change lyrics accordingly
            labelFocus.Focus(); //An invisible label to shift the focus off of a button
        }

        private void btnForwardFifteen_Click(object sender, EventArgs e)
        {
            btnPause.PerformClick();

            if (audioFile.CurrentTime.TotalSeconds + 15 >= audioFile.TotalTime.TotalSeconds) //If there aren't 15 seconds left, just go to the end
            {
                audioFile.CurrentTime = audioFile.TotalTime;
            }
            else //Otherwise go forward 15 seconds
            {
                audioFile.CurrentTime = audioFile.CurrentTime + TimeSpan.FromSeconds(15);
            }

            curTime = audioFile.CurrentTime; //Get the current time once to be used later
            tbPosition.Text = (audioFile.CurrentTime - TimeSpan.FromMilliseconds(audioFile.CurrentTime.Milliseconds)).ToString(); //Remove the milliseconds for display
            tickCount = 0; //Reset the tick count

            tbp = (int)curTime.TotalSeconds; //Get new value for trackbar
            if (tbp < trackbarSong.Maximum)
            {
                trackbarSong.Value = tbp;
            }
            else //If greater than maximum, then set to maximum
            {
                trackbarSong.Value = trackbarSong.Maximum;
            }

            findLineNumber(); //Find what line number we are now on
            updateLines(); //Adjust lyrics accordingly
            labelFocus.Focus(); //An invisible label to shift the focus off of a button
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            btnStop.PerformClick(); //Stop the audio before returning to the main screen

            Main m = new Main();
            Hide();
            m.ShowDialog();
        }

        private void btnAudio_Click(object sender, EventArgs e)
        {
            audioFileDialog = new OpenFileDialog();
            audioFileDialog.Title = "Audio File";
            audioFileDialog.Filter = "WAV Files (*.wav)|*.wav|M4A Files (*.m4a)|*.m4a|MP3 files (*.mp3)|*.mp3";
            audioFileDialog.Title = "Audio File";
            audioFileResult = audioFileDialog.ShowDialog();

            if (audioFileDialog.FileName.EndsWith(".mp3")) //Asks the user to convert MP3 files, see the documentation in the "btnAudio_Click" of Timestamp.cs for reasoning
            {
                var filePathWav = audioFileDialog.FileName.Remove(audioFileDialog.FileName.Length - 4, 4);
                var fileNameWav = audioFileDialog.SafeFileName.Remove(audioFileDialog.SafeFileName.Length - 4, 4);
                if (File.Exists(filePathWav + ".wav")) //If a WAV file with same name as MP3 already exists, use that
                {
                    audioFileName = filePathWav + ".wav";
                    MessageBox.Show("using \"" + fileNameWav + ".wav\" as audio file", "Wav File Name");
                    audioFileResult = DialogResult.OK;
                    btnLyric.Enabled = true;
                }
                else //Otherwise prompt for conversion
                {
                    MessageBoxButtons mb = MessageBoxButtons.OKCancel;
                    DialogResult mbResult = MessageBox.Show("MP3 Files Must Be Converted to WAV File, Click 'Okay' to continue or click 'Cancel' to choose new file", "MP3 File Selected", mb);
                    if (mbResult == DialogResult.OK)
                    {
                        var saveFileDialog = new SaveFileDialog();
                        saveFileDialog.Filter = "WAV Files (*.wav)|*.wav*";
                        saveFileDialog.Title = "Save WAV File";
                        saveFileDialog.FileName = fileNameWav + ".wav";
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            audioFileName = saveFileDialog.FileName;
                            MessageBox.Show(saveFileDialog.FileName, "Wav File Name");
                            using (MediaFoundationReader mr = new MediaFoundationReader(audioFileDialog.FileName))
                            {
                                WaveFileWriter.CreateWaveFile(audioFileName, mr);
                            }
                            audioFileResult = DialogResult.OK;
                            btnLyric.Enabled = true;
                        }
                    }

                }
            }
            else if (audioFileResult == DialogResult.OK)
            {
                audioFileName = audioFileDialog.FileName; //Set the file name
                btnLyric.Enabled = true;
            }
        }

        private void btnLyric_Click(object sender, EventArgs e)
        {
            lyricFileDialog = new OpenFileDialog();
            lyricFileDialog.Filter = "LRC Files (*.lrc)|*.lrc";
            lyricFileDialog.Title = "Lyric File";
            lyricFileResult = lyricFileDialog.ShowDialog();
            if (lyricFileResult == DialogResult.OK)
            {
                lyricFileName = lyricFileDialog.FileName;
                setup(); //Read in lyrics and timestamps
            }
        }

        #endregion

        #region Trackbar Controls

        private void trackbarSetup()
        {
            trackbarSong.Enabled = true;
            trackbarSong.Minimum = 0;
            trackbarSong.Maximum = (int)audioFile.TotalTime.TotalSeconds;
            trackbarSong.Value = 0;
            trackbarSong.TickFrequency = 30;
        }

        private void trackbarSong_Scroll(object sender, EventArgs e)
        {
            int tsp = trackbarSong.Value; //Get where the trackbar is and set the audio file position to that value
            audioFile.CurrentTime = TimeSpan.FromSeconds(tsp);
        }

        private void trackbarSong_MouseUp(object sender, MouseEventArgs e)
        {
            findLineNumber(); //Find where we need to be
            updateLines(); //Set the values of nextLine and currentLine accordingly
            moveLyricLine(); //Update the textbox with the correct lyrics
            tickCount = 10; //Force the timerTick function to check values the next time it ticks
        }

        private void trackbarSong_MouseDown(object sender, MouseEventArgs e)
        {

        }

        #endregion

        #region Misc Functions
        private void Sing_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) //Pause or play the music
            {
                if (btnPause.Enabled)
                {
                    btnPause.PerformClick();
                }
                else if (btnPlay.Enabled)
                {
                    btnPlay.PerformClick();
                }
            }
        }

        private void setup()
        {
            
            if (!fromEdit) //If the user has come from the main screen
            {
                if(audioFileResult != DialogResult.OK && lyricFileResult != DialogResult.OK) //Make sure both audio file and lrc file have been "opened"
                {
                    return;
                }

                var allLines = File.ReadAllLines(lyricFileName);
                totalLines = allLines.Length;
                theTimeStamps = new TimeSpan[totalLines];
                lyricLines = new string[totalLines];
                lineCount = 0;
                StringBuilder sb = new StringBuilder();
                String timeStamp;
                foreach (var line in allLines) //See "editExisting" fucntion in Timestamp.cs for explanation of this section
                {
                    int j = 0;
                    StringBuilder time = new StringBuilder();
                    String tempString = String.Concat(line.Where(c => Char.IsDigit(c) || Char.Equals(c, '.') || Char.Equals(c, ':')));

                    var pieces = line.Split(' ');

                    timeStamp = tempString;

                    var numbers = timeStamp.Split(':');
                    var split = numbers[numbers.Length - 1].Split('.');
                    if (numbers.Length + 1 == 4)
                    {
                        theTimeStamps[lineCount] = new TimeSpan(Int32.Parse(numbers[0]), Int32.Parse(numbers[1]), Int32.Parse(split[0]));
                        for (int i = 1; i < pieces.Length; i++)
                        {
                            sb.Append(pieces[i] + " ");
                        }
                    }
                    else
                    {
                        theTimeStamps[lineCount] = new TimeSpan(0, Int32.Parse(numbers[0]), Int32.Parse(split[0]));
                        pieces[0] = String.Concat(pieces[0].Where(c => Char.IsLetter(c) || (Char.IsPunctuation(c) && !Char.Equals(c, ':') && !Char.Equals(c, '.') && !Char.Equals(c, '[') && !Char.Equals(c, ']'))));
                        for (int i = 0; i < pieces.Length; i++)
                        {
                            sb.Append(pieces[i] + " ");
                        }

                    }

                    lyricLines[lineCount] = sb.ToString();
                    sb.Clear();
                    lineCount++;

                } //foreach

            } //!fromEdit
            else
            {
                totalLines = lyricLines.Length;                
            }

            btnPlay.Enabled = true;
            btnStop.Enabled = true;
            btnPause.Enabled = false;
            btnLyric.Enabled = false;
            btnAudio.Enabled = false;

            this.FormClosing += btnStop_Click;
            this.FormClosing += btnBack_Click;

            if (outputDevice == null)
            {
                outputDevice = new WaveOut();
                outputDevice.PlaybackStopped += OnPlaybackStopped;
            }
            if (audioFile == null)
            {
                audioFile = new AudioFileReader(audioFileName);
                outputDevice.Init(audioFile);
            }

            lineLengths = new TimeSpan[totalLines];
            lineCount = 0;
            lineLengths[0] = theTimeStamps[0];
            for (int i = 1; i < totalLines - 1; i++) //Calculate how long each line of lyrics is
            {
                lineLengths[i] = (theTimeStamps[i] - theTimeStamps[i - 1]);
            }
            lineLengths[totalLines - 1] = (audioFile.TotalTime - theTimeStamps[totalLines - 1]);

            curLine = "";
            nextLine = lyricLines[lineCount];
            amountMove = 100 / (lineLengths[0].TotalSeconds * 2); //Used to move the progress bar every half-second

            pbLineLength.Maximum = 100;
            pbLineLength.Value = 0;

            moveLyricLine(); //update the lyrics textbox
            trackbarSetup(); //Initialize the trackbar
            curLine = nextLine;
            nextLine = lyricLines[lineCount + 1];
            tickCount = 1;
            firstMove = true;
            curTime = TimeSpan.Zero;


            songTimer = new Timer();
            songTimer.Interval = 100;
            songTimer.Tick += SongTimer_Tick;


            tbPosition.Text = new TimeSpan().ToString();
            labelFocus.Focus(); //An invisible label to shift the focus off of a button
        }

        private void SongTimer_Tick(object sender, EventArgs e)
        {
            tickCount++;
            if (tickCount >= 5) //"half second"
            {
                if (pbLineLength.Value + amountMove > pbLineLength.Maximum) //If the progress bar has reached its maximum, stay at the maximum
                {
                    pbLineLength.Value = pbLineLength.Maximum;
                }
                else
                {
                    if (firstMove) //Move an additional "amountMove" the first time since the progress bar starts at 0
                    {
                        pbLineLength.Value = pbLineLength.Value + (int)amountMove;
                        firstMove = false;
                    }

                    if (pbLineLength.Value + amountMove > pbLineLength.Maximum) //If the additional one didn't put it over the maximum, then make the actual move
                    {
                        pbLineLength.Value = pbLineLength.Maximum;
                    }
                    else
                    {
                        pbLineLength.Value = pbLineLength.Value + (int)amountMove;
                    }

                }
                tickCount = 1; //Reset tickCount for next move
            }

            curTime = audioFile.CurrentTime; //Get where we currently are in the song
            tbp = (int)curTime.TotalSeconds; //Update the trackbar using that value

            if (tbp < trackbarSong.Maximum)
            {
                trackbarSong.Value = tbp;
            }
            else
            {
                trackbarSong.Value = trackbarSong.Maximum;
            }

            if (lineCount < totalLines && theTimeStamps[lineCount] <= curTime) //If the curTime is passed where the next timeStamp is
            {

                pbLineLength.Value = pbLineLength.Maximum;

                //Update the lyric textbox without calling a function
                tbLyrics.Clear();
                tbLyrics.SelectionFont = new Font("Tahoma", 18, FontStyle.Bold);
                tbLyrics.SelectionColor = System.Drawing.Color.DeepPink;
                tbLyrics.SelectionAlignment = HorizontalAlignment.Center;
                tbLyrics.AppendText(curLine);
                tbLyrics.SelectionColor = System.Drawing.Color.LightGray;
                tbLyrics.SelectionFont = new Font("Tahoma", 14, FontStyle.Bold);
                tbLyrics.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + nextLine);
                tbLyrics.DeselectAll();


                lineCount++;
                firstMove = true; //In order to make the "additional move" the next time

                if (lineCount < totalLines - 1) //If we haven't reached the last line, then get the next line and adjust necessary values
                {
                    curLine = lyricLines[lineCount];
                    nextLine = lyricLines[lineCount + 1];
                    amountMove = 100 / (lineLengths[lineCount].TotalSeconds * 2);
                }
                else //Otherwise, the next line is empty
                {
                    curLine = lyricLines[totalLines - 1];
                    nextLine = String.Empty;
                    amountMove = 100 / (lineLengths[totalLines - 1].TotalSeconds * 2);
                }

                pbLineLength.Value = 0; //Start the progress bar over
            }
            tbPosition.Text = (curTime - TimeSpan.FromMilliseconds(curTime.Milliseconds)).ToString(); //Remove the milliseconds for display
        }

        private void moveLyricLine()
        {
            //Updates the lyric textbox with the current line in pink and the next line in gray
            tbLyrics.Clear();
            tbLyrics.SelectionFont = new Font("Tahoma", 18, FontStyle.Bold);
            tbLyrics.SelectionColor = System.Drawing.Color.DeepPink;
            tbLyrics.SelectionAlignment = HorizontalAlignment.Center;
            tbLyrics.AppendText(curLine);
            tbLyrics.SelectionColor = System.Drawing.Color.LightGray;
            tbLyrics.SelectionFont = new Font("Tahoma", 14, FontStyle.Bold);
            tbLyrics.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + nextLine);
            tbLyrics.DeselectAll();
        }

        private void findLineNumber()
        {
            //See "findLineNumber" in Timestamp.cs for explanation
            int theLine = 0;
            bool found = false;
            TimeSpan curTime = audioFile.CurrentTime;
            while (!found && theLine < totalLines)
            {
                if (theTimeStamps[theLine] != TimeSpan.Zero)
                {
                    if (theLine <= 0)
                    {
                        if (curTime < theTimeStamps[theLine + 1] && theTimeStamps[theLine + 1] != TimeSpan.Zero)
                        {
                            lineCount = theLine;
                            found = true;
                        }
                    }

                    else if (theLine == totalLines - 1)
                    {
                        if (curTime > theTimeStamps[theLine - 1] && theTimeStamps[theLine - 1] != TimeSpan.Zero)
                        {
                            lineCount = theLine;
                            found = true;
                        }
                    }
                    else
                    {
                        if ((curTime > theTimeStamps[theLine - 1] && curTime < theTimeStamps[theLine + 1])
                            && (theTimeStamps[theLine + 1] != TimeSpan.Zero && theTimeStamps[theLine - 1] != TimeSpan.Zero))
                        {
                            lineCount = theLine;
                            found = true;
                        }

                    }
                    theLine++;
                } //if !TimeSpan.Zero
                else
                {
                    lineCount = theLine - 1;
                    break;
                }
                
            }//While
                
        }

        private void updateLines()
        {
            //Sets the values of curLine and nextLine based on which line we are on
            curLine = lyricLines[lineCount];
            if (lineCount + 1 < totalLines)
            {
                nextLine = lyricLines[lineCount + 1];
            }
            else
            {
                nextLine = "";
            }
        }

        #endregion

    }
}
