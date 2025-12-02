using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTTH03_24520765_PhamNgocGiaKhang
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Chọn file media để phát";
            ofd.Filter = "Media Files|*.mp3;*.mp4;*.avi;*.mpeg;*.wav;*.midi|All Files|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                axWindowsMediaPlayer1.URL = ofd.FileName;
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = GetTimeText();
            timer1.Interval = 1000;
            timer1.Start();
        }

        private string GetTimeText()
        {
            DateTime now = DateTime.Now;
            return $"Hôm nay là ngày {now:dd/MM/yyyy} - Bây giờ là {now:hh:mm:ss tt}";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = GetTimeText();
        }
    }
}
