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
    public partial class Form6 : Form
    {
        MyDelegateBool _delegate;
        bool _countAsSum;
        public Form6()
        {
            InitializeComponent();
        }
        public Form6(bool isFirst, MyDelegateBool @delegate)
        {
            InitializeComponent();
            _delegate = @delegate;
            _countAsSum = isFirst;
            if (_countAsSum == true)
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

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
                _countAsSum = true;
            else
                _countAsSum = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _delegate(_countAsSum);
        }
    }
}
