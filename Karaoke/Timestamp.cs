using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;

namespace Karaoke
{
    public partial class Timestamp : Form
    {
        private WaveOut outputDevice;
        private AudioFileReader audioFile = null;
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        private String audioFileName;
        private Timer songTimer;
        private bool firstPlay = true;
        private bool allLines = false;
        private TimeSpan[] theTimeStamps;
        private TimeSpan redoTimeSpan = TimeSpan.Zero;

        private string[] lyricLines;
        private int totalLines; //The total number of timestamps that need to be placed/lines of lyrics
        private bool isPaused;
        private int lineCount; //What line the user is currently on

        public Timestamp()
        {
            InitializeComponent();
        }

        public Timestamp(string fName) //If the user is editing an existing
        {
            InitializeComponent();
            btnPlay.Enabled = false;
            btnPause.Enabled = false;
            btnStop.Enabled = false;
            btnUndo.Enabled = false;
            btnRedo.Enabled = false;
            trackbarSong.Enabled = false;
            btnBackFifteen.Enabled = false;
            btnForwardFifteen.Enabled = false;
            btnAudioFile.Enabled = false;

            editExisting(fName);
            songTimer = new Timer();
            songTimer.Tick += SongTimer_Tick;

            isPaused = true;
            this.FormClosing += btnStop_Click;
        }

        public Timestamp(string[] lines) //If the user is coming from the "lyric" section
        {
            InitializeComponent();
            btnPlay.Enabled = false;
            btnPause.Enabled = false;
            btnStop.Enabled = false;
            btnUndo.Enabled = false;
            btnRedo.Enabled = false;
            trackbarSong.Enabled = false;
            btnBackFifteen.Enabled = false;
            btnForwardFifteen.Enabled = false;

            setupLyrics(lines);

            songTimer = new Timer();
            songTimer.Tick += SongTimer_Tick;

            isPaused = true;
            this.FormClosing += btnStop_Click;

        }

