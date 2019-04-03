// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="MarkerCollection.cs" company="Zeroit Dev">
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
    /// An immutable collection of markers in a <see cref="Scintilla" /> control.
    /// </summary>
    public class MarkerCollection : IEnumerable<Marker>
    {
        private readonly ZeroitCodeExplorer Scintilla;

        /// <summary>
        /// Provides an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An object for enumerating all <see cref="Marker" /> objects within the <see cref="MarkerCollection" />.</returns>
        public IEnumerator<Marker> GetEnumerator()
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
        /// Gets the number of markers in the <see cref="MarkerCollection" />.
        /// </summary>
        /// <returns>This property always returns 32.</returns>
        public int Count
        {
            get
            {
                return (NativeMethods.MARKER_MAX + 1);
            }
        }

        /// <summary>
        /// Gets a <see cref="Marker" /> object at the specified index.
        /// </summary>
        /// <param name="index">The marker index.</param>
        /// <returns>An object representing the marker at the specified <paramref name="index" />.</returns>
        /// <remarks>Markers 25 through 31 are used by Scintilla for folding.</remarks>
        public Marker this[int index]
        {
            get
            {
                index = Helpers.Clamp(index, 0, Count - 1);
                return new Marker(Scintilla, index);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkerCollection" /> class.
        /// </summary>
        /// <param name="Scintilla">The <see cref="Scintilla" /> control that created this collection.</param>
        public MarkerCollection(ZeroitCodeExplorer Scintilla)
        {
            this.Scintilla = Scintilla;
        }
    }
}
