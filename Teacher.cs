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
    public partial class Teacher : Form
    {
        public Teacher()
        {
            InitializeComponent();
        }
        private bool passchecker()
        {
            //validate password 8 characters long and two special characters
            string password = textBox4.Text;
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

        private void button1_Click(object sender, EventArgs e)
        {
            string first_name = textBox1.Text;
            string last_name = textBox2.Text;
            string email = textBox3.Text;
            string password = textBox4.Text;
            if(first_name=="" || last_name=="" || email=="" || password=="")
            {
                label8.Text = "Please fill all the fields";
                return;
            }
            
            //password validation
            if (passchecker()==false)
            {
               label8.Text = "Password must be 8 characters long and contain at least two special characters";
                return;
            }
            
            



            
            string con= "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            if (comboBox1.Text == "Lecturer")
            {
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();

                    string checkEmailQuery = "SELECT COUNT(*) FROM Teacher WHERE email = @Email";
                    using (SqlCommand checkEmailCommand = new SqlCommand(checkEmailQuery, connection))
                    {
                        checkEmailCommand.Parameters.AddWithValue("@Email", email);
                        int count = (int)checkEmailCommand.ExecuteScalar();

                        if (count > 0)
                        {
                            label8.Text=("Email already exists.");
                            return;
                        }
                    }
                }

                // Insert data into the table
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO Teacher (first_name, last_name, email, [password]) VALUES (@FirstName, @LastName, @Email, @Password)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", first_name);
                        command.Parameters.AddWithValue("@LastName", last_name);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            label8.Text = "Teacher Added Successfully";
                        }
                        else
                        {
                            label8.Text = "Failed to add teacher.";
                        }
                    }
                }
            }
            else if(comboBox1.Text == "Lab Instructor")
            {
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();

                    string checkEmailQuery = "SELECT COUNT(*) FROM LabInstructor WHERE email = @Email";
                    using (SqlCommand checkEmailCommand = new SqlCommand(checkEmailQuery, connection))
                    {
                        checkEmailCommand.Parameters.AddWithValue("@Email", email);
                        int count = (int)checkEmailCommand.ExecuteScalar();

                        if (count > 0)
                        {
                            label8.Text=("Email already exists.");
                            return;
                        }
                    }
                }

                // Insert data into the table
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO Labinstructor (first_name, last_name, email, [password]) VALUES (@FirstName, @LastName, @Email, @Password)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", first_name);
                        command.Parameters.AddWithValue("@LastName", last_name);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            label8.Text = "Lab Instructor Added Successfully";
                        }
                        else
                        {
                            label8.Text = "Failed to add lab instructor.";
                        }
                    }
                }
            }       

        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form form = new Admin_dashboard();
            form.ShowDialog();
            this.Close();
            
        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Techerassignment techerassignment = new Techerassignment();
            techerassignment.ShowDialog();
            this.Close();

        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_dashboard admin = new Admin_dashboard();
            admin.ShowDialog();
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
            this.Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_dashboard admin = new Admin_dashboard();
            admin.ShowDialog();
            this.Close();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            this.Hide();
            Comitee comitee = new Comitee();
            comitee.ShowDialog();
            this.Close();

        }
    }
    }

