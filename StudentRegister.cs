using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Database_Project
{
    public partial class StudentRegister : Form
    {
        public StudentRegister()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Login_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string password = textBox2.Text;
            string cnstring = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
        using (SqlConnection cn = new SqlConnection(cnstring))
            {
            cn.Open();
            using (SqlCommand cmd = new SqlCommand("select * from Register_student where email = @Email and password = @Password", cn))
                {
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                    if (reader.Read())
                        {
                        label3.Text="Login Successful";
                        this.Hide();
                            Student_dashboard dashboard = new Student_dashboard(email);
                            dashboard.Show();
                        }
                    else
                        {
                        label3.Text="Login Failed";
                    }
                }
            }
        }   

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            /*home.FlatStyle = FlatStyle.Flat;
            home.FlatAppearance.BorderSize = 0;*/
            Form form = new Form1();
            form.ShowDialog();
             this.Close();

        }

        private void Register_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form form = new student_2();
            form.Show();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void StudentRegister_Load(object sender, EventArgs e)
        {

        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();

            this.Close();
        }
    }
}
