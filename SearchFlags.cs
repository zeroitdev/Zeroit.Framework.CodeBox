// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="SearchFlags.cs" company="Zeroit Dev">
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
    /// Specifies the how patterns are matched when performing a search in a <see cref="Scintilla" /> control.
    /// </summary>
    /// <remarks>This enumeration has a FlagsAttribute attribute that allows a bitwise combination of its member values.</remarks>
    [Flags]
    public enum SearchFlags
    {
        /// <summary>
        /// Matches every instance of the search string.
        /// </summary>
        None = 0,

        /// <summary>
        /// A match only occurs with text that matches the case of the search string.
        /// </summary>
        MatchCase = NativeMethods.SCFIND_MATCHCASE,

        /// <summary>
        /// A match only occurs if the characters before and after are not word characters.
        /// </summary>
        WholeWord = NativeMethods.SCFIND_WHOLEWORD,

        /// <summary>
        /// A match only occurs if the character before is not a word character.
        /// </summary>
        WordStart = NativeMethods.SCFIND_WORDSTART,

        /// <summary>
        /// The search string should be interpreted as a regular expression.
        /// Regular expressions will only match ranges within a single line, never matching over multiple lines.
        /// </summary>
        Regex = NativeMethods.SCFIND_REGEXP,

        /// <summary>
        /// Treat regular expression in a more POSIX compatible manner by interpreting bare '(' and ')' for tagged sections rather than "\(" and "\)".
        /// </summary>
        Posix = NativeMethods.SCFIND_POSIX,

        /// <summary>
        /// The search string should be interpreted as a regular expression and use the C++11 &lt;regex&gt; standard library engine.
        /// The <see cref="Scintilla.Status" /> property can queried to determine if the regular expression is invalid.
        /// The ECMAScript flag is set on the regex object and documents will exhibit Unicode-compliant behaviour.
        /// Regular expressions will only match ranges within a single line, never matching over multiple lines.
        /// </summary>
        Cxx11Regex = NativeMethods.SCFIND_CXX11REGEX
    }
}
