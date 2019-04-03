using System;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace Zeroit.Framework.CodeBox
{
    
    [Serializable]
    public class HotKeyManager : ISerializable
    {

        
        private Form form;
        private Keys key;
        private bool ctrl, shift, alt = false;
        

        
        public Form Form
        {
            get { return form; }
            set
            {
                form = value;
            }
        }

        public Keys Key
        {
            get { return key; }
            set
            {
                key = value;
            }
        }

        public bool Ctrl
        {
            get { return ctrl; }
            set
            {
                ctrl = value;
            }
        }

        public bool Shift
        {
            get { return shift; }
            set
            {
                shift = value; 
            }

        }

        public bool Alt
        {
            get { return alt; }
            set { alt = value; }
        }

        public HotKeyManager()
        {
            if (form == null)
                return;
            
        }

        public void AddHotKey(Action function)
        {
            form.KeyPreview = true;

            form.KeyDown += delegate(object sender, KeyEventArgs e)
            {
                if (IsHotkey(e, Key, e.Control, e.Shift, e.Alt))
                {
                    function();
                }
            };
        }

        public static bool IsHotkey(KeyEventArgs eventData, Keys key, bool ctrl = false, bool shift = false,
            bool alt = false)
        {
            return eventData.KeyCode == key && eventData.Control == ctrl && eventData.Shift == shift &&
                   eventData.Alt == alt;
        }


        public HotKeyManager(SerializationInfo info, StreamingContext context)
        {
            form = (Form) info.GetValue("form", typeof(Form));
            key = (Keys)info.GetValue("key", typeof(Keys));
            form = (Form)info.GetValue("ctrl", typeof(bool));
            form = (Form)info.GetValue("alt", typeof(bool));
            form = (Form)info.GetValue("shift", typeof(bool));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("form", form);
            info.AddValue("key", key);
            info.AddValue("ctrl", ctrl);
            info.AddValue("alt", alt);
            info.AddValue("shift", shift);
        }
    }
}