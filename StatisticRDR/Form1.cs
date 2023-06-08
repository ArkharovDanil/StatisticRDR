using System;
using System.Windows.Forms;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
/// <summary>
/// необходимо добавить другие статформы!5,11,12
/// также добавить выгрузку по одной библиотеке
/// также возможность выбирать сервер
/// </summary>
namespace StatisticRDR
{
    public delegate void MyDelegate(string[] str);
    public delegate void MyDelegate1(string s1,string s2,string s3,string s4);
    public delegate void MyDelegateBool(bool b);


    public partial class Form1 : Form
    {
        string ConnectionString;
        string[] category;
        string[] lib;
        string _host;
        string _port;
        string _user;
        string _password;
        bool _searchOnlyOne;
        bool _countAsSum;
        bool _countAsDay;

        public Form1()
        {
            InitializeComponent();
            InitializeForms();
            ReadLibrariesFromFile();
            ReadCategoriesFromFile();
            InitializeComboboxLibrary();
            SomethingLikeProgressWork();
            InitializeConnectionString();
            InitializeBoolInSearch();
            InitializeCountAsSum();
            InitializeCountAsDay();
        }
        public void MakeProgressBarInvisible()
        {
            progressBar1.Visible = false;
        }
        public void MakeProgressBarVisible()
        {
            progressBar1.Visible = true;
        }
        public void ReadLibrariesFromFile()
        {
            try
            {
                string text;
                string path = "lib.txt";
                using (StreamReader reader = new StreamReader(path))
                {
                 text = reader.ReadToEnd();
                }
                 lib = text.Split('\n');
                 lib = DeleteLastSymbolsExceptLastWord(lib);
                  int t = WhereFirstNull(lib);
                    if (t < lib.GetUpperBound(0) + 1)
                    {
                string[] answer = new string[t];
                for (int i = 0; i < t; i++)
                {
                    answer[i] = lib[i];
                }
                lib = answer;
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void ReadCategoriesFromFile()
        {
            try
            {
                string text;
                string path = "cat.txt";
                using (StreamReader reader = new StreamReader(path))
                {
                    text = reader.ReadToEnd();
                }
                category = text.Split('\n');
                category = DeleteLastSymbolsExceptLastWord(category);
                int t = WhereFirstNull(category);
                if (t < category.GetUpperBound(0) + 1)
                {
                    string[] answer = new string[t];
                    for (int i = 0; i < t; i++)
                    {
                        answer[i] = category[i];
                    }
                    category = answer;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           

        }
        public int WhereFirstNull(string[] text)
        {
            int t = 0;
            foreach (string str in text)
            {
                if (str == "")
                {
                    return t;
                }
                t++;
            }
            return t;
        }
        public void ReadCategoriesFromForm2(string[] str)
        {
            category = str;
            SaveCategoriesInFile(str);
        }

        public void ReadLibrariesFromForm2(string[] str)
        {
            lib = str;
            SaveLibrariesInFile(lib);
        }
        public void SaveCategoriesInFile(string[] str)
        {
            string path = "cat.txt";
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                fileInf.Delete();
                // альтернатива с помощью класса File
                // File.Delete(path);
            }
            else
            {
                fileInf.Create();
            }

            StreamWriter wr = new StreamWriter("cat.txt", true);

            foreach (string str2 in str)
            {
                wr.WriteLine(str2);
            }

            wr.Close();
        }
        public void SaveLibrariesInFile(string[] str)
        {
            string path = "lib.txt";
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                fileInf.Delete();
                // альтернатива с помощью класса File
                // File.Delete(path);
            }
            else
            {
                fileInf.Create();
            }
            StreamWriter wr = new StreamWriter("lib.txt", true);
            foreach (string str2 in str)
            {
                wr.WriteLine(str2);
            }

            wr.Close();
        }

        public string[] GetLibraries()
        {
            return lib;
        }
        public string[] GetCategories()
        {
            return category;
        }
        public void InitializeForms()
        {
            comboBoxStatForms.Items.Add("StatForm5");
            comboBoxStatForms.Items.Add("StatForm6");
            comboBoxStatForms.Items.Add("StatForm11");
            comboBoxStatForms.Items.Add("StatForm12");
            comboBoxStatForms.SelectedIndex = 1;
        }

        public void InitializeComboboxLibrary()
        {
            radioButton1.Checked = true;
            string[] libs = GetLibraries();
            for (int i = 0; i < libs.Length; i++)
            {
                comboBoxLibrary.Items.Add(lib[i]);
            }
        }
        /// <summary>
        /// Удаление служебных символов из массива 
        /// </summary>
        public string[] DeleteLastSymbolsExceptLastWord(string[] str)
        {
            string[] _str = str;
            for (int i = 0; i < _str.Count() - 1; i++)
            {
                _str[i] = _str[i].Remove(_str[i].Length - 1);
            }
            return _str;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ifRadioButtonChecked();
            ifRadioButtonNotChecked();
        }
        public void InitializeLoadingCircle()
        {
            textBoxAnswer.Text = "";
        }

        public void DoStatForm5()
        {
            string[] lib = GetLibraries();
            string[] cat = GetCategories();
            StatForm5.CreateTable(lib, cat, textBoxDate.Text, ConnectionString, _searchOnlyOne, _countAsSum, _countAsDay);
        }

        public void DoStatForm6()
        {
            string[] lib = GetLibraries();
            string[] cat = GetCategories();
            StatForm6.CreateTable(lib, cat, textBoxDate.Text, ConnectionString, _searchOnlyOne,_countAsSum,_countAsDay);
        }
        public void DoStatForm11()
        {
            string[] lib = GetLibraries();
            string[] cat = GetCategories();
            StatForm11.CreateTable(lib, cat, textBoxDate.Text, ConnectionString, _searchOnlyOne, _countAsSum,_countAsDay);
        }
        public void DoStatForm12()
        {
            string[] lib = GetLibraries();
            string[] cat = GetCategories();
            StatForm12.CreateTable(lib, cat, textBoxDate.Text, ConnectionString, _searchOnlyOne, _countAsSum, _countAsDay);
        }
        public void UpdatePercentAtTextBox(double x)
        {
            double t = Convert.ToDouble(textBoxAnswer.Text);
            textBoxAnswer.Text = (t + x * 100).ToString();
        }
        private void ifRadioButtonChecked()
        {

            if (radioButton1.Checked)
            {
                if (comboBoxStatForms.SelectedIndex == -1)
                {
                    MessageBox.Show("выберите статистическую форму");
                }
                if (comboBoxStatForms.SelectedIndex == 0)
                {
                    progressBar1.Visible = true;
                    Thread myThread1 = new Thread(DoStatForm5);
                    myThread1.Start();
                    textBoxAnswer.Text = "Подождите, пожалуйста...\n Не забудьте нажать \"начальное состояние\" после получения таблицы ";
                }
                if (comboBoxStatForms.SelectedIndex == 1)
                {
                    progressBar1.Visible = true;
                    Thread myThread2 = new Thread(DoStatForm6);
                    myThread2.Start();
                    textBoxAnswer.Text = "Подождите, пожалуйста...\n Не забудьте нажать \"начальное состояние\" после получения таблицы ";
                }
                if (comboBoxStatForms.SelectedIndex == 2)
                {
                    progressBar1.Visible = true;
                    Thread myThread3 = new Thread(DoStatForm11);
                    myThread3.Start();
                    textBoxAnswer.Text = "Подождите, пожалуйста...\n Не забудьте нажать \"начальное состояние\" после получения таблицы ";
                }
                if (comboBoxStatForms.SelectedIndex == 3)
                {
                    progressBar1.Visible = true;
                    Thread myThread4 = new Thread(DoStatForm12);
                    myThread4.Start();
                    textBoxAnswer.Text = "Подождите, пожалуйста...\n Не забудьте нажать \"начальное состояние\" после получения таблицы ";
                }
            }


        }
        private void ifRadioButtonNotChecked()
        {
            if (radioButton2.Checked)
            {
                string[] cat = GetCategories();
                MessageBox.Show(Convert.ToString(StatForm6.SearchForTable(comboBoxLibrary.Text, cat, textBoxDate.Text, ConnectionString,_searchOnlyOne)));
            }

        }
        /// <summary>
        /// Редактировать список библиотек
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void списокБиблиотекToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyDelegate @delegate = ReadLibrariesFromForm2;
            Form2 newForm = new Form2(lib, @delegate);
            newForm.Show();

        }
        /// <summary>
        /// Редактировать список категорий
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void списокКатегорийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyDelegate @delegate = ReadCategoriesFromForm2;
            Form2 newForm = new Form2(category, @delegate);
            newForm.Show();
        }
        /// <summary>
        /// Показать список библиотек на отдельной форме
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void списокБиблиотекToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form3 newForm = new Form3(lib);
            newForm.Show();
        }
        /// <summary>
        /// Показать список категорий на отдельной форме
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void списокКатегорийToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form3 newForm = new Form3(category);
            newForm.Show();
        }
        public void SomethingLikeProgressWork()
        {
            progressBar1.Visible = false;
            progressBar1.Value = 0;
            progressBar1.Maximum = 100;
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            for (int i = 0; i < 10000; i++)
            {
                Thread.Sleep(500);
                backgroundWorker1.ReportProgress(0);
            }

        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            if (progressBar1.Value == 99)
            {
                progressBar1.Value = 0;
            }
            progressBar1.Value += 1;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Task completed");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                comboBoxLibrary.Visible = false;
                label2.Visible = false;
            }
            else
            {
                comboBoxLibrary.Visible = true;
                label2.Visible = true;
            }
        }

        private void основнаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ConnectionString = "host=10.24.223.197;port=6666;user=Архаров;password=0411;db=RDR;";
            label5.Text = "Версия: основная";
        }

        private void тестоваяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionString = "host=127.0.0.1;port=6666;user=1;password=1;db=RDR";
            label5.Text = "Версия: тестовая";
        }

        private void начальноеСостояниеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = false;
            textBoxAnswer.Text = "Приложение готово к работе";
        }

        private void настройкаПриложенияToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        public void InitializeConnectionString()
        {
            (string, string, string, string) s = ReadSeparateConnectionStringFromFile();
            InitializeConnectionString(s.Item1, s.Item2, s.Item3, s.Item4);
        }
        public void InitializeConnectionString(string host, string port, string user, string password)
        {
            ConnectionString = "host=" + host + ";port=" + port + ";user=" + user + ";password=" + password + ";db=RDR;";
            label5.Text = "Версия: основная";
            SaveSeparateConnectionStringToFile(SeparateConnectionString());
        }
        public (string, string, string, string) ReadSeparateConnectionStringFromFile()
        {
            string[] answer;
            string text;
            string path = "ini1.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                text = reader.ReadToEnd();
            }
            answer = text.Split('\n');
            answer = DeleteLastSymbolsExceptLastWord(answer);
            string[] current = new string[answer.GetUpperBound(0)];
            if (category.Last() == "")
            {
                for (int i = 0; i < category.GetUpperBound(0); i++)
                {
                    current[i] = answer[i];
                }
                answer = current;
            }
            return (answer[0], answer[1], answer[2], answer[3]);

        }

        public void SaveSeparateConnectionStringToFile((string, string, string, string) strTuple)
        {
            string path = "ini1.txt";
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                fileInf.Delete();
                // альтернатива с помощью класса File
                // File.Delete(path);
            }
            else
            {
                fileInf.Create();
            }
            StreamWriter wr = new StreamWriter("ini1.txt", true);
            wr.WriteLine(strTuple.Item1);
            wr.WriteLine(strTuple.Item2);
            wr.WriteLine(strTuple.Item3);
            wr.WriteLine(strTuple.Item4);

            wr.Close();
        }

        public (string, string, string, string) SeparateConnectionString()
        {
            string[] s = ConnectionString.Split(';');
            string[] t0 = s[0].Split('=');
            string[] t1 = s[1].Split('=');
            string[] t2 = s[2].Split('=');
            string[] t3 = s[3].Split('=');
            string s0 = t0[1];
            string s1 = t1[1];
            string s2 = t2[1];
            string s3 = t3[1];
            return (s0, s1, s2, s3);

        }

        private void comboBoxStatForms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxStatForms.SelectedIndex == 0)//statform5
            {
                label6.Text = "Распределение книговыдач по категориям читателей  и местам выдач";
            }
            if (comboBoxStatForms.SelectedIndex == 1)//statform6
            {
                label6.Text = "Распределение посещений по категориям читателей  и местам выдач";
            }
            if (comboBoxStatForms.SelectedIndex == 2)//statform11
            {
                label6.Text = "Распределение кол - ва записанных читателей по категориям читателей и местам выдач";
            }
            if (comboBoxStatForms.SelectedIndex == 3)//statform12
            {
                label6.Text = "Распределение кол-ва перерегистрированных читателей по категориям читателей  и местам выдач";
            }

        }

        private void настройкаПодключенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyDelegate1 @delegate = InitializeConnectionString;
            (string, string, string, string) SeparateInfoConnection = SeparateConnectionString();
            Form4 newForm = new Form4(SeparateInfoConnection.Item1, SeparateInfoConnection.Item2, SeparateInfoConnection.Item3, SeparateInfoConnection.Item4, @delegate);
            newForm.Show();
        }
        private void InitializeBoolInSearch()
        {
            _searchOnlyOne = LoadFromIsFirstFile();           
        }
        private void InitializeBoolInSearch(bool b)
        {
            _searchOnlyOne=b;
            SaveIsFirstInSearchToFile(_searchOnlyOne);
        }
        private void настройкаПоискаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyDelegateBool @delegate = InitializeBoolInSearch;
            bool b = LoadFromIsFirstFile();
            Form5 newForm = new Form5(b, @delegate);
            newForm.Show();        
        }
        private void Save(string path,bool isTrue)
        {
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                fileInf.Delete();
                // альтернатива с помощью класса File
                // File.Delete(path);
            }
            else
            {
                fileInf.Create();
            }
            StreamWriter wr = new StreamWriter(path, true);
            if (isTrue)
            {
                wr.WriteLine("true");
            }
            else
            {
                wr.WriteLine("false");
            }


            wr.Close();
        }
        private bool LoadFromFile(string path)
        {
            string[] answer;
            string text;
            
            using (StreamReader reader = new StreamReader(path))
            {
                text = reader.ReadToEnd();
            }
            answer = text.Split('\n');
            answer = DeleteLastSymbolsExceptLastWord(answer);
            foreach (string str in answer)
            {
                if (str == "true") return true;
                if (str == "false") return false;
            }
            return false;
        }
        private bool LoadFromIsFirstFile()
        {
            string path = "ini2.txt";
            return LoadFromFile(path);
        }
        public void SaveIsFirstInSearchToFile(bool isFirst)
        {
            string path = "ini2.txt";
            Save(path, isFirst);
        }
        public void SaveCountAsSumToFile(bool isFirst)
        {
            string path = "ini3.txt";
            Save(path, isFirst);
        }
        private bool LoadFromSumAsCount()
        {
            string path = "ini3.txt";
            return LoadFromFile(path);
        }
        public void SaveCountAsDayToFile(bool isDay)
        {
            string path = "ini4.txt";
            Save(path, isDay);
        }
        private bool LoadFromCountAsDay()
        {
            string path = "ini4.txt";
            return LoadFromFile(path);
        }
        private void InitializeCountAsSum()
        {
            _countAsSum = LoadFromSumAsCount();
        }
        private void InitializeCountAsSum(bool b)
        {
            _countAsSum = b;
            SaveCountAsSumToFile(_countAsSum);
        }
        private void InitializeCountAsDay()
        {
            _countAsDay = LoadFromCountAsDay();
        }
        private void InitializeCountAsDay(bool b)
        {
            _countAsDay = b;
            SaveCountAsDayToFile(_countAsDay);
        }

        private void настройкаВыводаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyDelegateBool @delegate = InitializeCountAsSum;
            bool b = LoadFromSumAsCount();
            Form6 newForm = new Form6(b, @delegate);
            newForm.Show();
        }

        private void настройкиОтбораToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyDelegateBool @delegate = InitializeCountAsDay;
            bool b = LoadFromCountAsDay();
            Form7 newForm = new Form7(b, @delegate);
            newForm.Show();
        }
    }
    
}
