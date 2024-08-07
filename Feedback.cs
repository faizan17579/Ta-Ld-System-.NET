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
    public partial class Feedback : Form
    {
        int std_id;
        public Feedback(int id)
        {
            std_id=id;
            InitializeComponent();
            add();
           
        }
        private void add()
        {


            string connectionString = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            string qu = "select distinct ld.LabAssign,s.first_name,s.last_name from LabDemonstrator ld  inner join Student s on ld.LD_id=s.student_id";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(qu, con);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dataGridView2.DataSource = dt;
            }
            else
            {
              //  MessageBox.Show("No data found");
            }

            dataGridView2.DataSource = dt;
            con.Close();



            string connectionString1 = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            string qu1 = "select  ld.Assign_id,s.first_name,s.last_name from TeacherAssistant ld  inner join Student s on ld.Ta_id=s.student_id";
            SqlConnection con1 = new SqlConnection(connectionString1);
            SqlCommand cmd1 = new SqlCommand(qu1, con1);
            con1.Open();
            SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            adapter1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt1;
            }
            else
            {
               // MessageBox.Show("No data found");
            }

            dataGridView1.DataSource = dt1;
            con1.Close();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();

            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (dataGridView2.SelectedRows.Count > 0)
            {
                if (comboBox2.Text == "")
                {
                    MessageBox.Show("Please select Rating");
                    return;
                }
                int rating = Convert.ToInt32(comboBox2.Text);
                int labAssign = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value);
                string connectionString = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
                string q1 = "select * from LDfeedback where Assign_id=@LabAssign and stu_id=@std_id";
                SqlConnection con1 = new SqlConnection(connectionString);
                SqlCommand cmd1 = new SqlCommand(q1, con1);
                con1.Open();
                cmd1.Parameters.AddWithValue("@LabAssign", labAssign);
                cmd1.Parameters.AddWithValue("@std_id", std_id);
                cmd1.ExecuteNonQuery();
                SqlDataReader reader = cmd1.ExecuteReader();
                if (reader.Read())
                {
                    MessageBox.Show("You have already given feedback for this assignment");
                    return;
                }







                string qu = "insert into LDfeedback values(@LabAssign,@rating,@std_id)";
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(qu, con);
                con.Open();
                cmd.Parameters.AddWithValue("@LabAssign", labAssign);
                cmd.Parameters.AddWithValue("@rating", rating);
                cmd.Parameters.AddWithValue("@std_id", std_id);

                cmd.ExecuteNonQuery();
                con.Close();
                add();
            }
            else
            {
                MessageBox.Show("Please select a row");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (comboBox1.Text == "")
                {
                    MessageBox.Show("Please select Rating");
                    return;
                }


                int rating = Convert.ToInt32(comboBox1.Text);
                int assign_id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);


                string connectionString = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
                string q1 = "select * from TAfeedback where Assign_id=@LabAssign and stu_id=@std_id";
                SqlConnection con1 = new SqlConnection(connectionString);
                SqlCommand cmd1 = new SqlCommand(q1, con1);
                con1.Open();
                cmd1.Parameters.AddWithValue("@LabAssign", assign_id);
                cmd1.Parameters.AddWithValue("@std_id", std_id);
                cmd1.ExecuteNonQuery();
                SqlDataReader reader = cmd1.ExecuteReader();
                if (reader.Read())
                {
                    MessageBox.Show("You have already given feedback for this assignment");
                    return;
                }



                string qu = "insert into TAfeedback values(@LabAssign,@rating,@std_id)";
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(qu, con);
                con.Open();
                cmd.Parameters.AddWithValue("@LabAssign", assign_id);
                cmd.Parameters.AddWithValue("@rating", rating);
                cmd.Parameters.AddWithValue("@std_id", std_id);

                cmd.ExecuteNonQuery();
                con.Close();
                add();
            }
            else
            {
                MessageBox.Show("Please select a row");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            StudentRegister form1= new StudentRegister();   
            form1.ShowDialog();

            this.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
