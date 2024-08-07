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
    public partial class FeedbackReview : Form
    {
        public FeedbackReview()
        {
            InitializeComponent();
            add();
        }
        private void add()
        {
            string conn = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";


            string query = "select ta.Assign_id,s.first_name, s.last_name,se.section,sum(ta.rating) as total_rating from TeacherAssistant te inner join TAfeedback ta on " +
                "te.Assign_id=ta.Assign_id inner join Student s on s.student_id=te.Ta_id  inner join teacherassignment teach on teach.assignment_id=te.teacher_assignment" +
                " inner join Section se on se.Section_id=teach.section_id  group by s.first_name, s.last_name,ta.Assign_id,se.section;";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
              //  MessageBox.Show("No data found");
            }
            con.Close();
            string q2 = "select ld.labAssign,s.first_name, s.last_name,se.section,sum(lf.rating) as total_rating from LabDemonstrator ld inner join LDfeedback lf on  " +
                "ld.labAssign=lf.Assign_id inner join Student s on s.student_id=ld.LD_id\r\ninner join Labassignment teach on teach.lab_assignment_id=ld.instructor_assignment  " +
                "inner join Section se on se.Section_id=teach.section_id\r\ngroup by s.first_name, s.last_name,ld.labAssign,se.section;";
            SqlConnection con1 = new SqlConnection(conn);
            SqlCommand cmd1 = new SqlCommand(q2, con1);
            con1.Open();
            SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            adapter1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                dataGridView2.DataSource = dt1;
            }
            else
            {
               // MessageBox.Show("No data found");
            }
            con1.Close();


        }

        private void FeedbackReview_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //clear 
            dataGridView3.DataSource = null;
            string conn = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            string qu = "SELECT ta.Assign_id, s.first_name, s.last_name, se.section, SUM(ta.rating) as total_rating  " +
                "FROM TeacherAssistant te   INNER JOIN TAfeedback ta ON te.Assign_id = ta.Assign_id  " +
                "INNER JOIN Student s ON s.student_id = te.Ta_id  " +
                "INNER JOIN teacherassignment teach ON teach.assignment_id = te.teacher_assignment  " +
                "INNER JOIN Section se ON se.Section_id = teach.section_id  " +
                "GROUP BY ta.Assign_id, s.first_name, s.last_name, se.section  HAVING SUM(ta.rating) = ( " +
                "  SELECT MAX(total_rating) " +
                "   FROM (    SELECT SUM(ta.rating) as total_rating  " +
                "      FROM TeacherAssistant te " +
                "      INNER JOIN TAfeedback ta ON te.Assign_id = ta.Assign_id  GROUP BY ta.Assign_id) AS max_ratings_per_section );";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(qu, con);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dataGridView3.DataSource = dt;
            }
            else
            {
                MessageBox.Show("No data found");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView3.DataSource = null;
            string conn = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            string qu = "SELECT ld.labAssign,s.first_name, s.last_name, se.section, SUM(la.rating) as total_rating  " +
                "FROM LabDemonstrator ld   INNER JOIN LDfeedback la ON la.Assign_id =ld.labAssign   " +
                "INNER JOIN Student s ON s.student_id = ld.LD_id   " +
                "INNER JOIN Labassignment  teach ON teach.lab_assignment_id = ld.instructor_assignment   " +
                "INNER JOIN Section se ON se.Section_id = teach.section_id   " +
                "GROUP BY ld.labAssign, s.first_name, s.last_name, se.section  " +
                "HAVING SUM(la.rating) = (SELECT MIN(total_rating)  FROM (SELECT SUM(ta.rating) as total_rating  FROM LabDemonstrator te " +
                "INNER JOIN LDfeedback ta ON ta.Assign_id =te.LabAssign    GROUP BY ta.Assign_id ) AS min_ratings_per_section );";

            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(qu, con);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dataGridView3.DataSource = dt;
            }
            else
            {
                MessageBox.Show("No data found");
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            dataGridView3.DataSource = null;
            string conn = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            string qu = "SELECT ta.Assign_id, s.first_name, s.last_name, se.section, SUM(ta.rating) as total_rating  " +
                "FROM TeacherAssistant te   INNER JOIN TAfeedback ta ON te.Assign_id = ta.Assign_id  " +
                "INNER JOIN Student s ON s.student_id = te.Ta_id  " +
                "INNER JOIN teacherassignment teach ON teach.assignment_id = te.teacher_assignment  " +
                "INNER JOIN Section se ON se.Section_id = teach.section_id  " +
                "GROUP BY ta.Assign_id, s.first_name, s.last_name, se.section  HAVING SUM(ta.rating) = ( " +
                "  SELECT MIN(total_rating) " +
                "   FROM (    SELECT SUM(ta.rating) as total_rating  " +
                "      FROM TeacherAssistant te " +
                "      INNER JOIN TAfeedback ta ON te.Assign_id = ta.Assign_id  GROUP BY ta.Assign_id) AS min_ratings_per_section );";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(qu, con);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dataGridView3.DataSource = dt;
            }
            else
            {
                MessageBox.Show("No data found");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView3.DataSource = null;
            string conn = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            string qu = "SELECT ld.labAssign,s.first_name, s.last_name, se.section, SUM(la.rating) as total_rating  " +
                 "FROM LabDemonstrator ld   INNER JOIN LDfeedback la ON la.Assign_id =ld.labAssign   " +
                 "INNER JOIN Student s ON s.student_id = ld.LD_id   " +
                 "INNER JOIN Labassignment  teach ON teach.lab_assignment_id = ld.instructor_assignment   " +
                 "INNER JOIN Section se ON se.Section_id = teach.section_id   " +
                 "GROUP BY ld.labAssign, s.first_name, s.last_name, se.section  " +
                 "HAVING SUM(la.rating) = (SELECT MAX(total_rating)  FROM (SELECT SUM(ta.rating) as total_rating  FROM LabDemonstrator te " +
                 "INNER JOIN LDfeedback ta ON ta.Assign_id =te.LabAssign    GROUP BY ta.Assign_id ) AS max_ratings_per_section );";


            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(qu, con);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dataGridView3.DataSource = dt;
            }
            else
            {
                MessageBox.Show("No data found");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
             this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Commitee_Dashboard commitee_Dashboard = new Commitee_Dashboard();
            commitee_Dashboard.ShowDialog();
            this.Close();
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
