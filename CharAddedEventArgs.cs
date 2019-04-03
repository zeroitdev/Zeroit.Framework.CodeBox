// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="CharAddedEventArgs.cs" company="Zeroit Dev">
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
    /// Provides data for the <see cref="Scintilla.CharAdded" /> event.
    /// </summary>
    public class CharAddedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the text character added to a <see cref="Scintilla" /> control.
        /// </summary>
        /// <returns>The character added.</returns>
        public int Char { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CharAddedEventArgs" /> class.
        /// </summary>
        /// <param name="ch">The character added.</param>
        public CharAddedEventArgs(int ch)
        {
            Char = ch;
        }
    }
}
