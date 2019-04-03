// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="MainText.cs" company="Zeroit Dev">
//     Copyright © 2017 Zeroit Dev Technologies. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Serialization;

namespace Zeroit.Framework.CodeBox
{
    [Serializable]
    public partial class MainText : ISerializable
    {
        #region Constants

        /// <summary>
        /// change this to whatever margin you want the bookmarks/breakpoints to show in
        /// </summary>
        private const int BOOKMARK_MARGIN = 2;
        private const int BOOKMARK_MARKER = 2;

        /// <summary>
        /// change this to whatever margin you want the code folding tree (+/-) to show in
        /// </summary>
        private const int FOLDING_MARGIN = 3;


        #endregion

        #region Private Fields
        private ZeroitCodeExplorer TextArea;

        private Font font = new Font("Consolas", 10);

        private Color backColor = Color.FromArgb(255, (byte)(0x212121 >> 16),
            (byte)(unchecked((byte)0x212121) >> 8), unchecked((byte)0x212121));

        private Color foreColor = Color.FromArgb(255, (byte)(0xFFFFFF >> 16),
            (byte)(unchecked((byte)0xFFFFFF) >> 8), unchecked((byte)0xFFFFFF));

            
        //private Color highLightColor = Color.FromArgb(255, (byte)(0x114D9C >> 16),
        //    (byte)(unchecked((byte)0x114D9C) >> 8), unchecked((byte)0x114D9C));

        private Color highLightColor = Color.FromArgb(68, 140, 203);

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

        //private Color languageWord2 = Color.FromArgb(255, (byte)(0xF98906 >> 16),
        //    (byte)(unchecked((byte)0xF98906) >> 8), unchecked((byte)0xF98906));

        private Color languageWord2 = Color.CadetBlue;


        private Color languageCommentDocKeyword = Color.FromArgb(255, (byte)(0xB3D991 >> 16),
            (byte)(unchecked((byte)0xB3D991) >> 8), unchecked((byte)0xB3D991));

        private Color languageCommentDocKeywordError = Color.FromArgb(255, (byte)(0xFF0000 >> 16),
            (byte)(unchecked((byte)0xFF0000) >> 8), unchecked((byte)0xFF0000));

            
        //private Color languageGlobalClass = Color.FromArgb(255, (byte)(0x48A8EE >> 16),
        //    (byte)(unchecked((byte)0x48A8EE) >> 8), unchecked((byte)0x48A8EE));

        private Color languageGlobalClass = Color.FromArgb(0, 122, 204);


        private Color marginNumberLineBack = Color.FromArgb(255, (byte)(0x2A211C >> 16),
            (byte)(unchecked((byte)0x2A211C) >> 8), unchecked((byte)0x2A211C));

        private Color marginNumberLineFore = Color.FromArgb(255, (byte)(0xB7B7B7 >> 16),
            (byte)(unchecked((byte)0xB7B7B7) >> 8), unchecked((byte)0xB7B7B7));

        private Color marginNumberIndentBack = Color.FromArgb(255, (byte)(0x2A211C >> 16),
            (byte)(unchecked((byte)0x2A211C) >> 8), unchecked((byte)0x2A211C));

        private Color marginNumberIndentFore= Color.FromArgb(255, (byte)(0xB7B7B7 >> 16),
            (byte)(unchecked((byte)0xB7B7B7) >> 8), unchecked((byte)0xB7B7B7));

        private Color foldMargin = Color.FromArgb(255, (byte)(0x2A211C >> 16),
            (byte)(unchecked((byte)0x2A211C) >> 8), unchecked((byte)0x2A211C));

        private Color foldMarginHighlight = Color.FromArgb(255, (byte)(0x2A211C >> 16),
            (byte)(unchecked((byte)0x2A211C) >> 8), unchecked((byte)0x2A211C));

        private MarginType foldMarginType = MarginType.Symbol;

        private bool foldMarginSensitive = true;

        private int foldingMarginWidth = 20;

        private Color foldMarginBack = Color.FromArgb(255, (byte)(0x2A211C >> 16),
            (byte)(unchecked((byte)0x2A211C) >> 8), unchecked((byte)0x2A211C));

        private Color foldMarginFore = Color.FromArgb(255, (byte)(0xB7B7B7 >> 16),
            (byte)(unchecked((byte)0xB7B7B7) >> 8), unchecked((byte)0xB7B7B7));

        /// <summary>
        /// change this to whatever margin you want the line numbers to show in
        /// </summary>
        private const int NUMBER_MARGIN = 1;


        private int marginNumberWidth = 30;

        private MarginType marginNumberType = MarginType.Number;

        private bool marginNumberSensitive = true;

        private uint marginNumberMask = 0;

        private List<string> keywords = new List<string>()
        {
            "class extends implements import interface new case do while else if for in switch throw get set function var try catch finally while with default break continue delete return each const namespace package include use is as instanceof typeof author copy default deprecated eventType example exampleText exception haxe inheritDoc internal link mtasc mxmlc param private return see serial serialData serialField since throws usage version langversion playerversion productversion dynamic private public partial static intrinsic internal native override protected AS3 final super this arguments null Infinity NaN undefined true false abstract as base bool break by byte case catch char checked class const continue decimal default delegate do double descending explicit event extern else enum false finally fixed float for foreach from goto group if implicit in int interface internal into is lock long new null namespace object operator out override orderby params private protected public readonly ref return switch struct sbyte sealed short sizeof stackalloc static string select this throw true try typeof uint ulong unchecked unsafe ushort using var virtual volatile void while where yield",
            "void Null ArgumentError arguments Array Boolean Class Date DefinitionError Error EvalError Function int Math Namespace Number Object RangeError ReferenceError RegExp SecurityError String SyntaxError TypeError uint XML XMLList Boolean Byte Char DateTime Decimal Double Int16 Int32 Int64 IntPtr SByte Single UInt16 UInt32 UInt64 UIntPtr Void Path File System Windows Forms Zeroit.Framework.CodeExplorer"
        };
        
        
        private MarkerSymbol foldFolder = MarkerSymbol.CirclePlus;

        private MarkerSymbol foldFolderOpen = MarkerSymbol.CircleMinus;

        private MarkerSymbol foldFolderEnd = MarkerSymbol.CirclePlusConnected;

        private MarkerSymbol foldFolderMidTail = MarkerSymbol.TCorner;

        private MarkerSymbol foldFolderOpenMid = MarkerSymbol.CircleMinusConnected;

        private MarkerSymbol foldFolderSub = MarkerSymbol.VLine;

        private MarkerSymbol foldFolderTail = MarkerSymbol.LCorner;

        private AutomaticFold folderautomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);


        private int bookmarginWidth = 20;

        private MarginType bookmarginType = MarginType.Symbol;

        private bool bookmarginSensitive = true;

        private MarkerSymbol bookmarkerSymbol = MarkerSymbol.Circle;

        private Color bookmarkerBack = Color.FromArgb(255, (byte)(0xFF003B >> 16),
            (byte)(unchecked((byte)0xFF003B) >> 8), unchecked((byte)0xFF003B));

        private Color bookmarkerFore = Color.FromArgb(255, (byte)(0x000000 >> 16),
            (byte)(unchecked((byte)0x000000) >> 8), unchecked((byte)0x000000));

        private float bookmarkerAlpha = 0.5f;


        #endregion

        #region Constructor
        public MainText(ZeroitCodeExplorer TextArea)
        {

            this.TextArea = TextArea;
            
        } 
        #endregion

        #region Methods


        #region Numbers, Bookmarks, Code Folding





        #endregion

        #region Utilities

        public static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        #endregion

        #endregion

        #region Properties

        public List<string> Keywords
        {
            get { return keywords; }
            set
            {
                keywords = value;

                foreach (string text in value)
                {
                    for (int i = 0; i < value.Count - 1; i++)
                    {
                        TextArea.SetKeywords(i, text[i].ToString());
                    }
                }

            }
        }

