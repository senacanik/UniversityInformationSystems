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
	public partial class NewMember : Form
	{
        
        public NewMember()
		{
			InitializeComponent();
		}

		readonly SqlCommand komut = new SqlCommand();
		static readonly string conString = "Data Source= DESKTOP-GSM889F\\SQLEXPRESS ;Initial Catalog=cbuSystem; Integrated Security=True";
		readonly SqlConnection baglanti = new SqlConnection(conString);
		SqlDataReader oku;


		private void NewMember_Load(object sender, EventArgs e)
		{
            Dep();
            Rol();
            Tit();
            Active();
        }
        private void TextClear()
		{
            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
            textBox3.Text = String.Empty;
            textBox4.Text = String.Empty;
            textBox5.Text = String.Empty;
            textBox6.Text = String.Empty;
            textBox7.Text = String.Empty;   
            comboBox1.Text = String.Empty;
            comboBox2.Text = String.Empty;
            comboBox3.Text = String.Empty;
            comboBox4.Text = String.Empty;
        }
        private void Dep()
        {
            comboBox2.Items.Clear();
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "select* from departments";
            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox2.Items.Add(oku[1].ToString());

            }
            baglanti.Close();
        }

        public void Rol()
        {
            comboBox1.Items.Clear();
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "select* from rols";
            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox1.Items.Add(oku[1].ToString());
            }
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
        }

		private void Button2_Click(object sender, EventArgs e)
		{
            Admin adminopen = new Admin();
            adminopen.Show();
            this.Hide();
        }

		private void button1_Click_1(object sender, EventArgs e)
		{
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();
                // Bağlantımızı kontrol ediyoruz, eğer kapalıysa açıyoruz.
                string kayit = "insert into users (mail,password,rolId,depId,titId,userName,surName,tel,intNum,actId,tcNo) values " +
                    "(@mail,@password,@rolId,@depId,@titId,@userName,@surName,@tel,@intNum,@actId,@tcNo)";
                // müşteriler tablomuzun ilgili alanlarına kayıt ekleme işlemini gerçekleştirecek sorgumuz.
                SqlCommand komut = new SqlCommand(kayit, baglanti);
                //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
                komut.Parameters.AddWithValue("@mail", textBox1.Text.Trim());
                komut.Parameters.AddWithValue("@password", textBox2.Text.Trim());
                komut.Parameters.AddWithValue("@rolId", comboBox1.SelectedIndex);
                komut.Parameters.AddWithValue("@depId", comboBox2.SelectedIndex);
                komut.Parameters.AddWithValue("@titId", comboBox3.SelectedIndex);
                komut.Parameters.AddWithValue("@userName", textBox3.Text.Trim());
                komut.Parameters.AddWithValue("@surName", textBox4.Text.Trim());
                komut.Parameters.AddWithValue("@tel", textBox5.Text.Trim());
                komut.Parameters.AddWithValue("@intNum", textBox6.Text.Trim());
                komut.Parameters.AddWithValue("@actId", comboBox4.SelectedIndex);
                komut.Parameters.AddWithValue("@tcNo", textBox7.Text.Trim());
                //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
                komut.ExecuteNonQuery();
                //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
                baglanti.Close();
                MessageBox.Show(" Kayıt işlemi gerçekleşti.");
                TextClear();
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem sırasında hata oluştu." + hata.Message);
            }
        }
	}
}
