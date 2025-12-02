using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTTH05_24520765_PhamNgocGiaKhang
{
    public partial class AddSinhVien : Form
    {
        public SinhVien NewStudent { get; set; }

        public AddSinhVien()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMSSV.Text) || string.IsNullOrEmpty(txtTen.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                    return;
                }

                float score;
                if (!float.TryParse(txtDiemTB.Text, out score))
                {
                    MessageBox.Show("Điểm trung bình phải là số!");
                    return;
                }

                if (score < 0 || score > 10)
                {
                    MessageBox.Show("Điểm trung bình phải nằm trong khoảng từ 0.0 đến 10.0!");
                    return;
                }

                NewStudent = new SinhVien()
                {
                    MSSV = txtMSSV.Text,
                    Ten = txtTen.Text,
                    Khoa = comboBox1.Text,
                    DiemTB = score
                };

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Close_click
            (object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
