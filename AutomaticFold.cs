// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="AutomaticFold.cs" company="Zeroit Dev">
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
    /// Configuration options for automatic code folding.
    /// </summary>
    /// <remarks>This enumeration has a FlagsAttribute attribute that allows a bitwise combination of its member values.</remarks>
    [Flags]
    public enum AutomaticFold
    {
        /// <summary>
        /// Automatic folding is disabled. This is the default.
        /// </summary>
        None = 0,

        /// <summary>
        /// Automatically show lines as needed. The <see cref="Scintilla.NeedShown" /> event is not raised when this value is used.
        /// </summary>
        Show = NativeMethods.SC_AUTOMATICFOLD_SHOW,

        /// <summary>
        /// Handle clicks in fold margin automatically. The <see cref="Scintilla.MarginClick" /> event is not raised for folding margins when this value is used.
        /// </summary>
        Click = NativeMethods.SC_AUTOMATICFOLD_CLICK,

        /// <summary>
        /// Show lines as needed when the fold structure is changed.
        /// </summary>
        Change = NativeMethods.SC_AUTOMATICFOLD_CHANGE
    }
}
