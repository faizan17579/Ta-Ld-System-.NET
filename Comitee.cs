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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Database_Project
{
    public partial class Comitee : Form
    {
        public Comitee()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
               string.IsNullOrWhiteSpace(textBox2.Text) ||
               string.IsNullOrWhiteSpace(textBox3.Text) ||
               string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }
            String firstName = textBox1.Text;
            String lastName = textBox2.Text;
            String email = textBox3.Text;
            String pass = textBox4.Text;
        String conn= "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";

            string query = "SELECT COUNT(*) FROM Commitee WHERE first_name = @FirstName AND last_name = @LastName AND email = @Email";

            using (SqlConnection connection = new SqlConnection(conn))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@Email", email);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    if(count>0)
                    {
                        MessageBox.Show("This committee member already exists.");
                        return;
                    }
                    else
                    {
                        string query2 = "INSERT INTO Commitee (first_name, last_name, email, password) VALUES (@FirstName, @LastName, @Email, @Password)";
                        using (SqlCommand command2 = new SqlCommand(query2, connection))
                        {
                            command2.Parameters.AddWithValue("@FirstName", firstName);
                            command2.Parameters.AddWithValue("@LastName", lastName);
                            command2.Parameters.AddWithValue("@Email", email);
                            command2.Parameters.AddWithValue("@Password", pass);
                            command2.ExecuteNonQuery();
                            MessageBox.Show("Committee member added successfully.");
                        }
                    }



                }
            }


                }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_dashboard admin_Dashboard = new Admin_dashboard();
            admin_Dashboard.ShowDialog();
            this.Close();

        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Teacher teacher = new Teacher();
            teacher.ShowDialog();
            this.Close();


        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Techerassignment teacherAssignment = new Techerassignment();
            teacherAssignment.ShowDialog();
            this.Close();

        }

        private void label10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Enrollment enrollment = new Enrollment();
            enrollment.ShowDialog();
            this.Close();
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_dashboard admin_Dashboard
                = new Admin_dashboard();
            admin_Dashboard.ShowDialog();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Comitee_Load(object sender, EventArgs e)
        {

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
