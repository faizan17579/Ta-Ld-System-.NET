using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Database_Project
{
    public partial class Lecturerdashboard : Form
    {     int id;
          string coursename="";
        
        public Lecturerdashboard(String email)
        {
            InitializeComponent();
            setbox(email);
        }
        private void setbox(String email)
        {
           
            string cn = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(cn))
            {
                con.Open();

                string query = "SELECT * FROM Teacher where email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {

                    cmd.Parameters.AddWithValue("@Email", email);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        label1.Text = reader["first_name"].ToString();
                        label2.Text = reader["last_name"].ToString();
                        label3.Text = reader["email"].ToString();
                        id = Convert.ToInt32(reader["teacher_id"]);             
                    }
                    reader.Close();

                    string q1 = "Select s.course_name,se.section from Teacher t inner join teacherassignment C on C.teacher_id=t.teacher_id" +
                        " inner join Section se on se.Section_id=C.section_id inner join Course s on s.course_id=C.course_id where t.teacher_id=@teacher_id";
                    // add above to grid view
                    using (SqlCommand cmd1 = new SqlCommand(q1, con))
                    {
                        cmd1.Parameters.AddWithValue("@teacher_id", id);

                        SqlDataReader sqlDataReader = cmd1.ExecuteReader();
                        while (sqlDataReader.Read())
                        {
                            comboBox1.Items.Add(sqlDataReader["section"].ToString());
                        }
                        sqlDataReader.Close();

                        SqlDataAdapter sda = new SqlDataAdapter();
                        sda.SelectCommand = cmd1;
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        BindingSource bs = new BindingSource();

                        bs.DataSource = dt;
                        dataGridView1.DataSource = bs;
                        sda.Update(dt);
                    }
                    String q2 = "SELECT S.first_name AS first_name, S.last_name AS last_name,SE.Grade AS Grade, A.status, A.Position, " +
                    "S.cgpa, D.dep_name,S.email,C.course_name FROM Application A " +
                     "JOIN student_enrollment SE ON A.enroll_id = SE.enrollment_id " +
                      "JOIN Course C ON SE.course_id = C.course_id " +
                      "JOIN teacherassignment TA ON TA.course_id = C.course_id " +
                      "JOIN Teacher T ON T.teacher_id = TA.teacher_id " +
                      "JOIN Student S ON S.student_id = SE.student_id " +
                      "JOIN Department D ON D.departmentId = S.departmentId " +
                     "WHERE A.Position = 'Teacher Assistant' " +
                      "AND A.status = 'Pending' " +
                      "AND T.first_name = @fname " +
                      "AND T.last_name = @lname";

                    using (SqlCommand cmd2 = new SqlCommand(q2, con))
                    {
                        
                        cmd2.Parameters.AddWithValue("@fname", label1.Text);
                        cmd2.Parameters.AddWithValue("@lname", label2.Text);
                       



                        SqlDataAdapter sda = new SqlDataAdapter();
                        sda.SelectCommand = cmd2;
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        BindingSource bs = new BindingSource();

                        bs.DataSource = dt;
                        dataGridView2.DataSource = bs;
                        sda.Update(dt);

                    }
                 





                }
            }
            string qw = "select total_enroll from Application";
            using (SqlConnection con = new SqlConnection(cn))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(qw, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        label8.Text = reader["total_enroll"].ToString();

                    }
                    reader.Close();
                }
            }

        }
        private void Lecturerdashboard_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int course_id=0;
            int std_id = 0;
            if (dataGridView2.RowCount == 0)
            {
                MessageBox.Show("No rows selected in the DataGridView.");
                return;
            }
            if (dataGridView2.SelectedRows.Count > 0)
            {
                if(comboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Please select a section");
                    return;
                }

             
                string fname = dataGridView2.SelectedRows[0].Cells[0].Value.ToString(); // assuming first column is string fname
                string lname = dataGridView2.SelectedRows[0].Cells[1].Value.ToString(); // assuming second column isstring lname 
                string grade = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
                string status = dataGridView2.SelectedRows[0].Cells[3].Value.ToString();
                string position = dataGridView2.SelectedRows[0].Cells[4].Value.ToString();
                string cgpa = dataGridView2.SelectedRows[0].Cells[5].Value.ToString();
                string dep = dataGridView2.SelectedRows[0].Cells[6].Value.ToString();
                string email = dataGridView2.SelectedRows[0].Cells[7].Value.ToString();
                string course = dataGridView2.SelectedRows[0].Cells[8].Value.ToString();
                
                string cn = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection con = new SqlConnection(cn))
                {
                    con.Open();
                    string q = "SELECT student_id FROM Student WHERE first_name = @fname AND last_name = @lname AND email=@email";
                    using (SqlCommand cmd = new SqlCommand(q, con))
                    {
                        cmd.Parameters.AddWithValue("@fname", fname);
                        cmd.Parameters.AddWithValue("@lname", lname);
                        cmd.Parameters.AddWithValue("@email", email);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if(reader.Read())
                        {
                            std_id = Convert.ToInt32(reader["student_id"]);
                        }
                        else
                        {
                            MessageBox.Show("Student not found");
                            return;
                        }

                        

                       
                        reader.Close();

                    string section = comboBox1.SelectedItem.ToString();
                        int sec_id=0;
                        string q0 = "select Section_id from Section where section=@section ";
                        using (SqlCommand cmd8 = new SqlCommand(q0, con))
                        {
                            cmd8.Parameters.AddWithValue("@section",section);
                            SqlDataReader reader1 = cmd8.ExecuteReader();

                            while (reader1.Read())
                            {
                                sec_id= Convert.ToInt32(reader1["Section_id"]);
                            }

                            reader1.Close();

                        }

                        String q3="select course_id from Course where course_name=@course";
                        using (SqlCommand cmd3 = new SqlCommand(q3, con))
                        {
                            cmd3.Parameters.AddWithValue("@course", course);
                            SqlDataReader reader1 = cmd3.ExecuteReader();
                            
                            while (reader1.Read())
                            {
                                course_id = Convert.ToInt32(reader1["course_id"]);
                            }
                        
                            reader1.Close();
                         
                        }
                        string q4="Select assignment_id from teacherassignment where course_id=@course_id and teacher_id=@teacher_id AND section_id=@section";
                        using (SqlCommand cmd4 = new SqlCommand(q4, con))
                        {

                            cmd4.Parameters.AddWithValue("@course_id", course_id);
                            cmd4.Parameters.AddWithValue("@teacher_id", id);
                            cmd4.Parameters.AddWithValue("@section", sec_id);
                            SqlDataReader reader2 = cmd4.ExecuteReader();
                            int assignment_id = 0;
                            while (reader2.Read())
                            {
                                assignment_id = Convert.ToInt32(reader2["assignment_id"]);
                            }
                        
                            reader2.Close();
                    

                        string q5="insert into TeacherAssistant values(@Ta_id,@teacher_assignment)";
                            using (SqlCommand cmd5 = new SqlCommand(q5, con))
                            {
                                cmd5.Parameters.AddWithValue("@Ta_id", std_id);
                                cmd5.Parameters.AddWithValue("@teacher_assignment", assignment_id);
                                //check if student is already assigned
                                string q6 = "Select * from TeacherAssistant where Ta_id=@Ta_id and teacher_assignment=@teacher_assignment";
                                using (SqlCommand cmd6 = new SqlCommand(q6, con))
                                {
                                    cmd6.Parameters.AddWithValue("@Ta_id", std_id);
                                    cmd6.Parameters.AddWithValue("@teacher_assignment", assignment_id);
                                    SqlDataReader reader3 = cmd6.ExecuteReader();
                                    if (reader3.Read())
                                    {
                                        MessageBox.Show("Already Assigned");
                                        return;
                                    }
                                    reader3.Close();
                                }   
                           
                            cmd5.ExecuteNonQuery();
                            MessageBox.Show("Assigned");
                        }
                        }
                           

                      //  string q1 = "UPDATE Application SET status = 'Approved' WHERE Position = 'Teacher Assistant' AND student_id = @id and "+

                       string q1="Update  Application set status=@reject where Position='Teacher Assistant' and enroll_id="+
                         " (select enrollment_id from student_enrollment where student_id=@id and course_id=@course_id)";
                            

                        using (SqlCommand cmd1 = new SqlCommand(q1, con))
                        {
                            cmd1.Parameters.AddWithValue("@id", std_id);
                            cmd1.Parameters.AddWithValue("@course_id", course_id);
                            cmd1.Parameters.AddWithValue("@reject", "Approved");
                            cmd1.ExecuteNonQuery();
                            // update grid view 2
                            String q2 = "SELECT S.first_name AS first_name, S.last_name AS last_name,SE.Grade AS Grade, A.status, A.Position, " +
                                "S.cgpa, D.dep_name,S.email,C.course_name FROM Application A " +
                                "JOIN student_enrollment SE ON A.enroll_id = SE.enrollment_id " +
                                    "JOIN Course C ON SE.course_id = C.course_id " +
                                    "JOIN teacherassignment TA ON TA.course_id = C.course_id " +
                                    "JOIN Teacher T ON T.teacher_id = TA.teacher_id " +
                                    "JOIN Student S ON S.student_id = SE.student_id " +
                                    "JOIN Department D ON D.departmentId = S.departmentId " +
                                    "WHERE A.Position = 'Teacher Assistant' " +
                                    "AND A.status = 'Pending' " +
                                    "AND T.first_name = @fname " +
                                    "AND T.last_name = @lname"; 
                            using (SqlCommand cmd2 = new SqlCommand(q2, con))
                            {
                                cmd2.Parameters.AddWithValue("@fname", label1.Text);
                                cmd2.Parameters.AddWithValue("@lname", label2.Text);
                                SqlDataAdapter sda = new SqlDataAdapter();
                                sda.SelectCommand = cmd2;
                                DataTable dt = new DataTable();
                                sda.Fill(dt);
                                BindingSource bs = new BindingSource();

                                bs.DataSource = dt;
                                dataGridView2.DataSource = bs;
                                sda.Update(dt);
                            }

                           
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row");
            }

            }

        private void button3_Click(object sender, EventArgs e)
        {
            //want to delete the selected row from the grid view
            if (dataGridView2.RowCount == 0)
            {
                MessageBox.Show("No rows selected in the DataGridView.");
                return;
            }
            if (dataGridView2.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];

                // Get the values from the selected row
                string firstName = selectedRow.Cells["first_name"].Value.ToString();
                string lastName = selectedRow.Cells["last_name"].Value.ToString();
                string courseName = selectedRow.Cells["course_name"].Value.ToString();

                // Delete the row from dataGridView2
                dataGridView2.Rows.RemoveAt(selectedRow.Index);

                // Delete the corresponding record from the Application table
                string cn = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";

                using (SqlConnection con = new SqlConnection(cn))
                {
                    con.Open();

                    // Find the student_id and course_id for the selected row
                    string query = "SELECT S.student_id, C.course_id FROM Student S " +
                                   "JOIN student_enrollment SE ON S.student_id = SE.student_id " +
                                   "JOIN Course C ON SE.course_id = C.course_id " +
                                   "WHERE S.first_name = @firstName AND S.last_name = @lastName AND C.course_name = @courseName";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@firstName", firstName);
                        cmd.Parameters.AddWithValue("@lastName", lastName);
                        cmd.Parameters.AddWithValue("@courseName", courseName);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            int studentId = Convert.ToInt32(reader["student_id"]);
                            int courseId = Convert.ToInt32(reader["course_id"]);

                            reader.Close();

                            // Delete the record from the Application table
                            string deleteQuery = "Update Application set status =@reject WHERE enroll_id IN " +
                                                 "(SELECT enrollment_id FROM student_enrollment WHERE student_id = @studentId AND course_id = @courseId)";

                            using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, con))
                            {

                                deleteCmd.Parameters.AddWithValue("@studentId", studentId);
                                deleteCmd.Parameters.AddWithValue("@courseId", courseId);
                                deleteCmd.Parameters.AddWithValue("@reject", "Rejected");

                                deleteCmd.ExecuteNonQuery();
                                MessageBox.Show("Record Updated successfully.");
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Tadetails tadetails = new Tadetails(id);
            tadetails.Show();
            this.Close();


            

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Teacherlogin teacherlogin= new Teacherlogin();
            teacherlogin.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.ShowDialog();
            this.Close();

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
    }


