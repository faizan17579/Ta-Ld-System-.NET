using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Database_Project
{
    public partial class Admin_dashboard : Form
    {
        
        public Admin_dashboard()
        {
            InitializeComponent();
            fillbox();
        }
        private void fillbox()

        {
            string cn = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(cn))
            {
                con.Open();

                string query = "SELECT * FROM Department";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader["dep_name"].ToString());
                    }
                    reader.Close();
                }
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;
                pictureBox1.Image = Image.FromFile(imagePath);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fname= textBox1.Text;
            string lname = textBox2.Text;
            string email = textBox3.Text;
            string cgpa = textBox4.Text;
            int departmentId=0;
        

            string conec= "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            using (SqlConnection con = new SqlConnection(conec))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("select * from Department where dep_name = @dep_name", con))
                {
                    cmd.Parameters.AddWithValue("@dep_name", comboBox1.SelectedItem);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            departmentId = Convert.ToInt32(reader["departmentId"]);
                        }
                    }
                }
            }

          

            if (pictureBox1.Image != null)
            {
                byte[] imageBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                    imageBytes = ms.ToArray();
                }

                string cn = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection con = new SqlConnection(cn))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Student (first_name, last_name,email,cgpa,student_image,departmentId) VALUES(@first_name, @last_name, @email, @cgpa, @student_image,@departmentId) ", con))
                    {
                        cmd.Parameters.AddWithValue("@first_name", fname);
                        cmd.Parameters.AddWithValue("@last_name", lname);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@cgpa", cgpa);
                        cmd.Parameters.AddWithValue("@departmentId", departmentId);
                        cmd.Parameters.AddWithValue("@student_image", imageBytes);

                        cmd.ExecuteNonQuery();
                        label12.Text=("Student Added Successfully");
                    }
                }

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_MouseHover(object sender, EventArgs e)
        {


        }

        private void label2_Click(object sender, EventArgs e)
        {
            label1.BackColor = Color.White;
            this.Hide();
            Teacher teacher = new Teacher();
            teacher.ShowDialog();
            this.Close();
            label2.BackColor = Color.Fuchsia;
        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
           
        }

        private void label3_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Techerassignment ass = new Techerassignment();
            ass.ShowDialog();
            this.Close();

        }

        private void label10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Enrollment enrollment = new Enrollment();
            enrollment.ShowDialog();
            this.Close();

        }

        private void label11_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
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
            Form1 form1 = new Form1();
            form1.ShowDialog();

            this.Close();
        }

        private void label11_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Comitee comitee = new Comitee();
            comitee.ShowDialog();
            this.Close();
        

        }

        private void label13_Click(object sender, EventArgs e)
        {
            this.Hide();
            paymentview paymentview = new paymentview();
            paymentview.ShowDialog();
            this.Close();

        }
    }
}

