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
    public partial class PaymentforTA : Form
    {
        public PaymentforTA(int assign)
        {
            InitializeComponent();
            addapp(assign);
        }
       private void addapp(int assign)
        {
            int count = 0;
            string conne = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            string q = "select S.num_students from  TeacherAssistant ta " +
                "inner join teacherassignment te on ta.teacher_assignment=te.assignment_id  " +
                "inner join Section S on S.Section_id=te.section_id  where ta.Assign_id=@id;";
            using (SqlConnection c = new SqlConnection(conne))
            {
                c.Open();
                using (SqlCommand cm = new SqlCommand(q, c))
                {
                    cm.Parameters.AddWithValue("@id", assign);
                    if (cm.ExecuteScalar() == null)
                    {
                        label1.Text = ("No data found for the specified Assign_id.");
                        return;
                    }
                    count = (int)cm.ExecuteScalar();

                }
            }

          //  MessageBox.Show(assign.ToString());
                
            int payment = count * 250;
            string query2 = "INSERT INTO paymentrequestforTA(labAssign,totalpayment,status) VALUES(@Assign_id,@Payment,@status)";
            using (SqlConnection connection = new SqlConnection(conne))
            {
                using (SqlCommand command = new SqlCommand(query2, connection))
                {
                    command.Parameters.AddWithValue("@Assign_id", assign);
                    command.Parameters.AddWithValue("@Payment", payment);
                    command.Parameters.AddWithValue("@status", "Pending");
                    connection.Open();
                    command.ExecuteNonQuery();
                    label1.Text = "Payment request has been sent successfully. Total payment is " + payment;
                }
            }



            dataGridView1.DataSource = null;

            string query = "SELECT * FROM  paymentrequestforTA WHERE labAssign = @Assign_id";
            using (SqlConnection connection = new SqlConnection(conne))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Assign_id", assign);
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void PaymentforTA_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.ShowDialog();
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Talogin form = new Talogin();
            form.ShowDialog();
            this.Close();
        }
    }
}
