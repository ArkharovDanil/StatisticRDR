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

    
    public partial class Form1 : Form
    {
        string ConnectionString;
        string[] category;
        string[] lib;
        public Form1()
        {
            InitializeComponent();         
            InitializeForms();
            ReadLibrariesFromFile();
            ReadCategoriesFromFile();
            InitializeComboboxLibrary();
            SomethingLikeProgressWork();
            InitializeConnectionString();

            //InitializeLoadingCircle();
        }
        public void MakeProgressBarInvisible()
        {
            progressBar1.Visible= false;
        }
        public void MakeProgressBarVisible()
        {
            progressBar1.Visible = true;
        }
        public void ReadLibrariesFromFile()
        {
            string text;
            string path = "lib.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                text = reader.ReadToEnd();
            }
            lib = text.Split('\n');
            lib = DeleteLastSymbolsExceptLastWord(lib);
            string[] answer = new string[lib.GetUpperBound(0)];
            if (lib.Last() == "")
            {
                for (int i = 0; i < lib.GetUpperBound(0); i++)
                {
                    answer[i] = lib[i];
                }
                lib = answer;
            }
        }
        public void ReadCategoriesFromFile()
        {
            string text;
            string path = "cat.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                text = reader.ReadToEnd();
            }
            category = text.Split('\n');
            category = DeleteLastSymbolsExceptLastWord(category);
            string[] answer=new string[category.GetUpperBound(0)];
            if (category.Last()=="")
            {
                for (int i=0;i< category.GetUpperBound(0);i++)
                {
                    answer[i] = category[i];
                }
                category = answer;
            }
            
        }
        public void ReadCategoriesFromForm2(string[] str)
        {
            category = str;
            SaveCategoriesInFile(str);
        }
 
        public void ReadLibrariesFromForm2(string[] str)
        {
            lib=str;
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
          //  comboBoxStatForms.Items.Add("StatForm5");
            comboBoxStatForms.Items.Add("StatForm6");
            comboBoxStatForms.SelectedIndex = 0; 
          //  comboBoxStatForms.Items.Add("StatForm11");
          //  comboBoxStatForms.Items.Add("StatForm12");
        }
        public void InitializeConnectionString()
        {       
                ConnectionString = "host=10.24.223.197;port=6666;user=Архаров;password=0411;db=RDR;";
                label5.Text = "Версия: основная";
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
            for(int i=0;i<_str.Count()-1;i++)
            {
                _str[i]=_str[i].Remove(_str[i].Length-1);
            }
            return _str;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ifRadioButtonChecked();
            ifRadioButtonNotChecked();
           // LoadingProgress();
        }
        public void InitializeLoadingCircle()
        {
            textBoxAnswer.Text = "";
        }
       

        
        public void DoStatForm6()
        {
            string[] lib = GetLibraries();
            string[] cat = GetCategories();            
            StatForm6.CreateTable(lib, cat, textBoxDate.Text, ConnectionString);
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
                    Thread myThread1 = new Thread(DoStatForm6);
                    myThread1.Start();
                    textBoxAnswer.Text = "Подождите, пожалуйста...";
                    

                }
            }
           
            
        }
        private void ifRadioButtonNotChecked()
        {
            if (radioButton2.Checked)
            {
                string[] cat = GetCategories();
                MessageBox.Show(Convert.ToString(StatForm6.SearchForTable(comboBoxLibrary.Text,cat,textBoxDate.Text,ConnectionString)));
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
            Form2 newForm = new Form2(lib,@delegate);
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
            Form2 newForm = new Form2(category,@delegate);
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
            for (int i = 0; i< 10000; i++)
            {               
                Thread.Sleep(500);
                backgroundWorker1.ReportProgress(0);
            }

        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            if (progressBar1.Value==99)
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
            ConnectionString = "host=10.24.223.197;port=6666;user=Архаров;password=0411;db=RDR;";
            label5.Text = "Версия: основная";
        }

        private void тестоваяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionString = "host=127.0.0.1;port=6666;user=1;password=1;db=RDR";
            label5.Text = "Версия: тестовая";
        }
    }
    
}
