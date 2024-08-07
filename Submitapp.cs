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
    public partial class Submitapp : Form
    {
        public int id;
        public Submitapp(int id)
        {
           
            this.id = id;
            InitializeComponent();      
       
        }
       
        private void setcourse(int id)
        {    
            label3.Visible = false;
            comboBox2.Visible = false;
            label4.Visible = false;
            comboBox3.Visible = false;
            checkedListBox1.Items.Clear();
            listBox1.Items.Clear();
            string cn = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            using (SqlConnection con = new SqlConnection(cn))
            {
                con.Open();
                if (comboBox1.Text == "Teacher Assistant")
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT c.*,se.* FROM Course c INNER JOIN student_enrollment se ON c.course_id = se.course_id WHERE se.student_id = @id AND se.Grade IS NOT NULL and credit_hour=@credit_hour", con))
                    {
                        cmd.Parameters.AddWithValue("@id", id); 
                        cmd.Parameters.AddWithValue("@credit_hour", 3);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string courseName = reader["course_name"].ToString();
                                checkedListBox1.Items.Add(courseName);
                                string grade = reader["Grade"].ToString();
                                listBox1.Items.Add(grade);
                            }

                            reader.Close();
                        }

                    }
                }
                else if (comboBox1.Text== "Lab Demonstrator")
                {
                    label3.Visible = true;
                    comboBox2.Visible = true;
                    label4.Visible = true;
                    comboBox3.Visible = true;
                   

                    using (SqlCommand cmd = new SqlCommand("SELECT c.*,se.* FROM Course c INNER JOIN student_enrollment se ON c.course_id = se.course_id WHERE se.student_id = @id AND se.Grade IS NOT NULL and credit_hour=@credit_hour", con))
                    {
                        cmd.Parameters.AddWithValue("@id", id); 
                        cmd.Parameters.AddWithValue("@credit_hour", 1);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string courseName = reader["course_name"].ToString();
                                checkedListBox1.Items.Add(courseName);
                                string grade = reader["Grade"].ToString();
                                listBox1.Items.Add(grade);

                            }

                            reader.Close();
                        }

                    }
                }
            }


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {

            // Submit the application
            if (comboBox1.Text == "Teacher Assistant")
            {
                if (checkedListBox1.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Please select at least one course to apply for.");
                    return;
                }
                else if (checkedListBox1.CheckedItems.Count > 2)
                {
                    MessageBox.Show("You can only apply for one course at one time.");
                    return;
                }
                else if (string.IsNullOrEmpty(comboBox1.Text))
                {
                    MessageBox.Show("Please select a department.");
                    return;
                }

                List<string> selectedCourses = new List<string>();
                string selectedposition = comboBox1.Text;

                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        selectedCourses.Add(checkedListBox1.Items[i].ToString());
                    }
                }

                string cn = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";

                using (SqlConnection con = new SqlConnection(cn))
                {
                    con.Open();


                    String que = "SELECT st.student_id FROM Application A INNER JOIN student_enrollment se ON A.enroll_id = se.enrollment_id " +
                        "INNER JOIN Student st ON se.student_id = st.student_id " +
                        "WHERE st.student_id = @id GROUP BY st.student_id HAVING COUNT(st.student_id) = 2";
                    using (SqlCommand cmd = new SqlCommand(que, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            MessageBox.Show("You have already applied for 2 courses");
                            return;
                        }
                        reader.Close();
                    }


                    foreach (string course in selectedCourses)
                    {
                        int enrollmentId = 0;

                        // Retrieve enrollment_id for the selected course
                        SqlCommand sqlCommand = new SqlCommand("SELECT enrollment_id FROM student_enrollment WHERE student_id = @student_id AND course_id = (SELECT course_id FROM Course WHERE course_name = @course_name)", con);
                        sqlCommand.Parameters.AddWithValue("@student_id", id);
                        sqlCommand.Parameters.AddWithValue("@course_name", course);
                        SqlDataReader reader = sqlCommand.ExecuteReader();

                        if (reader.Read())
                        {
                            enrollmentId = Convert.ToInt32(reader["enrollment_id"]);
                        }

                        reader.Close();
                        //check if the student has already applied for the course
                        SqlCommand cmd3 = new SqlCommand("SELECT * FROM Application WHERE enroll_id = @enrollment_id", con);
                        cmd3.Parameters.AddWithValue("@enrollment_id", enrollmentId);
                        SqlDataReader dr = cmd3.ExecuteReader();
                        if (dr.Read())
                        {
                            MessageBox.Show("You have already applied for the course: " + course);
                            return;
                        }
                        dr.Close();

                        // Insert the enrollment_id into the Application table
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Application (enroll_id,status,position) VALUES (@enrollment_id,@status,@Position)", con))
                        {
                            cmd.Parameters.AddWithValue("@enrollment_id", enrollmentId);
                            cmd.Parameters.AddWithValue("@status", "Pending");
                            cmd.Parameters.AddWithValue("@Position", selectedposition);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Application submitted successfully for course: " + course);
                            return;
                        }
                    }
                }
            }
            else if(comboBox1.Text== "Lab Demonstrator")
            {
                if (checkedListBox1.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Please select at least one course to apply for.");
                    return;
                }
                else if (checkedListBox1.CheckedItems.Count > 2)
                {
                    MessageBox.Show("You can only apply for one course at one time.");
                    return;
                }
                else if (string.IsNullOrEmpty(comboBox1.Text))
                {
                    MessageBox.Show("Please select a department.");
                    return;
                }
                else if (string.IsNullOrEmpty(comboBox2.Text))
                {
                    MessageBox.Show("Please select a position.");
                    return;
                }

                List<string> selectedCourses = new List<string>();
                string selectedposition = comboBox1.Text;

                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        selectedCourses.Add(checkedListBox1.Items[i].ToString());
                    }
                }

                string cn = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";

                using (SqlConnection con = new SqlConnection(cn))
                {
                    con.Open();
                    String que = "SELECT st.student_id FROM LDApplication A INNER JOIN student_enrollment se ON A.enroll_id = se.enrollment_id " +
                     "INNER JOIN Student st ON se.student_id = st.student_id " +
                     "WHERE st.student_id = @id GROUP BY st.student_id HAVING COUNT(st.student_id) = 2";
                    using (SqlCommand cmd = new SqlCommand(que, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            MessageBox.Show("You have already applied for 2 courses");
                            return;
                        }
                        reader.Close();
                    }
                    foreach (string course in selectedCourses)
                    {
                        int enrollmentId = 0;

                        // Retrieve enrollment_id for the selected course
                        SqlCommand sqlCommand = new SqlCommand("SELECT enrollment_id FROM student_enrollment WHERE student_id = @student_id AND course_id = (SELECT course_id FROM Course WHERE course_name = @course_name)", con);
                        sqlCommand.Parameters.AddWithValue("@student_id", id);
                        sqlCommand.Parameters.AddWithValue("@course_name", course);
                        SqlDataReader reader = sqlCommand.ExecuteReader();

                        if (reader.Read())
                        {
                            enrollmentId = Convert.ToInt32(reader["enrollment_id"]);
                        }

                        reader.Close();
                        //check if the student has already applied for the course
                        SqlCommand cmd3 = new SqlCommand("SELECT * FROM LDApplication WHERE enroll_id = @enrollment_id", con);
                        cmd3.Parameters.AddWithValue("@enrollment_id", enrollmentId);
                        SqlDataReader dr = cmd3.ExecuteReader();
                        if (dr.Read())
                        {
                            MessageBox.Show("You have already applied for the course: " + course);
                            return;
                        }
                        dr.Close();

                        // Insert the enrollment_id into the Application table
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO LDApplication (enroll_id,status,position,section,depart) VALUES (@enrollment_id,@status,@Position,@section,@depart)", con))
                        {
                            cmd.Parameters.AddWithValue("@enrollment_id", enrollmentId);
                            cmd.Parameters.AddWithValue("@status", "Pending");
                            cmd.Parameters.AddWithValue("@Position", selectedposition);
                            cmd.Parameters.AddWithValue("@section", comboBox2.Text);
                            cmd.Parameters.AddWithValue("@depart", comboBox3.Text);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Application submitted successfully for course: " + course);
                            return;
                        }

                    }

                    }



            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();


        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
           

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text=="")
            {
                MessageBox.Show("Please select a Position.");
                return;
            }
            setcourse(id);
        }

        private void comboBox3_MouseClick(object sender, MouseEventArgs e)
        {
            comboBox3.Items.Clear();

            // Define your connection string
            string connectionString = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";

  
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
            
                string query = "SELECT dep_name FROM Department";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    
                     
                        connection.Open();

                      

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                      
                                comboBox3.Items.Add(reader["dep_name"].ToString());
                            }
                        }
                    
                   
                }
            }





        }

        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {
            comboBox2.Items.Clear();
            string connectionString = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string query = "Select distinct section from Section";

                using (SqlCommand command = new SqlCommand(query, connection))
                {


                    connection.Open();



                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            comboBox2.Items.Add(reader["section"].ToString());
                        }
                    }


                }
            }



        }

        private void Submitapp_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
             StudentRegister form1= new StudentRegister();
            form1.ShowDialog();

            this.Close();
        }
    }
}
