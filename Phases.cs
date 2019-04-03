// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="Phases.cs" company="Zeroit Dev">
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
    /// The number of phases used when drawing.
    /// </summary>
    public enum Phases
    {
        /// <summary>
        /// Drawing is done in a single phase. This is the fastest but provides no support for kerning.
        /// </summary>
        One = NativeMethods.SC_PHASES_ONE,

        /// <summary>
        /// Drawing is done in two phases; the background first and then the text. This is the default.
        /// </summary>
        Two = NativeMethods.SC_PHASES_TWO,

        /// <summary>
        /// Drawing is done in multiple phases; once for each feature. This is the slowest but allows
        /// extreme ascenders and descenders to overflow into adjacent lines.
        /// </summary>
        Multiple = NativeMethods.SC_PHASES_MULTIPLE
    }
}
