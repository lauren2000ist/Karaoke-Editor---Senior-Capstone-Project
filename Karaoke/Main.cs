using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karaoke
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            tbWelcome.Clear();
            tbWelcome.SelectionAlignment = HorizontalAlignment.Center;
            tbWelcome.AppendText("Sing With Mi");
            tbWelcome.ReadOnly = true;
            labelName.Focus();
            
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Lyrics lyricForm = new Lyrics();
            Hide();
            lyricForm.ShowDialog();
            
        }

        private void btnSing_Click(object sender, EventArgs e)
        {
            Sing karaokeForm = new Sing();
            Hide();
            karaokeForm.ShowDialog();

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OpenFileDialog lrcFileDialog = new OpenFileDialog();
            lrcFileDialog.Filter = "LRC Files (*.lrc)|*.lrc";
            lrcFileDialog.Title = "LRC File";
            if (lrcFileDialog.ShowDialog() == DialogResult.OK)
            {
                Timestamp timestampForm = new Timestamp(lrcFileDialog.FileName);
                Hide();
                timestampForm.ShowDialog();
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            String instructions = "Create: Allows you to choose your lyrics and then place the timestamps for each line of lyrics" +
                "\n\nEdit: Allows you to open an existing LRC file and change any timestamps" +
                "\n\nSing: Allows you to open an existing LRC file and audio file to sing along to";
            MessageBox.Show(instructions, "About");
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Close();
        }
    }
}
