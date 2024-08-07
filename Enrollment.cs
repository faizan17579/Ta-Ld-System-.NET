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
    public partial class Enrollment : Form
    {
        public Enrollment()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="" || comboBox1.Text=="" || comboBox2.Text=="")
            {
                label4.Text=("Please fill all the fields");
                return;
            }
            String email = textBox1.Text;


            String con= "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            using (SqlConnection cn = new SqlConnection(con))
            {
                cn.Open();
                SqlCommand cmd3=new SqlCommand("Select * from student_enrollment where student_id=(select student_id from Student where email=@Email) and course_id=(select course_id from Course where course_name=@Course)",cn);
                cmd3.Parameters.AddWithValue("@Email", email);
                cmd3.Parameters.AddWithValue("@Course", comboBox1.Text);
                SqlDataReader reader = cmd3.ExecuteReader();
                if (reader.Read())
                {
                    label4.Text=("Already Enrolled in this course");
                    return;
                }
                reader.Close();
                SqlCommand cmd= new SqlCommand("select student_id from Student where email = @Email", cn);
                SqlCommand cmd2= new SqlCommand("select course_id from Course where course_name = @Course", cn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd2.Parameters.AddWithValue("@Course", comboBox1.Text);
                String student_id = cmd.ExecuteScalar().ToString();
                String course_id = cmd2.ExecuteScalar().ToString();
                SqlCommand cmd4 = new SqlCommand("insert into student_enrollment(student_id,course_id,Grade) values(@student_id,@course_id,@Grade)", cn);
                cmd4.Parameters.AddWithValue("@student_id", student_id);   
                cmd4.Parameters.AddWithValue("@course_id", course_id);
                String grade=comboBox2.Text;
                cmd4.Parameters.AddWithValue("@Grade", grade);
                cmd4.ExecuteNonQuery();
                label4.Text=("Enrollment Successful");
                
                
            }

        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            comboBox1.Items.Clear();
            String con = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            using (SqlConnection cn = new SqlConnection(con))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("select course_name from Course", cn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox1.Items.Add(reader["course_name"].ToString());
                        }
                    }
                }
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_dashboard admin = new Admin_dashboard();
            admin.ShowDialog();
            this.Close();

        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Teacher teacher = new Teacher();
            teacher.ShowDialog();
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Techerassignment ass = new Techerassignment();
            ass.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || comboBox1.Text == "")
            {
                label4.Text = ("Please fill all the fields");
                return;
            }

            string email = textBox1.Text;
            string courseName = comboBox1.Text;

            string connectionString = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if the student is enrolled in the selected course
                SqlCommand checkEnrollmentCmd = new SqlCommand("SELECT COUNT(*) FROM student_enrollment SE " +
                                                                "JOIN Student S ON SE.student_id = S.student_id " +
                                                                "JOIN Course C ON SE.course_id = C.course_id " +
                                                                "WHERE S.email = @Email AND C.course_name = @CourseName", connection);
                checkEnrollmentCmd.Parameters.AddWithValue("@Email", email);
                checkEnrollmentCmd.Parameters.AddWithValue("@CourseName", courseName);
                int enrollmentCount = (int)checkEnrollmentCmd.ExecuteScalar();

                if (enrollmentCount == 0)
                {
                    label4.Text = ("The student is not enrolled in the selected course.");
                    return;
                }

                // Update the grade for the enrolled student
                SqlCommand updateGradeCmd = new SqlCommand("UPDATE student_enrollment " +
                                                           "SET Grade = @Grade " +
                                                           "WHERE student_id = (SELECT student_id FROM Student WHERE email = @Email) " +
                                                           "AND course_id = (SELECT course_id FROM Course WHERE course_name = @CourseName)", connection);
                updateGradeCmd.Parameters.AddWithValue("@Email", email);
                updateGradeCmd.Parameters.AddWithValue("@CourseName", courseName);
                updateGradeCmd.Parameters.AddWithValue("@Grade", comboBox2.Text);
                int rowsAffected = updateGradeCmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    label4.Text = ("Grade updated successfully.");
                }
                else
                {
                    label4.Text=("Failed to update grade.");
                }
            }



        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_dashboard admin = new Admin_dashboard();
            admin.ShowDialog();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

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
