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


namespace Cbu
{
	public partial class ListUsers : Form
	{
		public ListUsers()
		{
			InitializeComponent();
		}
		static string conString = "Data Source= DESKTOP-GSM889F\\SQLEXPRESS ;Initial Catalog=cbuSystem; Integrated Security=True";
		SqlConnection baglanti = new SqlConnection(conString);
		private void Button1_Click(object sender, EventArgs e)
		{
			Admin adminopen = new Admin();
			adminopen.Show();
			this.Hide();
		}

		private void button2_Click_1(object sender, EventArgs e)
		{
			try
			{
				baglanti.Open();
				SqlCommand komut = new SqlCommand("select * from users where userName like'%" + textBox1.Text + "%'", baglanti);
				SqlDataAdapter da = new SqlDataAdapter(komut);
				DataSet ds = new DataSet();
				da.Fill(ds);
				dataGridView1.DataSource = ds.Tables[0];
				baglanti.Close();
				//MessageBox.Show(" bulundu.");
			}
			catch (Exception hata)
			{
				MessageBox.Show("İşlem sırasında hata oluştu." + hata.Message);
			}
		}

		private void ListUsers_Load(object sender, EventArgs e)
		{
			DatagridviewSetting(dataGridView1);
			string kayit = "SELECT u.mail, u.password, rols.rolName, d.depName, titles.titName, u.userName, u.surname,u.tel, u.intNum, actives.actives, u.tcNo " +
				"FROM users AS u " +
				"LEFT OUTER JOIN rols ON u.rolId = rols.id " +
				"LEFT OUTER JOIN departments AS d ON u.depId = d.id " +
				"LEFT OUTER JOIN titles ON u.titId = titles.id " +
				"LEFT OUTER JOIN actives ON u.actId = actives.id ";
				DataShow(kayit);
		}
		public void DataShow(string data)
		{
			SqlDataAdapter da = new SqlDataAdapter(data, baglanti); //veri
			DataSet ds = new DataSet(); // tablo
			da.Fill(ds); // veriyi tabloya atıyorum
			dataGridView1.DataSource = ds.Tables[0];
		}

		private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			int selected = dataGridView1.SelectedCells[0].RowIndex;
			string name = dataGridView1.Rows[selected].Cells[1].Value.ToString();
			textBox1.Text = name;
		}
		public void DatagridviewSetting(DataGridView datagridview)
		{
			datagridview.RowHeadersVisible = false;
			datagridview.BorderStyle = BorderStyle.None;
			datagridview.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
			datagridview.DefaultCellStyle.SelectionBackColor = Color.FromArgb(45, 45, 45);
			datagridview.DefaultCellStyle.SelectionForeColor = Color.Black;
			datagridview.EnableHeadersVisualStyles = false;
			datagridview.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
			datagridview.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
			datagridview.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
			datagridview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			datagridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
		}
	}
}
