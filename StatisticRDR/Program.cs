using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace StatisticRDR
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new ThreadExceptionEventHandler(Exception);
            Application.Run(new Form1());
        }

         static void Exception(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString());
        }
    }
}
