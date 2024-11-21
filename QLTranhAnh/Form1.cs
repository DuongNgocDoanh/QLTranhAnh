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
        string str = "Data Source=DESKTOP-VT4B3DF\\SQLEXPRESS;Initial Catalog=ManagePicture;User ID=sa;Password = abc123";
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
        

        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            loadData();
            LoadCategories();
            LoadCategories1();
            LoadCategories2();
            LoadCategories3();
            LoadCategories4();
            LoadCategories5();
            LoadCategories6();
            LoadCategories7();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridView1.CurrentRow.Index;
            tbMahanghoa.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            tbTenhanghoa.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            tbMaloai.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            comboBox3.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
            comboBox5.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();
            comboBox4.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();
            comboBox6.Text = dataGridView1.Rows[i].Cells[8].Value.ToString();
            comboBox7.Text = dataGridView1.Rows[i].Cells[9].Value.ToString();
            tbSoluong.Text = dataGridView1.Rows[i].Cells[10].Value.ToString();
            tbDongianhap.Text = dataGridView1.Rows[i].Cells[11].Value.ToString();
            tbDongiaban.Text = dataGridView1.Rows[i].Cells[12].Value.ToString();
            tbThoigianbaohanh.Text = dataGridView1.Rows[i].Cells[13].Value.ToString();
            tbGhichu.Text = dataGridView1.Rows[i].Cells[15].Value.ToString();
            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbMahanghoa.Text))
            {
                MessageBox.Show("Vui lòng nhập mã hàng hoá để sửa.");
                return;
            }


            string updateQuery = "UPDATE DMHangHoa SET TenHangHoa = @TenHangHoa, MaLoai = @MaLoai, MaKichThuoc = @MaKichThuoc,MaNhom = @MaNhom,MaChatLieu = @MaChatLieu,MaCongDong = @MaCongDong,MaKhung = @MaKhung,MaMau = @MaMau,MaNoiSX = @MaNoiSX,SoLuong = @SoLuong,DonGiaNhap = @DonGiaNhap,DonGiaBan = @DonGiaBan,ThoiGianBaoHanh = @ThoiGianBaoHanh,GhiChu = @GhiChu WHERE MaHang = @MaHang";

            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
            {

                
                updateCommand.Parameters.AddWithValue("@MaHang", tbMahanghoa.Text);
                updateCommand.Parameters.AddWithValue("@TenHangHoa", tbTenhanghoa.Text);
                updateCommand.Parameters.AddWithValue("@MaLoai", tbMaloai.Text);
                updateCommand.Parameters.AddWithValue("@MaKichThuoc", comboBox1.Text);
                updateCommand.Parameters.AddWithValue("@MaNhom", comboBox2.Text);
                updateCommand.Parameters.AddWithValue("@MaChatLieu", comboBox3.Text);
                updateCommand.Parameters.AddWithValue("@MaKhung", comboBox4.Text);
                updateCommand.Parameters.AddWithValue("@MaCongDong", comboBox5.Text);
                updateCommand.Parameters.AddWithValue("@MaMau", comboBox6.Text);
                updateCommand.Parameters.AddWithValue("@MaNoiSX", comboBox7.Text);
                updateCommand.Parameters.AddWithValue("@SoLuong", tbSoluong.Text);
                updateCommand.Parameters.AddWithValue("@DonGiaNhap", tbDongianhap.Text);
                updateCommand.Parameters.AddWithValue("@DonGiaBan", tbDongiaban.Text);
                updateCommand.Parameters.AddWithValue("@ThoiGianBaoHanh", tbThoigianbaohanh.Text);
                updateCommand.Parameters.AddWithValue("@GhiChu", tbGhichu.Text);

                try
                {
                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đã sửa thành công!");
                        loadData(); // Làm mới DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Mã Hàng không tồn tại!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string checkQuery = "SELECT COUNT(*) FROM DMHangHoa WHERE MaHang = @MaHang";

            using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
            {
                checkCommand.Parameters.AddWithValue("@MaHang", tbMahanghoa.Text);

                int count = (int)checkCommand.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("Mã Hàng Hoá đã tồn tại! Vui lòng nhập mã khác.");
                    return;
                }
            }


            string insertQuery = "INSERT INTO DMHangHoa (MaHang, TenHangHoa, MaLoai, MaKichThuoc, MaNhom, MaChatLieu, MaKhung,MaCongDong, MaMau, MaNoiSX, SoLuong, DonGiaNhap, DonGiaBan, ThoiGianBaoHanh, GhiChu) " +
                "VALUES (@MaHang, @TenHangHoa, @MaLoai, @MaKichThuoc, @MaNhom, @MaChatLieu, @MaKhung,@MaCongDong, @MaMau, @MaNoiSX, @SoLuong, @DonGiaNhap, @DonGiaBan, @ThoiGianBaoHanh, @GhiChu)";

            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
            {

                insertCommand.Parameters.AddWithValue("@MaHang", tbMahanghoa.Text);
                insertCommand.Parameters.AddWithValue("@TenHangHoa", tbTenhanghoa.Text);
                insertCommand.Parameters.AddWithValue("@MaLoai", tbMaloai.Text);
                insertCommand.Parameters.AddWithValue("@MaKichThuoc", comboBox1.Text);
                insertCommand.Parameters.AddWithValue("@MaNhom", comboBox2.Text);
                insertCommand.Parameters.AddWithValue("@MaChatLieu", comboBox3.Text);
                insertCommand.Parameters.AddWithValue("@MaKhung", comboBox4.Text);
                insertCommand.Parameters.AddWithValue("@MaCongDong", comboBox5.Text);
                insertCommand.Parameters.AddWithValue("@MaMau", comboBox6.Text);
                insertCommand.Parameters.AddWithValue("@MaNoiSX", comboBox7.Text);
                insertCommand.Parameters.AddWithValue("@SoLuong", tbSoluong.Text);
                insertCommand.Parameters.AddWithValue("@DonGiaNhap", tbDongianhap.Text);
                insertCommand.Parameters.AddWithValue("@DonGiaBan", tbDongiaban.Text);
                insertCommand.Parameters.AddWithValue("@ThoiGianBaoHanh", tbThoigianbaohanh.Text);
                insertCommand.Parameters.AddWithValue("@GhiChu", tbGhichu.Text);



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
        private void LoadCategories()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(str))
                {
                    connection.Open();
                    string query = "select * from Loai";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán dữ liệu vào ComboBox
                    tbMaloai.DataSource = dt;
                    tbMaloai.DisplayMember = "MaLoai";  // Hiển thị tên loại
                    tbMaloai.ValueMember = "MaLoai";      // Lưu CategoryId (mã loại)
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
            }
        }
        private void LoadCategories1()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(str))
                {
                    connection.Open();
                    string query = "select * from KichThuoc";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán dữ liệu vào ComboBox
                    comboBox1.DataSource = dt;
                    comboBox1.DisplayMember = "MaKichThuoc";  // Hiển thị tên loại
                    comboBox1.ValueMember = "MaKichThuoc";      // Lưu CategoryId (mã loại)
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
            }
        }
        private void LoadCategories2()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(str))
                {
                    connection.Open();
                    string query = "select * from NhomTranhAnh";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán dữ liệu vào ComboBox
                    comboBox2.DataSource = dt;
                    comboBox2.DisplayMember = "MaNhom";  // Hiển thị tên loại
                    comboBox2.ValueMember = "MaNhom";      // Lưu CategoryId (mã loại)
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
            }
        }
        private void LoadCategories3()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(str))
                {
                    connection.Open();
                    string query = "select * from ChatLieu";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán dữ liệu vào ComboBox
                    comboBox3.DataSource = dt;
                    comboBox3.DisplayMember = "MaChatLieu";  // Hiển thị tên loại
                    comboBox3.ValueMember = "MaChatLieu";      // Lưu CategoryId (mã loại)
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
            }
        }
        private void LoadCategories4()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(str))
                {
                    connection.Open();
                    string query = "select * from CongDong";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán dữ liệu vào ComboBox
                    comboBox4.DataSource = dt;
                    comboBox4.DisplayMember = "MaCongDong";  // Hiển thị tên loại
                    comboBox4.ValueMember = "MaCongDong";      // Lưu CategoryId (mã loại)
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
            }
        }
        private void LoadCategories5()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(str))
                {
                    connection.Open();
                    string query = "select * from KhungAnh";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán dữ liệu vào ComboBox
                    comboBox5.DataSource = dt;
                    comboBox5.DisplayMember = "MaKhung";  // Hiển thị tên loại
                    comboBox5.ValueMember = "MaKhung";      // Lưu CategoryId (mã loại)
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
            }
        }
        private void LoadCategories6()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(str))
                {
                    connection.Open();
                    string query = "select * from MauSac";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán dữ liệu vào ComboBox
                    comboBox6.DataSource = dt;
                    comboBox6.DisplayMember = "MaMau";  // Hiển thị tên loại
                    comboBox6.ValueMember = "MaMau";      // Lưu CategoryId (mã loại)
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
            }
        }
        private void LoadCategories7()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(str))
                {
                    connection.Open();
                    string query = "select * from NoiSanXuat";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán dữ liệu vào CmboBox
                    comboBox7.DataSource = dt;
                    comboBox7.DisplayMember = "MaNoiSX";  // Hiển thị tên loại
                    comboBox7.ValueMember = "MaNoiSX";      // Lưu CategoryId (mã loại)
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
            }
        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
            loadData();
            ClearInputs();
        } 
        private void ClearInputs()
        {
            tbMahanghoa.Clear();
            tbTenhanghoa.Clear();
            tbMaloai.SelectedIndex = -1;
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            comboBox6.SelectedIndex = -1;
            comboBox7.SelectedIndex = -1;
            tbDongianhap.Clear();
            tbDongiaban.Clear();
            tbThoigianbaohanh.Clear();
            tbGhichu.Clear();
        }
        
    }
}