        public ZeroitCodeExplorer CodeExplorer
        {
            get { return TextArea; }
            set
            {
                TextArea = value;

            }
        }

        private Lexer language = Lexer.Cpp;

        [DefaultValue(Lexer.Cpp)]
        public Lexer Language
        {
            get { return language;}
            set { language = value; }
        }
        
        public Font Font
        {
            get { return font; }
            set
            {
                font = value;
                TextArea.Styles[Style.Default].Font = value.Name;
                TextArea.Styles[Style.Default].Size = (int)value.Size;
            }
        }

        public Color BackColor
        {
            get { return backColor; }
            set
            {
                backColor = value;
                TextArea.Styles[Style.Default].BackColor = value;

                #region Syntax Coloring
                TextArea.StyleResetDefault();
                TextArea.Styles[Style.Default].Font = font.Name;
                TextArea.Styles[Style.Default].Size = (int)font.Size;
                TextArea.Styles[Style.Default].BackColor = BackColor;
                TextArea.Styles[Style.Default].ForeColor = foreColor;
                TextArea.StyleClearAll();

                // Configure the CPP (C#) lexer styles
                TextArea.Styles[Style.Cpp.Identifier].ForeColor = languageIdentifier;
                TextArea.Styles[Style.Cpp.Comment].ForeColor = languageComment;
                TextArea.Styles[Style.Cpp.CommentLine].ForeColor = languageCommentLine;
                TextArea.Styles[Style.Cpp.CommentDoc].ForeColor = languageCommentDoc;
                TextArea.Styles[Style.Cpp.Number].ForeColor = languageNumber;
                TextArea.Styles[Style.Cpp.String].ForeColor = languageString;
                TextArea.Styles[Style.Cpp.Character].ForeColor = languageCharacter;
                TextArea.Styles[Style.Cpp.Preprocessor].ForeColor = languagePreprocessor;
                TextArea.Styles[Style.Cpp.Operator].ForeColor = languageOperator;
                TextArea.Styles[Style.Cpp.Regex].ForeColor = languageRegex;
                TextArea.Styles[Style.Cpp.CommentLineDoc].ForeColor = languageCommentLineDoc;
                TextArea.Styles[Style.Cpp.Word].ForeColor = languageWord;
                TextArea.Styles[Style.Cpp.Word2].ForeColor = languageWord2;
                TextArea.Styles[Style.Cpp.CommentDocKeyword].ForeColor = languageCommentDocKeyword;
                TextArea.Styles[Style.Cpp.CommentDocKeywordError].ForeColor = languageCommentDocKeywordError;
                TextArea.Styles[Style.Cpp.GlobalClass].ForeColor = languageGlobalClass;

                TextArea.Lexer = Language;

                foreach (string keywords in Keywords)
                {
                    for (int i = 0; i < Keywords.Count - 1; i++)
                    {
                        TextArea.SetKeywords(i, keywords[i].ToString());
                    }
                }

                #endregion

                #region Number Margin

                TextArea.Styles[Style.LineNumber].BackColor = marginNumberLineBack;
                TextArea.Styles[Style.LineNumber].ForeColor = marginNumberLineFore;
                TextArea.Styles[Style.IndentGuide].ForeColor = MarginNumberIndentFore;
                TextArea.Styles[Style.IndentGuide].BackColor = MarginNumberIndentBack;

                var nums = TextArea.Margins[NUMBER_MARGIN];
                nums.Width = MarginNumberWidth;
                nums.Type = MarginNumberType;
                nums.Sensitive = MarginNumberSensitive;
                nums.Mask = MarginNumberMask;

                #endregion

                #region Book Margin

                var margin = TextArea.Margins[BOOKMARK_MARGIN];
                margin.Width = BookmarginWidth;
                margin.Sensitive = bookmarginSensitive;
                margin.Type = BookmarginType;
                margin.Mask = (1 << BOOKMARK_MARKER);
                //margin.Cursor = MarginCursor.Arrow;

                var marker = TextArea.Markers[BOOKMARK_MARKER];
                marker.Symbol = BookmarkerSymbol;
                marker.SetBackColor(BookmarkerBack);
                marker.SetForeColor(BookmarkerFore);
                marker.SetAlpha(Convert.ToInt32(BookmarkerAlpha * 255));

                #endregion

                #region Folding

                TextArea.SetFoldMarginColor(true, FoldMargin);
                TextArea.SetFoldMarginHighlightColor(true, FoldMarginHighlight);

                // Enable code folding

                // Configure a margin to display folding symbols
                TextArea.Margins[FOLDING_MARGIN].Type = FoldMarginType;
                TextArea.Margins[FOLDING_MARGIN].Mask = Marker.MaskFolders;
                TextArea.Margins[FOLDING_MARGIN].Sensitive = FoldMarginSensitive;
                TextArea.Margins[FOLDING_MARGIN].Width = FoldingMarginWidth;

                // Set colors for all folding markers
                for (int i = 25; i <= 31; i++)
                {
                    TextArea.Markers[i].SetForeColor(FoldMarginFore); // styles for [+] and [-]
                    TextArea.Markers[i].SetBackColor(FoldMarginBack); // styles for [+] and [-]
                }

                // Configure folding markers with respective symbols
                TextArea.Markers[Marker.Folder].Symbol = FoldFolder;
                TextArea.Markers[Marker.FolderOpen].Symbol = FoldFolderOpen;
                TextArea.Markers[Marker.FolderEnd].Symbol = FoldFolderEnd;
                TextArea.Markers[Marker.FolderMidTail].Symbol = FoldFolderMidTail;
                TextArea.Markers[Marker.FolderOpenMid].Symbol = FoldFolderOpenMid;
                TextArea.Markers[Marker.FolderSub].Symbol = FoldFolderSub;
                TextArea.Markers[Marker.FolderTail].Symbol = FoldFolderTail;

                // Enable automatic folding
                TextArea.AutomaticFold = FolderAutomaticFold;


                #endregion
            }
        }

        public Color ForeColor
        {
            get { return foreColor; }
            set
            {
                foreColor = value;
                TextArea.Styles[Style.Default].ForeColor = value;
            }
        }

        public Color HighlightColor
        {
            get { return highLightColor; }
            set
            {
                highLightColor = value;
                TextArea.SetSelectionBackColor(true, value);
            }
        }