        #region BTN clicks
        private void btnPlay_Click(object sender, EventArgs e)
        {
            isPaused = false;
            labelLyrics.Focus();
            songTimer.Start();
            btnBackFifteen.Enabled = true;
            btnForwardFifteen.Enabled = true;
            btnStop.Enabled = true;


            if (firstPlay) //Only need to initialize output device and AudioFileReader once when a file has been chosed
            {
                if (outputDevice == null)
                {
                    outputDevice = new WaveOut();
                    outputDevice.PlaybackStopped += OnPlaybackStopped;
                }
                if (audioFile == null && openFileDialog.FileName != null)
                {
                    audioFile = new AudioFileReader(audioFileName);

                    outputDevice.Init(audioFile);
                }

                setupTrackbar(); //Set the maximum value and starting value
                btnAudioFile.Enabled = false; //Don't allow the user to choose a new audio file unless they start over
                firstPlay = false;
            }
            outputDevice.Play();
            btnPause.Enabled = true;
            btnPlay.Enabled = false;
            btnAudioFile.Enabled = false;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            outputDevice.Pause();
            isPaused = true;
            btnPlay.Enabled = true;
            btnPause.Enabled = false;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            tbPosition.Text = new TimeSpan(0, 0, 0).ToString();
            openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Audio File";
            openFileDialog.Filter = "WAV Files (*.wav)|*.wav|M4A Files (*.m4a)|*.m4a|MP3 files (*.mp3)|*.mp3";
            openFileDialog.Title = "Audio File";
            DialogResult fileResult = openFileDialog.ShowDialog();

            if (openFileDialog.FileName.EndsWith(".mp3")) //MP3 Files need to be converted since the AudioFileReader does not support MP3s
            {
                var filePathWav = openFileDialog.FileName.Remove(openFileDialog.FileName.Length - 4, 4);
                var fileNameWav = openFileDialog.SafeFileName.Remove(openFileDialog.SafeFileName.Length - 4, 4);
                if (File.Exists(filePathWav + ".wav")) //See if there is already a wav file of the same name as the MP3 before converting
                {
                    audioFileName = filePathWav + ".wav";
                    MessageBox.Show("using \"" + fileNameWav + ".wav\" as audio file", "Wav File Name");
                    btnPlay.Enabled = true;
                    btnStop.Enabled = true;
                }
                else //Otherwise prompt the user to convert the file if they want to use that song
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
                            //Use a reader that accepts MP3s to play the song and write it to a WAV file
                            //MediaFoundationReader does not support repositioning so it can't/shouldn't be used as the main audio reader
                            using (MediaFoundationReader mr = new MediaFoundationReader(openFileDialog.FileName)) 
                            {
                                WaveFileWriter.CreateWaveFile(audioFileName, mr);
                            }
                        }
                        btnPlay.Enabled = true;
                        btnStop.Enabled = true;
                    }

                }

            }
            else if (fileResult == DialogResult.OK)
            {
                MessageBox.Show(openFileDialog.SafeFileName, "File Name"); //Show the user what file is being used
                audioFileName = openFileDialog.FileName; //Store the file name/path to be used later
                btnPlay.Enabled = true;
                btnStop.Enabled = true;

            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            btnPause.PerformClick(); //To prevent the audio from continuing to play
            songTimer.Stop();

            lineCount--; //To clear out the array
            if (lineCount < 0) //To be able to undo the final timestamp after the song has started over
            {
                lineCount = totalLines - 1;
            }

            redoTimeSpan = theTimeStamps[lineCount]; //In case we want to redo that timestamp
            theTimeStamps[lineCount] = TimeSpan.Zero;
            if (lineCount > 0)
            {
                audioFile.CurrentTime = theTimeStamps[lineCount - 1];
            }
            else
            {
                audioFile.Position = 0;
            }
            tbTimestamps.Clear();
            updateTimeTB(); //The remove the previous timestamp from the textbox
            lineCount--; //To move the colored line correctly
            TextColor(tbLyrics);
            TextColor(tbTimestamps);
            lineCount++; //To get back to the correct spot for storing/placing timestmaps
            tbPosition.Text = audioFile.CurrentTime.ToString();

            AdjustTrackbar(); //Based on updated time

            btnRedo.Enabled = true;
            btnUndo.Enabled = false;
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            btnPause.PerformClick(); //To prevent the audio from continuing to play
            songTimer.Stop();
            btnUndo.Enabled = true;
            btnRedo.Enabled = false;

            theTimeStamps[lineCount] = redoTimeSpan; //Store the recently removed timestamp back in its spot

            audioFile.CurrentTime = theTimeStamps[lineCount]; //Adjust where in the audio we are
            updateTimeTB(); //Rewrite the timestamp in the textbox
            TextColor(tbLyrics); //Move the colored lines
            TextColor(tbTimestamps);

            tbPosition.Text = audioFile.CurrentTime.ToString();

            AdjustTrackbar();

            lineCount++; //So that we have moved on
        }

        private void btnBackFifteen_Click(object sender, EventArgs e)
        {
            if (audioFile.CurrentTime.TotalSeconds < 15) //If the song isn't 15 seconds in, just go back to the beginning
            {
                audioFile.Position = 0;
            }
            else //Otherwise just jump back 15 seconds
            {
                audioFile.CurrentTime = audioFile.CurrentTime - TimeSpan.FromSeconds(15);
            }

            //Just removes the millisecond part of the current time to make it more aesthetically pleasing
            tbPosition.Text = (audioFile.CurrentTime - TimeSpan.FromMilliseconds(audioFile.CurrentTime.Milliseconds) - TimeSpan.FromHours(audioFile.CurrentTime.Hours)).ToString();

            AdjustTrackbar();

            findLineNumber(); //Figure out where the lyrics now need to be
            labelLyrics.Focus(); //To remove the focus off of a button so that when 'enter' is pressed it doesn't trigger the button
        }

        private void btnForwardFifteen_Click(object sender, EventArgs e)
        {
            if (audioFile.CurrentTime.TotalSeconds + 15 >= audioFile.TotalTime.TotalSeconds) //If there aren't 15 seconds left, just jump to the end
            {
                audioFile.CurrentTime = audioFile.TotalTime;
            }
            else //otherwise jump forward 15 seconds
            {
                audioFile.CurrentTime = audioFile.CurrentTime + TimeSpan.FromSeconds(15);
            }

            //Remove the millisecond part from the current time
            tbPosition.Text = (audioFile.CurrentTime - TimeSpan.FromMilliseconds(audioFile.CurrentTime.Milliseconds)).ToString();

            AdjustTrackbar();

            findLineNumber(); //Figure out where the lyrics need to be
            labelLyrics.Focus(); //Remove focus on button so that pressing 'enter' doesn't trigger button click
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            btnPause.PerformClick();
            if (allLines) //If the user has placed all of the necessary timestamps
            {
                string file = "";
                StringBuilder sb = new StringBuilder();
                saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "LRC Files (*.lrc)|*.lrc";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    file = saveFileDialog.FileName;
                    for (int i = 0; i < totalLines; i++) //Write timestamps followed by lyrics to file in "[timestamp] lyrics" format
                    {
                        sb.Append("[" + theTimeStamps[i].ToString() + "] " + lyricLines[i] + Environment.NewLine);
                    }

                    File.WriteAllText(file, sb.ToString());
                    MessageBox.Show("File Successfully Exported"); //Let the user know that it worked
                }
                
            }
            else
            {
                //A user must place all timestamps before they can export 
                MessageBox.Show($"You have not finished timestamping for this song, please finish all {totalLines} lines before exporting");
            }
        }

        private void btnSaveandContinue_Click(object sender, EventArgs e)
        {
            if(audioFile == null) //In case the user came from the "edit" button rather than lyric section
            {
                MessageBox.Show("Please choose an audio file before continuing", "Invalid");
            }
            else if (allLines) //Make sure all timestamps have been placed before continuing
            {
                Sing s = new Sing(audioFileName, lyricLines, theTimeStamps);
                Hide();
                s.ShowDialog();
            }
            else
            {
                //Notify if the user hasn't placed all timestamps
                MessageBox.Show("You must place all TimeStamps before continuing", "Invalid");
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            //Displays the instructions for placing timestamps
            String instructions = "Choose your audio file and press play to begin" +
                "\n\nPlace timestamps at the beginning of each line of lyrics by pressing the \"Enter\" key" +
                "\n\nPress the \"Spacebar\" to play or pause the music" +
                "\n\nPress the undo button or \"CTRL + Z\" to remove the most recently placed timestamp" +
                "\n\nPress the redo button or \"CTRL + Y\" to add an undone timestamp back" +
                "\n\nPress the \"Export\" button once you've placed all timestamps to save your LRC file" +
                "\n\nPress the \"Save and Continue\" button to sing along to your karaoke lyrics";
            MessageBox.Show(instructions, "How To");
        }
     
        private void btnBack_Click(object sender, EventArgs e)
        {
            //Goes back to the main screen
            Main m = new Main();
            Hide();
            m.ShowDialog();
        }

        #endregion //btn clicks

        #region Trackbar Controls

        private void trackbarSong_Scroll(object sender, EventArgs e)
        {
            int ms = trackbarSong.Value; //Get where the user has scrolled to
            audioFile.CurrentTime = TimeSpan.FromSeconds(ms); //Update the position of the song to where they scrolled to

        }
    
        private void trackbarSong_MouseUp(object sender, MouseEventArgs e)
        {
            //Once the user has finished scrolling

            findLineNumber(); //Determine where in the lyrics we need to be
            TextColor(tbLyrics); //Update the color of both textboxes based on where we are in the song
            TextColor(tbTimestamps);
            if (songTimer != null) //Resume the timer
            {
                songTimer.Start();
            }
        }

        private void trackbarSong_MouseDown(object sender, MouseEventArgs e)
        {
            btnPause.PerformClick(); //Pause the music
            if (songTimer != null) //Pause the timer
            {
                songTimer.Stop();
            }
        }

        public void setupTrackbar()
        {
            trackbarSong.Enabled = true;
            trackbarSong.Minimum = 0;
            trackbarSong.Maximum = (int)audioFile.TotalTime.TotalSeconds; //How many seconds the song is
            trackbarSong.Value = 0; //Start at the beginning
            trackbarSong.TickFrequency = 15;
        }
      
        public void AdjustTrackbar() 
        {
            //Adjusts where the tracker is on the trackbar is after reposistioning of the audio has occurred

            int ms = (int)audioFile.CurrentTime.TotalSeconds; //Get where we currently are in the song

            if (ms > trackbarSong.Maximum) //If it gets above the maximum somehow, just set the trackbar value to the maximum
            {

                trackbarSong.Value = trackbarSong.Maximum;
            }
            else
            {
                trackbarSong.Value = ms; //Otherwise set to where we currently are
            }
        }


        #endregion

        #region Misc Functions

        private void SongTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan curTime = audioFile.CurrentTime;
            int ms = (int)curTime.TotalSeconds; //Get where we currently are in the song

            if (ms > trackbarSong.Maximum) //If greater than the maximum value of the trackbar, set the value of trackbar to maximum
            {

                trackbarSong.Value = trackbarSong.Maximum;
            }
            else //Otherwise set the value of the trackbar to the current position in the song
            {
                trackbarSong.Value = ms;
            }

            if (tbTimestamps.Lines.Length >= totalLines) //Has the user placed all of the neccessary timestamps
            {
                allLines = true;
            }
            else
            {
                allLines = false;
            }

            //If the current position of the song has gone past an already placed timestamp
            if (lineCount < totalLines && (theTimeStamps[lineCount] <= curTime && theTimeStamps[lineCount] != TimeSpan.Zero))
            {
                TextColor(tbTimestamps); //Update the colored line of both textboxes
                TextColor(tbLyrics);
                lineCount++; //Move to next line
            }

            tbPosition.Text = (curTime - TimeSpan.FromMilliseconds(curTime.Milliseconds)).ToString(); //Remove milliseconds for display


            if (tbTimestamps.Lines.Length > 1) //If there is at least one timestamp to undo
            {
                btnUndo.Enabled = true;
            }
            else
            {
                btnUndo.Enabled = false;
            }

            if (redoTimeSpan != TimeSpan.Zero) //If the user has undone a timestamp, they can then redo
            {
                btnRedo.Enabled = true;
            }
            else
            {
                btnRedo.Enabled = false;
            }
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs args)
        {
            if (audioFile != null)
            {
                audioFile.Position = 0; //Start the audio file over
            }
            //Enable and disable buttons accordingly
            btnPlay.Enabled = true;
            btnPause.Enabled = false;
            btnStop.Enabled = false;
            btnBackFifteen.Enabled = false;
            btnForwardFifteen.Enabled = false;
            btnAudioFile.Enabled = true;

            TextColor(tbLyrics); //Change the color of lines as necessary
            TextColor(tbTimestamps);
            if (tbTimestamps.Lines.Length >= totalLines) //Has the user placed all of the neccessary timestamps
            {
                allLines = true;
                lineCount = 0; //Start back at beginning to go through song again if desired
            }
        }

        private void Timestamp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) //Play or pause the music
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

            else if (e.KeyCode == Keys.Enter && !isPaused) //Place a timestamp as long as music is playing
            {
                if (redoTimeSpan != TimeSpan.Zero)
                {
                    redoTimeSpan = TimeSpan.Zero; //Clear out redo if necessary as soon as new timestamp is placed
                }

                if (lineCount < totalLines) //If the user hasn't placed all timestamps already
                {
                    if (lineCount < 0)
                    {
                        lineCount = 0;
                    }

                    theTimeStamps[lineCount] = audioFile.CurrentTime; //Store the current position of audio file

                    TextColor(tbLyrics); //Update the colored line
                    updateTimeTB(); //Update timestamp textbox to show new timestamp
                    TextColor(tbTimestamps); //Change the color

                    lineCount++; //Move to next line for next time

                }
                else //If the user has placed all timestamps, let them know they can't place anymore
                {
                    btnPause.PerformClick();
                    MessageBox.Show($"You have already placed all {totalLines} timestamps");
                }
            }

            else if (e.Control && e.KeyCode == Keys.Z) //Undo keyboard shortcut
            {

                btnUndo.PerformClick();
            }

            else if (e.Control && e.KeyCode == Keys.Y) //Redo keyboard shortcut
            {
                btnRedo.PerformClick();
            }
        }

        private void findLineNumber()
        {
            int theLine = 0;
            bool found = false;
            TimeSpan curTime = audioFile.CurrentTime;
            while (!found && theLine < totalLines)
            {
                if (theTimeStamps[theLine] != TimeSpan.Zero) //Make sure that the current line has actually been placed
                {
                    if (theLine <= 0) //So that we don't look at a spot in the array that doesn't exist
                    {
                        if (curTime < theTimeStamps[theLine + 1] && theTimeStamps[theLine + 1] != TimeSpan.Zero)
                        {
                            lineCount = theLine;
                            found = true;
                        }
                    }

                    else if (theLine == totalLines - 1) //So that we don't look at a spot in the array that doesn't exist
                    {
                        if (curTime > theTimeStamps[theLine - 1] && theTimeStamps[theLine - 1] != TimeSpan.Zero)
                        {
                            lineCount = theLine;
                            found = true;
                        }
                    }
                    else
                    {
                        //See if the current time belongs in the current "line" (i.e. between the previous and next lines)
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
                    lineCount = theLine - 1; //Set the linecount to the last line
                    break;
                }

            }//While
        }

        private void setupLyrics(string[] lyrics)
        {
            btnHelp.PerformClick();
            tbLyrics.Clear();
            lyricLines = new string[lyrics.Length]; //Create an array to hold the lyrics
            lineCount = 0;
            StringBuilder sb = new StringBuilder();
            foreach(string line in lyrics)
            {
                sb.Append(lineCount + 1 + ": " + line + Environment.NewLine); //Show the lyrics with the corresponding line number in the textbox
                tbLyrics.AppendText(sb.ToString());
                sb.Clear();
                lyricLines[lineCount] = line;
                lineCount++;
            }
            tbLyrics.ReadOnly = true; //Don't let the user change anything in the textbox
            totalLines = lyricLines.Length;
            theTimeStamps = new TimeSpan[totalLines];
            for(int i = 0; i < totalLines; i++) //Initialize all of the timestamps as TimeSpan.Zero
            {
                theTimeStamps[i] = TimeSpan.Zero;
            }
            allLines = false;
            lineCount = 0; //Start at the very beginning
        }

        private void editExisting(string fileName)
        {
            btnHelp.PerformClick();
            var theLines = File.ReadAllLines(fileName); //Read in everything from the file
            totalLines = theLines.Length;
            theTimeStamps = new TimeSpan[totalLines];
            lyricLines = new string[totalLines];
            StringBuilder sb = new StringBuilder();
            lineCount = 0;
            foreach (string line in theLines)
            {
                int j = 0;
                StringBuilder time = new StringBuilder();
                
                String timeStamp = String.Concat(line.Where(c => Char.IsDigit(c) || Char.Equals(c, '.') || Char.Equals(c, ':'))); //Remove the timestamp part from the line

                var pieces = line.Split(' '); //Split the line into pieces so that we can skip over timestamp

                var numbers = timeStamp.Split(':'); //Split the timestamp into pieces
                var split = numbers[numbers.Length - 1].Split('.'); //Split the last piece of the timestamp into seconds and milliseconds
                if (numbers.Length + 1 == 4) //If the LRC file was created using this program, the TimeSpan element in the file is different and contains the 'hour' value as well as minutes, seconds, and milliseconds
                {
                    theTimeStamps[lineCount] = new TimeSpan(Int32.Parse(numbers[0]), Int32.Parse(numbers[1]), Int32.Parse(split[0])); //Create the timestamp
                    for (int i = 1; i < pieces.Length; i++) //Skip over the timestamp but reconnect all other pieces of the line
                    {
                        sb.Append(pieces[i] + " ");
                    }
                }
                else //LRC files downloaded from the internet just have minute, second, and millisecond
                {
                    //Everything else is same as above
                    theTimeStamps[lineCount] = new TimeSpan(0, Int32.Parse(numbers[0]), Int32.Parse(split[0]));
                    pieces[0] = String.Concat(pieces[0].Where(c => Char.IsLetter(c) || (Char.IsPunctuation(c) && !Char.Equals(c, ':') && !Char.Equals(c, '.') && !Char.Equals(c, '[') && !Char.Equals(c, ']'))));
                    for (int i = 0; i < pieces.Length; i++)
                    {
                        sb.Append(pieces[i] + " ");
                    }

                }

                lyricLines[lineCount] = sb.ToString(); //Store the line
                sb.Clear(); //Clear the stringbuilder for the next line
                lineCount++;
            } //Foreach

            lineCount = 0;
            //Setup the textboxes as necessary
            for (int i = 0; i < totalLines; i++)
            {
                tbLyrics.AppendText(i + 1 + ": " + lyricLines[i] + Environment.NewLine);
            }
            for (int i = 0; i < totalLines; i++)
            {
                tbTimestamps.AppendText(i + 1 + ": [" + theTimeStamps[i] + "]" + Environment.NewLine);
            }
            //Color the first lines
            TextColor(tbLyrics);
            TextColor(tbTimestamps);
            allLines = true; 
            btnAudioFile.Enabled = true;          
        }

        private void updateTimeTB()
        {
            tbTimestamps.Clear(); //Clear it all out

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < totalLines; i++) //Go through all lines
            {
                if (theTimeStamps[i] != TimeSpan.Zero) //Only write lines that have a value not equal to TimeSpan.Zero
                {
                    sb.Append(i + 1 + ": [" + theTimeStamps[i] + "]" + Environment.NewLine);
                }
            }
            tbTimestamps.Text = sb.ToString();
        }

        private void TextColor(RichTextBox tb)
        {
            string[] lines = tb.Lines; //Get the text that is being updated
            int vertPos = ScrollAPIs.GetScrollPosition(tb.Handle, ScrollAPIs.ScrollbarDirection.Vertical); //Maintain the scrollbar's position
            tb.Clear();
            tb.SuspendLayout();
            for (int i = 0; i < lines.Length; i++) //Go through all of the lines
            {
                if (i == lineCount) //If we're at the one that needs to be colored, then use DeepPink
                {
                    tb.SelectionColor = System.Drawing.Color.DeepPink;

                }
                else //Otherwise use Black
                {
                    tb.SelectionColor = System.Drawing.Color.Black;
                }

                tb.AppendText(lines[i]);
                if (i != lines.Length - 1) //Don't add a NewLine after the last line
                {
                    tb.AppendText(Environment.NewLine);
                }

            }

            if (tb == tbTimestamps && !allLines) //If not all lines have been placed, scroll to the bottom of the timestamp textbox
            {
                tb.ScrollToCaret();
            }
            else //Otherwise just scroll to where we were before
            {
                tb.SelectionStart = 0;
                tb.SelectionLength = 0;
                ScrollAPIs.SetScrollPosition(tb.Handle, ScrollAPIs.ScrollbarDirection.Vertical, vertPos);
            }
            tb.ResumeLayout();
            labelLyrics.Focus();
        }

        private void Reset()
        {
            if (audioFile != null)
            { audioFile.Position = 0; } //Start at the beginning

            outputDevice?.Stop();
            songTimer?.Stop();


            trackbarSong.Value = 0; //Reset the trackbar
            tbPosition.Text = new TimeSpan(0, 0, 0).ToString();
            tbTimestamps.Text = "";
            audioFile = null; //To reset and reload the audioplayer

            for (int i = 0; i < theTimeStamps.Length; i++) //Remove all timestamps
            {
                theTimeStamps[i] = TimeSpan.Zero;
            }

            lineCount = 0;

            firstPlay = true;
            isPaused = true;

            btnPause.Enabled = false;
            btnPlay.Enabled = true;
            btnAudioFile.Enabled = true;
        }

        private void Timestamp_Load(object sender, EventArgs e)
        {
            btnHelp.PerformClick(); //Show instructions on load
        }

        #endregion

        #region Scroll APIs From https://stackoverflow.com/questions/21894017/textbox-maintain-scroll-bar-position-during-text-updates

        //Allows me to maintain the position of the scrollbar as I update the textbox
        public static class ScrollAPIs 
        {
            [DllImport("user32.dll")]
            internal static extern int GetScrollPos(IntPtr hWnd, int nBar);

            [DllImport("user32.dll")]
            internal static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);

            [DllImport("user32.dll")]
            internal static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

            public enum ScrollbarDirection
            {
                Horizontal = 0,
                Vertical = 1,
            }

            private enum Messages
            {
                WM_HSCROLL = 0x0114,
                WM_VSCROLL = 0x0115
            }

            public static int GetScrollPosition(IntPtr hWnd, ScrollbarDirection direction)
            {
                return GetScrollPos(hWnd, (int)direction);
            }

            public static void GetScrollPosition(IntPtr hWnd, out int horizontalPosition, out int verticalPosition)
            {
                horizontalPosition = GetScrollPos(hWnd, (int)ScrollbarDirection.Horizontal);
                verticalPosition = GetScrollPos(hWnd, (int)ScrollbarDirection.Vertical);
            }

            public static void SetScrollPosition(IntPtr hwnd, int hozizontalPosition, int verticalPosition)
            {
                SetScrollPosition(hwnd, ScrollbarDirection.Horizontal, hozizontalPosition);
                SetScrollPosition(hwnd, ScrollbarDirection.Vertical, verticalPosition);
            }

            public static void SetScrollPosition(IntPtr hwnd, ScrollbarDirection direction, int position)
            {
                //move the scroll bar
                SetScrollPos(hwnd, (int)direction, position, true);

                //convert the position to the windows message equivalent
                IntPtr msgPosition = new IntPtr((position << 16) + 4);
                Messages msg = (direction == ScrollbarDirection.Horizontal) ? Messages.WM_HSCROLL : Messages.WM_VSCROLL;
                SendMessage(hwnd, (int)msg, msgPosition, IntPtr.Zero);
            }
        } //End of ScrollAPIs from StackOverflow

        #endregion

    }
}
