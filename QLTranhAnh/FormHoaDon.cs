using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLTranhAnh
{
    public partial class FormHoaDon : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = "Data Source=DESKTOP-VT4B3DF\\SQLEXPRESS;Initial Catalog=ManagePicture;User ID=sa;Password = abc123";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable tableHDN = new DataTable();
        DataTable tableNCC = new DataTable();
        DataTable tableNV = new DataTable();
        DataTable tableCV = new DataTable();
        void loadDataHDN()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from HoaDonNhap";
            adapter.SelectCommand = command;
            tableHDN.Clear();
            adapter.Fill(tableHDN);
            dataGridView1.DataSource = tableHDN;
        }
        void loadDataNCC()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from NhaCungCap";
            adapter.SelectCommand = command;
            tableNCC.Clear();
            adapter.Fill(tableNCC);
            dataGridView3.DataSource = tableNCC;
        }
        void loadCV()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from CongViec";
            adapter.SelectCommand = command;
            tableCV.Clear();
            adapter.Fill(tableCV);
            dataGridView5.DataSource = tableCV;
        }
        void loadNV()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from NhanVien";
            adapter.SelectCommand = command;
            tableNV.Clear();
            adapter.Fill(tableNV);
            dataGridView4.DataSource = tableNV;
        }
        public FormHoaDon()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btXoaNCC_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox12.Text))
            {
                MessageBox.Show("Vui lòng nhập mã cần xóa.");
                return;
            }

            // Thông báo xác nhận
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                string deleteQuery = "DELETE FROM NhaCungCap WHERE MaNCC = @MaNCC";

                using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                {
                    deleteCommand.Parameters.AddWithValue("@MaNCC", textBox12.Text);

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
                            loadDataNCC();
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
                    cbNV.DataSource = dt;
                    cbNV.DisplayMember = "MaNhanVien";  // Hiển thị tên loại
                    cbNV.ValueMember = "MaNhanVien";      // Lưu CategoryId (mã loại)
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
                    string query = "select * from NhaCungCap ";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán dữ liệu vào ComboBox
                    comboBox1.DataSource = dt;
                    comboBox1.DisplayMember = "MaNCC";  // Hiển thị tên loại
                    comboBox1.ValueMember = "MaNCC";      // Lưu CategoryId (mã loại)
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
                    string query = "select * from CongViec ";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán dữ liệu vào ComboBox
                    comboBox4.DataSource = dt;
                    comboBox4.DisplayMember = "MaCV";  // Hiển thị tên loại
                    comboBox4.ValueMember = "MaCV";      // Lưu CategoryId (mã loại)
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
            }
        }
        private void btnChiTietHDN_Click(object sender, EventArgs e)
        {
            string maHDN = tbSoHDN.Text;
            using (SqlConnection connection = new SqlConnection(str))
            {
                connection.Open();

                // Truy vấn để kiểm tra sự tồn tại của mã HDN
                string checkQuery = "SELECT COUNT(*) FROM HoaDonNhap WHERE SoHDN = @SoHDN"; // Giả sử bảng là HoaDonNhap
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@SoHDN", maHDN);
                    int count = (int)checkCommand.ExecuteScalar();

                    if (count == 0)
                    {
                        MessageBox.Show("Mã Hóa Đơn Nhập không tồn tại trong cơ sở dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; // Dừng thực hiện nếu mã HDN không tồn tại
                    }
                }
            }

            // Nếu mã HDN hợp lệ và tồn tại, mở form ChiTietHDN
            ChiTietHDN chiTietForm = new ChiTietHDN();
            chiTietForm.soHDN = maHDN;
            chiTietForm.Show();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                loadDataHDN();
            }
            else if (tabControl1.SelectedTab == tabPage3)
            {
                loadDataNCC();
            }
            else if (tabControl1.SelectedTab == tabPage4)
            {
                loadNV();

            }
            else if (tabControl1.SelectedTab == tabPage5)
            {
                loadCV();

            }
        }

        private void FormHoaDon_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            loadDataHDN();
            LoadCategories();
            LoadCategories1();
            LoadCategories2();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void btnThemNCC_Click(object sender, EventArgs e)
        {
            string checkQuery = "SELECT COUNT(*) FROM NhaCungCap WHERE MaNCC = @MaNCC";

            using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
            {
                checkCommand.Parameters.AddWithValue("@MaNCC", textBox12.Text);

                int count = (int)checkCommand.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("Mã đã tồn tại! Vui lòng nhập mã khác.");
                    return;
                }
            }


            string insertQuery = "INSERT INTO NhaCungCap (MaNCC,TenNCC,DiaChi,DienThoai) VALUES (@MaNCC,@TenNCC,@DiaChi,@DienThoai)";
            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
            {

                insertCommand.Parameters.AddWithValue("@MaNCC", textBox12.Text);
                insertCommand.Parameters.AddWithValue("@TenNCC", textBox13.Text);
                insertCommand.Parameters.AddWithValue("@DiaChi", textBox14.Text);
                insertCommand.Parameters.AddWithValue("@DienThoai", textBox15.Text);

                try
                {

                    insertCommand.ExecuteNonQuery();
                    MessageBox.Show("Đã thêm thành công!");


                    loadDataNCC();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnThemNV_Click(object sender, EventArgs e)
        {
            string checkQuery = "SELECT COUNT(*) FROM NhanVien WHERE MaNhanVien = @MaNhanVien";

            using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
            {
                checkCommand.Parameters.AddWithValue("@MaNhanVien", textBox19.Text);

                int count = (int)checkCommand.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("Mã đã tồn tại! Vui lòng nhập mã khác.");
                    return;
                }
            }


            string insertQuery = "INSERT INTO NhanVien (MaNhanVien,TenNhanVien,GioiTinh,NgaySinh,DienThoai,DiaChi,MaCV) VALUES (@MaNhanVien,@TenNhanVien,@GioiTinh,@NgaySinh,@DienThoai,@DiaChi,@MaCV)";
            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
            {

                insertCommand.Parameters.AddWithValue("@MaNhanVien", textBox19.Text);
                insertCommand.Parameters.AddWithValue("@TenNhanVien", textBox18.Text);
                insertCommand.Parameters.AddWithValue("@GioiTinh", comboBox3.Text);
                insertCommand.Parameters.AddWithValue("@NgaySinh", dateTimePicker2.Value);
                insertCommand.Parameters.AddWithValue("@DienThoai", textBox20.Text);
                insertCommand.Parameters.AddWithValue("@DiaChi", textBox21.Text);
                insertCommand.Parameters.AddWithValue("@MaCV", comboBox4.Text);
                try
                {

                    insertCommand.ExecuteNonQuery();
                    MessageBox.Show("Đã thêm thành công!");


                    loadNV();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btTimKiemNCC_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox12.Text.Trim(); // Lấy từ khóa tìm kiếm

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
                FROM NhaCungCap
                WHERE MaNCC = @SearchTerm
            ";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@SearchTerm", searchTerm);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán kết quả tìm kiếm vào DataGridView
                    dataGridView3.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
            }
        }

        private void btSuaNCC_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox12.Text))
            {
                MessageBox.Show("Vui lòng nhập mã để sửa.");
                return;
            }


            string updateQuery = "UPDATE NhaCungCap SET TenNCC = @TenNCC,DiaChi = @DiaChi,DienThoai = @DienThoai WHERE MaNCC = @MaNCC";

            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
            {

                updateCommand.Parameters.AddWithValue("@MaNCC", textBox12.Text);
                updateCommand.Parameters.AddWithValue("@TenNCC", textBox13.Text);
                updateCommand.Parameters.AddWithValue("@DiaChi", textBox14.Text);
                updateCommand.Parameters.AddWithValue("@DienThoai", textBox15.Text);
                try
                {
                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đã sửa thành công!");
                        loadDataNCC();
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

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox19.Text))
            {
                MessageBox.Show("Vui lòng nhập mã cần xóa.");
                return;
            }

            // Thông báo xác nhận
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                string deleteQuery = "DELETE FROM NhanVien WHERE MaNhanVien = @MaNhanVien";

                using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                {
                    deleteCommand.Parameters.AddWithValue("@MaNhanVien", textBox19.Text);

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
                            loadNV();
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

        private void btnSuaNV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox19.Text))
            {
                MessageBox.Show("Vui lòng nhập mã để sửa.");
                return;
            }


            string updateQuery = "UPDATE NhanVien SET TenNhanVien = @TenNhanVien,GioiTinh = @GioiTinh,NgaySinh = @NgaySinh,DienThoai = @DienThoai,DiaChi = @DiaChi,MaCV = @MaCv WHERE MaNhanVien = @MaNhanVien";

            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
            {

                updateCommand.Parameters.AddWithValue("@MaNhanVien", textBox19.Text);
                updateCommand.Parameters.AddWithValue("@TenNhanVien", textBox18.Text);
                updateCommand.Parameters.AddWithValue("@GioiTinh", comboBox3.Text);
                updateCommand.Parameters.AddWithValue("@NgaySinh", dateTimePicker2.Value);
                updateCommand.Parameters.AddWithValue("@DienThoai", textBox20.Text);
                updateCommand.Parameters.AddWithValue("@DiaChi", textBox21.Text);
                updateCommand.Parameters.AddWithValue("@MaCV", comboBox4.Text);


                try
                {
                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đã sửa thành công!");
                        loadNV();
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

        private void btnTimKiemNV_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox19.Text.Trim(); // Lấy từ khóa tìm kiếm

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
                FROM NhanVien
                WHERE MaNhanVien = @SearchTerm
            ";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@SearchTerm", searchTerm);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán kết quả tìm kiếm vào DataGridView
                    dataGridView4.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
            }
        }

        private void btnThemCV_Click(object sender, EventArgs e)
        {
            string checkQuery = "SELECT COUNT(*) FROM CongViec WHERE MaCV = @MaCV";

            using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
            {
                checkCommand.Parameters.AddWithValue("@MaCV", textBox26.Text);

                int count = (int)checkCommand.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("Mã đã tồn tại! Vui lòng nhập mã khác.");
                    return;
                }
            }


            string insertQuery = "INSERT INTO CongViec (MaCV,TenCV,MucLuong) VALUES (@MaCV,@TenCV,@MucLuong)";
            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
            {

                insertCommand.Parameters.AddWithValue("@MaCV", textBox26.Text);
                insertCommand.Parameters.AddWithValue("@TenCV", textBox25.Text);
                insertCommand.Parameters.AddWithValue("@MucLuong", textBox24.Text);


                try
                {

                    insertCommand.ExecuteNonQuery();
                    MessageBox.Show("Đã thêm thành công!");


                    loadCV();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnXoaCV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox26.Text))
            {
                MessageBox.Show("Vui lòng nhập mã cần xóa.");
                return;
            }

            // Thông báo xác nhận
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                string deleteQuery = "DELETE FROM CongViec WHERE MaCV = @MaCV";

                using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                {
                    deleteCommand.Parameters.AddWithValue("@MaCV", textBox26.Text);

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
                            loadCV();
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

        private void btnSuaCV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox26.Text))
            {
                MessageBox.Show("Vui lòng nhập mã để sửa.");
                return;
            }


            string updateQuery = "UPDATE CongViec SET TenCV = @TenCV,MucLuong = @MucLuong WHERE MaCV = @MaCV";

            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
            {

                updateCommand.Parameters.AddWithValue("@MaCV", textBox26.Text);
                updateCommand.Parameters.AddWithValue("@TenCV", textBox25.Text);
                updateCommand.Parameters.AddWithValue("@MucLuong", textBox24.Text);

                try
                {
                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đã sửa thành công!");
                        loadCV();
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

        private void btnTimKiemCV_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox26.Text.Trim(); // Lấy từ khóa tìm kiếm

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
                FROM CongViec
                WHERE MaCV = @SearchTerm
            ";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@SearchTerm", searchTerm);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán kết quả tìm kiếm vào DataGridView
                    dataGridView5.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
            }
        }
    }
}
