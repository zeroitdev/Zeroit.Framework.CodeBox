// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="Status.cs" company="Zeroit Dev">
//    This program is for creating a Code Editor control.
//    Copyright ©  2017  Zeroit Dev Technologies
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <https://www.gnu.org/licenses/>.
//
//    You can contact me at zeroitdevnet@gmail.com or zeroitdev@outlook.com
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Zeroit.Framework.CodeBox
{
    /// <summary>
    /// Possible status codes returned by the <see cref="Scintilla.Status" /> property.
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// No failures.
        /// </summary>
        Ok = NativeMethods.SC_STATUS_OK,

        /// <summary>
        /// Generic failure.
        /// </summary>
        Failure = NativeMethods.SC_STATUS_FAILURE,

        /// <summary>
        /// Memory is exhausted.
        /// </summary>
        BadAlloc = NativeMethods.SC_STATUS_BADALLOC,

        /// <summary>
        /// Regular expression is invalid.
        /// </summary>
        WarnRegex = NativeMethods.SC_STATUS_WARN_REGEX
    }
}
