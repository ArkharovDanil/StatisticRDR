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
    public partial class Form2 : Form
    {
        MyDelegate _delegate;
        string[] strings;
        DataTable dt= new DataTable();
        /// <summary>
        /// Флаг, обозначающий с каким списком мы работаем 0-Библиотеки,1-категории
        /// </summary>
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(string[] Strings, MyDelegate sender)
        {
            _delegate = sender;
            InitializeComponent();
            strings = Strings;
            InitializeDataGridView1();
        }
        public void InitializeDataGridView1()
        {
            dt.Reset();
            dt.Columns.Add("Список категорий или библиотек");
            foreach (string line in strings)
            {
                if (line != "\n" && line != "\r" && line != "\t" && line != "")
                {
                    DataRow r = dt.NewRow();
                    r["Список категорий или библиотек"] = line;
                    dt.Rows.Add(r);
                }
            }
            dataGridView1.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InitializeDataGridView1();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // string[] str = dataGridView1.Rows.ToString().Split('\n');
            string[] str = new string[dataGridView1.Rows.Count-1];
            for (int i= 0; i < dataGridView1.Rows.Count-1;i++)
            {
               string s = dataGridView1.Rows[i].Cells[0].Value.ToString();
                str[i] = s;
            }           
            _delegate(str);


        }
    }
}
