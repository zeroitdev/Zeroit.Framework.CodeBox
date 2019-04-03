// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="Properties.cs" company="Zeroit Dev">
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
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Zeroit.Framework.CodeBox
{
    public partial class ZeroitCodeExplorer : Control
    {
        
        #region Properties

        /// <summary>
        /// Gets or sets the caret foreground color for additional selections.
        /// </summary>
        /// <returns>The caret foreground color in additional selections. The default is (127, 127, 127).</returns>
        [Category("Multiple Selection")]
        [Description("The additional caret foreground color.")]
        public Color AdditionalCaretForeColor
        {
            get
            {
                var color = DirectMessage(NativeMethods.SCI_GETADDITIONALCARETFORE).ToInt32();
                return ColorTranslator.FromWin32(color);
            }
            set
            {
                var color = ColorTranslator.ToWin32(value);
                DirectMessage(NativeMethods.SCI_SETADDITIONALCARETFORE, new IntPtr(color));
            }
        }

        /// <summary>
        /// Gets or sets whether the carets in additional selections will blink.
        /// </summary>
        /// <returns>true if additional selection carets should blink; otherwise, false. The default is true.</returns>
        [DefaultValue(true)]
        [Category("Multiple Selection")]
        [Description("Whether the carets in additional selections should blink.")]
        public bool AdditionalCaretsBlink
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_GETADDITIONALCARETSBLINK) != IntPtr.Zero;
            }
            set
            {
                var additionalCaretsBlink = (value ? new IntPtr(1) : IntPtr.Zero);
                DirectMessage(NativeMethods.SCI_SETADDITIONALCARETSBLINK, additionalCaretsBlink);
            }
        }

        /// <summary>
        /// Gets or sets whether the carets in additional selections are visible.
        /// </summary>
        /// <returns>true if additional selection carets are visible; otherwise, false. The default is true.</returns>
        [DefaultValue(true)]
        [Category("Multiple Selection")]
        [Description("Whether the carets in additional selections are visible.")]
        public bool AdditionalCaretsVisible
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_GETADDITIONALCARETSVISIBLE) != IntPtr.Zero;
            }
            set
            {
                var additionalCaretsBlink = (value ? new IntPtr(1) : IntPtr.Zero);
                DirectMessage(NativeMethods.SCI_SETADDITIONALCARETSVISIBLE, additionalCaretsBlink);
            }
        }

        /// <summary>
        /// Gets or sets the alpha transparency of additional multiple selections.
        /// </summary>
        /// <returns>
        /// The alpha transparency ranging from 0 (completely transparent) to 255 (completely opaque).
        /// The value 256 will disable alpha transparency. The default is 256.
        /// </returns>
        [DefaultValue(256)]
        [Category("Multiple Selection")]
        [Description("The transparency of additional selections.")]
        public int AdditionalSelAlpha
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_GETADDITIONALSELALPHA).ToInt32();
            }
            set
            {
                value = Helpers.Clamp(value, 0, NativeMethods.SC_ALPHA_NOALPHA);
                DirectMessage(NativeMethods.SCI_SETADDITIONALSELALPHA, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets whether additional typing affects multiple selections.
        /// </summary>
        /// <returns>true if typing will affect multiple selections instead of just the main selection; otherwise, false. The default is false.</returns>
        [DefaultValue(false)]
        [Category("Multiple Selection")]
        [Description("Whether typing, backspace, or delete works with multiple selection simultaneously.")]
        public bool AdditionalSelectionTyping
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_GETADDITIONALSELECTIONTYPING) != IntPtr.Zero;
            }
            set
            {
                var additionalSelectionTyping = (value ? new IntPtr(1) : IntPtr.Zero);
                DirectMessage(NativeMethods.SCI_SETADDITIONALSELECTIONTYPING, additionalSelectionTyping);
            }
        }

        /// <summary>
        /// Gets or sets the current anchor position.
        /// </summary>
        /// <returns>The zero-based character position of the anchor.</returns>
        /// <remarks>
        /// Setting the current anchor position will create a selection between it and the <see cref="CurrentPosition" />.
        /// The caret is not scrolled into view.
        /// </remarks>
        /// <seealso cref="ScrollCaret" />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int AnchorPosition
        {
            get
            {
                var bytePos = DirectMessage(NativeMethods.SCI_GETANCHOR).ToInt32();
                return Lines.ByteToCharPosition(bytePos);
            }
            set
            {
                value = Helpers.Clamp(value, 0, TextLength);
                var bytePos = Lines.CharToBytePosition(value);
                DirectMessage(NativeMethods.SCI_SETANCHOR, new IntPtr(bytePos));
            }
        }

        /// <summary>
        /// Gets or sets the display of annotations.
        /// </summary>
        /// <returns>One of the <see cref="Annotation" /> enumeration values. The default is <see cref="Annotation.Hidden" />.</returns>
        [DefaultValue(Annotation.Hidden)]
        [Category("Appearance")]
        [Description("Display and location of annotations.")]
        public Annotation AnnotationVisible
        {
            get
            {
                return (Annotation)DirectMessage(NativeMethods.SCI_ANNOTATIONGETVISIBLE).ToInt32();
            }
            set
            {
                var visible = (int)value;
                DirectMessage(NativeMethods.SCI_ANNOTATIONSETVISIBLE, new IntPtr(visible));
            }
        }

        /// <summary>
        /// Gets a value indicating whether there is an autocompletion list displayed.
        /// </summary>
        /// <returns>true if there is an active autocompletion list; otherwise, false.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AutoCActive
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_AUTOCACTIVE) != IntPtr.Zero;
            }
        }

        /// <summary>
        /// Gets or sets whether to automatically cancel autocompletion when there are no viable matches.
        /// </summary>
        /// <returns>
        /// true to automatically cancel autocompletion when there is no possible match; otherwise, false.
        /// The default is true.
        /// </returns>
        [DefaultValue(true)]
        [Category("Autocompletion")]
        [Description("Whether to automatically cancel autocompletion when no match is possible.")]
        public bool AutoCAutoHide
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_AUTOCGETAUTOHIDE) != IntPtr.Zero;
            }
            set
            {
                var autoHide = (value ? new IntPtr(1) : IntPtr.Zero);
                DirectMessage(NativeMethods.SCI_AUTOCSETAUTOHIDE, autoHide);
            }
        }

        /// <summary>
        /// Gets or sets whether to cancel an autocompletion if the caret moves from its initial location,
        /// or is allowed to move to the word start.
        /// </summary>
        /// <returns>
        /// true to cancel autocompletion when the caret moves.
        /// false to allow the caret to move to the beginning of the word without cancelling autocompletion.
        /// </returns>
        [DefaultValue(true)]
        [Category("Autocompletion")]
        [Description("Whether to cancel an autocompletion if the caret moves from its initial location, or is allowed to move to the word start.")]
        public bool AutoCCancelAtStart
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_AUTOCGETCANCELATSTART) != IntPtr.Zero;
            }
            set
            {
                var cancel = (value ? new IntPtr(1) : IntPtr.Zero);
                DirectMessage(NativeMethods.SCI_AUTOCSETCANCELATSTART, cancel);
            }
        }

        /// <summary>
        /// Gets the index of the current autocompletion list selection.
        /// </summary>
        /// <returns>The zero-based index of the current autocompletion selection.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int AutoCCurrent
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_AUTOCGETCURRENT).ToInt32();
            }
        }

        /// <summary>
        /// Gets or sets whether to automatically select an item when it is the only one in an autocompletion list.
        /// </summary>
        /// <returns>
        /// true to automatically choose the only autocompletion item and not display the list; otherwise, false.
        /// The default is false.
        /// </returns>
        [DefaultValue(false)]
        [Category("Autocompletion")]
        [Description("Whether to automatically choose an autocompletion item when it is the only one in the list.")]
        public bool AutoCChooseSingle
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_AUTOCGETCHOOSESINGLE) != IntPtr.Zero;
            }
            set
            {
                var chooseSingle = (value ? new IntPtr(1) : IntPtr.Zero);
                DirectMessage(NativeMethods.SCI_AUTOCSETCHOOSESINGLE, chooseSingle);
            }
        }

        /// <summary>
        /// Gets or sets whether to delete any word characters following the caret after an autocompletion.
        /// </summary>
        /// <returns>
        /// true to delete any word characters following the caret after autocompletion; otherwise, false.
        /// The default is false.</returns>
        [DefaultValue(false)]
        [Category("Autocompletion")]
        [Description("Whether to delete any existing word characters following the caret after autocompletion.")]
        public bool AutoCDropRestOfWord
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_AUTOCGETDROPRESTOFWORD) != IntPtr.Zero;
            }
            set
            {
                var dropRestOfWord = (value ? new IntPtr(1) : IntPtr.Zero);
                DirectMessage(NativeMethods.SCI_AUTOCSETDROPRESTOFWORD, dropRestOfWord);
            }
        }

        /// <summary>
        /// Gets or sets whether matching characters to an autocompletion list is case-insensitive.
        /// </summary>
        /// <returns>true to use case-insensitive matching; otherwise, false. The default is false.</returns>
        [DefaultValue(false)]
        [Category("Autocompletion")]
        [Description("Whether autocompletion word matching can ignore case.")]
        public bool AutoCIgnoreCase
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_AUTOCGETIGNORECASE) != IntPtr.Zero;
            }
            set
            {
                var ignoreCase = (value ? new IntPtr(1) : IntPtr.Zero);
                DirectMessage(NativeMethods.SCI_AUTOCSETIGNORECASE, ignoreCase);
            }
        }

        /// <summary>
        /// Gets or sets the maximum height of the autocompletion list measured in rows.
        /// </summary>
        /// <returns>The max number of rows to display in an autocompletion window. The default is 5.</returns>
        /// <remarks>If there are more items in the list than max rows, a vertical scrollbar is shown.</remarks>
        [DefaultValue(5)]
        [Category("Autocompletion")]
        [Description("The maximum number of rows to display in an autocompletion list.")]
        public int AutoCMaxHeight
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_AUTOCGETMAXHEIGHT).ToInt32();
            }
            set
            {
                value = Helpers.ClampMin(value, 0);
                DirectMessage(NativeMethods.SCI_AUTOCSETMAXHEIGHT, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets the width in characters of the autocompletion list.
        /// </summary>
        /// <returns>
        /// The width of the autocompletion list expressed in characters, or 0 to automatically set the width
        /// to the longest item. The default is 0.
        /// </returns>
        /// <remarks>Any items that cannot be fully displayed will be indicated with ellipsis.</remarks>
        [DefaultValue(0)]
        [Category("Autocompletion")]
        [Description("The width of the autocompletion list measured in characters.")]
        public int AutoCMaxWidth
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_AUTOCGETMAXWIDTH).ToInt32();
            }
            set
            {
                value = Helpers.ClampMin(value, 0);
                DirectMessage(NativeMethods.SCI_AUTOCSETMAXWIDTH, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets the autocompletion list sort order to expect when calling <see cref="AutoCShow" />.
        /// </summary>
        /// <returns>One of the <see cref="Order" /> enumeration values. The default is <see cref="Order.Presorted" />.</returns>
        [DefaultValue(Order.Presorted)]
        [Category("Autocompletion")]
        [Description("The order of words in an autocompletion list.")]
        public Order AutoCOrder
        {
            get
            {
                return (Order)DirectMessage(NativeMethods.SCI_AUTOCGETORDER).ToInt32();
            }
            set
            {
                var order = (int)value;
                DirectMessage(NativeMethods.SCI_AUTOCSETORDER, new IntPtr(order));
            }
        }

        /// <summary>
        /// Gets the document position at the time <see cref="AutoCShow" /> was called.
        /// </summary>
        /// <returns>The zero-based document position at the time <see cref="AutoCShow" /> was called.</returns>
        /// <seealso cref="AutoCShow" />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int AutoCPosStart
        {
            get
            {
                var pos = DirectMessage(NativeMethods.SCI_AUTOCPOSSTART).ToInt32();
                pos = Lines.ByteToCharPosition(pos);

                return pos;
            }
        }

        /// <summary>
        /// Gets or sets the delimiter character used to separate words in an autocompletion list.
        /// </summary>
        /// <returns>The separator character used when calling <see cref="AutoCShow" />. The default is the space character.</returns>
        /// <remarks>The <paramref name="value" /> specified should be limited to printable ASCII characters.</remarks>
        [DefaultValue(' ')]
        [Category("Autocompletion")]
        [Description("The autocompletion list word delimiter. The default is a space character.")]
        public Char AutoCSeparator
        {
            get
            {
                var separator = DirectMessage(NativeMethods.SCI_AUTOCGETSEPARATOR).ToInt32();
                return (Char)separator;
            }
            set
            {
                // The autocompletion separator character is stored as a byte within Scintilla,
                // not a character. Thus it's possible for a user to supply a character that does
                // not fit within a single byte. The likelyhood of this, however, seems so remote that
                // I'm willing to risk a possible conversion error to provide a better user experience.
                var separator = (byte)value;
                DirectMessage(NativeMethods.SCI_AUTOCSETSEPARATOR, new IntPtr(separator));
            }
        }

        /// <summary>
        /// Gets or sets the delimiter character used to separate words and image type identifiers in an autocompletion list.
        /// </summary>
        /// <returns>The separator character used to reference an image registered with <see cref="RegisterRgbaImage" />. The default is '?'.</returns>
        /// <remarks>The <paramref name="value" /> specified should be limited to printable ASCII characters.</remarks>
        [DefaultValue('?')]
        [Category("Autocompletion")]
        [Description("The autocompletion list image type delimiter.")]
        public Char AutoCTypeSeparator
        {
            get
            {
                var separatorCharacter = DirectMessage(NativeMethods.SCI_AUTOCGETTYPESEPARATOR).ToInt32();
                return (Char)separatorCharacter;
            }
            set
            {
                // The autocompletion type separator character is stored as a byte within Scintilla,
                // not a character. Thus it's possible for a user to supply a character that does
                // not fit within a single byte. The likelyhood of this, however, seems so remote that
                // I'm willing to risk a possible conversion error to provide a better user experience.
                var separatorCharacter = (byte)value;
                DirectMessage(NativeMethods.SCI_AUTOCSETTYPESEPARATOR, new IntPtr(separatorCharacter));
            }
        }

        /// <summary>
        /// Gets or sets the automatic folding flags.
        /// </summary>
        /// <returns>
        /// A bitwise combination of the <see cref="Zeroit.Framework.CodeBox.AutomaticFold" /> enumeration.
        /// The default is <see cref="Zeroit.Framework.CodeBox.AutomaticFold.None" />.
        /// </returns>
        [DefaultValue(AutomaticFold.None)]
        [Category("Behavior")]
        [Description("Options for allowing the control to automatically handle folding.")]
        [TypeConverter(typeof(FlagsEnumTypeConverter.FlagsEnumConverter))]
        public AutomaticFold AutomaticFold
        {
            get
            {
                return (AutomaticFold)DirectMessage(NativeMethods.SCI_GETAUTOMATICFOLD);
            }
            set
            {
                var automaticFold = (int)value;
                DirectMessage(NativeMethods.SCI_SETAUTOMATICFOLD, new IntPtr(automaticFold));
            }
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        [Browsable(true)]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                Styles[Style.Default].BackColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override Image BackgroundImage
        {
            get
            {
                return base.BackgroundImage;
            }
            set
            {
                base.BackgroundImage = value;
            }
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override ImageLayout BackgroundImageLayout
        {
            get
            {
                return base.BackgroundImageLayout;
            }
            set
            {
                base.BackgroundImageLayout = value;
            }
        }

        /// <summary>
        /// Gets or sets the border type of the <see cref="Scintilla" /> control.
        /// </summary>
        /// <returns>A BorderStyle enumeration value that represents the border type of the control. The default is Fixed3D.</returns>
        /// <exception cref="InvalidEnumArgumentException">A value that is not within the range of valid values for the enumeration was assigned to the property.</exception>
        [Category("Appearance")]
        [Description("Indicates whether the control should have a border.")]
        public Border BorderStyle
        {
            get
            {
                return borderStyle;
            }
            set
            {
                if (borderStyle != value)
                {
                    //if (!Enum.IsDefined(typeof(BorderStyle), value))
                    //    throw new InvalidEnumArgumentException("value", (int)value, typeof(BorderStyle));

                    borderStyle = value;
                    UpdateStyles();
                    OnBorderStyleChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets whether drawing is double-buffered.
        /// </summary>
        /// <returns>
        /// true to draw each line into an offscreen bitmap first before copying it to the screen; otherwise, false.
        /// The default is true.
        /// </returns>
        /// <remarks>Disabling buffer can improve performance but will cause flickering.</remarks>
        [DefaultValue(true)]
        [Category("Misc")]
        [Description("Determines whether drawing is double-buffered.")]
        public bool BufferedDraw
        {
            get
            {
                return (DirectMessage(NativeMethods.SCI_GETBUFFEREDDRAW) != IntPtr.Zero);
            }
            set
            {
                var isBuffered = (value ? new IntPtr(1) : IntPtr.Zero);
                DirectMessage(NativeMethods.SCI_SETBUFFEREDDRAW, isBuffered);
            }
        }

        /*
        /// <summary>
        /// Gets or sets the current position of a call tip.
        /// </summary>
        /// <returns>The zero-based document position indicated when <see cref="CallTipShow" /> was called to display a call tip.</returns>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int CallTipPosStart
        {
            get
            {
                var pos = DirectMessage(NativeMethods.SCI_CALLTIPPOSSTART).ToInt32();
                if (pos < 0)
                    return pos;

                return Lines.ByteToCharPosition(pos);
            }
            set
            {
                value = Helpers.Clamp(value, 0, TextLength);
                value = Lines.CharToBytePosition(value);
                DirectMessage(NativeMethods.SCI_CALLTIPSETPOSSTART, new IntPtr(value));
            }
        }
        */

        /// <summary>
        /// Gets a value indicating whether there is a call tip window displayed.
        /// </summary>
        /// <returns>true if there is an active call tip window; otherwise, false.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CallTipActive
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_CALLTIPACTIVE) != IntPtr.Zero;
            }
        }

        /// <summary>
        /// Gets a value indicating whether there is text on the clipboard that can be pasted into the document.
        /// </summary>
        /// <returns>true when there is text on the clipboard to paste; otherwise, false.</returns>
        /// <remarks>The document cannot be <see cref="ReadOnly" />  and the selection cannot contain protected text.</remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanPaste
        {
            get
            {
                return (DirectMessage(NativeMethods.SCI_CANPASTE) != IntPtr.Zero);
            }
        }

        /// <summary>
        /// Gets a value indicating whether there is an undo action to redo.
        /// </summary>
        /// <returns>true when there is something to redo; otherwise, false.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanRedo
        {
            get
            {
                return (DirectMessage(NativeMethods.SCI_CANREDO) != IntPtr.Zero);
            }
        }

        /// <summary>
        /// Gets a value indicating whether there is an action to undo.
        /// </summary>
        /// <returns>true when there is something to undo; otherwise, false.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanUndo
        {
            get
            {
                return (DirectMessage(NativeMethods.SCI_CANUNDO) != IntPtr.Zero);
            }
        }

        /// <summary>
        /// Gets or sets the caret foreground color.
        /// </summary>
        /// <returns>The caret foreground color. The default is black.</returns>
        [DefaultValue(typeof(Color), "Black")]
        [Category("Caret")]
        [Description("The caret foreground color.")]
        public Color CaretForeColor
        {
            get
            {
                var color = DirectMessage(NativeMethods.SCI_GETCARETFORE).ToInt32();
                return ColorTranslator.FromWin32(color);
            }
            set
            {
                var color = ColorTranslator.ToWin32(value);
                DirectMessage(NativeMethods.SCI_SETCARETFORE, new IntPtr(color));
            }
        }

        /// <summary>
        /// Gets or sets the caret line background color.
        /// </summary>
        /// <returns>The caret line background color. The default is yellow.</returns>
        [DefaultValue(typeof(Color), "Yellow")]
        [Category("Caret")]
        [Description("The background color of the current line.")]
        public Color CaretLineBackColor
        {
            get
            {
                var color = DirectMessage(NativeMethods.SCI_GETCARETLINEBACK).ToInt32();
                return ColorTranslator.FromWin32(color);
            }
            set
            {
                var color = ColorTranslator.ToWin32(value);
                DirectMessage(NativeMethods.SCI_SETCARETLINEBACK, new IntPtr(color));
            }
        }

        /// <summary>
        /// Gets or sets the alpha transparency of the <see cref="CaretLineBackColor" />.
        /// </summary>
        /// <returns>
        /// The alpha transparency ranging from 0 (completely transparent) to 255 (completely opaque).
        /// The value 256 will disable alpha transparency. The default is 256.
        /// </returns>
        [DefaultValue(256)]
        [Category("Caret")]
        [Description("The transparency of the current line background color.")]
        public int CaretLineBackColorAlpha
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_GETCARETLINEBACKALPHA).ToInt32();
            }
            set
            {
                value = Helpers.Clamp(value, 0, NativeMethods.SC_ALPHA_NOALPHA);
                DirectMessage(NativeMethods.SCI_SETCARETLINEBACKALPHA, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets the width of the caret line frame.
        /// </summary>
        /// <returns><see cref="CaretLineVisible" /> must be set to true. A value of 0 disables the frame. The default is 0.</returns>
        [DefaultValue(0)]
        [Category("Caret")]
        [Description("The Width of the current line frame.")]
        public int CaretLineFrame
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_GETCARETLINEFRAME).ToInt32();
            }
            set
            {
                value = Helpers.ClampMin(value, 0);
                DirectMessage(NativeMethods.SCI_SETCARETLINEFRAME, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets whether the caret line is visible (highlighted).
        /// </summary>
        /// <returns>true if the caret line is visible; otherwise, false. The default is false.</returns>
        [DefaultValue(false)]
        [Category("Caret")]
        [Description("Determines whether to highlight the current caret line.")]
        public bool CaretLineVisible
        {
            get
            {
                return (DirectMessage(NativeMethods.SCI_GETCARETLINEVISIBLE) != IntPtr.Zero);
            }
            set
            {
                var visible = (value ? new IntPtr(1) : IntPtr.Zero);
                DirectMessage(NativeMethods.SCI_SETCARETLINEVISIBLE, visible);
            }
        }

        /// <summary>
        /// Gets or sets whether the caret line is always visible even when the window is not in focus.
        /// </summary>
        /// <returns>true if the caret line is always visible; otherwise, false. The default is false.</returns>
        [DefaultValue(false)]
        [Category("Caret")]
        [Description("Determines whether the caret line always visible even when the window is not in focus..")]
        public bool CaretLineVisibleAlways
        {
            get
            {
                return (DirectMessage(NativeMethods.SCI_GETCARETLINEVISIBLEALWAYS) != IntPtr.Zero);
            }
            set
            {
                var visibleAlways = (value ? new IntPtr(1) : IntPtr.Zero);
                DirectMessage(NativeMethods.SCI_SETCARETLINEVISIBLEALWAYS, visibleAlways);
            }
        }

        /// <summary>
        /// Gets or sets the caret blink rate in milliseconds.
        /// </summary>
        /// <returns>The caret blink rate measured in milliseconds. The default is 530.</returns>
        /// <remarks>A value of 0 will stop the caret blinking.</remarks>
        [DefaultValue(530)]
        [Category("Caret")]
        [Description("The caret blink rate in milliseconds.")]
        public int CaretPeriod
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_GETCARETPERIOD).ToInt32();
            }
            set
            {
                value = Helpers.ClampMin(value, 0);
                DirectMessage(NativeMethods.SCI_SETCARETPERIOD, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets the caret display style.
        /// </summary>
        /// <returns>
        /// One of the <see cref="Zeroit.Framework.CodeBox.CaretStyle" /> enumeration values.
        /// The default is <see cref="Zeroit.Framework.CodeBox.CaretStyle.Line" />.
        /// </returns>
        [DefaultValue(CaretStyle.Line)]
        [Category("Caret")]
        [Description("The caret display style.")]
        public CaretStyle CaretStyle
        {
            get
            {
                return (CaretStyle)DirectMessage(NativeMethods.SCI_GETCARETSTYLE).ToInt32();
            }
            set
            {
                var style = (int)value;
                DirectMessage(NativeMethods.SCI_SETCARETSTYLE, new IntPtr(style));
            }
        }

        /// <summary>
        /// Gets or sets the width in pixels of the caret.
        /// </summary>
        /// <returns>The width of the caret in pixels. The default is 1 pixel.</returns>
        /// <remarks>
        /// The caret width can only be set to a value of 0, 1, 2 or 3 pixels and is only effective
        /// when the <see cref="CaretStyle" /> property is set to <see cref="Zeroit.Framework.CodeBox.CaretStyle.Line" />.
        /// </remarks>
        [DefaultValue(1)]
        [Category("Caret")]
        [Description("The width of the caret line measured in pixels (between 0 and 3).")]
        public int CaretWidth
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_GETCARETWIDTH).ToInt32();
            }
            set
            {
                value = Helpers.Clamp(value, 0, 3);
                DirectMessage(NativeMethods.SCI_SETCARETWIDTH, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets the required creation parameters when the control handle is created.
        /// </summary>
        /// <returns>A CreateParams that contains the required creation parameters when the handle to the control is created.</returns>
        protected override CreateParams CreateParams
        {
            get
            {
                if (moduleHandle == IntPtr.Zero)
                {
                    var path = GetModulePath();

                    // Load the native Scintilla library
                    moduleHandle = NativeMethods.LoadLibrary(path);
                    if (moduleHandle == IntPtr.Zero)
                    {
                        var message = string.Format(CultureInfo.InvariantCulture, "Could not load the Scintilla module at the path '{0}'.", path);
                        throw new Win32Exception(message, new Win32Exception()); // Calls GetLastError
                    }

                    // Get the native Scintilla direct function -- the only function the library exports
                    var directFunctionPointer = NativeMethods.GetProcAddress(new HandleRef(this, moduleHandle), "Scintilla_DirectFunction");
                    if (directFunctionPointer == IntPtr.Zero)
                    {
                        var message = "The Scintilla module has no export for the 'Scintilla_DirectFunction' procedure.";
                        throw new Win32Exception(message, new Win32Exception()); // Calls GetLastError
                    }

                    // Create a managed callback
                    directFunction = (NativeMethods.Scintilla_DirectFunction)Marshal.GetDelegateForFunctionPointer(
                        directFunctionPointer,
                        typeof(NativeMethods.Scintilla_DirectFunction));
                }

                CreateParams cp = base.CreateParams;
                cp.ClassName = "Scintilla";

                // The border effect is achieved through a native Windows style
                cp.ExStyle &= (~NativeMethods.WS_EX_CLIENTEDGE);
                cp.Style &= (~NativeMethods.WS_BORDER);
                switch (borderStyle)
                {
                    case Border.Fixed3D:
                        cp.ExStyle |= NativeMethods.WS_EX_CLIENTEDGE;
                        break;
                    case Border.FixedSingle:
                        cp.Style |= NativeMethods.WS_BORDER;
                        break;
                    case Border.None:
                        break;
                    case Border.Adjust:
                        break;
                    case Border.Bump:
                        break;
                    case Border.Flat:
                        break;
                    case Border.Etched:
                        break;
                    case Border.Raised:
                        break;
                    case Border.RaisedInner:
                        break;
                    case Border.RaisedOuter:
                        break;
                    case Border.Sunken:
                        break;
                    case Border.SunkenInner:
                        break;
                    case Border.SunkenOuter:
                        break;
                    case Border.Custom:
                        break;
                }

                return cp;
            }
        }

        /// <summary>
        /// Gets the current line index.
        /// </summary>
        /// <returns>The zero-based line index containing the <see cref="CurrentPosition" />.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CurrentLine
        {
            get
            {
                var currentPos = DirectMessage(NativeMethods.SCI_GETCURRENTPOS).ToInt32();
                var line = DirectMessage(NativeMethods.SCI_LINEFROMPOSITION, new IntPtr(currentPos)).ToInt32();
                return line;
            }
        }

        /// <summary>
        /// Gets or sets the current caret position.
        /// </summary>
        /// <returns>The zero-based character position of the caret.</returns>
        /// <remarks>
        /// Setting the current caret position will create a selection between it and the current <see cref="AnchorPosition" />.
        /// The caret is not scrolled into view.
        /// </remarks>
        /// <seealso cref="ScrollCaret" />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CurrentPosition
        {
            get
            {
                var bytePos = DirectMessage(NativeMethods.SCI_GETCURRENTPOS).ToInt32();
                return Lines.ByteToCharPosition(bytePos);
            }
            set
            {
                value = Helpers.Clamp(value, 0, TextLength);
                var bytePos = Lines.CharToBytePosition(value);
                DirectMessage(NativeMethods.SCI_SETCURRENTPOS, new IntPtr(bytePos));
            }
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override Cursor Cursor
        {
            get
            {
                return base.Cursor;
            }
            set
            {
                base.Cursor = value;
            }
        }

        /// <summary>
        /// Gets or sets the default cursor for the control.
        /// </summary>
        /// <returns>An object of type Cursor representing the current default cursor.</returns>
        protected override Cursor DefaultCursor
        {
            get
            {
                return Cursors.IBeam;
            }
        }

        /// <summary>
        /// Gets the default size of the control.
        /// </summary>
        /// <returns>The default Size of the control.</returns>
        protected override Size DefaultSize
        {
            get
            {
                // I've discovered that using a DefaultSize property other than 'empty' triggers a flaw (IMO)
                // in Windows Forms that will cause CreateParams to be called in the base constructor.
                // That's too early. It makes it impossible to use the Site or DesignMode properties during
                // handle creation because they haven't been set yet. Since we don't currently depend on those
                // properties it's okay, but if we need them this is the place to start fixing things.

                return new Size(200, 100);
            }
        }

        /// <summary>
        /// Gets or sets the current document used by the control.
        /// </summary>
        /// <returns>The current <see cref="Document" />.</returns>
        /// <remarks>
        /// Setting this property is equivalent to calling <see cref="ReleaseDocument" /> on the current document, and
        /// calling <see cref="CreateDocument" /> if the new <paramref name="value" /> is <see cref="Zeroit.Framework.CodeBox.Document.Empty" /> or
        /// <see cref="AddRefDocument" /> if the new <paramref name="value" /> is not <see cref="Zeroit.Framework.CodeBox.Document.Empty" />.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Document Document
        {
            get
            {
                var ptr = DirectMessage(NativeMethods.SCI_GETDOCPOINTER);
                return new Document { Value = ptr };
            }
            set
            {
                var eolMode = EolMode;
                var useTabs = UseTabs;
                var tabWidth = TabWidth;
                var indentWidth = IndentWidth;

                var ptr = value.Value;
                DirectMessage(NativeMethods.SCI_SETDOCPOINTER, IntPtr.Zero, ptr);

                // Carry over properties to new document
                InitDocument(eolMode, useTabs, tabWidth, indentWidth);

                // Rebuild the line cache
                Lines.RebuildLineData();
            }
        }

        /// <summary>
        /// Gets or sets the background color to use when indicating long lines with
        /// <see cref="Zeroit.Framework.CodeBox.EdgeMode.Background" />.
        /// </summary>
        /// <returns>The background Color. The default is Silver.</returns>
        [DefaultValue(typeof(Color), "Silver")]
        [Category("Long Lines")]
        [Description("The background color to use when indicating long lines.")]
        public Color EdgeColor
        {
            get
            {
                var color = DirectMessage(NativeMethods.SCI_GETEDGECOLOUR).ToInt32();
                return ColorTranslator.FromWin32(color);
            }
            set
            {
                var color = ColorTranslator.ToWin32(value);
                DirectMessage(NativeMethods.SCI_SETEDGECOLOUR, new IntPtr(color));
            }
        }

        /// <summary>
        /// Gets or sets the column number at which to begin indicating long lines.
        /// </summary>
        /// <returns>The number of columns in a long line. The default is 0.</returns>
        /// <remarks>
        /// When using <see cref="Zeroit.Framework.CodeBox.EdgeMode.Line"/>, a column is defined as the width of a space character in the <see cref="Style.Default" /> style.
        /// When using <see cref="Zeroit.Framework.CodeBox.EdgeMode.Background" /> a column is equal to a character (including tabs).
        /// </remarks>
        [DefaultValue(0)]
        [Category("Long Lines")]
        [Description("The number of columns at which to display long line indicators.")]
        public int EdgeColumn
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_GETEDGECOLUMN).ToInt32();
            }
            set
            {
                value = Helpers.ClampMin(value, 0);
                DirectMessage(NativeMethods.SCI_SETEDGECOLUMN, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets the mode for indicating long lines.
        /// </summary>
        /// <returns>
        /// One of the <see cref="Zeroit.Framework.CodeBox.EdgeMode" /> enumeration values.
        /// The default is <see cref="Zeroit.Framework.CodeBox.EdgeMode.None" />.
        /// </returns>
        [DefaultValue(EdgeMode.None)]
        [Category("Long Lines")]
        [Description("Determines how long lines are indicated.")]
        public EdgeMode EdgeMode
        {
            get
            {
                return (EdgeMode)DirectMessage(NativeMethods.SCI_GETEDGEMODE);
            }
            set
            {
                var edgeMode = (int)value;
                DirectMessage(NativeMethods.SCI_SETEDGEMODE, new IntPtr(edgeMode));
            }
        }

        internal Encoding Encoding
        {
            get
            {
                // Should always be UTF-8 unless someone has done an end run around us
                int codePage = (int)DirectMessage(NativeMethods.SCI_GETCODEPAGE);
                return (codePage == 0 ? Encoding.Default : Encoding.GetEncoding(codePage));
            }
        }

        /// <summary>
        /// Gets or sets whether vertical scrolling ends at the last line or can scroll past.
        /// </summary>
        /// <returns>true if the maximum vertical scroll position ends at the last line; otherwise, false. The default is true.</returns>
        [DefaultValue(true)]
        [Category("Scrolling")]
        [Description("Determines whether the maximum vertical scroll position ends at the last line or can scroll past.")]
        public bool EndAtLastLine
        {
            get
            {
                return (DirectMessage(NativeMethods.SCI_GETENDATLASTLINE) != IntPtr.Zero);
            }
            set
            {
                var endAtLastLine = (value ? new IntPtr(1) : IntPtr.Zero);
                DirectMessage(NativeMethods.SCI_SETENDATLASTLINE, endAtLastLine);
            }
        }

        /// <summary>
        /// Gets or sets the end-of-line mode, or rather, the characters added into
        /// the document when the user presses the Enter key.
        /// </summary>
        /// <returns>One of the <see cref="Eol" /> enumeration values. The default is <see cref="Eol.CrLf" />.</returns>
        [DefaultValue(Eol.CrLf)]
        [Category("Line Endings")]
        [Description("Determines the characters added into the document when the user presses the Enter key.")]
        public Eol EolMode
        {
            get
            {
                return (Eol)DirectMessage(NativeMethods.SCI_GETEOLMODE);
            }
            set
            {
                var eolMode = (int)value;
                DirectMessage(NativeMethods.SCI_SETEOLMODE, new IntPtr(eolMode));
            }
        }

        /// <summary>
        /// Gets or sets the amount of whitespace added to the ascent (top) of each line.
        /// </summary>
        /// <returns>The extra line ascent. The default is zero.</returns>
        [DefaultValue(0)]
        [Category("Whitespace")]
        [Description("Extra whitespace added to the ascent (top) of each line.")]
        public int ExtraAscent
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_GETEXTRAASCENT).ToInt32();
            }
            set
            {
                DirectMessage(NativeMethods.SCI_SETEXTRAASCENT, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets the amount of whitespace added to the descent (bottom) of each line.
        /// </summary>
        /// <returns>The extra line descent. The default is zero.</returns>
        [DefaultValue(0)]
        [Category("Whitespace")]
        [Description("Extra whitespace added to the descent (bottom) of each line.")]
        public int ExtraDescent
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_GETEXTRADESCENT).ToInt32();
            }
            set
            {
                DirectMessage(NativeMethods.SCI_SETEXTRADESCENT, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets the first visible line on screen.
        /// </summary>
        /// <returns>The zero-based index of the first visible screen line.</returns>
        /// <remarks>The value is a visible line, not a document line.</remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int FirstVisibleLine
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_GETFIRSTVISIBLELINE).ToInt32();
            }
            set
            {
                value = Helpers.ClampMin(value, 0);
                DirectMessage(NativeMethods.SCI_SETFIRSTVISIBLELINE, new IntPtr(value));
            }
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
            }
        }

        /// <summary>
        /// Gets or sets font quality (anti-aliasing method) used to render fonts.
        /// </summary>
        /// <returns>
        /// One of the <see cref="Zeroit.Framework.CodeBox.FontQuality" /> enumeration values.
        /// The default is <see cref="Zeroit.Framework.CodeBox.FontQuality.Default" />.
        /// </returns>
        [DefaultValue(FontQuality.Default)]
        [Category("Misc")]
        [Description("Specifies the anti-aliasing method to use when rendering fonts.")]
        public FontQuality FontQuality
        {
            get
            {
                return (FontQuality)DirectMessage(NativeMethods.SCI_GETFONTQUALITY);
            }
            set
            {
                var fontQuality = (int)value;
                DirectMessage(NativeMethods.SCI_SETFONTQUALITY, new IntPtr(fontQuality));
            }
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the column number of the indentation guide to highlight.
        /// </summary>
        /// <returns>The column number of the indentation guide to highlight or 0 if disabled.</returns>
        /// <remarks>Guides are highlighted in the <see cref="Style.BraceLight" /> style. Column numbers can be determined by calling <see cref="GetColumn" />.</remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int HighlightGuide
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_GETHIGHLIGHTGUIDE).ToInt32();
            }
            set
            {
                value = Helpers.ClampMin(value, 0);
                DirectMessage(NativeMethods.SCI_SETHIGHLIGHTGUIDE, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets whether to display the horizontal scroll bar.
        /// </summary>
        /// <returns>true to display the horizontal scroll bar when needed; otherwise, false. The default is true.</returns>
        [DefaultValue(true)]
        [Category("Scrolling")]
        [Description("Determines whether to show the horizontal scroll bar if needed.")]
        public bool HScrollBar
        {
            get
            {
                return (DirectMessage(NativeMethods.SCI_GETHSCROLLBAR) != IntPtr.Zero);
            }
            set
            {
                var visible = (value ? new IntPtr(1) : IntPtr.Zero);
                DirectMessage(NativeMethods.SCI_SETHSCROLLBAR, visible);
            }
        }

        /// <summary>
        /// Gets or sets the strategy used to perform styling using application idle time.
        /// </summary>
        /// <returns>
        /// One of the <see cref="Zeroit.Framework.CodeBox.IdleStyling" /> enumeration values.
        /// The default is <see cref="Zeroit.Framework.CodeBox.IdleStyling.None" />.
        /// </returns>
        [DefaultValue(IdleStyling.None)]
        [Category("Misc")]
        [Description("Specifies how to use application idle time for styling.")]
        public IdleStyling IdleStyling
        {
            get
            {
                return (IdleStyling)DirectMessage(NativeMethods.SCI_GETIDLESTYLING);
            }
            set
            {
                var idleStyling = (int)value;
                DirectMessage(NativeMethods.SCI_SETIDLESTYLING, new IntPtr(idleStyling));
            }
        }

        /// <summary>
        /// Gets or sets the size of indentation in terms of space characters.
        /// </summary>
        /// <returns>The indentation size measured in characters. The default is 0.</returns>
        /// <remarks> A value of 0 will make the indent width the same as the tab width.</remarks>
        [DefaultValue(0)]
        [Category("Indentation")]
        [Description("The indentation size in characters or 0 to make it the same as the tab width.")]
        public int IndentWidth
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_GETINDENT).ToInt32();
            }
            set
            {
                value = Helpers.ClampMin(value, 0);
                DirectMessage(NativeMethods.SCI_SETINDENT, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets whether to display indentation guides.
        /// </summary>
        /// <returns>One of the <see cref="IndentView" /> enumeration values. The default is <see cref="IndentView.None" />.</returns>
        /// <remarks>The <see cref="Style.IndentGuide" /> style can be used to specify the foreground and background color of indentation guides.</remarks>
        [DefaultValue(IndentView.None)]
        [Category("Indentation")]
        [Description("Indicates whether indentation guides are displayed.")]
        public IndentView IndentationGuides
        {
            get
            {
                return (IndentView)DirectMessage(NativeMethods.SCI_GETINDENTATIONGUIDES);
            }
            set
            {
                var indentView = (int)value;
                DirectMessage(NativeMethods.SCI_SETINDENTATIONGUIDES, new IntPtr(indentView));
            }
        }

        /// <summary>
        /// Gets or sets the indicator used in a subsequent call to <see cref="IndicatorFillRange" /> or <see cref="IndicatorClearRange" />.
        /// </summary>
        /// <returns>The zero-based indicator index to apply when calling <see cref="IndicatorFillRange" /> or remove when calling <see cref="IndicatorClearRange" />.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int IndicatorCurrent
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_GETINDICATORCURRENT).ToInt32();
            }
            set
            {
                value = Helpers.Clamp(value, 0, Indicators.Count - 1);
                DirectMessage(NativeMethods.SCI_SETINDICATORCURRENT, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets a collection of objects for working with indicators.
        /// </summary>
        /// <returns>A collection of <see cref="Indicator" /> objects.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IndicatorCollection Indicators { get; private set; }

        /// <summary>
        /// Gets or sets the user-defined value used in a subsequent call to <see cref="IndicatorFillRange" />.
        /// </summary>
        /// <returns>The indicator value to apply when calling <see cref="IndicatorFillRange" />.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int IndicatorValue
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_GETINDICATORVALUE).ToInt32();
            }
            set
            {
                DirectMessage(NativeMethods.SCI_SETINDICATORVALUE, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets the current lexer.
        /// </summary>
        /// <returns>One of the <see cref="Lexer" /> enumeration values. The default is <see cref="Zeroit.Framework.CodeBox.Lexer.Container" />.</returns>
        [DefaultValue(Lexer.Container)]
        [Category("Lexing")]
        [Description("The current lexer.")]
        public Lexer Lexer
        {
            get
            {
                return (Lexer)DirectMessage(NativeMethods.SCI_GETLEXER);
            }
            set
            {
                var lexer = (int)value;
                DirectMessage(NativeMethods.SCI_SETLEXER, new IntPtr(lexer));
            }
        }

        /// <summary>
        /// Gets or sets the current lexer by name.
        /// </summary>
        /// <returns>A String representing the current lexer.</returns>
        /// <remarks>Lexer names are case-sensitive.</remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public unsafe string LexerLanguage
        {
            get
            {
                var length = DirectMessage(NativeMethods.SCI_GETLEXERLANGUAGE).ToInt32();
                if (length == 0)
                    return string.Empty;

                var bytes = new byte[length + 1];
                fixed (byte* bp = bytes)
                {
                    DirectMessage(NativeMethods.SCI_GETLEXERLANGUAGE, IntPtr.Zero, new IntPtr(bp));
                    return Helpers.GetString(new IntPtr(bp), length, Encoding.ASCII);
                }
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    DirectMessage(NativeMethods.SCI_SETLEXERLANGUAGE, IntPtr.Zero, IntPtr.Zero);
                }
                else
                {
                    var bytes = Helpers.GetBytes(value, Encoding.ASCII, zeroTerminated: true);
                    fixed (byte* bp = bytes)
                        DirectMessage(NativeMethods.SCI_SETLEXERLANGUAGE, IntPtr.Zero, new IntPtr(bp));
                }
            }
        }

        /// <summary>
        /// Gets the combined result of the <see cref="LineEndTypesSupported" /> and <see cref="LineEndTypesAllowed" />
        /// properties to report the line end types actively being interpreted.
        /// </summary>
        /// <returns>A bitwise combination of the <see cref="LineEndType" /> enumeration.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public LineEndType LineEndTypesActive
        {
            get
            {
                return (LineEndType)DirectMessage(NativeMethods.SCI_GETLINEENDTYPESACTIVE);
            }
        }

        /// <summary>
        /// Gets or sets the line ending types interpreted by the <see cref="Scintilla" /> control.
        /// </summary>
        /// <returns>
        /// A bitwise combination of the <see cref="LineEndType" /> enumeration.
        /// The default is <see cref="LineEndType.Default" />.
        /// </returns>
        /// <remarks>The line ending types allowed must also be supported by the current lexer to be effective.</remarks>
        [DefaultValue(LineEndType.Default)]
        [Category("Line Endings")]
        [Description("Line endings types interpreted by the control.")]
        [TypeConverter(typeof(FlagsEnumTypeConverter.FlagsEnumConverter))]
        public LineEndType LineEndTypesAllowed
        {
            get
            {
                return (LineEndType)DirectMessage(NativeMethods.SCI_GETLINEENDTYPESALLOWED);
            }
            set
            {
                var lineEndBitsSet = (int)value;
                DirectMessage(NativeMethods.SCI_SETLINEENDTYPESALLOWED, new IntPtr(lineEndBitsSet));
            }
        }

        /// <summary>
        /// Gets the different types of line ends supported by the current lexer.
        /// </summary>
        /// <returns>A bitwise combination of the <see cref="LineEndType" /> enumeration.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public LineEndType LineEndTypesSupported
        {
            get
            {
                return (LineEndType)DirectMessage(NativeMethods.SCI_GETLINEENDTYPESSUPPORTED);
            }
        }

        /// <summary>
        /// Gets a collection representing lines of text in the <see cref="Scintilla" /> control.
        /// </summary>
        /// <returns>A collection of text lines.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public LineCollection Lines { get; private set; }

        /// <summary>
        /// Gets the number of lines that can be shown on screen given a constant
        /// line height and the space available.
        /// </summary>
        /// <returns>
        /// The number of screen lines which could be displayed (including any partial lines).
        /// </returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int LinesOnScreen
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_LINESONSCREEN).ToInt32();
            }
        }

        /// <summary>
        /// Gets or sets the main selection when their are multiple selections.
        /// </summary>
        /// <returns>The zero-based main selection index.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MainSelection
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_GETMAINSELECTION).ToInt32();
            }
            set
            {
                value = Helpers.ClampMin(value, 0);
                DirectMessage(NativeMethods.SCI_SETMAINSELECTION, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets a collection representing margins in a <see cref="Scintilla" /> control.
        /// </summary>
        /// <returns>A collection of margins.</returns>
        [Category("Collections")]
        [Description("The margins collection.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public MarginCollection Margins { get; private set; }

        /// <summary>
        /// Gets a collection representing markers in a <see cref="Scintilla" /> control.
        /// </summary>
        /// <returns>A collection of markers.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MarkerCollection Markers { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the document has been modified (is dirty)
        /// since the last call to <see cref="SetSavePoint" />.
        /// </summary>
        /// <returns>true if the document has been modified; otherwise, false.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Modified
        {
            get
            {
                return (DirectMessage(NativeMethods.SCI_GETMODIFY) != IntPtr.Zero);
            }
        }

        /// <summary>
        /// Gets or sets the time in milliseconds the mouse must linger to generate a <see cref="DwellStart" /> event.
        /// </summary>
        /// <returns>
        /// The time in milliseconds the mouse must linger to generate a <see cref="DwellStart" /> event
        /// or <see cref="Scintilla.TimeForever" /> if dwell events are disabled.
        /// </returns>
        [DefaultValue(TimeForever)]
        [Category("Behavior")]
        [Description("The time in milliseconds the mouse must linger to generate a dwell start event. A value of 10000000 disables dwell events.")]
        public int MouseDwellTime
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_GETMOUSEDWELLTIME).ToInt32();
            }
            set
            {
                value = Helpers.ClampMin(value, 0);
                DirectMessage(NativeMethods.SCI_SETMOUSEDWELLTIME, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets the ability to switch to rectangular selection mode while making a selection with the mouse.
        /// </summary>
        /// <returns>
        /// true if the current mouse selection can be switched to a rectangular selection by pressing the ALT key; otherwise, false.
        /// The default is false.
        /// </returns>
        [DefaultValue(false)]
        [Category("Multiple Selection")]
        [Description("Enable or disable the ability to switch to rectangular selection mode while making a selection with the mouse.")]
        public bool MouseSelectionRectangularSwitch
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_GETMOUSESELECTIONRECTANGULARSWITCH) != IntPtr.Zero;
            }
            set
            {
                var mouseSelectionRectangularSwitch = (value ? new IntPtr(1) : IntPtr.Zero);
                DirectMessage(NativeMethods.SCI_SETMOUSESELECTIONRECTANGULARSWITCH, mouseSelectionRectangularSwitch);
            }
        }

        // The MouseWheelCaptures property doesn't seem to work correctly in Windows Forms so hiding for now...
        // P.S. I'm avoiding the MouseDownCaptures property (SCI_SETMOUSEDOWNCAPTURES & SCI_GETMOUSEDOWNCAPTURES) for the same reason... I don't expect it to work in Windows Forms.

        /* 
        /// <summary>
        /// Gets or sets whether to respond to mouse wheel messages if the control has focus but the mouse is not currently over the control.
        /// </summary>
        /// <returns>
        /// true to respond to mouse wheel messages even when the mouse is not currently over the control; otherwise, false.
        /// The default is true.
        /// </returns>
        /// <remarks>Scintilla will still react to the mouse wheel if the mouse pointer is over the editor window.</remarks>
        [DefaultValue(true)]
        [Category("Mouse")]
        [Description("Enable or disable mouse wheel support when the mouse is outside the control bounds, but the control still has focus.")]
        public bool MouseWheelCaptures
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_GETMOUSEWHEELCAPTURES) != IntPtr.Zero;
            }
            set
            {
                var mouseWheelCaptures = (value ? new IntPtr(1) : IntPtr.Zero);
                DirectMessage(NativeMethods.SCI_SETMOUSEWHEELCAPTURES, mouseWheelCaptures);
            }
        }
        */

        /// <summary>
        /// Gets or sets whether multiple selection is enabled.
        /// </summary>
        /// <returns>
        /// true if multiple selections can be made by holding the CTRL key and dragging the mouse; otherwise, false.
        /// The default is false.
        /// </returns>
        [DefaultValue(false)]
        [Category("Multiple Selection")]
        [Description("Enable or disable multiple selection with the CTRL key.")]
        public bool MultipleSelection
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_GETMULTIPLESELECTION) != IntPtr.Zero;
            }
            set
            {
                var multipleSelection = (value ? new IntPtr(1) : IntPtr.Zero);
                DirectMessage(NativeMethods.SCI_SETMULTIPLESELECTION, multipleSelection);
            }
        }

        /// <summary>
        /// Gets or sets the behavior when pasting text into multiple selections.
        /// </summary>
        /// <returns>One of the <see cref="Zeroit.Framework.CodeBox.MultiPaste" /> enumeration values. The default is <see cref="Zeroit.Framework.CodeBox.MultiPaste.Once" />.</returns>
        [DefaultValue(MultiPaste.Once)]
        [Category("Multiple Selection")]
        [Description("Determines how pasted text is applied to multiple selections.")]
        public MultiPaste MultiPaste
        {
            get
            {
                return (MultiPaste)DirectMessage(NativeMethods.SCI_GETMULTIPASTE);
            }
            set
            {
                var multiPaste = (int)value;
                DirectMessage(NativeMethods.SCI_SETMULTIPASTE, new IntPtr(multiPaste));
            }
        }

        /// <summary>
        /// Gets or sets whether to write over text rather than insert it.
        /// </summary>
        /// <return>true to write over text; otherwise, false. The default is false.</return>
        [DefaultValue(false)]
        [Category("Behavior")]
        [Description("Puts the caret into overtype mode.")]
        public bool Overtype
        {
            get
            {
                return (DirectMessage(NativeMethods.SCI_GETOVERTYPE) != IntPtr.Zero);
            }
            set
            {
                var overtype = (value ? new IntPtr(1) : IntPtr.Zero);
                DirectMessage(NativeMethods.SCI_SETOVERTYPE, overtype);
            }
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Padding Padding
        {
            get
            {
                return base.Padding;
            }
            set
            {
                base.Padding = value;
            }
        }

        /// <summary>
        /// Gets or sets whether line endings in pasted text are convereted to the document <see cref="EolMode" />.
        /// </summary>
        /// <returns>true to convert line endings in pasted text; otherwise, false. The default is true.</returns>
        [DefaultValue(true)]
        [Category("Line Endings")]
        [Description("Whether line endings in pasted text are converted to match the document end-of-line mode.")]
        public bool PasteConvertEndings
        {
            get
            {
                return (DirectMessage(NativeMethods.SCI_GETPASTECONVERTENDINGS) != IntPtr.Zero);
            }
            set
            {
                var convert = (value ? new IntPtr(1) : IntPtr.Zero);
                DirectMessage(NativeMethods.SCI_SETPASTECONVERTENDINGS, convert);
            }
        }

        /// <summary>
        /// Gets or sets the number of phases used when drawing.
        /// </summary>
        /// <returns>One of the <see cref="Phases" /> enumeration values. The default is <see cref="Phases.Two" />.</returns>
        [DefaultValue(Phases.Two)]
        [Category("Misc")]
        [Description("Adjusts the number of phases used when drawing.")]
        public Phases PhasesDraw
        {
            get
            {
                return (Phases)DirectMessage(NativeMethods.SCI_GETPHASESDRAW);
            }
            set
            {
                var phases = (int)value;
                DirectMessage(NativeMethods.SCI_SETPHASESDRAW, new IntPtr(phases));
            }
        }

        /// <summary>
        /// Gets or sets whether the document is read-only.
        /// </summary>
        /// <returns>true if the document is read-only; otherwise, false. The default is false.</returns>
        /// <seealso cref="ModifyAttempt" />
        [DefaultValue(false)]
        [Category("Behavior")]
        [Description("Controls whether the document text can be modified.")]
        public bool ReadOnly
        {
            get
            {
                return (DirectMessage(NativeMethods.SCI_GETREADONLY) != IntPtr.Zero);
            }
            set
            {
                var readOnly = (value ? new IntPtr(1) : IntPtr.Zero);
                DirectMessage(NativeMethods.SCI_SETREADONLY, readOnly);
            }
        }

        /// <summary>
        /// Gets or sets the anchor position of the rectangular selection.
        /// </summary>
        /// <returns>The zero-based document position of the rectangular selection anchor.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int RectangularSelectionAnchor
        {
            get
            {
                var pos = DirectMessage(NativeMethods.SCI_GETRECTANGULARSELECTIONANCHOR).ToInt32();
                if (pos <= 0)
                    return pos;

                return Lines.ByteToCharPosition(pos);
            }
            set
            {
                value = Helpers.Clamp(value, 0, TextLength);
                value = Lines.CharToBytePosition(value);
                DirectMessage(NativeMethods.SCI_SETRECTANGULARSELECTIONANCHOR, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets the amount of anchor virtual space in a rectangular selection.
        /// </summary>
        /// <returns>The amount of virtual space past the end of the line offsetting the rectangular selection anchor.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int RectangularSelectionAnchorVirtualSpace
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_GETRECTANGULARSELECTIONANCHORVIRTUALSPACE).ToInt32();
            }
            set
            {
                value = Helpers.ClampMin(value, 0);
                DirectMessage(NativeMethods.SCI_SETRECTANGULARSELECTIONANCHORVIRTUALSPACE, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets the caret position of the rectangular selection.
        /// </summary>
        /// <returns>The zero-based document position of the rectangular selection caret.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int RectangularSelectionCaret
        {
            get
            {
                var pos = DirectMessage(NativeMethods.SCI_GETRECTANGULARSELECTIONCARET).ToInt32();
                if (pos <= 0)
                    return 0;

                return Lines.ByteToCharPosition(pos);
            }
            set
            {
                value = Helpers.Clamp(value, 0, TextLength);
                value = Lines.CharToBytePosition(value);
                DirectMessage(NativeMethods.SCI_SETRECTANGULARSELECTIONCARET, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets the amount of caret virtual space in a rectangular selection.
        /// </summary>
        /// <returns>The amount of virtual space past the end of the line offsetting the rectangular selection caret.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int RectangularSelectionCaretVirtualSpace
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_GETRECTANGULARSELECTIONCARETVIRTUALSPACE).ToInt32();
            }
            set
            {
                value = Helpers.ClampMin(value, 0);
                DirectMessage(NativeMethods.SCI_SETRECTANGULARSELECTIONCARETVIRTUALSPACE, new IntPtr(value));
            }
        }

        private IntPtr SciPointer
        {
            get
            {
                // Enforce illegal cross-thread calls the way the Handle property does
                if (Control.CheckForIllegalCrossThreadCalls && InvokeRequired)
                {
                    string message = string.Format(CultureInfo.InvariantCulture, "Control '{0}' accessed from a thread other than the thread it was created on.", Name);
                    throw new InvalidOperationException(message);
                }

                if (sciPtr == IntPtr.Zero)
                {
                    // Get a pointer to the native Scintilla object (i.e. C++ 'this') to use with the
                    // direct function. This will happen for each Scintilla control instance.
                    sciPtr = NativeMethods.SendMessage(new HandleRef(this, Handle), NativeMethods.SCI_GETDIRECTPOINTER, IntPtr.Zero, IntPtr.Zero);
                }

                return sciPtr;
            }
        }

        /// <summary>
        /// Gets or sets the range of the horizontal scroll bar.
        /// </summary>
        /// <returns>The range in pixels of the horizontal scroll bar. The default is 2000.</returns>
        /// <remarks>The width will automatically increase as needed when <see cref="ScrollWidthTracking" /> is enabled.</remarks>
        [DefaultValue(2000)]
        [Category("Scrolling")]
        [Description("The range in pixels of the horizontal scroll bar.")]
        public int ScrollWidth
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_GETSCROLLWIDTH).ToInt32();
            }
            set
            {
                DirectMessage(NativeMethods.SCI_SETSCROLLWIDTH, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets whether the <see cref="ScrollWidth" /> is automatically increased as needed.
        /// </summary>
        /// <returns>
        /// true to automatically increase the horizontal scroll width as needed; otherwise, false.
        /// The default is true.
        /// </returns>
        [DefaultValue(true)]
        [Category("Scrolling")]
        [Description("Determines whether to increase the horizontal scroll width as needed.")]
        public bool ScrollWidthTracking
        {
            get
            {
                return (DirectMessage(NativeMethods.SCI_GETSCROLLWIDTHTRACKING) != IntPtr.Zero);
            }
            set
            {
                var tracking = (value ? new IntPtr(1) : IntPtr.Zero);
                DirectMessage(NativeMethods.SCI_SETSCROLLWIDTHTRACKING, tracking);
            }
        }

        /// <summary>
        /// Gets or sets the search flags used when searching text.
        /// </summary>
        /// <returns>A bitwise combination of <see cref="Zeroit.Framework.CodeBox.SearchFlags" /> values. The default is <see cref="Zeroit.Framework.CodeBox.SearchFlags.None" />.</returns>
        /// <seealso cref="SearchInTarget" />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SearchFlags SearchFlags
        {
            get
            {
                return (SearchFlags)DirectMessage(NativeMethods.SCI_GETSEARCHFLAGS).ToInt32();
            }
            set
            {
                var searchFlags = (int)value;
                DirectMessage(NativeMethods.SCI_SETSEARCHFLAGS, new IntPtr(searchFlags));
            }
        }

        /// <summary>
        /// Gets the selected text.
        /// </summary>
        /// <returns>The selected text if there is any; otherwise, an empty string.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public unsafe string SelectedText
        {
            get
            {
                // NOTE: For some reason the length returned by this API includes the terminating NULL
                var length = DirectMessage(NativeMethods.SCI_GETSELTEXT).ToInt32() - 1;
                if (length <= 0)
                    return string.Empty;

                var bytes = new byte[length + 1];
                fixed (byte* bp = bytes)
                {
                    DirectMessage(NativeMethods.SCI_GETSELTEXT, IntPtr.Zero, new IntPtr(bp));
                    return Helpers.GetString(new IntPtr(bp), length, Encoding);
                }
            }
        }

        /// <summary>
        /// Gets or sets the end position of the selection.
        /// </summary>
        /// <returns>The zero-based document position where the selection ends.</returns>
        /// <remarks>
        /// When getting this property, the return value is <code>Math.Max(<see cref="AnchorPosition" />, <see cref="CurrentPosition" />)</code>.
        /// When setting this property, <see cref="CurrentPosition" /> is set to the value specified and <see cref="AnchorPosition" /> set to <code>Math.Min(<see cref="AnchorPosition" />, <paramref name="value" />)</code>.
        /// The caret is not scrolled into view.
        /// </remarks>
        /// <seealso cref="SelectionStart" />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionEnd
        {
            get
            {
                var pos = DirectMessage(NativeMethods.SCI_GETSELECTIONEND).ToInt32();
                return Lines.ByteToCharPosition(pos);
            }
            set
            {
                value = Helpers.Clamp(value, 0, TextLength);
                value = Lines.CharToBytePosition(value);
                DirectMessage(NativeMethods.SCI_SETSELECTIONEND, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets whether to fill past the end of a line with the selection background color.
        /// </summary>
        /// <returns>true to fill past the end of the line; otherwise, false. The default is false.</returns>
        [DefaultValue(false)]
        [Category("Selection")]
        [Description("Determines whether a selection should fill past the end of the line.")]
        public bool SelectionEolFilled
        {
            get
            {
                return (DirectMessage(NativeMethods.SCI_GETSELEOLFILLED) != IntPtr.Zero);
            }
            set
            {
                var eolFilled = (value ? new IntPtr(1) : IntPtr.Zero);
                DirectMessage(NativeMethods.SCI_SETSELEOLFILLED, eolFilled);
            }
        }

        /// <summary>
        /// Gets a collection representing multiple selections in a <see cref="Scintilla" /> control.
        /// </summary>
        /// <returns>A collection of selections.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SelectionCollection Selections { get; private set; }

        /// <summary>
        /// Gets or sets the start position of the selection.
        /// </summary>
        /// <returns>The zero-based document position where the selection starts.</returns>
        /// <remarks>
        /// When getting this property, the return value is <code>Math.Min(<see cref="AnchorPosition" />, <see cref="CurrentPosition" />)</code>.
        /// When setting this property, <see cref="AnchorPosition" /> is set to the value specified and <see cref="CurrentPosition" /> set to <code>Math.Max(<see cref="CurrentPosition" />, <paramref name="value" />)</code>.
        /// The caret is not scrolled into view.
        /// </remarks>
        /// <seealso cref="SelectionEnd" />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionStart
        {
            get
            {
                var pos = DirectMessage(NativeMethods.SCI_GETSELECTIONSTART).ToInt32();
                return Lines.ByteToCharPosition(pos);
            }
            set
            {
                value = Helpers.Clamp(value, 0, TextLength);
                value = Lines.CharToBytePosition(value);
                DirectMessage(NativeMethods.SCI_SETSELECTIONSTART, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets the last internal error code used by Scintilla.
        /// </summary>
        /// <returns>
        /// One of the <see cref="Status" /> enumeration values.
        /// The default is <see cref="Zeroit.Framework.CodeBox.Status.Ok" />.
        /// </returns>
        /// <remarks>The status can be reset by setting the property to <see cref="Zeroit.Framework.CodeBox.Status.Ok" />.</remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Status Status
        {
            get
            {
                return (Status)DirectMessage(NativeMethods.SCI_GETSTATUS);
            }
            set
            {
                var status = (int)value;
                DirectMessage(NativeMethods.SCI_SETSTATUS, new IntPtr(status));
            }
        }

        /// <summary>
        /// Gets a collection representing style definitions in a <see cref="Scintilla" /> control.
        /// </summary>
        /// <returns>A collection of style definitions.</returns>

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public StyleCollection Styles { get; private set; }

        /// <summary>
        /// Gets or sets how tab characters are represented when whitespace is visible.
        /// </summary>
        /// <returns>
        /// One of the <see cref="Zeroit.Framework.CodeBox.TabDrawMode" /> enumeration values.
        /// The default is <see cref="TabDrawMode.LongArrow" />.
        /// </returns>
        /// <seealso cref="ViewWhitespace" />
        [DefaultValue(TabDrawMode.LongArrow)]
        [Category("Whitespace")]
        [Description("Style of visible tab characters.")]
        public TabDrawMode TabDrawMode
        {
            get
            {
                return (TabDrawMode)DirectMessage(NativeMethods.SCI_GETTABDRAWMODE);
            }
            set
            {
                var tabDrawMode = (int)value;
                DirectMessage(NativeMethods.SCI_SETTABDRAWMODE, new IntPtr(tabDrawMode));
            }
        }

        /// <summary>
        /// Gets or sets the width of a tab as a multiple of a space character.
        /// </summary>
        /// <returns>The width of a tab measured in characters. The default is 4.</returns>
        [DefaultValue(4)]
        [Category("Indentation")]
        [Description("The tab size in characters.")]
        public int TabWidth
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_GETTABWIDTH).ToInt32();
            }
            set
            {
                DirectMessage(NativeMethods.SCI_SETTABWIDTH, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets the end position used when performing a search or replace.
        /// </summary>
        /// <returns>The zero-based character position within the document to end a search or replace operation.</returns>
        /// <seealso cref="TargetStart"/>
        /// <seealso cref="SearchInTarget" />
        /// <seealso cref="ReplaceTarget" />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int TargetEnd
        {
            get
            {
                // The position can become stale and point to a place outside of the document so we must clamp it
                var bytePos = Helpers.Clamp(DirectMessage(NativeMethods.SCI_GETTARGETEND).ToInt32(), 0, DirectMessage(NativeMethods.SCI_GETTEXTLENGTH).ToInt32());
                return Lines.ByteToCharPosition(bytePos);
            }
            set
            {
                value = Helpers.Clamp(value, 0, TextLength);
                value = Lines.CharToBytePosition(value);
                DirectMessage(NativeMethods.SCI_SETTARGETEND, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets the start position used when performing a search or replace.
        /// </summary>
        /// <returns>The zero-based character position within the document to start a search or replace operation.</returns>
        /// <seealso cref="TargetEnd"/>
        /// <seealso cref="SearchInTarget" />
        /// <seealso cref="ReplaceTarget" />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int TargetStart
        {
            get
            {
                // The position can become stale and point to a place outside of the document so we must clamp it
                var bytePos = Helpers.Clamp(DirectMessage(NativeMethods.SCI_GETTARGETSTART).ToInt32(), 0, DirectMessage(NativeMethods.SCI_GETTEXTLENGTH).ToInt32());
                return Lines.ByteToCharPosition(bytePos);
            }
            set
            {
                value = Helpers.Clamp(value, 0, TextLength);
                value = Lines.CharToBytePosition(value);
                DirectMessage(NativeMethods.SCI_SETTARGETSTART, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets the current target text.
        /// </summary>
        /// <returns>A String representing the text between <see cref="TargetStart" /> and <see cref="TargetEnd" />.</returns>
        /// <remarks>Targets which have a start position equal or greater to the end position will return an empty String.</remarks>
        /// <seealso cref="TargetStart" />
        /// <seealso cref="TargetEnd" />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public unsafe string TargetText
        {
            get
            {
                var length = DirectMessage(NativeMethods.SCI_GETTARGETTEXT).ToInt32();
                if (length == 0)
                    return string.Empty;

                var bytes = new byte[length + 1];
                fixed (byte* bp = bytes)
                {
                    DirectMessage(NativeMethods.SCI_GETTARGETTEXT, IntPtr.Zero, new IntPtr(bp));
                    return Helpers.GetString(new IntPtr(bp), length, Encoding);
                }
            }
        }

        /// <summary>
        /// Gets or sets the rendering technology used.
        /// </summary>
        /// <returns>
        /// One of the <see cref="Technology" /> enumeration values.
        /// The default is <see cref="Zeroit.Framework.CodeBox.Technology.Default" />.
        /// </returns>
        [DefaultValue(Technology.Default)]
        [Category("Misc")]
        [Description("The rendering technology used to draw text.")]
        public Technology Technology
        {
            get
            {
                return (Technology)DirectMessage(NativeMethods.SCI_GETTECHNOLOGY);
            }
            set
            {
                var technology = (int)value;
                DirectMessage(NativeMethods.SCI_SETTECHNOLOGY, new IntPtr(technology));
            }
        }

        /// <summary>
        /// Gets or sets the current document text in the <see cref="Scintilla" /> control.
        /// </summary>
        /// <returns>The text displayed in the control.</returns>
        /// <remarks>Depending on the length of text get or set, this operation can be expensive.</remarks>
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design", typeof(UITypeEditor))]
        public unsafe override string Text
        {
            get
            {
                var length = DirectMessage(NativeMethods.SCI_GETTEXTLENGTH).ToInt32();
                var ptr = DirectMessage(NativeMethods.SCI_GETRANGEPOINTER, new IntPtr(0), new IntPtr(length));
                if (ptr == IntPtr.Zero)
                    return string.Empty;

                // Assumption is that moving the gap will always be equal to or less expensive
                // than using one of the APIs which requires an intermediate buffer.
                var text = new string((sbyte*)ptr, 0, length, Encoding);
                return text;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    DirectMessage(NativeMethods.SCI_CLEARALL);
                }
                else
                {
                    fixed (byte* bp = Helpers.GetBytes(value, Encoding, zeroTerminated: true))
                        DirectMessage(NativeMethods.SCI_SETTEXT, IntPtr.Zero, new IntPtr(bp));
                }
            }
        }

        /// <summary>
        /// Gets the length of the text in the control.
        /// </summary>
        /// <returns>The number of characters in the document.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int TextLength
        {
            get
            {
                return Lines.TextLength;
            }
        }

        /// <summary>
        /// Gets or sets whether to use a mixture of tabs and spaces for indentation or purely spaces.
        /// </summary>
        /// <returns>true to use tab characters; otherwise, false. The default is true.</returns>
        [DefaultValue(false)]
        [Category("Indentation")]
        [Description("Determines whether indentation allows tab characters or purely space characters.")]
        public bool UseTabs
        {
            get
            {
                return (DirectMessage(NativeMethods.SCI_GETUSETABS) != IntPtr.Zero);
            }
            set
            {
                var useTabs = (value ? new IntPtr(1) : IntPtr.Zero);
                DirectMessage(NativeMethods.SCI_SETUSETABS, useTabs);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use the wait cursor for the current control.
        /// </summary>
        /// <returns>true to use the wait cursor for the current control; otherwise, false. The default is false.</returns>
        public new bool UseWaitCursor
        {
            get
            {
                return base.UseWaitCursor;
            }
            set
            {
                base.UseWaitCursor = value;
                var cursor = (value ? NativeMethods.SC_CURSORWAIT : NativeMethods.SC_CURSORNORMAL);
                DirectMessage(NativeMethods.SCI_SETCURSOR, new IntPtr(cursor));
            }
        }

        /// <summary>
        /// Gets or sets the visibility of end-of-line characters.
        /// </summary>
        /// <returns>true to display end-of-line characters; otherwise, false. The default is false.</returns>
        [DefaultValue(false)]
        [Category("Line Endings")]
        [Description("Display end-of-line characters.")]
        public bool ViewEol
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_GETVIEWEOL) != IntPtr.Zero;
            }
            set
            {
                var visible = (value ? new IntPtr(1) : IntPtr.Zero);
                DirectMessage(NativeMethods.SCI_SETVIEWEOL, visible);
            }
        }

        /// <summary>
        /// Gets or sets how to display whitespace characters.
        /// </summary>
        /// <returns>One of the <see cref="WhitespaceMode" /> enumeration values. The default is <see cref="WhitespaceMode.Invisible" />.</returns>
        /// <seealso cref="SetWhitespaceForeColor" />
        /// <seealso cref="SetWhitespaceBackColor" />
        [DefaultValue(WhitespaceMode.Invisible)]
        [Category("Whitespace")]
        [Description("Options for displaying whitespace characters.")]
        public WhitespaceMode ViewWhitespace
        {
            get
            {
                return (WhitespaceMode)DirectMessage(NativeMethods.SCI_GETVIEWWS);
            }
            set
            {
                var wsMode = (int)value;
                DirectMessage(NativeMethods.SCI_SETVIEWWS, new IntPtr(wsMode));
            }
        }

        /// <summary>
        /// Gets or sets the ability for the caret to move into an area beyond the end of each line, otherwise known as virtual space.
        /// </summary>
        /// <returns>
        /// A bitwise combination of the <see cref="VirtualSpace" /> enumeration.
        /// The default is <see cref="VirtualSpace.None" />.
        /// </returns>
        [DefaultValue(VirtualSpace.None)]
        [Category("Behavior")]
        [Description("Options for allowing the caret to move beyond the end of each line.")]
        [TypeConverter(typeof(FlagsEnumTypeConverter.FlagsEnumConverter))]
        public VirtualSpace VirtualSpaceOptions
        {
            get
            {
                return (VirtualSpace)DirectMessage(NativeMethods.SCI_GETVIRTUALSPACEOPTIONS);
            }
            set
            {
                var virtualSpace = (int)value;
                DirectMessage(NativeMethods.SCI_SETVIRTUALSPACEOPTIONS, new IntPtr(virtualSpace));
            }
        }

        /// <summary>
        /// Gets or sets whether to display the vertical scroll bar.
        /// </summary>
        /// <returns>true to display the vertical scroll bar when needed; otherwise, false. The default is true.</returns>
        [DefaultValue(true)]
        [Category("Scrolling")]
        [Description("Determines whether to show the vertical scroll bar when needed.")]
        public bool VScrollBar
        {
            get
            {
                return (DirectMessage(NativeMethods.SCI_GETVSCROLLBAR) != IntPtr.Zero);
            }
            set
            {
                var visible = (value ? new IntPtr(1) : IntPtr.Zero);
                DirectMessage(NativeMethods.SCI_SETVSCROLLBAR, visible);
            }
        }

        /// <summary>
        /// Gets or sets the size of the dots used to mark whitespace.
        /// </summary>
        /// <returns>The size of the dots used to mark whitespace. The default is 1.</returns>
        /// <seealso cref="ViewWhitespace" />
        [DefaultValue(1)]
        [Category("Whitespace")]
        [Description("The size of whitespace dots.")]
        public int WhitespaceSize
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_GETWHITESPACESIZE).ToInt32();
            }
            set
            {
                DirectMessage(NativeMethods.SCI_SETWHITESPACESIZE, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets the characters considered 'word' characters when using any word-based logic.
        /// </summary>
        /// <returns>A string of word characters.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public unsafe string WordChars
        {
            get
            {
                var length = DirectMessage(NativeMethods.SCI_GETWORDCHARS, IntPtr.Zero, IntPtr.Zero).ToInt32();
                var bytes = new byte[length + 1];
                fixed (byte* bp = bytes)
                {
                    DirectMessage(NativeMethods.SCI_GETWORDCHARS, IntPtr.Zero, new IntPtr(bp));
                    return Helpers.GetString(new IntPtr(bp), length, Encoding.ASCII);
                }
            }
            set
            {
                if (value == null)
                {
                    DirectMessage(NativeMethods.SCI_SETWORDCHARS, IntPtr.Zero, IntPtr.Zero);
                    return;
                }

                // Scintilla stores each of the characters specified in a char array which it then
                // uses as a lookup for word matching logic. Thus, any multibyte chars wouldn't work.
                var bytes = Helpers.GetBytes(value, Encoding.ASCII, zeroTerminated: true);
                fixed (byte* bp = bytes)
                    DirectMessage(NativeMethods.SCI_SETWORDCHARS, IntPtr.Zero, new IntPtr(bp));
            }
        }

        /// <summary>
        /// Gets or sets the line wrapping indent mode.
        /// </summary>
        /// <returns>
        /// One of the <see cref="Zeroit.Framework.CodeBox.WrapIndentMode" /> enumeration values.
        /// The default is <see cref="Zeroit.Framework.CodeBox.WrapIndentMode.Fixed" />.
        /// </returns>
        [DefaultValue(WrapIndentMode.Fixed)]
        [Category("Line Wrapping")]
        [Description("Determines how wrapped sublines are indented.")]
        public WrapIndentMode WrapIndentMode
        {
            get
            {
                return (WrapIndentMode)DirectMessage(NativeMethods.SCI_GETWRAPINDENTMODE);
            }
            set
            {
                var wrapIndentMode = (int)value;
                DirectMessage(NativeMethods.SCI_SETWRAPINDENTMODE, new IntPtr(wrapIndentMode));
            }
        }

        /// <summary>
        /// Gets or sets the line wrapping mode.
        /// </summary>
        /// <returns>
        /// One of the <see cref="Zeroit.Framework.CodeBox.WrapMode" /> enumeration values.
        /// The default is <see cref="Zeroit.Framework.CodeBox.WrapMode.None" />.
        /// </returns>
        [DefaultValue(WrapMode.None)]
        [Category("Line Wrapping")]
        [Description("The line wrapping strategy.")]
        public WrapMode WrapMode
        {
            get
            {
                return (WrapMode)DirectMessage(NativeMethods.SCI_GETWRAPMODE);
            }
            set
            {
                var wrapMode = (int)value;
                DirectMessage(NativeMethods.SCI_SETWRAPMODE, new IntPtr(wrapMode));
            }
        }

        /// <summary>
        /// Gets or sets the indented size in pixels of wrapped sublines.
        /// </summary>
        /// <returns>The indented size of wrapped sublines measured in pixels. The default is 0.</returns>
        /// <remarks>
        /// Setting <see cref="WrapVisualFlags" /> to <see cref="Zeroit.Framework.CodeBox.WrapVisualFlags.Start" /> will add an
        /// additional 1 pixel to the value specified.
        /// </remarks>
        [DefaultValue(0)]
        [Category("Line Wrapping")]
        [Description("The amount of pixels to indent wrapped sublines.")]
        public int WrapStartIndent
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_GETWRAPSTARTINDENT).ToInt32();
            }
            set
            {
                value = Helpers.ClampMin(value, 0);
                DirectMessage(NativeMethods.SCI_SETWRAPSTARTINDENT, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets the wrap visual flags.
        /// </summary>
        /// <returns>
        /// A bitwise combination of the <see cref="Zeroit.Framework.CodeBox.WrapVisualFlags" /> enumeration.
        /// The default is <see cref="Zeroit.Framework.CodeBox.WrapVisualFlags.None" />.
        /// </returns>
        [DefaultValue(WrapVisualFlags.None)]
        [Category("Line Wrapping")]
        [Description("The visual indicator displayed on a wrapped line.")]
        [TypeConverter(typeof(FlagsEnumTypeConverter.FlagsEnumConverter))]
        public WrapVisualFlags WrapVisualFlags
        {
            get
            {
                return (WrapVisualFlags)DirectMessage(NativeMethods.SCI_GETWRAPVISUALFLAGS);
            }
            set
            {
                int wrapVisualFlags = (int)value;
                DirectMessage(NativeMethods.SCI_SETWRAPVISUALFLAGS, new IntPtr(wrapVisualFlags));
            }
        }

        /// <summary>
        /// Gets or sets additional location options when displaying wrap visual flags.
        /// </summary>
        /// <returns>
        /// One of the <see cref="Zeroit.Framework.CodeBox.WrapVisualFlagLocation" /> enumeration values.
        /// The default is <see cref="Zeroit.Framework.CodeBox.WrapVisualFlagLocation.Default" />.
        /// </returns>
        [DefaultValue(WrapVisualFlagLocation.Default)]
        [Category("Line Wrapping")]
        [Description("The location of wrap visual flags in relation to the line text.")]
        public WrapVisualFlagLocation WrapVisualFlagLocation
        {
            get
            {
                return (WrapVisualFlagLocation)DirectMessage(NativeMethods.SCI_GETWRAPVISUALFLAGSLOCATION);
            }
            set
            {
                var location = (int)value;
                DirectMessage(NativeMethods.SCI_SETWRAPVISUALFLAGSLOCATION, new IntPtr(location));
            }
        }

        /// <summary>
        /// Gets or sets the horizontal scroll offset.
        /// </summary>
        /// <returns>The horizontal scroll offset in pixels.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int XOffset
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_GETXOFFSET).ToInt32();
            }
            set
            {
                value = Helpers.ClampMin(value, 0);
                DirectMessage(NativeMethods.SCI_SETXOFFSET, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets the zoom factor.
        /// </summary>
        /// <returns>The zoom factor measured in points.</returns>
        /// <remarks>For best results, values should range from -10 to 20 points.</remarks>
        /// <seealso cref="ZoomIn" />
        /// <seealso cref="ZoomOut" />
        [DefaultValue(0)]
        [Category("Appearance")]
        [Description("Zoom factor in points applied to the displayed text.")]
        public int Zoom
        {
            get
            {
                return DirectMessage(NativeMethods.SCI_GETZOOM).ToInt32();
            }
            set
            {
                DirectMessage(NativeMethods.SCI_SETZOOM, new IntPtr(value));
            }
        }

        #endregion Properties
        
    }
}