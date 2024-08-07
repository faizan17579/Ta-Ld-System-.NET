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
    public partial class LDdashborad : Form
    {
        public LDdashborad( int ld_id)
        {
            InitializeComponent();
            data(ld_id);
            
        }
       private  void data(int ld_id)
        {
            dataGridView1.DataSource = null;
            string conne= "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            string query = "SELECT * FROM LabDemonstratorDetails WHERE LD_id = @Ld_ID";
            using (SqlConnection connection = new SqlConnection(conne))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LD_Id", ld_id);
                    connection.Open();
                // insert data to grid view
                SqlDataAdapter data= new SqlDataAdapter(command);

                    DataTable dt = new DataTable();
                    
                    data.Fill(dt);
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No data found for the specified LD_id.");
                        return;
                    }
                    dataGridView1.DataSource = dt;
                        
                  
                }
            }


        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to view the task");
                return;

            }
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            int taskId = Convert.ToInt32(selectedRow.Cells["LabAssign"].Value);
            this.Hide();
            Ldviewtask ldviewtask = new Ldviewtask(taskId);
            ldviewtask.Show();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to view the task");
                return;

            }
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            int taskId = Convert.ToInt32(selectedRow.Cells["LabAssign"].Value);
            this.Hide();
            Payment payment = new Payment(taskId);
            payment.Show();
          


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.ShowDialog();
            this.Close();

        }
    }
}
