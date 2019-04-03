// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="IdleStyling.cs" company="Zeroit Dev">
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
    /// Possible strategies for styling text using application idle time.
    /// </summary>
    /// <seealso cref="Scintilla.IdleStyling" />
    public enum IdleStyling
    {
        /// <summary>
        /// Syntax styling is performed for all the currently visible text before displaying it.
        /// This is the default.
        /// </summary>
        None = NativeMethods.SC_IDLESTYLING_NONE,

        /// <summary>
        /// A small amount of styling is performed before display and then further styling is performed incrementally in the background as an idle-time task.
        /// This can improve initial display/scroll performance, but may result in the text initially appearing uncolored and then, some time later, it is colored.
        /// </summary>
        ToVisible = NativeMethods.SC_IDLESTYLING_TOVISIBLE,

        /// <summary>
        /// Text after the currently visible portion may be styled as an idle-time task.
        /// This will not improve initial display/scroll performance, but may improve subsequent display/scroll performance.
        /// </summary>
        AfterVisible = NativeMethods.SC_IDLESTYLING_AFTERVISIBLE,

        /// <summary>
        /// Text before and after the current visible text.
        /// This is a combination of <see cref="ToVisible" /> and <see cref="AfterVisible" />.
        /// </summary>
        All = NativeMethods.SC_IDLESTYLING_ALL
    }
}
