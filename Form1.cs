﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Database_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Talogin form = new Talogin();
            form.Show();
           

        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            StudentRegister form = new StudentRegister();
            form.Show();
           

            
        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.Hide();
            admin form = new admin();
            form.Show();  
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Teacherlogin teacherlogin = new Teacherlogin();
            teacherlogin.Show();
           

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            LDlogin ldlogin = new LDlogin();
            ldlogin.Show();
            

        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Commiteelogin commiteelogin = new Commiteelogin();
            commiteelogin.ShowDialog();
           

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
