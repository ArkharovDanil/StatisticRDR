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
    public class StatForm6
    {
        string ConnectionString;
        private static IrbisConnection Connection;
        private static string prefix = "VS=";
        private static string path = "C:\\tempStatRDR\\StatForm6";
        private static string name = "Распределение посещений по категориям читателей  и местам выдач за ";

        /// <summary>
        /// вход в алгоритм
        /// </summary>
        /// <param name="library"></param>
        /// <param name="categories"></param>
        /// <param name="date"></param>
        /// <param name="CS"></param>
        /// <returns></returns>
        static public int[] SearchForTable(string library,string[] categories,string date,string CS,bool isFirst)
        {
            
            int[] categoriesArray = new int[categories.Length];
            categoriesArray=StatFormInstruments.SearchForTable(library, categories, date, CS, isFirst, prefix, Connection);
            return categoriesArray;
        }
        static public int[][] SearchAllForTable(string[] libraries, string[] categories, string date, string CS, bool isFirst)
        {
            int[][] answerTableForLibraries = new int[libraries.Count()][];
            answerTableForLibraries=StatFormInstruments.SearchAllForTable(libraries, categories, date, CS,isFirst, prefix, Connection);
            return answerTableForLibraries;
        }
 
        /// <summary>
        /// поиск для таблицы?
        /// </summary>
        /// <param name="library"></param>
        /// <param name="categories"></param>
        /// <param name="date"></param>
        /// <param name="CS"></param>
        /// <param name="isFirst">Если да-считать как первое повторение, если нет, то как все повторения</param>
        /// <param name="countAsSum">Считать в экселе как сумму</param>
        /// <param name="countAsDay">считать как день или как месяц</param>
        static public void CreateTable(string[] library,string[] categories,string date,string CS, bool isFirst, bool countAsSum,bool countAsDay)
        {   
            int[][] tableForLibraries = new int[library.Count()][];
            if (countAsDay)
                for (int i = 0; i < library.Count(); i++)
                {
                    // AddPercentAtTextBox(i, library.Count(), textBox);
                    tableForLibraries[i] = SearchForTable(library[i], categories, date, CS, isFirst);
                }
            else
                {
                 tableForLibraries = SearchAllForTable(library, categories, date, CS, isFirst);
                }
            ShowInExcelByCreating(tableForLibraries, library,categories, date, countAsSum);
            MessageBox.Show("Сделано");
        }
       
        static public void ShowInExcelByCreating(int[][] tableForLibraries, string[] library, string[] categories, string date, bool countAsSum)
        {
            StatFormInstruments.ShowInExcelByCreating(tableForLibraries,library,categories,date,countAsSum,path,name);
        }
    }
}
