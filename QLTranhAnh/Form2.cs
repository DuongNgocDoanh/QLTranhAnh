using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLTranhAnh
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private Form currentFormChild;
        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel_Body.Controls.Add(childForm);
            panel_Body.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

       
        private void btDanhMuc_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form1());
            label1.Text = btDanhMuc.Text;
        }

        private void btnTimkiem_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new FormTimkiem());
            label1.Text = btnTimkiem.Text;
        }

        private void btnHoadon_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new FormHoaDon());
            label1.Text = btnHoadon.Text;
        }

        private void btnKhachhang_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new FormKhachHang());
            label1.Text = btnKhachhang.Text;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận người dùng có muốn thoát không
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn thoát chương trình không?",
                                                        "Xác nhận thoát",
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                // Nếu người dùng chọn Yes, đóng ứng dụng
                Application.Exit();
            }
        }
    }
}
