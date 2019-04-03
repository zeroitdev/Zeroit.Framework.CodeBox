using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Serialization;

namespace Zeroit.Framework.CodeBox
{
    
    public partial class MainText : ISerializable
    {
        public MainText(SerializationInfo info, StreamingContext context)
        {
            Keywords = (List<string>)info.GetValue("Keywords", typeof(List<string>));
            Language = (Lexer)info.GetValue("Language", typeof(Lexer));
            Font = (Font)info.GetValue("Font", typeof(Font));
            BackColor = (Color)info.GetValue("BackColor", typeof(Color));
            ForeColor = (Color)info.GetValue("ForeColor", typeof(Color));
            HighlightColor = (Color)info.GetValue("HighlightColor", typeof(Color));
            LanguageIdentifier = (Color)info.GetValue("LanguageIdentifier", typeof(Color));
            LanguageComment = (Color)info.GetValue("LanguageComment", typeof(Color));
            LanguageCommentLine = (Color)info.GetValue("LanguageCommentLine", typeof(Color));
            LanguageCommentDoc = (Color)info.GetValue("LanguageCommentDoc", typeof(Color));
            LanguageNumber = (Color)info.GetValue("LanguageNumber", typeof(Color));
            LanguageString = (Color)info.GetValue("LanguageString", typeof(Color));
            LanguageCharacter = (Color)info.GetValue("LanguageCharacter", typeof(Color));
            LanguagePreprocessor = (Color)info.GetValue("LanguagePreprocessor", typeof(Color));
            LanguageOperator = (Color)info.GetValue("LanguageOperator", typeof(Color));
            LanguageRegex = (Color)info.GetValue("LanguageRegex", typeof(Color));
            LanguageCommentLineDoc = (Color)info.GetValue("LanguageCommentLineDoc", typeof(Color));
            LanguageWord = (Color)info.GetValue("LanguageWord", typeof(Color));
            LanguageWord2 = (Color)info.GetValue("LanguageWord2", typeof(Color));
            LanguageCommentDocKeyword = (Color)info.GetValue("LanguageCommentDocKeyword", typeof(Color));
            LanguageCommentDocKeywordError = (Color)info.GetValue("LanguageCommentDocKeywordError", typeof(Color));
            LanguageGlobalClass = (Color)info.GetValue("LanguageGlobalClass", typeof(Color));
            MarginNumberLineBack = (Color)info.GetValue("MarginNumberLineBack", typeof(Color));
            MarginNumberLineFore = (Color)info.GetValue("MarginNumberLineFore", typeof(Color));
            MarginNumberIndentBack = (Color)info.GetValue("MarginNumberIndentBack", typeof(Color));
            MarginNumberIndentFore = (Color)info.GetValue("MarginNumberIndentFore", typeof(Color));
            MarginNumberWidth = info.GetInt32("MarginNumberWidth");
            MarginNumberType = (MarginType)info.GetValue("MarginNumberType", typeof(MarginType));
            MarginNumberSensitive = info.GetBoolean("MarginNumberSensitive");
            MarginNumberMask = info.GetUInt32("MarginNumberMask");
            FoldMargin = (Color)info.GetValue("FoldMargin", typeof(Color));
            FoldMarginHighlight = (Color)info.GetValue("FoldMarginHighlight", typeof(Color));
            FoldMarginType = (MarginType)info.GetValue("FoldMarginType", typeof(MarginType));
            FoldMarginSensitive = info.GetBoolean("FoldMarginSensitive");
            FoldingMarginWidth = info.GetInt32("FoldingMarginWidth");
            FoldMarginBack = (Color)info.GetValue("FoldMarginBack", typeof(Color));
            FoldMarginFore = (Color)info.GetValue("FoldMarginFore", typeof(Color));
            FoldFolder = (MarkerSymbol)info.GetValue("FoldFolder", typeof(MarkerSymbol));
            FoldFolderOpen = (MarkerSymbol)info.GetValue("FoldFolderOpen", typeof(MarkerSymbol));
            FoldFolderEnd = (MarkerSymbol)info.GetValue("FoldFolderEnd", typeof(MarkerSymbol));
            FoldFolderMidTail = (MarkerSymbol)info.GetValue("FoldFolderMidTail", typeof(MarkerSymbol));
            FoldFolderOpenMid = (MarkerSymbol)info.GetValue("FoldFolderOpenMid", typeof(MarkerSymbol));
            FoldFolderSub = (MarkerSymbol)info.GetValue("FoldFolderSub", typeof(MarkerSymbol));
            FoldFolderTail = (MarkerSymbol)info.GetValue("FoldFolderTail", typeof(MarkerSymbol));
            FolderAutomaticFold = (AutomaticFold)info.GetValue("FolderAutomaticFold", typeof(AutomaticFold));
            BookmarginWidth = info.GetInt32("BookmarginWidth");
            BookmarginType = (MarginType)info.GetValue("BookmarginType", typeof(MarginType));
            BookmarginSensitive = info.GetBoolean("BookmarginSensitive");
            BookmarkerSymbol = (MarkerSymbol)info.GetValue("BookmarkerSymbol", typeof(MarkerSymbol));
            BookmarkerBack = (Color)info.GetValue("BookmarkerBack", typeof(Color));
            BookmarkerFore = (Color)info.GetValue("BookmarkerFore", typeof(Color));
            BookmarkerAlpha = (float)info.GetValue("BookmarkerAlpha", typeof(float));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Keywords", Keywords);
            info.AddValue("Language", Language);
            info.AddValue("Font", Font);
            info.AddValue("BackColor", BackColor);
            info.AddValue("ForeColor", ForeColor);
            info.AddValue("HighlightColor", HighlightColor);
            info.AddValue("LanguageIdentifier", LanguageIdentifier);
            info.AddValue("LanguageComment", LanguageComment);
            info.AddValue("LanguageCommentLine", LanguageCommentLine);
            info.AddValue("LanguageCommentDoc", LanguageCommentDoc);
            info.AddValue("LanguageNumber", LanguageNumber);
            info.AddValue("LanguageString", LanguageString);
            info.AddValue("LanguageCharacter", LanguageCharacter);
            info.AddValue("LanguagePreprocessor", LanguagePreprocessor);
            info.AddValue("LanguageOperator", LanguageOperator);
            info.AddValue("LanguageRegex", LanguageRegex);
            info.AddValue("LanguageCommentLineDoc", LanguageCommentLineDoc);
            info.AddValue("LanguageWord", LanguageWord);
            info.AddValue("LanguageWord2", LanguageWord2);
            info.AddValue("LanguageCommentDocKeyword", LanguageCommentDocKeyword);
            info.AddValue("LanguageCommentDocKeywordError", LanguageCommentDocKeywordError);
            info.AddValue("LanguageGlobalClass", LanguageGlobalClass);
            info.AddValue("MarginNumberLineBack", MarginNumberLineBack);
            info.AddValue("MarginNumberLineFore", MarginNumberLineFore);
            info.AddValue("MarginNumberIndentBack", MarginNumberIndentBack);
            info.AddValue("MarginNumberIndentFore", MarginNumberIndentFore);
            info.AddValue("MarginNumberWidth", MarginNumberWidth);
            info.AddValue("MarginNumberType", MarginNumberType);
            info.AddValue("MarginNumberSensitive", MarginNumberSensitive);
            info.AddValue("MarginNumberMask", MarginNumberMask);
            info.AddValue("FoldMargin", FoldMargin);
            info.AddValue("FoldMarginHighlight", FoldMarginHighlight);
            info.AddValue("FoldMarginType", FoldMarginType);
            info.AddValue("FoldMarginSensitive", FoldMarginSensitive);
            info.AddValue("FoldingMarginWidth", FoldingMarginWidth);
            info.AddValue("FoldMarginBack", FoldMarginBack);
            info.AddValue("FoldMarginFore", FoldMarginFore);
            info.AddValue("FoldFolder", FoldFolder);
            info.AddValue("FoldFolderOpen", FoldFolderOpen);
            info.AddValue("FoldFolderEnd", FoldFolderEnd);
            info.AddValue("FoldFolderMidTail", FoldFolderMidTail);
            info.AddValue("FoldFolderOpenMid", FoldFolderOpenMid);
            info.AddValue("FoldFolderSub", FoldFolderSub);
            info.AddValue("FoldFolderTail", FoldFolderTail);
            info.AddValue("FolderAutomaticFold", FolderAutomaticFold);
            info.AddValue("BookmarginWidth", BookmarginWidth);
            info.AddValue("BookmarginType", BookmarginType);
            info.AddValue("BookmarginSensitive", BookmarginSensitive);
            info.AddValue("BookmarkerSymbol", BookmarkerSymbol);
            info.AddValue("BookmarkerBack", BookmarkerBack);
            info.AddValue("BookmarkerFore", BookmarkerFore);
            info.AddValue("BookmarkerAlpha", BookmarkerAlpha);
            
        }
    }


    
}