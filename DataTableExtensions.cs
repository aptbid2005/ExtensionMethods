// ***********************************************************************
// Assembly         : Extensions.Core
// Author           : Wes Atha, Jason Nowicki
// Created          : 07-12-2018
//
// Last Modified By : Jason Nowicki
// Last Modified On : 07-12-2018
// ***********************************************************************
// <copyright file="DataTableExtensions.cs" company="EECPPC KY.gov">
//     Copyright ©  2018
// </copyright>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace Extensions.Core
{
    /// <summary>
    /// Class DataTableExtensions.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class DataTableExtensions
    {

        /// <summary>
        /// Convert a data table to a list of objects 
        /// </summary>
        /// <typeparam name="T">Type you want returned</typeparam>
        /// <param name="table">Datatable you want to convert</param>
        /// <returns>List&lt;T&gt;.</returns>
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (DataRow row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo?.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }

        }


        /// <summary>
        /// Output a DataTable To a CSV file.
        /// </summary>
        /// <param name="table">The table to export.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <param name="includeHeader">if set to <c>true</c> [include header].</param>
        /// <param name="outputPath">The output path.</param>
        /// <example>
        /// DataTable dataTableExportToCSV;
        /// dataTableExportToCSV.ToCSV(",", false, "C:\temp.csv");
        /// </example>
        // ReSharper disable once InconsistentNaming
        public static void ToCSV(this DataTable table, string delimiter, bool includeHeader, string outputPath)
        {
            StringBuilder result = new StringBuilder();

            if (includeHeader)
            {
                foreach (DataColumn column in table.Columns)
                {
                    result.Append(column.ColumnName);
                    result.Append(delimiter);
                }

                result.Remove(--result.Length, 0);
                result.Append(Environment.NewLine);
            }

            foreach (DataRow row in table.Rows)
            {
                foreach (object item in row.ItemArray)
                {
                    if (item is DBNull)
                        result.Append(delimiter);
                    else
                    {
                        string itemAsString = item.ToString();

                        // Double up all embedded double quotes
                        itemAsString = itemAsString.Replace("\"", "\"\"");

                        // To keep things simple, always delimit with double-quotes
                        // so we don't have to determine in which cases they're necessary
                        // and which cases they're not.
                        itemAsString = "\"" + itemAsString + "\"";

                        result.Append(itemAsString + delimiter);
                    }
                }

                result.Remove(--result.Length, 0);
                result.Append(Environment.NewLine);
            }

            // if directory doesn't exist then create it.
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            // if file doesn't exist then create it.
            if (!File.Exists(outputPath))
            {
                File.Create(outputPath);
            }


            // now write contents to the file.
            using (StreamWriter writer = new StreamWriter(outputPath, true))
            {
                writer.Write(result.ToString());
            }
        }


        /// <summary>
        /// Convert a datatable to XML.
        /// </summary>
        /// <param name="dt">The datatable to convert.</param>
        /// <param name="rootName">Name of the root.</param>
        /// <returns>XDocument.</returns>
        /// <example>
        /// dtCert.ToXml("Certs")
        /// </example>
        public static XDocument ToXml(this DataTable dt, string rootName)
        {
            XDocument xdoc = new XDocument
            {
                Declaration = new XDeclaration("1.0", "utf-8", "")
            };
            xdoc.Add(new XElement(rootName));
            foreach (DataRow row in dt.Rows)
            {
                XElement element = new XElement(dt.TableName);
                foreach (DataColumn col in dt.Columns)
                {
                    element.Add(new XElement(col.ColumnName, row[col].ToString().Trim(' ')));
                }

                xdoc.Root?.Add(element);
            }

            return xdoc;
        }


        /// <summary>
        /// Renames the column.
        /// </summary>
        /// <param name="dt">The Datatable.</param>
        /// <param name="oldName">The old column name.</param>
        /// <param name="newName">The new column name.</param>
        /// <example>
        /// dataSetName.Tables[0].RenameColumn("Column1", "Name");
        /// </example>
        public static void RenameColumn(this DataTable dt, string oldName, string newName)
        {
            if (dt == null || string.IsNullOrEmpty(oldName) || string.IsNullOrEmpty(newName) || oldName == newName)
            {
                return;
            }

            int idx = dt.Columns.IndexOf(oldName);
            dt.Columns[idx].ColumnName = newName;
            dt.AcceptChanges();
        }



        /// <summary>
        /// Removes the specified column from a datatable.
        /// </summary>
        /// <param name="dt">The datatable.</param>
        /// <param name="columnName">Name of the column to remove.</param>
        /// <example>
        /// dataSetName.Tables[0].RemoveColumn("Column12");
        /// </example>
        public static void RemoveColumn(this DataTable dt, string columnName)
        {
            if (dt == null || string.IsNullOrEmpty(columnName) || dt.Columns.IndexOf(columnName) < 0)
            {
                return;
            }

            int idx = dt.Columns.IndexOf(columnName);
            dt.Columns.RemoveAt(idx);
            dt.AcceptChanges();
        }

    }
}
