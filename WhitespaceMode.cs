// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="WhitespaceMode.cs" company="Zeroit Dev">
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
    /// Specifies the display mode of whitespace characters.
    /// </summary>
    public enum WhitespaceMode
    {
        /// <summary>
        /// The normal display mode with whitespace displayed as an empty background color.
        /// </summary>
        Invisible = NativeMethods.SCWS_INVISIBLE,

        /// <summary>
        /// Whitespace characters are drawn as dots and arrows.
        /// </summary>
        VisibleAlways = NativeMethods.SCWS_VISIBLEALWAYS,

        /// <summary>
        /// Whitespace used for indentation is displayed normally but after the first visible character,
        /// it is shown as dots and arrows.
        /// </summary>
        VisibleAfterIndent = NativeMethods.SCWS_VISIBLEAFTERINDENT,

        /// <summary>
        /// Whitespace used for indentation is displayed as dots and arrows.
        /// </summary>
        VisibleOnlyIndent = NativeMethods.SCWS_VISIBLEONLYININDENT
    }
}
