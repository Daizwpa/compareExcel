using IronXL;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compareExcel
{
    public class CompareModel
    {

        string[] columns1;
        string[] columns2;
        string path1;
        string path2;


        public CompareModel(string columns1, string columns2, string path1, string path2)
        {
            this.path1 = path1;
            this.path2 = path2;
            this.columns1 = columns1.Split(',').Select(c => c.Trim()).ToArray();
            this.columns2 = columns2.Split(",").Select(c => c.Trim()).ToArray();
        }

        public DataTable Check()
        {

            DataTable content = CreateTAble(columns1);
            
            WorkSheet file1 = ReadExcel(path1);
            WorkSheet file2 = ReadExcel(path2);

            for (int row1 = 1; row1 <= file1.Rows.Length; row1++)
            {
                string[] values = getValues(file1, row1, columns1);

                bool found = isExists(file2, values);

                if (!found)
                    content.Rows.Add(values);

            }
            return content;
        
        }

        private bool isExists(WorkSheet source, string[] values)
        {       bool found = false;
                for (int i = 1;  i <= source.RowCount; i++)
                {
                    bool[] tablebool = new bool[columns2.Length];
                    for (int col = 0; col < columns2.Length; col++)
                    {
                        tablebool[col] = false;
                        if (source[columns2[col] + i].Value.ToString() == values[col])
                        {
                            tablebool[col] = true;

                            break;
                        }

                    }
                    if(tablebool.Any(s => s == true))
                    {
                        found = true;
                        break;
                    }
                    
                }

            return found;

        }


        private DataTable CreateTAble(string[] cols)
        {

            DataTable table = new DataTable("compare");

            for (int col = 0; col < columns1.Length; col++)
            {
                table.Columns.Add(cols[col], typeof(string));

            }
            return table;
        }


        private string[] getValues(WorkSheet  table,int row1, string[] cols)
        {
            string[] value = new string[cols.Length];
            

            try
            {
                if (table == null) throw new InvalidOperationException();
                if (cols == null) throw new ArgumentNullException();


                for (int j = 0; j < value.Length; j++)
                {
                    value[j] = table[cols[j] + row1].Value.ToString() ?? string.Empty;
                }

            }
            catch (Exception e)
            {

            }

            return value;


        }

        /// <summary>
        /// this method will read the excel file and copy its data into a datatable
        /// </summary>
        /// <param name="fileName">name of the file</param>
        /// <returns>DataTable</returns>
        private WorkSheet ReadExcel(string fileName)
        {
            WorkBook workbook = WorkBook.Load(fileName);
            //// Work with a single WorkSheet.
            ////you can pass static sheet name like Sheet1 to get that sheet
            ////WorkSheet sheet = workbook.GetWorkSheet("Sheet1");
            //You can also use workbook.DefaultWorkSheet to get default in case you want to get first sheet only
            WorkSheet sheet = workbook.DefaultWorkSheet;
            //Convert the worksheet to System.Data.DataTable
            //Boolean parameter sets the first row as column names of your table.
            return sheet;
        }
    }
}
