﻿using System;
using System.Linq;
using ManagedIrbis;
using ManagedIrbis.Batch;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace StatisticRDR
{
    public class StatForm11
    {
        string ConnectionString;
        private static IrbisConnection Connection;

        /// <summary>
        /// вход в алгоритм
        /// </summary>
        /// <param name="library"></param>
        /// <param name="categories"></param>
        /// <param name="date"></param>
        /// <param name="CS"></param>
        /// <returns></returns>
        static public int[] SearchForTable(string library, string[] categories, string date, string CS, bool isFirst)
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
                        string searchString = "RD=" + date + "-" + library;
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
                                if (record.FMA(50)[0] != null)
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
                Console.WriteLine(exception);
            }

            return categoriesArray;
        }

        /// <summary>
        /// поиск для таблицы?
        /// </summary>
        /// <param name="library"></param>
        /// <param name="categories"></param>
        /// <param name="date"></param>
        /// <param name="CS"></param>
        static public void CreateTable(string[] library, string[] categories, string date, string CS, bool isFirst, bool countAsSum)
        {
            int[][] tableForLibraries = new int[library.Count()][];
            for (int i = 0; i < library.Count(); i++)
            {
                tableForLibraries[i] = SearchForTable(library[i], categories, date, CS, isFirst);
            }
            ShowInExcelByCreating(tableForLibraries, library, categories, date,countAsSum);
            MessageBox.Show("Сделано");
        }

        static public void ShowInExcelByCreating(int[][] tableForLibraries, string[] library, string[] categories, string date, bool countAsSum)
        {
            string path = "C:\\tempStatRDR\\StatForm11";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = "C:\\tempStatRDR\\StatForm11\\" + DateTime.Now.ToLongDateString() + "-" + ReturnAsNormalDate(date) + ".xls";

            try
            {
                var excel = new Excel.Application();

                var workBooks = excel.Workbooks;
                var workBook = workBooks.Add();
                var workSheet = (Excel.Worksheet)excel.ActiveSheet;

                int RowsCount = tableForLibraries.GetUpperBound(0) + 1;
                int ColumnsCount = tableForLibraries[0].Length;
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

                workSheet.Cells[1, "A"] = "Распределение кол-ва записанных читателей по категориям читателей  и местам выдач за " + ReturnAsNormalDate(date);
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
        static string ReturnAsNormalDate(string date)
        {
            string answer = date[6].ToString() + date[7].ToString() + "." + date[4].ToString() + date[5].ToString() + "." + date[0].ToString() + date[1].ToString() + date[2].ToString() + date[3].ToString();

            return answer;
        }
    }
}
