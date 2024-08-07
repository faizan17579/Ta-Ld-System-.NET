using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Database_Project
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
       
       // Data Source = DESKTOP - 4DR82DE\SQLEXPRESS;Initial Catalog = Ta_LD_managementsystem; Integrated Security = True; Trust Server Certificate=True
    }
}
