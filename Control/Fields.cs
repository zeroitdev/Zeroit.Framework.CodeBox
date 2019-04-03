// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="Fields.cs" company="Zeroit Dev">
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
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Zeroit.Framework.CodeBox
{
    public partial class ZeroitCodeExplorer : Control
    {
        #region Private Fields
        private ZeroitCodeExplorer TextArea;

        /// <summary>
        /// change this to whatever margin you want the line numbers to show in
        /// </summary>
        private const int NUMBER_MARGIN = 1;

        /// <summary>
        /// the background color of the text area
        /// </summary>
        private const int BACK_COLOR = 0x2A211C;

        /// <summary>
        /// default text color of the text area
        /// </summary>
        private const int FORE_COLOR = 0xB7B7B7;

        /// <summary>
        /// change this to whatever margin you want the code folding tree (+/-) to show in
        /// </summary>
        private const int FOLDING_MARGIN = 3;
        /// <summary>
        /// set this true to show circular buttons for code folding (the [+] and [-] buttons on the margin)
        /// </summary>
        private const bool CODEFOLDING_CIRCULAR = true;

        private Font font = new Font("Consolas", 10);

        private Color backColor = Color.FromArgb(255, (byte)(0x212121 >> 16),
            (byte)(unchecked((byte)0x212121) >> 8), unchecked((byte)0x212121));

        private Color foreColor = Color.FromArgb(255, (byte)(0xFFFFFF >> 16),
            (byte)(unchecked((byte)0xFFFFFF) >> 8), unchecked((byte)0xFFFFFF));

        private Color highLightColor = Color.FromArgb(255, (byte)(0x114D9C >> 16),
            (byte)(unchecked((byte)0x114D9C) >> 8), unchecked((byte)0x114D9C));

        private Color languageIdentifier = Color.FromArgb(255, (byte)(0xD0DAE2 >> 16),
            (byte)(unchecked((byte)0xD0DAE2) >> 8), unchecked((byte)0xD0DAE2));

        private Color languageComment = Color.FromArgb(255, (byte)(0xBD758B >> 16),
            (byte)(unchecked((byte)0xBD758B) >> 8), unchecked((byte)0xBD758B));

        private Color languageCommentLine = Color.FromArgb(255, (byte)(0x40BF57 >> 16),
            (byte)(unchecked((byte)0x40BF57) >> 8), unchecked((byte)0x40BF57));

        private Color languageCommentDoc = Color.FromArgb(255, (byte)(0x2FAE35 >> 16),
            (byte)(unchecked((byte)0x2FAE35) >> 8), unchecked((byte)0x2FAE35));

        private Color languageNumber = Color.FromArgb(255, (byte)(0xFFFF00 >> 16),
            (byte)(unchecked((byte)0xFFFF00) >> 8), unchecked((byte)0xFFFF00));

        private Color languageString = Color.FromArgb(255, (byte)(0xFFFF00 >> 16),
            (byte)(unchecked((byte)0xFFFF00) >> 8), unchecked((byte)0xFFFF00));

        private Color languageCharacter = Color.FromArgb(255, (byte)(0xE95454 >> 16),
            (byte)(unchecked((byte)0xE95454) >> 8), unchecked((byte)0xE95454));

        private Color languagePreprocessor = Color.FromArgb(255, (byte)(0x8AAFEE >> 16),
            (byte)(unchecked((byte)0x8AAFEE) >> 8), unchecked((byte)0x8AAFEE));

        private Color languageOperator = Color.FromArgb(255, (byte)(0xE0E0E0 >> 16),
            (byte)(unchecked((byte)0xE0E0E0) >> 8), unchecked((byte)0xE0E0E0));

        private Color languageRegex = Color.FromArgb(255, (byte)(0xff00ff >> 16),
            (byte)(unchecked((byte)0xff00ff) >> 8), unchecked((byte)0xff00ff));

        private Color languageCommentLineDoc = Color.FromArgb(255, (byte)(0x77A7DB >> 16),
            (byte)(unchecked((byte)0x77A7DB) >> 8), unchecked((byte)0x77A7DB));

        private Color languageWord = Color.FromArgb(255, (byte)(0x48A8EE >> 16),
            (byte)(unchecked((byte)0x48A8EE) >> 8), unchecked((byte)0x48A8EE));

        private Color languageWord2 = Color.FromArgb(255, (byte)(0xF98906 >> 16),
            (byte)(unchecked((byte)0xF98906) >> 8), unchecked((byte)0xF98906));

        private Color languageCommentDocKeyword = Color.FromArgb(255, (byte)(0xB3D991 >> 16),
            (byte)(unchecked((byte)0xB3D991) >> 8), unchecked((byte)0xB3D991));

        private Color languageCommentDocKeywordError = Color.FromArgb(255, (byte)(0xFF0000 >> 16),
            (byte)(unchecked((byte)0xFF0000) >> 8), unchecked((byte)0xFF0000));

        private Color languageGlobalClass = Color.FromArgb(255, (byte)(0x48A8EE >> 16),
            (byte)(unchecked((byte)0x48A8EE) >> 8), unchecked((byte)0x48A8EE));


        private List<string> keywords = new List<string>()
        {
            "class extends implements import interface new case do while else if for in switch throw get set function var try catch finally while with default break continue delete return each const namespace package include use is as instanceof typeof author copy default deprecated eventType example exampleText exception haxe inheritDoc internal link mtasc mxmlc param private return see serial serialData serialField since throws usage version langversion playerversion productversion dynamic private public partial static intrinsic internal native override protected AS3 final super this arguments null Infinity NaN undefined true false abstract as base bool break by byte case catch char checked class const continue decimal default delegate do double descending explicit event extern else enum false finally fixed float for foreach from goto group if implicit in int interface internal into is lock long new null namespace object operator out override orderby params private protected public readonly ref return switch struct sbyte sealed short sizeof stackalloc static string select this throw true try typeof uint ulong unchecked unsafe ushort using var virtual volatile void while where yield",
            "void Null ArgumentError arguments Array Boolean Class Date DefinitionError Error EvalError Function int Math Namespace Number Object RangeError ReferenceError RegExp SecurityError String SyntaxError TypeError uint XML XMLList Boolean Byte Char DateTime Decimal Double Int16 Int32 Int64 IntPtr SByte Single UInt16 UInt32 UInt64 UIntPtr Void Path File System Windows Forms Zeroit.Framework.CodeExplorer"
        };

        // WM_DESTROY workaround
        private static bool? reparentAll;
        private bool reparent;

        // Static module data
        private static string modulePath;
        private static IntPtr moduleHandle;
        private static NativeMethods.Scintilla_DirectFunction directFunction;

        // Events
        private static readonly object scNotificationEventKey = new object();
        private static readonly object insertCheckEventKey = new object();
        private static readonly object beforeInsertEventKey = new object();
        private static readonly object beforeDeleteEventKey = new object();
        private static readonly object insertEventKey = new object();
        private static readonly object deleteEventKey = new object();
        private static readonly object updateUIEventKey = new object();
        private static readonly object modifyAttemptEventKey = new object();
        private static readonly object styleNeededEventKey = new object();
        private static readonly object savePointReachedEventKey = new object();
        private static readonly object savePointLeftEventKey = new object();
        private static readonly object changeAnnotationEventKey = new object();
        private static readonly object marginClickEventKey = new object();
        private static readonly object marginRightClickEventKey = new object();
        private static readonly object charAddedEventKey = new object();
        private static readonly object autoCSelectionEventKey = new object();
        private static readonly object autoCCompletedEventKey = new object();
        private static readonly object autoCCancelledEventKey = new object();
        private static readonly object autoCCharDeletedEventKey = new object();
        private static readonly object dwellStartEventKey = new object();
        private static readonly object dwellEndEventKey = new object();
        private static readonly object borderStyleChangedEventKey = new object();
        private static readonly object doubleClickEventKey = new object();
        private static readonly object paintedEventKey = new object();
        private static readonly object needShownEventKey = new object();
        private static readonly object hotspotClickEventKey = new object();
        private static readonly object hotspotDoubleClickEventKey = new object();
        private static readonly object hotspotReleaseClickEventKey = new object();
        private static readonly object indicatorClickEventKey = new object();
        private static readonly object indicatorReleaseEventKey = new object();
        private static readonly object zoomChangedEventKey = new object();

        // The goods
        private IntPtr sciPtr;
        private Border borderStyle;

        // Set style
        private int stylingPosition;
        private int stylingBytePosition;

        // Modified event optimization
        private int? cachedPosition = null;
        private string cachedText = null;

        // Double-click
        private bool doubleClick;

        // Pinned data
        private IntPtr fillUpChars;

        // For highlight calculations
        private string lastCallTip = string.Empty;

        /// <summary>
        /// A constant used to specify an infinite mouse dwell wait time.
        /// </summary>
        public const int TimeForever = NativeMethods.SC_TIME_FOREVER;

        /// <summary>
        /// A constant used to specify an invalid document position.
        /// </summary>
        public const int InvalidPosition = NativeMethods.INVALID_POSITION;


        #endregion
        
    }
}