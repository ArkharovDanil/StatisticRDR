using ManagedIrbis;
using System;
using System.Linq;
using System.Windows.Forms;

namespace StatisticRDR
{
    public class StatForm11
    {
        private string ConnectionString;

        private static IrbisConnection Connection;

        private static string prefix;

        private static string name;

        private static string path;

        private static string delimiter;

        static StatForm11()
        {
            StatForm11.prefix = "RD=";
            StatForm11.name = "Распределение кол-ва записанных читателей по категориям читателей  и местам выдач за ";
            StatForm11.path = "C:\\tempStatRDR\\StatForm11\\";
            StatForm11.delimiter = "-";
        }

        public StatForm11()
        {
        }

        public static void CreateTable(string[] library, string[] categories, string date, string CS, bool isFirst, bool countAsSum, bool countAsDay)
        {
            int[][] numArray = new int[library.Count<string>()][];
            if (!countAsDay)
            {
                numArray = StatForm11.SearchAllForTable(library, categories, date, CS, isFirst);
            }
            else
            {
                for (int i = 0; i < library.Count<string>(); i++)
                {
                    numArray[i] = StatForm11.SearchForTable(library[i], categories, date, CS, isFirst);
                }
            }
            StatForm11.ShowInExcelByCreating(numArray, library, categories, date, countAsSum);
            MessageBox.Show("Сделано");
        }

        public static int[][] SearchAllForTable(string[] libraries, string[] categories, string date, string CS, bool isFirst)
        {
            int[][] numArray = new int[libraries.Count<string>()][];
            numArray = StatFormInstruments.SearchAllForThreadMethodForMonth(libraries, categories, date, CS, isFirst, StatForm11.prefix, StatForm11.Connection, StatForm11.delimiter);
            return numArray;
        }

        public static int[] SearchForTable(string library, string[] categories, string date, string CS, bool isFirst)
        {
            int[] numArray = new int[(int)categories.Length];
            numArray = StatFormInstruments.SearchForTable(library, categories, date, CS, isFirst, StatForm11.prefix, StatForm11.Connection, StatForm11.delimiter);
            return numArray;
        }

        public static void ShowInExcelByCreating(int[][] tableForLibraries, string[] library, string[] categories, string date, bool countAsSum)
        {
            StatFormInstruments.ShowInExcelByCreating(tableForLibraries, library, categories, date, countAsSum, StatForm11.path, StatForm11.name);
        }
    }
}