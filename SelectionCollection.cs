// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="SelectionCollection.cs" company="Zeroit Dev">
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
using System.Collections;
using System.Collections.Generic;

namespace Zeroit.Framework.CodeBox
{
    /// <summary>
    /// A multiple selection collection.
    /// </summary>
    public class SelectionCollection : IEnumerable<Selection>
    {
        private readonly ZeroitCodeExplorer Scintilla;

        /// <summary>
        /// Provides an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An object that contains all <see cref="Selection" /> objects within the <see cref="SelectionCollection" />.</returns>
        public IEnumerator<Selection> GetEnumerator()
        {
            int count = Count;
            for (int i = 0; i < count; i++)
                yield return this[i];

            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Gets the number of active selections.
        /// </summary>
        /// <returns>The number of selections in the <see cref="SelectionCollection" />.</returns>
        public int Count
        {
            get
            {
                return Scintilla.DirectMessage(NativeMethods.SCI_GETSELECTIONS).ToInt32();
            }
        }

        /// <summary>
        /// Gets a value indicating whether all selection ranges are empty.
        /// </summary>
        /// <returns>true if all selection ranges are empty; otherwise, false.</returns>
        public bool IsEmpty
        {
            get
            {
                return Scintilla.DirectMessage(NativeMethods.SCI_GETSELECTIONEMPTY) != IntPtr.Zero;
            }
        }

        /// <summary>
        /// Gets the <see cref="Selection" /> at the specified zero-based index.
        /// </summary>
        /// <param name="index">The zero-based index of the <see cref="Selection" /> to get.</param>
        /// <returns>The <see cref="Selection" /> at the specified index.</returns>
        public Selection this[int index]
        {
            get
            {
                index = Helpers.Clamp(index, 0, Count - 1);
                return new Selection(Scintilla, index);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectionCollection" /> class.
        /// </summary>
        /// <param name="Scintilla"></param>
        public SelectionCollection(ZeroitCodeExplorer Scintilla)
        {
            this.Scintilla = Scintilla;
        }
    }
}
