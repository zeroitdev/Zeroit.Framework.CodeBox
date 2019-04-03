// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="MarkerHandle.cs" company="Zeroit Dev">
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
    /// A <see cref="Marker" /> handle.
    /// </summary>
    /// <remarks>
    /// This is an opaque type, meaning it can be used by a <see cref="Scintilla" /> control but
    /// otherwise has no public members of its own.
    /// </remarks>
    public struct MarkerHandle
    {
        internal IntPtr Value;

        /// <summary>
        /// A read-only field that represents an uninitialized handle.
        /// </summary>
        public static readonly MarkerHandle Zero;

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance or null.</param>
        /// <returns>true if <paramref name="obj" /> is an instance of <see cref="MarkerHandle" /> and equals the value of this instance; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return (obj is IntPtr) && Value == ((MarkerHandle)obj).Value;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="MarkerHandle" /> are equal.
        /// </summary>
        /// <param name="a">The first handle to compare.</param>
        /// <param name="b">The second handle to compare.</param>
        /// <returns>true if <paramref name="a" /> equals <paramref name="b" />; otherwise, false.</returns>
        public static bool operator ==(MarkerHandle a, MarkerHandle b)
        {
            return a.Value == b.Value;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="MarkerHandle" /> are not equal.
        /// </summary>
        /// <param name="a">The first handle to compare.</param>
        /// <param name="b">The second handle to compare.</param>
        /// <returns>true if <paramref name="a" /> does not equal <paramref name="b" />; otherwise, false.</returns>
        public static bool operator !=(MarkerHandle a, MarkerHandle b)
        {
            return a.Value != b.Value;
        }
    }
}
