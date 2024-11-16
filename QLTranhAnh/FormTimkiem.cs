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
namespace QLTranhAnh
{
    public partial class FormTimkiem : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = "Data Source=DESKTOP-VT4B3DF\\SQLEXPRESS;Initial Catalog=TranhAnh;User ID=sa;Password = abc123";
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
            command.CommandText = "select * from NhamAnh";
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
            command.CommandText = "select * from NuocSX";
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

    }
}
