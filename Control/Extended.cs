using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Zeroit.Framework.CodeBox
{
    public partial class ZeroitCodeExplorer
    {
        private List<HotKeyManager> hotKeyManager = new List<HotKeyManager>()
        {

        };

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public List<HotKeyManager> HotKeys
        {
            get
            {
                return hotKeyManager;

            }
            set
            {
                hotKeyManager = value;
                Invalidate();
                
            }
        }

        private MainText mainText;


        [TypeConverter(typeof(ExpandableObjectConverter))]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public MainText TextBox
        {
            get { return mainText; }
            set
            {
                mainText = value;
                Invalidate();
            }
        }
        
    }



}