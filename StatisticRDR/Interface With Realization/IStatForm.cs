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
     interface IStatForm
    {
        int[] SearchForTable(string library, string date, TextBox textBox);
        void CreateTable(string library, string date, TextBox textBox);
        void ShowInExcel(int[][] tableForLibraries);
    }
}
