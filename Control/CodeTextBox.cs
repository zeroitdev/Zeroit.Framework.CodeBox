// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="CodeTextBox.cs" company="Zeroit Dev">
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
using System.Windows.Forms;

namespace Zeroit.Framework.CodeBox
{
    /// <summary>
    /// Represents a Scintilla editor control.
    /// </summary>
    [Docking(DockingBehavior.Ask)]
    public partial class ZeroitCodeExplorer : Control
    {
        
        
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Scintilla" /> class.
        /// </summary>
        public ZeroitCodeExplorer()
        {
            // WM_DESTROY workaround
            if (reparentAll == null || (bool)reparentAll)
                reparent = true;

            // We don't want .NET to use GetWindowText because we manage ('cache') our own text
            base.SetStyle(ControlStyles.CacheText, true);

            SetStyle(ControlStyles.ResizeRedraw | 
                     ControlStyles.DoubleBuffer | 
                     //ControlStyles.AllPaintingInWmPaint|
                     //ControlStyles.UserPaint|
                     ControlStyles.SupportsTransparentBackColor, true);

            DoubleBuffered = true;

            // Necessary control styles (see TextBoxBase)
            base.SetStyle(ControlStyles.StandardClick |
                     ControlStyles.StandardDoubleClick |
                     ControlStyles.UseTextForAccessibility 
                     |ControlStyles.UserPaint
                     ,false);

            this.borderStyle = Border.FixedSingle;

            mainText = new MainText(this);
            
            Lines = new LineCollection(this);
            Styles = new StyleCollection(this);
            Indicators = new IndicatorCollection(this);
            Margins = new MarginCollection(this);
            Markers = new MarkerCollection(this);
            Selections = new SelectionCollection(this);

            this.SetSelectionBackColor(true, highLightColor);
            InitializeSyntaxColoringStyle();
            InitializeNumberMargin();
            InitBookmarkMargin();
            InitializeCodeFolding();

            AllowDrop = true;

            if (AllowDrop)
            {
                InitializeDragDropFile();
            }
            
        }

        
        #endregion Constructors
        
    }
}