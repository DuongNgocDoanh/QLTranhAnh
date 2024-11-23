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
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
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

        public void AddMaHangToDanhMuc(string maHang, string tenHang, int soLuong)
        {
            using (SqlConnection conn = new SqlConnection(str))
            {
                conn.Open();
                string queryB = "INSERT INTO DMHangHoa (MaHang,SoLuong) VALUES (@MaHang,@SoLuong)";
                using (SqlCommand cmdB = new SqlCommand(queryB, conn))
                {
                    cmdB.Parameters.AddWithValue("@MaHang", maHang);
                    cmdB.Parameters.AddWithValue("@SoLuong", soLuong);
                    cmdB.ExecuteNonQuery();
                }
            }
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
            cbMaLoai.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            cbMaKT.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            cbMaNhom.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            cbMaCL.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
            cbMaCD.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();
            cbMaKhung.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();
            cbMaMau.Text = dataGridView1.Rows[i].Cells[8].Value.ToString();
            cbMaNSX.Text = dataGridView1.Rows[i].Cells[9].Value.ToString();
            numericUpDown1.Text = dataGridView1.Rows[i].Cells[10].Value.ToString();
            tbDongianhap.Text = dataGridView1.Rows[i].Cells[11].Value.ToString();
            tbDongiaban.Text = dataGridView1.Rows[i].Cells[12].Value.ToString();
            cbTGBH.Text = dataGridView1.Rows[i].Cells[13].Value.ToString();
            tbGhichu.Text = dataGridView1.Rows[i].Cells[14].Value.ToString();
            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbMahanghoa.Text))
            {
                MessageBox.Show("Vui lòng nhập mã hàng hoá để sửa.");
                return;
            }


            string updateQuery = "UPDATE DMHangHoa SET TenHangHoa = @TenHangHoa, MaLoai = @MaLoai, MaKichThuoc = @MaKichThuoc,MaNhom = @MaNhom,MaChatLieu = @MaChatLieu,MaCongDong = @MaCongDong,MaKhung = @MaKhung,MaMau = @MaMau,MaNoiSX = @MaNoiSX,SoLuong = @SoLuong,DonGiaNhap = @DonGiaNhap,DonGiaBan = @DonGiaBan,ThoiGianBaoHanh = @ThoiGianBaoHanh,GhiChu = @GhiChu,Anh = @Anh WHERE MaHang = @MaHang";

            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
            {
                updateCommand.Parameters.AddWithValue("@MaHang", tbMahanghoa.Text);
                updateCommand.Parameters.AddWithValue("@TenHangHoa", tbTenhanghoa.Text);
                updateCommand.Parameters.AddWithValue("@MaLoai", cbMaLoai.Text);
                updateCommand.Parameters.AddWithValue("@MaKichThuoc", cbMaKT.Text);
                updateCommand.Parameters.AddWithValue("@MaNhom", cbMaNhom.Text);
                updateCommand.Parameters.AddWithValue("@MaChatLieu", cbMaCL.Text);
                updateCommand.Parameters.AddWithValue("@MaCongDong", cbMaCD.Text);
                updateCommand.Parameters.AddWithValue("@MaKhung", cbMaKhung.Text);   
                updateCommand.Parameters.AddWithValue("@MaMau", cbMaMau.Text);
                updateCommand.Parameters.AddWithValue("@MaNoiSX", cbMaNSX.Text);
                updateCommand.Parameters.AddWithValue("@SoLuong", numericUpDown1.Text);
                updateCommand.Parameters.AddWithValue("@DonGiaNhap", decimal.Parse(tbDongianhap.Text));
                updateCommand.Parameters.AddWithValue("@DonGiaBan", decimal.Parse(tbDongiaban.Text));
                updateCommand.Parameters.AddWithValue("@ThoiGianBaoHanh", cbTGBH.Text);
                updateCommand.Parameters.AddWithValue("@GhiChu", tbGhichu.Text);
                if (pictureBox1.Image != null)
                {
                    byte[] imageBytes = ImageToByteArray(pictureBox1.Image);
                    updateCommand.Parameters.AddWithValue("@Anh", imageBytes);
                }
                
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
            string insertQuery = "INSERT INTO DMHangHoa (MaHang, TenHangHoa, MaLoai, MaKichThuoc, MaNhom, MaChatLieu, MaCongDong,MaKhung, MaMau, MaNoiSX, SoLuong, DonGiaNhap, DonGiaBan, ThoiGianBaoHanh, GhiChu,Anh) " +
                "VALUES (@MaHang, @TenHangHoa, @MaLoai, @MaKichThuoc, @MaNhom, @MaChatLieu, @MaCongDong,@MaKhung, @MaMau, @MaNoiSX, @SoLuong, @DonGiaNhap, @DonGiaBan, @ThoiGianBaoHanh,@GhiChu,@Anh)";

            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
            {

                insertCommand.Parameters.AddWithValue("@MaHang", tbMahanghoa.Text);
                insertCommand.Parameters.AddWithValue("@TenHangHoa", tbTenhanghoa.Text);
                insertCommand.Parameters.AddWithValue("@MaLoai", cbMaLoai.Text);
                insertCommand.Parameters.AddWithValue("@MaKichThuoc", cbMaKT.Text);
                insertCommand.Parameters.AddWithValue("@MaNhom", cbMaNhom.Text);
                insertCommand.Parameters.AddWithValue("@MaChatLieu", cbMaCL.Text);
                insertCommand.Parameters.AddWithValue("@MaCongDong", cbMaCD.Text);
                insertCommand.Parameters.AddWithValue("@MaKhung", cbMaKhung.Text);
                insertCommand.Parameters.AddWithValue("@MaMau", cbMaMau.Text);
                insertCommand.Parameters.AddWithValue("@MaNoiSX", cbMaNSX.Text);
                insertCommand.Parameters.AddWithValue("@SoLuong", numericUpDown1.Value);
                insertCommand.Parameters.AddWithValue("@DonGiaNhap", decimal.Parse(tbDongianhap.Text));
                insertCommand.Parameters.AddWithValue("@DonGiaBan", decimal.Parse(tbDongiaban.Text));
                insertCommand.Parameters.AddWithValue("@ThoiGianBaoHanh", cbTGBH.Text);
                insertCommand.Parameters.AddWithValue("@GhiChu", tbGhichu.Text);
                if (pictureBox1.Image != null)
                {
                    byte[] imageBytes = ImageToByteArray(pictureBox1.Image);
                    insertCommand.Parameters.AddWithValue("@Anh", imageBytes);
                }
                else
                {
                    insertCommand.Parameters.AddWithValue("@Anh", DBNull.Value);  // Nếu không chọn ảnh, thêm giá trị null
                }
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
        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat); // Lưu ảnh vào bộ nhớ
                return ms.ToArray(); // Chuyển ảnh thành mảng byte[]
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbMahanghoa.Text))
            {
                MessageBox.Show("Vui lòng nhập mã hàng hoá cần xóa.");
                return;
            }

            
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa hàng hóa này?", "Xác nhận xóa", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
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
                    cbMaLoai.DataSource = dt;
                    cbMaLoai.DisplayMember = "MaLoai";  // Hiển thị tên loại
                    cbMaLoai.ValueMember = "MaLoai";      // Lưu CategoryId (mã loại)
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
                    cbMaKT.DataSource = dt;
                    cbMaKT.DisplayMember = "MaKichThuoc";  // Hiển thị tên loại
                    cbMaKT.ValueMember = "MaKichThuoc";      // Lưu CategoryId (mã loại)
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
                    cbMaNhom.DataSource = dt;
                    cbMaNhom.DisplayMember = "MaNhom";  // Hiển thị tên loại
                    cbMaNhom.ValueMember = "MaNhom";      // Lưu CategoryId (mã loại)
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
                    cbMaCL.DataSource = dt;
                    cbMaCL.DisplayMember = "MaChatLieu";  // Hiển thị tên loại
                    cbMaCL.ValueMember = "MaChatLieu";      // Lưu CategoryId (mã loại)
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
                    string query = "select * from KhungAnh";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán dữ liệu vào ComboBox
                    cbMaKhung.DataSource = dt;
                    cbMaKhung.DisplayMember = "MaKhung";  // Hiển thị tên loại
                    cbMaKhung.ValueMember = "MaKhung";      // Lưu CategoryId (mã loại)
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
                    string query = "select * from CongDong";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán dữ liệu vào ComboBox
                    cbMaCD.DataSource = dt;
                    cbMaCD.DisplayMember = "MaCongDong";  // Hiển thị tên loại
                    cbMaCD.ValueMember = "MaCongDong";      // Lưu CategoryId (mã loại)
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
                    cbMaMau.DataSource = dt;
                    cbMaMau.DisplayMember = "MaMau";  // Hiển thị tên loại
                    cbMaMau.ValueMember = "MaMau";      // Lưu CategoryId (mã loại)
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
                    cbMaNSX.DataSource = dt;
                    cbMaNSX.DisplayMember = "MaNoiSX";  // Hiển thị tên loại
                    cbMaNSX.ValueMember = "MaNoiSX";      // Lưu CategoryId (mã loại)
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
            cbMaLoai.SelectedIndex = -1;
            cbMaKT.SelectedIndex = -1;
            cbMaNhom.SelectedIndex = -1;
            cbMaCL.SelectedIndex = -1;
            cbMaKhung.SelectedIndex = -1;
            cbMaCD.SelectedIndex = -1;
            cbMaMau.SelectedIndex = -1;
            cbMaNSX.SelectedIndex = -1;
            numericUpDown1.Value = numericUpDown1.Minimum;
            tbDongianhap.Clear();
            tbDongiaban.Clear();
            cbTGBH.SelectedIndex = -1;
            tbGhichu.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn Ảnh";
            openFileDialog.Filter = "Image Files(*.gif;*.jpeg;*.bmp;*.wmf;*.png)|*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*.png";
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFileDialog.FileName;
            }
        }

        
    }
}


