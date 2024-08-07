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
    public partial class Attendence : Form
    {
        int assign;
        public Attendence(int assign_id)
        {
            assign = assign_id;
            
            InitializeComponent();
        }
        private void add(int assign_id)
        {
            dataGridView1.DataSource = null;
            string conne = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            string query = "SELECT * FROM  Attendence WHERE Assign_id = @Assign_id";
            using (SqlConnection connection = new SqlConnection(conne))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Assign_id", assign_id);
                    connection.Open();
                    // insert data to grid view
                    SqlDataAdapter data = new SqlDataAdapter(command);

                    DataTable dt = new DataTable();

                    data.Fill(dt);
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No data found for the specified Assign_id.");
                        return;
                    }
                    dataGridView1.DataSource = dt;
                }
            }

        }
        private void Attendence_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String conne = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            string la = comboBox1.Text;
             int lab_no = Convert.ToInt32(comboBox1.Text);
            string date = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss");
            string status = comboBox2.Text;
          
            if (la == "" || status == "")
            {
                label5.Text=("Please fill all the fields");
                return;
            }
            using (SqlConnection connection = new SqlConnection(conne))
            {
                connection.Open();
                string query = "INSERT INTO Attendence (Assign_id, lab_no,Date,Marking) VALUES (@Assign_id, @labno, @time, @status)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Assign_id", assign);
                    command.Parameters.AddWithValue("@labno", lab_no);
                    command.Parameters.AddWithValue("@time", date);
                    command.Parameters.AddWithValue("@status", status);
                  
                    command.ExecuteNonQuery();
                    label5.Text=("Attendence added successfully");
                    add(assign);
                }
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();    
            Form1 form1 = new Form1();
            form1.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Teacherlogin teacherlogin = new Teacherlogin();
            teacherlogin.ShowDialog();
            this.Close();
           
        }
    }
}
