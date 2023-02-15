using System;
using System.Collections.Generic;
using System.Linq;
using ManagedIrbis;
using ManagedIrbis.Batch;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading;

namespace StatisticRDR
{
    public class TempStatForm6 : StatForm
    {
        public TempStatForm6(List<string> rl, List<string> cl, TextBox textBoxAnswer, string connectionString) : base(rl, cl, textBoxAnswer, connectionString)
        {
        }
    }
}
