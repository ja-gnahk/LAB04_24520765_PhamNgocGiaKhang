using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BTTH04_24520765_PhamNgocGiaKhang
{
    public partial class Bai4 : Form
    {
        public Bai4()
        {
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            if (fontSizeComboBox != null)
            {
                for (int i = 8; i <= 70; i += (i < 20 ? 2 : 4))
                {
                    fontSizeComboBox.Items.Add(i);
                }
                if (mainRichTextBox != null)
                {
                    fontSizeComboBox.Text = mainRichTextBox.Font.Size.ToString();
                }
            }

            if (fontFamilyComboBox != null)
            {
                foreach (FontFamily font in FontFamily.Families)
                {
                    fontFamilyComboBox.Items.Add(font.Name);
                }
                if (mainRichTextBox != null)
                {
                    fontFamilyComboBox.Text = mainRichTextBox.Font.FontFamily.Name;
                }
            }
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mainRichTextBox.Text.Length > 0)
            {
                DialogResult result = MessageBox.Show(
                    "Bạn có muốn lưu thay đổi hiện tại không?",
                    "Tạo mới tệp",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(sender, e);

                    if (result != DialogResult.Cancel)
                    {
                        mainRichTextBox.Clear();
                    }
                }
                else if (result == DialogResult.No)
                {
                    mainRichTextBox.Clear();
                }
            }
            else
            {
                mainRichTextBox.Clear();
            }
        }
        //Lưu tệp 
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            // Rich Text Format giữ lại định dạng B I U
            saveDlg.Filter = "Rich Text Format (*.rtf)|*.rtf|Text Files (*.txt)|*.txt";

            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (saveDlg.FileName.EndsWith(".rtf", StringComparison.OrdinalIgnoreCase))
                    {
                        mainRichTextBox.SaveFile(saveDlg.FileName, RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        mainRichTextBox.SaveFile(saveDlg.FileName, RichTextBoxStreamType.PlainText);
                    }
                    MessageBox.Show("Tệp đã được lưu thành công.", "Lưu Tệp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể lưu tệp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void fontDialogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDlg = new FontDialog();
            ColorDialog colorDlg = new ColorDialog();

            if (mainRichTextBox.SelectionLength > 0)
            {
                fontDlg.Font = mainRichTextBox.SelectionFont;
                fontDlg.Color = mainRichTextBox.SelectionColor;
            }
            else
            {
                fontDlg.Font = mainRichTextBox.Font;
                fontDlg.Color = mainRichTextBox.ForeColor;
            }

            fontDlg.ShowColor = true;

            if (fontDlg.ShowDialog() == DialogResult.OK)
            {
                if (mainRichTextBox.SelectionLength > 0)
                {
                    mainRichTextBox.SelectionFont = fontDlg.Font;
                }
                else
                {
                    mainRichTextBox.Font = fontDlg.Font;
                }

                if (mainRichTextBox.SelectionLength > 0)
                {
                    mainRichTextBox.SelectionColor = fontDlg.Color;
                }
                else
                {
                    mainRichTextBox.ForeColor = fontDlg.Color;
                }

                if (fontFamilyComboBox != null)
                {
                    fontFamilyComboBox.Text = fontDlg.Font.FontFamily.Name;
                }
                if (fontSizeComboBox != null)
                {
                    fontSizeComboBox.Text = fontDlg.Font.Size.ToString();
                }
            }
        }
        private void fontFamilyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolStripComboBox comboBox = sender as ToolStripComboBox;
            if (comboBox != null && mainRichTextBox.SelectionLength > 0)
            {
                string newFontFamilyName = comboBox.SelectedItem.ToString();
                Font currentFont = mainRichTextBox.SelectionFont;
                mainRichTextBox.SelectionFont = new Font(newFontFamilyName, currentFont.Size, currentFont.Style);
            }
            else if (comboBox != null)
            {
                string newFontFamilyName = comboBox.SelectedItem.ToString();
                Font currentFont = mainRichTextBox.Font;
                mainRichTextBox.Font = new Font(newFontFamilyName, currentFont.Size, currentFont.Style);
            }
        }

        private void fontSizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolStripComboBox comboBox = sender as ToolStripComboBox;
            if (comboBox != null && float.TryParse(comboBox.SelectedItem.ToString(), out float newSize) && newSize > 0)
            {
                ApplyFontSize(newSize);
            }
        }

        private void fontSizeComboBox_Leave(object sender, EventArgs e)
        {
            ToolStripComboBox comboBox = sender as ToolStripComboBox;
            if (comboBox != null && float.TryParse(comboBox.Text, out float newSize) && newSize > 0)
            {
                ApplyFontSize(newSize);
            }
        }

        private void ApplyFontSize(float newSize)
        {
            if (mainRichTextBox.SelectionLength > 0)
            {
                Font currentFont = mainRichTextBox.SelectionFont;
                mainRichTextBox.SelectionFont = new Font(currentFont.FontFamily, newSize, currentFont.Style);
            }
            else
            {
                Font currentFont = mainRichTextBox.Font;
                mainRichTextBox.Font = new Font(currentFont.FontFamily, newSize, currentFont.Style);
            }
        }

        private void boldButton_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Bold);
        }

        private void italicButton_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Italic);
        }
        private void underlineButton_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Underline);
        }

        private void ToggleFontStyle(FontStyle style)
        {
            if (mainRichTextBox.SelectionLength > 0)
            {
                Font currentFont = mainRichTextBox.SelectionFont;
                FontStyle newStyle = currentFont.Style ^ style;

                mainRichTextBox.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newStyle);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "Rich Text Format (*.rtf)|*.rtf|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (openDlg.FileName.EndsWith(".rtf", StringComparison.OrdinalIgnoreCase))
                    {
                        mainRichTextBox.LoadFile(openDlg.FileName, RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        mainRichTextBox.LoadFile(openDlg.FileName, RichTextBoxStreamType.PlainText);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể mở tệp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
