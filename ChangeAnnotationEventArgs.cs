// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="ChangeAnnotationEventArgs.cs" company="Zeroit Dev">
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
    /// Provides data for the <see cref="Scintilla.ChangeAnnotation" /> event.
    /// </summary>
    public class ChangeAnnotationEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the line index where the annotation changed.
        /// </summary>
        /// <returns>The zero-based line index where the annotation change occurred.</returns>
        public int Line { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeAnnotationEventArgs" /> class.
        /// </summary>
        /// <param name="line">The zero-based line index of the annotation that changed.</param>
        public ChangeAnnotationEventArgs(int line)
        {
            Line = line;
        }
    }
}
