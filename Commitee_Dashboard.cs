using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Database_Project
{
    public partial class Commitee_Dashboard : Form
    {
        int id = 0;
        public Commitee_Dashboard()
        {
            InitializeComponent();
            Addappl();
            
        }
        private void Addappl()
        {
            string conne = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection connection = new SqlConnection(conne))
            {
                connection.Open();

                string query = "SELECT S.student_id AS ID,S.first_name AS StudentFirstName, S.last_name AS StudentLastName,C.course_name As Course," +
                    "SE.Grade As Grade,S.cgpa As CGPA,La.status as Status,La.section As AppliedSection,La.depart As AppliedDepartment,La.enroll_id as Enrollid FROM LDApplication La\r\ninner join student_enrollment SE On La.enroll_id=SE.enrollment_id " +
                    "inner join Course C on SE.course_id=C.course_id  inner join Student S on S.student_id=SE.student_id;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = command;
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        //MessageBox.Show("No data found for the specified Assign_id.");
                        return;
                    }
                    BindingSource bs = new BindingSource();
                    bs.DataSource = dt;
                    dataGridView1.DataSource = bs;
                    sda.Update(dt);
                }
            }
            string q = "Select total_enroll from  LDApplication";
            using (SqlConnection connection = new SqlConnection(conne))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(q, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int total = Convert.ToInt32(reader["total_enroll"]);
                            label5.Text = "" + total;
                        }
                    }
                }
            }
            


            


            }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        int courseId = 0;
        int lab_assignment_id = 0;
        int sectionId = 0;
        int studentId = 0;
            
            string conne = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            // string 
            if (dataGridView1.RowCount== 0)
            {
                MessageBox.Show("No request present");
                return;
                
            }
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (comboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Please select a section");
                    return;
                }

             string student = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            studentId = Convert.ToInt32(student);
       
            string course = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
             string section = comboBox2.SelectedItem.ToString();
            string department = comboBox3.SelectedItem.ToString();
            string enroll= dataGridView1.SelectedRows[0].Cells[9].Value.ToString();
    
            int enroll_id = Convert.ToInt32(enroll);
           // MessageBox.Show(student_id);
                MessageBox.Show(course);
            
             string query = "SELECT course_id FROM Course WHERE course_name = @course";
                using (SqlConnection connection = new SqlConnection(conne))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@course", course);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // If a record is found, retrieve the course_id
                                courseId= Convert.ToInt32(reader["course_id"]);
                            
                            }
                            reader.Close();
                        }
                    }
                }
              
                string query1 = "SELECT S.section_id   FROM Section S  INNER JOIN Department D ON S.Department_id = D.departmentId  WHERE S.section = @section AND D.dep_name = @name;";
                using (SqlConnection connection = new SqlConnection(conne))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query1, connection))
                    {
                        command.Parameters.AddWithValue("@section", section);
                        command.Parameters.AddWithValue("@name", department);
                        using (SqlDataReader reader2 = command.ExecuteReader())
                        {
                            if (reader2.Read())
                            {
                                // If a record is found, retrieve the section_id
                                sectionId = Convert.ToInt32(reader2["section_id"]);
                              
                             
                            }
                            reader2.Close();
                        }
                        
                    }
                   
                }
                string query3="select lab_assignment_id  from  Labassignment where instructor_id=@id and course_id=@course and section_id=@section";
                using (SqlConnection connection = new SqlConnection(conne))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query3, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@course", courseId);
                        command.Parameters.AddWithValue("@section", sectionId);
                        using (SqlDataReader reader3 = command.ExecuteReader())
                        {
                            if (reader3.Read())
                            {
                                // If a record is found, retrieve the section_id
                                 lab_assignment_id = Convert.ToInt32(reader3["lab_assignment_id"]);
                               
                               
                            }
                            else
                            {
                                MessageBox.Show("lab assignment Not  Matched to application");
                                return;
                            }
                            reader3.Close();
                        }
                        
                    }

                    
                }   
                 string q9="select * from LabDemonstrator where LD_id=@student_id and instructor_assignment=@lab_assignment_id";
                using (SqlConnection connection = new SqlConnection(conne))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(q9, connection))
                    {

                         
                        command.Parameters.AddWithValue("@student_id", studentId);
                        command.Parameters.AddWithValue("@lab_assignment_id", lab_assignment_id);
                        using (SqlDataReader reader4 = command.ExecuteReader())
                        {

                           



                            if (reader4.Read())
                            {
                              
                                MessageBox.Show("Student Already Assigned");
                                return;
                            }
                            else
                            {
                                reader4.Close();
                            }

                            reader4.Close();
                        }
                        
                    }

                    
                }
                
                string query4="insert into LabDemonstrator values(@student_id,@lab_assignment_id)";
                using (SqlConnection connection = new SqlConnection(conne))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query4, connection))
                    {
                        command.Parameters.AddWithValue("@student_id", studentId);
                        command.Parameters.AddWithValue("@lab_assignment_id", lab_assignment_id);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Student Assigned");
                    }
                }
                string section1 = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                string department1 = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
               
            string  query5 = "Update LDApplication SET status = @status WHERE section=@section and depart=@depart and enroll_id=@enroll";
                using (SqlConnection connection = new SqlConnection(conne))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query5, connection))
                    {
                        command.Parameters.AddWithValue("@status", "Assigned");
                        command.Parameters.AddWithValue("@section", section1);
                        command.Parameters.AddWithValue("@depart", department1);
                        command.Parameters.AddWithValue("@enroll", enroll_id);
                        command.ExecuteNonQuery();


                     
                            
                        
                    }
                }
                Addappl();
               

               






            }
            else
            {
                MessageBox.Show("Please select a student to assign");
            }

            }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            string conne = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection connection = new SqlConnection(conne))
            {
                connection.Open();

                string query = "SELECT S.student_id AS ID,S.first_name AS StudentFirstName, S.last_name AS StudentLastName,C.course_name As Course," +
                    "SE.Grade As Grade,S.cgpa As CGPA,La.section As AppliedSection,La.depart As AppliedDepartment FROM LDApplication La\r\ninner join student_enrollment SE On La.enroll_id=SE.enrollment_id " +
                    "inner join Course C on SE.course_id=C.course_id  inner join Student S on S.student_id=SE.student_id order by Grade ASC;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = command;
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    BindingSource bs = new BindingSource();

                    bs.DataSource = dt;
                    dataGridView1.DataSource = bs;
                    sda.Update(dt);
                }
            }





        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            string conne = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection connection = new SqlConnection(conne))
            {
                connection.Open();

                string query = "SELECT S.student_id AS ID,S.first_name AS StudentFirstName, S.last_name AS StudentLastName,C.course_name As Course," +
                    "SE.Grade As Grade,S.cgpa As CGPA,La.section As AppliedSection,La.depart As AppliedDepartment FROM LDApplication La\r\ninner join student_enrollment SE On La.enroll_id=SE.enrollment_id " +
                    "inner join Course C on SE.course_id=C.course_id  inner join Student S on S.student_id=SE.student_id order by CGPA DESC;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = command;
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    BindingSource bs = new BindingSource();

                    bs.DataSource = dt;
                    dataGridView1.DataSource = bs;
                    sda.Update(dt);
                }
            }



        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
            //add all lab instructor
            comboBox1.Items.Clear();
            List<string> instructorNames = new List<string>();
            string conne = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            string query = "SELECT * FROM Labinstructor";
            using (SqlConnection connection = new SqlConnection(conne))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string fname = reader["first_name"].ToString();
                            string lname = reader["last_name"].ToString();
                            string fullName = fname + " " + lname;
                            comboBox1.Items.Add(fullName);
                           
                           

                        }
                    }
                }
            }
           
          
          

            
           
          


        }

        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {
            comboBox2.Items.Clear();
            string conne = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";

            string query1 = "SELECT * FROM Labinstructor WHERE first_name = @fname";
            using (SqlConnection connection = new SqlConnection(conne))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query1, connection))
                {
                    string[] name = comboBox1.Text.Split(' ');
                    string fname = name[0];

                    command.Parameters.AddWithValue("@fname", fname); ;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            id = Convert.ToInt32(reader["instructor"]);
                     
                          

                        }
                    }
                }
            }
            String q = "select S.section,d.dep_name from Labinstructor la inner join Labassignment las on la.instructor=las.instructor_id inner join  " +
                     "Section S on las.section_id=S.Section_id inner join Department d on S.Department_id=d.departmentId where la.instructor=@id;";
            using (SqlConnection connection = new SqlConnection(conne))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(q, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            comboBox2.Items.Add(reader["section"].ToString());
                           

                        }
                    }

                }
            }



        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_MouseClick(object sender, MouseEventArgs e)
        {
            comboBox3.Items.Clear();
            string conne = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";

            string query1 = "SELECT * FROM Labinstructor WHERE first_name = @fname";
            using (SqlConnection connection = new SqlConnection(conne))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query1, connection))
                {
                    string[] name = comboBox1.Text.Split(' ');
                    string fname = name[0];

                    command.Parameters.AddWithValue("@fname", fname); ;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            id = Convert.ToInt32(reader["instructor"]);
                            

                        }
                    }
                }
            }
            String q = "select S.section,d.dep_name from Labinstructor la inner join Labassignment las on la.instructor=las.instructor_id inner join  " +
                     "Section S on las.section_id=S.Section_id inner join Department d on S.Department_id=d.departmentId where la.instructor=@id;";
            using (SqlConnection connection = new SqlConnection(conne))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(q, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            
                            comboBox3.Items.Add(reader["dep_name"].ToString());

                        }
                    }

                }
            }



        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            FeedbackReview feedback = new FeedbackReview();
            feedback.Show();
        }
    }
}
