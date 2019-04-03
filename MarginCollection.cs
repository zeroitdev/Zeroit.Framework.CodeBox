// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="MarginCollection.cs" company="Zeroit Dev">
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
using System.ComponentModel;

namespace Zeroit.Framework.CodeBox
{
    /// <summary>
    /// An immutable collection of margins in a <see cref="Scintilla" /> control.
    /// </summary>
    public class MarginCollection : IEnumerable<Margin>
    {
        private readonly ZeroitCodeExplorer Scintilla;

        /// <summary>
        /// Removes all text displayed in every <see cref="MarginType.Text" /> and <see cref="MarginType.RightText" /> margins.
        /// </summary>
        public void ClearAllText()
        {
            Scintilla.DirectMessage(NativeMethods.SCI_MARGINTEXTCLEARALL);
        }

        /// <summary>
        /// Provides an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An object that contains all <see cref="Margin" /> objects within the <see cref="MarginCollection" />.</returns>
        public IEnumerator<Margin> GetEnumerator()
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
        /// Gets or sets the number of margins in the <see cref="MarginCollection" />.
        /// </summary>
        /// <returns>The number of margins in the collection. The default is 5.</returns>
        [DefaultValue(NativeMethods.SC_MAX_MARGIN + 1)]
        [Description("The maximum number of margins.")]
        public int Capacity
        {
            get
            {
                return Scintilla.DirectMessage(NativeMethods.SCI_GETMARGINS).ToInt32();
            }
            set
            {
                value = Helpers.ClampMin(value, 0);
                Scintilla.DirectMessage(NativeMethods.SCI_SETMARGINS, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets the number of margins in the <see cref="MarginCollection" />.
        /// </summary>
        /// <returns>The number of margins in the collection.</returns>
        /// <remarks>This property is kept for convenience. The return value will always be equal to <see cref="Capacity" />.</remarks>
        /// <seealso cref="Capacity" />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Count
        {
            get
            {
                return Capacity;
            }
        }

        /// <summary>
        /// Gets or sets the width in pixels of the left margin padding.
        /// </summary>
        /// <returns>The left margin padding measured in pixels. The default is 1.</returns>
        [DefaultValue(1)]
        [Description("The left margin padding in pixels.")]
        public int Left
        {
            get
            {
                return Scintilla.DirectMessage(NativeMethods.SCI_GETMARGINLEFT).ToInt32();
            }
            set
            {
                value = Helpers.ClampMin(value, 0);
                Scintilla.DirectMessage(NativeMethods.SCI_SETMARGINLEFT, IntPtr.Zero, new IntPtr(value));
            }
        }

        // TODO Why is this commented out?
        /*
        /// <summary>
        /// Gets or sets the margin options.
        /// </summary>
        /// <returns>
        /// A <see cref="Zeroit.Framework.CodeBox.MarginOptions" /> that represents the margin options.
        /// The default is <see cref="Zeroit.Framework.CodeBox.MarginOptions.None" />.
        /// </returns>
        [DefaultValue(MarginOptions.None)]
        [Description("Margin options flags.")]
        [TypeConverter(typeof(FlagsEnumTypeConverter.FlagsEnumConverter))]
        public MarginOptions Options
        {
            get
            {
                return (MarginOptions)Scintilla.DirectMessage(NativeMethods.SCI_GETMARGINOPTIONS);
            }
            set
            {
                var options = (int)value;
                Scintilla.DirectMessage(NativeMethods.SCI_SETMARGINOPTIONS, new IntPtr(options));
            }
        }
        */

        /// <summary>
        /// Gets or sets the width in pixels of the right margin padding.
        /// </summary>
        /// <returns>The right margin padding measured in pixels. The default is 1.</returns>
        [DefaultValue(1)]
        [Description("The right margin padding in pixels.")]
        public int Right
        {
            get
            {
                return Scintilla.DirectMessage(NativeMethods.SCI_GETMARGINRIGHT).ToInt32();
            }
            set
            {
                value = Helpers.ClampMin(value, 0);
                Scintilla.DirectMessage(NativeMethods.SCI_SETMARGINRIGHT, IntPtr.Zero, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets a <see cref="Margin" /> object at the specified index.
        /// </summary>
        /// <param name="index">The margin index.</param>
        /// <returns>An object representing the margin at the specified <paramref name="index" />.</returns>
        /// <remarks>By convention margin 0 is used for line numbers and the two following for symbols.</remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Margin this[int index]
        {
            get
            {
                index = Helpers.Clamp(index, 0, Count - 1);
                return new Margin(Scintilla, index);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarginCollection" /> class.
        /// </summary>
        /// <param name="Scintilla">The <see cref="Scintilla" /> control that created this collection.</param>
        public MarginCollection(ZeroitCodeExplorer Scintilla)
        {
            this.Scintilla = Scintilla;
        }
    }
}
