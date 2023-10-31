using ManagedIrbis;
using System;
using System.Linq;
using System.Windows.Forms;

namespace StatisticRDR
{
    public class StatForm12
    {
        private string ConnectionString;

        private static IrbisConnection Connection;

        private static string prefix;

        private static string path;

        private static string name;

        private static string delimiter;

        static StatForm12()
        {
            StatForm12.prefix = "RDP=";
            StatForm12.path = "C:\\tempStatRDR\\StatForm12\\";
            StatForm12.name = "Распределение кол-ва перерегистрированных читателей по категориям читателей  и местам выдач за ";
            StatForm12.delimiter = "-";
        }

        public StatForm12()
        {
        }

        public static void CreateTable(string[] library, string[] categories, string date, string CS, bool isFirst, bool countAsSum, bool countAsDay)
        {
            int[][] numArray = new int[library.Count<string>()][];
            if (!countAsDay)
            {
                numArray = StatForm12.SearchAllForTable(library, categories, date, CS, isFirst);
            }
            else
            {
                for (int i = 0; i < library.Count<string>(); i++)
                {
                    numArray[i] = StatForm12.SearchForTable(library[i], categories, date, CS, isFirst);
                }
            }
            StatForm12.ShowInExcelByCreating(numArray, library, categories, date, countAsSum);
            MessageBox.Show("Сделано");
        }

        public static int[][] SearchAllForTable(string[] libraries, string[] categories, string date, string CS, bool isFirst)
        {
            int[][] numArray = new int[libraries.Count<string>()][];
            numArray = StatFormInstruments.SearchAllForThreadMethodForMonth(libraries, categories, date, CS, isFirst, StatForm12.prefix, StatForm12.Connection, StatForm12.delimiter);
            return numArray;
        }

        public static int[] SearchForTable(string library, string[] categories, string date, string CS, bool isFirst)
        {
            int[] numArray = new int[(int)categories.Length];
            numArray = StatFormInstruments.SearchForTable(library, categories, date, CS, isFirst, StatForm12.prefix, StatForm12.Connection, StatForm12.delimiter);
            return numArray;
        }

        public static void ShowInExcelByCreating(int[][] tableForLibraries, string[] library, string[] categories, string date, bool countAsSum)
        {
            StatFormInstruments.ShowInExcelByCreating(tableForLibraries, library, categories, date, countAsSum, StatForm12.path, StatForm12.name);
        }
    }
}