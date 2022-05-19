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
using System.IO;

namespace Cbu
{
	public partial class ListLecturer : Form
	{
		public ListLecturer()
		{
			InitializeComponent();
		}

		static string conString = "Data Source= DESKTOP-GSM889F\\SQLEXPRESS ;Initial Catalog=cbuSystem; Integrated Security=True";
		SqlConnection baglanti = new SqlConnection(conString);
		SqlCommand komut = new SqlCommand();
		SqlDataReader oku;
		private void Button1_Click(object sender, EventArgs e)
		{
			Admin adminopen = new Admin();
			adminopen.Show();
			this.Hide();
		}

		private void Button2_Click(object sender, EventArgs e)
		{
			Rol();
			Dep();
			Tit();
			Active();

			string kayit = "SELECT * FROM users " +
				"LEFT OUTER JOIN rols ON users.rolId = rols.id " +
				"LEFT OUTER JOIN departments ON users.depId = departments.id " +
				"LEFT OUTER JOIN titles ON users.titId = titles.id " +
				"LEFT OUTER JOIN actives ON users.actId = actives.id " +
				"WHERE tcNo=@tcNo";
			//tcNo parametresine bağlı olarak users bilgilerini çeken sql kodu
			baglanti.Open();
			SqlCommand komut = new SqlCommand(kayit, baglanti);
			komut.Parameters.AddWithValue("@tcNo", textBox1.Text);
			//tcNo parametremize textbox'dan girilen değeri aktarıyoruz.
			SqlDataReader dr = komut.ExecuteReader();
			//SqlDataAdapter da = new SqlDataAdapter(komut);
			if (dr.Read()) //Sadece tek bir kayıt döndürüleceği için while kullanmadım.
			{
				dr.Close();
				komut.Connection = baglanti;
				komut.CommandText = "SELECT * FROM users " +
				"LEFT OUTER JOIN rols ON users.rolId = rols.id " +
				"LEFT OUTER JOIN departments ON users.depId = departments.id " +
				"LEFT OUTER JOIN titles ON users.titId = titles.id " +
				"LEFT OUTER JOIN actives ON users.actId = actives.id " +
				"WHERE tcNo='" + textBox1.Text + "' AND titId!=5 ";

				dr = komut.ExecuteReader();
				if (dr.Read())
				{
					textBox1.Text = dr["tcNo"].ToString();
					textBox2.Text = dr["mail"].ToString();
					comboBox1.Text = dr["rolName"].ToString();
					comboBox2.Text = dr["depName"].ToString();
					comboBox3.Text = dr["titName"].ToString();
					textBox3.Text = dr["userName"].ToString();
					textBox4.Text = dr["surname"].ToString();
					textBox5.Text = dr["tel"].ToString();
					textBox6.Text = dr["intNum"].ToString();
					comboBox4.Text = dr["actives"].ToString();
					//Datareader ile okunan verileri form kontrollerine aktardık.
					MessageBox.Show("tebrikler Öğretim Görevlisi girişi başarılı ");
				}
				else
				{
					MessageBox.Show("Bu kimlik numarasına ait Öğretim Görevlisi kayıdı bulunmamaktadır");
				}
				baglanti.Close();
			}
			else
			{
				baglanti.Close();
			}

		}
		private void Button3_Click(object sender, EventArgs e)
		{
			try
			{
				baglanti.Open();
				string kayit = "update users set mail=@mail,rolId=@rolId,depId=@depId,titId=@titId,userName=@userName,surname=@surname ,tel=@tel ,intNum=@intNum ,actId=@actId where tcNo=@tcNo";
				// users tablomuzun ilgili alanlarını değiştirecek olan güncelleme sorgusu.
				SqlCommand komut = new SqlCommand(kayit, baglanti);
				//Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
				komut.Parameters.AddWithValue("@mail", textBox2.Text);
				komut.Parameters.AddWithValue("@rolId", comboBox1.SelectedIndex);
				komut.Parameters.AddWithValue("@depId", comboBox2.SelectedIndex);
				komut.Parameters.AddWithValue("@titId", comboBox3.SelectedIndex);
				komut.Parameters.AddWithValue("@userName", textBox3.Text);
				komut.Parameters.AddWithValue("@surname", textBox4.Text);
				komut.Parameters.AddWithValue("@tel", textBox5.Text);
				komut.Parameters.AddWithValue("@intNum", textBox6.Text);
				komut.Parameters.AddWithValue("@actId", comboBox4.SelectedIndex);
				komut.Parameters.AddWithValue("@tcNo", textBox1.Text);

				//Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
				komut.ExecuteNonQuery();
				//Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
				baglanti.Close();
				MessageBox.Show("Öğretim Görevlisi Bilgileri Güncellendi.");
				TextClear();
			}

			catch (Exception hata)
			{
				MessageBox.Show("İşlem sırasında hata oluştu." + hata.Message);
			}
		}
		public void Rol()
		{
			baglanti.Open();
			komut.Connection = baglanti;
			komut.CommandText = "select* from rols";
			oku = komut.ExecuteReader();
			while (oku.Read())
			{
				comboBox1.Items.Add(oku[1].ToString());
			}
			oku.Close();
			baglanti.Close();
		}

