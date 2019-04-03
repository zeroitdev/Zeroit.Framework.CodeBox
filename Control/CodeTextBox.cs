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