        public Color LanguageIdentifier
        {
            get { return languageIdentifier; }
            set
            {
                languageIdentifier = value;

                switch (Language)
                {
                    case Lexer.Container:
                        break;
                    case Lexer.Null:
                        break;
                    case Lexer.Ada:
                        TextArea.Styles[Style.Ada.Identifier].ForeColor = value;
                        break;
                    case Lexer.Asm:
                        TextArea.Styles[Style.Asm.Identifier].ForeColor = value;
                        break;
                    case Lexer.Batch:
                        TextArea.Styles[Style.Batch.Identifier].ForeColor = value;
                        break;
                    case Lexer.Cpp:
                        TextArea.Styles[Style.Cpp.Identifier].ForeColor = value;
                        break;
                    case Lexer.Css:
                        TextArea.Styles[Style.Css.Identifier].ForeColor = value;
                        break;
                    case Lexer.Fortran:
                        TextArea.Styles[Style.Fortran.Identifier].ForeColor = value;
                        break;
                    case Lexer.FreeBasic:
                        TextArea.Styles[Style.FreeBasic.Identifier].ForeColor = value;
                        break;
                    case Lexer.Html:
                        break;
                    case Lexer.Json:
                        break;
                    case Lexer.Lisp:
                        TextArea.Styles[Style.Lisp.Identifier].ForeColor = value;
                        break;
                    case Lexer.Lua:
                        TextArea.Styles[Style.Lua.Identifier].ForeColor = value;
                        break;
                    case Lexer.Matlab:
                        TextArea.Styles[Style.Matlab.Identifier].ForeColor = value;
                        break;
                    case Lexer.Pascal:
                        TextArea.Styles[Style.Pascal.Identifier].ForeColor = value;
                        break;
                    case Lexer.Perl:
                        TextArea.Styles[Style.Perl.Identifier].ForeColor = value;
                        break;
                    case Lexer.PhpScript:
                        break;
                    case Lexer.PowerShell:
                        TextArea.Styles[Style.PowerShell.Identifier].ForeColor = value;
                        break;
                    case Lexer.Properties:
                        break;
                    case Lexer.PureBasic:
                        TextArea.Styles[Style.PureBasic.Identifier].ForeColor = value;
                        break;
                    case Lexer.Python:
                        TextArea.Styles[Style.Python.Identifier].ForeColor = value;
                        break;
                    case Lexer.Ruby:
                        TextArea.Styles[Style.Ruby.Identifier].ForeColor = value;
                        break;
                    case Lexer.Smalltalk:
                        break;
                    case Lexer.Sql:
                        TextArea.Styles[Style.Sql.Identifier].ForeColor = value;
                        break;
                    case Lexer.Vb:
                        TextArea.Styles[Style.Vb.Identifier].ForeColor = value;
                        break;
                    case Lexer.VbScript:
                        TextArea.Styles[Style.VbScript.Identifier].ForeColor = value;
                        break;
                    case Lexer.Verilog:
                        TextArea.Styles[Style.Verilog.Identifier].ForeColor = value;
                        break;
                    case Lexer.Xml:
                        break;
                    case Lexer.BlitzBasic:
                        TextArea.Styles[Style.BlitzBasic.Identifier].ForeColor = value;
                        break;
                    case Lexer.Markdown:
                        break;
                    case Lexer.R:
                        TextArea.Styles[Style.R.Identifier].ForeColor = value;
                        break;
                }
            }
        }

        public Color LanguageComment
        {
            get { return languageComment; }
            set
            {
                languageComment = value;
                switch (Language)
                {
                    case Lexer.Container:
                        break;
                    case Lexer.Null:
                        break;
                    case Lexer.Ada:
                        break;
                    case Lexer.Asm:
                        TextArea.Styles[Style.Asm.Comment].ForeColor = value;
                        break;
                    case Lexer.Batch:
                        TextArea.Styles[Style.Batch.Comment].ForeColor = value;
                        break;
                    case Lexer.Cpp:
                        TextArea.Styles[Style.Cpp.Comment].ForeColor = value;
                        break;
                    case Lexer.Css:
                        TextArea.Styles[Style.Css.Comment].ForeColor = value;
                        break;
                    case Lexer.Fortran:
                        TextArea.Styles[Style.Fortran.Comment].ForeColor = value;
                        break;
                    case Lexer.FreeBasic:
                        TextArea.Styles[Style.FreeBasic.Comment].ForeColor = value;
                        break;
                    case Lexer.Html:
                        TextArea.Styles[Style.Html.Comment].ForeColor = value;
                        break;
                    case Lexer.Json:
                        break;
                    case Lexer.Lisp:
                        TextArea.Styles[Style.Lisp.Comment].ForeColor = value;
                        break;
                    case Lexer.Lua:
                        TextArea.Styles[Style.Lua.Comment].ForeColor = value;
                        break;
                    case Lexer.Matlab:
                        TextArea.Styles[Style.Matlab.Comment].ForeColor = value;
                        break;
                    case Lexer.Pascal:
                        TextArea.Styles[Style.Pascal.Comment].ForeColor = value;
                        break;
                    case Lexer.Perl:
                        break;
                    case Lexer.PhpScript:
                        TextArea.Styles[Style.PhpScript.Comment].ForeColor = value;
                        break;
                    case Lexer.PowerShell:
                        TextArea.Styles[Style.PowerShell.Comment].ForeColor = value;
                        break;
                    case Lexer.Properties:
                        TextArea.Styles[Style.Properties.Comment].ForeColor = value;
                        break;
                    case Lexer.PureBasic:
                        TextArea.Styles[Style.PureBasic.Comment].ForeColor = value;
                        break;
                    case Lexer.Python:
                        break;
                    case Lexer.Ruby:
                        break;
                    case Lexer.Smalltalk:
                        TextArea.Styles[Style.Smalltalk.Comment].ForeColor = value;
                        break;
                    case Lexer.Sql:
                        TextArea.Styles[Style.Sql.Comment].ForeColor = value;
                        break;
                    case Lexer.Vb:
                        TextArea.Styles[Style.Vb.Comment].ForeColor = value;
                        break;
                    case Lexer.VbScript:
                        TextArea.Styles[Style.VbScript.Comment].ForeColor = value;
                        break;
                    case Lexer.Verilog:
                        TextArea.Styles[Style.Verilog.Comment].ForeColor = value;
                        break;
                    case Lexer.Xml:
                        TextArea.Styles[Style.Xml.Comment].ForeColor = value;
                        break;
                    case Lexer.BlitzBasic:
                        TextArea.Styles[Style.BlitzBasic.Comment].ForeColor = value;
                        break;
                    case Lexer.Markdown:
                        break;
                    case Lexer.R:
                        TextArea.Styles[Style.R.Comment].ForeColor = value;
                        break;
                }
            }
        }

        public Color LanguageCommentLine
        {
            get { return languageCommentLine; }
            set
            {
                languageCommentLine = value;
                switch (Language)
                {
                    case Lexer.Container:
                        break;
                    case Lexer.Null:
                        break;
                    case Lexer.Ada:
                        TextArea.Styles[Style.Ada.CommentLine].ForeColor = value;
                        break;
                    case Lexer.Asm:
                        break;
                    case Lexer.Batch:
                        break;
                    case Lexer.Cpp:
                        TextArea.Styles[Style.Cpp.CommentLine].ForeColor = value;
                        break;
                    case Lexer.Css:
                        break;
                    case Lexer.Fortran:
                        break;
                    case Lexer.FreeBasic:
                        break;
                    case Lexer.Html:
                        break;
                    case Lexer.Json:
                        break;
                    case Lexer.Lisp:
                        break;
                    case Lexer.Lua:
                        TextArea.Styles[Style.Lua.CommentLine].ForeColor = value;
                        break;
                    case Lexer.Matlab:
                        break;
                    case Lexer.Pascal:
                        TextArea.Styles[Style.Pascal.CommentLine].ForeColor = value;
                        break;
                    case Lexer.Perl:
                        TextArea.Styles[Style.Perl.CommentLine].ForeColor = value;
                        break;
                    case Lexer.PhpScript:
                        TextArea.Styles[Style.PhpScript.CommentLine].ForeColor = value;
                        break;
                    case Lexer.PowerShell:
                        break;
                    case Lexer.Properties:
                        break;
                    case Lexer.PureBasic:
                        break;
                    case Lexer.Python:
                        TextArea.Styles[Style.Python.CommentLine].ForeColor = value;
                        break;
                    case Lexer.Ruby:
                        TextArea.Styles[Style.Ruby.CommentLine].ForeColor = value;
                        break;
                    case Lexer.Smalltalk:
                        break;
                    case Lexer.Sql:
                        TextArea.Styles[Style.Sql.CommentLine].ForeColor = value;
                        break;
                    case Lexer.Vb:
                        break;
                    case Lexer.VbScript:
                        break;
                    case Lexer.Verilog:
                        TextArea.Styles[Style.Verilog.CommentLine].ForeColor = value;
                        break;
                    case Lexer.Xml:
                        break;
                    case Lexer.BlitzBasic:
                        break;
                    case Lexer.Markdown:
                        break;
                    case Lexer.R:
                        break;
                }
            }
        }

