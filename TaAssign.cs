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
    public partial class TaAssign : Form
    {
        int  Ta_id;
        public TaAssign(int Ta_id)
        {
            this.Ta_id = Ta_id;
            InitializeComponent();
            Add(Ta_id); 

        }
        private void Add(int ta_id)
        {
            string conne = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            string query = "SELECT * FROM TeacherAssistantDetails WHERE Ta_id = @Ta_id";
            using (SqlConnection connection = new SqlConnection(conne))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Ta_id", ta_id);
                    connection.Open();
                    // insert data to grid view
                    SqlDataAdapter data = new SqlDataAdapter(command);

                    DataTable dt = new DataTable();

                    data.Fill(dt);
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No data found for the specified Ta_id.");
                        return;
                    }
                    dataGridView1.DataSource = dt;
                }
            }

        }
        private void TaAssign_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to view the task");
                return;

            }
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            int taskId = Convert.ToInt32(selectedRow.Cells["Assign_id"].Value);
            this.Hide();
            Tadashboard ldviewtask = new Tadashboard(taskId);
            ldviewtask.ShowDialog();
            this.Close();

        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to view the task");
                return;

            }
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            int taskId = Convert.ToInt32(selectedRow.Cells["Assign_id"].Value);
            this.Hide();
            PaymentforTA payment = new PaymentforTA(taskId);
            payment.ShowDialog();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.ShowDialog();
            this.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Talogin form = new Talogin();
            form.ShowDialog();
            this.Close();
        }
    }
}
