// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="DwellEventArgs.cs" company="Zeroit Dev">
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
    /// Provides data for the <see cref="Scintilla.DwellStart" /> and <see cref="Scintilla.DwellEnd" /> events.
    /// </summary>
    public class DwellEventArgs : EventArgs
    {
        private readonly ZeroitCodeExplorer Scintilla;
        private readonly int bytePosition;
        private int? position;

        /// <summary>
        /// Gets the zero-based document position where the mouse pointer was lingering.
        /// </summary>
        /// <returns>The nearest zero-based document position to where the mouse pointer was lingering.</returns>
        public int Position
        {
            get
            {
                if (position == null)
                    position = Scintilla.Lines.ByteToCharPosition(bytePosition);

                return (int)position;
            }
        }

        /// <summary>
        /// Gets the x-coordinate of the mouse pointer.
        /// </summary>
        /// <returns>The x-coordinate of the mouse pointer relative to the <see cref="Scintilla" /> control.</returns>
        public int X { get; private set; }

        /// <summary>
        /// Gets the y-coordinate of the mouse pointer.
        /// </summary>
        /// <returns>The y-coordinate of the mouse pointer relative to the <see cref="Scintilla" /> control.</returns>
        public int Y { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DwellEventArgs" /> class.
        /// </summary>
        /// <param name="Scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
        /// <param name="bytePosition">The zero-based byte position within the document where the mouse pointer was lingering.</param>
        /// <param name="x">The x-coordinate of the mouse pointer relative to the <see cref="Scintilla" /> control.</param>
        /// <param name="y">The y-coordinate of the mouse pointer relative to the <see cref="Scintilla" /> control.</param>
        public DwellEventArgs(ZeroitCodeExplorer Scintilla, int bytePosition, int x, int y)
        {
            this.Scintilla = Scintilla;
            this.bytePosition = bytePosition;
            X = x;
            Y = y;

            // The position is not over text
            if (bytePosition < 0)
                position = bytePosition;
        }
    }
}
