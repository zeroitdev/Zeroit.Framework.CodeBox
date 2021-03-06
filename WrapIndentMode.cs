﻿// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="WrapIndentMode.cs" company="Zeroit Dev">
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
    /// Indenting behavior of wrapped sublines.
    /// </summary>
    public enum WrapIndentMode
    {
        /// <summary>
        /// Wrapped sublines aligned to left of window plus the amount set by <see cref="Zeroit.Framework.CodeBox.Scintilla.WrapStartIndent" />.
        /// This is the default.
        /// </summary>
        Fixed,

        /// <summary>
        /// Wrapped sublines are aligned to first subline indent.
        /// </summary>
        Same,

        /// <summary>
        /// Wrapped sublines are aligned to first subline indent plus one more level of indentation.
        /// </summary>
        Indent = NativeMethods.SC_WRAPINDENT_INDENT
    }
}
