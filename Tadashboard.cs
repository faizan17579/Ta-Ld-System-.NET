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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Database_Project
{
    public partial class Tadashboard : Form
    {

        int assign_id;
        public Tadashboard(int Ass_id)
        {

            InitializeComponent();
            addtask(Ass_id);

        }
        private void addtask(int Ass_id)
        {

            dataGridView1.DataSource = null;
            string conne = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            string query = "SELECT * FROM  TATasks WHERE Assign_id = @Assign_id";
            using (SqlConnection connection = new SqlConnection(conne))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Assign_id",Ass_id );
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
        private void Tadashboard_Load(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a task to update.");
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            // Retrieve task details from the selected row
            int taskId = Convert.ToInt32(selectedRow.Cells["Task_id"].Value);
          
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True"))
            {
                connection.Open();
                if (selectedRow.Cells["status"].Value == null)
                {
                
                    return;
                }
                String status = selectedRow.Cells["status"].Value.ToString();
                if (status == "Complete")
                {
                    MessageBox.Show("Task is already complete.");
                    return;
                }
                // Update the task status
                string query = "UPDATE TaTasks SET status = @status WHERE Task_id = @taskId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@status","Complete");
                    command.Parameters.AddWithValue("@taskId", taskId);
                   


                    command.ExecuteNonQuery();
                    MessageBox.Show("Task updated successfully.");
                }
                DataGridViewRow row = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index];
                row.Cells["status"].Value ="Complete";
                

            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Talogin form = new Talogin();
            form.ShowDialog();
            this.Close();

        }
    }
}
