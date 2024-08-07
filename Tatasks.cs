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
    public partial class Tatasks : Form
    {
        int  ta_id;
        int  tech_task;
        int assignment_id;
        int assign_id;
        public Tatasks(int ta_id,int tech_task,int assignment_id)
        {
            this.ta_id = ta_id;
            this.tech_task = tech_task;
            this.assignment_id = assignment_id;

            InitializeComponent();
            addtask(ta_id,tech_task,assignment_id);
        }
        private void addtask(int ta_id,int tech_task,int assignment_id){
          
            string conne = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            string que="select Assign_id from TeacherAssistant where Ta_id=@Ta_id and teacher_assignment=@tecaherassignment";
            using(SqlConnection c=new SqlConnection(conne))
            {
                c.Open();
                using(SqlCommand cm=new SqlCommand(que,c))
                {
                    cm.Parameters.AddWithValue("@Ta_id",ta_id);
                    cm.Parameters.AddWithValue("@tecaherassignment",assignment_id);
                    if(cm.ExecuteScalar()==null)
                    {
                        
                        return;
                    }
                    assign_id=(int)cm.ExecuteScalar();
                    MessageBox.Show(assign_id.ToString());

                    
                }
            }





            using (SqlConnection connection = new SqlConnection(conne))
            {
                
                connection.Open();

             
                string query = "SELECT task_id, details, status, Time, Duedate FROM TaTasks TA INNER JOIN TeacherAssistant ts ON TA.assign_id=ts.Assign_id INNER JOIN " +
                    "  teacherassignment Tas ON Tas.assignment_id = ts.teacher_assignment WHERE TA.Assign_id = @TaId And Tas.teacher_id = @TeacherId ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
               
                    command.Parameters.AddWithValue("@TaId", assign_id);
                    command.Parameters.AddWithValue("@TeacherId", tech_task);


                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = command;
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No data found for the specified Assign_id.");
                        return;
                    }
                    BindingSource bs = new BindingSource();
                    bs.DataSource = null;
                    bs.DataSource = dt;
                    dataGridView1.DataSource = bs;
                    sda.Update(dt);




                }
            }





        }
        private void Tatasks_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String conne = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            String details= textBox1.Text;
            String status=comboBox1.Text;
            string date = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss");
            if (details==""||status=="")
            {
                MessageBox.Show("Please fill all the fields");
                return;
            }
           String qu="Select COUNT(*) from TaTasks where Assign_id=(select Assign_id from TeacherAssistant where Ta_id=@Ta_id and teacher_assignment=@assignment)";
            using(SqlConnection c=new SqlConnection(conne))
            {
                c.Open();
                using(SqlCommand cm=new SqlCommand(qu,c))
                {
                    cm.Parameters.AddWithValue("@Ta_id",ta_id);
                    cm.Parameters.AddWithValue("@assignment",assignment_id);
                    int count=(int)cm.ExecuteScalar();
                    if(count==8)
                    {
                        MessageBox.Show("You have already 8 tasks");
                        return;
                    }
                }

            }



          
                using (SqlConnection con = new SqlConnection(conne))
                {
                    string query = "INSERT INTO TaTasks (Assign_id, details, status, Time, Duedate) VALUES (@Ta_id, @details, @status, @Time, @Duedate)";
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query,con))
                    {
                        cmd.Parameters.AddWithValue("@Ta_id",assign_id);

                        cmd.Parameters.AddWithValue("@details",details);
                        cmd.Parameters.AddWithValue("@status",status);
                        cmd.Parameters.AddWithValue("@Time",DateTime.Now);
                        cmd.Parameters.AddWithValue("@Duedate",date);
                       
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Task added successfully");
                    }
                }

            //update the datagridview

            
            addtask(ta_id,tech_task,assignment_id);
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
                string query = "UPDATE TaTasks SET status = @status,DueDate=@date WHERE task_id = @taskId";

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

        private void button4_Click(object sender, EventArgs e)
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
                string query = "DELETE FROM TaTasks WHERE task_id = @taskId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@taskId", taskId);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Task deleted successfully.");
                }
                dataGridView1.Rows.Remove(selectedRow);
            }
            addtask(ta_id,tech_task,assignment_id);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
            this.Close();
            
        }
    }
}
