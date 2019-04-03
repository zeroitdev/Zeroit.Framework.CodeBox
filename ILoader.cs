// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="ILoader.cs" company="Zeroit Dev">
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

namespace Zeroit.Framework.CodeBox
{
    /// <summary>
    /// Provides methods for loading and creating a <see cref="Document" /> on a background (non-UI) thread.
    /// </summary>
    /// <remarks>
    /// Internally an <see cref="ILoader" /> maintains a <see cref="Document" /> instance with a reference count of 1.
    /// You are responsible for ensuring the reference count eventually reaches 0 or memory leaks will occur.
    /// </remarks>
    public interface ILoader
    {
        /// <summary>
        /// Adds the data specified to the internal document.
        /// </summary>
        /// <param name="data">The character buffer to copy to the new document.</param>
        /// <param name="length">The number of characters in <paramref name="data" /> to copy.</param>
        /// <returns>
        /// true if the data was added successfully; otherwise, false.
        /// A return value of false should be followed by a call to <see cref="Release" />.
        /// </returns>
        bool AddData(char[] data, int length);

        /// <summary>
        /// Returns the internal document.
        /// </summary>
        /// <returns>A <see cref="Document" /> containing the added text. The document has a reference count of 1.</returns>
        Document ConvertToDocument();

        /// <summary>
        /// Called to release the internal document when an error occurs using <see cref="AddData" /> or to abandon loading.
        /// </summary>
        /// <returns>
        /// The internal document reference count.
        /// A return value of 0 indicates that the document has been destroyed and all associated memory released.
        /// </returns>
        int Release();
    }
}
