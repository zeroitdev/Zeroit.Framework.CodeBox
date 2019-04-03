// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="InsertCheckEventArgs.cs" company="Zeroit Dev">
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
    /// Provides data for the <see cref="Scintilla.InsertCheck" /> event.
    /// </summary>
    public class InsertCheckEventArgs : EventArgs
    {
        private readonly ZeroitCodeExplorer Scintilla;
        private readonly int bytePosition;
        private readonly int byteLength;
        private readonly IntPtr textPtr;

        internal int? CachedPosition { get; set; }
        internal string CachedText { get; set; }

        /// <summary>
        /// Gets the zero-based document position where text will be inserted.
        /// </summary>
        /// <returns>The zero-based character position within the document where text will be inserted.</returns>
        public int Position
        {
            get
            {
                if (CachedPosition == null)
                    CachedPosition = Scintilla.Lines.ByteToCharPosition(bytePosition);

                return (int)CachedPosition;
            }
        }

        /// <summary>
        /// Gets or sets the text being inserted.
        /// </summary>
        /// <returns>The text being inserted into the document.</returns>
        public unsafe string Text
        {
            get
            {
                if (CachedText == null)
                    CachedText = Helpers.GetString(textPtr, byteLength, Scintilla.Encoding);

                return CachedText;
            }
            set
            {
                CachedText = value ?? string.Empty;

                var bytes = Helpers.GetBytes(CachedText, Scintilla.Encoding, zeroTerminated: false);
                fixed (byte* bp = bytes)
                    Scintilla.DirectMessage(NativeMethods.SCI_CHANGEINSERTION, new IntPtr(bytes.Length), new IntPtr(bp));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InsertCheckEventArgs" /> class.
        /// </summary>
        /// <param name="Scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
        /// <param name="bytePosition">The zero-based byte position within the document where text is being inserted.</param>
        /// <param name="byteLength">The length in bytes of the inserted text.</param>
        /// <param name="text">A pointer to the text being inserted.</param>
        public InsertCheckEventArgs(ZeroitCodeExplorer Scintilla, int bytePosition, int byteLength, IntPtr text)
        {
            this.Scintilla = Scintilla;
            this.bytePosition = bytePosition;
            this.byteLength = byteLength;
            this.textPtr = text;
        }
    }
}
