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
    public partial class paymentview : Form
    {
        public paymentview()
        {
            InitializeComponent();
            add();

        }
        void add()
        {
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            string conn= "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            string q="Select * from Ldrequestdetail";
            using (SqlConnection c = new SqlConnection(conn))
            {
                c.Open();
                using (SqlCommand cm = new SqlCommand(q, c))
                {
                    SqlDataAdapter data = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    data.Fill(dt);
                    if (dt == null || dt.Rows.Count == 0)
                    {
                       // MessageBox.Show("No data found for the specified Assign_id.");
                        return;
                    }
                    dataGridView1.DataSource = dt;

                }
            }
            string q2 = "select  * from TArequestdetail";
            using (SqlConnection c = new SqlConnection(conn))
            {
                c.Open();
                using (SqlCommand cm = new SqlCommand(q2, c))
                {
                    SqlDataAdapter data = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    data.Fill(dt);
                    if (dt == null || dt.Rows.Count == 0)
                    {
                    //    MessageBox.Show("No data found for the specified Assign_id.");
                        return;
                    }
                    dataGridView2.DataSource = dt;

                }
            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string conn = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to view the task");
                return;

            }
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            int task_id = Convert.ToInt32(selectedRow.Cells["req_id"].Value);
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                string query = "Update paymentrequest set status='Paid' WHERE req_id = @req_id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@req_id", task_id);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Payment has been made");
                } 
            }
            add();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string conn = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to reject ");
                return;

            }
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            int task_id = Convert.ToInt32(selectedRow.Cells["req_id"].Value);
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                string query = "Update paymentrequest set status='Reject' WHERE req_id = @req_id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@req_id", task_id);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Payment Request has been Reject");
                }
            }
            add();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string conn = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to Accept");
                return;

            }
            DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];
            int task_id = Convert.ToInt32(selectedRow.Cells["req_id"].Value);
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                string query = "Update paymentrequestforTA set status='Paid' WHERE req_id = @req_id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@req_id", task_id);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Payment has been Piad");
                }
            }
            add();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string conn = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to Reject");
                return;

            }
            DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];
            int task_id = Convert.ToInt32(selectedRow.Cells["req_id"].Value);
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                string query = "Update paymentrequestforTA set status='Reject' WHERE req_id = @req_id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@req_id", task_id);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Payment Request has been Reject");
                }
            }
            add();

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }
            if (dataGridView1.SelectedRows[0].Cells[0].Value == null)
            {
                return;
            }
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            int task_id = Convert.ToInt32(selectedRow.Cells["labAssign"].Value);
            string conn = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            string quey = "select Count(*) from Attendence where Assign_id = @id group by Assign_id having count(*)> 14";
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(quey, connection))
                {
                    command.Parameters.AddWithValue("@id", task_id);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {

                        label5.Text = "Fullfill";
                    }
                    else
                    {
                        label5.Text = "NotFullfill";
                        
                    }
                }
            }
            




        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                return;
            }
            if (dataGridView2.SelectedRows[0].Cells[0].Value == null)
            {
                return;
            }
            DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];
            int task_id = Convert.ToInt32(selectedRow.Cells["labAssign"].Value);
            string conn = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            string quey = "select Count(*) from TaTasks where Assign_id = @id and status =@status group by Assign_id having count(*) > 8";
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(quey, connection))
                {
                    command.Parameters.AddWithValue("@id", task_id);
                    command.Parameters.AddWithValue("@status", "Complete");
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {

                        label6.Text = "Fullfill";
                    }
                    else
                    {
                        label6.Text= "NotFullfill";
                    }
                }
            }

        }

        private void paymentview_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.ShowDialog();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_dashboard admin_Dashboard = new Admin_dashboard();
            admin_Dashboard.ShowDialog();
            this.Close();

        }
    }
}
