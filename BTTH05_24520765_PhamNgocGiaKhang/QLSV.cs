using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BTTH05_24520765_PhamNgocGiaKhang
{
    public partial class QLSV : Form
    {
        private List<SinhVien> listStudents = new List<SinhVien>();

        public QLSV()
        {
            InitializeComponent();
            dgvSinhVien.AllowUserToAddRows = true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void BindDataGridView(List<SinhVien> list)
        {
            dgvSinhVien.AutoGenerateColumns = false;

            dgvSinhVien.DataSource = null;
            dgvSinhVien.DataSource = list;


            if (dgvSinhVien.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvSinhVien.Rows)
                {
                    row.Cells[1].Value = (row.Index + 1).ToString();
                }
            }
        }

        private void ThemSinhVien()
        {
            AddSinhVien SV = new AddSinhVien();

            if (SV.ShowDialog() == DialogResult.OK)
            {
                SinhVien newSV = SV.NewStudent;

                bool daTonTai = listStudents.Any(s => s.MSSV == newSV.MSSV);

                if (daTonTai)
                    MessageBox.Show("MSSV đã tồn tại, vui lòng nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    listStudents.Add(newSV);
                    BindDataGridView(listStudents);
                }
            }
        }

        private void ThemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThemSinhVien();
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            string keyword = toolStripTextBox1.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(keyword))
            {
                BindDataGridView(listStudents);
            }
            else
            {
                var searchResult = listStudents
                                       .Where(s => s.Ten.ToLower().Contains(keyword))
                                       .ToList();

                BindDataGridView(searchResult);
            }
        }

        private void ThoatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}