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
    public partial class Tadetails : Form
    {
        int tech_id = 0;
       

        public Tadetails(int id)
        {
            tech_id = id;
            InitializeComponent();
            fillgrid(id);

        }
        void fillgrid(int id)
        {
            string connectionString = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";

            string query = @"SELECT S.student_id AS ID,S.first_name AS StudentFirstName, S.last_name AS StudentLastName,SE.section AS Section,C.course_name AS Course,Tas.assignment_id AS AssignmentId
               FROM TeacherAssistant TA
               INNER JOIN Student S ON TA.Ta_id = S.student_id
               INNER JOIN teacherassignment Tas ON Tas.assignment_id = TA.teacher_assignment
               Inner join Course C on C.course_id=Tas.course_id
               INNER JOIN Section SE on SE.Section_id=Tas.section_id
               WHERE Tas.teacher_id = @TeacherId";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameter for teacher ID
                        command.Parameters.AddWithValue("@TeacherId", id);

                        // want to store teacher assignment id


                       






                        SqlDataAdapter sda = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        BindingSource bs = new BindingSource(dt, null);
                        dataGridView1.DataSource = bs;
                    }
                }
            }
            catch (Exception ex)
            {
                label7.Text=("Error: " + ex.Message);
            }
         
       
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {    
            if(dataGridView1.SelectedRows.Count == 0)
            {
                label7.Text=("Please select a student");
                return;
            }
            //from selected row, get the student id]
            int ta_id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            int assignment_id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[5].Value);
            MessageBox.Show("Ta_id: " + ta_id + " Assignment_id: " + assignment_id);
            this.Hide();
            Tatasks t = new Tatasks(ta_id,tech_id,assignment_id); 
            t.Show();




        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count == 0)
            {
                return;
            }


            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }
            if (dataGridView1.SelectedRows[0].Cells[0].Value == null)
            {
                return;
            }
            if (dataGridView1.SelectedRows[0].Cells[0].Value == null)
            {
                return;
            }
            object cellValue = dataGridView1.SelectedRows[0].Cells[0].Value;
            if (cellValue == null)
            {
                return;
            }



            int ta_id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            String connectionString = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            string query = "SELECT COUNT(*) FROM TaTasks TA INNER JOIN TeacherAssistant ts ON TA.Assign_id=ts.Assign_id INNER JOIN" +
                " teacherassignment Tas ON Tas.assignment_id = ts.teacher_assignment WHERE ts.Ta_id = @TaId And Tas.teacher_id = @TeacherId ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TaId", ta_id);
                    command.Parameters.AddWithValue("@TeacherId", tech_id);
                   
                    connection.Open();

                    if(command.ExecuteScalar() == null)
                    {
                        label4.Text = "0";
                        return;
                    }
                    int count = (int)command.ExecuteScalar();

                    label4.Text = "" + count;
                }
            }
            

            String connections = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            String query2 = "SELECT COUNT(*) FROM TaTasks TA INNER JOIN TeacherAssistant ts ON TA.Assign_id=ts.Assign_id INNER JOIN teacherassignment Tas ON Tas.assignment_id = ts.teacher_assignment " +
                "WHERE ts.Ta_id = @TaId And Tas.teacher_id = @TeacherId And status=@UnComplete";
            using (SqlConnection connection = new SqlConnection(connections))
            {
                using (SqlCommand command = new SqlCommand(query2, connection))
                {
                    command.Parameters.AddWithValue("@TaId", ta_id);
                    command.Parameters.AddWithValue("@TeacherId", tech_id);
                    command.Parameters.AddWithValue("@UnComplete", "Complete");
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    label5.Text = "" + count;
                }



            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Tadetails_Load(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.ShowDialog();
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Teacherlogin teacherlogin
                = new Teacherlogin();
            teacherlogin.ShowDialog();
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