        public Color LanguageCommentDoc
        {
            get { return languageCommentDoc; }
            set
            {
                languageCommentDoc = value;
                switch (Language)
                {
                    case Lexer.Container:
                        break;
                    case Lexer.Null:
                        break;
                    case Lexer.Ada:
                        break;
                    case Lexer.Asm:
                        break;
                    case Lexer.Batch:
                        break;
                    case Lexer.Cpp:
                        TextArea.Styles[Style.Cpp.CommentDoc].ForeColor = value;
                        break;
                    case Lexer.Css:
                        break;
                    case Lexer.Fortran:
                        break;
                    case Lexer.FreeBasic:
                        break;
                    case Lexer.Html:
                        break;
                    case Lexer.Json:
                        break;
                    case Lexer.Lisp:
                        break;
                    case Lexer.Lua:
                        TextArea.Styles[Style.Lua.CommentDoc].ForeColor = value;
                        break;
                    case Lexer.Matlab:
                        break;
                    case Lexer.Pascal:
                        break;
                    case Lexer.Perl:
                        break;
                    case Lexer.PhpScript:
                        break;
                    case Lexer.PowerShell:
                        break;
                    case Lexer.Properties:
                        break;
                    case Lexer.PureBasic:
                        break;
                    case Lexer.Python:
                        break;
                    case Lexer.Ruby:
                        break;
                    case Lexer.Smalltalk:
                        break;
                    case Lexer.Sql:
                        TextArea.Styles[Style.Sql.CommentDoc].ForeColor = value;
                        break;
                    case Lexer.Vb:
                        break;
                    case Lexer.VbScript:
                        break;
                    case Lexer.Verilog:
                        break;
                    case Lexer.Xml:
                        break;
                    case Lexer.BlitzBasic:
                        break;
                    case Lexer.Markdown:
                        break;
                    case Lexer.R:
                        break;
                }
            }
        }

        public Color LanguageNumber
        {
            get { return languageNumber; }
            set
            {
                languageNumber = value;
                switch (Language)
                {
                    case Lexer.Container:
                        break;
                    case Lexer.Null:
                        break;
                    case Lexer.Ada:
                        TextArea.Styles[Style.Ada.Number].ForeColor = value;
                        break;
                    case Lexer.Asm:
                        TextArea.Styles[Style.Asm.Number].ForeColor = value;
                        break;
                    case Lexer.Batch:
                        break;
                    case Lexer.Cpp:
                        TextArea.Styles[Style.Cpp.Number].ForeColor = value;
                        break;
                    case Lexer.Css:
                        break;
                    case Lexer.Fortran:
                        TextArea.Styles[Style.Fortran.Number].ForeColor = value;
                        break;
                    case Lexer.FreeBasic:
                        TextArea.Styles[Style.FreeBasic.Number].ForeColor = value;
                        break;
                    case Lexer.Html:
                        TextArea.Styles[Style.Html.Number].ForeColor = value;
                        break;
                    case Lexer.Json:
                        TextArea.Styles[Style.Json.Number].ForeColor = value;
                        break;
                    case Lexer.Lisp:
                        TextArea.Styles[Style.Lisp.Number].ForeColor = value;
                        break;
                    case Lexer.Lua:
                        TextArea.Styles[Style.Lua.Number].ForeColor = value;
                        break;
                    case Lexer.Matlab:
                        TextArea.Styles[Style.Matlab.Number].ForeColor = value;
                        break;
                    case Lexer.Pascal:
                        TextArea.Styles[Style.Pascal.Number].ForeColor = value;
                        break;
                    case Lexer.Perl:
                        TextArea.Styles[Style.Perl.Number].ForeColor = value;
                        break;
                    case Lexer.PhpScript:
                        TextArea.Styles[Style.PhpScript.Number].ForeColor = value;
                        break;
                    case Lexer.PowerShell:
                        TextArea.Styles[Style.PowerShell.Number].ForeColor = value;
                        break;
                    case Lexer.Properties:
                        break;
                    case Lexer.PureBasic:
                        TextArea.Styles[Style.PureBasic.Number].ForeColor = value;
                        break;
                    case Lexer.Python:
                        TextArea.Styles[Style.Python.Number].ForeColor = value;
                        break;
                    case Lexer.Ruby:
                        TextArea.Styles[Style.Ruby.Number].ForeColor = value;
                        break;
                    case Lexer.Smalltalk:
                        TextArea.Styles[Style.Smalltalk.Number].ForeColor = value;
                        break;
                    case Lexer.Sql:
                        TextArea.Styles[Style.Sql.Number].ForeColor = value;
                        break;
                    case Lexer.Vb:
                        TextArea.Styles[Style.Vb.Number].ForeColor = value;
                        break;
                    case Lexer.VbScript:
                        TextArea.Styles[Style.VbScript.Number].ForeColor = value;
                        break;
                    case Lexer.Verilog:
                        TextArea.Styles[Style.Verilog.Number].ForeColor = value;
                        break;
                    case Lexer.Xml:
                        TextArea.Styles[Style.Xml.Number].ForeColor = value;
                        break;
                    case Lexer.BlitzBasic:
                        TextArea.Styles[Style.BlitzBasic.Number].ForeColor = value;
                        break;
                    case Lexer.Markdown:
                        break;
                    case Lexer.R:
                        TextArea.Styles[Style.R.Number].ForeColor = value;
                        break;
                }
            }
        }

        public Color LanguageString
        {
            get { return languageString; }
            set
            {
                languageString = value;
                switch (Language)
                {
                    case Lexer.Container:
                        break;
                    case Lexer.Null:
                        break;
                    case Lexer.Ada:
                        TextArea.Styles[Style.Ada.String].ForeColor = value;
                        break;
                    case Lexer.Asm:
                        TextArea.Styles[Style.Asm.String].ForeColor = value;
                        break;
                    case Lexer.Batch:
                        break;
                    case Lexer.Cpp:
                        TextArea.Styles[Style.Cpp.String].ForeColor = value;
                        break;
                    case Lexer.Css:
                        break;
                    case Lexer.Fortran:
                        break;
                    case Lexer.FreeBasic:
                        TextArea.Styles[Style.FreeBasic.String].ForeColor = value;
                        break;
                    case Lexer.Html:
                        break;
                    case Lexer.Json:
                        TextArea.Styles[Style.Json.String].ForeColor = value;
                        break;
                    case Lexer.Lisp:
                        TextArea.Styles[Style.Lisp.String].ForeColor = value;
                        break;
                    case Lexer.Lua:
                        TextArea.Styles[Style.Lua.String].ForeColor = value;
                        break;
                    case Lexer.Matlab:
                        TextArea.Styles[Style.Matlab.String].ForeColor = value;
                        break;
                    case Lexer.Pascal:
                        TextArea.Styles[Style.Pascal.String].ForeColor = value;
                        break;
                    case Lexer.Perl:
                        TextArea.Styles[Style.Perl.String].ForeColor = value;
                        break;
                    case Lexer.PhpScript:
                        break;
                    case Lexer.PowerShell:
                        TextArea.Styles[Style.PowerShell.String].ForeColor = value;
                        break;
                    case Lexer.Properties:
                        break;
                    case Lexer.PureBasic:
                        TextArea.Styles[Style.PureBasic.String].ForeColor = value;
                        break;
                    case Lexer.Python:
                        TextArea.Styles[Style.Python.String].ForeColor = value;
                        break;
                    case Lexer.Ruby:
                        TextArea.Styles[Style.Ruby.String].ForeColor = value;
                        break;
                    case Lexer.Smalltalk:
                        TextArea.Styles[Style.Smalltalk.String].ForeColor = value;
                        break;
                    case Lexer.Sql:
                        TextArea.Styles[Style.Sql.String].ForeColor = value;
                        break;
                    case Lexer.Vb:
                        TextArea.Styles[Style.Vb.String].ForeColor = value;
                        break;
                    case Lexer.VbScript:
                        TextArea.Styles[Style.VbScript.String].ForeColor = value;
                        break;
                    case Lexer.Verilog:
                        TextArea.Styles[Style.Verilog.String].ForeColor = value;
                        break;
                    case Lexer.Xml:
                        break;
                    case Lexer.BlitzBasic:
                        TextArea.Styles[Style.BlitzBasic.String].ForeColor = value;
                        break;
                    case Lexer.Markdown:
                        break;
                    case Lexer.R:
                        TextArea.Styles[Style.R.String].ForeColor = value;
                        break;
                }
            }
        }

