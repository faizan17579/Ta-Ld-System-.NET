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
    public partial class Labinstructor : Form
    {
        public Labinstructor(String email)
        {
            InitializeComponent();
            addbox(email);

        }

        private void addbox(String email)
        {
            int id = 0;
            string cn = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(cn))
            {
                con.Open();

                string query = "SELECT * FROM Labinstructor where email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        label1.Text = reader["first_name"].ToString();
                        label2.Text = reader["last_name"].ToString();
                        label3.Text = reader["email"].ToString();
                        id = reader.GetInt32(reader.GetOrdinal("instructor"));
                        
                    }
                    reader.Close();

                    string q1 = "SELECT co.course_name, S.section  FROM Labinstructor t  INNER JOIN Labassignment C ON C.instructor_id = t.instructor  " +
                        "INNER JOIN Course co ON co.course_id = C.course_id \r\nINNER JOIN Section S ON S.Section_id = C.section_id where instructor=@Lab";


                    using (SqlCommand cmd1 = new SqlCommand(q1, con))
                    {
                        cmd1.Parameters.AddWithValue("@Lab", id);
                        SqlDataAdapter sda = new SqlDataAdapter();
                        sda.SelectCommand = cmd1;
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt == null || dt.Rows.Count == 0)
                        {
                            MessageBox.Show("No data found for the specified LD_id.");
                            return;
                        }
                        BindingSource bs = new BindingSource();

                        bs.DataSource = dt;
                        dataGridView1.DataSource = bs;
                        sda.Update(dt);
                    }

                }
            }
            string qu= "select * from LDforinstructor where instructor_id=@Instructor";
            using (SqlConnection con = new SqlConnection(cn))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(qu, con))
                {
                    cmd.Parameters.AddWithValue("@Instructor", id);
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No data found for the specified LD_id.");
                        return;
                    }
                    BindingSource bs = new BindingSource();

                    bs.DataSource = dt;
                    dataGridView2.DataSource = bs;
                    sda.Update(dt);
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if(dataGridView2.SelectedRows.Count == 0|| dataGridView2.SelectedRows[0] == null)
            {
                MessageBox.Show("Please select a LD to Assign");
                return;
            }
            if(dataGridView2.SelectedRows.Count > 0)
            {
                int assignId = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["labAssign"].Value);
                this.Hide();
                LDTasks form = new LDTasks(assignId);
                form.ShowDialog();
                this.Close();
                


            }   

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a LD to Assign");
                return;
            }
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int assignId = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["labAssign"].Value);
                this.Hide();
                Attendence attendence = new Attendence(assignId);
                attendence.ShowDialog();  
                this.Close();

                


            }



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
            Teacherlogin teacherlogin = new Teacherlogin();
            teacherlogin.ShowDialog();  
            this.Close();
        }
    }
}
