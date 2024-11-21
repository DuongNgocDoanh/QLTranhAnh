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
        DataTable tableCTHD = new DataTable();
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
        void loadCTHD()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from ChiTietHoaDonBan";
            adapter.SelectCommand = command;
            tableCTHD.Clear();
            adapter.Fill(tableCTHD);
            dataGridView4.DataSource = tableCTHD;
        }

        public FormKhachHang()
        {
            InitializeComponent();
        }

        private void btnThemKH_Click(object sender, EventArgs e)
        {

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
            else if (tabControl1.SelectedTab == tabPage4)
            {
                loadCTHD();
            }
        }

        private void FormKhachHang_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            loadDataKH();
        }
    }
}
