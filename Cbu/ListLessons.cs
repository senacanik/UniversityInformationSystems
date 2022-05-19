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
	public partial class ListLessons : Form
	{
		static string conString = "Data Source= DESKTOP-GSM889F\\SQLEXPRESS ;Initial Catalog=cbuSystem; Integrated Security=True";
		SqlConnection baglanti = new SqlConnection(conString);
		SqlCommand komut = new SqlCommand();
		SqlDataReader oku;
		public ListLessons()
		{
			InitializeComponent();
		}
		public void DataShow(string data)
		{
			SqlDataAdapter da = new SqlDataAdapter(data, baglanti); //veri
			DataSet ds = new DataSet(); // tablo
			da.Fill(ds); // veriyi tabloya atıyorum
			dataGridView1.DataSource = ds.Tables[0];
		}
		private void button1_Click(object sender, EventArgs e)
		{
			Admin adminopen = new Admin();
			adminopen.Show();
			this.Hide();
		}
		private void Dep()
		{
			baglanti.Open();
			komut.Connection = baglanti;
			komut.CommandText = "select depName from departments";
			oku = komut.ExecuteReader();
			while (oku.Read())
			{
				comboBox1.Items.Add(oku[0].ToString());

			}
			baglanti.Close();
		}

		private void TextClear()
		{
			textBox1.Text = String.Empty;
			textBox2.Text = String.Empty;
			textBox3.Text = String.Empty;
			textBox4.Text = String.Empty;
			comboBox1.Text = String.Empty;
		}
		private void button3_Click(object sender, EventArgs e)
		{
			try
			{
				baglanti.Open();
				SqlCommand komut = new SqlCommand("insert into lessons(depId,number,lesName,akts,teacher) values(@depId,@number,@lesName,@akts,@teacher)", baglanti);
				komut.Parameters.AddWithValue("@depId", comboBox1.SelectedIndex);
				komut.Parameters.AddWithValue("@number", textBox1.Text.Trim());
				komut.Parameters.AddWithValue("@lesName", textBox2.Text.Trim());
				komut.Parameters.AddWithValue("@akts", textBox3.Text.Trim());
				komut.Parameters.AddWithValue("@teacher", textBox4.Text.Trim());
				komut.ExecuteNonQuery();
				DataShow("select * from lessons");
				baglanti.Close();
				MessageBox.Show(" Kayıt işlemi gerçekleşti.");
				TextClear();
				
			}
			catch (Exception hata)
			{

				MessageBox.Show("İşlem sırasında hata oluştu." + hata.Message);
			}
		}

		private void ListLessons_Load(object sender, EventArgs e)
		{
			Dep();
			DatagridviewSetting(dataGridView1);
			DataShow("select departments.depName, lessons.number, lessons.lesName, lessons.akts, lessons.teacher from lessons LEFT OUTER JOIN departments ON lessons.depId=departments.id");
		}
		public void DatagridviewSetting(DataGridView datagridview)
		{
			datagridview.RowHeadersVisible = false;
			
			datagridview.BorderStyle = BorderStyle.None;
			datagridview.AlternatingRowsDefaultCellStyle.BackColor = Color.Gray;
			datagridview.DefaultCellStyle.SelectionForeColor = Color.Black;
			datagridview.EnableHeadersVisualStyles = false;
			datagridview.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
			datagridview.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
			datagridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
		}
		private void button4_Click(object sender, EventArgs e)
		{
			try
			{
				baglanti.Open();
				SqlCommand komut = new SqlCommand("delete from lessons where lesName = @lesName", baglanti);
				komut.Parameters.AddWithValue("@lesName", textBox2.Text);
				komut.ExecuteNonQuery();
				DataShow("select * from lessons");
				baglanti.Close();
				MessageBox.Show(" Kayıt Silindi.");
				TextClear();
			}
			catch (Exception hata)
			{
				MessageBox.Show("İşlem sırasında hata oluştu." + hata.Message);
			}
		}

		private void groupBox1_Enter(object sender, EventArgs e)
		{

		}

		private void button5_Click(object sender, EventArgs e)
		{
			try
			{
				baglanti.Open();
				SqlCommand komut = new SqlCommand("select * from lessons where lesName like'%" + textBox5.Text + "%'", baglanti);
				SqlDataAdapter da = new SqlDataAdapter(komut);
				DataSet ds = new DataSet();
				da.Fill(ds);
				dataGridView1.DataSource = ds.Tables[0];
				baglanti.Close();
				
			}
			catch (Exception hata)
			{
				MessageBox.Show("İşlem sırasında hata oluştu." + hata.Message);
			}

		}


		private void button6_Click(object sender, EventArgs e)
		{
			try
			{	
				baglanti.Open();
				string depId="";
				string kayit = "SELECT id FROM departments WHERE depName='" +comboBox1.SelectedItem +"'";
				SqlCommand komut = new SqlCommand(kayit, baglanti);
				oku = komut.ExecuteReader();
				if (oku.Read())
				{
					depId = oku["id"].ToString();
				}
				oku.Close();
				string number = dataGridView1.CurrentRow.Cells[1].Value.ToString();
				kayit = "update lessons set depId=@depId, number=@number,lesName=@lesName,akts=@akts,teacher=@teacher where number=@no";

				// users tablomuzun ilgili alanlarını değiştirecek olan güncelleme sorgusu.
				komut.CommandText = kayit;
				//Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
				komut.Parameters.AddWithValue("@depId", depId.Trim());
				komut.Parameters.AddWithValue("@number", textBox1.Text.Trim());
				komut.Parameters.AddWithValue("@lesName", textBox2.Text.Trim());
				komut.Parameters.AddWithValue("@akts", textBox3.Text.Trim());
				komut.Parameters.AddWithValue("@teacher", textBox4.Text.Trim());
				komut.Parameters.AddWithValue("@no", number.Trim()); 
				//komut.CommandText = "SELECT * FROM lessons LEFT OUTER JOIN departments ON lessons.depId = departments.id WHERE number='" + textBox1.Text + "'";
				komut.ExecuteNonQuery();
				DataShow("select departments.depName, lessons.number, lessons.lesName, lessons.akts, lessons.teacher from lessons LEFT OUTER JOIN departments ON lessons.depId=departments.id");

				baglanti.Close();
				MessageBox.Show("Ders Bilgileri Güncellendi.");
				TextClear();
			}

			catch (Exception hata)
			{
				MessageBox.Show("İşlem sırasında hata oluştu." + hata.Message);
			}
		}

		private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			int selected = dataGridView1.SelectedCells[0].RowIndex;
			string dep = dataGridView1.Rows[selected].Cells[0].Value.ToString();
			string number = dataGridView1.Rows[selected].Cells[1].Value.ToString();
			string name = dataGridView1.Rows[selected].Cells[2].Value.ToString();
			string akts = dataGridView1.Rows[selected].Cells[3].Value.ToString();
			string teacher = dataGridView1.Rows[selected].Cells[4].Value.ToString();
			comboBox1.Text = dep;
			textBox1.Text = number;
			textBox2.Text = name;
			textBox3.Text = akts;
			textBox4.Text = teacher;
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void textBox2_TextChanged(object sender, EventArgs e)
		{

		}
	}
}