		public void Dep()
		{
			baglanti.Open();
			komut.Connection = baglanti;
			komut.CommandText = "select* from departments";
			oku = komut.ExecuteReader();
			while (oku.Read())
			{
				comboBox2.Items.Add(oku[1].ToString());
			}
			oku.Close();
			baglanti.Close();
		}

		private void Tit()
		{
			comboBox3.Items.Clear();
			baglanti.Open();
			komut.Connection = baglanti;
			komut.CommandText = "select* from titles";
			oku = komut.ExecuteReader();

			while (oku.Read())
			{
				comboBox3.Items.Add(oku[1].ToString());
			}
			baglanti.Close();
			oku.Close();
		}

		private void Active()
		{
			comboBox4.Items.Clear();
			baglanti.Open();
			komut.Connection = baglanti;
			komut.CommandText = "select* from actives";
			oku = komut.ExecuteReader();

			while (oku.Read())
			{
				comboBox4.Items.Add(oku[1].ToString());
			}
			baglanti.Close();
			oku.Close();
		}

		private void TextClear()
		{
			textBox1.Text = String.Empty;
			textBox2.Text = String.Empty;
			textBox3.Text = String.Empty;
			textBox4.Text = String.Empty;
			textBox5.Text = String.Empty;
			textBox6.Text = String.Empty;
			comboBox1.Text = String.Empty;
			comboBox2.Text = String.Empty;
			comboBox3.Text = String.Empty;
			comboBox4.Text = String.Empty;
		}

		private void Button4_Click(object sender, EventArgs e)
		{
			TextClear();
		}

		private void Button5_Click(object sender, EventArgs e)
		{
			baglanti.Open();
			string sorgu = "SELECT * from users where tcNo=@tcNo";
			SqlCommand komut = new SqlCommand(sorgu, baglanti);
			komut.Parameters.AddWithValue("@tcNo", textBox1.Text);
			SqlDataAdapter da = new SqlDataAdapter(komut);
			SqlDataReader dr = komut.ExecuteReader();
			if (dr.Read())
			{
				textBox1.Text = dr["tcNo"].ToString();
				dr.Close();
				DialogResult onay = MessageBox.Show("Öğretim Görevlisini silmek istediğinize emin misiniz ?", "Silme onayı", MessageBoxButtons.YesNo);
				if (DialogResult.Yes == onay)
				{
					string silSorgu = "DELETE from users where tcNo=@tcNo";

					SqlCommand silKomut = new SqlCommand(silSorgu, baglanti);
					silKomut.Parameters.AddWithValue("@tcNo", textBox1.Text);
					silKomut.ExecuteNonQuery();
					MessageBox.Show("Öğretim Görevlisi Kayıt Silindi..");
					TextClear();
				}
				else
				{
					MessageBox.Show("Öğretim Görevlisi Kayıdı Bulunamadı..");
					baglanti.Close();
				}

			}
		}

		private void ListLecturer_Load(object sender, EventArgs e)
		{

		}
	}
}
