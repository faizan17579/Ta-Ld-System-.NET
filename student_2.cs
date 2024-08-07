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
    public partial class student_2 : Form
    {
        public student_2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        private bool passchecker()
        {
            //validate password 8 characters long and two special characters
            string password = pass.Text;
            int specialCharCount = 0;
            bool isValid = true;
            foreach (char c in password)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    specialCharCount++;
                }
            }
            if (password.Length < 8 || specialCharCount < 2)
            {
              //  MessageBox.Show("Password must be 8 characters long and contain at least two special characters");
              isValid = false;
            }
            return isValid;

        }

        private void Register_Click(object sender, EventArgs e)
        {
            string em = email.Text;
            string password = pass.Text;
            string pass2= passagain.Text;
            //first check email is present in Student table or not
            //if present then show message that email is already registered
            //if not present then insert email and password in Student table
            if(em=="" || password=="" || pass2=="")
            {
                label7.Text = "Please fill all fields";
                return;
            }

            if (password == pass2 && passchecker())
            {
                label6.Text = "valid";
            }
            else
            {
                if (password != pass2)
                {
                   label6.Text = "unmatched";

                    return;
                }
                else
                {
                    label6.Text = "invalid";
                    return;
                }

            }
            string cnstring = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection cn = new SqlConnection(cnstring))
            {
                cn.Open();

                // Use parameterized query to check the email
                string query = "SELECT * FROM Student WHERE email = @Email";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@Email", em); // Add the email parameter

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())

                {
                    label5.Text="Student Found";
                    int id = Convert.ToInt32(dr["student_id"]);
                    string existingmail= dr["email"].ToString();   
                    dr.Close();
                    string query1 = "insert into Register_student(student_id,email,password) values(@id,@existingemail,@password)";
                    SqlCommand cmd1 = new SqlCommand(query1, cn);
                    string query2 = "select * from Register_student where email=@existingemail";
                    SqlCommand cmd2 = new SqlCommand(query2, cn);
                    cmd2.Parameters.AddWithValue("@existingemail", existingmail);
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    if (dr2.Read())
                    {
                        label5.Text = "Email Already Registered";
                        return;
                    }
                    else
                    {

                        dr2.Close();
                        cmd1.Parameters.AddWithValue("@id", id);
                        cmd1.Parameters.AddWithValue("@existingemail", existingmail);
                        cmd1.Parameters.AddWithValue("@password", password);
                        cmd1.ExecuteNonQuery();
                        label7.Text = "Registered Successfully";
                    }
                }
                else
                {

                  
                }

            }
              




        }

        private void email_TextChanged(object sender, EventArgs e)
        {

        }

        private void pass_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {

            email.Text = "";
            pass.Text = "";
            passagain.Text = "";
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form form = new StudentRegister();
            form.Show();

        }

        private void button2_Click(object sender, EventArgs e)

        {
            this.Hide();
            Form form = new Form1();
            form.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