        public Color LanguageCharacter
        {
            get { return languageCharacter; }
            set
            {
                languageCharacter = value;

                switch (Language)
                {
                    case Lexer.Container:
                        break;
                    case Lexer.Null:
                        break;
                    case Lexer.Ada:
                        TextArea.Styles[Style.Ada.Character].ForeColor = value;
                        break;
                    case Lexer.Asm:
                        TextArea.Styles[Style.Asm.Character].ForeColor = value;
                        break;
                    case Lexer.Batch:
                        break;
                    case Lexer.Cpp:
                        TextArea.Styles[Style.Cpp.Character].ForeColor = value;
                        break;
                    case Lexer.Css:
                        break;
                    case Lexer.Fortran:
                        break;
                    case Lexer.FreeBasic:
                        break;
                    case Lexer.Html:
                        break;
                    case Lexer.Json:
                        break;
                    case Lexer.Lisp:
                        break;
                    case Lexer.Lua:
                        TextArea.Styles[Style.Lua.Character].ForeColor = value;
                        break;
                    case Lexer.Matlab:
                        break;
                    case Lexer.Pascal:
                        TextArea.Styles[Style.Pascal.Character].ForeColor = value;
                        break;
                    case Lexer.Perl:
                        TextArea.Styles[Style.Perl.Character].ForeColor = value;
                        break;
                    case Lexer.PhpScript:
                        break;
                    case Lexer.PowerShell:
                        TextArea.Styles[Style.PowerShell.Character].ForeColor = value;
                        break;
                    case Lexer.Properties:
                        break;
                    case Lexer.PureBasic:
                        break;
                    case Lexer.Python:
                        TextArea.Styles[Style.Python.Character].ForeColor = value;
                        break;
                    case Lexer.Ruby:
                        TextArea.Styles[Style.Ruby.Character].ForeColor = value;
                        break;
                    case Lexer.Smalltalk:
                        TextArea.Styles[Style.Smalltalk.Character].ForeColor = value;
                        break;
                    case Lexer.Sql:
                        TextArea.Styles[Style.Sql.Character].ForeColor = value;
                        break;
                    case Lexer.Vb:
                        break;
                    case Lexer.VbScript:
                        break;
                    case Lexer.Verilog:
                        break;
                    case Lexer.Xml:
                        break;
                    case Lexer.BlitzBasic:
                        break;
                    case Lexer.Markdown:
                        break;
                    case Lexer.R:
                        break;
                }
            }
        }

        public Color LanguagePreprocessor
        {
            get { return languagePreprocessor; }
            set
            {
                languagePreprocessor = value;
                switch (Language)
                {
                    case Lexer.Container:
                        break;
                    case Lexer.Null:
                        break;
                    case Lexer.Ada:
                        break;
                    case Lexer.Asm:
                        break;
                    case Lexer.Batch:
                        break;
                    case Lexer.Cpp:
                        TextArea.Styles[Style.Cpp.Preprocessor].ForeColor = value;
                        break;
                    case Lexer.Css:
                        break;
                    case Lexer.Fortran:
                        TextArea.Styles[Style.Fortran.Preprocessor].ForeColor = value;
                        break;
                    case Lexer.FreeBasic:
                        TextArea.Styles[Style.FreeBasic.Preprocessor].ForeColor = value;
                        break;
                    case Lexer.Html:
                        break;
                    case Lexer.Json:
                        break;
                    case Lexer.Lisp:
                        break;
                    case Lexer.Lua:
                        TextArea.Styles[Style.Lua.Preprocessor].ForeColor = value;
                        break;
                    case Lexer.Matlab:
                        break;
                    case Lexer.Pascal:
                        TextArea.Styles[Style.Pascal.Preprocessor].ForeColor = value;
                        break;
                    case Lexer.Perl:
                        TextArea.Styles[Style.Perl.Preprocessor].ForeColor = value;
                        break;
                    case Lexer.PhpScript:
                        break;
                    case Lexer.PowerShell:
                        break;
                    case Lexer.Properties:
                        break;
                    case Lexer.PureBasic:
                        TextArea.Styles[Style.PureBasic.Preprocessor].ForeColor = value;
                        break;
                    case Lexer.Python:
                        break;
                    case Lexer.Ruby:
                        break;
                    case Lexer.Smalltalk:
                        break;
                    case Lexer.Sql:
                        break;
                    case Lexer.Vb:
                        TextArea.Styles[Style.Vb.Preprocessor].ForeColor = value;
                        break;
                    case Lexer.VbScript:
                        TextArea.Styles[Style.VbScript.Preprocessor].ForeColor = value;
                        break;
                    case Lexer.Verilog:
                        TextArea.Styles[Style.Verilog.Preprocessor].ForeColor = value;
                        break;
                    case Lexer.Xml:
                        break;
                    case Lexer.BlitzBasic:
                        TextArea.Styles[Style.BlitzBasic.Preprocessor].ForeColor = value;
                        break;
                    case Lexer.Markdown:
                        break;
                    case Lexer.R:
                        break;
                }
            }
        }

        public Color LanguageOperator
        {
            get { return languageOperator; }
            set
            {
                languageOperator = value;

                switch (Language)
                {
                    case Lexer.Container:
                        break;
                    case Lexer.Null:
                        break;
                    case Lexer.Ada:
                        break;
                    case Lexer.Asm:
                        TextArea.Styles[Style.Asm.Operator].ForeColor = value;
                        break;
                    case Lexer.Batch:
                        TextArea.Styles[Style.Batch.Operator].ForeColor = value;
                        break;
                    case Lexer.Cpp:
                        TextArea.Styles[Style.Cpp.Operator].ForeColor = value;
                        break;
                    case Lexer.Css:
                        TextArea.Styles[Style.Css.Operator].ForeColor = value;
                        break;
                    case Lexer.Fortran:
                        TextArea.Styles[Style.Fortran.Operator].ForeColor = value;
                        break;
                    case Lexer.FreeBasic:
                        TextArea.Styles[Style.FreeBasic.Operator].ForeColor = value;
                        break;
                    case Lexer.Html:
                        break;
                    case Lexer.Json:
                        TextArea.Styles[Style.Json.Operator].ForeColor = value;
                        break;
                    case Lexer.Lisp:
                        TextArea.Styles[Style.Lisp.Operator].ForeColor = value;
                        break;
                    case Lexer.Lua:
                        TextArea.Styles[Style.Lua.Operator].ForeColor = value;
                        break;
                    case Lexer.Matlab:
                        TextArea.Styles[Style.Matlab.Operator].ForeColor = value;
                        break;
                    case Lexer.Pascal:
                        TextArea.Styles[Style.Pascal.Operator].ForeColor = value;
                        break;
                    case Lexer.Perl:
                        TextArea.Styles[Style.Perl.Operator].ForeColor = value;
                        break;
                    case Lexer.PhpScript:
                        TextArea.Styles[Style.PhpScript.Operator].ForeColor = value;
                        break;
                    case Lexer.PowerShell:
                        TextArea.Styles[Style.PowerShell.Operator].ForeColor = value;
                        break;
                    case Lexer.Properties:
                        break;
                    case Lexer.PureBasic:
                        TextArea.Styles[Style.PureBasic.Operator].ForeColor = value;
                        break;
                    case Lexer.Python:
                        TextArea.Styles[Style.Python.Operator].ForeColor = value;
                        break;
                    case Lexer.Ruby:
                        TextArea.Styles[Style.Ruby.Operator].ForeColor = value;
                        break;
                    case Lexer.Smalltalk:
                        break;
                    case Lexer.Sql:
                        TextArea.Styles[Style.Sql.Operator].ForeColor = value;
                        break;
                    case Lexer.Vb:
                        TextArea.Styles[Style.Vb.Operator].ForeColor = value;
                        break;
                    case Lexer.VbScript:
                        TextArea.Styles[Style.VbScript.Operator].ForeColor = value;
                        break;
                    case Lexer.Verilog:
                        TextArea.Styles[Style.Verilog.Operator].ForeColor = value;
                        break;
                    case Lexer.Xml:
                        break;
                    case Lexer.BlitzBasic:
                        TextArea.Styles[Style.BlitzBasic.Operator].ForeColor = value;
                        break;
                    case Lexer.Markdown:
                        break;
                    case Lexer.R:
                        TextArea.Styles[Style.R.Operator].ForeColor = value;
                        break;
                }

            }
        }

