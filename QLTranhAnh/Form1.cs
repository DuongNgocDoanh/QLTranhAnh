using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace QLTranhAnh
{
    public partial class Form1 : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = "Data Source=DESKTOP-VT4B3DF\\SQLEXPRESS;Initial Catalog=QuanLyTranhAnh1;User ID=sa;Password = abc123";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        void loadData()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from DMHangHoa";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }
        public Form1()
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

        private void btnHoadon_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormHoaDon());
            label1.Text = btnHoadon.Text;
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormTimkiem());
            label1.Text = btnTimkiem.Text;
        }

        private void btnKhachhang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormKhachHang());
            label1.Text = btnKhachhang.Text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            loadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridView1.CurrentRow.Index;
            tbMahanghoa.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            tbTenhanghoa.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            tbMaloai.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            tbMakichthuoc.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            tbManhom.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            tbMacl.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
            tbMakhung.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();
            tbMacongdong.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();
            tbMamau.Text = dataGridView1.Rows[i].Cells[8].Value.ToString();
            tbMaNSX.Text = dataGridView1.Rows[i].Cells[9].Value.ToString();
            tbSoluong.Text = dataGridView1.Rows[i].Cells[10].Value.ToString();
            tbDongianhap.Text = dataGridView1.Rows[i].Cells[11].Value.ToString();
            tbDongiaban.Text = dataGridView1.Rows[i].Cells[12].Value.ToString();
            tbThoigianbaohanh.Text = dataGridView1.Rows[i].Cells[13].Value.ToString();
            tbGhichu.Text = dataGridView1.Rows[i].Cells[15].Value.ToString();
            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string checkQuery = "SELECT COUNT(*) FROM DMHangHoa WHERE MaHangHoa = @MaHangHoa";

            using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
            {
                checkCommand.Parameters.AddWithValue("@MaHangHoa", tbMahanghoa.Text);

                int count = (int)checkCommand.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("Mã Hàng Hoá đã tồn tại! Vui lòng nhập mã khác.");
                    return;
                }
            }


            string insertQuery = "INSERT INTO DMHangHoa (MaHang, TenHangHoa, MaLoai, MaKichThuoc, MaNhom, MaChatLieu, MaKhung,MaCongDong, MaMau, MaNoiSX, SoLuong, DonGiaNhap, DonGiaBan, ThoiGianBaoHanh, Anh, GhiChu) " +
                "VALUES (@MaHang, @TenHangHoa, @MaLoai, @MaKichThuoc, @MaNhom, @MaChatLieu, @MaKhung,@MaCongDong, @MaMau, @MaNoiSX, @SoLuong, @DonGiaNhap, @DonGiaBan, @ThoiGianBaoHanh, @Anh, @GhiChu)";

            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
            {

                insertCommand.Parameters.AddWithValue("@MaHang", tbMahanghoa.Text);
                insertCommand.Parameters.AddWithValue("@TenHangHoa", tbTenhanghoa.Text);
                insertCommand.Parameters.AddWithValue("@MaLoai", tbMaloai.Text);
                insertCommand.Parameters.AddWithValue("@MaKichThuoc", tbMakichthuoc.Text);
                insertCommand.Parameters.AddWithValue("@MaNhom", tbManhom.Text);
                insertCommand.Parameters.AddWithValue("@MaChatLieu", tbMacl.Text);
                insertCommand.Parameters.AddWithValue("@MaKhung", tbMakhung.Text);
                insertCommand.Parameters.AddWithValue("@MaCongDong", tbMacongdong.Text);
                insertCommand.Parameters.AddWithValue("@MaMau", tbMamau.Text);
                insertCommand.Parameters.AddWithValue("@MaNoiSX", tbMaNSX.Text);
                insertCommand.Parameters.AddWithValue("@SoLuong", tbSoluong.Text);
                insertCommand.Parameters.AddWithValue("@DonGiaNhap", tbDongianhap.Text);
                insertCommand.Parameters.AddWithValue("@DonGiaBan", tbDongiaban.Text);



                try
                {

                    insertCommand.ExecuteNonQuery();
                    MessageBox.Show("Đã thêm thành công!");


                    loadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbMahanghoa.Text))
            {
                MessageBox.Show("Vui lòng nhập mã hàng hoá cần xóa.");
                return;
            }


            string deleteQuery = "DELETE FROM DMHangHoa WHERE MaHang = @MaHang";

            using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
            {
                deleteCommand.Parameters.AddWithValue("@MaHang", tbMahanghoa.Text);

                try
                {
                    int rowsAffected = deleteCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đã xóa thành công!");
                        loadData();
                    }
                    else
                    {
                        MessageBox.Show("Mã hàng hoá không tồn tại!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
    }
}
