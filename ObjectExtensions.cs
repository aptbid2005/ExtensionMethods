// ***********************************************************************
// Assembly         : Extensions.Core
// Author           : Jason Nowicki
// Created          : 02-28-2018
//
// Last Modified By : Jason Nowicki
// Last Modified On : 02-28-2018
// ***********************************************************************
// <copyright file="ObjectExtensions.cs" company="EECPPC KY.gov">
//     Copyright ©  2018
// </copyright>
// ***********************************************************************
using System;

namespace Extensions.Core
{
    /// <summary>
    /// Class ObjectExtensions.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Converts to.
        /// </summary>
        /// <typeparam name="TValue">The type of the t value.</typeparam>
        /// <param name="obj">The object.</param>
        /// <returns>TValue.</returns>
        /// <exception cref="NotSupportedException"></exception>
        public static TValue ConvertTo<TValue>(this object obj)
        {
            TValue res = default(TValue);
            System.ComponentModel.TypeConverter tc = System.ComponentModel.TypeDescriptor.GetConverter(typeof(TValue));
            if (tc.CanConvertFrom(obj.GetType()))
                res = (TValue)tc.ConvertFrom(obj);
            else
            {
                tc = System.ComponentModel.TypeDescriptor.GetConverter(obj.GetType());
                if (tc.CanConvertTo(typeof(TValue)))
                    res = (TValue)tc.ConvertTo(obj, typeof(TValue));
                else
                    throw new NotSupportedException();
            }
            return res;
        }


    }
}