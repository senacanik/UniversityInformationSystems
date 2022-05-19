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
	public partial class UserSettings : Form
	{
		public UserSettings()
		{
			InitializeComponent();
		}
		static string conString = "Data Source= DESKTOP-GSM889F\\SQLEXPRESS ;Initial Catalog=cbuSystem; Integrated Security=True";
		SqlConnection baglanti = new SqlConnection(conString);
		SqlCommand komut = new SqlCommand();

		private void Button1_Click(object sender, EventArgs e)
		{
			User u = new User();
			u.Show();
			this.Hide();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			try
			{
				baglanti.Open();
				string kayit = "update users set mail=@mail, password=@password  where mail='" + Class1.mail + "'";
				// users tablomuzun ilgili alanlarını değiştirecek olan güncelleme sorgusu.
				SqlCommand komut = new SqlCommand(kayit, baglanti);
				//Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
				komut.Parameters.AddWithValue("@mail", textBox2.Text.Trim());
				komut.Parameters.AddWithValue("@tel", textBox3.Text.Trim());
				komut.Parameters.AddWithValue("@password", textBox4.Text.Trim());
				//Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
				komut.ExecuteNonQuery();
				//Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
				baglanti.Close();
				MessageBox.Show("Bilgileriniz Güncellendi. Tekrar giriş yapınız");
			
			}

			catch (Exception hata)
			{
				MessageBox.Show("İşlem sırasında hata oluştu." + hata.Message);
			}
		}

		private void UserSettings_Load(object sender, EventArgs e)
		{
			string kayit = "SELECT u.mail, u.tel, u.password FROM users AS u WHERE mail='"+ Class1.mail +"'";
			//mail parametresine bağlı olarak user bilgilerini çeken sql kodu
			baglanti.Open();
			SqlCommand komut = new SqlCommand(kayit, baglanti);
			komut.Parameters.AddWithValue("@mail", Class1.mail);
			//mail parametremize session maildeki değeri aktarıyoruz.
			SqlDataReader dr = komut.ExecuteReader();
			//SqlDataAdapter da = new SqlDataAdapter(komut);

			if (dr.Read()) //Sadece tek bir kayıt döndürüleceği için while kullanmadım.
			{
				dr.Close();
				komut.Connection = baglanti;
				komut.CommandText = "SELECT * FROM users WHERE mail='" + Class1.mail + "'";

				dr = komut.ExecuteReader();
				if (dr.Read())
				{
					textBox2.Text = dr["mail"].ToString();
					textBox3.Text = dr["tel"].ToString();
					textBox4.Text = dr["password"].ToString();
					//Datareader ile okunan verileri form kontrollerine aktardık.
				}
				else
				{
					MessageBox.Show("hata");
				}
				baglanti.Close();
			}
			else
			{
				baglanti.Close();
			}
		}

		private void groupBox1_Enter(object sender, EventArgs e)
		{

		}
	}
}
