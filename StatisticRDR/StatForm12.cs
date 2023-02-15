using System;
using System.Collections.Generic;
using System.Linq;
using ManagedIrbis;
using ManagedIrbis.Batch;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace StatisticRDR
{
    public class StatForm12
    {
        string ConnectionString;
        private static IrbisConnection Connection;
        TextBox _textBoxAnswer;
        StatForm12(List<string> rl, List<string> cl, TextBox textBoxAnswer, string CS)
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
        static void AddStringAtTextbox(string s, TextBox textBox)
        {
            textBox.Text += "     " + s + "    ";
            MessageBox.Show("Добавлена строка");
        }
        static void AddPercentAtTextBox(double cur, double max, TextBox textBox)
        {
            textBox.Text = ((cur / max) * 100).ToString() + "%";
            textBox.Update();
        }
        static void UpdatePercentAtTextBox(double x, TextBox textBox)
        {
            double t = Convert.ToDouble(textBox.Text);
            textBox.Text = (t + x * 100).ToString();
            textBox.Update();
        }
        static public int[] SearchForTable(string library, string date, TextBox textBox, string CS)
        {
            string[] categories = InitializeCategories();
            int[] categoriesArray = new int[13];
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
                        string searchString = "RDP=" + date + "-" + library;
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

                                for (int i = 1; i < 13; i++)
                                {


                                    if (str == categories[i])
                                    {
                                        categoriesArray[i]++;
                                    }
                                }

                            }

                        }

                        UpdatePercentAtTextBox(1 / 5, textBox);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            return categoriesArray;
        }

        static public string[] InitializeCategories()
        {
            string[] category =
            {
                "Всего",
                "Рук.",
                "Спец.",
                "Служ.",
                "Раб.",
                "Студ." ,
                "Шк.",
                "Проч.",
                "КЗА",
                "КП" ,
                "ПВЛ",
                "СБ",
                "Мероприятие"
        };
            return category;
        }
        static public void CreateTable(string[] library, string date, TextBox textBox, string CS)
        {
            int[][] tableForLibraries = new int[library.Count()][];
            for (int i = 0; i < library.Count(); i++)
            {
                AddPercentAtTextBox(i, library.Count(), textBox);
                tableForLibraries[i] = SearchForTable(library[i], date, textBox, CS);
            }
            ShowInExcel(tableForLibraries);
            MessageBox.Show("Сделано");
        }
        static public void ShowInExcel(int[][] tableForLibraries)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            // Задаем расширение имени файла по умолчанию (открывается папка с программой)
            ofd.DefaultExt = "*.xls;*.xlsx";
            // Задаем строку фильтра имен файлов, которая определяет варианты
            ofd.Filter = "файл Excel (data.xls)|*.xls";
            // Задаем заголовок диалогового окна
            ofd.Title = "Выберите файл базы данных";
            ofd.ShowDialog();

            Excel.Application ObjWorkExcel = new Excel.Application();
            Excel.Workbook ObjWorkBook = ObjWorkExcel.Workbooks.Open(ofd.FileName);
            Excel.Worksheet ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[1]; //получить 1-й лист
            for (int i = 0; i < 11; i++) //по всем строкам
                for (int j = 0; j < 13; j++) // по всем столбцам
                    ObjWorkSheet.Cells[i + 7, j + 3] = tableForLibraries[i][j]; //записываем данные
                                                                                // ObjWorkBook.Close(true, Type.Missing, Type.Missing); //закрыть и сохранить
            ObjWorkExcel.Visible = true;
            ObjWorkExcel.UserControl = true;
            //ObjWorkExcel.Quit(); // выйти из Excel
        }
        static private int Search(MarcRecord record, string date, string library)
        {

            foreach (string overlapping in record.FMA(40))
            {
                MessageBox.Show(overlapping);
            }


            return 0;
        }
    }
}





