using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Database_Project
{
    public partial class Student_dashboard : Form
    {
        public int id;

        public Student_dashboard(string email)
        {
            InitializeComponent();
            id = 0;
            setall(email);
            gettotalapp(id);


        }
        private void gettotalapp(int id)
        {
            int total = 0;
            string cn = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            using (SqlConnection con = new SqlConnection(cn))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) as total FROM Application " +
                              "INNER JOIN student_enrollment ON Application.enroll_id = student_enrollment.enrollment_id " +
                              "INNER JOIN Course ON student_enrollment.course_id = Course.course_id " +
                              "WHERE student_enrollment.student_id = @id", con))

                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                        total= Convert.ToInt32(reader["total"]);
                           
                        

                            
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }
            
            
            using (SqlConnection co = new SqlConnection(cn))
            {
                co.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) as total FROM LDApplication " +
                              "INNER JOIN student_enrollment ON LDApplication.enroll_id = student_enrollment.enrollment_id " +
                              "INNER JOIN Course ON student_enrollment.course_id = Course.course_id " +
                              "WHERE student_enrollment.student_id = @id", co))

                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            total= total+ Convert.ToInt32(reader["total"]);
                        }
                        reader.Close();
                    }
                }
            }
            label7.Text = "" + total;

            


        }
        private void setall(string email)
        {
            
            
            string cn= "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(cn))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("select * from Student where email = @Email", con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            id = Convert.ToInt32(reader["student_id"]);
                            label1.Text = reader["first_name"].ToString();
                            label2.Text = reader["last_name"].ToString();
                            label3.Text = reader["email"].ToString();
                            label4.Text = "CGPA: "+reader["cgpa"].ToString();
                            if (reader["student_image"] != DBNull.Value)
                            {
                                byte[] imageBytes = (byte[])reader["student_image"];
                                MemoryStream ms = new MemoryStream(imageBytes);
                                pictureBox1.Image = Image.FromStream(ms);
                            }
                            else
                            {
                                pictureBox1.Image = null;

                            }
                            reader.Close();
                            string cmd1 = "select * from Department where departmentId= (select departmentId from Student where email = @Email)";
                            SqlCommand cmd2 = new SqlCommand(cmd1, con);
                            cmd2.Parameters.AddWithValue("@Email", email);
                            SqlDataReader reader1 = cmd2.ExecuteReader();
                            if (reader1.Read())
                            {
                                label5.Text = reader1["dep_name"].ToString();
                                reader1.Close();
                            }
                            
                           
                        }
                    }
                }
            }

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
         
            this.Hide();
            Submitapp app = new Submitapp(id);
            app.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            viewapp app = new viewapp(id);
            app.Show();

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Student_dashboard_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Feedback app = new Feedback(id);
            app.Show();


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
            StudentRegister form1 = new StudentRegister();
            form1.ShowDialog();

            this.Close();
        }
    }
}
