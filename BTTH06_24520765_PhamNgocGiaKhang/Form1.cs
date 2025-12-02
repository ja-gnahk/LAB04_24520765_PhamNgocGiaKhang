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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BTTH06_24520765_PhamNgocGiaKhang
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Chọn thư mục chứa file cần chép";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fbd.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Chọn thư mục lưu file";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = fbd.SelectedPath;
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            string sourceDir = textBox1.Text.Trim();
            string destDir = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(sourceDir) || string.IsNullOrEmpty(destDir))
            {
                MessageBox.Show("Vui lòng chọn đầy đủ đường dẫn nguồn và đích!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Directory.Exists(sourceDir))
            {
                MessageBox.Show("Thư mục nguồn không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Directory.Exists(destDir))
            {
                Directory.CreateDirectory(destDir);
            }

            string[] files = Directory.GetFiles(sourceDir);
            int totalFiles = files.Length;

            if (totalFiles == 0)
            {
                MessageBox.Show("Thư mục nguồn trống!", "Thông báo");
                return;
            }

            progressBar1.Minimum = 0;
            progressBar1.Maximum = totalFiles;
            progressBar1.Value = 0;

            button3.Enabled = false;

            try
            {
                for (int i = 0; i < totalFiles; i++)
                {
                    string sourceFile = files[i];
                    string fileName = Path.GetFileName(sourceFile);
                    string destFile = Path.Combine(destDir, fileName);

                    toolStripStatusLabel2.Text = $"Đang sao chép: {fileName}";

                    toolTip1.SetToolTip(progressBar1, sourceFile);

                    await Task.Run(() => File.Copy(sourceFile, destFile, true));

                    progressBar1.Value = i + 1;

                }

                MessageBox.Show("Sao chép hoàn tất!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                toolStripStatusLabel2.Text = "Đang chờ...";
                toolTip1.SetToolTip(progressBar1, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                button3.Enabled = true;
                progressBar1.Value = 0;
            }
        }
    }
}
