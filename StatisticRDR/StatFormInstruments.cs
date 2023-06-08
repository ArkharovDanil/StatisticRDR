using System;
using System.Linq;
using System.Collections.Generic;
using ManagedIrbis;
using ManagedIrbis.Batch;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace StatisticRDR
{
    internal static class StatFormInstruments
    {

        static public bool isValidDate(string[] dates, string currentDate)
        {
            try
            {
                int currentDateInt = Convert.ToInt32(currentDate);
                int currentFirstDate = Convert.ToInt32(dates[0]);
                int currentSecondDate = Convert.ToInt32(dates[30]);
                if (currentDateInt >= currentFirstDate && currentDateInt <= currentSecondDate)
                {
                    return true;
                }
            }
            catch
            {
                
            }

            return false;
        }
        static public bool isFourtyField(RecordField field1)
        {
            string field = field1.Path;

            if (field[0] == '4' && field[1] == '0' && field[2] == '/')
            {
                return true;
            }
            else return false;
        }
        static public string[] MakeDatesFromMonth(string month)
        {
            string[] days = new string[31];

            for (int i = 0; i < 9; i++)
            {
                days[i] = month + "0" + (i + 1).ToString();
            }
            for (int i = 9; i < 31; i++)
            {
                days[i] = month + (i + 1).ToString();
            }
            return days;
        }
        static string ReturnAsNormalDate(string date)
        {
            string answer;
            if (date.Length == 6)
            {
                answer = date[4].ToString() + date[5].ToString() + "." + date[0].ToString() + date[1].ToString() + date[2].ToString() + date[3].ToString();
                return answer;
            }
            if (date.Length == 8)
            {
                answer = date[6].ToString() + date[7].ToString() + "." + date[4].ToString() + date[5].ToString() + "." + date[0].ToString() + date[1].ToString() + date[2].ToString() + date[3].ToString();
                return answer;
            }
            return "";
        }
        static public void ShowInExcelByCreating(int[][] tableForLibraries, string[] library, string[] categories, string date, bool countAsSum,string path, string name)
        {
             if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = "C:\\tempStatRDR\\StatForm6\\" + DateTime.Now.ToLongDateString() + "-" + ReturnAsNormalDate(date) + ".xls";

            try
            {
                var excel = new Excel.Application();

                var workBooks = excel.Workbooks;
                var workBook = workBooks.Add();
                var workSheet = (Excel.Worksheet)excel.ActiveSheet;

                int RowsCount = tableForLibraries.GetUpperBound(0) + 1;
                int ColumnsCount = tableForLibraries[0].Length;
                //  int[] sum = new int[RowsCount];
                if (countAsSum)
                {
                    for (int i = 0; i < RowsCount; i++)
                    {
                        int currentSum = 0;
                        for (int j = 1; j < ColumnsCount; j++) // по всем столбцам
                        {
                            currentSum += tableForLibraries[i][j];
                        }
                        tableForLibraries[i][0] = currentSum;
                    }//по всем строкам
                }

                workSheet.Cells[1, "A"] = name + ReturnAsNormalDate(date);
                for (int i = 0; i < RowsCount; i++)
                {
                    workSheet.Cells[i + 3, "A"] = library[i];
                }
                for (int i = 0; i < ColumnsCount; i++)
                {
                    workSheet.Cells[2, i + 2] = categories[i];
                }
                for (int i = 0; i < RowsCount; i++) //по всем строкам
                    for (int j = 0; j < ColumnsCount; j++) // по всем столбцам
                    {
                        workSheet.Cells[i + 3, j + 2] = tableForLibraries[i][j];
                    }

                workBook.SaveAs(fileName);
                workBook.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.ToString());
            }

            MessageBox.Show("Файл " + fileName + " записан успешно!");

        }
        static public int[][] SearchAllForTable(string[] libraries, string[] categories, string date, string CS, bool isFirst,string prefix,IrbisConnection Connection)
        {
            List<(int, RecordField)> exceptions = new List<(int, RecordField)>();
            int counter = 0;
            int[,] tableForLibraries = new int[libraries.Count(), categories.Count()];
            for (int i = 0; i < libraries.Count(); i++)
            {
                for (int j = 0; j < categories.Count(); j++)
                {
                    tableForLibraries[i, j] = 0;
                }
            }
            int answer = 0;
            string connectionString = CS;
            try
            {

                {
                    using (Connection = new IrbisConnection())
                    {



                        Connection.ParseConnectionString(connectionString);
                        Connection.Connect();
                        //MarcRecord record = new MarcRecord();
                        string searchString = prefix + date + "$";
                        ///
                        /// Очевидно, что для поиска используется внутренний в ирбисе префикс. 
                        /// Префикс соответствует префиксу того словаря поиск по которому мы ведём.
                        ///


                        int[] found = Connection.Search(searchString);
                        answer = found.Count();
                        for (int i = 0; i < libraries.Length; i++)
                        {
                            tableForLibraries[i, 0] = 0;
                        }
                        // categoriesArray[0] = answer;

                        BatchRecordReader batch = new BatchRecordReader(Connection, Connection.Database, 5, found);

                        ;
                        if (isFirst)
                        {
                            foreach (MarcRecord record in batch)
                            {
                                string[] dates = StatFormInstruments.MakeDatesFromMonth(date);
                                string currentLibrary = "";
                                string currentDate = "";
                                foreach (var field in record.Fields)
                                {
                                    try
                                    {
                                        if (StatFormInstruments.isFourtyField(field))
                                        {
                                            foreach (var subfield in field.SubFields)
                                            {
                                                string current = subfield.Value;
                                                if (subfield.CodeString == "v")
                                                    currentLibrary = current;
                                                if (subfield.CodeString == "d")
                                                {
                                                    currentDate = current;
                                                    try
                                                    {
                                                        int t = Convert.ToInt32(currentDate);
                                                    }
                                                    catch
                                                    {
                                                        exceptions.Add((record.Mfn, field));
                                                    }

                                                }



                                            }

                                            if (currentDate != "" && currentLibrary != "" && StatFormInstruments.isValidDate(dates, currentDate))
                                            {
                                                for (int i = 0; i < libraries.Length; i++)
                                                {
                                                    for (int j = 0; j < dates.Length; j++)
                                                    {
                                                        if (currentDate == dates[j] && currentLibrary.ToUpper() == libraries[i].ToUpper())
                                                        {
                                                            for (int k = 1; k < categories.Length; k++)
                                                            {
                                                                string str;
                                                                if (record.HaveField(50))
                                                                {
                                                                    str = record.FMA(50)[0];
                                                                }
                                                                else
                                                                {
                                                                    str = "";
                                                                }
                                                                if (str == categories[k])
                                                                {
                                                                    tableForLibraries[i, k]++;
                                                                }

                                                            }

                                                        }
                                                    }


                                                }

                                            }
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.ToString());
                                    }

                                }
                            }
                        }
                        else
                        {
                            foreach (MarcRecord record in batch)
                            {
                                string[] dates = StatFormInstruments.MakeDatesFromMonth(date);
                                string currentLibrary = "";
                                string currentDate = "";
                                foreach (var field in record.Fields)
                                {
                                    try
                                    {
                                        if (StatFormInstruments.isFourtyField(field))
                                        {
                                            foreach (var subfield in field.SubFields)
                                            {
                                                string current = subfield.Value;
                                                if (subfield.CodeString == "v")
                                                    currentLibrary = current;
                                                if (subfield.CodeString == "d")
                                                {
                                                    currentDate = current;
                                                    try
                                                    {
                                                        int t = Convert.ToInt32(currentDate);
                                                    }
                                                    catch
                                                    {
                                                        exceptions.Add((record.Mfn, field));
                                                    }

                                                }

                                            }
                                            try
                                            {
                                                int t = Convert.ToInt32(currentDate);
                                            }
                                            catch
                                            {
                                                counter++;
                                            }

                                            if (currentDate != "" && currentLibrary != "" && StatFormInstruments.isValidDate(dates, currentDate))
                                            {
                                                for (int i = 0; i < libraries.Length; i++)
                                                {
                                                    for (int j = 0; j < dates.Length; j++)
                                                    {
                                                        if (currentDate == dates[j] && currentLibrary.ToUpper() == libraries[i].ToUpper())
                                                        {
                                                            for (int k = 1; k < categories.Length; k++)
                                                            {
                                                                foreach (string str in record.FMA(50))
                                                                {

                                                                    if (str == categories[k])
                                                                    {
                                                                        tableForLibraries[i, k]++;
                                                                    }
                                                                }
                                                            }

                                                        }
                                                    }


                                                }

                                            }
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.ToString());
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
            int[][] answerTableForLibraries = new int[libraries.Count()][];

            for (int i = 0; i < libraries.Count(); i++)
            {
                int[] answerCurrent = new int[categories.Count()];
                for (int j = 0; j < categories.Count(); j++)
                {
                    answerCurrent[j] = tableForLibraries[i, j];
                }
                answerTableForLibraries[i] = answerCurrent;
            }
            return answerTableForLibraries;
        }
        static public int[] SearchForTable(string library, string[] categories, string date, string CS, bool isFirst,string prefix,IrbisConnection Connection)
        {
            int[] categoriesArray = new int[categories.Length];
            int answer = 0;
            string connectionString = CS;
            try
            {

                {
                    using (Connection = new IrbisConnection())
                    {



                        Connection.ParseConnectionString(connectionString);
                        Connection.Connect();
                        //MarcRecord record = new MarcRecord();
                        string searchString = prefix + date + "/" + library;
                        ///
                        /// Очевидно, что для поиска используется внутренний в ирбисе префикс. 
                        /// Префикс соответствует префиксу того словаря поиск по которому мы ведём.
                        ///


                        int[] found = Connection.Search(searchString);
                        answer = found.Count();
                        categoriesArray[0] = answer;

                        BatchRecordReader batch = new BatchRecordReader(Connection, Connection.Database, 5, found);

                        ;
                        if (!isFirst)
                        {
                            foreach (MarcRecord record in batch)
                            {
                                foreach (string str in record.FMA(50))
                                {

                                    for (int i = 1; i < categories.Length; i++)
                                    {


                                        if (str == categories[i])
                                        {
                                            categoriesArray[i]++;
                                        }
                                    }

                                }

                            }
                        }
                        else
                        {
                            foreach (MarcRecord record in batch)
                            {
                                string str;
                                if (record.HaveField(50))
                                {
                                    str = record.FMA(50)[0];
                                }
                                else
                                {
                                    str = "";
                                }

                                for (int i = 1; i < categories.Length; i++)
                                {
                                    if (str == categories[i])
                                    {
                                        categoriesArray[i]++;
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

            return categoriesArray;
        }

    }
}
