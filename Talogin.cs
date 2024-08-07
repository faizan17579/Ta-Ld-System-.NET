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
    public partial class Talogin : Form
    {
        public Talogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String email = textBox1.Text;
            String password = textBox2.Text;


            if (email.Equals("") && password.Equals(""))
            {
                label3.Text="Please enter email and password";
                return;

            }
            String Conn ="Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            String Query = "SELECT TA.Ta_id FROM TeacherAssistant TA INNER JOIN Student S ON TA.Ta_id = S.student_id " +
                "INNER JOIN Register_student Re on Re.student_id=S.Student_id WHERE Re.email = @Email AND Re.password = @Password";
            using (SqlConnection con = new SqlConnection(Conn))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("Ta_id"));
                         //   MessageBox.Show("Login Successful");
                            this.Hide();
                            TaAssign form = new TaAssign(id);
                            form.Show();
                            
                          
                           
                           
                        }
                        else
                        {
                            label3.Text = "Invalid email or password";
                        }
                    }
                }
            }
        }

        private void Talogin_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.ShowDialog();
            this.Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.ShowDialog();
            this.Close();
        }
    }
}