        public Color LanguageRegex
        {
            get { return languageRegex; }
            set
            {
                languageRegex = value;

                switch (Language)
                {
                    case Lexer.Container:
                        break;
                    case Lexer.Null:
                        break;
                    case Lexer.Ada:
                        break;
                    case Lexer.Asm:
                        break;
                    case Lexer.Batch:
                        break;
                    case Lexer.Cpp:
                        TextArea.Styles[Style.Cpp.Regex].ForeColor = value;
                        break;
                    case Lexer.Css:
                        break;
                    case Lexer.Fortran:
                        break;
                    case Lexer.FreeBasic:
                        break;
                    case Lexer.Html:
                        break;
                    case Lexer.Json:
                        break;
                    case Lexer.Lisp:
                        break;
                    case Lexer.Lua:
                        break;
                    case Lexer.Matlab:
                        break;
                    case Lexer.Pascal:
                        break;
                    case Lexer.Perl:
                        TextArea.Styles[Style.Perl.Regex].ForeColor = value;
                        break;
                    case Lexer.PhpScript:
                        break;
                    case Lexer.PowerShell:
                        break;
                    case Lexer.Properties:
                        break;
                    case Lexer.PureBasic:
                        break;
                    case Lexer.Python:
                        break;
                    case Lexer.Ruby:
                        TextArea.Styles[Style.Ruby.Regex].ForeColor = value;
                        break;
                    case Lexer.Smalltalk:
                        break;
                    case Lexer.Sql:
                        break;
                    case Lexer.Vb:
                        break;
                    case Lexer.VbScript:
                        break;
                    case Lexer.Verilog:
                        break;
                    case Lexer.Xml:
                        break;
                    case Lexer.BlitzBasic:
                        break;
                    case Lexer.Markdown:
                        break;
                    case Lexer.R:
                        break;
                }
            }
        }

        public Color LanguageCommentLineDoc
        {
            get { return languageCommentLineDoc; }
            set
            {
                languageCommentLineDoc = value;

                switch (Language)
                {
                    case Lexer.Container:
                        break;
                    case Lexer.Null:
                        break;
                    case Lexer.Ada:
                        break;
                    case Lexer.Asm:
                        break;
                    case Lexer.Batch:
                        break;
                    case Lexer.Cpp:
                        TextArea.Styles[Style.Cpp.CommentLineDoc].ForeColor = value;
                        break;
                    case Lexer.Css:
                        break;
                    case Lexer.Fortran:
                        break;
                    case Lexer.FreeBasic:
                        break;
                    case Lexer.Html:
                        break;
                    case Lexer.Json:
                        break;
                    case Lexer.Lisp:
                        break;
                    case Lexer.Lua:
                        break;
                    case Lexer.Matlab:
                        break;
                    case Lexer.Pascal:
                        break;
                    case Lexer.Perl:
                        break;
                    case Lexer.PhpScript:
                        break;
                    case Lexer.PowerShell:
                        break;
                    case Lexer.Properties:
                        break;
                    case Lexer.PureBasic:
                        break;
                    case Lexer.Python:
                        break;
                    case Lexer.Ruby:
                        break;
                    case Lexer.Smalltalk:
                        break;
                    case Lexer.Sql:
                        break;
                    case Lexer.Vb:
                        break;
                    case Lexer.VbScript:
                        break;
                    case Lexer.Verilog:
                        break;
                    case Lexer.Xml:
                        break;
                    case Lexer.BlitzBasic:
                        break;
                    case Lexer.Markdown:
                        break;
                    case Lexer.R:
                        break;
                }
            }
        }

        public Color LanguageWord
        {
            get { return languageWord; }
            set
            {
                languageWord = value;

                switch (Language)
                {
                    case Lexer.Container:
                        break;
                    case Lexer.Null:
                        break;
                    case Lexer.Ada:
                        TextArea.Styles[Style.Ada.Word].ForeColor = value;
                        break;
                    case Lexer.Asm:
                        break;
                    case Lexer.Batch:
                        TextArea.Styles[Style.Batch.Word].ForeColor = value;
                        break;
                    case Lexer.Cpp:
                        TextArea.Styles[Style.Cpp.Word].ForeColor = value;
                        break;
                    case Lexer.Css:
                        break;
                    case Lexer.Fortran:
                        TextArea.Styles[Style.Fortran.Word].ForeColor = value;
                        break;
                    case Lexer.FreeBasic:
                        break;
                    case Lexer.Html:
                        break;
                    case Lexer.Json:
                        break;
                    case Lexer.Lisp:
                        break;
                    case Lexer.Lua:
                        TextArea.Styles[Style.Lua.Word].ForeColor = value;
                        break;
                    case Lexer.Matlab:
                        break;
                    case Lexer.Pascal:
                        TextArea.Styles[Style.Pascal.Word].ForeColor = value;
                        break;
                    case Lexer.Perl:
                        TextArea.Styles[Style.Perl.Word].ForeColor = value;
                        break;
                    case Lexer.PhpScript:
                        TextArea.Styles[Style.PhpScript.Word].ForeColor = value;
                        break;
                    case Lexer.PowerShell:
                        break;
                    case Lexer.Properties:
                        break;
                    case Lexer.PureBasic:
                        break;
                    case Lexer.Python:
                        TextArea.Styles[Style.Python.Word].ForeColor = value;
                        break;
                    case Lexer.Ruby:
                        TextArea.Styles[Style.Ruby.Word].ForeColor = value;
                        break;
                    case Lexer.Smalltalk:
                        break;
                    case Lexer.Sql:
                        TextArea.Styles[Style.Sql.Word].ForeColor = value;
                        break;
                    case Lexer.Vb:
                        break;
                    case Lexer.VbScript:
                        break;
                    case Lexer.Verilog:
                        TextArea.Styles[Style.Verilog.Word].ForeColor = value;
                        break;
                    case Lexer.Xml:
                        break;
                    case Lexer.BlitzBasic:
                        break;
                    case Lexer.Markdown:
                        break;
                    case Lexer.R:
                        break;
                }
            }
        }

