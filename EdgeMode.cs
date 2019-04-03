// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="EdgeMode.cs" company="Zeroit Dev">
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
    /// The long line edge display mode.
    /// </summary>
    public enum EdgeMode
    {
        /// <summary>
        /// Long lines are not indicated. This is the default.
        /// </summary>
        None = NativeMethods.EDGE_NONE,

        /// <summary>
        /// Long lines are indicated with a vertical line.
        /// </summary>
        Line = NativeMethods.EDGE_LINE,

        /// <summary>
        /// Long lines are indicated with a background color.
        /// </summary>
        Background = NativeMethods.EDGE_BACKGROUND,

        /// <summary>
        /// Similar to <see cref="Line" /> except allows for multiple vertical lines to be visible using the <see cref="Scintilla.MultiEdgeAddLine" /> method.
        /// </summary>
        /// <remarks><see cref="Line" /> and <see cref="Scintilla.EdgeColumn" /> are completely independant of this mode.</remarks>
        MultiLine = NativeMethods.EDGE_MULTILINE
    }
}
