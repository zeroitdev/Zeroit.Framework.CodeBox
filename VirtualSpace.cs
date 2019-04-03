// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="VirtualSpace.cs" company="Zeroit Dev">
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
    /// Enables virtual space for rectangular selections or in other circumstances or in both.
    /// </summary>
    /// <remarks>This enumeration has a FlagsAttribute attribute that allows a bitwise combination of its member values.</remarks>
    [Flags]
    public enum VirtualSpace
    {
        /// <summary>
        /// Virtual space is not enabled. This is the default.
        /// </summary>
        None = NativeMethods.SCVS_NONE,

        /// <summary>
        /// Virtual space is enabled for rectangular selections.
        /// </summary>
        RectangularSelection = NativeMethods.SCVS_RECTANGULARSELECTION,

        /// <summary>
        /// Virtual space is user accessible.
        /// </summary>
        UserAccessible = NativeMethods.SCVS_USERACCESSIBLE,

        /// <summary>
        /// Prevents left arrow movement and selection from wrapping to the previous line.
        /// </summary>
        NoWrapLineStart = NativeMethods.SCVS_NOWRAPLINESTART
    }
}
