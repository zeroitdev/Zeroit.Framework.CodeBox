// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="UpdateUIEventArgs.cs" company="Zeroit Dev">
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
    /// Provides data for the <see cref="Scintilla.UpdateUI" /> event.
    /// </summary>
    public class UpdateUIEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// The UI update that occurred.
        /// </summary>
        /// <returns>A bitwise combination of <see cref="UpdateChange" /> values specifying the UI update that occurred.</returns>
        public UpdateChange Change { get; private set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUIEventArgs" /> class.
        /// </summary>
        /// <param name="change">A bitwise combination of <see cref="UpdateChange" /> values specifying the reason to update the UI.</param>
        public UpdateUIEventArgs(UpdateChange change)
        {
            Change = change;
        }

        #endregion Constructors
    }
}
