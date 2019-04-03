using System;

namespace Zeroit.Framework.CodeBox
{
    /// <summary>
    /// Provides data for the <see cref="Scintilla.Insert" /> and <see cref="Scintilla.Delete" /> events.
    /// </summary>
    public class ModificationEventArgs : BeforeModificationEventArgs
    {
        private readonly ZeroitCodeExplorer Scintilla;
        private readonly int bytePosition;
        private readonly int byteLength;
        private readonly IntPtr textPtr;

        /// <summary>
        /// Gets the number of lines added or removed.
        /// </summary>
        /// <returns>The number of lines added to the document when text is inserted, or the number of lines removed from the document when text is deleted.</returns>
        /// <remarks>When lines are deleted the return value will be negative.</remarks>
        public int LinesAdded { get; private set; }

        /// <summary>
        /// Gets the text that was inserted or deleted.
        /// </summary>
        /// <returns>The text inserted or deleted from the document.</returns>
        public override unsafe string Text
        {
            get
            {
                if (CachedText == null)
                    CachedText = Helpers.GetString(textPtr, byteLength, Scintilla.Encoding);

                return CachedText;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModificationEventArgs" /> class.
        /// </summary>
        /// <param name="Scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
        /// <param name="source">The source of the modification.</param>
        /// <param name="bytePosition">The zero-based byte position within the document where text was modified.</param>
        /// <param name="byteLength">The length in bytes of the inserted or deleted text.</param>
        /// <param name="text">>A pointer to the text inserted or deleted.</param>
        /// <param name="linesAdded">The number of lines added or removed (delta).</param>
        public ModificationEventArgs(ZeroitCodeExplorer Scintilla, ModificationSource source, int bytePosition, int byteLength, IntPtr text, int linesAdded) : base(Scintilla, source, bytePosition, byteLength, text)
        {
            this.Scintilla = Scintilla;
            this.bytePosition = bytePosition;
            this.byteLength = byteLength;
            this.textPtr = text;

            LinesAdded = linesAdded;
        }
    }
}
