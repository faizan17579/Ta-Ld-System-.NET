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
    public partial class LDTasks : Form
    {
        int assign_id;
        public LDTasks(int assign_id)
        {
            this.assign_id = assign_id;
            InitializeComponent();
            addtask(assign_id);
        }
        void addtask(int assign_id)
        {
            string conne = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            string query = "SELECT task_id, details, status, Time, Duedate FROM LDTasks Where Assign_id = @Assign_Id";
            using (SqlConnection connection = new SqlConnection(conne))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Assign_Id", assign_id);
                    connection.Open();
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

        private void LDTasks_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String conne = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            String details = textBox1.Text;
            String status = comboBox1.Text;
            string date = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss");
            if (details == "" || status == "")
            {
                MessageBox.Show("Please fill all the fields");
                return;
            }
            String qu = "Select COUNT(*) from LDTasks where Assign_id=@assign group by Assign_id having count(*)>14";
            using (SqlConnection c = new SqlConnection(conne))
            {
                c.Open();
                using (SqlCommand cm = new SqlCommand(qu, c))
                {

                   cm.Parameters.AddWithValue("@assign", assign_id);
                    if(cm.ExecuteScalar()!=null)
                    {
                        MessageBox.Show("You have reached the maximum number of tasks");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("You can add more tasks to this assignment.");
                    }

                   
                }

            }




            using (SqlConnection con = new SqlConnection(conne))
            {
                string query = "INSERT INTO  LDTasks (Assign_id, details, status, Time, Duedate) VALUES (@Ta_id, @details, @status, @Time, @Duedate)";
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Ta_id", assign_id);
                    cmd.Parameters.AddWithValue("@details", details);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@Time", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Duedate", date);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Task added successfully");
                }
            }

            //update the datagridview


            addtask(assign_id);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //update the selected task from the datagridview
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a task to update.");
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            // Retrieve task details from the selected row
            int taskId = Convert.ToInt32(selectedRow.Cells["task_id"].Value);
            string date = dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss");
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True"))
            {
                connection.Open();

                // Update the task status
                string query = "UPDATE LDTasks SET status = @status,DueDate=@date WHERE task_id = @taskId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@status", comboBox2.Text);
                    command.Parameters.AddWithValue("@taskId", taskId);
                    command.Parameters.AddWithValue("@date", date);


                    command.ExecuteNonQuery();
                    MessageBox.Show("Task updated successfully.");
                }
                DataGridViewRow row = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index];
                row.Cells["status"].Value = comboBox2.Text;
                row.Cells["Duedate"].Value = date;

            }

        }

       

        private void button3_Click_1(object sender, EventArgs e)
        {
            //update the selected task from the datagridview
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a task to update.");
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            // Retrieve task details from the selected row
            int taskId = Convert.ToInt32(selectedRow.Cells["task_id"].Value);
            string date = dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss");
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True"))
            {
                connection.Open();

                // Update the task status
                string query = "UPDATE LDTasks SET status = @status,DueDate=@date WHERE task_id = @taskId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@status", comboBox2.Text);
                    command.Parameters.AddWithValue("@taskId", taskId);
                    command.Parameters.AddWithValue("@date", date);


                    command.ExecuteNonQuery();
                    MessageBox.Show("Task updated successfully.");
                }
                DataGridViewRow row = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index];
                row.Cells["status"].Value = comboBox2.Text;
                row.Cells["Duedate"].Value = date;

            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            //delete the selected task from the datagridview
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a task to delete.");
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            String conne = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            int taskId = Convert.ToInt32(selectedRow.Cells["task_id"].Value);
            using (SqlConnection connection = new SqlConnection(conne))
            {
                connection.Open();

                // Delete the selected task
                string query = "DELETE FROM LDTasks WHERE task_id = @taskId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@taskId", taskId);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Task deleted successfully.");
                }
                dataGridView1.Rows.Remove(selectedRow);
            }
            addtask(assign_id);


        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
            this.Close();
             

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
            LDlogin lDlogin = new LDlogin();
            lDlogin.ShowDialog();
            this.Close();
        }
    }
}
