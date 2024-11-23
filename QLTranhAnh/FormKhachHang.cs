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

namespace QLTranhAnh
{
    public partial class FormKhachHang : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = "Data Source=DESKTOP-VT4B3DF\\SQLEXPRESS;Initial Catalog=ManagePicture;User ID=sa;Password = abc123";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable tableKH = new DataTable();
        DataTable tableCD = new DataTable();
        DataTable tableHD = new DataTable();
        void loadDataKH()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from KhachHang";
            adapter.SelectCommand = command;
            tableKH.Clear();
            adapter.Fill(tableKH);
            dataGridView1.DataSource = tableKH;
        }
        void loadDataCongDong()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from CongDong";
            adapter.SelectCommand = command;
            tableCD.Clear();
            adapter.Fill(tableCD);
            dataGridView2.DataSource = tableCD;
        }
        void loadHD()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from HoaDonBan";
            adapter.SelectCommand = command;
            tableHD.Clear();
            adapter.Fill(tableHD);
            dataGridView3.DataSource = tableHD;
        }
        
        public FormKhachHang()
        {
            InitializeComponent();
        }

        private void btnThemKH_Click(object sender, EventArgs e)
        {
            string checkQuery = "SELECT COUNT(*) FROM KhachHang WHERE MaKhach = @MaKhach";

            using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
            {
                checkCommand.Parameters.AddWithValue("@MaKhach", textBox1.Text);

                int count = (int)checkCommand.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("Mã đã tồn tại! Vui lòng nhập mã khác.");
                    return;
                }
            }


            string insertQuery = "INSERT INTO KhachHang (MaKhach,TenKhach,DiaChi,DienThoai) VALUES (@MaKhach,@TenKhach,@DiaChi,@DienThoai)";
            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
            {

                insertCommand.Parameters.AddWithValue("@MaKhach", textBox1.Text);
                insertCommand.Parameters.AddWithValue("@TenKhach", textBox2.Text);
                insertCommand.Parameters.AddWithValue("@DiaChi", textBox4.Text);
                insertCommand.Parameters.AddWithValue("@DienThoai", textBox3.Text);

                try
                {

                    insertCommand.ExecuteNonQuery();
                    MessageBox.Show("Đã thêm thành công!");


                    loadDataKH();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                loadDataKH();
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {
                loadDataCongDong();
            }
            else if (tabControl1.SelectedTab == tabPage3)
            {
                loadHD();
            }
            
        }

        private void FormKhachHang_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            loadDataKH();
            LoadCategories();
            LoadCategories1();
        }
        private void LoadCategories()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(str))
                {
                    connection.Open();
                    string query = "select * from NhanVien";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán dữ liệu vào ComboBox
                    cbMaNV.DataSource = dt;
                    cbMaNV.DisplayMember = "MaNhanVien";  // Hiển thị tên loại
                    cbMaNV.ValueMember = "MaNhanVien";      // Lưu CategoryId (mã loại)
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
                    string query = "select * from KhachHang";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán dữ liệu vào ComboBox
                    comboBox1.DataSource = dt;
                    comboBox1.DisplayMember = "MaKhach";  // Hiển thị tên loại
                    comboBox1.ValueMember = "MaKhach";      // Lưu CategoryId (mã loại)
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            string maHDN = tbSoHDB.Text;
            using (SqlConnection connection = new SqlConnection(str))
            {
                connection.Open();

                // Truy vấn để kiểm tra sự tồn tại của mã HDN
                string checkQuery = "SELECT COUNT(*) FROM HoaDonBan WHERE SoHDN = @SoHDN"; // Giả sử bảng là HoaDonNhap
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@SoHDN", maHDN);
                    int count = (int)checkCommand.ExecuteScalar();

                    if (count == 0)
                    {
                        MessageBox.Show("Mã Hóa Đơn không tồn tại trong cơ sở dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; // Dừng thực hiện nếu mã HDN không tồn tại
                    }
                }
            }

            // Nếu mã HDN hợp lệ và tồn tại, mở form ChiTietHDN
            ChiTiet chiTietForm = new ChiTiet();
            chiTietForm.soHDB = maHDN;
            chiTietForm.Show();
        }

        private void btnXoaKH_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Vui lòng nhập mã cần xóa.");
                return;
            }

            // Thông báo xác nhận
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                string deleteQuery = "DELETE FROM KhachHang WHERE MaKhach = @MaKhach";

                using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                {
                    deleteCommand.Parameters.AddWithValue("@MaKhach", textBox1.Text);

                    try
                    {
                        // Mở kết nối nếu nó chưa được mở
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }

                        int rowsAffected = deleteCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đã xóa thành công!");
                            loadDataKH();
                        }
                        else
                        {
                            MessageBox.Show("Mã không tồn tại!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                    finally
                    {
                        // Đóng kết nối nếu nó được mở trong khối try
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Hủy thao tác xóa.");
            }
        }

        private void btnSuaKH_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Vui lòng nhập mã để sửa.");
                return;
            }


            string updateQuery = "UPDATE KhachHang SET TenKhach = @TenKhach WHERE MaKhach = @MaKhach";

            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
            {

                updateCommand.Parameters.AddWithValue("@MaKhach", textBox1.Text);
                updateCommand.Parameters.AddWithValue("@TenKhach", textBox2.Text);


                try
                {
                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đã sửa thành công!");
                        loadDataKH();
                    }
                    else
                    {
                        MessageBox.Show("Mã không tồn tại!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        private void btnLammoiKH_Click(object sender, EventArgs e)
        {
            loadDataKH();
            ClearInputs();
        }
        private void ClearInputs()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }
        private void btnTimKiemKH_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox1.Text.Trim(); // Lấy từ khóa tìm kiếm

            if (string.IsNullOrEmpty(searchTerm))
            {
                MessageBox.Show("Vui lòng nhập mã cần tìm.");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(str))
                {
                    connection.Open();


                    string query = @"
                SELECT *
                FROM KhachHang
                WHERE MaKhach = @SearchTerm
            ";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@SearchTerm", searchTerm);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán kết quả tìm kiếm vào DataGridView
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
            }
        }

        private void btnThemCD_Click(object sender, EventArgs e)
        {
            string checkQuery = "SELECT COUNT(*) FROM CongDong WHERE MaCongDong = @MaCongDong";

            using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
            {
                checkCommand.Parameters.AddWithValue("@MaCongDong", textBox5.Text);

                int count = (int)checkCommand.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("Mã đã tồn tại! Vui lòng nhập mã khác.");
                    return;
                }
            }


            string insertQuery = "INSERT INTO CongDong (MaCongDong,TenCongDong) VALUES (@MaCongDong,@TenCongDong)";
            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
            {

                insertCommand.Parameters.AddWithValue("@MaCongDong", textBox5.Text);
                insertCommand.Parameters.AddWithValue("@TenCongDong", textBox6.Text);


                try
                {

                    insertCommand.ExecuteNonQuery();
                    MessageBox.Show("Đã thêm thành công!");


                    loadDataCongDong();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnXoaCD_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Vui lòng nhập mã cần xóa.");
                return;
            }

            // Thông báo xác nhận
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                string deleteQuery = "DELETE FROM CongDong WHERE MaCongDong = @MaCongDong";

                using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                {
                    deleteCommand.Parameters.AddWithValue("@MaCongDong", textBox5.Text);

                    try
                    {
                        // Mở kết nối nếu nó chưa được mở
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }

                        int rowsAffected = deleteCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đã xóa thành công!");
                            loadDataCongDong();
                        }
                        else
                        {
                            MessageBox.Show("Mã không tồn tại!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                    finally
                    {
                        // Đóng kết nối nếu nó được mở trong khối try
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Hủy thao tác xóa.");
            }
        }

        private void btnSuaCD_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Vui lòng nhập mã để sửa.");
                return;
            }


            string updateQuery = "UPDATE CongDong SET TenCongDong = @TenCongDong WHERE MaCongDong = @MaCongDong";

            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
            {

                updateCommand.Parameters.AddWithValue("@MaCongDong", textBox5.Text);
                updateCommand.Parameters.AddWithValue("@TenCongDong", textBox6.Text);


                try
                {
                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đã sửa thành công!");
                        loadDataCongDong();
                    }
                    else
                    {
                        MessageBox.Show("Mã không tồn tại!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnLammoiCD_Click(object sender, EventArgs e)
        {
            loadDataCongDong();
            ClearInputs1();
        }
        private void ClearInputs1()
        {
            textBox5.Clear();
            textBox6.Clear();
            
        }

        private void btnTimCongDong_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox5.Text.Trim(); // Lấy từ khóa tìm kiếm

            if (string.IsNullOrEmpty(searchTerm))
            {
                MessageBox.Show("Vui lòng nhập mã cần tìm.");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(str))
                {
                    connection.Open();


                    string query = @"
                SELECT *
                FROM CongDong
                WHERE MaCongDong = @SearchTerm
            ";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@SearchTerm", searchTerm);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán kết quả tìm kiếm vào DataGridView
                    dataGridView2.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
            }
        }
    }
}
