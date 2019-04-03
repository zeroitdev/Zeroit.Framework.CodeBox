// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="IndentView.cs" company="Zeroit Dev">
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
    /// Options for displaying indentation guides in a <see cref="Scintilla" /> control.
    /// </summary>
    /// <remarks>Indentation guides can be styled using the <see cref="Style.IndentGuide" /> style.</remarks>
    public enum IndentView
    {
        /// <summary>
        /// No indentation guides are shown. This is the default.
        /// </summary>
        None = NativeMethods.SC_IV_NONE,

        /// <summary>
        /// Indentation guides are shown inside real indentation whitespace.
        /// </summary>
        Real = NativeMethods.SC_IV_REAL,

        /// <summary>
        /// Indentation guides are shown beyond the actual indentation up to the level of the next non-empty line.
        /// If the previous non-empty line was a fold header then indentation guides are shown for one more level of indent than that line.
        /// This setting is good for Python.
        /// </summary>
        LookForward = NativeMethods.SC_IV_LOOKFORWARD,

        /// <summary>
        /// Indentation guides are shown beyond the actual indentation up to the level of the next non-empty line or previous non-empty line whichever is the greater.
        /// This setting is good for most languages.
        /// </summary>
        LookBoth = NativeMethods.SC_IV_LOOKBOTH
    }
}
