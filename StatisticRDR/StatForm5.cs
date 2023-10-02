using ManagedIrbis;
using System;
using System.Linq;
using System.Windows.Forms;

namespace StatisticRDR
{
    public class StatForm5
    {
        private string ConnectionString;

        private static IrbisConnection Connection;

        private static string prefix;

        private static string name;

        private static string path;

        private static string delimiter;

        static StatForm5()
        {
            StatForm5.prefix = "DW=";
            StatForm5.name = "Распределение книговыдач по категориям читателей  и местам выдач за ";
            StatForm5.path = "C:\\tempStatRDR\\StatForm5\\";
            StatForm5.delimiter = "/";
        }

        public StatForm5()
        {
        }

        public static void CreateTable(string[] library, string[] categories, string date, string CS, bool isFirst, bool countAsSum, bool countAsDay)
        {
            int[][] numArray = new int[library.Count<string>()][];
            if (!countAsDay)
            {
                numArray = StatForm5.SearchAllForTable(library, categories, date, CS, isFirst);
            }
            else
            {
                for (int i = 0; i < library.Count<string>(); i++)
                {
                    numArray[i] = StatForm5.SearchForTable(library[i], categories, date, CS, isFirst);
                }
            }
            StatForm5.ShowInExcelByCreating(numArray, library, categories, date, countAsSum);
            MessageBox.Show("Сделано");
        }

        public static int[][] SearchAllForTable(string[] libraries, string[] categories, string date, string CS, bool isFirst)
        {
            int[][] numArray = new int[libraries.Count<string>()][];
            numArray = StatFormInstruments.SearchAllForThreadMethodForMonth(libraries, categories, date, CS, isFirst, StatForm5.prefix, StatForm5.Connection, StatForm5.delimiter);
            return numArray;
        }

        public static int[] SearchForTable(string library, string[] categories, string date, string CS, bool isFirst)
        {
            int[] numArray = new int[(int)categories.Length];
            numArray = StatFormInstruments.SearchForTable(library, categories, date, CS, isFirst, StatForm5.prefix, StatForm5.Connection, StatForm5.delimiter);
            return numArray;
        }

        public static void ShowInExcelByCreating(int[][] tableForLibraries, string[] library, string[] categories, string date, bool countAsSum)
        {
            StatFormInstruments.ShowInExcelByCreating(tableForLibraries, library, categories, date, countAsSum, StatForm5.path, StatForm5.name);
        }
    }
}