using ManagedIrbis;
using System;
using System.Linq;
using System.Windows.Forms;

namespace StatisticRDR
{
    public class StatForm6
    {
        private string ConnectionString;

        private static IrbisConnection Connection;

        private static string prefix;

        private static string path;

        private static string name;

        private static string delimiter;

        static StatForm6()
        {
            StatForm6.prefix = "VS=";
            StatForm6.path = "C:\\tempStatRDR\\StatForm6\\";
            StatForm6.name = "Распределение посещений по категориям читателей  и местам выдач за ";
            StatForm6.delimiter = "/";
        }

        public StatForm6()
        {
        }

        public static void CreateTable(string[] library, string[] categories, string date, string CS, bool isFirst, bool countAsSum, bool countAsDay)
        {
            int[][] numArray = new int[library.Count<string>()][];
            if (!countAsDay)
            {
                numArray = StatForm6.SearchAllForTable(library, categories, date, CS, isFirst);
            }
            else
            {
                for (int i = 0; i < library.Count<string>(); i++)
                {
                    numArray[i] = StatForm6.SearchForTable(library[i], categories, date, CS, isFirst);
                }
            }
            StatForm6.ShowInExcelByCreating(numArray, library, categories, date, countAsSum);
            MessageBox.Show("Сделано");
        }

        public static int[][] SearchAllForTable(string[] libraries, string[] categories, string date, string CS, bool isFirst)
        {
            int[][] numArray = new int[libraries.Count<string>()][];
            numArray = StatFormInstruments.SearchAllForThreadMethodForMonth(libraries, categories, date, CS, isFirst, StatForm6.prefix, StatForm6.Connection, StatForm6.delimiter);
            return numArray;
        }

        public static int[] SearchForTable(string library, string[] categories, string date, string CS, bool isFirst)
        {
            int[] numArray = new int[(int)categories.Length];
            numArray = StatFormInstruments.SearchForTable(library, categories, date, CS, isFirst, StatForm6.prefix, StatForm6.Connection, StatForm6.delimiter);
            return numArray;
        }

        public static void ShowInExcelByCreating(int[][] tableForLibraries, string[] library, string[] categories, string date, bool countAsSum)
        {
            StatFormInstruments.ShowInExcelByCreating(tableForLibraries, library, categories, date, countAsSum, StatForm6.path, StatForm6.name);
        }
    }
}