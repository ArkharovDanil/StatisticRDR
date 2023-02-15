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
    public partial class Form3 : Form
    {
        string[] strings;

        public Form3()
        {
            InitializeComponent();
        }

        public Form3(string[] Strings)
        {
            InitializeComponent();
            strings = Strings;
            InitializeDataGridView1();
        }

        public void InitializeDataGridView1()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Список категорий или библиотек");
            foreach(string line in strings)
            {           
                DataRow r = dt.NewRow();
                r["Список категорий или библиотек"] = line;
                dt.Rows.Add(r);
            }
            dataGridView1.DataSource = dt;         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
