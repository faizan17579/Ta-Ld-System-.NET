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
    public partial class Commiteelogin : Form
    {
        public Commiteelogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // login comittee
            String email = textBox1.Text;
            String password = textBox2.Text;
            if(email.Equals("") && password.Equals(""))
            {
                label3.Text = "Please enter email and password";
                return;
            }
            String Conn = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            String Query = "SELECT * FROM Commitee WHERE email = @Email AND password = @Password";
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
                            this.Hide();

                            Commitee_Dashboard form = new Commitee_Dashboard();
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
