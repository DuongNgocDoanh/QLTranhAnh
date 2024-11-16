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
        string str = "Data Source=DESKTOP-VT4B3DF\\SQLEXPRESS;Initial Catalog=QuanLyTranhAnh1;User ID=sa;Password = abc123";
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
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            textBox12.Clear();
            textBox13.Clear();
            textBox14.Clear();
            textBox15.Clear();
            textBox16.Clear();
            textBox17.Clear();
            textBox18.Clear();
            textBox19.Clear();
            textBox20.Clear();
            textBox21.Clear();
            textBox22.Clear();
            textBox23.Clear();
            textBox24.Clear();
            textBox25.Clear();
            textBox26.Clear();
            textBox27.Clear();
            textBox28.Clear();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
