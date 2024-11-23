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
    public partial class FormTimkiem : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = "Data Source=DESKTOP-VT4B3DF\\SQLEXPRESS;Initial Catalog=ManagePicture;User ID=sa;Password = abc123";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable tableKT = new DataTable();
        DataTable tableLoai = new DataTable();
        DataTable tableNhom = new DataTable();
        DataTable tableCL = new DataTable();
        DataTable tableNSX = new DataTable();
        DataTable tableMau = new DataTable();
        DataTable tableKA = new DataTable();

        void loadDataKT()
        {
                command = connection.CreateCommand();
                command.CommandText = "select * from KichThuoc";
                adapter.SelectCommand = command;
                tableKT.Clear();
                adapter.Fill(tableKT);
                dataGridViewKT.DataSource = tableKT;
        }

        void loadDataLoai()
        {   
                command = connection.CreateCommand();
                command.CommandText = "select * from Loai";
                adapter.SelectCommand = command;
                tableLoai.Clear();
                adapter.Fill(tableLoai);
                dataGridViewLoai.DataSource = tableLoai;
        }
        void loadDataNhom()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from NhomTranhAnh";
            adapter.SelectCommand = command;
            tableNhom.Clear();
            adapter.Fill(tableNhom);
            dataGridViewNhom.DataSource = tableNhom;
        }
        void loadDataCL()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from ChatLieu";
            adapter.SelectCommand = command;
            tableCL.Clear();
            adapter.Fill(tableCL);
            dataGridViewCL.DataSource = tableCL;
        }
        void loadDataNSX()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from NoiSanXuat";
            adapter.SelectCommand = command;
            tableNSX.Clear();
            adapter.Fill(tableNSX);
            dataGridViewNSX.DataSource = tableNSX;
        }
        void loadDataMau()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from MauSac";
            adapter.SelectCommand = command;
            tableMau.Clear();
            adapter.Fill(tableMau);
            dataGridViewMau.DataSource = tableMau;
        }
        void loadDataKA()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from KhungAnh";
            adapter.SelectCommand = command;
            tableKA.Clear();
            adapter.Fill(tableKA);
            dataGridViewKA.DataSource = tableKA;
        }

        public FormTimkiem()
        {
            InitializeComponent();

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string checkQuery = "SELECT COUNT(*) FROM KichThuoc WHERE MaKichThuoc = @MaKichThuoc";

            using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
            {
                checkCommand.Parameters.AddWithValue("@MaKichThuoc", textBox1.Text);

                int count = (int)checkCommand.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("Mã Kích Thước đã tồn tại! Vui lòng nhập mã khác.");
                    return;
                }
            }


            string insertQuery = "INSERT INTO KichThuoc (MaKichThuoc,TenKichThuoc) VALUES (@MaKichThuoc,@TenKichThuoc)";
            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
            {

                insertCommand.Parameters.AddWithValue("@MaKichThuoc", textBox1.Text);
                insertCommand.Parameters.AddWithValue("@TenKichThuoc", textBox2.Text);


                try
                {

                    insertCommand.ExecuteNonQuery();
                    MessageBox.Show("Đã thêm thành công!");


                    loadDataKT();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void FormTimkiem_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            loadDataKT();
            
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2)
            {
                loadDataLoai();
            }
            else if (tabControl1.SelectedTab == tabPage1)
            {
                loadDataKT();
            }
            else if (tabControl1.SelectedTab == tabPage3)
            {
                loadDataNhom();
            }
            else if (tabControl1.SelectedTab == tabPage4)
            {
                loadDataCL();
            }
            else if (tabControl1.SelectedTab == tabPage5)
            {
                loadDataNSX();
            }
            else if (tabControl1.SelectedTab == tabPage6)
            {
                loadDataMau();
            }
            else if (tabControl1.SelectedTab == tabPage7)
            {
                loadDataKA();
            }
        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
            loadDataKT();
            loadDataCL() ;
            loadDataKA() ;
            loadDataLoai();
            loadDataMau() ;
            loadDataNhom() ;
            loadDataNSX() ;
            ClearInputs();
        }
        private void ClearInputs()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            tbMaLoai.Clear();
            tbTenLoai.Clear();
            tbMLTimKiem.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox12.Clear();
            textBox13.Clear();
            textBox14.Clear();
            textBox15.Clear();
            textBox17.Clear();
            textBox18.Clear();
            textBox19.Clear();
            textBox21.Clear();
            textBox22.Clear();
            textBox23.Clear();
            textBox25.Clear();
            textBox26.Clear();
            textBox27.Clear();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnThem1_Click(object sender, EventArgs e)
        {
            string checkQuery = "SELECT COUNT(*) FROM Loai WHERE MaLoai = @MaLoai";

            using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
            {
                checkCommand.Parameters.AddWithValue("@MaLoai", tbMaLoai.Text);

                int count = (int)checkCommand.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("Mã Loại đã tồn tại! Vui lòng nhập mã khác.");
                    return;
                }
            }


            string insertQuery = "INSERT INTO Loai (MaLoai,TenLoai) VALUES (@MaLoai,@TenLoai)";
            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
            {

                insertCommand.Parameters.AddWithValue("@MaLoai", tbMaLoai.Text);
                insertCommand.Parameters.AddWithValue("@TenLoai", tbTenLoai.Text);
                

                try
                {

                    insertCommand.ExecuteNonQuery();
                    MessageBox.Show("Đã thêm thành công!");


                    loadDataLoai();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnTim1_Click(object sender, EventArgs e)
        {
            string searchTerm = tbMLTimKiem.Text.Trim(); // Lấy từ khóa tìm kiếm

            if (string.IsNullOrEmpty(searchTerm))
            {
                MessageBox.Show("Vui lòng nhập mã loại hoặc tên loại cần tìm.");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(str))
                {
                    connection.Open();

                    
                    string query = @"
                        SELECT p.MaHang, p.TenHangHoa,p.SoLuong, c.MaLoai ,c.TenLoai
                        FROM DMHangHoa p
                        INNER JOIN Loai c ON p.MaLoai = c.MaLoai
                        WHERE c.MaLoai = @SearchTerm
                    ";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@SearchTerm", searchTerm);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán kết quả tìm kiếm vào DataGridView
                    dataGridViewLoai.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Vui lòng nhập mã kích thước để sửa.");
                return;
            }


            string updateQuery = "UPDATE KichThuoc SET TenKichThuoc = @TenKichThuoc WHERE MaKichThuoc = @MaKichThuoc";

            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
            {

                updateCommand.Parameters.AddWithValue("@MaKichThuoc", textBox1.Text);
                updateCommand.Parameters.AddWithValue("@TenKichThuoc", textBox2.Text);
                

                try
                {
                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đã sửa thành công!");
                        loadDataKT(); 
                    }
                    else
                    {
                        MessageBox.Show("Mã kích thước không tồn tại!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnSua1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbMaLoai.Text))
            {
                MessageBox.Show("Vui lòng nhập mã loại để sửa.");
                return;
            }


            string updateQuery = "UPDATE Loai SET TenLoai = @TenLoai WHERE MaLoai = @MaLoai";

            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
            {

                updateCommand.Parameters.AddWithValue("@MaLoai", tbMaLoai.Text);
                updateCommand.Parameters.AddWithValue("@TenLoai", tbTenLoai.Text);


                try
                {
                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đã sửa thành công!");
                        loadDataLoai();
                    }
                    else
                    {
                        MessageBox.Show("Mã loại không tồn tại!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Vui lòng nhập mã kích thước cần xóa.");
                return;
            }

            // Thông báo xác nhận
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa mã kích thước này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                string deleteQuery = "DELETE FROM KichThuoc WHERE MaKichThuoc = @MaKichThuoc";

                using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                {
                    deleteCommand.Parameters.AddWithValue("@MaKichThuoc", textBox1.Text);

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
                            loadDataKT();
                        }
                        else
                        {
                            MessageBox.Show("Mã kích thước không tồn tại!");
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


        private void btnTim_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox3.Text.Trim(); // Lấy từ khóa tìm kiếm

            if (string.IsNullOrEmpty(searchTerm))
            {
                MessageBox.Show("Vui lòng nhập mã kích thước cần tìm.");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(str))
                {
                    connection.Open();


                    string query = @"
                        SELECT p.MaHang, p.TenHangHoa,p.SoLuong,c.MaKichThuoc,c.TenKichThuoc
                        FROM DMHangHoa p
                        INNER JOIN KichThuoc c ON p.MaKichThuoc = c.MaKichThuoc
                        WHERE c.MaKichThuoc = @SearchTerm
                    ";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@SearchTerm", searchTerm);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán kết quả tìm kiếm vào DataGridView
                    dataGridViewKT.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
            }
        }

        private void btnThemNhom_Click(object sender, EventArgs e)
        {
            string checkQuery = "SELECT COUNT(*) FROM NhomTranhAnh WHERE MaNhom = @MaNhom";

            using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
            {
                checkCommand.Parameters.AddWithValue("@MaNhom", textBox9.Text);

                int count = (int)checkCommand.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("Mã Nhóm Tranh Ảnh đã tồn tại! Vui lòng nhập mã khác.");
                    return;
                }
            }


            string insertQuery = "INSERT INTO NhomTranhAnh (MaNhom,TenNhom) VALUES (@MaNhom,@TenNhom)";
            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
            {

                insertCommand.Parameters.AddWithValue("@MaNhom", textBox9.Text);
                insertCommand.Parameters.AddWithValue("@TenNhom", textBox10.Text);


                try
                {

                    insertCommand.ExecuteNonQuery();
                    MessageBox.Show("Đã thêm thành công!");


                    loadDataNhom();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnXoaNhom_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox9.Text))
            {
                MessageBox.Show("Vui lòng nhập mã nhóm cần xóa.");
                return;
            }

            // Thông báo xác nhận
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa mã nhóm này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                string deleteQuery = "DELETE FROM NhomTranhAnh WHERE MaNhom = @MaNhom";

                using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                {
                    deleteCommand.Parameters.AddWithValue("@MaNhom", textBox9.Text);

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
                            loadDataNhom();
                        }
                        else
                        {
                            MessageBox.Show("Mã nhóm không tồn tại!");
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

        private void btnSuaNhom_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox9.Text))
            {
                MessageBox.Show("Vui lòng nhập mã nhóm sửa.");
                return;
            }


            string updateQuery = "UPDATE NhomTranhAnh SET TenNhom = @TenNhom WHERE MaNhom = @MaNhom";

            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
            {

                updateCommand.Parameters.AddWithValue("@MaNhom", textBox9.Text);
                updateCommand.Parameters.AddWithValue("@TenNhom", textBox10.Text);


                try
                {
                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đã sửa thành công!");
                        loadDataNhom();
                    }
                    else
                    {
                        MessageBox.Show("Mã nhóm không tồn tại!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnTimNhom_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox12.Text.Trim(); // Lấy từ khóa tìm kiếm

            if (string.IsNullOrEmpty(searchTerm))
            {
                MessageBox.Show("Vui lòng nhập mã nhóm cần tìm.");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(str))
                {
                    connection.Open();


                    string query = @"
                        SELECT p.MaHang, p.TenHangHoa,p.SoLuong,c.MaNhom,c.TenNhom
                        FROM DMHangHoa p
                        INNER JOIN NhomTranhAnh c ON p.MaNhom = c.MaNhom
                        WHERE c.MaNhom = @SearchTerm
                    ";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@SearchTerm", searchTerm);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán kết quả tìm kiếm vào DataGridView
                    dataGridViewNhom.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
            }
        }

        private void dataGridViewKT_AutoSizeColumnsModeChanged(object sender, DataGridViewAutoSizeColumnsModeEventArgs e)
        {
            dataGridViewKT.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void btnThemCL_Click(object sender, EventArgs e)
        {
            string checkQuery = "SELECT COUNT(*) FROM ChatLieu WHERE MaChatLieu = @MaChatLieu";

            using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
            {
                checkCommand.Parameters.AddWithValue("@MaChatLieu", textBox13.Text);

                int count = (int)checkCommand.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("Mã Chất Liệu đã tồn tại! Vui lòng nhập mã khác.");
                    return;
                }
            }


            string insertQuery = "INSERT INTO ChatLieu (MaChatLieu,TenChatLieu) VALUES (@MaChatLieu,@TenChatLieu)";
            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
            {

                insertCommand.Parameters.AddWithValue("@MaChatLieu", textBox13.Text);
                insertCommand.Parameters.AddWithValue("@TenChatLieu", textBox15.Text);


                try
                {

                    insertCommand.ExecuteNonQuery();
                    MessageBox.Show("Đã thêm thành công!");


                    loadDataCL();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnXoaCL_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox13.Text))
            {
                MessageBox.Show("Vui lòng nhập mã chất liệu cần xóa.");
                return;
            }

            // Thông báo xác nhận
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa mã chất liệu này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                string deleteQuery = "DELETE FROM ChatLieu WHERE MaChatLieu = @MaChatLieu";

                using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                {
                    deleteCommand.Parameters.AddWithValue("@MaChatLieu", textBox13.Text);

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
                            loadDataCL();
                        }
                        else
                        {
                            MessageBox.Show("Mã chất liệu không tồn tại!");
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
               
            }
        }

        private void btnSuaCL_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox13.Text))
            {
                MessageBox.Show("Vui lòng nhập mã chất liệu để sửa.");
                return;
            }


            string updateQuery = "UPDATE ChatLieu SET TenChatLieu = @TenChatLieu WHERE MaChatLieu = @MaChatLieu";

            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
            {

                updateCommand.Parameters.AddWithValue("@MaChatLieu", textBox13.Text);
                updateCommand.Parameters.AddWithValue("@TenChatLieu", textBox15.Text);


                try
                {
                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đã sửa thành công!");
                        loadDataCL();
                    }
                    else
                    {
                        MessageBox.Show("Mã chất liệu không tồn tại!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnTimCL_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox14.Text.Trim(); // Lấy từ khóa tìm kiếm

            if (string.IsNullOrEmpty(searchTerm))
            {
                MessageBox.Show("Vui lòng nhập mã loại hoặc tên loại cần tìm.");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(str))
                {
                    connection.Open();


                    string query = @"
                SELECT p.MaHang, p.TenHangHoa,p.SoLuong, c.MaChatLieu,c.TenChatLieu
                FROM DMHangHoa p
                INNER JOIN ChatLieu c ON p.MaChatLieu = c.MaChatLieu
                WHERE c.MaChatLieu = @SearchTerm
            ";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@SearchTerm", searchTerm);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán kết quả tìm kiếm vào DataGridView
                    dataGridViewCL.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
            }
        }

        private void btnThemNSX_Click(object sender, EventArgs e)
        {
            string checkQuery = "SELECT COUNT(*) FROM NoiSanXuat WHERE MaNoiSX = @MaNoiSX";

            using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
            {
                checkCommand.Parameters.AddWithValue("@MaNoiSX", textBox17.Text);

                int count = (int)checkCommand.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("Mã Nơi Sản Xuất đã tồn tại! Vui lòng nhập mã khác.");
                    return;
                }
            }


            string insertQuery = "INSERT INTO NoiSanXuat (MaNoiSX,TenNoiSX) VALUES (@MaNoiSX,@TenNoiSX)";
            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
            {

                insertCommand.Parameters.AddWithValue("@MaNoiSX", textBox17.Text);
                insertCommand.Parameters.AddWithValue("@TenNoiSX", textBox18.Text);


                try
                {

                    insertCommand.ExecuteNonQuery();
                    MessageBox.Show("Đã thêm thành công!");


                    loadDataNSX();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnXoaNSX_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox17.Text))
            {
                MessageBox.Show("Vui lòng nhập mã nơi sản xuất cần xóa.");
                return;
            }

            // Thông báo xác nhận
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa mã nơi sản xuất này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                string deleteQuery = "DELETE FROM NoiSanXuat WHERE MaNoiSX = @MaNoiSX";

                using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                {
                    deleteCommand.Parameters.AddWithValue("@MaNoiSX", textBox17.Text);

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
                            loadDataNSX();
                        }
                        else
                        {
                            MessageBox.Show("Mã NSX không tồn tại!");
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

        private void btnSuaNSX_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox17.Text))
            {
                MessageBox.Show("Vui lòng nhập mã nơi sản xuất để sửa.");
                return;
            }


            string updateQuery = "UPDATE NoiSanXuat SET TenNoiSX = @TenNoiSX WHERE MaNoiSX = @MaNoiSX";

            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
            {

                updateCommand.Parameters.AddWithValue("@MaNoiSX", textBox17.Text);
                updateCommand.Parameters.AddWithValue("@TenNoiSX", textBox18.Text);


                try
                {
                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đã sửa thành công!");
                        loadDataNSX();
                    }
                    else
                    {
                        MessageBox.Show("Mã nơi sản xuất không tồn tại!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnTimNSX_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox19.Text.Trim(); // Lấy từ khóa tìm kiếm

            if (string.IsNullOrEmpty(searchTerm))
            {
                MessageBox.Show("Vui lòng nhập mã NSX cần tìm.");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(str))
                {
                    connection.Open();


                    string query = @"
                SELECT p.MaHang, p.TenHangHoa,p.SoLuong, c.MaNoiSX,c.TenNoiSX 
                FROM DMHangHoa p
                INNER JOIN NoiSanXuat c ON p.MaNoiSX = c.MaNoiSX
                WHERE c.MaNoiSX = @SearchTerm
            ";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@SearchTerm", searchTerm);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán kết quả tìm kiếm vào DataGridView
                    dataGridViewNSX.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
            }
        }

        private void btnThemMau_Click(object sender, EventArgs e)
        {
            string checkQuery = "SELECT COUNT(*) FROM MauSac WHERE MaMau = @MaMau";

            using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
            {
                checkCommand.Parameters.AddWithValue("@MaMau", textBox21.Text);

                int count = (int)checkCommand.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("Mã Màu đã tồn tại! Vui lòng nhập mã khác.");
                    return;
                }
            }


            string insertQuery = "INSERT INTO MauSac (MaMau,TenMau) VALUES (@MaMau,@TenMau)";
            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
            {

                insertCommand.Parameters.AddWithValue("@MaMau", textBox21.Text);
                insertCommand.Parameters.AddWithValue("@TenMau", textBox22.Text);


                try
                {

                    insertCommand.ExecuteNonQuery();
                    MessageBox.Show("Đã thêm thành công!");


                    loadDataMau();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnXoaMau_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox21.Text))
            {
                MessageBox.Show("Vui lòng nhập mã cần xóa.");
                return;
            }

            // Thông báo xác nhận
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                string deleteQuery = "DELETE FROM MauSac WHERE MaMau = @MaMau";

                using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                {
                    deleteCommand.Parameters.AddWithValue("@MaMau", textBox21.Text);

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
                            loadDataMau();
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

        private void btnSuaMau_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox21.Text))
            {
                MessageBox.Show("Vui lòng nhập mã để sửa.");
                return;
            }


            string updateQuery = "UPDATE MauSac SET TenMau = @TenMau WHERE MaMau = @MaMau";

            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
            {

                updateCommand.Parameters.AddWithValue("@MaMau", textBox21.Text);
                updateCommand.Parameters.AddWithValue("@TenMau", textBox22.Text);


                try
                {
                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đã sửa thành công!");
                        loadDataMau();
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

        private void btnTimKiemMau_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox23.Text.Trim(); // Lấy từ khóa tìm kiếm

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
                SELECT p.MaHang, p.TenHangHoa,p.SoLuong, c.MaMau,c.TenMau
                FROM DMHangHoa p
                INNER JOIN MauSac c ON p.MaMau = c.MaMau
                WHERE c.MaMau = @SearchTerm
            ";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@SearchTerm", searchTerm);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán kết quả tìm kiếm vào DataGridView
                    dataGridViewMau.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
            }
        }

        private void btnThemKA_Click(object sender, EventArgs e)
        {
            string checkQuery = "SELECT COUNT(*) FROM KhungAnh WHERE MaKhung = @MaKhung";

            using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
            {
                checkCommand.Parameters.AddWithValue("@MaKhung", textBox25.Text);

                int count = (int)checkCommand.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("Mã đã tồn tại! Vui lòng nhập mã khác.");
                    return;
                }
            }


            string insertQuery = "INSERT INTO KhungAnh (MaKhung,TenKhung) VALUES (@MaKhung,@TenKhung)";
            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
            {

                insertCommand.Parameters.AddWithValue("@MaKhung", textBox25.Text);
                insertCommand.Parameters.AddWithValue("@TenKhung", textBox26.Text);


                try
                {

                    insertCommand.ExecuteNonQuery();
                    MessageBox.Show("Đã thêm thành công!");


                    loadDataKA();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnXoaKA_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox25.Text))
            {
                MessageBox.Show("Vui lòng nhập mã cần xóa.");
                return;
            }

            // Thông báo xác nhận
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                string deleteQuery = "DELETE FROM KhungAnh WHERE MaKhung = @MaKhung";

                using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                {
                    deleteCommand.Parameters.AddWithValue("@MaKhung", textBox25.Text);

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
                            loadDataKA();
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

        private void btnSuaKA_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox25.Text))
            {
                MessageBox.Show("Vui lòng nhập mã để sửa.");
                return;
            }


            string updateQuery = "UPDATE KhungAnh SET TenKhung = @TenKhung WHERE MaKhung = @MaKhung";

            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
            {

                updateCommand.Parameters.AddWithValue("@MaKhung", textBox25.Text);
                updateCommand.Parameters.AddWithValue("@TenKhung", textBox26.Text);


                try
                {
                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đã sửa thành công!");
                        loadDataKA();
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

        private void btnTimKiemKA_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox27.Text.Trim(); // Lấy từ khóa tìm kiếm

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
                SELECT p.MaHang, p.TenHangHoa,p.SoLuong, c.MaKhung ,c.TenKhung
                FROM DMHangHoa p
                INNER JOIN KhungAnh c ON p.MaKhung = c.MaKhung
                WHERE c.MaKhung = @SearchTerm
            ";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@SearchTerm", searchTerm);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán kết quả tìm kiếm vào DataGridView
                    dataGridViewKA.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
            }
        }

        private void btnXoa1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbMaLoai.Text))
            {
                MessageBox.Show("Vui lòng nhập mã cần xóa.");
                return;
            }

            // Thông báo xác nhận
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                string deleteQuery = "DELETE FROM Loai WHERE MaLoai = @MaLoai";

                using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                {
                    deleteCommand.Parameters.AddWithValue("@MaLoai", tbMaLoai.Text);

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
                            loadDataLoai();
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
    }
}
