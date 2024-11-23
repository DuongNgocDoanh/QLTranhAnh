using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLTranhAnh
{
    public partial class ChiTietHDN : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = "Data Source=DESKTOP-VT4B3DF\\SQLEXPRESS;Initial Catalog=ManagePicture;User ID=sa;Password = abc123";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        public ChiTietHDN()
        {
            InitializeComponent();
        }
        public string soHDN
        {
            get { return tbSoHDN.Text; }
            set { tbSoHDN.Text = value; }

        }
        private void LoadCategories()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(str))
                {
                    connection.Open();
                    string query = "select * from DMHangHoa";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán dữ liệu vào ComboBox
                    cbMaHang.DataSource = dt;
                    cbMaHang.DisplayMember = "MaHang";  // Hiển thị tên loại
                    cbMaHang.ValueMember = "MaHang";      // Lưu CategoryId (mã loại)
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
            }
        }
        void LoadData()
        {
            string soHDN = tbSoHDN.Text.Trim();

            if (!string.IsNullOrEmpty(soHDN)) // Kiểm tra SoHDN không rỗng
            {
                try
                {
                    string query = @"SELECT * FROM ChiTietHoaDonNhap WHERE SoHDN = @SoHDN";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@SoHDN", soHDN);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán dữ liệu vào DataGridView
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi truy vấn dữ liệu: {ex.Message}");
                }
            }
        }


        private void tbSoHDN_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string checkQuery = "SELECT COUNT(*) FROM ChiTietHoaDonNhap WHERE MaHang = @MaHang";
            string maHang = cbMaHang.Text;
            int soLuongMoi = int.Parse(SoLuongHang.Text);
            using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
            {
                checkCommand.Parameters.AddWithValue("@MaHang", cbMaHang.Text);

                int count = (int)checkCommand.ExecuteScalar();
                if (count > 0)
                {
                    string updateQuery = "UPDATE ChiTietHoaDonNhap SET SoLuong = SoLuong + @SoLuongMoi WHERE MaHang = @MaHang";
                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@MaHang", maHang);
                        updateCommand.Parameters.AddWithValue("@SoLuongMoi", soLuongMoi);
                        updateCommand.ExecuteNonQuery();
                    }
                    MessageBox.Show("Đã cập nhật số lượng hàng thành công.");
                    UpdateDMHangHoa(maHang, soLuongMoi, connection);
                    LoadData();
                }
            }


            string insertQuery = "INSERT INTO ChiTietHoaDonNhap (SoHDN,MaHang,SoLuong,DonGia,GiamGia) VALUES (@SoHDN,@MaHang,@SoLuong,@DonGia,@GiamGia)";
            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
            {
                insertCommand.Parameters.AddWithValue("@SoHDN", tbSoHDN.Text);
                insertCommand.Parameters.AddWithValue("@MaHang", cbMaHang.Text);
                insertCommand.Parameters.AddWithValue("@SoLuong", SoLuongHang.Text);
                insertCommand.Parameters.AddWithValue("@DonGia", textBox4.Text);
                insertCommand.Parameters.AddWithValue("@GiamGia", textBox5.Text);
                try
                {

                    insertCommand.ExecuteNonQuery();
                    MessageBox.Show("Đã thêm thành công!");
                    UpdateDMHangHoa(maHang, soLuongMoi, connection);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void ChiTietHDN_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            LoadData();
            LoadCategories();
        }
        private void UpdateDMHangHoa(string maHang, int soLuongMoi, SqlConnection connection)
        {
            string updateDMQuery = "UPDATE DMHangHoa SET SoLuong = SoLuong + @SoLuongMoi WHERE MaHang = @MaHang";
            using (SqlCommand updateDMCommand = new SqlCommand(updateDMQuery, connection))
            {
                updateDMCommand.Parameters.AddWithValue("@MaHang", maHang);
                updateDMCommand.Parameters.AddWithValue("@SoLuongMoi", soLuongMoi);
                try
                {
                    updateDMCommand.ExecuteNonQuery();
                    MessageBox.Show("Đã cập nhật số lượng hàng trong DMHangHoa thành công.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật DMHangHoa: " + ex.Message);
                }
            }
        }
    }
}
