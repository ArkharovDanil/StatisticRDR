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
    public class StatForm// : IStatForm
    {
        string ConnectionString;
        private static IrbisConnection Connection;
        TextBox _textBoxAnswer;
        private List<string> rowsList;
        private List<string> columnsList;
        private List<string> rl;
        private List<string> cl;
        private TextBox textBoxAnswer;

        public StatForm(List<string> rl, List<string> cl, TextBox textBoxAnswer,string connectionString)
        {
            SetRowsList(rl);
            SetColumnsList(cl);
            int[][] array = new int[rl.Count][];
            _textBoxAnswer = textBoxAnswer;
            ConnectionString = connectionString;

        }

        public StatForm(List<string> rl, List<string> cl, TextBox textBoxAnswer)
        {
            this.rl = rl;
            this.cl = cl;
            this.textBoxAnswer = textBoxAnswer;
        }

        public List<string> GetRowsList()
        {
            return rowsList;

        }
        public void SetRowsList(List<string> rl)
        {
            rowsList = rl;
        }
        public List<string> GetColumnsList()
        {
            return columnsList;
        }
        public void SetColumnsList(List<string> cl)
        {
            columnsList = cl;
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
        public int[]  SearchForTable(string library, string date, TextBox textBox)
        {
            string[] categories = InitializeCategories();
            int[] categoriesArray = new int[13];
            int answer = 0;
            string connectionString = ConnectionString;
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
        public void CreateTable(string[] library, string date, TextBox textBox)
        {
            int[][] tableForLibraries = new int[library.Count()][];
            for (int i = 0; i < library.Count(); i++)
            {
                AddPercentAtTextBox(i, library.Count(), textBox);
                tableForLibraries[i] = SearchForTable(library[i], date, textBox);
            }
            ShowInExcel(tableForLibraries);
            MessageBox.Show("Сделано");
        }
        public static void ShowInExcel(int[][] tableForLibraries)
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
        public static void AddStringAtTextbox(string s, TextBox textBox)
        {
            textBox.Text += "     " + s + "    ";
            MessageBox.Show("Добавлена строка");
        }
        public static void AddPercentAtTextBox(double cur, double max, TextBox textBox)
        {
            textBox.Text = ((cur / max) * 100).ToString() + "%";
            textBox.Update();
        }
        public static void UpdatePercentAtTextBox(double x, TextBox textBox)
        {
            double t = Convert.ToDouble(textBox.Text);
            textBox.Text = (t + x * 100).ToString();
            textBox.Update();
        }
    }
}
