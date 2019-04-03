// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="LineEndType.cs" company="Zeroit Dev">
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
using System;

namespace Zeroit.Framework.CodeBox
{
    /// <summary>
    /// Line endings types supported by lexers and allowed by a <see cref="Scintilla" /> control.
    /// </summary>
    /// <seealso cref="Scintilla.LineEndTypesSupported" />
    /// <seealso cref="Scintilla.LineEndTypesAllowed" />
    /// <seealso cref="Scintilla.LineEndTypesActive" />
    [Flags]
    public enum LineEndType
    {
        /// <summary>
        /// ASCII line endings. Carriage Return, Line Feed pair "\r\n" (0x0D0A); Carriage Return '\r' (0x0D); Line Feed '\n' (0x0A).
        /// </summary>
        Default = NativeMethods.SC_LINE_END_TYPE_DEFAULT,

        /// <summary>
        /// Unicode line endings. Next Line (0x0085); Line Separator (0x2028); Paragraph Separator (0x2029).
        /// </summary>
        Unicode = NativeMethods.SC_LINE_END_TYPE_UNICODE
    }
}
