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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Database_Project
{
    public partial class Techerassignment : Form
    {
        int dep_id;
        public Techerassignment()
        { 
            



            InitializeComponent();


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_MouseClick(object sender, MouseEventArgs e)

        {
            comboBox2.Items.Clear();
            string fname;
            string lname;
            String con = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            if (comboBox1.Text == "Lecturer")
            {
                using (SqlConnection cn = new SqlConnection(con))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("select * from Teacher", cn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            fname = reader["first_name"].ToString();
                            lname = reader["last_name"].ToString();
                            comboBox2.Items.Add(fname + " " + lname);
                        }
                        reader.Close();



                    }
                }
            }
            else if (comboBox1.Text == "Lab Instructor")
            {
                using (SqlConnection cn = new SqlConnection(con))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("select * from Labinstructor", cn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            fname = reader["first_name"].ToString();
                            lname = reader["last_name"].ToString();
                            comboBox2.Items.Add(fname + " " + lname);
                        }
                        reader.Close();
                    }
                }
            }

        }

        private void comboBox3_MouseClick(object sender, MouseEventArgs e)
        {
            comboBox3.Items.Clear();
            int num;
            if (comboBox1.Text == "Lecturer")
            {
                num = 3;
                string con = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection cn = new SqlConnection(con))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("select * from Course where credit_hour=@num", cn))
                    {
                        cmd.Parameters.AddWithValue("@num", num);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            comboBox3.Items.Add(reader["course_name"].ToString());
                        }
                        reader.Close();
                    }
                }
            }
            else if (comboBox1.Text == "Lab Instructor")
            {
                num = 1;
                string con = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection cn = new SqlConnection(con))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("select * from Course where credit_hour=@num", cn))
                    {
                        cmd.Parameters.AddWithValue("@num", num);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            comboBox3.Items.Add(reader["course_name"].ToString());
                        }
                        reader.Close();
                    }
                }
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {       
            if(comboBox1.Text==""||comboBox2.Text==""||comboBox3.Text==""||comboBox4.Text=="")
            {
                label5.Text="Please fill all the fields";
            }

            int techid=0;
            int courseid=0;
            int sectionid=0;


          string con = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            if (comboBox1.Text == "Lecturer")
            {
                using (SqlConnection cn = new SqlConnection(con))
                {
                    cn.Open();
                SqlCommand cmd4=new SqlCommand("select teacher_id from teacherassignment where teacher_id=(select teacher_id from Teacher where first_name=@fname and last_name=@lname)", cn);
                    string[] name1 = comboBox2.Text.Split(' ');
                    cmd4.Parameters.AddWithValue("@fname", name1[0]);
                    cmd4.Parameters.AddWithValue("@lname", name1[1]);
                    SqlDataReader reader4 = cmd4.ExecuteReader();
                    if (reader4.Read())
                    {

                        techid = Convert.ToInt32(reader4["teacher_id"]);
                        reader4.Close();
                    }
                    else
                    {
                        reader4.Close();
                    }
                SqlCommand cmd5 = new SqlCommand("select course_id from teacherassignment where course_id=(select course_id from Course where course_name=@cname)", cn);
                    cmd5.Parameters.AddWithValue("@cname", comboBox3.Text);
                    SqlDataReader reader5 = cmd5.ExecuteReader();
                    if (reader5.Read())
                    {
                        courseid = Convert.ToInt32(reader5["course_id"]);
                        reader5.Close();
                    }
                    else
                    {
                        reader5.Close();
                    }
                    SqlCommand cmd3 = new SqlCommand("select Section_id from Section where section=@sec and Department_id=(Select departmentId from Department where dep_name=@dep_name)", cn);
                    cmd3.Parameters.AddWithValue("@sec", comboBox4.Text);
                    cmd3.Parameters.AddWithValue("@dep_name", comboBox5.Text);
                    SqlDataReader reader2 = cmd3.ExecuteReader();
                    if (reader2.Read())
                    {
                        sectionid = Convert.ToInt32(reader2["Section_id"]);
                        
                        reader2.Close();
                    }
                    else
                    {
                        reader2.Close();
                    }
                    
                    SqlCommand sqlCommand = new SqlCommand("select * from teacherassignment where teacher_id=@techid and course_id=@courseid and section_id=@se", cn);
                    sqlCommand.Parameters.AddWithValue("@techid", techid);
                    sqlCommand.Parameters.AddWithValue("@courseid", courseid);
                    sqlCommand.Parameters.AddWithValue("@se", sectionid);
                    
                    SqlDataReader reader6 = sqlCommand.ExecuteReader();
                    if (reader6.Read())
                    {
                        label5.Text = "Assignment already done";
                        reader6.Close();
                        return;
                    }
                    else
                    {
                        reader6.Close();
                    }
                    using (SqlCommand cmd = new SqlCommand("select * from Teacher where first_name=@fname and last_name=@lname", cn))
                    {
                        string[] name = comboBox2.Text.Split(' ');
                        cmd.Parameters.AddWithValue("@fname", name[0]);
                        cmd.Parameters.AddWithValue("@lname", name[1]);
                        SqlDataReader reader = cmd.ExecuteReader();
                        reader.Read();
                        int id = Convert.ToInt32(reader["teacher_id"]);
                        reader.Close();
                        using (SqlCommand cmd1 = new SqlCommand("select * from Course where course_name=@cname", cn))
                        {
                            cmd1.Parameters.AddWithValue("@cname", comboBox3.Text);
                            SqlDataReader reader1 = cmd1.ExecuteReader();
                            reader1.Read();
                            int cid = Convert.ToInt32(reader1["course_id"]);
                            reader1.Close();
                            using (SqlCommand cmd2 = new SqlCommand("insert into teacherassignment values(@teacher_id,@course_id,@section_id)", cn))
                            {
                                cmd2.Parameters.AddWithValue("@teacher_id", id);
                                cmd2.Parameters.AddWithValue("@course_id", cid);
                                cmd2.Parameters.AddWithValue("@section_id", sectionid);
                                
                                cmd2.ExecuteNonQuery();
                                label5.Text= "Assignment Done";

                            }
                        }
                    }
                }
            }

            else if (comboBox1.Text == "Lab Instructor")
            {
                if (comboBox1.Text == "" || comboBox2.Text == "" || comboBox3.Text == "" || comboBox4.Text == "")
                {
                    label5.Text=("Please fill all the fields");
                }

                using (SqlConnection cn = new SqlConnection(con))
                {
                    cn.Open();
                    SqlCommand cmd4 = new SqlCommand("select instructor_id from Labassignment where instructor_id=(select instructor from Labinstructor where first_name=@fname and last_name=@lname)", cn);
                    string[] name1 = comboBox2.Text.Split(' ');
                    cmd4.Parameters.AddWithValue("@fname", name1[0]);
                    cmd4.Parameters.AddWithValue("@lname", name1[1]);
                    SqlDataReader reader4 = cmd4.ExecuteReader();


                    if (reader4.Read())
                    {
                        techid = Convert.ToInt32(reader4["instructor_id"]);
                        reader4.Close();
                    }
                    else
                    {
                        reader4.Close();
                    }

                    SqlCommand cmd5 = new SqlCommand("select course_id from Labassignment where course_id=(select course_id from Course where course_name=@cname)", cn);
                    cmd5.Parameters.AddWithValue("@cname", comboBox3.Text);
                    SqlDataReader reader5 = cmd5.ExecuteReader();

                    if (reader5.Read())
                    {
                        courseid = Convert.ToInt32(reader5["course_id"]);
                        reader5.Close();
                    }
                    else
                    {
                        reader5.Close();
                    }
                    SqlCommand cmd3 = new SqlCommand("select Section_id from Section where section=@sec and Department_id=(Select departmentId from Department where dep_name=@dep_name)", cn);

                    cmd3.Parameters.AddWithValue("@sec", comboBox4.Text);
                    cmd3.Parameters.AddWithValue("@dep_name", comboBox5.Text);
                    SqlDataReader reader2 = cmd3.ExecuteReader();
                    if (reader2.Read())
                    {
                        sectionid = Convert.ToInt32(reader2["Section_id"]);

                        reader2.Close();
                    }
                    else
                    {
                        reader2.Close();
                    }

                    SqlCommand sqlCommand = new SqlCommand("select * from Labassignment where instructor_id=@techid and course_id=@courseid and section_id=@se", cn);
                    sqlCommand.Parameters.AddWithValue("@techid", techid);
                    sqlCommand.Parameters.AddWithValue("@courseid", courseid);
                    sqlCommand.Parameters.AddWithValue("@se", sectionid);
                    SqlDataReader reader6 = sqlCommand.ExecuteReader();
                    if (reader6.Read())
                    {
                        label5.Text = "Assignment already done";
                        reader6.Close();
                        return;
                    }
                    else
                    {
                        reader6.Close();
                    }



                    using (SqlCommand cmd = new SqlCommand("select * from Labinstructor where first_name=@fname and last_name=@lname", cn))
                    {

                        string[] name = comboBox2.Text.Split(' ');
                        cmd.Parameters.AddWithValue("@fname", name[0]);
                        cmd.Parameters.AddWithValue("@lname", name[1]);
                        SqlDataReader reader = cmd.ExecuteReader();
                        reader.Read();
                        int id = Convert.ToInt32(reader["instructor"]);
                        reader.Close();
                        using (SqlCommand cmd1 = new SqlCommand("select * from Course where course_name=@cname", cn))
                        {
                            cmd1.Parameters.AddWithValue("@cname", comboBox3.Text);
                            SqlDataReader reader1 = cmd1.ExecuteReader();
                            reader1.Read();
                            int cid = Convert.ToInt32(reader1["course_id"]);
                            reader1.Close();
                            using (SqlCommand cmd2 = new SqlCommand("insert into Labassignment values(@lid,@cid,@sec)", cn))
                            {
                                cmd2.Parameters.AddWithValue("@lid", id);
                                cmd2.Parameters.AddWithValue("@cid", cid);
                                cmd2.Parameters.AddWithValue("@sec", sectionid);
                                cmd2.ExecuteNonQuery();
                                label5.Text = "Assignment Done";


                            }


                        }
                    }
                }
            }
           
           
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Teacher ad=new Teacher();
            ad.ShowDialog();
            this.Close();

        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_dashboard admin_Dashboard = new Admin_dashboard();
            admin_Dashboard.ShowDialog();
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_dashboard admin = new Admin_dashboard();
            admin.ShowDialog();
            this.Close();
        }

        private void label10_Click(object sender, EventArgs e)
        {

            this.Hide();
            Enrollment enrollment = new Enrollment();
            enrollment.ShowDialog();
            this.Close();
                
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox5_MouseClick(object sender, MouseEventArgs e)
        {
            String con="Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            comboBox5.Items.Clear();
            using (SqlConnection cn = new SqlConnection(con))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("select * from  Department", cn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox5.Items.Add(reader["Dep_name"].ToString());
                      
                    }
                    reader.Close();
                }
            }

        }

        private void comboBox4_MouseClick(object sender, MouseEventArgs e)
        {
            String con = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
            comboBox4.Items.Clear();
            String dep_name = comboBox5.Text;
            using (SqlConnection cn = new SqlConnection(con))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("select * from  Department where Dep_name=@Dep_name", cn))
                {
                    cmd.Parameters.AddWithValue("@Dep_name", dep_name);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        dep_id = Convert.ToInt32(reader["departmentId"]);
                    }
                    reader.Close();
                }
            }
            using (SqlConnection cn = new SqlConnection(con))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("select * from  Section where Department_id=@Department_id", cn))
                {
                    cmd.Parameters.AddWithValue("@Department_id", dep_id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox4.Items.Add(reader["section"].ToString());
                    }
                    reader.Close();
                }
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {
            this.Hide();
            Comitee comitee = new Comitee();    
            comitee.ShowDialog();
            this.Close();

        }
    }
   
}
