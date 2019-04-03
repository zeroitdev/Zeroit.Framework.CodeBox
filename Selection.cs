// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="Selection.cs" company="Zeroit Dev">
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
    /// Represents a selection when there are multiple active selections in a <see cref="Scintilla" /> control.
    /// </summary>
    public class Selection
    {
        private readonly ZeroitCodeExplorer Scintilla;

        /// <summary>
        /// Gets or sets the anchor position of the selection.
        /// </summary>
        /// <returns>The zero-based document position of the selection anchor.</returns>
        public int Anchor
        {
            get
            {
                var pos = Scintilla.DirectMessage(NativeMethods.SCI_GETSELECTIONNANCHOR, new IntPtr(Index)).ToInt32();
                if (pos <= 0)
                    return pos;

                return Scintilla.Lines.ByteToCharPosition(pos);
            }
            set
            {
                value = Helpers.Clamp(value, 0, Scintilla.TextLength);
                value = Scintilla.Lines.CharToBytePosition(value);
                Scintilla.DirectMessage(NativeMethods.SCI_SETSELECTIONNANCHOR, new IntPtr(Index), new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets the amount of anchor virtual space.
        /// </summary>
        /// <returns>The amount of virtual space past the end of the line offsetting the selection anchor.</returns>
        public int AnchorVirtualSpace
        {
            get
            {
                return Scintilla.DirectMessage(NativeMethods.SCI_GETSELECTIONNANCHORVIRTUALSPACE, new IntPtr(Index)).ToInt32();
            }
            set
            {
                value = Helpers.ClampMin(value, 0);
                Scintilla.DirectMessage(NativeMethods.SCI_SETSELECTIONNANCHORVIRTUALSPACE, new IntPtr(Index), new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets the caret position of the selection.
        /// </summary>
        /// <returns>The zero-based document position of the selection caret.</returns>
        public int Caret
        {
            get
            {
                var pos = Scintilla.DirectMessage(NativeMethods.SCI_GETSELECTIONNCARET, new IntPtr(Index)).ToInt32();
                if (pos <= 0)
                    return pos;

                return Scintilla.Lines.ByteToCharPosition(pos);
            }
            set
            {
                value = Helpers.Clamp(value, 0, Scintilla.TextLength);
                value = Scintilla.Lines.CharToBytePosition(value);
                Scintilla.DirectMessage(NativeMethods.SCI_SETSELECTIONNCARET, new IntPtr(Index), new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets the amount of caret virtual space.
        /// </summary>
        /// <returns>The amount of virtual space past the end of the line offsetting the selection caret.</returns>
        public int CaretVirtualSpace
        {
            get
            {
                return Scintilla.DirectMessage(NativeMethods.SCI_GETSELECTIONNCARETVIRTUALSPACE, new IntPtr(Index)).ToInt32();
            }
            set
            {
                value = Helpers.ClampMin(value, 0);
                Scintilla.DirectMessage(NativeMethods.SCI_SETSELECTIONNCARETVIRTUALSPACE, new IntPtr(Index), new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets the end position of the selection.
        /// </summary>
        /// <returns>The zero-based document position where the selection ends.</returns>
        public int End
        {
            get
            {
                var pos = Scintilla.DirectMessage(NativeMethods.SCI_GETSELECTIONNEND, new IntPtr(Index)).ToInt32();
                if (pos <= 0)
                    return pos;

                return Scintilla.Lines.ByteToCharPosition(pos);
            }
            set
            {
                value = Helpers.Clamp(value, 0, Scintilla.TextLength);
                value = Scintilla.Lines.CharToBytePosition(value);
                Scintilla.DirectMessage(NativeMethods.SCI_SETSELECTIONNEND, new IntPtr(Index), new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets the selection index.
        /// </summary>
        /// <returns>The zero-based selection index within the <see cref="SelectionCollection" /> that created it.</returns>
        public int Index { get; private set; }

        /// <summary>
        /// Gets or sets the start position of the selection.
        /// </summary>
        /// <returns>The zero-based document position where the selection starts.</returns>
        public int Start
        {
            get
            {
                var pos = Scintilla.DirectMessage(NativeMethods.SCI_GETSELECTIONNSTART, new IntPtr(Index)).ToInt32();
                if (pos <= 0)
                    return pos;

                return Scintilla.Lines.ByteToCharPosition(pos);
            }
            set
            {
                value = Helpers.Clamp(value, 0, Scintilla.TextLength);
                value = Scintilla.Lines.CharToBytePosition(value);
                Scintilla.DirectMessage(NativeMethods.SCI_SETSELECTIONNSTART, new IntPtr(Index), new IntPtr(value));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Selection" /> class.
        /// </summary>
        /// <param name="Scintilla">The <see cref="Scintilla" /> control that created this selection.</param>
        /// <param name="index">The index of this selection within the <see cref="SelectionCollection" /> that created it.</param>
        public Selection(ZeroitCodeExplorer Scintilla, int index)
        {
            this.Scintilla = Scintilla;
            Index = index;
        }
    }
}
