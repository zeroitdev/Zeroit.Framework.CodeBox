// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="Paint.cs" company="Zeroit Dev">
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
using System.Drawing;
using System.Windows.Forms;

namespace Zeroit.Framework.CodeBox
{
   public partial class ZeroitCodeExplorer : Control
    {
        
        #region Paint

        public enum Border
        {
            None,
            Adjust,
            Bump,
            Flat,
            FixedSingle,
            Fixed3D,
            Etched,
            Raised,
            RaisedInner,
            RaisedOuter,
            Sunken,
            SunkenInner,
            SunkenOuter,
            Custom
            
        }

        private Border3DSide borderSides = Border3DSide.All;

        public Border3DSide BorderSides
        {
            get { return borderSides; }
            set
            {
                borderSides = value;
                Invalidate();
            }
        }

        private Color borderColor = Color.Gray;

        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        private float borderSize = 1;

        public float BorderSize
        {
            get { return borderSize; }
            set
            {
                borderSize = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(Parent.BackColor);
            switch (BorderStyle)
            {
                case Border.None:
                    break;
                case Border.Adjust:
                    ControlPaint.DrawBorder3D(g, new Rectangle((int)BorderSize, (int)BorderSize, Width - (2 * (int)BorderSize), Height - (2 * (int)BorderSize)), Border3DStyle.Adjust, BorderSides);
                    break;
                case Border.Bump:
                    ControlPaint.DrawBorder3D(g, new Rectangle((int)BorderSize, (int)BorderSize, Width - (2 * (int)BorderSize), Height - (2 * (int)BorderSize)), Border3DStyle.Bump, BorderSides);
                    break;
                case Border.Etched:
                    ControlPaint.DrawBorder3D(g, new Rectangle((int)BorderSize, (int)BorderSize, Width - (2 * (int)BorderSize), Height - (2 * (int)BorderSize)), Border3DStyle.Etched, BorderSides);
                    break;
                case Border.Flat:
                    ControlPaint.DrawBorder3D(g, new Rectangle((int)BorderSize, (int)BorderSize, Width - (2 * (int)BorderSize), Height - (2 * (int)BorderSize)), Border3DStyle.Flat, BorderSides);
                    break;
                case Border.Fixed3D:
                    break;
                case Border.FixedSingle:
                    break;
                case Border.Raised:
                    ControlPaint.DrawBorder3D(g, ClientRectangle, Border3DStyle.Raised, BorderSides);
                    break;
                case Border.RaisedInner:
                    ControlPaint.DrawBorder3D(g, ClientRectangle, Border3DStyle.RaisedInner, BorderSides);
                    break;
                case Border.RaisedOuter:
                    ControlPaint.DrawBorder3D(g, ClientRectangle, Border3DStyle.RaisedOuter, BorderSides);
                    break;
                case Border.Sunken:
                    ControlPaint.DrawBorder3D(g, ClientRectangle, Border3DStyle.Sunken, BorderSides);
                    break;
                case Border.SunkenInner:
                    ControlPaint.DrawBorder3D(g, ClientRectangle, Border3DStyle.SunkenInner, BorderSides);
                    break;
                case Border.SunkenOuter:
                    ControlPaint.DrawBorder3D(g, ClientRectangle, Border3DStyle.SunkenOuter, BorderSides);
                    break;
                case Border.Custom:
                    g.DrawRectangle(new Pen(BorderColor, BorderSize), BorderSize, BorderSize, Width - (2 * BorderSize),
                        Height - (2 * BorderSize));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            base.OnPaint(e);
        }

        #endregion

        

    }
}