        public Color LanguageWord2
        {
            get { return languageWord2; }
            set
            {
                languageWord2 = value;
                switch (Language)
                {
                    case Lexer.Container:
                        break;
                    case Lexer.Null:
                        break;
                    case Lexer.Ada:
                        break;
                    case Lexer.Asm:
                        break;
                    case Lexer.Batch:
                        break;
                    case Lexer.Cpp:
                        TextArea.Styles[Style.Cpp.Word2].ForeColor = value;
                        break;
                    case Lexer.Css:
                        break;
                    case Lexer.Fortran:
                        TextArea.Styles[Style.Fortran.Word2].ForeColor = value;
                        break;
                    case Lexer.FreeBasic:
                        break;
                    case Lexer.Html:
                        break;
                    case Lexer.Json:
                        break;
                    case Lexer.Lisp:
                        break;
                    case Lexer.Lua:
                        TextArea.Styles[Style.Lua.Word2].ForeColor = value;
                        break;
                    case Lexer.Matlab:
                        break;
                    case Lexer.Pascal:
                        break;
                    case Lexer.Perl:
                        break;
                    case Lexer.PhpScript:
                        break;
                    case Lexer.PowerShell:
                        break;
                    case Lexer.Properties:
                        break;
                    case Lexer.PureBasic:
                        break;
                    case Lexer.Python:
                        TextArea.Styles[Style.Python.Word2].ForeColor = value;
                        break;
                    case Lexer.Ruby:
                        break;
                    case Lexer.Smalltalk:
                        break;
                    case Lexer.Sql:
                        TextArea.Styles[Style.Sql.Word2].ForeColor = value;
                        break;
                    case Lexer.Vb:
                        break;
                    case Lexer.VbScript:
                        break;
                    case Lexer.Verilog:
                        TextArea.Styles[Style.Verilog.Word2].ForeColor = value;
                        break;
                    case Lexer.Xml:
                        break;
                    case Lexer.BlitzBasic:
                        break;
                    case Lexer.Markdown:
                        break;
                    case Lexer.R:
                        break;
                }
            }
        }

        public Color LanguageCommentDocKeyword
        {
            get { return languageCommentDocKeyword; }
            set
            {
                languageCommentDocKeyword = value;

                switch (Language)
                {
                    case Lexer.Container:
                        break;
                    case Lexer.Null:
                        break;
                    case Lexer.Ada:
                        break;
                    case Lexer.Asm:
                        break;
                    case Lexer.Batch:
                        break;
                    case Lexer.Cpp:
                        TextArea.Styles[Style.Cpp.CommentDocKeyword].ForeColor = value;
                        break;
                    case Lexer.Css:
                        break;
                    case Lexer.Fortran:
                        break;
                    case Lexer.FreeBasic:
                        break;
                    case Lexer.Html:
                        break;
                    case Lexer.Json:
                        break;
                    case Lexer.Lisp:
                        break;
                    case Lexer.Lua:
                        break;
                    case Lexer.Matlab:
                        break;
                    case Lexer.Pascal:
                        break;
                    case Lexer.Perl:
                        break;
                    case Lexer.PhpScript:
                        break;
                    case Lexer.PowerShell:
                        TextArea.Styles[Style.PowerShell.CommentDocKeyword].ForeColor = value;
                        break;
                    case Lexer.Properties:
                        break;
                    case Lexer.PureBasic:
                        break;
                    case Lexer.Python:
                        break;
                    case Lexer.Ruby:
                        break;
                    case Lexer.Smalltalk:
                        break;
                    case Lexer.Sql:
                        TextArea.Styles[Style.Sql.CommentDocKeyword].ForeColor = value;
                        break;
                    case Lexer.Vb:
                        break;
                    case Lexer.VbScript:
                        break;
                    case Lexer.Verilog:
                        break;
                    case Lexer.Xml:
                        break;
                    case Lexer.BlitzBasic:
                        break;
                    case Lexer.Markdown:
                        break;
                    case Lexer.R:
                        break;
                }
            }
        }

        public Color LanguageCommentDocKeywordError
        {
            get { return languageCommentDocKeywordError; }
            set
            {
                languageCommentDocKeywordError = value;

                switch (Language)
                {
                    case Lexer.Container:
                        break;
                    case Lexer.Null:
                        break;
                    case Lexer.Ada:
                        break;
                    case Lexer.Asm:
                        break;
                    case Lexer.Batch:
                        break;
                    case Lexer.Cpp:
                        TextArea.Styles[Style.Cpp.CommentDocKeywordError].ForeColor = value;
                        break;
                    case Lexer.Css:
                        break;
                    case Lexer.Fortran:
                        break;
                    case Lexer.FreeBasic:
                        break;
                    case Lexer.Html:
                        break;
                    case Lexer.Json:
                        break;
                    case Lexer.Lisp:
                        break;
                    case Lexer.Lua:
                        break;
                    case Lexer.Matlab:
                        break;
                    case Lexer.Pascal:
                        break;
                    case Lexer.Perl:
                        break;
                    case Lexer.PhpScript:
                        break;
                    case Lexer.PowerShell:
                        break;
                    case Lexer.Properties:
                        break;
                    case Lexer.PureBasic:
                        break;
                    case Lexer.Python:
                        break;
                    case Lexer.Ruby:
                        break;
                    case Lexer.Smalltalk:
                        break;
                    case Lexer.Sql:
                        TextArea.Styles[Style.Sql.CommentDocKeywordError].ForeColor = value;
                        break;
                    case Lexer.Vb:
                        break;
                    case Lexer.VbScript:
                        break;
                    case Lexer.Verilog:
                        break;
                    case Lexer.Xml:
                        break;
                    case Lexer.BlitzBasic:
                        break;
                    case Lexer.Markdown:
                        break;
                    case Lexer.R:
                        break;
                }
            }
        }

        public Color LanguageGlobalClass
        {
            get { return languageGlobalClass; }
            set
            {
                languageGlobalClass = value;

                switch (Language)
                {
                    case Lexer.Container:
                        break;
                    case Lexer.Null:
                        break;
                    case Lexer.Ada:
                        TextArea.Styles[Style.Ada.Identifier].ForeColor = value;
                        break;
                    case Lexer.Asm:
                        TextArea.Styles[Style.Asm.Identifier].ForeColor = value;
                        break;
                    case Lexer.Batch:
                        TextArea.Styles[Style.Batch.Identifier].ForeColor = value;
                        break;
                    case Lexer.Cpp:
                        TextArea.Styles[Style.Cpp.Identifier].ForeColor = value;
                        break;
                    case Lexer.Css:
                        TextArea.Styles[Style.Css.Identifier].ForeColor = value;
                        break;
                    case Lexer.Fortran:
                        TextArea.Styles[Style.Fortran.Identifier].ForeColor = value;
                        break;
                    case Lexer.FreeBasic:
                        TextArea.Styles[Style.FreeBasic.Identifier].ForeColor = value;
                        break;
                    case Lexer.Html:
                        break;
                    case Lexer.Json:
                        break;
                    case Lexer.Lisp:
                        TextArea.Styles[Style.Lisp.Identifier].ForeColor = value;
                        break;
                    case Lexer.Lua:
                        TextArea.Styles[Style.Lua.Identifier].ForeColor = value;
                        break;
                    case Lexer.Matlab:
                        TextArea.Styles[Style.Matlab.Identifier].ForeColor = value;
                        break;
                    case Lexer.Pascal:
                        TextArea.Styles[Style.Pascal.Identifier].ForeColor = value;
                        break;
                    case Lexer.Perl:
                        TextArea.Styles[Style.Perl.Identifier].ForeColor = value;
                        break;
                    case Lexer.PhpScript:
                        break;
                    case Lexer.PowerShell:
                        TextArea.Styles[Style.PowerShell.Identifier].ForeColor = value;
                        break;
                    case Lexer.Properties:
                        break;
                    case Lexer.PureBasic:
                        TextArea.Styles[Style.PureBasic.Identifier].ForeColor = value;
                        break;
                    case Lexer.Python:
                        TextArea.Styles[Style.Python.Identifier].ForeColor = value;
                        break;
                    case Lexer.Ruby:
                        TextArea.Styles[Style.Ruby.Identifier].ForeColor = value;
                        break;
                    case Lexer.Smalltalk:
                        break;
                    case Lexer.Sql:
                        TextArea.Styles[Style.Sql.Identifier].ForeColor = value;
                        break;
                    case Lexer.Vb:
                        TextArea.Styles[Style.Vb.Identifier].ForeColor = value;
                        break;
                    case Lexer.VbScript:
                        TextArea.Styles[Style.VbScript.Identifier].ForeColor = value;
                        break;
                    case Lexer.Verilog:
                        TextArea.Styles[Style.Verilog.Identifier].ForeColor = value;
                        break;
                    case Lexer.Xml:
                        break;
                    case Lexer.BlitzBasic:
                        TextArea.Styles[Style.BlitzBasic.Identifier].ForeColor = value;
                        break;
                    case Lexer.Markdown:
                        break;
                    case Lexer.R:
                        TextArea.Styles[Style.R.Identifier].ForeColor = value;
                        break;
                }
            }
        }

