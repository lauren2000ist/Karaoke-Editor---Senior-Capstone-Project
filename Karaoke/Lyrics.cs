using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karaoke
{

    public partial class Lyrics : Form
    {
        private OpenFileDialog openFileDialog;
        private String fileName;
        private string[] lyricLines;
        private bool loadFile = false;

        public Lyrics()
        {
            InitializeComponent();
        }

        #region Other Functions

        private void showLyrics()
        {
            lyricLines = File.ReadAllLines(fileName);
            tbLyrics.Clear();
            foreach (string line in lyricLines)
            {
                tbLyrics.AppendText(line + Environment.NewLine);
            }
            tbNumLines.Text = lyricLines.Length.ToString();
            loadFile = false;
        }

        private void tbLyrics_TextChanged(object sender, EventArgs e)
        {
            if (!loadFile) //Makes sure that the file has been read in before counting the number of lines and storing contents
            {
                lyricLines = tbLyrics.Lines;
                tbNumLines.Text = lyricLines.Length.ToString();
            }
        }


        #endregion //other functions

        #region btn clicks
        private void btnChooseLyrics_Click(object sender, EventArgs e)
        {
            loadFile = true;
            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(openFileDialog.SafeFileName, "File Name"); //For testing purposes
                fileName = openFileDialog.FileName;
                showLyrics();
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            tbLyrics.Undo();
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            tbLyrics.Redo();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
            var lyrics = tbLyrics.Lines;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllLines(saveFileDialog.FileName, lyrics);
                MessageBox.Show("File Successfully Exported");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbLyrics.Lines.Length > 0)
            {
                Timestamp t = new Timestamp(tbLyrics.Lines);
                Hide();
                t.ShowDialog();
            }
            else
            {
                MessageBox.Show("You must load lyrics before you can continue", "Invalid");
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            String instructions = "Open an existing Text File or paste lyrics into the text box and edit as needed" +
                "\n\nPress the \"Export\" button to save your lyrics to a text file" +
                "\n\nPress the \"Save and Continue\" button to continue on to the timestamping section";
            MessageBox.Show(instructions, "How To");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Main m = new Main();
            Hide();
            m.ShowDialog();
        }

        #endregion //btn clicks

    }
}
