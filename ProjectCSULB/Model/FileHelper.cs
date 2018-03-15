using CsvHelper.Configuration;
using GenericParsing;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectCSULB.Model
{
   public static class FileHelper
    {
        public static List<ScheduleReportItem> getDataFromScheduleCSV(string path)
        {
            List<ScheduleReportItem> report = new List<ScheduleReportItem>();

            try
            {
                report = DataTableToList<ScheduleReportItem>(ConvertCSVtoDataTable(path));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return report;
        }

        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            StreamReader sr = new StreamReader(strFilePath);
            string[] headers = sr.ReadLine().Split(',');
            DataTable dt = new DataTable();
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            while (!sr.EndOfStream)
            {
                string[] rows = Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                DataRow dr = dt.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    dr[i] = rows[i];
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static T ToObject<T>(this DataRow row) where T : class, new()
        {
            T obj = new T();

            foreach (var prop in obj.GetType().GetProperties())
            {
                try
                {
                    if (prop.PropertyType.IsGenericType && prop.PropertyType.Name.Contains("Nullable"))
                    {
                        if (!string.IsNullOrEmpty(row[prop.Name].ToString()))
                            prop.SetValue(obj, Convert.ChangeType(row[prop.Name],
                            Nullable.GetUnderlyingType(prop.PropertyType), null));
                        //else do nothing
                    }
                    else
                        prop.SetValue(obj, Convert.ChangeType(row[prop.Name], prop.PropertyType), null);
                }
                catch(Exception ex)
                {
                    continue;
                }
            }
            return obj;
        }

        /// <summary>
        /// Creates the CSV from a generic list.
        /// </summary>;
        /// <typeparam name="T"></typeparam>;
        /// <param name="list">The list.</param>;
        /// <param name="csvNameWithExt">Name of CSV (w/ path) w/ file ext.</param>;
        public static void CreateCSVFromGenericList<T>(List<T> list, string csvNameWithExt)
        {
            if (list == null || list.Count == 0) return;

            //get type from 0th member
            Type t = list[0].GetType();
            string newLine = Environment.NewLine;

            using (var sw = new StreamWriter(csvNameWithExt))
            {
                //make a new instance of the class name we figured out to get its props
                object o = Activator.CreateInstance(t);
                //gets all properties
                PropertyInfo[] props = o.GetType().GetProperties();

                //foreach of the properties in class above, write out properties
                //this is the header row
                foreach (PropertyInfo pi in props)
                {
                    sw.Write(pi.Name.ToUpper() + ",");
                }
                sw.Write(newLine);

                //this acts as datarow
                foreach (T item in list)
                {
                    //this acts as datacolumn
                    foreach (PropertyInfo pi in props)
                    {
                        //this is the row+col intersection (the value)
                        string whatToWrite =
                            Convert.ToString(item.GetType()
                                                 .GetProperty(pi.Name)
                                                 .GetValue(item, null))
                                .Replace(',', ' ') + ',';

                        sw.Write(whatToWrite);

                    }
                    sw.Write(newLine);
                }
            }
        }
        /// <summary>
        /// Converts a DataTable to a list with generic objects
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List with generic objects</returns>
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    var obj = row.ToObject<T>();

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

    }

  

    public sealed class ScheduleMap: CsvClassMap<ScheduleReportItem>
    {
        public ScheduleMap()
        {
            //Map(m => m.APDB_Learning_Mode).Name("APDB Learning Mode");
            //Map(m => m.APDB_Learning_Mode_Descr).Name("APDB Learning Mode Descr");
            Map(m => m.Begin_Time).Name("Begin Time");
            Map(m => m.Enrollment_Cap).Name("Enrollment Cap");
            Map(m => m.Enrollment_Act).Name("Enrollment Act");
            Map(m => m.WaitList_Total).Name("Waitlist Total");
            //Map(m => m.Catalog_Nbr).Name("Catalog Nbr");
            Map(m => m.End_Time).Name("End Time");
            Map(m => m.Class_Nbr).Name("Class Nbr");
            //Map(m => m.College).Name("College");
            Map(m => m.Components).Name("Components");
            Map(m => m.Days).Name("Days");
            //Map(m => m.Department).Name("Department");
            Map(m => m.Section).Name("Section");
            Map(m => m.Session).Name("Session");
            Map(m => m.Subject).Name("Subject");
            Map(m => m.Title).Name("Title");
            Map(m => m.Units).Name("Units");
        }
    }

    public sealed class StudentMap: CsvClassMap<Student>
    {

        public StudentMap()
        {

            Map(m => m.StudentId).Name("StudentId");
            Map(m => m.StudentName).Name("StudentName");
            Map(m => m.Major).Name("Major");
            Map(m => m.FreshmanYear).Name("FreshmanYear");
        }
    }
    public sealed class GEMap: CsvClassMap<GECourses>
    {
        public GEMap()
        {
            Map(m => m.Category).Name("Category");
            Map(m => m.Subject).Name("Subject");
        }
    }
}
