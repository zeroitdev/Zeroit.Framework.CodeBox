// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="Eol.cs" company="Zeroit Dev">
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
    /// End-of-line format.
    /// </summary>
    public enum Eol
    {
        /// <summary>
        /// Carriage Return, Line Feed pair "\r\n" (0x0D0A).
        /// </summary>
        CrLf = NativeMethods.SC_EOL_CRLF,

        /// <summary>
        /// Carriage Return '\r' (0x0D).
        /// </summary>
        Cr = NativeMethods.SC_EOL_CR,

        /// <summary>
        /// Line Feed '\n' (0x0A).
        /// </summary>
        Lf = NativeMethods.SC_EOL_LF
    }
}
