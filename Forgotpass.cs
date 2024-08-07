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
    public partial class Forgotpass : Form
    {
        string cate;
        public Forgotpass(string cate)
        {
            this.cate = cate;
        
            InitializeComponent();

          

        }
      

        private void Forgotpass_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            if (cate == "Admin")
            {
                string name = textBox1.Text;
                string last_name = textBox2.Text;
               
                string cn = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection con = new SqlConnection(cn))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("select * from admin where first_name = @name and last_name=@lnamw", con))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@lnamw", last_name);
                       
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                label4.Text=("Your password is " + reader["password"].ToString());
                            }
                            else
                            {
                                label4.Text=("No data found for the specified name.");
                            }
                        }
                    }
                }
            }
            else if(cate=="Teacher")
            {
                string name = textBox1.Text;
                string last_name = textBox2.Text;
                string cn = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection con = new SqlConnection(cn))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("select * from Teacher where first_name = @name and last_name=@lnamw", con))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@lnamw", last_name);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                label4.Text=("Your password is " + reader["password"].ToString());
                            }
                            else
                            {
                                label4.Text=("No data found for the specified name.");
                            }
                        }
                    }
                }
            }
           

            else if (cate == " Lecturer")
            {
                string name = textBox1.Text;
                string last_name = textBox2.Text;
                string cn = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection con = new SqlConnection(cn))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("select * from Teacher where first_name = @name and last_name=@lnamw", con))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@lnamw", last_name);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                label4.Text=("Your password is " + reader["password"].ToString());
                            }
                            else
                            {
                                label4.Text=("No data found for the specified name.");
                            }
                        }
                    }
                }
            }
            else if(cate=="Lab Instructor")
            {
                string name = textBox1.Text;
                string last_name = textBox2.Text;
                string cn = "Data Source=DESKTOP-4DR82DE\\SQLEXPRESS;Initial Catalog=Ta_LD_managementsystem;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection con = new SqlConnection(cn))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("select * from Labinstructor where first_name = @name and last_name=@lnamw", con))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@lnamw", last_name);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                label4.Text = ("Your password is " + reader["password"].ToString());
                            }
                            else
                            {
                                label4.Text = ("No data found for the specified name.");
                            }
                        }
                    }
                }

            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
