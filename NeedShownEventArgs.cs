// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="NeedShownEventArgs.cs" company="Zeroit Dev">
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
    /// Provides data for the <see cref="Scintilla.NeedShown" /> event.
    /// </summary>
    public class NeedShownEventArgs : EventArgs
    {
        private readonly ZeroitCodeExplorer Scintilla;
        private readonly int bytePosition;
        private readonly int byteLength;
        private int? position;
        private int? length;

        /// <summary>
        /// Gets the length of the text that needs to be shown.
        /// </summary>
        /// <returns>The length of text starting at <see cref="Position" /> that needs to be shown.</returns>
        public int Length
        {
            get
            {
                if (length == null)
                {
                    var endBytePosition = (bytePosition + byteLength);
                    var endPosition = Scintilla.Lines.ByteToCharPosition(endBytePosition);
                    length = (endPosition - Position);
                }

                return (int)length;
            }
        }

        /// <summary>
        /// Gets the zero-based document position where text needs to be shown.
        /// </summary>
        /// <returns>The zero-based document position where the range of text to be shown starts.</returns>
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
        /// Initializes a new instance of the <see cref="NeedShownEventArgs" /> class.
        /// </summary>
        /// <param name="Scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
        /// <param name="bytePosition">The zero-based byte position within the document where text needs to be shown.</param>
        /// <param name="byteLength">The length in bytes of the text that needs to be shown.</param>
        public NeedShownEventArgs(ZeroitCodeExplorer Scintilla, int bytePosition, int byteLength)
        {
            this.Scintilla = Scintilla;
            this.bytePosition = bytePosition;
            this.byteLength = byteLength;
        }
    }
}
