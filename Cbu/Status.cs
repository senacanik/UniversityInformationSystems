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
	public partial class Status : Form
	{
		public Status()
		{
			InitializeComponent();
		}

		static string conString = "Data Source= DESKTOP-GSM889F\\SQLEXPRESS ;Initial Catalog=cbuSystem; Integrated Security=True";
		SqlConnection baglanti = new SqlConnection(conString);
		public void DataShow(string data)
		{
			SqlDataAdapter da = new SqlDataAdapter(data, baglanti); //veri
			DataSet ds = new DataSet(); // tablo
			da.Fill(ds); // veriyi tabloya atıyorum
			dataGridView1.DataSource = ds.Tables[0];
		}

		private void TextClear()
		{
			textBox1.Text = String.Empty;
		}
		private void Status_Load(object sender, EventArgs e)
		{
			DatagridviewSetting(dataGridView1);
			DataShow("select * from actives");
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				baglanti.Open();
				SqlCommand komut = new SqlCommand("select * from actives where actives like'%" + textBox1.Text + "%'", baglanti);
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

		private void button2_Click(object sender, EventArgs e)
		{
			try
			{
				baglanti.Open();
				SqlCommand komut = new SqlCommand("insert into actives(actives) values(@actives)", baglanti);
				komut.Parameters.AddWithValue("@actives", textBox1.Text.Trim());
				komut.ExecuteNonQuery();
				DataShow("select * from actives");
				baglanti.Close();
				MessageBox.Show(" Kayıt işlemi gerçekleşti.");
				TextClear();
			}
			catch (Exception hata)
			{
				MessageBox.Show("İşlem sırasında hata oluştu." + hata.Message);
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			try
			{
				baglanti.Open();
				SqlCommand komut = new SqlCommand("delete from actives where actives = @actives", baglanti);
				komut.Parameters.AddWithValue("@actives", textBox1.Text);
				komut.ExecuteNonQuery();
				DataShow("select * from actives");
				baglanti.Close();
				MessageBox.Show(" Kayıt işlemi gerçekleşti.");
				TextClear();
			}
			catch (Exception hata)
			{
				MessageBox.Show("İşlem sırasında hata oluştu." + hata.Message);
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			try
			{
				baglanti.Open();
				SqlCommand komut = new SqlCommand("update actives set actives='" + textBox1.Text.Trim() + "' where actives='" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "'", baglanti);//currentrow geçerli satır,cell hücre
				komut.ExecuteNonQuery();
				DataShow("select * from actives");
				baglanti.Close();
				MessageBox.Show(" güncellendi.");
			}
			catch (Exception hata)
			{
				MessageBox.Show("İşlem sırasında hata oluştu." + hata.Message);
			}
		}

		private void button5_Click(object sender, EventArgs e)
		{
			Admin adminopen = new Admin();
			adminopen.Show();
			this.Hide();
		}

		

		public void DatagridviewSetting(DataGridView datagridview)
		{
			datagridview.BorderStyle = BorderStyle.None;
			datagridview.AlternatingRowsDefaultCellStyle.BackColor = Color.Gray;
			datagridview.DefaultCellStyle.SelectionForeColor = Color.Black;
			datagridview.EnableHeadersVisualStyles = false;
			datagridview.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
			datagridview.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
			datagridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
		}

		private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
		{
			int selected = dataGridView1.SelectedCells[0].RowIndex;
			string name = dataGridView1.Rows[selected].Cells[1].Value.ToString();
			textBox1.Text = name;
		}
	}
}
