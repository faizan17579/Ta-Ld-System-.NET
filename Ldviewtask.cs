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
    public partial class Ldviewtask : Form
    {
        int assume_id;
        public Ldviewtask(int assign_id)
        {
            assume_id = assign_id;
            InitializeComponent();
            addtask(assign_id);
        }
        private void addtask(int assume_id)
        {
            dataGridView1.DataSource = null;
            string conne = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            string query = "SELECT * FROM  LDTasks WHERE Assign_id = @Assign_id";
            using (SqlConnection connection = new SqlConnection(conne))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Assign_id", assume_id);
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
        private void Ldviewtask_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            string conne = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            if(dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a Task To complete");
                return;
            }
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            int task_id = Convert.ToInt32(selectedRow.Cells["task_id"].Value);
            using (SqlConnection connection = new SqlConnection(conne))
            {
                connection.Open();
                string query = "select status  from LDTasks WHERE task_id = @task_id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@task_id", task_id);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        string status = reader.GetString(0);
                        if (status == "Completed")
                        {
                            MessageBox.Show("Task already completed");
                            return;
                        }
                        reader.Close();
                    }
                    else
                    {
                        reader.Close();
                        
                    }
                   
                   
                    
                }
                
            }
            
            using (SqlConnection connection = new SqlConnection(conne))
            {
                connection.Open();
                string query = "UPDATE LDTasks SET status = @status WHERE task_id = @task_id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@status", "Completed");
                    command.Parameters.AddWithValue("@task_id", task_id);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Task Completed Successfully");
                }
                
            }


            addtask(assume_id);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
           Form1 form = new Form1();
            form.ShowDialog();
            this.Close();

        }
    }
}
