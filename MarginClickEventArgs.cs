﻿// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="MarginClickEventArgs.cs" company="Zeroit Dev">
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
using System.Windows.Forms;

namespace Zeroit.Framework.CodeBox
{
    /// <summary>
    /// Provides data for the <see cref="Scintilla.MarginClick" /> event.
    /// </summary>
    public class MarginClickEventArgs : EventArgs
    {
        private readonly ZeroitCodeExplorer Scintilla;
        private readonly int bytePosition;
        private int? position;

        /// <summary>
        /// Gets the margin clicked.
        /// </summary>
        /// <returns>The zero-based index of the clicked margin.</returns>
        public int Margin { get; private set; }

        /// <summary>
        /// Gets the modifier keys (SHIFT, CTRL, ALT) held down when the margin was clicked.
        /// </summary>
        /// <returns>A bitwise combination of the Keys enumeration indicating the modifier keys.</returns>
        public Keys Modifiers { get; private set; }

        /// <summary>
        /// Gets the zero-based document position where the line ajacent to the clicked margin starts.
        /// </summary>
        /// <returns>The zero-based character position within the document of the start of the line adjacent to the margin clicked.</returns>
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
        /// Initializes a new instance of the <see cref="MarginClickEventArgs" /> class.
        /// </summary>
        /// <param name="Scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
        /// <param name="modifiers">The modifier keys that where held down at the time of the margin click.</param>
        /// <param name="bytePosition">The zero-based byte position within the document where the line adjacent to the clicked margin starts.</param>
        /// <param name="margin">The zero-based index of the clicked margin.</param>
        public MarginClickEventArgs(ZeroitCodeExplorer Scintilla, Keys modifiers, int bytePosition, int margin)
        {
            this.Scintilla = Scintilla;
            this.bytePosition = bytePosition;
            Modifiers = modifiers;
            Margin = margin;
        }
    }
}
