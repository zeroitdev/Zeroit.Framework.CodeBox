// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="AutoCSelectionEventArgs.cs" company="Zeroit Dev">
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
    /// Provides data for the <see cref="Scintilla.AutoCSelection" /> event.
    /// </summary>
    public class AutoCSelectionEventArgs : EventArgs
    {
        private readonly ZeroitCodeExplorer Scintilla;
        private readonly IntPtr textPtr;
        private readonly int bytePosition;
        private int? position;
        private string text;

        /// <summary>
        /// Gets the fillup character that caused the completion.
        /// </summary>
        /// <returns>The fillup character used to cause the completion; otherwise, 0.</returns>
        /// <remarks>Only a <see cref="ListCompletionMethod" /> of <see cref="Zeroit.Framework.CodeBox.ListCompletionMethod.FillUp" /> will return a non-zero character.</remarks>
        /// <seealso cref="Scintilla.AutoCSetFillUps" />
        public int Char { get; private set; }

        /// <summary>
        /// Gets a value indicating how the completion occurred.
        /// </summary>
        /// <returns>One of the <see cref="Zeroit.Framework.CodeBox.ListCompletionMethod" /> enumeration values.</returns>
        public ListCompletionMethod ListCompletionMethod { get; private set; }

        /// <summary>
        /// Gets the start position of the word being completed.
        /// </summary>
        /// <returns>The zero-based document position of the word being completed.</returns>
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
        /// Gets the text of the selected autocompletion item.
        /// </summary>
        /// <returns>The selected autocompletion item text.</returns>
        public unsafe string Text
        {
            get
            {
                if (text == null)
                {
                    var len = 0;
                    while (((byte*)textPtr)[len] != 0)
                        len++;

                    text = Helpers.GetString(textPtr, len, Scintilla.Encoding);
                }

                return text;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCSelectionEventArgs" /> class.
        /// </summary>
        /// <param name="Scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
        /// <param name="bytePosition">The zero-based byte position within the document of the word being completed.</param>
        /// <param name="text">A pointer to the selected autocompletion text.</param>
        /// <param name="ch">The character that caused the completion.</param>
        /// <param name="listCompletionMethod">A value indicating the way in which the completion occurred.</param>
        public AutoCSelectionEventArgs(ZeroitCodeExplorer Scintilla, int bytePosition, IntPtr text, int ch, ListCompletionMethod listCompletionMethod)
        {
            this.Scintilla = Scintilla;
            this.bytePosition = bytePosition;
            this.textPtr = text;
            Char = ch;
            ListCompletionMethod = listCompletionMethod;
        }
    }
}
