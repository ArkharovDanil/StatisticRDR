using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StatisticRDR
{
    public partial class Form4 : Form
    {

        MyDelegate1 _delegate;
        string[] strings;
        DataTable dt = new DataTable();
        /// <summary>
        /// Флаг, обозначающий с каким списком мы работаем 0-Библиотеки,1-категории
        /// </summary>
        public Form4()
        {
            InitializeComponent();
        }
        public Form4(string host,string port,string user, string password, MyDelegate1 sender)
        {
            _delegate = sender;
            InitializeComponent();
            label5.Text = host;
            label6.Text = port;
            label7.Text = user;
            label8.Text = password;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string host =textBox1.Text;
            string port =textBox2.Text;
            string user =textBox3.Text;
            string password =textBox4.Text;
            _delegate(host, port, user, password);
            this.Close();
        }
    }
}
