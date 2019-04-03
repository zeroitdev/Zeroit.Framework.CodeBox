// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="ModificationEventArgs.cs" company="Zeroit Dev">
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
    /// Provides data for the <see cref="Scintilla.Insert" /> and <see cref="Scintilla.Delete" /> events.
    /// </summary>
    public class ModificationEventArgs : BeforeModificationEventArgs
    {
        private readonly ZeroitCodeExplorer Scintilla;
        private readonly int bytePosition;
        private readonly int byteLength;
        private readonly IntPtr textPtr;

        /// <summary>
        /// Gets the number of lines added or removed.
        /// </summary>
        /// <returns>The number of lines added to the document when text is inserted, or the number of lines removed from the document when text is deleted.</returns>
        /// <remarks>When lines are deleted the return value will be negative.</remarks>
        public int LinesAdded { get; private set; }

        /// <summary>
        /// Gets the text that was inserted or deleted.
        /// </summary>
        /// <returns>The text inserted or deleted from the document.</returns>
        public override unsafe string Text
        {
            get
            {
                if (CachedText == null)
                    CachedText = Helpers.GetString(textPtr, byteLength, Scintilla.Encoding);

                return CachedText;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModificationEventArgs" /> class.
        /// </summary>
        /// <param name="Scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
        /// <param name="source">The source of the modification.</param>
        /// <param name="bytePosition">The zero-based byte position within the document where text was modified.</param>
        /// <param name="byteLength">The length in bytes of the inserted or deleted text.</param>
        /// <param name="text">>A pointer to the text inserted or deleted.</param>
        /// <param name="linesAdded">The number of lines added or removed (delta).</param>
        public ModificationEventArgs(ZeroitCodeExplorer Scintilla, ModificationSource source, int bytePosition, int byteLength, IntPtr text, int linesAdded) : base(Scintilla, source, bytePosition, byteLength, text)
        {
            this.Scintilla = Scintilla;
            this.bytePosition = bytePosition;
            this.byteLength = byteLength;
            this.textPtr = text;

            LinesAdded = linesAdded;
        }
    }
}
