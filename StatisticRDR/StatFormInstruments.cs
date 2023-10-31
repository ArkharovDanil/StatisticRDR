using ManagedIrbis;
using ManagedIrbis.Batch;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace StatisticRDR
{
    internal static class StatFormInstruments
    {
        public static bool isFourtyField(RecordField field1)
        {
            string path = field1.Path;
            return ((path[0] != '4' || path[1] != '0' ? true : path[2] != '/') ? false : true);
        }

        public static bool isValidDate(string[] dates, string currentDate)
        {
            bool flag;
            try
            {
                int num = Convert.ToInt32(currentDate);
                int num1 = Convert.ToInt32(dates[0]);
                int num2 = Convert.ToInt32(dates[30]);
                if ((num < num1 ? false : num <= num2))
                {
                    flag = true;
                    return flag;
                }
            }
            catch
            {
            }
            flag = false;
            return flag;
        }

        public static string[] MakeDatesFromMonth(string month)
        {
            int num;
            string[] strArrays = new string[31];
            for (int i = 0; i < 9; i++)
            {
                num = i + 1;
                strArrays[i] = string.Concat(month, "0", num.ToString());
            }
            for (int j = 9; j < 31; j++)
            {
                num = j + 1;
                strArrays[j] = string.Concat(month, num.ToString());
            }
            return strArrays;
        }

        private static string ReturnAsNormalDate(string date)
        {
            char chr;
            string str;
            if (date.Length == 6)
            {
                string[] strArrays = new string[7];
                chr = date[4];
                strArrays[0] = chr.ToString();
                chr = date[5];
                strArrays[1] = chr.ToString();
                strArrays[2] = ".";
                chr = date[0];
                strArrays[3] = chr.ToString();
                chr = date[1];
                strArrays[4] = chr.ToString();
                chr = date[2];
                strArrays[5] = chr.ToString();
                chr = date[3];
                strArrays[6] = chr.ToString();
                str = string.Concat(strArrays);
            }
            else if (date.Length != 8)
            {
                str = "";
            }
            else
            {
                string[] str1 = new string[10];
                chr = date[6];
                str1[0] = chr.ToString();
                chr = date[7];
                str1[1] = chr.ToString();
                str1[2] = ".";
                chr = date[4];
                str1[3] = chr.ToString();
                chr = date[5];
                str1[4] = chr.ToString();
                str1[5] = ".";
                chr = date[0];
                str1[6] = chr.ToString();
                chr = date[1];
                str1[7] = chr.ToString();
                chr = date[2];
                str1[8] = chr.ToString();
                chr = date[3];
                str1[9] = chr.ToString();
                str = string.Concat(str1);
            }
            return str;
        }

        public static int[][] SearchAllForTable(string[] libraries, string[] categories, string date, string CS, bool isFirst, string prefix, IrbisConnection Connection)
        {
            string str;
            List<ValueTuple<int, RecordField>> valueTuples = new List<ValueTuple<int, RecordField>>();
            int num = 0;
            int[,] numArray = new int[libraries.Count<string>(), categories.Count<string>()];
            for (int i = 0; i < libraries.Count<string>(); i++)
            {
                for (int j = 0; j < categories.Count<string>(); j++)
                {
                    numArray[i, j] = 0;
                }
            }
            int num1 = 0;
            string cS = CS;
            try
            {
                IrbisConnection irbisConnection = new IrbisConnection();
                Connection = irbisConnection;
                using (irbisConnection)
                {
                    Connection.ParseConnectionString(cS);
                    Connection.Connect();
                    string str1 = string.Concat(prefix, date, "$");
                    int[] numArray1 = Connection.Search(str1);
                    num1 = numArray1.Count<int>();
                    for (int k = 0; k < (int)libraries.Length; k++)
                    {
                        numArray[k, 0] = 0;
                    }
                    BatchRecordReader batchRecordReaders = new BatchRecordReader(Connection, Connection.Database, 5, numArray1);
                    if (!isFirst)
                    {
                        foreach (MarcRecord batchRecordReader in batchRecordReaders)
                        {
                            string[] strArrays = StatFormInstruments.MakeDatesFromMonth(date);
                            string str2 = "";
                            string str3 = "";
                            foreach (RecordField field in batchRecordReader.Fields)
                            {
                                try
                                {
                                    if (StatFormInstruments.isFourtyField(field))
                                    {
                                        foreach (SubField subField in field.SubFields)
                                        {
                                            string value = subField.Value;
                                            if (subField.CodeString == "v")
                                            {
                                                str2 = value;
                                            }
                                            if (subField.CodeString == "d")
                                            {
                                                str3 = value;
                                                try
                                                {
                                                    Convert.ToInt32(str3);
                                                }
                                                catch
                                                {
                                                    valueTuples.Add(new ValueTuple<int, RecordField>(batchRecordReader.Mfn, field));
                                                }
                                            }
                                        }
                                        try
                                        {
                                            Convert.ToInt32(str3);
                                        }
                                        catch
                                        {
                                            num++;
                                        }
                                        if ((!(str3 != "") || !(str2 != "") ? false : StatFormInstruments.isValidDate(strArrays, str3)))
                                        {
                                            for (int l = 0; l < (int)libraries.Length; l++)
                                            {
                                                for (int m = 0; m < (int)strArrays.Length; m++)
                                                {
                                                    if ((str3 != strArrays[m] ? false : str2.ToUpper() == libraries[l].ToUpper()))
                                                    {
                                                        for (int n = 1; n < (int)categories.Length; n++)
                                                        {
                                                            string[] strArrays1 = batchRecordReader.FMA(50);
                                                            for (int o = 0; o < (int)strArrays1.Length; o++)
                                                            {
                                                                if (strArrays1[o] == categories[n])
                                                                {
                                                                    numArray[l, n]++;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                catch (Exception exception)
                                {
                                    MessageBox.Show(exception.ToString());
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (MarcRecord marcRecord in batchRecordReaders)
                        {
                            string[] strArrays2 = StatFormInstruments.MakeDatesFromMonth(date);
                            string str4 = "";
                            string str5 = "";
                            foreach (RecordField recordField in marcRecord.Fields)
                            {
                                try
                                {
                                    if (StatFormInstruments.isFourtyField(recordField))
                                    {
                                        foreach (SubField subField1 in recordField.SubFields)
                                        {
                                            string value1 = subField1.Value;
                                            if (subField1.CodeString == "v")
                                            {
                                                str4 = value1;
                                            }
                                            if (subField1.CodeString == "d")
                                            {
                                                str5 = value1;
                                                try
                                                {
                                                    Convert.ToInt32(str5);
                                                }
                                                catch
                                                {
                                                    valueTuples.Add(new ValueTuple<int, RecordField>(marcRecord.Mfn, recordField));
                                                }
                                            }
                                        }
                                        if ((!(str5 != "") || !(str4 != "") ? false : StatFormInstruments.isValidDate(strArrays2, str5)))
                                        {
                                            for (int p = 0; p < (int)libraries.Length; p++)
                                            {
                                                for (int q = 0; q < (int)strArrays2.Length; q++)
                                                {
                                                    if ((str5 != strArrays2[q] ? false : str4.ToUpper() == libraries[p].ToUpper()))
                                                    {
                                                        for (int r = 1; r < (int)categories.Length; r++)
                                                        {
                                                            str = (!marcRecord.HaveField(50) ? "" : marcRecord.FMA(50)[0]);
                                                            if (str == categories[r])
                                                            {
                                                                numArray[p, r]++;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                catch (Exception exception1)
                                {
                                    MessageBox.Show(exception1.ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.ToString());
            }
            int[][] numArray2 = new int[libraries.Count<string>()][];
            for (int s = 0; s < libraries.Count<string>(); s++)
            {
                int[] numArray3 = new int[categories.Count<string>()];
                for (int t = 0; t < categories.Count<string>(); t++)
                {
                    numArray3[t] = numArray[s, t];
                }
                numArray2[s] = numArray3;
            }
            return numArray2;
        }

        public static int[][] SearchAllForTableBySum(string[] libraries, string[] categories, string date, string CS, bool isFirst, string prefix, IrbisConnection Connection, string delimiter)
        {
            int[][] numArray = new int[libraries.Count<string>()][];
            for (int i = 0; i < libraries.Count<string>(); i++)
            {
                int[] numArray1 = new int[categories.Count<string>()];
                for (int j = 0; j < categories.Count<string>(); j++)
                {
                    numArray1[j] = 0;
                }
                numArray[i] = numArray1;
            }
            numArray = StatFormInstruments.SearchForTableSecondWayForMonth(libraries, categories, date, CS, isFirst, prefix, Connection, delimiter);
            return numArray;
        }

        public static int[][] SearchAllForThreadMethodForMonth(string[] libraries, string[] categories, string date, string CS, bool isFirst, string prefix, IrbisConnection Connection, string delimiter)
        {
            WorkingWithThreads workingWithThread = new WorkingWithThreads(libraries, categories, date, CS, isFirst, prefix, Connection, delimiter);
            return workingWithThread.ReturnSumMatrixAsync().Result;
        }

        public static int[] SearchForTable(string library, string[] categories, string date, string CS, bool isFirst, string prefix, IrbisConnection Connection, string delimiter)
        {
            string str;
            int[] numArray = new int[(int)categories.Length];
            string cS = CS;
            try
            {
                IrbisConnection irbisConnection = new IrbisConnection();
                Connection = irbisConnection;
                using (irbisConnection)
                {
                    Connection.ParseConnectionString(cS);
                    Connection.Connect();
                    string str1 = string.Concat(prefix, date, delimiter, library);
                    int[] numArray1 = Connection.Search(str1);
                    numArray[0] = numArray1.Count<int>();
                    BatchRecordReader batchRecordReaders = new BatchRecordReader(Connection, Connection.Database, 5, numArray1);
                    if (isFirst)
                    {
                        foreach (MarcRecord batchRecordReader in batchRecordReaders)
                        {
                            str = (!batchRecordReader.HaveField(50) ? "" : batchRecordReader.FMA(50)[0]);
                            for (int i = 1; i < (int)categories.Length; i++)
                            {
                                if (str == categories[i])
                                {
                                    numArray[i]++;
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (MarcRecord marcRecord in batchRecordReaders)
                        {
                            string[] strArrays = marcRecord.FMA(50);
                            for (int j = 0; j < (int)strArrays.Length; j++)
                            {
                                string str2 = strArrays[j];
                                for (int k = 1; k < (int)categories.Length; k++)
                                {
                                    if (str2 == categories[k])
                                    {
                                        numArray[k]++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
            return numArray;
        }

        public static int[][] SearchForTableSecondWay(string[] libraries, string[] categories, string date, string CS, bool isFirst, string prefix, IrbisConnection Connection, string delimiter)
        {
            string str;
            int[][] numArray = new int[(int)libraries.Length][];
            string cS = CS;
            int num = -1;
            string[] strArrays = libraries;
            for (int i = 0; i < (int)strArrays.Length; i++)
            {
                string str1 = strArrays[i];
                int[] numArray1 = new int[(int)categories.Length];
                num++;
                try
                {
                    IrbisConnection irbisConnection = new IrbisConnection();
                    Connection = irbisConnection;
                    using (irbisConnection)
                    {
                        Connection.ParseConnectionString(cS);
                        Connection.Connect();
                        string str2 = string.Concat(prefix, date, delimiter, str1);
                        int[] numArray2 = Connection.Search(str2);
                        numArray1[0] = numArray2.Count<int>();
                        BatchRecordReader batchRecordReaders = new BatchRecordReader(Connection, Connection.Database, 200, numArray2);
                        if (isFirst)
                        {
                            foreach (MarcRecord batchRecordReader in batchRecordReaders)
                            {
                                str = (!batchRecordReader.HaveField(50) ? "" : batchRecordReader.FMA(50)[0]);
                                for (int j = 1; j < (int)categories.Length; j++)
                                {
                                    if (str == categories[j])
                                    {
                                        numArray1[j]++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (MarcRecord marcRecord in batchRecordReaders)
                            {
                                string[] strArrays1 = marcRecord.FMA(50);
                                for (int k = 0; k < (int)strArrays1.Length; k++)
                                {
                                    string str3 = strArrays1[k];
                                    for (int l = 1; l < (int)categories.Length; l++)
                                    {
                                        if (str3 == categories[l])
                                        {
                                            numArray1[l]++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                }
                numArray[num] = numArray1;
            }
            return numArray;
        }

        public static int[][] SearchForTableSecondWayForMonth(string[] libraries, string[] categories, string date, string CS, bool isFirst, string prefix, IrbisConnection Connection, string delimiter)
        {
            string str;
            int[][] numArray = new int[(int)libraries.Length][];
            string cS = CS;
            int num = -1;
            string[] strArrays = libraries;
            for (int i = 0; i < (int)strArrays.Length; i++)
            {
                string str1 = strArrays[i];
                int[] numArray1 = new int[(int)categories.Length];
                num++;
                try
                {
                    IrbisConnection irbisConnection = new IrbisConnection();
                    Connection = irbisConnection;
                    using (irbisConnection)
                    {
                        Connection.ParseConnectionString(cS);
                        Connection.Connect();
                        string str2 = string.Concat(prefix, date, delimiter, str1);
                        List<int> nums = new List<int>();
                        string[] strArrays1 = StatFormInstruments.MakeDatesFromMonth(date);
                        for (int j = 0; j < (int)strArrays1.Length; j++)
                        {
                            string str3 = strArrays1[j];
                            str2 = string.Concat(prefix, str3, delimiter, str1);
                            int[] numArray2 = Connection.Search(str2);
                            for (int k = 0; k < (int)numArray2.Length; k++)
                            {
                                nums.Add(numArray2[k]);
                            }
                        }
                        int[] array = nums.ToArray();
                        numArray1[0] = array.Count<int>();
                        BatchRecordReader batchRecordReaders = new BatchRecordReader(Connection, Connection.Database, 200, array);
                        if (isFirst)
                        {
                            foreach (MarcRecord batchRecordReader in batchRecordReaders)
                            {
                                str = (!batchRecordReader.HaveField(50) ? "" : batchRecordReader.FMA(50)[0]);
                                for (int l = 1; l < (int)categories.Length; l++)
                                {
                                    if (str == categories[l])
                                    {
                                        numArray1[l]++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (MarcRecord marcRecord in batchRecordReaders)
                            {
                                string[] strArrays2 = marcRecord.FMA(50);
                                for (int m = 0; m < (int)strArrays2.Length; m++)
                                {
                                    string str4 = strArrays2[m];
                                    for (int n = 1; n < (int)categories.Length; n++)
                                    {
                                        if (str4 == categories[n])
                                        {
                                            numArray1[n]++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                }
                numArray[num] = numArray1;
            }
            return numArray;
        }

        public static void ShowInExcelByCreating(int[][] tableForLibraries, string[] library, string[] categories, string date, bool countAsSum, string path, string name)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string[] longDateString = new string[] { path, null, null, null, null };
            longDateString[1] = DateTime.Now.ToLongDateString();
            longDateString[2] = "-";
            longDateString[3] = StatFormInstruments.ReturnAsNormalDate(date);
            longDateString[4] = ".xls";
            string str = string.Concat(longDateString);
            try
            {
                Application variable = (Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
                Workbook variable1 = variable.Workbooks.Add(Type.Missing);
                Worksheet activeSheet = (Worksheet)((dynamic)variable.ActiveSheet);
                int upperBound = tableForLibraries.GetUpperBound(0) + 1;
                int length = (int)tableForLibraries[0].Length;
                if (countAsSum)
                {
                    for (int i = 0; i < upperBound; i++)
                    {
                        int num = 0;
                        for (int j = 1; j < length; j++)
                        {
                            num += tableForLibraries[i][j];
                        }
                        tableForLibraries[i][0] = num;
                    }
                }
                activeSheet.Cells[1, "A"] = string.Concat(name, StatFormInstruments.ReturnAsNormalDate(date));
                for (int k = 0; k < upperBound; k++)
                {
                    activeSheet.Cells[k + 3, "A"] = library[k];
                }
                for (int l = 0; l < length; l++)
                {
                    activeSheet.Cells[2, l + 2] = categories[l];
                }
                for (int m = 0; m < upperBound; m++)
                {
                    for (int n = 0; n < length; n++)
                    {
                        activeSheet.Cells[m + 3, n + 2] = tableForLibraries[m][n];
                    }
                }
                variable1.SaveAs(str, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                variable1.Close(Type.Missing, Type.Missing, Type.Missing);
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                MessageBox.Show(string.Concat("Ошибка: ", exception.ToString()));
            }
            MessageBox.Show(string.Concat("Файл ", str, " записан успешно!"));
        }
    }
}