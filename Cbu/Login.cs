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
using System.Data.Sql;

namespace Cbu
{ 
	public partial class Login : Form
	{

		public Login()
		{
			InitializeComponent();
		}

		SqlConnection con; //sqlconnection türünce con değişkeni
		SqlDataReader dr; // sqldatareader türünde dr değişkeni	
		SqlCommand com; // sqlcommand türünde com değişkeni

		
		private void Button1_Click(object sender, EventArgs e)
		{
			con = new SqlConnection("Data Source= DESKTOP-GSM889F\\SQLEXPRESS ;Initial Catalog=cbuSystem; Integrated Security=True");
			// yeni bağlantı içinde (data kaynağımı) veritabanımı tanımlıyorum
			com = new SqlCommand(); // yeni bir sqlcommand nesnesi oluşturuyorum
			con.Open(); // bağlantımı açtım
			com.Connection = con;
			com.CommandText = "Select*From users where mail=@mail And password=@pass"; // sorgumu yazıyorum
			com.Parameters.AddWithValue("@mail",textBox1.Text); // textlerimden gelen verileri sorguma parametre olarak veriyorum
			com.Parameters.AddWithValue("@pass", textBox2.Text);
			dr = com.ExecuteReader(); // sorgumu yürüt ve dataReader'a at
			if (dr.Read()) // dataReader'ı oku
			{
				Class1.mail = textBox1.Text; // kullanıcının mailini class1 sınıfımın içindeki mail propertisinde tut.
				dr.Close(); // Okunan sorguyu kapat 
				com.CommandText = "Select*From users where mail = '"+textBox1.Text+"' AND rolId = 0 "; // sorgu rolId=0'a eşit olan şartı
				dr = com.ExecuteReader(); // sorgu çalıştır
				if (dr.Read())  // dr içindeki sorguyu oku
				{
					MessageBox.Show("tebrikler giriş başarılı admin"); 
					//Şart sağlandıysa başarılı mesaj ver 
					Admin adminopen = new Admin(); // admin türünde adminopen nesnesi oluşturuyorum
					adminopen.Show(); // adminopen nesnesini (Admin form) göster
					this.Hide(); // bu sayfayı gizliyor
				}
				dr.Close(); // okunan sorgu kapat
				com.CommandText = "Select*From users where mail = '"+ textBox1.Text+"' AND rolId = 1 ";// rolId 1 e eşit olan şart
				dr = com.ExecuteReader(); // sorgu çalıştır
				if (dr.Read()) // datareader içindeki sorguyu oku
				{
					
					MessageBox.Show("tebrikler giriş başarılı user"); //başarılı user mesaj ver
					User studentopen = new User();  // user türünde studentopen nesnesi oluşturuyorum
					studentopen.Show(); // studentopen nesnemi gösteriyorum 
					this.Hide(); // bu sayfayı gizliyorum
				}
				con.Close(); // bağlantı kapatıyorum
			}
			else
			{
				MessageBox.Show("Hatalı kullanıcı adı veya şifre"); // şartlar sağlanmadıysa uyarı mesajı
				con.Close(); // bağlantı kapat
			}
		}
		private void Form1_Load(object sender, EventArgs e)
		{

		}

		
	}
}
