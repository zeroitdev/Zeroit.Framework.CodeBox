// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="UpdateChange.cs" company="Zeroit Dev">
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
    /// Specifies the change that triggered a <see cref="Scintilla.UpdateUI" /> event.
    /// </summary>
    /// <remarks>This enumeration has a FlagsAttribute attribute that allows a bitwise combination of its member values.</remarks>
    [Flags]
    public enum UpdateChange
    {
        /// <summary>
        /// Contents, styling or markers have been changed.
        /// </summary>
        Content = NativeMethods.SC_UPDATE_CONTENT,

        /// <summary>
        /// Selection has been changed.
        /// </summary>
        Selection = NativeMethods.SC_UPDATE_SELECTION,

        /// <summary>
        /// Scrolled vertically.
        /// </summary>
        VScroll = NativeMethods.SC_UPDATE_V_SCROLL,

        /// <summary>
        /// Scrolled horizontally.
        /// </summary>
        HScroll = NativeMethods.SC_UPDATE_H_SCROLL
    }
}
