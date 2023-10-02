using ManagedIrbis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace StatisticRDR
{
    public class WorkingWithThreads
    {
        private List<int[][]> GlobalMatrix = new List<int[][]>();

        private List<Task> ListOfTasks = new List<Task>();

        private string[] dates;

        private Dictionary<int?, string> dictionaryTasksForDates = new Dictionary<int?, string>();

        private string[] _libraries;

        private string[] _categories;

        private string _date;

        private string _CS;

        private bool _isFirst;

        private string _prefix;

        private IrbisConnection _Connection;

        private string _delimiter;

        private int currentDay = -1;

        public WorkingWithThreads(string[] libraries, string[] categories, string date, string CS, bool isFirst, string prefix, IrbisConnection Connection, string delimiter)
        {
            this._libraries = libraries;
            this._categories = categories;
            this._date = date;
            this._CS = CS;
            this._isFirst = isFirst;
            this._prefix = prefix;
            this._delimiter = delimiter;
            this._Connection = Connection;
            this.dates = StatFormInstruments.MakeDatesFromMonth(date);
        }

        public void DoSmthgTrash()
        {
            this.currentDay++;
            int[][] numArray = new int[this._libraries.Count<string>()][];
            for (int i = 0; i < this._libraries.Count<string>(); i++)
            {
                numArray[i] = StatFormInstruments.SearchForTable(this._libraries[i], this._categories, this.dictionaryTasksForDates[Task.CurrentId], this._CS, this._isFirst, this._prefix, this._Connection, this._delimiter);
            }
            this.GlobalMatrix.Add(numArray);
        }

        public async Task<int[][]> ReturnSumMatrixAsync()
        {
            await this.ThreadsIsComing();
            int[][] numArray = new int[this._libraries.Count<string>()][];
            for (int i = 0; i < this._libraries.Count<string>(); i++)
            {
                int[] numArray1 = new int[this._categories.Count<string>()];
                numArray[i] = numArray1;
            }
            for (int j = 0; j < 31; j++)
            {
                int[][] numArray2 = new int[this._libraries.Count<string>()][];
                for (int k = 0; k < this._libraries.Count<string>(); k++)
                {
                    for (int l = 0; l < this._categories.Count<string>(); l++)
                    {
                        numArray[k][l] = 0;
                    }
                }
            }
            foreach (int[][] globalMatrix in this.GlobalMatrix)
            {
                for (int m = 0; m < this._libraries.Count<string>(); m++)
                {
                    for (int n = 0; n < this._categories.Count<string>(); n++)
                    {
                        numArray[m][n] += globalMatrix[m][n];
                    }
                }
            }
            int[][] numArray3 = numArray;
            return numArray3;
        }

        public async Task ThreadsIsComing()
        {
            for (int i = 0; i < 31; i++)
            {
                Task task = new Task(new Action(this.DoSmthgTrash));
                this.dictionaryTasksForDates.Add(new int?(task.Id), this.dates[i]);
                this.ListOfTasks.Add(task);
            }
            foreach (Task listOfTask in this.ListOfTasks)
            {
                listOfTask.Start();
            }
            await Task.WhenAll(this.ListOfTasks);
        }
    }
}