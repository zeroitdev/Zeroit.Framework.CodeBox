// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="ListCompletionMethod.cs" company="Zeroit Dev">
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
    /// Indicates how an autocompletion occurred.
    /// </summary>
    public enum ListCompletionMethod
    {
        /// <summary>
        /// A fillup character (see <see cref="Scintilla.AutoCSetFillUps" />) triggered the completion.
        /// The character used is indicated by the <see cref="AutoCSelectionEventArgs.Char" /> property.
        /// </summary>
        FillUp = NativeMethods.SC_AC_FILLUP,

        /// <summary>
        /// A double-click triggered the completion.
        /// </summary>
        DoubleClick = NativeMethods.SC_AC_DOUBLECLICK,

        /// <summary>
        /// A tab key or the <see cref="Zeroit.Framework.CodeBox.Command.Tab" /> command triggered the completion.
        /// </summary>
        Tab = NativeMethods.SC_AC_TAB,

        /// <summary>
        /// A new line or <see cref="Zeroit.Framework.CodeBox.Command.NewLine" /> command triggered the completion.
        /// </summary>
        NewLine = NativeMethods.SC_AC_NEWLINE,

        /// <summary>
        /// The <see cref="Scintilla.AutoCSelect" /> method triggered the completion.
        /// </summary>
        Command = NativeMethods.SC_AC_COMMAND
    }
}
