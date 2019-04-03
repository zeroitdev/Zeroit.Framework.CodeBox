// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="Events.cs" company="Zeroit Dev">
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
using System.Windows.Forms;

namespace Zeroit.Framework.CodeBox
{
    public partial class ZeroitCodeExplorer : Control
    {
        
        #region Events

        /// <summary>
        /// Occurs when an autocompletion list is cancelled.
        /// </summary>
        [Category("Notifications")]
        [Description("Occurs when an autocompletion list is cancelled.")]
        public event EventHandler<EventArgs> AutoCCancelled
        {
            add
            {
                Events.AddHandler(autoCCancelledEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(autoCCancelledEventKey, value);
            }
        }

        /// <summary>
        /// Occurs when the user deletes a character while an autocompletion list is active.
        /// </summary>
        [Category("Notifications")]
        [Description("Occurs when the user deletes a character while an autocompletion list is active.")]
        public event EventHandler<EventArgs> AutoCCharDeleted
        {
            add
            {
                Events.AddHandler(autoCCharDeletedEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(autoCCharDeletedEventKey, value);
            }
        }

        /// <summary>
        /// Occurs after autocompleted text is inserted.
        /// </summary>
        [Category("Notifications")]
        [Description("Occurs after autocompleted text has been inserted.")]
        public event EventHandler<AutoCSelectionEventArgs> AutoCCompleted
        {
            add
            {
                Events.AddHandler(autoCCompletedEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(autoCCompletedEventKey, value);
            }
        }

        /// <summary>
        /// Occurs when a user has selected an item in an autocompletion list.
        /// </summary>
        /// <remarks>Automatic insertion can be cancelled by calling <see cref="AutoCCancel" /> from the event handler.</remarks>
        [Category("Notifications")]
        [Description("Occurs when a user has selected an item in an autocompletion list.")]
        public event EventHandler<AutoCSelectionEventArgs> AutoCSelection
        {
            add
            {
                Events.AddHandler(autoCSelectionEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(autoCSelectionEventKey, value);
            }
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler BackColorChanged
        {
            add
            {
                base.BackColorChanged += value;
            }
            remove
            {
                base.BackColorChanged -= value;
            }
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler BackgroundImageChanged
        {
            add
            {
                base.BackgroundImageChanged += value;
            }
            remove
            {
                base.BackgroundImageChanged -= value;
            }
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler BackgroundImageLayoutChanged
        {
            add
            {
                base.BackgroundImageLayoutChanged += value;
            }
            remove
            {
                base.BackgroundImageLayoutChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when text is about to be deleted.
        /// </summary>
        [Category("Notifications")]
        [Description("Occurs before text is deleted.")]
        public event EventHandler<BeforeModificationEventArgs> BeforeDelete
        {
            add
            {
                Events.AddHandler(beforeDeleteEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(beforeDeleteEventKey, value);
            }
        }

        /// <summary>
        /// Occurs when text is about to be inserted.
        /// </summary>
        [Category("Notifications")]
        [Description("Occurs before text is inserted.")]
        public event EventHandler<BeforeModificationEventArgs> BeforeInsert
        {
            add
            {
                Events.AddHandler(beforeInsertEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(beforeInsertEventKey, value);
            }
        }

        /// <summary>
        /// Occurs when the value of the <see cref="Scintilla.BorderStyle" /> property has changed.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the value of the BorderStyle property changes.")]
        public event EventHandler BorderStyleChanged
        {
            add
            {
                Events.AddHandler(borderStyleChangedEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(borderStyleChangedEventKey, value);
            }
        }

        /// <summary>
        /// Occurs when an annotation has changed.
        /// </summary>
        [Category("Notifications")]
        [Description("Occurs when an annotation has changed.")]
        public event EventHandler<ChangeAnnotationEventArgs> ChangeAnnotation
        {
            add
            {
                Events.AddHandler(changeAnnotationEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(changeAnnotationEventKey, value);
            }
        }

        /// <summary>
        /// Occurs when the user enters a text character.
        /// </summary>
        [Category("Notifications")]
        [Description("Occurs when the user types a character.")]
        public event EventHandler<CharAddedEventArgs> CharAdded
        {
            add
            {
                Events.AddHandler(charAddedEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(charAddedEventKey, value);
            }
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler CursorChanged
        {
            add
            {
                base.CursorChanged += value;
            }
            remove
            {
                base.CursorChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when text has been deleted from the document.
        /// </summary>
        [Category("Notifications")]
        [Description("Occurs when text is deleted.")]
        public event EventHandler<ModificationEventArgs> Delete
        {
            add
            {
                Events.AddHandler(deleteEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(deleteEventKey, value);
            }
        }

        /// <summary>
        /// Occurs when the <see cref="Scintilla" /> control is double-clicked.
        /// </summary>
        [Category("Notifications")]
        [Description("Occurs when the editor is double clicked.")]
        public new event EventHandler<DoubleClickEventArgs> DoubleClick
        {
            add
            {
                Events.AddHandler(doubleClickEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(doubleClickEventKey, value);
            }
        }

        /// <summary>
        /// Occurs when the mouse moves or another activity such as a key press ends a <see cref="DwellStart" /> event.
        /// </summary>
        [Category("Notifications")]
        [Description("Occurs when the mouse moves from its dwell start position.")]
        public event EventHandler<DwellEventArgs> DwellEnd
        {
            add
            {
                Events.AddHandler(dwellEndEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(dwellEndEventKey, value);
            }
        }

        /// <summary>
        /// Occurs when the mouse is kept in one position (hovers) for the <see cref="MouseDwellTime" />.
        /// </summary>
        [Category("Notifications")]
        [Description("Occurs when the mouse is kept in one position (hovers) for a period of time.")]
        public event EventHandler<DwellEventArgs> DwellStart
        {
            add
            {
                Events.AddHandler(dwellStartEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(dwellStartEventKey, value);
            }
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler FontChanged
        {
            add
            {
                base.FontChanged += value;
            }
            remove
            {
                base.FontChanged -= value;
            }
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler ForeColorChanged
        {
            add
            {
                base.ForeColorChanged += value;
            }
            remove
            {
                base.ForeColorChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when the user clicks on text that is in a style with the <see cref="Style.Hotspot" /> property set.
        /// </summary>
        [Category("Notifications")]
        [Description("Occurs when the user clicks text styled with the hotspot flag.")]
        public event EventHandler<HotspotClickEventArgs> HotspotClick
        {
            add
            {
                Events.AddHandler(hotspotClickEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(hotspotClickEventKey, value);
            }
        }

        /// <summary>
        /// Occurs when the user double clicks on text that is in a style with the <see cref="Style.Hotspot" /> property set.
        /// </summary>
        [Category("Notifications")]
        [Description("Occurs when the user double clicks text styled with the hotspot flag.")]
        public event EventHandler<HotspotClickEventArgs> HotspotDoubleClick
        {
            add
            {
                Events.AddHandler(hotspotDoubleClickEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(hotspotDoubleClickEventKey, value);
            }
        }

        /// <summary>
        /// Occurs when the user releases a click on text that is in a style with the <see cref="Style.Hotspot" /> property set.
        /// </summary>
        [Category("Notifications")]
        [Description("Occurs when the user releases a click on text styled with the hotspot flag.")]
        public event EventHandler<HotspotClickEventArgs> HotspotReleaseClick
        {
            add
            {
                Events.AddHandler(hotspotReleaseClickEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(hotspotReleaseClickEventKey, value);
            }
        }

        /// <summary>
        /// Occurs when the user clicks on text that has an indicator.
        /// </summary>
        [Category("Notifications")]
        [Description("Occurs when the user clicks text with an indicator.")]
        public event EventHandler<IndicatorClickEventArgs> IndicatorClick
        {
            add
            {
                Events.AddHandler(indicatorClickEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(indicatorClickEventKey, value);
            }
        }

        /// <summary>
        /// Occurs when the user releases a click on text that has an indicator.
        /// </summary>
        [Category("Notifications")]
        [Description("Occurs when the user releases a click on text with an indicator.")]
        public event EventHandler<IndicatorReleaseEventArgs> IndicatorRelease
        {
            add
            {
                Events.AddHandler(indicatorReleaseEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(indicatorReleaseEventKey, value);
            }
        }

        /// <summary>
        /// Occurs when text has been inserted into the document.
        /// </summary>
        [Category("Notifications")]
        [Description("Occurs when text is inserted.")]
        public event EventHandler<ModificationEventArgs> Insert
        {
            add
            {
                Events.AddHandler(insertEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(insertEventKey, value);
            }
        }

        /// <summary>
        /// Occurs when text is about to be inserted. The inserted text can be changed.
        /// </summary>
        [Category("Notifications")]
        [Description("Occurs before text is inserted. Permits changing the inserted text.")]
        public event EventHandler<InsertCheckEventArgs> InsertCheck
        {
            add
            {
                Events.AddHandler(insertCheckEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(insertCheckEventKey, value);
            }
        }

        /// <summary>
        /// Occurs when the mouse was clicked inside a margin that was marked as sensitive.
        /// </summary>
        /// <remarks>The <see cref="Margin.Sensitive" /> property must be set for a margin to raise this event.</remarks>
        [Category("Notifications")]
        [Description("Occurs when the mouse is clicked in a sensitive margin.")]
        public event EventHandler<MarginClickEventArgs> MarginClick
        {
            add
            {
                Events.AddHandler(marginClickEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(marginClickEventKey, value);
            }
        }


        // TODO This isn't working in my tests. Could be Windows Forms interfering.
        /// <summary>
        /// Occurs when the mouse was right-clicked inside a margin that was marked as sensitive.
        /// </summary>
        /// <remarks>The <see cref="Margin.Sensitive" /> property and <see cref="PopupMode.Text" /> must be set for a margin to raise this event.</remarks>
        /// <seealso cref="UsePopup(PopupMode)" />
        [Category("Notifications")]
        [Description("Occurs when the mouse is right-clicked in a sensitive margin.")]
        public event EventHandler<MarginClickEventArgs> MarginRightClick
        {
            add
            {
                Events.AddHandler(marginRightClickEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(marginRightClickEventKey, value);
            }
        }

        /// <summary>
        /// Occurs when a user attempts to change text while the document is in read-only mode.
        /// </summary>
        /// <seealso cref="ReadOnly" />
        [Category("Notifications")]
        [Description("Occurs when an attempt is made to change text in read-only mode.")]
        public event EventHandler<EventArgs> ModifyAttempt
        {
            add
            {
                Events.AddHandler(modifyAttemptEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(modifyAttemptEventKey, value);
            }
        }

        /// <summary>
        /// Occurs when the control determines hidden text needs to be shown.
        /// </summary>
        /// <remarks>An example of when this event might be raised is if the end of line of a contracted fold point is deleted.</remarks>
        [Category("Notifications")]
        [Description("Occurs when hidden (folded) text should be shown.")]
        public event EventHandler<NeedShownEventArgs> NeedShown
        {
            add
            {
                Events.AddHandler(needShownEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(needShownEventKey, value);
            }
        }

        internal event EventHandler<SCNotificationEventArgs> SCNotification
        {
            add
            {
                Events.AddHandler(scNotificationEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(scNotificationEventKey, value);
            }
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event PaintEventHandler Paint
        {
            add
            {
                base.Paint += value;
            }
            remove
            {
                base.Paint -= value;
            }
        }

        /// <summary>
        /// Occurs when painting has just been done.
        /// </summary>
        [Category("Notifications")]
        [Description("Occurs when the control is painted.")]
        public event EventHandler<EventArgs> Painted
        {
            add
            {
                Events.AddHandler(paintedEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(paintedEventKey, value);
            }
        }

        /// <summary>
        /// Occurs when the document becomes 'dirty'.
        /// </summary>
        /// <remarks>The document 'dirty' state can be checked with the <see cref="Modified" /> property and reset by calling <see cref="SetSavePoint" />.</remarks>
        /// <seealso cref="SetSavePoint" />
        /// <seealso cref="SavePointReached" />
        [Category("Notifications")]
        [Description("Occurs when a save point is left and the document becomes dirty.")]
        public event EventHandler<EventArgs> SavePointLeft
        {
            add
            {
                Events.AddHandler(savePointLeftEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(savePointLeftEventKey, value);
            }
        }

        /// <summary>
        /// Occurs when the document 'dirty' flag is reset.
        /// </summary>
        /// <remarks>The document 'dirty' state can be reset by calling <see cref="SetSavePoint" /> or undoing an action that modified the document.</remarks>
        /// <seealso cref="SetSavePoint" />
        /// <seealso cref="SavePointLeft" />
        [Category("Notifications")]
        [Description("Occurs when a save point is reached and the document is no longer dirty.")]
        public event EventHandler<EventArgs> SavePointReached
        {
            add
            {
                Events.AddHandler(savePointReachedEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(savePointReachedEventKey, value);
            }
        }

        /// <summary>
        /// Occurs when the control is about to display or print text and requires styling.
        /// </summary>
        /// <remarks>
        /// This event is only raised when <see cref="Lexer" /> is set to <see cref="Zeroit.Framework.CodeBox.Lexer.Container" />.
        /// The last position styled correctly can be determined by calling <see cref="GetEndStyled" />.
        /// </remarks>
        /// <seealso cref="GetEndStyled" />
        [Category("Notifications")]
        [Description("Occurs when the text needs styling.")]
        public event EventHandler<StyleNeededEventArgs> StyleNeeded
        {
            add
            {
                Events.AddHandler(styleNeededEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(styleNeededEventKey, value);
            }
        }

        /// <summary>
        /// Occurs when the control UI is updated as a result of changes to text (including styling),
        /// selection, and/or scroll positions.
        /// </summary>
        [Category("Notifications")]
        [Description("Occurs when the control UI is updated.")]
        public event EventHandler<UpdateUIEventArgs> UpdateUI
        {
            add
            {
                Events.AddHandler(updateUIEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(updateUIEventKey, value);
            }
        }

        /// <summary>
        /// Occurs when the user zooms the display using the keyboard or the <see cref="Zoom" /> property is changed.
        /// </summary>
        [Category("Notifications")]
        [Description("Occurs when the control is zoomed.")]
        public event EventHandler<EventArgs> ZoomChanged
        {
            add
            {
                Events.AddHandler(zoomChangedEventKey, value);
            }
            remove
            {
                Events.RemoveHandler(zoomChangedEventKey, value);
            }
        }

        #endregion Events
        
    }
}