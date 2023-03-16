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
    public partial class Form5 : Form
    {
        MyDelegateBool _delegate;
        bool _isFirst;
        public Form5()
        {
            InitializeComponent();
        }
        public Form5(bool isFirst, MyDelegateBool @delegate)
        {
            InitializeComponent();
            _delegate = @delegate;
            _isFirst = isFirst;
            if (_isFirst == true)
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;

            }
            else
            {
                radioButton2.Checked = true;
                radioButton1.Checked = false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked==true)
            _isFirst = true;
            else
            _isFirst = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _delegate(_isFirst);
        }
    }
}
