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
using System.Runtime.InteropServices;

namespace 商店管理系統
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hank7\OneDrive\桌面\商店管理系統\store.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd;
        SqlDataAdapter adapter;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            displayStoreData();
        }
        private void displayStoreData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapter = new SqlDataAdapter("select * , (income - cost) as profit from store;", con);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "商店編號";
            dataGridView1.Columns[1].HeaderText = "商店名稱";
            dataGridView1.Columns[2].HeaderText = "店長";
            dataGridView1.Columns[3].HeaderText = "營收";
            dataGridView1.Columns[4].HeaderText = "營業成本";
            dataGridView1.Columns[5].HeaderText = "淨利";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("insert into store(Id,name,manager,income,cost) values(@Id,@name,@manager,@income,@cost)", con);
            int id;
            if(dataGridView1.RowCount <= 2)
            {
                id = 1;
            }
            else
            {
                id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.RowCount - 3].Cells[0].Value) + 1;
            }
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@name", dataGridView1.CurrentRow.Cells[1].Value.ToString());
            cmd.Parameters.AddWithValue("@manager", dataGridView1.CurrentRow.Cells[2].Value.ToString());
            cmd.Parameters.AddWithValue("@income", Convert.ToInt32(dataGridView1.CurrentRow.Cells[3].Value));
            cmd.Parameters.AddWithValue("@cost", Convert.ToInt32(dataGridView1.CurrentRow.Cells[4].Value));
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("新增成功");
            displayStoreData();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("delete store where Id=@id", con);
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(dataGridView1.CurrentCell.Value));
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("刪除成功");
            displayStoreData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            if (dataGridView1.CurrentCell.ColumnIndex == 1)
            {
                cmd = new SqlCommand("update store set name = @name where Id=@id", con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
                cmd.Parameters.AddWithValue("@name", dataGridView1.CurrentRow.Cells[1].Value.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("更新成功");
                displayStoreData();
            }
            if (dataGridView1.CurrentCell.ColumnIndex == 2)
            {
                cmd = new SqlCommand("update store set manager = @manager where Id=@id", con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
                cmd.Parameters.AddWithValue("@manager", dataGridView1.CurrentRow.Cells[2].Value.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("更新成功");
                displayStoreData();
            }
            if (dataGridView1.CurrentCell.ColumnIndex == 3)
            {
                cmd = new SqlCommand("update store set income = @income where Id=@id", con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
                cmd.Parameters.AddWithValue("@income", Convert.ToInt32(dataGridView1.CurrentRow.Cells[3].Value));
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("更新成功");
                displayStoreData();
            }
            if (dataGridView1.CurrentCell.ColumnIndex == 4)
            {
                cmd = new SqlCommand("update store set cost = @cost where Id=@id", con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
                cmd.Parameters.AddWithValue("@cost", Convert.ToInt32(dataGridView1.CurrentRow.Cells[4].Value));
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("更新成功");
                displayStoreData();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
