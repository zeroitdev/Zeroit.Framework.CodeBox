// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="IndicatorCollection.cs" company="Zeroit Dev">
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
using System.ComponentModel;

namespace Zeroit.Framework.CodeBox
{
    /// <summary>
    /// An immutable collection of indicators in a <see cref="Scintilla" /> control.
    /// </summary>
    public class IndicatorCollection : IEnumerable<Indicator>
    {
        private readonly ZeroitCodeExplorer Scintilla;

        /// <summary>
        /// Provides an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An object that contains all <see cref="Indicator" /> objects within the <see cref="IndicatorCollection" />.</returns>
        public IEnumerator<Indicator> GetEnumerator()
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
        /// Gets the number of indicators.
        /// </summary>
        /// <returns>The number of indicators in the <see cref="IndicatorCollection" />.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Count
        {
            get
            {
                return (NativeMethods.INDIC_MAX + 1);
            }
        }

        /// <summary>
        /// Gets an <see cref="Indicator" /> object at the specified index.
        /// </summary>
        /// <param name="index">The indicator index.</param>
        /// <returns>An object representing the indicator at the specified <paramref name="index" />.</returns>
        /// <remarks>
        /// Indicators 0 through 7 are used by lexers.
        /// Indicators 32 through 35 are used for IME.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Indicator this[int index]
        {
            get
            {
                index = Helpers.Clamp(index, 0, Count - 1);
                return new Indicator(Scintilla, index);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndicatorCollection" /> class.
        /// </summary>
        /// <param name="Scintilla">The <see cref="Scintilla" /> control that created this collection.</param>
        public IndicatorCollection(ZeroitCodeExplorer Scintilla)
        {
            this.Scintilla = Scintilla;
        }
    }
}
