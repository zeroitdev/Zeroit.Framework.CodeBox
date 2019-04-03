// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="Methods.cs" company="Zeroit Dev">
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
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Zeroit.Framework.CodeBox
{
    public partial class ZeroitCodeExplorer : Control
    {
        
        #region Methods

        /// <summary>
        /// Increases the reference count of the specified document by 1.
        /// </summary>
        /// <param name="document">The document reference count to increase.</param>
        public void AddRefDocument(Document document)
        {
            var ptr = document.Value;
            DirectMessage(NativeMethods.SCI_ADDREFDOCUMENT, IntPtr.Zero, ptr);
        }

        /// <summary>
        /// Adds an additional selection range to the existing main selection.
        /// </summary>
        /// <param name="caret">The zero-based document position to end the selection.</param>
        /// <param name="anchor">The zero-based document position to start the selection.</param>
        /// <remarks>A main selection must first have been set by a call to <see cref="SetSelection" />.</remarks>
        public void AddSelection(int caret, int anchor)
        {
            var textLength = TextLength;
            caret = Helpers.Clamp(caret, 0, textLength);
            anchor = Helpers.Clamp(anchor, 0, textLength);

            caret = Lines.CharToBytePosition(caret);
            anchor = Lines.CharToBytePosition(anchor);

            DirectMessage(NativeMethods.SCI_ADDSELECTION, new IntPtr(caret), new IntPtr(anchor));
        }

        /// <summary>
        /// Inserts the specified text at the current caret position.
        /// </summary>
        /// <param name="text">The text to insert at the current caret position.</param>
        /// <remarks>The caret position is set to the end of the inserted text, but it is not scrolled into view.</remarks>
        public unsafe void AddText(string text)
        {
            var bytes = Helpers.GetBytes(text ?? string.Empty, Encoding, zeroTerminated: false);
            fixed (byte* bp = bytes)
                DirectMessage(NativeMethods.SCI_ADDTEXT, new IntPtr(bytes.Length), new IntPtr(bp));
        }

        /// <summary>
        /// Removes the annotation text for every <see cref="Line" /> in the document.
        /// </summary>
        public void AnnotationClearAll()
        {
            DirectMessage(NativeMethods.SCI_ANNOTATIONCLEARALL);
        }

        /// <summary>
        /// Adds the specified text to the end of the document.
        /// </summary>
        /// <param name="text">The text to add to the document.</param>
        /// <remarks>The current selection is not changed and the new text is not scrolled into view.</remarks>
        public unsafe void AppendText(string text)
        {
            var bytes = Helpers.GetBytes(text ?? string.Empty, Encoding, zeroTerminated: false);
            fixed (byte* bp = bytes)
                DirectMessage(NativeMethods.SCI_APPENDTEXT, new IntPtr(bytes.Length), new IntPtr(bp));
        }

        /// <summary>
        /// Assigns the specified key definition to a <see cref="Scintilla" /> command.
        /// </summary>
        /// <param name="keyDefinition">The key combination to bind.</param>
        /// <param name="sciCommand">The command to assign.</param>
        public void AssignCmdKey(Keys keyDefinition, Command sciCommand)
        {
            var keys = Helpers.TranslateKeys(keyDefinition);
            DirectMessage(NativeMethods.SCI_ASSIGNCMDKEY, new IntPtr(keys), new IntPtr((int)sciCommand));
        }

        /// <summary>
        /// Cancels any displayed autocompletion list.
        /// </summary>
        /// <seealso cref="AutoCStops" />
        public void AutoCCancel()
        {
            DirectMessage(NativeMethods.SCI_AUTOCCANCEL);
        }

        /// <summary>
        /// Triggers completion of the current autocompletion word.
        /// </summary>
        public void AutoCComplete()
        {
            DirectMessage(NativeMethods.SCI_AUTOCCOMPLETE);
        }

        /// <summary>
        /// Selects an item in the autocompletion list.
        /// </summary>
        /// <param name="select">
        /// The autocompletion word to select.
        /// If found, the word in the autocompletion list is selected and the index can be obtained by calling <see cref="AutoCCurrent" />.
        /// If not found, the behavior is determined by <see cref="AutoCAutoHide" />.
        /// </param>
        /// <remarks>
        /// Comparisons are performed according to the <see cref="AutoCIgnoreCase" /> property
        /// and will match the first word starting with <paramref name="select" />.
        /// </remarks>
        /// <seealso cref="AutoCCurrent" />
        /// <seealso cref="AutoCAutoHide" />
        /// <seealso cref="AutoCIgnoreCase" />
        public unsafe void AutoCSelect(string select)
        {
            var bytes = Helpers.GetBytes(select, Encoding, zeroTerminated: true);
            fixed (byte* bp = bytes)
                DirectMessage(NativeMethods.SCI_AUTOCSELECT, IntPtr.Zero, new IntPtr(bp));
        }

        /// <summary>
        /// Sets the characters that, when typed, cause the autocompletion item to be added to the document.
        /// </summary>
        /// <param name="chars">A string of characters that trigger autocompletion. The default is null.</param>
        /// <remarks>Common fillup characters are '(', '[', and '.' depending on the language.</remarks>
        public unsafe void AutoCSetFillUps(string chars)
        {
            // Apparently Scintilla doesn't make a copy of our fill up string; it just keeps a pointer to it....
            // That means we need to keep a copy of the string around for the life of the control AND put it
            // in a place where it won't get moved by the GC.

            if (chars == null)
                chars = string.Empty;

            if (fillUpChars != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(fillUpChars);
                fillUpChars = IntPtr.Zero;
            }

            var count = (Encoding.GetByteCount(chars) + 1);
            IntPtr newFillUpChars = Marshal.AllocHGlobal(count);
            fixed (char* ch = chars)
                Encoding.GetBytes(ch, chars.Length, (byte*)newFillUpChars, count);

            ((byte*)newFillUpChars)[count - 1] = 0; // Null terminate
            fillUpChars = newFillUpChars;

            // var str = new String((sbyte*)fillUpChars, 0, count, Encoding);

            DirectMessage(NativeMethods.SCI_AUTOCSETFILLUPS, IntPtr.Zero, fillUpChars);
        }

        /// <summary>
        /// Displays an auto completion list.
        /// </summary>
        /// <param name="lenEntered">The number of characters already entered to match on.</param>
        /// <param name="list">A list of autocompletion words separated by the <see cref="AutoCSeparator" /> character.</param>
        public unsafe void AutoCShow(int lenEntered, string list)
        {
            if (string.IsNullOrEmpty(list))
                return;

            lenEntered = Helpers.ClampMin(lenEntered, 0);
            if (lenEntered > 0)
            {
                // Convert to bytes by counting back the specified number of characters
                var endPos = DirectMessage(NativeMethods.SCI_GETCURRENTPOS).ToInt32();
                var startPos = endPos;
                for (int i = 0; i < lenEntered; i++)
                    startPos = DirectMessage(NativeMethods.SCI_POSITIONRELATIVE, new IntPtr(startPos), new IntPtr(-1)).ToInt32();

                lenEntered = (endPos - startPos);
            }

            var bytes = Helpers.GetBytes(list, Encoding, zeroTerminated: true);
            fixed (byte* bp = bytes)
                DirectMessage(NativeMethods.SCI_AUTOCSHOW, new IntPtr(lenEntered), new IntPtr(bp));
        }

        /// <summary>
        /// Specifies the characters that will automatically cancel autocompletion without the need to call <see cref="AutoCCancel" />.
        /// </summary>
        /// <param name="chars">A String of the characters that will cancel autocompletion. The default is empty.</param>
        /// <remarks>Characters specified should be limited to printable ASCII characters.</remarks>
        public unsafe void AutoCStops(string chars)
        {
            var bytes = Helpers.GetBytes(chars ?? string.Empty, Encoding.ASCII, zeroTerminated: true);
            fixed (byte* bp = bytes)
                DirectMessage(NativeMethods.SCI_AUTOCSTOPS, IntPtr.Zero, new IntPtr(bp));
        }

        /// <summary>
        /// Marks the beginning of a set of actions that should be treated as a single undo action.
        /// </summary>
        /// <remarks>A call to <see cref="BeginUndoAction" /> should be followed by a call to <see cref="EndUndoAction" />.</remarks>
        /// <seealso cref="EndUndoAction" />
        public void BeginUndoAction()
        {
            DirectMessage(NativeMethods.SCI_BEGINUNDOACTION);
        }

        /// <summary>
        /// Styles the specified character position with the <see cref="Style.BraceBad" /> style when there is an unmatched brace.
        /// </summary>
        /// <param name="position">The zero-based document position of the unmatched brace character or <seealso cref="InvalidPosition"/> to remove the highlight.</param>
        public void BraceBadLight(int position)
        {
            position = Helpers.Clamp(position, -1, TextLength);
            if (position > 0)
                position = Lines.CharToBytePosition(position);

            DirectMessage(NativeMethods.SCI_BRACEBADLIGHT, new IntPtr(position));
        }

        /// <summary>
        /// Styles the specified character positions with the <see cref="Style.BraceLight" /> style.
        /// </summary>
        /// <param name="position1">The zero-based document position of the open brace character.</param>
        /// <param name="position2">The zero-based document position of the close brace character.</param>
        /// <remarks>Brace highlighting can be removed by specifying <see cref="InvalidPosition" /> for <paramref name="position1" /> and <paramref name="position2" />.</remarks>
        /// <seealso cref="HighlightGuide" />
        public void BraceHighlight(int position1, int position2)
        {
            var textLength = TextLength;

            position1 = Helpers.Clamp(position1, -1, textLength);
            if (position1 > 0)
                position1 = Lines.CharToBytePosition(position1);

            position2 = Helpers.Clamp(position2, -1, textLength);
            if (position2 > 0)
                position2 = Lines.CharToBytePosition(position2);

            DirectMessage(NativeMethods.SCI_BRACEHIGHLIGHT, new IntPtr(position1), new IntPtr(position2));
        }

        /// <summary>
        /// Finds a corresponding matching brace starting at the position specified.
        /// The brace characters handled are '(', ')', '[', ']', '{', '}', '&lt;', and '&gt;'.
        /// </summary>
        /// <param name="position">The zero-based document position of a brace character to start the search from for a matching brace character.</param>
        /// <returns>The zero-based document position of the corresponding matching brace or <see cref="InvalidPosition" /> it no matching brace could be found.</returns>
        /// <remarks>A match only occurs if the style of the matching brace is the same as the starting brace. Nested braces are handled correctly.</remarks>
        public int BraceMatch(int position)
        {
            position = Helpers.Clamp(position, 0, TextLength);
            position = Lines.CharToBytePosition(position);

            var match = DirectMessage(NativeMethods.SCI_BRACEMATCH, new IntPtr(position), IntPtr.Zero).ToInt32();
            if (match > 0)
                match = Lines.ByteToCharPosition(match);

            return match;
        }

        /// <summary>
        /// Cancels the display of a call tip window.
        /// </summary>
        public void CallTipCancel()
        {
            DirectMessage(NativeMethods.SCI_CALLTIPCANCEL);
        }

        /// <summary>
        /// Sets the color of highlighted text in a call tip.
        /// </summary>
        /// <param name="color">The new highlight text Color. The default is dark blue.</param>
        public void CallTipSetForeHlt(Color color)
        {
            var colour = ColorTranslator.ToWin32(color);
            DirectMessage(NativeMethods.SCI_CALLTIPSETFOREHLT, new IntPtr(colour));
        }

        /// <summary>
        /// Sets the specified range of the call tip text to display in a highlighted style.
        /// </summary>
        /// <param name="hlStart">The zero-based index in the call tip text to start highlighting.</param>
        /// <param name="hlEnd">The zero-based index in the call tip text to stop highlighting (exclusive).</param>
        public unsafe void CallTipSetHlt(int hlStart, int hlEnd)
        {
            // To do the char->byte translation we need to use a cached copy of the last call tip
            hlStart = Helpers.Clamp(hlStart, 0, lastCallTip.Length);
            hlEnd = Helpers.Clamp(hlEnd, 0, lastCallTip.Length);

            fixed (char* cp = lastCallTip)
            {
                hlEnd = Encoding.GetByteCount(cp + hlStart, hlEnd - hlStart);  // The bytes between start and end
                hlStart = Encoding.GetByteCount(cp, hlStart);                  // The bytes between 0 and start
                hlEnd += hlStart;                                              // The bytes between 0 and end
            }

            DirectMessage(NativeMethods.SCI_CALLTIPSETHLT, new IntPtr(hlStart), new IntPtr(hlEnd));
        }

        /// <summary>
        /// Determines whether to display a call tip above or below text.
        /// </summary>
        /// <param name="above">true to display above text; otherwise, false. The default is false.</param>
        public void CallTipSetPosition(bool above)
        {
            var val = (above ? new IntPtr(1) : IntPtr.Zero);
            DirectMessage(NativeMethods.SCI_CALLTIPSETPOSITION, val);
        }

        /// <summary>
        /// Displays a call tip window.
        /// </summary>
        /// <param name="posStart">The zero-based document position where the call tip window should be aligned.</param>
        /// <param name="definition">The call tip text.</param>
        /// <remarks>
        /// Call tips can contain multiple lines separated by '\n' characters. Do not include '\r', as this will most likely print as an empty box.
        /// The '\t' character is supported and the size can be set by using <see cref="CallTipTabSize" />.
        /// </remarks>
        public unsafe void CallTipShow(int posStart, string definition)
        {
            posStart = Helpers.Clamp(posStart, 0, TextLength);
            if (definition == null)
                return;

            lastCallTip = definition;
            posStart = Lines.CharToBytePosition(posStart);
            var bytes = Helpers.GetBytes(definition, Encoding, zeroTerminated: true);
            fixed (byte* bp = bytes)
                DirectMessage(NativeMethods.SCI_CALLTIPSHOW, new IntPtr(posStart), new IntPtr(bp));
        }

        /// <summary>
        /// Sets the call tip tab size in pixels.
        /// </summary>
        /// <param name="tabSize">The width in pixels of a tab '\t' character in a call tip. Specifying 0 disables special treatment of tabs.</param>
        public void CallTipTabSize(int tabSize)
        {
            // To support the STYLE_CALLTIP style we call SCI_CALLTIPUSESTYLE when the control is created. At
            // this point we're only adjusting the tab size. This breaks a bit with Scintilla convention, but
            // that's okay because the Scintilla convention is lame.

            tabSize = Helpers.ClampMin(tabSize, 0);
            DirectMessage(NativeMethods.SCI_CALLTIPUSESTYLE, new IntPtr(tabSize));
        }

        /// <summary>
        /// Indicates to the current <see cref="Lexer" /> that the internal lexer state has changed in the specified
        /// range and therefore may need to be redrawn.
        /// </summary>
        /// <param name="startPos">The zero-based document position at which the lexer state change starts.</param>
        /// <param name="endPos">The zero-based document position at which the lexer state change ends.</param>
        public void ChangeLexerState(int startPos, int endPos)
        {
            var textLength = TextLength;
            startPos = Helpers.Clamp(startPos, 0, textLength);
            endPos = Helpers.Clamp(endPos, 0, textLength);

            startPos = Lines.CharToBytePosition(startPos);
            endPos = Lines.CharToBytePosition(endPos);

            DirectMessage(NativeMethods.SCI_CHANGELEXERSTATE, new IntPtr(startPos), new IntPtr(endPos));
        }

        /// <summary>
        /// Finds the closest character position to the specified display point.
        /// </summary>
        /// <param name="x">The x pixel coordinate within the client rectangle of the control.</param>
        /// <param name="y">The y pixel coordinate within the client rectangle of the control.</param>
        /// <returns>The zero-based document position of the nearest character to the point specified.</returns>
        public int CharPositionFromPoint(int x, int y)
        {
            var pos = DirectMessage(NativeMethods.SCI_CHARPOSITIONFROMPOINT, new IntPtr(x), new IntPtr(y)).ToInt32();
            pos = Lines.ByteToCharPosition(pos);

            return pos;
        }

        /// <summary>
        /// Finds the closest character position to the specified display point or returns -1
        /// if the point is outside the window or not close to any characters.
        /// </summary>
        /// <param name="x">The x pixel coordinate within the client rectangle of the control.</param>
        /// <param name="y">The y pixel coordinate within the client rectangle of the control.</param>
        /// <returns>The zero-based document position of the nearest character to the point specified when near a character; otherwise, -1.</returns>
        public int CharPositionFromPointClose(int x, int y)
        {
            var pos = DirectMessage(NativeMethods.SCI_CHARPOSITIONFROMPOINTCLOSE, new IntPtr(x), new IntPtr(y)).ToInt32();
            if (pos >= 0)
                pos = Lines.ByteToCharPosition(pos);

            return pos;
        }

        /// <summary>
        /// Explicitly sets the current horizontal offset of the caret as the X position to track
        /// when the user moves the caret vertically using the up and down keys.
        /// </summary>
        /// <remarks>
        /// When not set explicitly, Scintilla automatically sets this value each time the user moves
        /// the caret horizontally.
        /// </remarks>
        public void ChooseCaretX()
        {
            DirectMessage(NativeMethods.SCI_CHOOSECARETX);
        }

        /// <summary>
        /// Removes the selected text from the document.
        /// </summary>
        public void Clear()
        {
            DirectMessage(NativeMethods.SCI_CLEAR);
        }

        /// <summary>
        /// Deletes all document text, unless the document is read-only.
        /// </summary>
        public void ClearAll()
        {
            DirectMessage(NativeMethods.SCI_CLEARALL);
        }

        /// <summary>
        /// Makes the specified key definition do nothing.
        /// </summary>
        /// <param name="keyDefinition">The key combination to bind.</param>
        /// <remarks>This is equivalent to binding the keys to <see cref="Command.Null" />.</remarks>
        public void ClearCmdKey(Keys keyDefinition)
        {
            var keys = Helpers.TranslateKeys(keyDefinition);
            DirectMessage(NativeMethods.SCI_CLEARCMDKEY, new IntPtr(keys));
        }

        /// <summary>
        /// Removes all the key definition command mappings.
        /// </summary>
        public void ClearAllCmdKeys()
        {
            DirectMessage(NativeMethods.SCI_CLEARALLCMDKEYS);
        }

        /// <summary>
        /// Removes all styling from the document and resets the folding state.
        /// </summary>
        public void ClearDocumentStyle()
        {
            DirectMessage(NativeMethods.SCI_CLEARDOCUMENTSTYLE);
        }

        /// <summary>
        /// Removes all images registered for autocompletion lists.
        /// </summary>
        public void ClearRegisteredImages()
        {
            DirectMessage(NativeMethods.SCI_CLEARREGISTEREDIMAGES);
        }

        /// <summary>
        /// Sets a single empty selection at the start of the document.
        /// </summary>
        public void ClearSelections()
        {
            DirectMessage(NativeMethods.SCI_CLEARSELECTIONS);
        }

        /// <summary>
        /// Requests that the current lexer restyle the specified range.
        /// </summary>
        /// <param name="startPos">The zero-based document position at which to start styling.</param>
        /// <param name="endPos">The zero-based document position at which to stop styling (exclusive).</param>
        /// <remarks>This will also cause fold levels in the range specified to be reset.</remarks>
        public void Colorize(int startPos, int endPos)
        {
            var textLength = TextLength;
            startPos = Helpers.Clamp(startPos, 0, textLength);
            endPos = Helpers.Clamp(endPos, 0, textLength);

            startPos = Lines.CharToBytePosition(startPos);
            endPos = Lines.CharToBytePosition(endPos);

            DirectMessage(NativeMethods.SCI_COLOURISE, new IntPtr(startPos), new IntPtr(endPos));
        }

        /// <summary>
        /// Changes all end-of-line characters in the document to the format specified.
        /// </summary>
        /// <param name="eolMode">One of the <see cref="Eol" /> enumeration values.</param>
        public void ConvertEols(Eol eolMode)
        {
            var eol = (int)eolMode;
            DirectMessage(NativeMethods.SCI_CONVERTEOLS, new IntPtr(eol));
        }

        /// <summary>
        /// Copies the selected text from the document and places it on the clipboard.
        /// </summary>
        public void Copy()
        {
            DirectMessage(NativeMethods.SCI_COPY);
        }

        /// <summary>
        /// Copies the selected text from the document and places it on the clipboard.
        /// </summary>
        /// <param name="format">One of the <see cref="CopyFormat" /> enumeration values.</param>
        public void Copy(CopyFormat format)
        {
            Helpers.Copy(this, format, true, false, 0, 0);
        }

        /// <summary>
        /// Copies the selected text from the document and places it on the clipboard.
        /// If the selection is empty the current line is copied.
        /// </summary>
        /// <remarks>
        /// If the selection is empty and the current line copied, an extra "MSDEVLineSelect" marker is added to the
        /// clipboard which is then used in <see cref="Paste" /> to paste the whole line before the current line.
        /// </remarks>
        public void CopyAllowLine()
        {
            DirectMessage(NativeMethods.SCI_COPYALLOWLINE);
        }

        /// <summary>
        /// Copies the selected text from the document and places it on the clipboard.
        /// If the selection is empty the current line is copied.
        /// </summary>
        /// <param name="format">One of the <see cref="CopyFormat" /> enumeration values.</param>
        /// <remarks>
        /// If the selection is empty and the current line copied, an extra "MSDEVLineSelect" marker is added to the
        /// clipboard which is then used in <see cref="Paste" /> to paste the whole line before the current line.
        /// </remarks>
        public void CopyAllowLine(CopyFormat format)
        {
            Helpers.Copy(this, format, true, true, 0, 0);
        }

        /// <summary>
        /// Copies the specified range of text to the clipboard.
        /// </summary>
        /// <param name="start">The zero-based character position in the document to start copying.</param>
        /// <param name="end">The zero-based character position (exclusive) in the document to stop copying.</param>
        public void CopyRange(int start, int end)
        {
            var textLength = TextLength;
            start = Helpers.Clamp(start, 0, textLength);
            end = Helpers.Clamp(end, 0, textLength);

            // Convert to byte positions
            start = Lines.CharToBytePosition(start);
            end = Lines.CharToBytePosition(end);

            DirectMessage(NativeMethods.SCI_COPYRANGE, new IntPtr(start), new IntPtr(end));
        }

        /// <summary>
        /// Copies the specified range of text to the clipboard.
        /// </summary>
        /// <param name="start">The zero-based character position in the document to start copying.</param>
        /// <param name="end">The zero-based character position (exclusive) in the document to stop copying.</param>
        /// <param name="format">One of the <see cref="CopyFormat" /> enumeration values.</param>
        public void CopyRange(int start, int end, CopyFormat format)
        {
            var textLength = TextLength;
            start = Helpers.Clamp(start, 0, textLength);
            end = Helpers.Clamp(end, 0, textLength);
            if (start == end)
                return;

            // Convert to byte positions
            start = Lines.CharToBytePosition(start);
            end = Lines.CharToBytePosition(end);

            Helpers.Copy(this, format, false, false, start, end);
        }

        /// <summary>
        /// Create a new, empty document.
        /// </summary>
        /// <returns>A new <see cref="Document" /> with a reference count of 1.</returns>
        /// <remarks>You are responsible for ensuring the reference count eventually reaches 0 or memory leaks will occur.</remarks>
        public Document CreateDocument()
        {
            var ptr = DirectMessage(NativeMethods.SCI_CREATEDOCUMENT);
            return new Document { Value = ptr };
        }

        /// <summary>
        /// Creates an <see cref="ILoader" /> object capable of loading a <see cref="Document" /> on a background (non-UI) thread.
        /// </summary>
        /// <param name="length">The initial number of characters to allocate.</param>
        /// <returns>A new <see cref="ILoader" /> object, or null if the loader could not be created.</returns>
        public ILoader CreateLoader(int length)
        {
            length = Helpers.ClampMin(length, 0);
            var ptr = DirectMessage(NativeMethods.SCI_CREATELOADER, new IntPtr(length));
            if (ptr == IntPtr.Zero)
                return null;

            return new Loader(ptr, Encoding);
        }

        /// <summary>
        /// Cuts the selected text from the document and places it on the clipboard.
        /// </summary>
        public void Cut()
        {
            DirectMessage(NativeMethods.SCI_CUT);
        }

        /// <summary>
        /// Deletes a range of text from the document.
        /// </summary>
        /// <param name="position">The zero-based character position to start deleting.</param>
        /// <param name="length">The number of characters to delete.</param>
        public void DeleteRange(int position, int length)
        {
            var textLength = TextLength;
            position = Helpers.Clamp(position, 0, textLength);
            length = Helpers.Clamp(length, 0, textLength - position);

            // Convert to byte position/length
            var byteStartPos = Lines.CharToBytePosition(position);
            var byteEndPos = Lines.CharToBytePosition(position + length);

            DirectMessage(NativeMethods.SCI_DELETERANGE, new IntPtr(byteStartPos), new IntPtr(byteEndPos - byteStartPos));
        }

        /// <summary>
        /// Retrieves a description of keyword sets supported by the current <see cref="Lexer" />.
        /// </summary>
        /// <returns>A String describing each keyword set separated by line breaks for the current lexer.</returns>
        public unsafe string DescribeKeywordSets()
        {
            var length = DirectMessage(NativeMethods.SCI_DESCRIBEKEYWORDSETS).ToInt32();
            var bytes = new byte[length + 1];

            fixed (byte* bp = bytes)
                DirectMessage(NativeMethods.SCI_DESCRIBEKEYWORDSETS, IntPtr.Zero, new IntPtr(bp));

            var str = Encoding.ASCII.GetString(bytes, 0, length);
            return str;
        }

        /// <summary>
        /// Retrieves a brief description of the specified property name for the current <see cref="Lexer" />.
        /// </summary>
        /// <param name="name">A property name supported by the current <see cref="Lexer" />.</param>
        /// <returns>A String describing the lexer property name if found; otherwise, String.Empty.</returns>
        /// <remarks>A list of supported property names for the current <see cref="Lexer" /> can be obtained by calling <see cref="PropertyNames" />.</remarks>
        public unsafe string DescribeProperty(string name)
        {
            if (String.IsNullOrEmpty(name))
                return String.Empty;

            var nameBytes = Helpers.GetBytes(name, Encoding.ASCII, zeroTerminated: true);
            fixed (byte* nb = nameBytes)
            {
                var length = DirectMessage(NativeMethods.SCI_DESCRIBEPROPERTY, new IntPtr(nb), IntPtr.Zero).ToInt32();
                if (length == 0)
                    return string.Empty;

                var descriptionBytes = new byte[length + 1];
                fixed (byte* db = descriptionBytes)
                {
                    DirectMessage(NativeMethods.SCI_DESCRIBEPROPERTY, new IntPtr(nb), new IntPtr(db));
                    return Helpers.GetString(new IntPtr(db), length, Encoding.ASCII);
                }
            }
        }

        internal IntPtr DirectMessage(int msg)
        {
            return DirectMessage(msg, IntPtr.Zero, IntPtr.Zero);
        }

        internal IntPtr DirectMessage(int msg, IntPtr wParam)
        {
            return DirectMessage(msg, wParam, IntPtr.Zero);
        }

        /// <summary>
        /// Sends the specified message directly to the native Scintilla window,
        /// bypassing any managed APIs.
        /// </summary>
        /// <param name="msg">The message ID.</param>
        /// <param name="wParam">The message <c>wparam</c> field.</param>
        /// <param name="lParam">The message <c>lparam</c> field.</param>
        /// <returns>An <see cref="IntPtr"/> representing the result of the message request.</returns>
        /// <remarks>This API supports the Scintilla infrastructure and is not intended to be used directly from your code.</remarks>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public virtual IntPtr DirectMessage(int msg, IntPtr wParam, IntPtr lParam)
        {
            // If the control handle, ptr, direct function, etc... hasn't been created yet, it will be now.
            var result = DirectMessage(SciPointer, msg, wParam, lParam);
            return result;
        }

        private static IntPtr DirectMessage(IntPtr sciPtr, int msg, IntPtr wParam, IntPtr lParam)
        {
            // Like Win32 SendMessage but directly to Scintilla
            var result = directFunction(sciPtr, msg, wParam, lParam);
            return result;
        }

        /// <summary>
        /// Releases the unmanaged resources used by the Control and its child controls and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // WM_DESTROY workaround
                if (reparent)
                {
                    reparent = false;
                    if (IsHandleCreated)
                        DestroyHandle();
                }

                if (fillUpChars != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(fillUpChars);
                    fillUpChars = IntPtr.Zero;
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Returns the zero-based document line index from the specified display line index.
        /// </summary>
        /// <param name="displayLine">The zero-based display line index.</param>
        /// <returns>The zero-based document line index.</returns>
        /// <seealso cref="Line.DisplayIndex" />
        public int DocLineFromVisible(int displayLine)
        {
            displayLine = Helpers.Clamp(displayLine, 0, Lines.Count);
            return DirectMessage(NativeMethods.SCI_DOCLINEFROMVISIBLE, new IntPtr(displayLine)).ToInt32();
        }

        /// <summary>
        /// If there are multiple selections, removes the specified selection.
        /// </summary>
        /// <param name="selection">The zero-based selection index.</param>
        /// <seealso cref="Selections" />
        public void DropSelection(int selection)
        {
            selection = Helpers.ClampMin(selection, 0);
            DirectMessage(NativeMethods.SCI_DROPSELECTIONN, new IntPtr(selection));
        }

        /// <summary>
        /// Clears any undo or redo history.
        /// </summary>
        /// <remarks>This will also cause <see cref="SetSavePoint" /> to be called but will not raise the <see cref="SavePointReached" /> event.</remarks>
        public void EmptyUndoBuffer()
        {
            DirectMessage(NativeMethods.SCI_EMPTYUNDOBUFFER);
        }

        /// <summary>
        /// Marks the end of a set of actions that should be treated as a single undo action.
        /// </summary>
        /// <seealso cref="BeginUndoAction" />
        public void EndUndoAction()
        {
            DirectMessage(NativeMethods.SCI_ENDUNDOACTION);
        }

        /// <summary>
        /// Performs the specified <see cref="Scintilla" />command.
        /// </summary>
        /// <param name="sciCommand">The command to perform.</param>
        public void ExecuteCmd(Command sciCommand)
        {
            var cmd = (int)sciCommand;
            DirectMessage(cmd);
        }

        /// <summary>
        /// Performs the specified fold action on the entire document.
        /// </summary>
        /// <param name="action">One of the <see cref="FoldAction" /> enumeration values.</param>
        /// <remarks>When using <see cref="FoldAction.Toggle" /> the first fold header in the document is examined to decide whether to expand or contract.</remarks>
        public void FoldAll(FoldAction action)
        {
            DirectMessage(NativeMethods.SCI_FOLDALL, new IntPtr((int)action));
        }

        /// <summary>
        /// Changes the appearance of fold text tags.
        /// </summary>
        /// <param name="style">One of the <see cref="FoldDisplayText" /> enumeration values.</param>
        /// <remarks>The text tag to display on a folded line can be set using <see cref="Line.ToggleFoldShowText" />.</remarks>
        /// <seealso cref="Line.ToggleFoldShowText" />.
        public void FoldDisplayTextSetStyle(FoldDisplayText style)
        {
            DirectMessage(NativeMethods.SCI_FOLDDISPLAYTEXTSETSTYLE, new IntPtr((int)style));
        }

        /// <summary>
        /// Returns the character as the specified document position.
        /// </summary>
        /// <param name="position">The zero-based document position of the character to get.</param>
        /// <returns>The character at the specified <paramref name="position" />.</returns>
        public unsafe int GetCharAt(int position)
        {
            position = Helpers.Clamp(position, 0, TextLength);
            position = Lines.CharToBytePosition(position);

            var nextPosition = DirectMessage(NativeMethods.SCI_POSITIONRELATIVE, new IntPtr(position), new IntPtr(1)).ToInt32();
            var length = (nextPosition - position);
            if (length <= 1)
            {
                // Position is at single-byte character
                return DirectMessage(NativeMethods.SCI_GETCHARAT, new IntPtr(position)).ToInt32();
            }

            // Position is at multibyte character
            var bytes = new byte[length + 1];
            fixed (byte* bp = bytes)
            {
                NativeMethods.Sci_TextRange* range = stackalloc NativeMethods.Sci_TextRange[1];
                range->chrg.cpMin = position;
                range->chrg.cpMax = nextPosition;
                range->lpstrText = new IntPtr(bp);

                DirectMessage(NativeMethods.SCI_GETTEXTRANGE, IntPtr.Zero, new IntPtr(range));
                var str = Helpers.GetString(new IntPtr(bp), length, Encoding);
                return str[0];
            }
        }

        /// <summary>
        /// Returns the column number of the specified document position, taking the width of tabs into account.
        /// </summary>
        /// <param name="position">The zero-based document position to get the column for.</param>
        /// <returns>The number of columns from the start of the line to the specified document <paramref name="position" />.</returns>
        public int GetColumn(int position)
        {
            position = Helpers.Clamp(position, 0, TextLength);
            position = Lines.CharToBytePosition(position);
            return DirectMessage(NativeMethods.SCI_GETCOLUMN, new IntPtr(position)).ToInt32();
        }

        /// <summary>
        /// Returns the last document position likely to be styled correctly.
        /// </summary>
        /// <returns>The zero-based document position of the last styled character.</returns>
        public int GetEndStyled()
        {
            var pos = DirectMessage(NativeMethods.SCI_GETENDSTYLED).ToInt32();
            return Lines.ByteToCharPosition(pos);
        }

        private static string GetModulePath()
        {
            // UI thread...
            if (modulePath == null)
            {
                // Extract the embedded SciLexer DLL
                // http://stackoverflow.com/a/768429/2073621
                var version = typeof(ZeroitCodeExplorer).Assembly.GetName().Version.ToString(3);
                modulePath = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.GetTempPath(), @"Zeroit.Framework.CodeBox"), version), (IntPtr.Size == 4 ? "x86" : "x64")), "SciLexer.dll");

                if (!File.Exists(modulePath))
                {
                    // http://stackoverflow.com/a/229567/2073621
                    // Synchronize access to the file across processes
                    var guid = ((GuidAttribute)typeof(ZeroitCodeExplorer).Assembly.GetCustomAttributes(typeof(GuidAttribute), false).GetValue(0)).Value.ToString();
                    var name = string.Format(CultureInfo.InvariantCulture, "Global\\{{{0}}}", guid);
                    using (var mutex = new Mutex(false, name))
                    {
                        var access = new MutexAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), MutexRights.FullControl, AccessControlType.Allow);
                        var security = new MutexSecurity();
                        security.AddAccessRule(access);
                        mutex.SetAccessControl(security);

                        var ownsHandle = false;
                        try
                        {
                            try
                            {
                                ownsHandle = mutex.WaitOne(5000, false); // 5 sec
                                if (ownsHandle == false)
                                {
                                    var timeoutMessage = string.Format(CultureInfo.InvariantCulture, "Timeout waiting for exclusive access to '{0}'.", modulePath);
                                    throw new TimeoutException(timeoutMessage);
                                }
                            }
                            catch (AbandonedMutexException)
                            {
                                // Previous process terminated abnormally
                                ownsHandle = true;
                            }

                            // Double-checked (process) lock
                            if (!File.Exists(modulePath))
                            {
                                // Write the embedded file to disk
                                var directory = Path.GetDirectoryName(modulePath);
                                if (!Directory.Exists(directory))
                                    Directory.CreateDirectory(directory);

                                var resource = string.Format(CultureInfo.InvariantCulture, "Zeroit.Framework.CodeBox.{0}.SciLexer.dll.gz", (IntPtr.Size == 4 ? "x86" : "x64"));
                                using (var resourceStream = typeof(ZeroitCodeExplorer).Assembly.GetManifestResourceStream(resource))
                                using (var gzipStream = new GZipStream(resourceStream, CompressionMode.Decompress))
                                using (var fileStream = File.Create(modulePath))
                                    gzipStream.CopyTo(fileStream);
                            }
                        }
                        finally
                        {
                            if (ownsHandle)
                                mutex.ReleaseMutex();
                        }
                    }
                }
            }

            return modulePath;
        }

        /// <summary>
        /// Lookup a property value for the current <see cref="Lexer" />.
        /// </summary>
        /// <param name="name">The property name to lookup.</param>
        /// <returns>
        /// A String representing the property value if found; otherwise, String.Empty.
        /// Any embedded property name macros as described in <see cref="SetProperty" /> will not be replaced (expanded).
        /// </returns>
        /// <seealso cref="GetPropertyExpanded" />
        public unsafe string GetProperty(string name)
        {
            if (String.IsNullOrEmpty(name))
                return String.Empty;

            var nameBytes = Helpers.GetBytes(name, Encoding.ASCII, zeroTerminated: true);
            fixed (byte* nb = nameBytes)
            {
                var length = DirectMessage(NativeMethods.SCI_GETPROPERTY, new IntPtr(nb)).ToInt32();
                if (length == 0)
                    return String.Empty;

                var valueBytes = new byte[length + 1];
                fixed (byte* vb = valueBytes)
                {
                    DirectMessage(NativeMethods.SCI_GETPROPERTY, new IntPtr(nb), new IntPtr(vb));
                    return Helpers.GetString(new IntPtr(vb), length, Encoding.ASCII);
                }
            }
        }

        /// <summary>
        /// Lookup a property value for the current <see cref="Lexer" /> and expand any embedded property macros.
        /// </summary>
        /// <param name="name">The property name to lookup.</param>
        /// <returns>
        /// A String representing the property value if found; otherwise, String.Empty.
        /// Any embedded property name macros as described in <see cref="SetProperty" /> will be replaced (expanded).
        /// </returns>
        /// <seealso cref="GetProperty" />
        public unsafe string GetPropertyExpanded(string name)
        {
            if (String.IsNullOrEmpty(name))
                return String.Empty;

            var nameBytes = Helpers.GetBytes(name, Encoding.ASCII, zeroTerminated: true);
            fixed (byte* nb = nameBytes)
            {
                var length = DirectMessage(NativeMethods.SCI_GETPROPERTYEXPANDED, new IntPtr(nb)).ToInt32();
                if (length == 0)
                    return String.Empty;

                var valueBytes = new byte[length + 1];
                fixed (byte* vb = valueBytes)
                {
                    DirectMessage(NativeMethods.SCI_GETPROPERTYEXPANDED, new IntPtr(nb), new IntPtr(vb));
                    return Helpers.GetString(new IntPtr(vb), length, Encoding.ASCII);
                }
            }
        }

        /// <summary>
        /// Lookup a property value for the current <see cref="Lexer" /> and convert it to an integer.
        /// </summary>
        /// <param name="name">The property name to lookup.</param>
        /// <param name="defaultValue">A default value to return if the property name is not found or has no value.</param>
        /// <returns>
        /// An Integer representing the property value if found;
        /// otherwise, <paramref name="defaultValue" /> if not found or the property has no value;
        /// otherwise, 0 if the property is not a number.
        /// </returns>
        public unsafe int GetPropertyInt(string name, int defaultValue)
        {
            if (String.IsNullOrEmpty(name))
                return defaultValue;

            var bytes = Helpers.GetBytes(name, Encoding.ASCII, zeroTerminated: true);
            fixed (byte* bp = bytes)
                return DirectMessage(NativeMethods.SCI_GETPROPERTYINT, new IntPtr(bp), new IntPtr(defaultValue)).ToInt32();
        }

        /// <summary>
        /// Gets the style of the specified document position.
        /// </summary>
        /// <param name="position">The zero-based document position of the character to get the style for.</param>
        /// <returns>The zero-based <see cref="Style" /> index used at the specified <paramref name="position" />.</returns>
        public int GetStyleAt(int position)
        {
            position = Helpers.Clamp(position, 0, TextLength);
            position = Lines.CharToBytePosition(position);

            return DirectMessage(NativeMethods.SCI_GETSTYLEAT, new IntPtr(position)).ToInt32();
        }

        /// <summary>
        /// Returns the capture group text of the most recent regular expression search.
        /// </summary>
        /// <param name="tagNumber">The capture group (1 through 9) to get the text for.</param>
        /// <returns>A String containing the capture group text if it participated in the match; otherwise, an empty string.</returns>
        /// <seealso cref="SearchInTarget" />
        public unsafe string GetTag(int tagNumber)
        {
            tagNumber = Helpers.Clamp(tagNumber, 1, 9);
            var length = DirectMessage(NativeMethods.SCI_GETTAG, new IntPtr(tagNumber), IntPtr.Zero).ToInt32();
            if (length <= 0)
                return string.Empty;

            var bytes = new byte[length + 1];
            fixed (byte* bp = bytes)
            {
                DirectMessage(NativeMethods.SCI_GETTAG, new IntPtr(tagNumber), new IntPtr(bp));
                return Helpers.GetString(new IntPtr(bp), length, Encoding);
            }
        }

        /// <summary>
        /// Gets a range of text from the document.
        /// </summary>
        /// <param name="position">The zero-based starting character position of the range to get.</param>
        /// <param name="length">The number of characters to get.</param>
        /// <returns>A string representing the text range.</returns>
        public unsafe string GetTextRange(int position, int length)
        {
            var textLength = TextLength;
            position = Helpers.Clamp(position, 0, textLength);
            length = Helpers.Clamp(length, 0, textLength - position);

            // Convert to byte position/length
            var byteStartPos = Lines.CharToBytePosition(position);
            var byteEndPos = Lines.CharToBytePosition(position + length);

            var ptr = DirectMessage(NativeMethods.SCI_GETRANGEPOINTER, new IntPtr(byteStartPos), new IntPtr(byteEndPos - byteStartPos));
            if (ptr == IntPtr.Zero)
                return string.Empty;

            return Helpers.GetString(ptr, (byteEndPos - byteStartPos), Encoding);
        }

        /// <summary>
        /// Gets a range of text from the document formatted as Hypertext Markup Language (HTML).
        /// </summary>
        /// <param name="position">The zero-based starting character position of the range to get.</param>
        /// <param name="length">The number of characters to get.</param>
        /// <returns>A string representing the text range formatted as HTML.</returns>
        public string GetTextRangeAsHtml(int position, int length)
        {
            var textLength = TextLength;
            position = Helpers.Clamp(position, 0, textLength);
            length = Helpers.Clamp(length, 0, textLength - position);

            var startBytePos = Lines.CharToBytePosition(position);
            var endBytePos = Lines.CharToBytePosition(position + length);

            return Helpers.GetHtml(this, startBytePos, endBytePos);
        }

        /// <summary>
        /// Returns the version information of the native Scintilla library.
        /// </summary>
        /// <returns>An object representing the version information of the native Scintilla library.</returns>
        public FileVersionInfo GetVersionInfo()
        {
            var path = GetModulePath();
            var version = FileVersionInfo.GetVersionInfo(path);

            return version;
        }

        ///<summary>
        /// Gets the word from the position specified.
        /// </summary>
        /// <param name="position">The zero-based document character position to get the word from.</param>
        /// <returns>The word at the specified position.</returns>
        public string GetWordFromPosition(int position)
        {
            int startPosition = WordStartPosition(position, true);
            int endPosition = WordEndPosition(position, true);
            return GetTextRange(startPosition, endPosition - startPosition);
        }

        /// <summary>
        /// Navigates the caret to the document position specified.
        /// </summary>
        /// <param name="position">The zero-based document character position to navigate to.</param>
        /// <remarks>Any selection is discarded.</remarks>
        public void GotoPosition(int position)
        {
            position = Helpers.Clamp(position, 0, TextLength);
            position = Lines.CharToBytePosition(position);
            DirectMessage(NativeMethods.SCI_GOTOPOS, new IntPtr(position));
        }

        /// <summary>
        /// Hides the range of lines specified.
        /// </summary>
        /// <param name="lineStart">The zero-based index of the line range to start hiding.</param>
        /// <param name="lineEnd">The zero-based index of the line range to end hiding.</param>
        /// <seealso cref="ShowLines" />
        /// <seealso cref="Line.Visible" />
        public void HideLines(int lineStart, int lineEnd)
        {
            lineStart = Helpers.Clamp(lineStart, 0, Lines.Count);
            lineEnd = Helpers.Clamp(lineEnd, lineStart, Lines.Count);

            DirectMessage(NativeMethods.SCI_HIDELINES, new IntPtr(lineStart), new IntPtr(lineEnd));
        }

        /// <summary>
        /// Returns a bitmap representing the 32 indicators in use at the specified position.
        /// </summary>
        /// <param name="position">The zero-based character position within the document to test.</param>
        /// <returns>A bitmap indicating which of the 32 indicators are in use at the specified <paramref name="position" />.</returns>
        public uint IndicatorAllOnFor(int position)
        {
            position = Helpers.Clamp(position, 0, TextLength);
            position = Lines.CharToBytePosition(position);

            var bitmap = DirectMessage(NativeMethods.SCI_INDICATORALLONFOR, new IntPtr(position)).ToInt32();
            return unchecked((uint)bitmap);
        }

        /// <summary>
        /// Removes the <see cref="IndicatorCurrent" /> indicator (and user-defined value) from the specified range of text.
        /// </summary>
        /// <param name="position">The zero-based character position within the document to start clearing.</param>
        /// <param name="length">The number of characters to clear.</param>
        public void IndicatorClearRange(int position, int length)
        {
            var textLength = TextLength;
            position = Helpers.Clamp(position, 0, textLength);
            length = Helpers.Clamp(length, 0, textLength - position);

            var startPos = Lines.CharToBytePosition(position);
            var endPos = Lines.CharToBytePosition(position + length);

            DirectMessage(NativeMethods.SCI_INDICATORCLEARRANGE, new IntPtr(startPos), new IntPtr(endPos - startPos));
        }

        /// <summary>
        /// Adds the <see cref="IndicatorCurrent" /> indicator and <see cref="IndicatorValue" /> value to the specified range of text.
        /// </summary>
        /// <param name="position">The zero-based character position within the document to start filling.</param>
        /// <param name="length">The number of characters to fill.</param>
        public void IndicatorFillRange(int position, int length)
        {
            var textLength = TextLength;
            position = Helpers.Clamp(position, 0, textLength);
            length = Helpers.Clamp(length, 0, textLength - position);

            var startPos = Lines.CharToBytePosition(position);
            var endPos = Lines.CharToBytePosition(position + length);

            DirectMessage(NativeMethods.SCI_INDICATORFILLRANGE, new IntPtr(startPos), new IntPtr(endPos - startPos));
        }

        private void InitDocument(Eol eolMode = Eol.CrLf, bool useTabs = false, int tabWidth = 4, int indentWidth = 0)
        {
            // Document.h
            // These properties are stored in the Scintilla document, not the control; meaning, when
            // a user changes documents these properties will change. If the user changes to a new
            // document, these properties will reset to defaults. That can cause confusion for our users
            // who would expect their tab settings, for example, to be unchanged based on which document
            // they have selected into the control. This is where we carry forward any of the user's
            // current settings -- and our default overrides -- to a new document.

            DirectMessage(NativeMethods.SCI_SETCODEPAGE, new IntPtr(NativeMethods.SC_CP_UTF8));
            DirectMessage(NativeMethods.SCI_SETUNDOCOLLECTION, new IntPtr(1));
            DirectMessage(NativeMethods.SCI_SETEOLMODE, new IntPtr((int)eolMode));
            DirectMessage(NativeMethods.SCI_SETUSETABS, useTabs ? new IntPtr(1) : IntPtr.Zero);
            DirectMessage(NativeMethods.SCI_SETTABWIDTH, new IntPtr(tabWidth));
            DirectMessage(NativeMethods.SCI_SETINDENT, new IntPtr(indentWidth));
        }

        /// <summary>
        /// Inserts text at the specified position.
        /// </summary>
        /// <param name="position">The zero-based character position to insert the text. Specify -1 to use the current caret position.</param>
        /// <param name="text">The text to insert into the document.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="position" /> less than zero and not equal to -1. -or-
        /// <paramref name="position" /> is greater than the document length.
        /// </exception>
        /// <remarks>No scrolling is performed.</remarks>
        public unsafe void InsertText(int position, string text)
        {
            if (position < -1)
                throw new ArgumentOutOfRangeException("position", "Position must be greater or equal to zero, or -1.");

            if (position != -1)
            {
                var textLength = TextLength;
                if (position > textLength)
                    throw new ArgumentOutOfRangeException("position", "Position cannot exceed document length.");

                position = Lines.CharToBytePosition(position);
            }

            fixed (byte* bp = Helpers.GetBytes(text ?? string.Empty, Encoding, zeroTerminated: true))
                DirectMessage(NativeMethods.SCI_INSERTTEXT, new IntPtr(position), new IntPtr(bp));
        }

        /// <summary>
        /// Determines whether the specified <paramref name="start" /> and <paramref name="end" /> positions are
        /// at the beginning and end of a word, respectively.
        /// </summary>
        /// <param name="start">The zero-based document position of the possible word start.</param>
        /// <param name="end">The zero-based document position of the possible word end.</param>
        /// <returns>
        /// true if <paramref name="start" /> and <paramref name="end" /> are at the beginning and end of a word, respectively;
        /// otherwise, false.
        /// </returns>
        /// <remarks>
        /// This method does not check whether there is whitespace in the search range,
        /// only that the <paramref name="start" /> and <paramref name="end" /> are at word boundaries.
        /// </remarks>
        public bool IsRangeWord(int start, int end)
        {
            var textLength = TextLength;
            start = Helpers.Clamp(start, 0, textLength);
            end = Helpers.Clamp(end, 0, textLength);

            start = Lines.CharToBytePosition(start);
            end = Lines.CharToBytePosition(end);

            return (DirectMessage(NativeMethods.SCI_ISRANGEWORD, new IntPtr(start), new IntPtr(end)) != IntPtr.Zero);
        }

        /// <summary>
        /// Returns the line that contains the document position specified.
        /// </summary>
        /// <param name="position">The zero-based document character position.</param>
        /// <returns>The zero-based document line index containing the character <paramref name="position" />.</returns>
        public int LineFromPosition(int position)
        {
            position = Helpers.Clamp(position, 0, TextLength);
            return Lines.LineFromCharPosition(position);
        }

        /// <summary>
        /// Scrolls the display the number of lines and columns specified.
        /// </summary>
        /// <param name="lines">The number of lines to scroll.</param>
        /// <param name="columns">The number of columns to scroll.</param>
        /// <remarks>
        /// Negative values scroll in the opposite direction.
        /// A column is the width in pixels of a space character in the <see cref="Style.Default" /> style.
        /// </remarks>
        public void LineScroll(int lines, int columns)
        {
            DirectMessage(NativeMethods.SCI_LINESCROLL, new IntPtr(columns), new IntPtr(lines));
        }

        /// <summary>
        /// Loads a <see cref="Scintilla" /> compatible lexer from an external DLL.
        /// </summary>
        /// <param name="path">The path to the external lexer DLL.</param>
        public unsafe void LoadLexerLibrary(string path)
        {
            if (String.IsNullOrEmpty(path))
                return;

            var bytes = Helpers.GetBytes(path, Encoding.Default, zeroTerminated: true);
            fixed (byte* bp = bytes)
                DirectMessage(NativeMethods.SCI_LOADLEXERLIBRARY, IntPtr.Zero, new IntPtr(bp));
        }

        /// <summary>
        /// Removes the specified marker from all lines.
        /// </summary>
        /// <param name="marker">The zero-based <see cref="Marker" /> index to remove from all lines, or -1 to remove all markers from all lines.</param>
        public void MarkerDeleteAll(int marker)
        {
            marker = Helpers.Clamp(marker, -1, Markers.Count - 1);
            DirectMessage(NativeMethods.SCI_MARKERDELETEALL, new IntPtr(marker));
        }

        /// <summary>
        /// Searches the document for the marker handle and deletes the marker if found.
        /// </summary>
        /// <param name="markerHandle">The <see cref="MarkerHandle" /> created by a previous call to <see cref="Line.MarkerAdd" /> of the marker to delete.</param>
        public void MarkerDeleteHandle(MarkerHandle markerHandle)
        {
            DirectMessage(NativeMethods.SCI_MARKERDELETEHANDLE, markerHandle.Value);
        }

        /// <summary>
        /// Enable or disable highlighting of the current folding block.
        /// </summary>
        /// <param name="enabled">true to highlight the current folding block; otherwise, false.</param>
        public void MarkerEnableHighlight(bool enabled)
        {
            var val = (enabled ? new IntPtr(1) : IntPtr.Zero);
            DirectMessage(NativeMethods.SCI_MARKERENABLEHIGHLIGHT, val);
        }

        /// <summary>
        /// Searches the document for the marker handle and returns the line number containing the marker if found.
        /// </summary>
        /// <param name="markerHandle">The <see cref="MarkerHandle" /> created by a previous call to <see cref="Line.MarkerAdd" /> of the marker to search for.</param>
        /// <returns>If found, the zero-based line index containing the marker; otherwise, -1.</returns>
        public int MarkerLineFromHandle(MarkerHandle markerHandle)
        {
            return DirectMessage(NativeMethods.SCI_MARKERLINEFROMHANDLE, markerHandle.Value).ToInt32();
        }

        /// <summary>
        /// Specifies the long line indicator column number and color when <see cref="EdgeMode" /> is <see cref="EdgeMode.MultiLine" />.
        /// </summary>
        /// <param name="column">The zero-based column number to indicate.</param>
        /// <param name="edgeColor">The color of the vertical long line indicator.</param>
        /// <remarks>A column is defined as the width of a space character in the <see cref="Style.Default" /> style.</remarks>
        /// <seealso cref="MultiEdgeClearAll" />
        public void MultiEdgeAddLine(int column, Color edgeColor)
        {
            column = Helpers.ClampMin(column, 0);
            var colour = ColorTranslator.ToWin32(edgeColor);

            DirectMessage(NativeMethods.SCI_MULTIEDGEADDLINE, new IntPtr(column), new IntPtr(colour));
        }

        /// <summary>
        /// Removes all the long line column indicators specified using <seealso cref="MultiEdgeAddLine" />.
        /// </summary>
        /// <seealso cref="MultiEdgeAddLine" />
        public void MultiEdgeClearAll()
        {
            DirectMessage(NativeMethods.SCI_MULTIEDGECLEARALL);
        }

        /// <summary>
        /// Searches for all instances of the main selection within the <see cref="TargetStart" /> and <see cref="TargetEnd" />
        /// range and adds any matches to the selection.
        /// </summary>
        /// <remarks>
        /// The <see cref="SearchFlags" /> property is respected when searching, allowing additional
        /// selections to match on different case sensitivity and word search options.
        /// </remarks>
        /// <seealso cref="MultipleSelectAddNext" />
        public void MultipleSelectAddEach()
        {
            DirectMessage(NativeMethods.SCI_MULTIPLESELECTADDEACH);
        }

        /// <summary>
        /// Searches for the next instance of the main selection within the <see cref="TargetStart" /> and <see cref="TargetEnd" />
        /// range and adds any match to the selection.
        /// </summary>
        /// <remarks>
        /// The <see cref="SearchFlags" /> property is respected when searching, allowing additional
        /// selections to match on different case sensitivity and word search options.
        /// </remarks>
        /// <seealso cref="MultipleSelectAddNext" />
        public void MultipleSelectAddNext()
        {
            DirectMessage(NativeMethods.SCI_MULTIPLESELECTADDNEXT);
        }

        /// <summary>
        /// Raises the <see cref="AutoCCancelled" /> event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnAutoCCancelled(EventArgs e)
        {
            var handler = Events[autoCCancelledEventKey] as EventHandler<EventArgs>;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Raises the <see cref="AutoCCharDeleted" /> event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnAutoCCharDeleted(EventArgs e)
        {
            var handler = Events[autoCCharDeletedEventKey] as EventHandler<EventArgs>;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Raises the <see cref="AutoCCompleted" /> event.
        /// </summary>
        /// <param name="e">An <see cref="AutoCSelectionEventArgs" /> that contains the event data.</param>
        protected virtual void OnAutoCCompleted(AutoCSelectionEventArgs e)
        {
            var handler = Events[autoCCompletedEventKey] as EventHandler<AutoCSelectionEventArgs>;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Raises the <see cref="AutoCSelection" /> event.
        /// </summary>
        /// <param name="e">An <see cref="AutoCSelectionEventArgs" /> that contains the event data.</param>
        protected virtual void OnAutoCSelection(AutoCSelectionEventArgs e)
        {
            var handler = Events[autoCSelectionEventKey] as EventHandler<AutoCSelectionEventArgs>;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Raises the <see cref="BeforeDelete" /> event.
        /// </summary>
        /// <param name="e">A <see cref="BeforeModificationEventArgs" /> that contains the event data.</param>
        protected virtual void OnBeforeDelete(BeforeModificationEventArgs e)
        {
            var handler = Events[beforeDeleteEventKey] as EventHandler<BeforeModificationEventArgs>;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Raises the <see cref="BeforeInsert" /> event.
        /// </summary>
        /// <param name="e">A <see cref="BeforeModificationEventArgs" /> that contains the event data.</param>
        protected virtual void OnBeforeInsert(BeforeModificationEventArgs e)
        {
            var handler = Events[beforeInsertEventKey] as EventHandler<BeforeModificationEventArgs>;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Raises the <see cref="BorderStyleChanged" /> event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnBorderStyleChanged(EventArgs e)
        {
            var handler = Events[borderStyleChangedEventKey] as EventHandler;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Raises the <see cref="ChangeAnnotation" /> event.
        /// </summary>
        /// <param name="e">A <see cref="ChangeAnnotationEventArgs" /> that contains the event data.</param>
        protected virtual void OnChangeAnnotation(ChangeAnnotationEventArgs e)
        {
            var handler = Events[changeAnnotationEventKey] as EventHandler<ChangeAnnotationEventArgs>;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Raises the <see cref="CharAdded" /> event.
        /// </summary>
        /// <param name="e">A <see cref="CharAddedEventArgs" /> that contains the event data.</param>
        protected virtual void OnCharAdded(CharAddedEventArgs e)
        {
            var handler = Events[charAddedEventKey] as EventHandler<CharAddedEventArgs>;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Raises the <see cref="Delete" /> event.
        /// </summary>
        /// <param name="e">A <see cref="ModificationEventArgs" /> that contains the event data.</param>
        protected virtual void OnDelete(ModificationEventArgs e)
        {
            var handler = Events[deleteEventKey] as EventHandler<ModificationEventArgs>;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Raises the <see cref="DoubleClick" /> event.
        /// </summary>
        /// <param name="e">A <see cref="DoubleClickEventArgs" /> that contains the event data.</param>
        protected virtual void OnDoubleClick(DoubleClickEventArgs e)
        {
            var handler = Events[doubleClickEventKey] as EventHandler<DoubleClickEventArgs>;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Raises the <see cref="DwellEnd" /> event.
        /// </summary>
        /// <param name="e">A <see cref="DwellEventArgs" /> that contains the event data.</param>
        protected virtual void OnDwellEnd(DwellEventArgs e)
        {
            var handler = Events[dwellEndEventKey] as EventHandler<DwellEventArgs>;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Raises the <see cref="DwellStart" /> event.
        /// </summary>
        /// <param name="e">A <see cref="DwellEventArgs" /> that contains the event data.</param>
        protected virtual void OnDwellStart(DwellEventArgs e)
        {
            var handler = Events[dwellStartEventKey] as EventHandler<DwellEventArgs>;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Raises the HandleCreated event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected unsafe override void OnHandleCreated(EventArgs e)
        {
            // Set more intelligent defaults...
            InitDocument();

            // I would like to see all of my text please
            DirectMessage(NativeMethods.SCI_SETSCROLLWIDTHTRACKING, new IntPtr(1));

            // Enable support for the call tip style and tabs
            DirectMessage(NativeMethods.SCI_CALLTIPUSESTYLE, new IntPtr(16));

            // Reset the valid "word chars" to work around a bug? in Scintilla which includes those below plus non-printable (beyond ASCII 127) characters
            var bytes = Helpers.GetBytes("abcdefghijklmnopqrstuvwxyz_ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", Encoding.ASCII, zeroTerminated: true);
            fixed (byte* bp = bytes)
                DirectMessage(NativeMethods.SCI_SETWORDCHARS, IntPtr.Zero, new IntPtr(bp));

            // Native Scintilla uses the WM_CREATE message to register itself as an
            // IDropTarget... beating Windows Forms to the punch. There are many possible
            // ways to solve this, but my favorite is to revoke drag and drop from the
            // native Scintilla control before base.OnHandleCreated does the standard
            // processing of AllowDrop.
            NativeMethods.RevokeDragDrop(Handle);

            base.OnHandleCreated(e);
        }

        /// <summary>
        /// Raises the <see cref="HotspotClick" /> event.
        /// </summary>
        /// <param name="e">A <see cref="HotspotClickEventArgs" /> that contains the event data.</param>
        protected virtual void OnHotspotClick(HotspotClickEventArgs e)
        {
            var handler = Events[hotspotClickEventKey] as EventHandler<HotspotClickEventArgs>;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Raises the <see cref="HotspotDoubleClick" /> event.
        /// </summary>
        /// <param name="e">A <see cref="HotspotClickEventArgs" /> that contains the event data.</param>
        protected virtual void OnHotspotDoubleClick(HotspotClickEventArgs e)
        {
            var handler = Events[hotspotDoubleClickEventKey] as EventHandler<HotspotClickEventArgs>;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Raises the <see cref="HotspotReleaseClick" /> event.
        /// </summary>
        /// <param name="e">A <see cref="HotspotClickEventArgs" /> that contains the event data.</param>
        protected virtual void OnHotspotReleaseClick(HotspotClickEventArgs e)
        {
            var handler = Events[hotspotReleaseClickEventKey] as EventHandler<HotspotClickEventArgs>;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Raises the <see cref="IndicatorClick" /> event.
        /// </summary>
        /// <param name="e">An <see cref="IndicatorClickEventArgs" /> that contains the event data.</param>
        protected virtual void OnIndicatorClick(IndicatorClickEventArgs e)
        {
            var handler = Events[indicatorClickEventKey] as EventHandler<IndicatorClickEventArgs>;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Raises the <see cref="IndicatorRelease" /> event.
        /// </summary>
        /// <param name="e">An <see cref="IndicatorReleaseEventArgs" /> that contains the event data.</param>
        protected virtual void OnIndicatorRelease(IndicatorReleaseEventArgs e)
        {
            var handler = Events[indicatorReleaseEventKey] as EventHandler<IndicatorReleaseEventArgs>;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Raises the <see cref="Insert" /> event.
        /// </summary>
        /// <param name="e">A <see cref="ModificationEventArgs" /> that contains the event data.</param>
        protected virtual void OnInsert(ModificationEventArgs e)
        {
            var handler = Events[insertEventKey] as EventHandler<ModificationEventArgs>;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Raises the <see cref="InsertCheck" /> event.
        /// </summary>
        /// <param name="e">An <see cref="InsertCheckEventArgs" /> that contains the event data.</param>
        protected virtual void OnInsertCheck(InsertCheckEventArgs e)
        {
            var handler = Events[insertCheckEventKey] as EventHandler<InsertCheckEventArgs>;
            if (handler != null)
                handler(this, e);
        }


        #region Zeroit Additions
        /// <summary>
        /// change this to whatever margin you want the bookmarks/breakpoints to show in
        /// </summary>
        private const int BOOKMARK_MARGIN = 2;
        private const int BOOKMARK_MARKER = 2; 
        #endregion

        /// <summary>
        /// Raises the <see cref="MarginClick" /> event.
        /// </summary>
        /// <param name="e">A <see cref="MarginClickEventArgs" /> that contains the event data.</param>
        protected virtual void OnMarginClick(MarginClickEventArgs e)
        {
            var handler = Events[marginClickEventKey] as EventHandler<MarginClickEventArgs>;
            if (handler != null)
                handler(this, e);

            #region Zeroit Additions 

            if (e.Margin == BOOKMARK_MARGIN)
            {
                // Do we have a marker for this line?
                const uint mask = (1 << BOOKMARK_MARKER);
                var line = TextArea.Lines[TextArea.LineFromPosition(e.Position)];
                if ((line.MarkerGet() & mask) > 0)
                {
                    // Remove existing bookmark
                    line.MarkerDelete(BOOKMARK_MARKER);
                }
                else
                {
                    // Add bookmark
                    line.MarkerAdd(BOOKMARK_MARKER);
                }
            }

            #endregion
        }

        /// <summary>
        /// Raises the <see cref="MarginRightClick" /> event.
        /// </summary>
        /// <param name="e">A <see cref="MarginClickEventArgs" /> that contains the event data.</param>
        protected virtual void OnMarginRightClick(MarginClickEventArgs e)
        {
            var handler = Events[marginRightClickEventKey] as EventHandler<MarginClickEventArgs>;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Raises the <see cref="ModifyAttempt" /> event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnModifyAttempt(EventArgs e)
        {
            var handler = Events[modifyAttemptEventKey] as EventHandler<EventArgs>;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Raises the MouseUp event.
        /// </summary>
        /// <param name="e">A MouseEventArgs that contains the event data.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            // Borrowed this from TextBoxBase.OnMouseUp
            if (!doubleClick)
            {
                OnClick(e);
                OnMouseClick(e);
            }
            else
            {
                var doubleE = new MouseEventArgs(e.Button, 2, e.X, e.Y, e.Delta);
                OnDoubleClick(doubleE);
                OnMouseDoubleClick(doubleE);
                doubleClick = false;
            }

            base.OnMouseUp(e);
        }

        /// <summary>
        /// Raises the <see cref="NeedShown" /> event.
        /// </summary>
        /// <param name="e">A <see cref="NeedShownEventArgs" /> that contains the event data.</param>
        protected virtual void OnNeedShown(NeedShownEventArgs e)
        {
            var handler = Events[needShownEventKey] as EventHandler<NeedShownEventArgs>;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Raises the <see cref="Painted" /> event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnPainted(EventArgs e)
        {
            var handler = Events[paintedEventKey] as EventHandler<EventArgs>;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Raises the <see cref="SavePointLeft" /> event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnSavePointLeft(EventArgs e)
        {
            var handler = Events[savePointLeftEventKey] as EventHandler<EventArgs>;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Raises the <see cref="SavePointReached" /> event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnSavePointReached(EventArgs e)
        {
            var handler = Events[savePointReachedEventKey] as EventHandler<EventArgs>;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Raises the <see cref="StyleNeeded" /> event.
        /// </summary>
        /// <param name="e">A <see cref="StyleNeededEventArgs" /> that contains the event data.</param>
        protected virtual void OnStyleNeeded(StyleNeededEventArgs e)
        {
            var handler = Events[styleNeededEventKey] as EventHandler<StyleNeededEventArgs>;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Raises the <see cref="UpdateUI" /> event.
        /// </summary>
        /// <param name="e">An <see cref="UpdateUIEventArgs" /> that contains the event data.</param>
        protected virtual void OnUpdateUI(UpdateUIEventArgs e)
        {
            EventHandler<UpdateUIEventArgs> handler = Events[updateUIEventKey] as EventHandler<UpdateUIEventArgs>;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Raises the <see cref="ZoomChanged" /> event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnZoomChanged(EventArgs e)
        {
            var handler = Events[zoomChangedEventKey] as EventHandler<EventArgs>;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Pastes the contents of the clipboard into the current selection.
        /// </summary>
        public void Paste()
        {
            DirectMessage(NativeMethods.SCI_PASTE);
        }

        /// <summary>
        /// Returns the X display pixel location of the specified document position.
        /// </summary>
        /// <param name="pos">The zero-based document character position.</param>
        /// <returns>The x-coordinate of the specified <paramref name="pos" /> within the client rectangle of the control.</returns>
        public int PointXFromPosition(int pos)
        {
            pos = Helpers.Clamp(pos, 0, TextLength);
            pos = Lines.CharToBytePosition(pos);
            return DirectMessage(NativeMethods.SCI_POINTXFROMPOSITION, IntPtr.Zero, new IntPtr(pos)).ToInt32();
        }

        /// <summary>
        /// Returns the Y display pixel location of the specified document position.
        /// </summary>
        /// <param name="pos">The zero-based document character position.</param>
        /// <returns>The y-coordinate of the specified <paramref name="pos" /> within the client rectangle of the control.</returns>
        public int PointYFromPosition(int pos)
        {
            pos = Helpers.Clamp(pos, 0, TextLength);
            pos = Lines.CharToBytePosition(pos);
            return DirectMessage(NativeMethods.SCI_POINTYFROMPOSITION, IntPtr.Zero, new IntPtr(pos)).ToInt32();
        }

        /// <summary>
        /// Retrieves a list of property names that can be set for the current <see cref="Lexer" />.
        /// </summary>
        /// <returns>A String of property names separated by line breaks.</returns>
        public unsafe string PropertyNames()
        {
            var length = DirectMessage(NativeMethods.SCI_PROPERTYNAMES).ToInt32();
            if (length == 0)
                return string.Empty;

            var bytes = new byte[length + 1];
            fixed (byte* bp = bytes)
            {
                DirectMessage(NativeMethods.SCI_PROPERTYNAMES, IntPtr.Zero, new IntPtr(bp));
                return Helpers.GetString(new IntPtr(bp), length, Encoding.ASCII);
            }
        }

        /// <summary>
        /// Retrieves the data type of the specified property name for the current <see cref="Lexer" />.
        /// </summary>
        /// <param name="name">A property name supported by the current <see cref="Lexer" />.</param>
        /// <returns>One of the <see cref="PropertyType" /> enumeration values. The default is <see cref="Zeroit.Framework.CodeBox.PropertyType.Boolean" />.</returns>
        /// <remarks>A list of supported property names for the current <see cref="Lexer" /> can be obtained by calling <see cref="PropertyNames" />.</remarks>
        public unsafe PropertyType PropertyType(string name)
        {
            if (String.IsNullOrEmpty(name))
                return Zeroit.Framework.CodeBox.PropertyType.Boolean;

            var bytes = Helpers.GetBytes(name, Encoding.ASCII, zeroTerminated: true);
            fixed (byte* bp = bytes)
                return (PropertyType)DirectMessage(NativeMethods.SCI_PROPERTYTYPE, new IntPtr(bp));
        }

        /// <summary>
        /// Redoes the effect of an <see cref="Undo" /> operation.
        /// </summary>
        public void Redo()
        {
            DirectMessage(NativeMethods.SCI_REDO);
        }

        /// <summary>
        /// Maps the specified image to a type identifer for use in an autocompletion list.
        /// </summary>
        /// <param name="type">The numeric identifier for this image.</param>
        /// <param name="image">The Bitmap to use in an autocompletion list.</param>
        /// <remarks>
        /// The <paramref name="image" /> registered can be referenced by its <paramref name="type" /> identifer in an autocompletion
        /// list by suffixing a word with the <see cref="AutoCTypeSeparator" /> character and the <paramref name="type" /> value. e.g.
        /// "int?2 long?3 short?1" etc....
        /// </remarks>
        /// <seealso cref="AutoCTypeSeparator" />
        public unsafe void RegisterRgbaImage(int type, Bitmap image)
        {
            // TODO Clamp type?
            if (image == null)
                return;

            DirectMessage(NativeMethods.SCI_RGBAIMAGESETWIDTH, new IntPtr(image.Width));
            DirectMessage(NativeMethods.SCI_RGBAIMAGESETHEIGHT, new IntPtr(image.Height));

            var bytes = Helpers.BitmapToArgb(image);
            fixed (byte* bp = bytes)
                DirectMessage(NativeMethods.SCI_REGISTERRGBAIMAGE, new IntPtr(type), new IntPtr(bp));
        }

        /// <summary>
        /// Decreases the reference count of the specified document by 1.
        /// </summary>
        /// <param name="document">
        /// The document reference count to decrease.
        /// When a document's reference count reaches 0 it is destroyed and any associated memory released.
        /// </param>
        public void ReleaseDocument(Document document)
        {
            var ptr = document.Value;
            DirectMessage(NativeMethods.SCI_RELEASEDOCUMENT, IntPtr.Zero, ptr);
        }

        /// <summary>
        /// Replaces the current selection with the specified text.
        /// </summary>
        /// <param name="text">The text that should replace the current selection.</param>
        /// <remarks>
        /// If there is not a current selection, the text will be inserted at the current caret position.
        /// Following the operation the caret is placed at the end of the inserted text and scrolled into view.
        /// </remarks>
        public unsafe void ReplaceSelection(string text)
        {
            fixed (byte* bp = Helpers.GetBytes(text ?? string.Empty, Encoding, zeroTerminated: true))
                DirectMessage(NativeMethods.SCI_REPLACESEL, IntPtr.Zero, new IntPtr(bp));
        }

        /// <summary>
        /// Replaces the target defined by <see cref="TargetStart" /> and <see cref="TargetEnd" /> with the specified <paramref name="text" />.
        /// </summary>
        /// <param name="text">The text that will replace the current target.</param>
        /// <returns>The length of the replaced text.</returns>
        /// <remarks>
        /// The <see cref="TargetStart" /> and <see cref="TargetEnd" /> properties will be updated to the start and end positions of the replaced text.
        /// The recommended way to delete text in the document is to set the target range to be removed and replace the target with an empty string.
        /// </remarks>
        public unsafe int ReplaceTarget(string text)
        {
            if (text == null)
                text = string.Empty;

            var bytes = Helpers.GetBytes(text, Encoding, false);
            fixed (byte* bp = bytes)
                DirectMessage(NativeMethods.SCI_REPLACETARGET, new IntPtr(bytes.Length), new IntPtr(bp));

            return text.Length;
        }

        /// <summary>
        /// Replaces the target text defined by <see cref="TargetStart" /> and <see cref="TargetEnd" /> with the specified value after first substituting
        /// "\1" through "\9" macros in the <paramref name="text" /> with the most recent regular expression capture groups.
        /// </summary>
        /// <param name="text">The text containing "\n" macros that will be substituted with the most recent regular expression capture groups and then replace the current target.</param>
        /// <returns>The length of the replaced text.</returns>
        /// <remarks>
        /// The "\0" macro will be substituted by the entire matched text from the most recent search.
        /// The <see cref="TargetStart" /> and <see cref="TargetEnd" /> properties will be updated to the start and end positions of the replaced text.
        /// </remarks>
        /// <seealso cref="GetTag" />
        public unsafe int ReplaceTargetRe(string text)
        {
            var bytes = Helpers.GetBytes(text ?? string.Empty, Encoding, false);
            fixed (byte* bp = bytes)
                DirectMessage(NativeMethods.SCI_REPLACETARGETRE, new IntPtr(bytes.Length), new IntPtr(bp));

            return Math.Abs(TargetEnd - TargetStart);
        }

        private void ResetAdditionalCaretForeColor()
        {
            AdditionalCaretForeColor = Color.FromArgb(127, 127, 127);
        }

        /// <summary>
        /// Makes the next selection the main selection.
        /// </summary>
        public void RotateSelection()
        {
            DirectMessage(NativeMethods.SCI_ROTATESELECTION);
        }

        private void ScnDoubleClick(ref NativeMethods.SCNotification scn)
        {
            var keys = Keys.Modifiers & (Keys)(scn.modifiers << 16);
            var eventArgs = new DoubleClickEventArgs(this, keys, scn.position, scn.line);
            OnDoubleClick(eventArgs);
        }

        private void ScnHotspotClick(ref NativeMethods.SCNotification scn)
        {
            var keys = Keys.Modifiers & (Keys)(scn.modifiers << 16);
            var eventArgs = new HotspotClickEventArgs(this, keys, scn.position);
            switch (scn.nmhdr.code)
            {
                case NativeMethods.SCN_HOTSPOTCLICK:
                    OnHotspotClick(eventArgs);
                    break;

                case NativeMethods.SCN_HOTSPOTDOUBLECLICK:
                    OnHotspotDoubleClick(eventArgs);
                    break;

                case NativeMethods.SCN_HOTSPOTRELEASECLICK:
                    OnHotspotReleaseClick(eventArgs);
                    break;
            }
        }

        private void ScnIndicatorClick(ref NativeMethods.SCNotification scn)
        {
            switch (scn.nmhdr.code)
            {
                case NativeMethods.SCN_INDICATORCLICK:
                    var keys = Keys.Modifiers & (Keys)(scn.modifiers << 16);
                    OnIndicatorClick(new IndicatorClickEventArgs(this, keys, scn.position));
                    break;

                case NativeMethods.SCN_INDICATORRELEASE:
                    OnIndicatorRelease(new IndicatorReleaseEventArgs(this, scn.position));
                    break;
            }
        }

        private void ScnMarginClick(ref NativeMethods.SCNotification scn)
        {
            var keys = Keys.Modifiers & (Keys)(scn.modifiers << 16);
            var eventArgs = new MarginClickEventArgs(this, keys, scn.position, scn.margin);

            if (scn.nmhdr.code == NativeMethods.SCN_MARGINCLICK)
                OnMarginClick(eventArgs);
            else
                OnMarginRightClick(eventArgs);
        }

        private void ScnModified(ref NativeMethods.SCNotification scn)
        {
            // The InsertCheck, BeforeInsert, BeforeDelete, Insert, and Delete events can all potentially require
            // the same conversions: byte to char position, char* to string, etc.... To avoid doing the same work
            // multiple times we share that data between events.

            if ((scn.modificationType & NativeMethods.SC_MOD_INSERTCHECK) > 0)
            {
                var eventArgs = new InsertCheckEventArgs(this, scn.position, scn.length, scn.text);
                OnInsertCheck(eventArgs);

                cachedPosition = eventArgs.CachedPosition;
                cachedText = eventArgs.CachedText;
            }

            const int sourceMask = (NativeMethods.SC_PERFORMED_USER | NativeMethods.SC_PERFORMED_UNDO | NativeMethods.SC_PERFORMED_REDO);

            if ((scn.modificationType & (NativeMethods.SC_MOD_BEFOREDELETE | NativeMethods.SC_MOD_BEFOREINSERT)) > 0)
            {
                var source = (ModificationSource)(scn.modificationType & sourceMask);
                var eventArgs = new BeforeModificationEventArgs(this, source, scn.position, scn.length, scn.text);

                eventArgs.CachedPosition = cachedPosition;
                eventArgs.CachedText = cachedText;

                if ((scn.modificationType & NativeMethods.SC_MOD_BEFOREINSERT) > 0)
                {
                    OnBeforeInsert(eventArgs);
                }
                else
                {
                    OnBeforeDelete(eventArgs);
                }

                cachedPosition = eventArgs.CachedPosition;
                cachedText = eventArgs.CachedText;
            }

            if ((scn.modificationType & (NativeMethods.SC_MOD_DELETETEXT | NativeMethods.SC_MOD_INSERTTEXT)) > 0)
            {
                var source = (ModificationSource)(scn.modificationType & sourceMask);
                var eventArgs = new ModificationEventArgs(this, source, scn.position, scn.length, scn.text, scn.linesAdded);

                eventArgs.CachedPosition = cachedPosition;
                eventArgs.CachedText = cachedText;

                if ((scn.modificationType & NativeMethods.SC_MOD_INSERTTEXT) > 0)
                {
                    OnInsert(eventArgs);
                }
                else
                {
                    OnDelete(eventArgs);
                }

                // Always clear the cache
                cachedPosition = null;
                cachedText = null;

                // For backward compatibility.... Of course this means that we'll raise two
                // TextChanged events for replace (insert/delete) operations, but that's life.
                OnTextChanged(EventArgs.Empty);
            }

            if ((scn.modificationType & NativeMethods.SC_MOD_CHANGEANNOTATION) > 0)
            {
                var eventArgs = new ChangeAnnotationEventArgs(scn.line);
                OnChangeAnnotation(eventArgs);
            }
        }

        /// <summary>
        /// Scrolls the current position into view, if it is not already visible.
        /// </summary>
        public void ScrollCaret()
        {
            DirectMessage(NativeMethods.SCI_SCROLLCARET);
        }

        /// <summary>
        /// Scrolls the specified range into view.
        /// </summary>
        /// <param name="start">The zero-based document start position to scroll to.</param>
        /// <param name="end">
        /// The zero-based document end position to scroll to if doing so does not cause the <paramref name="start" />
        /// position to scroll out of view.
        /// </param>
        /// <remarks>This may be used to make a search match visible.</remarks>
        public void ScrollRange(int start, int end)
        {
            var textLength = TextLength;
            start = Helpers.Clamp(start, 0, textLength);
            end = Helpers.Clamp(end, 0, textLength);

            // Convert to byte positions
            start = Lines.CharToBytePosition(start);
            end = Lines.CharToBytePosition(end);

            // The arguments would  seem reverse from Scintilla documentation
            // but empirical  evidence suggests this is correct....
            DirectMessage(NativeMethods.SCI_SCROLLRANGE, new IntPtr(start), new IntPtr(end));
        }

        /// <summary>
        /// Searches for the first occurrence of the specified text in the target defined by <see cref="TargetStart" /> and <see cref="TargetEnd" />.
        /// </summary>
        /// <param name="text">The text to search for. The interpretation of the text (i.e. whether it is a regular expression) is defined by the <see cref="SearchFlags" /> property.</param>
        /// <returns>The zero-based start position of the matched text within the document if successful; otherwise, -1.</returns>
        /// <remarks>
        /// If successful, the <see cref="TargetStart" /> and <see cref="TargetEnd" /> properties will be updated to the start and end positions of the matched text.
        /// Searching can be performed in reverse using a <see cref="TargetStart" /> greater than the <see cref="TargetEnd" />.
        /// </remarks>
        public unsafe int SearchInTarget(string text)
        {
            int bytePos = 0;
            var bytes = Helpers.GetBytes(text ?? string.Empty, Encoding, zeroTerminated: false);
            fixed (byte* bp = bytes)
                bytePos = DirectMessage(NativeMethods.SCI_SEARCHINTARGET, new IntPtr(bytes.Length), new IntPtr(bp)).ToInt32();

            if (bytePos == -1)
                return bytePos;

            return Lines.ByteToCharPosition(bytePos);
        }

        /// <summary>
        /// Selects all the text in the document.
        /// </summary>
        /// <remarks>The current position is not scrolled into view.</remarks>
        public void SelectAll()
        {
            DirectMessage(NativeMethods.SCI_SELECTALL);
        }

        /// <summary>
        /// Sets the background color of additional selections.
        /// </summary>
        /// <param name="color">Additional selections background color.</param>
        /// <remarks>Calling <see cref="SetSelectionBackColor" /> will reset the <paramref name="color" /> specified.</remarks>
        public void SetAdditionalSelBack(Color color)
        {
            var colour = ColorTranslator.ToWin32(color);
            DirectMessage(NativeMethods.SCI_SETADDITIONALSELBACK, new IntPtr(colour));
        }

        /// <summary>
        /// Sets the foreground color of additional selections.
        /// </summary>
        /// <param name="color">Additional selections foreground color.</param>
        /// <remarks>Calling <see cref="SetSelectionForeColor" /> will reset the <paramref name="color" /> specified.</remarks>
        public void SetAdditionalSelFore(Color color)
        {
            var colour = ColorTranslator.ToWin32(color);
            DirectMessage(NativeMethods.SCI_SETADDITIONALSELFORE, new IntPtr(colour));
        }

        /// <summary>
        /// Removes any selection and places the caret at the specified position.
        /// </summary>
        /// <param name="pos">The zero-based document position to place the caret at.</param>
        /// <remarks>The caret is not scrolled into view.</remarks>
        public void SetEmptySelection(int pos)
        {
            pos = Helpers.Clamp(pos, 0, TextLength);
            pos = Lines.CharToBytePosition(pos);
            DirectMessage(NativeMethods.SCI_SETEMPTYSELECTION, new IntPtr(pos));
        }

        /// <summary>
        /// Sets additional options for displaying folds.
        /// </summary>
        /// <param name="flags">A bitwise combination of the <see cref="FoldFlags" /> enumeration.</param>
        public void SetFoldFlags(FoldFlags flags)
        {
            DirectMessage(NativeMethods.SCI_SETFOLDFLAGS, new IntPtr((int)flags));
        }

        /// <summary>
        /// Sets a global override to the fold margin color.
        /// </summary>
        /// <param name="use">true to override the fold margin color; otherwise, false.</param>
        /// <param name="color">The global fold margin color.</param>
        /// <seealso cref="SetFoldMarginHighlightColor" />
        public void SetFoldMarginColor(bool use, Color color)
        {
            var colour = ColorTranslator.ToWin32(color);
            var useFoldMarginColour = (use ? new IntPtr(1) : IntPtr.Zero);

            DirectMessage(NativeMethods.SCI_SETFOLDMARGINCOLOUR, useFoldMarginColour, new IntPtr(colour));
        }

        /// <summary>
        /// Sets a global override to the fold margin highlight color.
        /// </summary>
        /// <param name="use">true to override the fold margin highlight color; otherwise, false.</param>
        /// <param name="color">The global fold margin highlight color.</param>
        /// <seealso cref="SetFoldMarginColor" />
        public void SetFoldMarginHighlightColor(bool use, Color color)
        {
            var colour = ColorTranslator.ToWin32(color);
            var useFoldMarginHighlightColour = (use ? new IntPtr(1) : IntPtr.Zero);

            DirectMessage(NativeMethods.SCI_SETFOLDMARGINHICOLOUR, useFoldMarginHighlightColour, new IntPtr(colour));
        }

        /// <summary>
        /// Updates a keyword set used by the current <see cref="Lexer" />.
        /// </summary>
        /// <param name="set">The zero-based index of the keyword set to update.</param>
        /// <param name="keywords">
        /// A list of keywords pertaining to the current <see cref="Lexer" /> separated by whitespace (space, tab, '\n', '\r') characters.
        /// </param>
        /// <remarks>The keywords specified will be styled according to the current <see cref="Lexer" />.</remarks>
        /// <seealso cref="DescribeKeywordSets" />
        public unsafe void SetKeywords(int set, string keywords)
        {
            set = Helpers.Clamp(set, 0, NativeMethods.KEYWORDSET_MAX);
            var bytes = Helpers.GetBytes(keywords ?? string.Empty, Encoding.ASCII, zeroTerminated: true);

            fixed (byte* bp = bytes)
                DirectMessage(NativeMethods.SCI_SETKEYWORDS, new IntPtr(set), new IntPtr(bp));
        }

        /// <summary>
        /// Sets the application-wide behavior for destroying <see cref="Scintilla" /> controls.
        /// </summary>
        /// <param name="reparent">
        /// true to reparent Scintilla controls to message-only windows when destroyed rather than actually destroying the control handle; otherwise, false.
        /// The default is true.
        /// </param>
        /// <remarks>This method must be called prior to the first <see cref="Scintilla" /> control being created.</remarks>
        public static void SetDestroyHandleBehavior(bool reparent)
        {
            // WM_DESTROY workaround
            if (reparentAll == null)
            {
                reparentAll = reparent;
            }
        }

        /// <summary>
        /// Sets the application-wide default module path of the native Scintilla library.
        /// </summary>
        /// <param name="modulePath">The native Scintilla module path.</param>
        /// <remarks>
        /// This method must be called prior to the first <see cref="Scintilla" /> control being created.
        /// The <paramref name="modulePath" /> can be relative or absolute.
        /// </remarks>
        public static void SetModulePath(string modulePath)
        {
            if (modulePath == null)
            {
                modulePath = modulePath;
            }
        }

        /// <summary>
        /// Passes the specified property name-value pair to the current <see cref="Lexer" />.
        /// </summary>
        /// <param name="name">The property name to set.</param>
        /// <param name="value">
        /// The property value. Values can refer to other property names using the syntax $(name), where 'name' is another property
        /// name for the current <see cref="Lexer" />. When the property value is retrieved by a call to <see cref="GetPropertyExpanded" />
        /// the embedded property name macro will be replaced (expanded) with that current property value.
        /// </param>
        /// <remarks>Property names are case-sensitive.</remarks>
        public unsafe void SetProperty(string name, string value)
        {
            if (String.IsNullOrEmpty(name))
                return;

            var nameBytes = Helpers.GetBytes(name, Encoding.ASCII, zeroTerminated: true);
            var valueBytes = Helpers.GetBytes(value ?? string.Empty, Encoding.ASCII, zeroTerminated: true);

            fixed (byte* nb = nameBytes)
            fixed (byte* vb = valueBytes)
            {
                DirectMessage(NativeMethods.SCI_SETPROPERTY, new IntPtr(nb), new IntPtr(vb));
            }
        }

        /// <summary>
        /// Marks the document as unmodified.
        /// </summary>
        /// <seealso cref="Modified" />
        public void SetSavePoint()
        {
            DirectMessage(NativeMethods.SCI_SETSAVEPOINT);
        }

        /// <summary>
        /// Sets the anchor and current position.
        /// </summary>
        /// <param name="anchorPos">The zero-based document position to start the selection.</param>
        /// <param name="currentPos">The zero-based document position to end the selection.</param>
        /// <remarks>
        /// A negative value for <paramref name="currentPos" /> signifies the end of the document.
        /// A negative value for <paramref name="anchorPos" /> signifies no selection (i.e. sets the <paramref name="anchorPos" />
        /// to the same position as the <paramref name="currentPos" />).
        /// The current position is scrolled into view following this operation.
        /// </remarks>
        public void SetSel(int anchorPos, int currentPos)
        {
            if (anchorPos == currentPos)
            {
                // Optimization so that we don't have to translate the anchor position
                // when we can instead just pass -1 and have Scintilla handle it.
                anchorPos = -1;
            }

            var textLength = TextLength;

            if (anchorPos >= 0)
            {
                anchorPos = Helpers.Clamp(anchorPos, 0, textLength);
                anchorPos = Lines.CharToBytePosition(anchorPos);
            }

            if (currentPos >= 0)
            {
                currentPos = Helpers.Clamp(currentPos, 0, textLength);
                currentPos = Lines.CharToBytePosition(currentPos);
            }

            DirectMessage(NativeMethods.SCI_SETSEL, new IntPtr(anchorPos), new IntPtr(currentPos));
        }

        /// <summary>
        /// Sets a single selection from anchor to caret.
        /// </summary>
        /// <param name="caret">The zero-based document position to end the selection.</param>
        /// <param name="anchor">The zero-based document position to start the selection.</param>
        public void SetSelection(int caret, int anchor)
        {
            var textLength = TextLength;

            caret = Helpers.Clamp(caret, 0, textLength);
            anchor = Helpers.Clamp(anchor, 0, textLength);

            caret = Lines.CharToBytePosition(caret);
            anchor = Lines.CharToBytePosition(anchor);

            DirectMessage(NativeMethods.SCI_SETSELECTION, new IntPtr(caret), new IntPtr(anchor));
        }

        /// <summary>
        /// Sets a global override to the selection background color.
        /// </summary>
        /// <param name="use">true to override the selection background color; otherwise, false.</param>
        /// <param name="color">The global selection background color.</param>
        /// <seealso cref="SetSelectionForeColor" />
        public void SetSelectionBackColor(bool use, Color color)
        {
            var colour = ColorTranslator.ToWin32(color);
            var useSelectionForeColour = (use ? new IntPtr(1) : IntPtr.Zero);

            DirectMessage(NativeMethods.SCI_SETSELBACK, useSelectionForeColour, new IntPtr(colour));
        }

        /// <summary>
        /// Sets a global override to the selection foreground color.
        /// </summary>
        /// <param name="use">true to override the selection foreground color; otherwise, false.</param>
        /// <param name="color">The global selection foreground color.</param>
        /// <seealso cref="SetSelectionBackColor" />
        public void SetSelectionForeColor(bool use, Color color)
        {
            var colour = ColorTranslator.ToWin32(color);
            var useSelectionForeColour = (use ? new IntPtr(1) : IntPtr.Zero);

            DirectMessage(NativeMethods.SCI_SETSELFORE, useSelectionForeColour, new IntPtr(colour));
        }

        /// <summary>
        /// Styles the specified length of characters.
        /// </summary>
        /// <param name="length">The number of characters to style.</param>
        /// <param name="style">The <see cref="Style" /> definition index to assign each character.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="length" /> or <paramref name="style" /> is less than zero. -or-
        /// The sum of a preceeding call to <see cref="StartStyling" /> or <see name="SetStyling" /> and <paramref name="length" /> is greater than the document length. -or-
        /// <paramref name="style" /> is greater than or equal to the number of style definitions.
        /// </exception>
        /// <remarks>
        /// The styling position is advanced by <paramref name="length" /> after each call allowing multiple
        /// calls to <see cref="SetStyling" /> for a single call to <see cref="StartStyling" />.
        /// </remarks>
        /// <seealso cref="StartStyling" />
        public void SetStyling(int length, int style)
        {
            var textLength = TextLength;

            if (length < 0)
                throw new ArgumentOutOfRangeException("length", "Length cannot be less than zero.");
            if (stylingPosition + length > textLength)
                throw new ArgumentOutOfRangeException("length", "Position and length must refer to a range within the document.");
            if (style < 0 || style >= Styles.Count)
                throw new ArgumentOutOfRangeException("style", "Style must be non-negative and less than the size of the collection.");

            var endPos = stylingPosition + length;
            var endBytePos = Lines.CharToBytePosition(endPos);
            DirectMessage(NativeMethods.SCI_SETSTYLING, new IntPtr(endBytePos - stylingBytePosition), new IntPtr(style));

            // Track this for the next call
            stylingPosition = endPos;
            stylingBytePosition = endBytePos;
        }

        /// <summary>
        /// Sets the <see cref="TargetStart" /> and <see cref="TargetEnd" /> properties in a single call.
        /// </summary>
        /// <param name="start">The zero-based character position within the document to start a search or replace operation.</param>
        /// <param name="end">The zero-based character position within the document to end a search or replace operation.</param>
        /// <seealso cref="TargetStart" />
        /// <seealso cref="TargetEnd" />
        public void SetTargetRange(int start, int end)
        {
            var textLength = TextLength;
            start = Helpers.Clamp(start, 0, textLength);
            end = Helpers.Clamp(end, 0, textLength);

            start = Lines.CharToBytePosition(start);
            end = Lines.CharToBytePosition(end);

            DirectMessage(NativeMethods.SCI_SETTARGETRANGE, new IntPtr(start), new IntPtr(end));
        }

        /// <summary>
        /// Sets a global override to the whitespace background color.
        /// </summary>
        /// <param name="use">true to override the whitespace background color; otherwise, false.</param>
        /// <param name="color">The global whitespace background color.</param>
        /// <remarks>When not overridden globally, the whitespace background color is determined by the current lexer.</remarks>
        /// <seealso cref="ViewWhitespace" />
        /// <seealso cref="SetWhitespaceForeColor" />
        public void SetWhitespaceBackColor(bool use, Color color)
        {
            var colour = ColorTranslator.ToWin32(color);
            var useWhitespaceBackColour = (use ? new IntPtr(1) : IntPtr.Zero);

            DirectMessage(NativeMethods.SCI_SETWHITESPACEBACK, useWhitespaceBackColour, new IntPtr(colour));
        }

        /// <summary>
        /// Sets a global override to the whitespace foreground color.
        /// </summary>
        /// <param name="use">true to override the whitespace foreground color; otherwise, false.</param>
        /// <param name="color">The global whitespace foreground color.</param>
        /// <remarks>When not overridden globally, the whitespace foreground color is determined by the current lexer.</remarks>
        /// <seealso cref="ViewWhitespace" />
        /// <seealso cref="SetWhitespaceBackColor" />
        public void SetWhitespaceForeColor(bool use, Color color)
        {
            var colour = ColorTranslator.ToWin32(color);
            var useWhitespaceForeColour = (use ? new IntPtr(1) : IntPtr.Zero);

            DirectMessage(NativeMethods.SCI_SETWHITESPACEFORE, useWhitespaceForeColour, new IntPtr(colour));
        }

        private bool ShouldSerializeAdditionalCaretForeColor()
        {
            return AdditionalCaretForeColor != Color.FromArgb(127, 127, 127);
        }

        /// <summary>
        /// Shows the range of lines specified.
        /// </summary>
        /// <param name="lineStart">The zero-based index of the line range to start showing.</param>
        /// <param name="lineEnd">The zero-based index of the line range to end showing.</param>
        /// <seealso cref="HideLines" />
        /// <seealso cref="Line.Visible" />
        public void ShowLines(int lineStart, int lineEnd)
        {
            lineStart = Helpers.Clamp(lineStart, 0, Lines.Count);
            lineEnd = Helpers.Clamp(lineEnd, lineStart, Lines.Count);

            DirectMessage(NativeMethods.SCI_SHOWLINES, new IntPtr(lineStart), new IntPtr(lineEnd));
        }

        /// <summary>
        /// Prepares for styling by setting the styling <paramref name="position" /> to start at.
        /// </summary>
        /// <param name="position">The zero-based character position in the document to start styling.</param>
        /// <remarks>
        /// After preparing the document for styling, use successive calls to <see cref="SetStyling" />
        /// to style the document.
        /// </remarks>
        /// <seealso cref="SetStyling" />
        public void StartStyling(int position)
        {
            position = Helpers.Clamp(position, 0, TextLength);
            var pos = Lines.CharToBytePosition(position);
            DirectMessage(NativeMethods.SCI_STARTSTYLING, new IntPtr(pos));

            // Track this so we can validate calls to SetStyling
            stylingPosition = position;
            stylingBytePosition = pos;
        }

        /// <summary>
        /// Resets all style properties to those currently configured for the <see cref="Style.Default" /> style.
        /// </summary>
        /// <seealso cref="StyleResetDefault" />
        public void StyleClearAll()
        {
            DirectMessage(NativeMethods.SCI_STYLECLEARALL);
        }

        

        /// <summary>
        /// Resets the <see cref="Style.Default" /> style to its initial state.
        /// </summary>
        /// <seealso cref="StyleClearAll" />
        public void StyleResetDefault()
        {
            DirectMessage(NativeMethods.SCI_STYLERESETDEFAULT);
        }

        /// <summary>
        /// Moves the caret to the opposite end of the main selection.
        /// </summary>
        public void SwapMainAnchorCaret()
        {
            DirectMessage(NativeMethods.SCI_SWAPMAINANCHORCARET);
        }

        /// <summary>
        /// Sets the <see cref="TargetStart" /> and <see cref="TargetEnd" /> to the start and end positions of the selection.
        /// </summary>
        /// <seealso cref="TargetWholeDocument" />
        public void TargetFromSelection()
        {
            DirectMessage(NativeMethods.SCI_TARGETFROMSELECTION);
        }

        /// <summary>
        /// Sets the <see cref="TargetStart" /> and <see cref="TargetEnd" /> to the start and end positions of the document.
        /// </summary>
        /// <seealso cref="TargetFromSelection" />
        public void TargetWholeDocument()
        {
            DirectMessage(NativeMethods.SCI_TARGETWHOLEDOCUMENT);
        }

        /// <summary>
        /// Measures the width in pixels of the specified string when rendered in the specified style.
        /// </summary>
        /// <param name="style">The index of the <see cref="Style" /> to use when rendering the text to measure.</param>
        /// <param name="text">The text to measure.</param>
        /// <returns>The width in pixels.</returns>
        public unsafe int TextWidth(int style, string text)
        {
            style = Helpers.Clamp(style, 0, Styles.Count - 1);
            var bytes = Helpers.GetBytes(text ?? string.Empty, Encoding, zeroTerminated: true);

            fixed (byte* bp = bytes)
            {
                return DirectMessage(NativeMethods.SCI_TEXTWIDTH, new IntPtr(style), new IntPtr(bp)).ToInt32();
            }
        }

        /// <summary>
        /// Undoes the previous action.
        /// </summary>
        public void Undo()
        {
            DirectMessage(NativeMethods.SCI_UNDO);
        }

        /// <summary>
        /// Determines whether to show the right-click context menu.
        /// </summary>
        /// <param name="enablePopup">true to enable the popup window; otherwise, false.</param>
        /// <seealso cref="UsePopup(PopupMode)" />
        public void UsePopup(bool enablePopup)
        {
            // NOTE: The behavior of UsePopup has changed in v3.7.1, however, this approach is still valid
            var bEnablePopup = (enablePopup ? new IntPtr(1) : IntPtr.Zero);
            DirectMessage(NativeMethods.SCI_USEPOPUP, bEnablePopup);
        }

        /// <summary>
        /// Determines the conditions for displaying the standard right-click context menu.
        /// </summary>
        /// <param name="popupMode">One of the <seealso cref="PopupMode" /> enumeration values.</param>
        public void UsePopup(PopupMode popupMode)
        {
            DirectMessage(NativeMethods.SCI_USEPOPUP, new IntPtr((int)popupMode));
        }

        private void WmDestroy(ref Message m)
        {
            // WM_DESTROY workaround
            if (reparent && IsHandleCreated)
            {
                // In some circumstances it's possible for the control's window handle to be destroyed
                // and recreated during the life of the control. I have no idea why Windows Forms was coded
                // this way but that creates an issue for us because most/all of our control state is stored
                // in the native Scintilla control (i.e. Handle) and to destroy it will bork us. So, rather
                // than destroying the handle as requested, we "reparent" ourselves to a message-only
                // (invisible) window to keep our handle alive. It doesn't appear that this causes any
                // issues to Windows Forms because it is completely unaware of it. When a control goes through
                // its regular (re)create handle process one of the steps is to assign the parent and so our
                // temporary bait-and-switch gets reconciled again automatically. Our Dispose method ensures
                // that we truly get destroyed when the time is right.

                NativeMethods.SetParent(Handle, new IntPtr(NativeMethods.HWND_MESSAGE));
                m.Result = IntPtr.Zero;
                return;
            }

            base.WndProc(ref m);
        }

        private void WmReflectNotify(ref Message m)
        {
            // A standard Windows notification and a Scintilla notification header are compatible
            NativeMethods.SCNotification scn = (NativeMethods.SCNotification)Marshal.PtrToStructure(m.LParam, typeof(NativeMethods.SCNotification));
            if (scn.nmhdr.code >= NativeMethods.SCN_STYLENEEDED && scn.nmhdr.code <= NativeMethods.SCN_AUTOCCOMPLETED)
            {
                var handler = Events[scNotificationEventKey] as EventHandler<SCNotificationEventArgs>;
                if (handler != null)
                    handler(this, new SCNotificationEventArgs(scn));

                switch (scn.nmhdr.code)
                {
                    case NativeMethods.SCN_PAINTED:
                        OnPainted(EventArgs.Empty);
                        break;

                    case NativeMethods.SCN_MODIFIED:
                        ScnModified(ref scn);
                        break;

                    case NativeMethods.SCN_MODIFYATTEMPTRO:
                        OnModifyAttempt(EventArgs.Empty);
                        break;

                    case NativeMethods.SCN_STYLENEEDED:
                        OnStyleNeeded(new StyleNeededEventArgs(this, scn.position));
                        break;

                    case NativeMethods.SCN_SAVEPOINTLEFT:
                        OnSavePointLeft(EventArgs.Empty);
                        break;

                    case NativeMethods.SCN_SAVEPOINTREACHED:
                        OnSavePointReached(EventArgs.Empty);
                        break;

                    case NativeMethods.SCN_MARGINCLICK:
                    case NativeMethods.SCN_MARGINRIGHTCLICK:
                        ScnMarginClick(ref scn);
                        break;

                    case NativeMethods.SCN_UPDATEUI:
                        OnUpdateUI(new UpdateUIEventArgs((UpdateChange)scn.updated));
                        break;

                    case NativeMethods.SCN_CHARADDED:
                        OnCharAdded(new CharAddedEventArgs(scn.ch));
                        break;

                    case NativeMethods.SCN_AUTOCSELECTION:
                        OnAutoCSelection(new AutoCSelectionEventArgs(this, scn.position, scn.text, scn.ch, (ListCompletionMethod)scn.listCompletionMethod));
                        break;

                    case NativeMethods.SCN_AUTOCCOMPLETED:
                        OnAutoCCompleted(new AutoCSelectionEventArgs(this, scn.position, scn.text, scn.ch, (ListCompletionMethod)scn.listCompletionMethod));
                        break;

                    case NativeMethods.SCN_AUTOCCANCELLED:
                        OnAutoCCancelled(EventArgs.Empty);
                        break;

                    case NativeMethods.SCN_AUTOCCHARDELETED:
                        OnAutoCCharDeleted(EventArgs.Empty);
                        break;

                    case NativeMethods.SCN_DWELLSTART:
                        OnDwellStart(new DwellEventArgs(this, scn.position, scn.x, scn.y));
                        break;

                    case NativeMethods.SCN_DWELLEND:
                        OnDwellEnd(new DwellEventArgs(this, scn.position, scn.x, scn.y));
                        break;

                    case NativeMethods.SCN_DOUBLECLICK:
                        ScnDoubleClick(ref scn);
                        break;

                    case NativeMethods.SCN_NEEDSHOWN:
                        OnNeedShown(new NeedShownEventArgs(this, scn.position, scn.length));
                        break;

                    case NativeMethods.SCN_HOTSPOTCLICK:
                    case NativeMethods.SCN_HOTSPOTDOUBLECLICK:
                    case NativeMethods.SCN_HOTSPOTRELEASECLICK:
                        ScnHotspotClick(ref scn);
                        break;

                    case NativeMethods.SCN_INDICATORCLICK:
                    case NativeMethods.SCN_INDICATORRELEASE:
                        ScnIndicatorClick(ref scn);
                        break;

                    case NativeMethods.SCN_ZOOM:
                        OnZoomChanged(EventArgs.Empty);
                        break;

                    default:
                        // Not our notification
                        base.WndProc(ref m);
                        break;
                }
            }
        }

        /// <summary>
        /// Processes Windows messages.
        /// </summary>
        /// <param name="m">The Windows Message to process.</param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case (NativeMethods.WM_REFLECT + NativeMethods.WM_NOTIFY):
                    WmReflectNotify(ref m);
                    break;

                case NativeMethods.WM_SETCURSOR:
                    DefWndProc(ref m);
                    break;

                case NativeMethods.WM_LBUTTONDBLCLK:
                case NativeMethods.WM_RBUTTONDBLCLK:
                case NativeMethods.WM_MBUTTONDBLCLK:
                case NativeMethods.WM_XBUTTONDBLCLK:
                    doubleClick = true;
                    goto default;

                case NativeMethods.WM_DESTROY:
                    WmDestroy(ref m);
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        /// <summary>
        /// Returns the position where a word ends, searching forward from the position specified.
        /// </summary>
        /// <param name="position">The zero-based document position to start searching from.</param>
        /// <param name="onlyWordCharacters">
        /// true to stop searching at the first non-word character regardless of whether the search started at a word or non-word character.
        /// false to use the first character in the search as a word or non-word indicator and then search for that word or non-word boundary.
        /// </param>
        /// <returns>The zero-based document postion of the word boundary.</returns>
        /// <seealso cref="WordStartPosition" />
        public int WordEndPosition(int position, bool onlyWordCharacters)
        {
            var onlyWordChars = (onlyWordCharacters ? new IntPtr(1) : IntPtr.Zero);
            position = Helpers.Clamp(position, 0, TextLength);
            position = Lines.CharToBytePosition(position);
            position = DirectMessage(NativeMethods.SCI_WORDENDPOSITION, new IntPtr(position), onlyWordChars).ToInt32();
            return Lines.ByteToCharPosition(position);
        }

        /// <summary>
        /// Returns the position where a word starts, searching backward from the position specified.
        /// </summary>
        /// <param name="position">The zero-based document position to start searching from.</param>
        /// <param name="onlyWordCharacters">
        /// true to stop searching at the first non-word character regardless of whether the search started at a word or non-word character.
        /// false to use the first character in the search as a word or non-word indicator and then search for that word or non-word boundary.
        /// </param>
        /// <returns>The zero-based document postion of the word boundary.</returns>
        /// <seealso cref="WordEndPosition" />
        public int WordStartPosition(int position, bool onlyWordCharacters)
        {
            var onlyWordChars = (onlyWordCharacters ? new IntPtr(1) : IntPtr.Zero);
            position = Helpers.Clamp(position, 0, TextLength);
            position = Lines.CharToBytePosition(position);
            position = DirectMessage(NativeMethods.SCI_WORDSTARTPOSITION, new IntPtr(position), onlyWordChars).ToInt32();
            return Lines.ByteToCharPosition(position);
        }

        /// <summary>
        /// Increases the zoom factor by 1 until it reaches 20 points.
        /// </summary>
        /// <seealso cref="Zoom" />
        public void ZoomIn()
        {
            DirectMessage(NativeMethods.SCI_ZOOMIN);
        }

        /// <summary>
        /// Decreases the zoom factor by 1 until it reaches -10 points.
        /// </summary>
        /// <seealso cref="Zoom" />
        public void ZoomOut()
        {
            DirectMessage(NativeMethods.SCI_ZOOMOUT);
        }

        #endregion Methods

        #region Helpers

        private void InitializeSyntaxColoringStyle()
        {
            this.StyleResetDefault();
            this.Styles[Style.Default].Font = font.Name;
            this.Styles[Style.Default].Size = (int)font.Size;
            this.Styles[Style.Default].BackColor = BackColor;
            this.Styles[Style.Default].ForeColor = foreColor;
            this.StyleClearAll();

            // Configure the CPP (C#) lexer styles
            this.Styles[Style.Cpp.Identifier].ForeColor = languageIdentifier;
            this.Styles[Style.Cpp.Comment].ForeColor = languageComment;
            this.Styles[Style.Cpp.CommentLine].ForeColor = languageCommentLine;
            this.Styles[Style.Cpp.CommentDoc].ForeColor = languageCommentDoc;
            this.Styles[Style.Cpp.Number].ForeColor = languageNumber;
            this.Styles[Style.Cpp.String].ForeColor = languageString;
            this.Styles[Style.Cpp.Character].ForeColor = languageCharacter;
            this.Styles[Style.Cpp.Preprocessor].ForeColor = languagePreprocessor;
            this.Styles[Style.Cpp.Operator].ForeColor = languageOperator;
            this.Styles[Style.Cpp.Regex].ForeColor = languageRegex;
            this.Styles[Style.Cpp.CommentLineDoc].ForeColor = languageCommentLineDoc;
            this.Styles[Style.Cpp.Word].ForeColor = languageWord;
            this.Styles[Style.Cpp.Word2].ForeColor = languageWord2;
            this.Styles[Style.Cpp.CommentDocKeyword].ForeColor = languageCommentDocKeyword;
            this.Styles[Style.Cpp.CommentDocKeywordError].ForeColor = languageCommentDocKeywordError;
            this.Styles[Style.Cpp.GlobalClass].ForeColor = languageGlobalClass;

            this.Lexer = Lexer.Cpp;

            this.SetKeywords(0,
                "class extends implements import interface new case do while else if for in switch throw get set function var try catch finally while with default break continue delete return each const namespace package include use is as instanceof typeof author copy default deprecated eventType example exampleText exception haxe inheritDoc internal link mtasc mxmlc param private return see serial serialData serialField since throws usage version langversion playerversion productversion dynamic private public partial static intrinsic internal native override protected AS3 final super this arguments null Infinity NaN undefined true false abstract as base bool break by byte case catch char checked class const continue decimal default delegate do double descending explicit event extern else enum false finally fixed float for foreach from goto group if implicit in int interface internal into is lock long new null namespace object operator out override orderby params private protected public readonly ref return switch struct sbyte sealed short sizeof stackalloc static string select this throw true try typeof uint ulong unchecked unsafe ushort using var virtual volatile void while where yield");
            this.SetKeywords(1,
                "void Null ArgumentError arguments Array Boolean Class Date DefinitionError Error EvalError Function int Math Namespace Number Object RangeError ReferenceError RegExp SecurityError String SyntaxError TypeError uint XML XMLList Boolean Byte Char DateTime Decimal Double Int16 Int32 Int64 IntPtr SByte Single UInt16 UInt32 UInt64 UIntPtr Void Path File System Windows Forms ScintillaNET");

        }

        private void InitializeNumberMargin()
        {

            this.Styles[Style.LineNumber].BackColor = IntToColor(BACK_COLOR);
            this.Styles[Style.LineNumber].ForeColor = IntToColor(FORE_COLOR);
            this.Styles[Style.IndentGuide].ForeColor = IntToColor(FORE_COLOR);
            this.Styles[Style.IndentGuide].BackColor = IntToColor(BACK_COLOR);

            var nums = this.Margins[NUMBER_MARGIN];
            nums.Width = 30;
            nums.Type = MarginType.Number;
            nums.Sensitive = true;
            nums.Mask = 0;


        }

        private void InitBookmarkMargin()
        {


            var margin = this.Margins[BOOKMARK_MARGIN];
            margin.Width = 20;
            margin.Sensitive = true;
            margin.Type = MarginType.Symbol;
            margin.Mask = (1 << BOOKMARK_MARKER);
            //margin.Cursor = MarginCursor.Arrow;

            var marker = this.Markers[BOOKMARK_MARKER];
            marker.Symbol = MarkerSymbol.Circle;
            marker.SetBackColor(IntToColor(0xFF003B));
            marker.SetForeColor(IntToColor(0x000000));
            marker.SetAlpha(100);

        }

        private void InitializeCodeFolding()
        {

            this.SetFoldMarginColor(true, IntToColor(BACK_COLOR));
            this.SetFoldMarginHighlightColor(true, IntToColor(BACK_COLOR));

            // Enable code folding
            this.SetProperty("fold", "1");
            this.SetProperty("fold.compact", "1");

            // Configure a margin to display folding symbols
            this.Margins[FOLDING_MARGIN].Type = MarginType.Symbol;
            this.Margins[FOLDING_MARGIN].Mask = Marker.MaskFolders;
            this.Margins[FOLDING_MARGIN].Sensitive = true;
            this.Margins[FOLDING_MARGIN].Width = 20;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                this.Markers[i].SetForeColor(IntToColor(BACK_COLOR)); // styles for [+] and [-]
                this.Markers[i].SetBackColor(IntToColor(FORE_COLOR)); // styles for [+] and [-]
            }

            // Configure folding markers with respective symbols
            this.Markers[Marker.Folder].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlus : MarkerSymbol.BoxPlus;
            this.Markers[Marker.FolderOpen].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinus : MarkerSymbol.BoxMinus;
            this.Markers[Marker.FolderEnd].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlusConnected : MarkerSymbol.BoxPlusConnected;
            this.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            this.Markers[Marker.FolderOpenMid].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinusConnected : MarkerSymbol.BoxMinusConnected;
            this.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            this.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Enable automatic folding
            this.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);

        }

        private void InitializeDragDropFile()
        {


            this.DragEnter += delegate (object sender, DragEventArgs e)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.None;
            };
            this.DragDrop += delegate (object sender, DragEventArgs e)
            {

                // get file drop
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {

                    Array a = (Array)e.Data.GetData(DataFormats.FileDrop);
                    if (a != null)
                    {

                        string path = a.GetValue(0).ToString();

                        LoadDataFromFile(path);

                    }
                }
            };

        }

        private void LoadDataFromFile(string path)
        {
            if (File.Exists(path))
            {
                this.Text = File.ReadAllText(path);
            }
        }

        private static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        #endregion

    }
}