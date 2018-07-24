// ***********************************************************************
// Assembly         : Extensions.Core
// Author           : Jason Nowicki
// Created          : 03-16-2018
//
// Last Modified By : Jason Nowicki
// Last Modified On : 03-16-2018
// ***********************************************************************
// <copyright file="HttpResponseExtensions.cs" company="EECPPC KY.gov">
//     Copyright ©  2018
// </copyright>
// ***********************************************************************
using System.Web;

namespace Extensions.Core
{
    /// <summary>
    /// Class HttpResponseExtensions.
    /// </summary>
    public static class HttpResponseExtensions
    {

        /// <summary>
        /// Force download of a file that exists on the server
        /// </summary>
        /// <param name="response">HttpResponse Object</param>
        /// <param name="fullPathToFile">Path to file on server</param>
        /// <param name="outputFileName">Name of the file to save to client</param>
        public static void ForceDownload(this HttpResponse response, string fullPathToFile, string outputFileName)
        {
            response.Clear();
            response.AddHeader("content-disposition", "attachment; filename=" + outputFileName);
            response.WriteFile(fullPathToFile);
            response.ContentType =  "";
            response.End();
        }


        /// <summary>
        /// Force download of a file that exists in a database
        /// </summary>
        /// <param name="response">httpResponse Object</param>
        /// <param name="fileBytes">Byte array containing file contents</param>
        /// <param name="outputFileName">Name of the file to save to client</param>
        public static void ForceDownload(this HttpResponse response, byte[] fileBytes, string outputFileName)
        {
            response.Clear();
            response.AddHeader("content-disposition", "attachment; filename=" + outputFileName);
            response.BinaryWrite(fileBytes);
            response.ContentType = "";
            response.Flush();
            response.End();
        }
    }
}
