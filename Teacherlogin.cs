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
    public partial class Teacherlogin : Form
    {
        public Teacherlogin()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Teacherlogin_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String email = textBox1.Text;
            String password = textBox2.Text;
            string cnstring = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";

            if(comboBox1.Text == "Lecturer")
            {
                using (SqlConnection cn = new SqlConnection(cnstring))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("select * from Teacher where email = @Email and password = @Password", cn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                label4.Text = "Login Successful";
                               this.Hide();
                               Lecturerdashboard dashboard = new Lecturerdashboard(email);
                               dashboard.ShowDialog();
                                this.Close();
                            }
                            else
                            {
                                label4.Text = "Login Failed";
                            }
                        }
                    }
                }
            }
            else if(comboBox1.Text== "Lab Instructor")
            {
                using (SqlConnection cn = new SqlConnection(cnstring))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("select * from Labinstructor where email = @Email and password = @Password", cn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                label4.Text = "Login Successful";
                                this.Hide();
                                Labinstructor labinstructor = new Labinstructor(email);
                                labinstructor.Show();
                            }
                            else
                            {
                                label4.Text = "Login Failed";
                            }
                        }
                    }
                }

               
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            string cate=comboBox1.Text;
            this.Hide();
            Forgotpass forgotpass = new Forgotpass(cate);
            forgotpass.ShowDialog();
            this.Close();

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.ShowDialog();
            this.Close();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.ShowDialog();
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";


        }
    }
}
