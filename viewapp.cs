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
    public partial class viewapp : Form
    {
        public viewapp(int id)
        {
            InitializeComponent();
            add(id);
        }
        private void add(int id)
        {

            string cn = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";

            string query = "SELECT * FROM Application where enroll_id in (select enrollment_id from student_enrollment where student_id=@id)";

            using (SqlConnection con = new SqlConnection(cn))
            {
                con.Open();
                using (SqlCommand cmd1 = new SqlCommand(query, con))
                {
                    cmd1.Parameters.AddWithValue("@id", id);

                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = cmd1;
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    BindingSource bs = new BindingSource();

                    bs.DataSource = dt;
                    dataGridView1.DataSource = bs;
                    sda.Update(dt);
                }


              string query2 = "SELECT * FROM LDApplication where enroll_id in (select enrollment_id from student_enrollment where student_id=@id)";
            using (SqlCommand cmd2 = new SqlCommand(query2, con))
                {
                        cmd2.Parameters.AddWithValue("@id", id);
    
                        SqlDataAdapter sda = new SqlDataAdapter();
                        sda.SelectCommand = cmd2;
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        BindingSource bs = new BindingSource();
    
                        bs.DataSource = dt;
                        dataGridView2.DataSource = bs;
                        sda.Update(dt);
                }

            }



        }

        private void viewapp_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
           StudentRegister form1 = new StudentRegister();
            form1.ShowDialog();

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();

            this.Close();
        }
    }
}
