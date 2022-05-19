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
	public partial class Admin : Form
	{
		public Admin()
		{
			InitializeComponent();
		}
		   
		static string conString = "Data Source= DESKTOP-GSM889F\\SQLEXPRESS ;Initial Catalog=cbuSystem; Integrated Security=True"; 
		SqlConnection baglanti = new SqlConnection(conString);
		/*SqlCommand komut = new SqlCommand();
		SqlDataReader oku;*/
		private void Admin_Load(object sender, EventArgs e)
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

				label2.Text = dr["userName"].ToString() + " " + dr["surname"].ToString();
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
			NewMember newMember = new NewMember();
			newMember.Show();
			this.Hide();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			ListUsers listUsers = new ListUsers();
			listUsers.Show();
			this.Hide();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			ListStudent listStudent = new ListStudent();
			listStudent.Show();
			this.Hide();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			ListLecturer listLecturer= new ListLecturer();
			listLecturer.Show();
			this.Hide();
		}

		private void button5_Click(object sender, EventArgs e)
		{
			ListLessons listLessons = new ListLessons();
			listLessons.Show();
			this.Hide();
		}

		private void button6_Click(object sender, EventArgs e)
		{
			Department department = new Department();
			department.Show();
			this.Hide();
		}

		private void button7_Click(object sender, EventArgs e)
		{
			Rol rol = new Rol();
			rol.Show();
			this.Hide();
		}

		private void button8_Click(object sender, EventArgs e)
		{
			Title title = new Title();
			title.Show();
			this.Hide();
		}

		private void button9_Click(object sender, EventArgs e)
		{
			Status status = new Status();
			status.Show();
			this.Hide();
		}
	}
}
