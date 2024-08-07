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
    public partial class Payment : Form
    {
        int assign_id;
        public Payment(int assign_id)
        {
            this.assign_id = assign_id;
            InitializeComponent();
            addtask(assign_id);
        }
        void addtask(int assign_id)
        {
            int count = 0;
            string conne = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            string q= "select Count(*) from Attendence where Assign_id=@id group by Assign_id having count(*)>14";
            using (SqlConnection c = new SqlConnection(conne))
            {
                c.Open();
                using (SqlCommand cm = new SqlCommand(q, c))
                {
                    cm.Parameters.AddWithValue("@id", assign_id);
                    if (cm.ExecuteScalar() == null)
                    {
                        label1.Text = ("You cannont make request untill attendene > 14  or no attendence till");
                        
                        return;
                    }
                    count = (int)cm.ExecuteScalar();
                   

                    
                  
                 

                }
            }
            int payment = (count * 3) * 500;
            string query2 = "INSERT INTO paymentrequest(labAssign,totalpayment,status) VALUES(@Assign_id,@Payment,@status)";
            using (SqlConnection connection = new SqlConnection(conne))
            {
                using (SqlCommand command = new SqlCommand(query2, connection))
                {
                    command.Parameters.AddWithValue("@Assign_id", assign_id);
                    command.Parameters.AddWithValue("@Payment", payment);
                    command.Parameters.AddWithValue("@status", "Pending");
                    connection.Open();
                    command.ExecuteNonQuery();
                    label1.Text = "Payment request has been sent successfully. Total payment is " + payment;
                }
            }



            dataGridView1.DataSource = null;
          
            string query = "SELECT * FROM  paymentrequest WHERE labAssign = @Assign_id";
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
                       label1.Text = ("No data found for the specified Assign_id.");
                        return;
                    }
                    dataGridView1.DataSource = dt;
                }
            }

        }

        private void Payment_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();

        }
    }
}
