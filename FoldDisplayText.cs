// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="FoldDisplayText.cs" company="Zeroit Dev">
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
    /// Display options for fold text tags.
    /// </summary>
    public enum FoldDisplayText
    {
        /// <summary>
        /// Do not display the text tags. This is the default.
        /// </summary>
        Hidden = NativeMethods.SC_FOLDDISPLAYTEXT_HIDDEN,

        /// <summary>
        /// Display the text tags.
        /// </summary>
        Standard = NativeMethods.SC_FOLDDISPLAYTEXT_STANDARD,

        /// <summary>
        /// Display the text tags with a box drawn around them.
        /// </summary>
        Boxed = NativeMethods.SC_FOLDDISPLAYTEXT_BOXED
    }
}
