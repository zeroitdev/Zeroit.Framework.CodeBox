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