// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="FontQuality.cs" company="Zeroit Dev">
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
    /// The font quality (antialiasing method) used to render text.
    /// </summary>
    public enum FontQuality
    {
        /// <summary>
        /// Specifies that the character quality of the font does not matter; so the lowest quality can be used.
        /// This is the default.
        /// </summary>
        Default = NativeMethods.SC_EFF_QUALITY_DEFAULT,

        /// <summary>
        /// Specifies that anti-aliasing should not be used when rendering text.
        /// </summary>
        NonAntiAliased = NativeMethods.SC_EFF_QUALITY_NON_ANTIALIASED,

        /// <summary>
        /// Specifies that anti-aliasing should be used when rendering text, if the font supports it.
        /// </summary>
        AntiAliased = NativeMethods.SC_EFF_QUALITY_ANTIALIASED,

        /// <summary>
        /// Specifies that ClearType anti-aliasing should be used when rendering text, if the font supports it.
        /// </summary>
        LcdOptimized = NativeMethods.SC_EFF_QUALITY_LCD_OPTIMIZED
    }
}
