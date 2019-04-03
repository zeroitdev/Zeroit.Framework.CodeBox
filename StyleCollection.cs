// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="StyleCollection.cs" company="Zeroit Dev">
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
using System.Collections;
using System.Collections.Generic;

namespace Zeroit.Framework.CodeBox
{
    /// <summary>
    /// An immutable collection of style definitions in a <see cref="Scintilla" /> control.
    /// </summary>
    public class StyleCollection : IEnumerable<Style>
    {
        private readonly ZeroitCodeExplorer Scintilla;

        /// <summary>
        /// Provides an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An object that contains all <see cref="Style" /> objects within the <see cref="StyleCollection" />.</returns>
        public IEnumerator<Style> GetEnumerator()
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
        /// Gets the number of styles.
        /// </summary>
        /// <returns>The number of styles in the <see cref="StyleCollection" />.</returns>
        public int Count
        {
            get
            {
                return (NativeMethods.STYLE_MAX + 1);
            }
        }

        /// <summary>
        /// Gets a <see cref="Style" /> object at the specified index.
        /// </summary>
        /// <param name="index">The style definition index.</param>
        /// <returns>An object representing the style definition at the specified <paramref name="index" />.</returns>
        /// <remarks>Styles 32 through 39 have special significance.</remarks>
        public Style this[int index]
        {
            get
            {
                index = Helpers.Clamp(index, 0, Count - 1);
                return new Style(Scintilla, index);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StyleCollection" /> class.
        /// </summary>
        /// <param name="Scintilla">The <see cref="Scintilla" /> control that created this collection.</param>
        public StyleCollection(ZeroitCodeExplorer Scintilla)
        {
            this.Scintilla = Scintilla;
        }
    }
}
