// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="WrapVisualFlags.cs" company="Zeroit Dev">
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
    /// The visual indicator used on a wrapped line.
    /// </summary>
    [Flags]
    public enum WrapVisualFlags
    {
        /// <summary>
        /// No visual indicator is displayed. This the default.
        /// </summary>
        None = NativeMethods.SC_WRAPVISUALFLAG_NONE,

        /// <summary>
        /// A visual indicator is displayed at th end of a wrapped subline.
        /// </summary>
        End = NativeMethods.SC_WRAPVISUALFLAG_END,

        /// <summary>
        /// A visual indicator is displayed at the beginning of a subline.
        /// The subline is indented by 1 pixel to make room for the display.
        /// </summary>
        Start = NativeMethods.SC_WRAPVISUALFLAG_START,

        /// <summary>
        /// A visual indicator is displayed in the number margin.
        /// </summary>
        Margin = NativeMethods.SC_WRAPVISUALFLAG_MARGIN
    }
}
