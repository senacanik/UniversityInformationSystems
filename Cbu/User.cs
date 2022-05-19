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
	public partial class User : Form
	{
		public User()
		{
			InitializeComponent();
		}
		static string conString = "Data Source= DESKTOP-GSM889F\\SQLEXPRESS ;Initial Catalog=cbuSystem; Integrated Security=True";
		SqlConnection baglanti = new SqlConnection(conString);
		

		private void User_Load(object sender, EventArgs e)
		{
			label7.Text = Class1.mail;
			string kayit = "SELECT * FROM users " +
			"LEFT OUTER JOIN rols ON users.rolId = rols.id " +
			"LEFT OUTER JOIN departments ON users.depId = departments.id " +
			"LEFT OUTER JOIN titles ON users.titId = titles.id " +
			"LEFT OUTER JOIN actives ON users.actId = actives.id " +
			"WHERE mail=@mail";
			baglanti.Open();
			SqlCommand komut = new SqlCommand(kayit, baglanti);
			komut.Parameters.AddWithValue("@mail", label7.Text);
			SqlDataReader dr = komut.ExecuteReader();
			if (dr.Read())
			{

				label2.Text = dr["userName"].ToString() +" "+ dr["surname"].ToString();
				label4.Text = dr["depName"].ToString();
				label5.Text = dr["titName"].ToString();
				label6.Text = dr["intNum"].ToString();
				label7.Text = dr["mail"].ToString();
				label8.Text = dr["tel"].ToString();
			}
			baglanti.Close();
		}

		private void Button1_Click_1(object sender, EventArgs e)
		{
			UserSettings usersettings = new UserSettings();
			usersettings.Show();
			this.Hide();
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			UserLessons userlessons = new UserLessons();
			userlessons.Show();
			this.Hide();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			UserTranscript usertranscript = new UserTranscript();
			usertranscript.Show();
			this.Hide();

		}
	}
}
