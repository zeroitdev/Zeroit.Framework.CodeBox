// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="HotKeyManager.cs" company="Zeroit Dev">
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