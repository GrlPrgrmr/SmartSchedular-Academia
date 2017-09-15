﻿using CsvHelper.Configuration;
using GenericParsing;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
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

    public sealed class GEMap: CsvClassMap<GECourses>
    {
        public GEMap()
        {
            Map(m => m.Category).Name("Category");
            Map(m => m.Subject).Name("Subject");
        }
    }
}
