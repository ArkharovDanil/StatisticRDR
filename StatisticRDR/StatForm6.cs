using System;
using System.Collections.Generic;
using System.Linq;
using ManagedIrbis;
using ManagedIrbis.Batch;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Threading;

/// <summary>
/// Первая сделанная форма, на неё в целом можно конечно ориентироваться, но не очень то и нужно, после 
/// разработки будет необходимо провести рефакторинг кода, уже очевидно, что четыре созданных одинаковых формы всего лишь с разными поисками 
/// это неадекватно :)
/// </summary>
namespace StatisticRDR
{
    public class StatForm6
    {
        string ConnectionString;
        private static IrbisConnection Connection;
        TextBox _textBoxAnswer;
        StatForm6(List<string> rl, List<string>cl , TextBox textBoxAnswer, string CS)
        {
          
            SetRowsList(rl);
            SetColumnsList(cl);
            int[][] array = new int[rl.Count][];
            _textBoxAnswer = textBoxAnswer;
            ConnectionString = CS;
        }
        
        
        private List<string> rowsList;
        private List<string> columnsList;
        List<string> GetRowsList()
        {
            return rowsList;
            
        }
        void SetRowsList(List<string> rl)
        {
            rowsList = rl;
        }
        List<string> GetColumnsList()
        {
            return columnsList;
        }
        void SetColumnsList(List<string> cl)
        {
            columnsList = cl;
        }
        /// <summary>
        /// вход в алгоритм
        /// </summary>
        /// <param name="library"></param>
        /// <param name="categories"></param>
        /// <param name="date"></param>
        /// <param name="refreshPercent"></param>
        /// <param name="CS"></param>
        /// <returns></returns>
        static public int[] SearchForTable(string library,string[] categories,string date,string CS)
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
                        string searchString = "VS=" + date + "/" + library;
                        ///
                        /// Очевидно, что для поиска используется внутренний в ирбисе префикс. 
                        /// Префикс соответствует префиксу того словаря поиск по которому мы ведём.
                        ///


                        int[] found = Connection.Search(searchString);
                        answer = found.Count();
                        categoriesArray[0] = answer;
                        
                        BatchRecordReader batch = new BatchRecordReader(Connection, Connection.Database, 5, found);

                        ;
                        
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
     /// <param name="refreshPercent"></param>
     /// <param name="CS"></param>
        static public void CreateTable(string[] library,string[] categories,string date,string CS)
        {   
            int[][] tableForLibraries = new int[library.Count()][];
            for (int i=0;i<library.Count();i++)
            {
               // AddPercentAtTextBox(i, library.Count(), textBox);
                tableForLibraries[i] = SearchForTable(library[i],categories,date,CS);
            }
            ShowInExcelByCreating(tableForLibraries, library,categories, date);
          //  delegate2();
            MessageBox.Show("Сделано");
        }
        static public void ShowInExcel(int[][] tableForLibraries)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                // Задаем расширение имени файла по умолчанию (открывается папка с программой)
                ofd.DefaultExt = "*.xls;*.xlsx";
                // Задаем строку фильтра имен файлов, которая определяет варианты
                ofd.Filter = "файл Excel (data.xls)|*.xls";
                // Задаем заголовок диалогового окна
                ofd.Title = "Выберите файл базы данных";
                ofd.ShowDialog();
                int RowsCount = tableForLibraries.GetUpperBound(0) + 1;
                int ColumnsCount = tableForLibraries[0].Length;
                Excel.Application ObjWorkExcel = new Excel.Application();
                Excel.Workbook ObjWorkBook = ObjWorkExcel.Workbooks.Open(ofd.FileName);
                Excel.Worksheet ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[1]; //получить 1-й лист
                for (int i = 0; i < RowsCount; i++) //по всем строкам
                    for (int j = 0; j < ColumnsCount; j++) // по всем столбцам
                        ObjWorkSheet.Cells[i + 7, j + 3] = tableForLibraries[i][j]; //записываем данные
                                                                                    // ObjWorkBook.Close(true, Type.Missing, Type.Missing); //закрыть и сохранить
                ObjWorkExcel.Visible = true;
                ObjWorkExcel.UserControl = true;
                
                //ObjWorkExcel.Quit(); // выйти из Excel
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }


        }
        static public void ShowInExcelByCreating(int[][] tableForLibraries, string[] library, string[] categories, string date)
        {
            string path= "C:\\tempStatRDR";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = "C:\\tempStatRDR\\"+DateTime.Now.ToLongDateString()+"-"+ReturnAsNormalDate(date)+".xls";

            try
            {
                var excel = new Excel.Application();

                var workBooks = excel.Workbooks;
                var workBook = workBooks.Add();
                var workSheet = (Excel.Worksheet)excel.ActiveSheet;

                int RowsCount = tableForLibraries.GetUpperBound(0) + 1;
                int ColumnsCount = tableForLibraries[0].Length;
                workSheet.Cells[1, "A"] = "Распределение книговыдач по категориям читателей  и местам выдач за "+ReturnAsNormalDate(date);
                for (int i = 0;i<RowsCount;i++)
                {
                    workSheet.Cells[i + 3, "A"] = library[i];
                }
                for (int i = 0; i < ColumnsCount; i++)
                {
                    workSheet.Cells[2, i+2] = categories[i];
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
        static private int Search(MarcRecord record,string date,string library)
        {
            
            foreach (string overlapping in record.FMA(40))
            {
                MessageBox.Show(overlapping);
            }


            return 0;
        }
        static string ReturnAsNormalDate(string date)
        {
            string answer = date[6].ToString() + date[7].ToString() + "." + date[4].ToString() + date[5].ToString() + "." + date[0].ToString() + date[1].ToString() + date[2].ToString() + date[3].ToString();

            return answer;
        }
    }
}