        public Color MarginNumberLineBack
        {
            get { return marginNumberLineBack; }
            set
            {
                marginNumberLineBack = value;

                TextArea.Styles[Style.LineNumber].BackColor = value;

            }
        }

        public Color MarginNumberLineFore
        {
            get { return marginNumberLineFore; }
            set
            {
                marginNumberLineFore = value;
                TextArea.Styles[Style.LineNumber].ForeColor = value;

            }
        }

        public Color MarginNumberIndentBack
        {
            get { return marginNumberIndentBack; }
            set
            {
                marginNumberIndentBack = value;
                TextArea.Styles[Style.IndentGuide].BackColor = value;

            }
        }

        public Color MarginNumberIndentFore
        {
            get { return marginNumberIndentFore; }
            set
            {
                marginNumberIndentFore = value;
                TextArea.Styles[Style.IndentGuide].ForeColor = value;


            }
        }

        public int MarginNumberWidth
        {
            get { return marginNumberWidth; }
            set
            {
                marginNumberWidth = value;

                TextArea.Margins[NUMBER_MARGIN].Width = value;

            }
        }

        public MarginType MarginNumberType
        {
            get { return marginNumberType; }
            set
            {
                marginNumberType = value;

                TextArea.Margins[NUMBER_MARGIN].Type = value;

            }
        }

        public bool MarginNumberSensitive
        {
            get { return marginNumberSensitive; }
            set
            {
                marginNumberSensitive = value;

                TextArea.Margins[NUMBER_MARGIN].Sensitive = value;

            }
        }

        public uint MarginNumberMask
        {
            get { return marginNumberMask; }
            set
            {
                marginNumberMask = value;

                TextArea.Margins[NUMBER_MARGIN].Mask = value;
            }
        }

        public Color FoldMargin
        {
            get { return foldMargin; }
            set
            {
                foldMargin = value;

                TextArea.SetFoldMarginColor(true, value);

            }
        }

        public Color FoldMarginHighlight
        {
            get { return foldMarginHighlight; }
            set
            {
                foldMarginHighlight = value;
                TextArea.SetFoldMarginHighlightColor(true, value);


            }
        }

        public MarginType FoldMarginType
        {
            get { return foldMarginType; }
            set
            {
                foldMarginType = value;


                // Configure a margin to display folding symbols
                TextArea.Margins[FOLDING_MARGIN].Type = value;

            }
        }

        public bool FoldMarginSensitive
        {
            get { return foldMarginSensitive; }
            set
            {
                foldMarginSensitive = value;
                TextArea.Margins[FOLDING_MARGIN].Sensitive = value;

            }
        }

        public int FoldingMarginWidth
        {
            get { return foldingMarginWidth; }
            set
            {
                foldingMarginWidth = value;

                TextArea.Margins[FOLDING_MARGIN].Width = value;


            }
        }

        public Color FoldMarginBack
        {
            get { return foldMarginBack; }
            set
            {
                foldMarginBack = value;

                // Set colors for all folding markers
                for (int i = 25; i <= 31; i++)
                {
                    TextArea.Markers[i].SetForeColor(value); // styles for [+] and [-]

                }


            }
        }

        public Color FoldMarginFore
        {
            get { return foldMarginFore; }
            set
            {
                foldMarginFore = value;
                for (int i = 25; i <= 31; i++)
                {
                    TextArea.Markers[i].SetBackColor(value); // styles for [+] and [-]
                }

            }
        }

        public MarkerSymbol FoldFolder
        {
            get { return foldFolder; }
            set
            {
                foldFolder = value;


                // Configure folding markers with respective symbols
                TextArea.Markers[Marker.Folder].Symbol = value;

            }
        }

        public MarkerSymbol FoldFolderOpen
        {
            get { return foldFolderOpen; }
            set
            {
                foldFolderOpen = value;

                TextArea.Markers[Marker.FolderOpen].Symbol = value;

            }
        }

        public MarkerSymbol FoldFolderEnd
        {
            get { return foldFolderEnd; }
            set
            {
                foldFolderEnd = value;

                TextArea.Markers[Marker.FolderEnd].Symbol = value;

            }
        }

        public MarkerSymbol FoldFolderMidTail
        {
            get { return foldFolderMidTail; }
            set
            {
                foldFolderMidTail = value;

                TextArea.Markers[Marker.FolderMidTail].Symbol = value;

            }
        }

        public MarkerSymbol FoldFolderOpenMid
        {
            get { return foldFolderOpenMid; }
            set
            {
                foldFolderOpenMid = value;

                TextArea.Markers[Marker.FolderOpenMid].Symbol = value;

            }
        }

        public MarkerSymbol FoldFolderSub
        {
            get { return foldFolderSub; }
            set
            {
                foldFolderSub = value;

                TextArea.Markers[Marker.FolderSub].Symbol = value;

            }
        }

        public MarkerSymbol FoldFolderTail
        {
            get { return foldFolderTail; }
            set
            {
                foldFolderTail = value;

                TextArea.Markers[Marker.FolderTail].Symbol = value;


            }
        }

        public AutomaticFold FolderAutomaticFold
        {
            get { return folderautomaticFold; }
            set
            {
                folderautomaticFold = value;

                TextArea.AutomaticFold = value;

            }
        }

        public int BookmarginWidth
        {
            get { return bookmarginWidth; }
            set
            {
                bookmarginWidth = value;

                TextArea.Margins[BOOKMARK_MARGIN].Width = value;
                
            }
        }

        public MarginType BookmarginType
        {
            get { return bookmarginType; }
            set
            {
                bookmarginType = value;

                TextArea.Margins[BOOKMARK_MARGIN].Type = value;
                

            }
        }

        public bool BookmarginSensitive
        {
            get { return bookmarginSensitive; }
            set
            {
                bookmarginSensitive = value;

                TextArea.Margins[BOOKMARK_MARGIN].Sensitive = value;

                
            }
        }

        public MarkerSymbol BookmarkerSymbol
        {
            get { return bookmarkerSymbol; }
            set
            {
                bookmarkerSymbol = value;
                TextArea.Markers[BOOKMARK_MARKER].Symbol = value;

            }
        }
        
        public Color BookmarkerBack
        {
            get { return bookmarkerBack; }
            set
            {
                bookmarkerBack = value;

                TextArea.Markers[BOOKMARK_MARKER].SetBackColor(value);
                
            }
        }

        public Color BookmarkerFore
        {
            get { return bookmarkerFore; }
            set
            {
                bookmarkerFore = value;
                TextArea.Markers[BOOKMARK_MARKER].SetForeColor(value);
                
            }
        }

        public float BookmarkerAlpha
        {
            get { return bookmarkerAlpha; }
            set
            {
                if (value > 1)
                {
                    value = 1;
                }

                if (value < 0)
                {
                    value = 0;
                }
                    
                bookmarkerAlpha = value;

                TextArea.Markers[BOOKMARK_MARKER].SetAlpha(Convert.ToInt32(value * 255));
            }
        }

        
        #endregion

        
    }


    
}