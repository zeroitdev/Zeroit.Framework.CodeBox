// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="NativeMemoryStream.cs" company="Zeroit Dev">
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
using System.IO;
using System.Runtime.InteropServices;

namespace Zeroit.Framework.CodeBox
{
    /// <summary>
    /// Like an UnmanagedMemoryStream execpt it can grow.
    /// </summary>
    internal sealed unsafe class NativeMemoryStream : Stream
    {
        #region Fields

        private IntPtr ptr;
        private int capacity;
        private int position;
        private int length;

        #endregion Fields

        #region Methods

        protected override void Dispose(bool disposing)
        {
            if (FreeOnDispose && ptr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(ptr);
                ptr = IntPtr.Zero;
            }

            base.Dispose(disposing);
        }

        public override void Flush()
        {
            // NOP
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    position = (int)offset;
                    break;

                default:
                    throw new NotImplementedException();
            }

            return position;
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if ((position + count) > capacity)
            {
                // Realloc buffer
                var minCapacity = (position + count);
                var newCapacity = (capacity * 2);
                if (newCapacity < minCapacity)
                    newCapacity = minCapacity;

                var newPtr = Marshal.AllocHGlobal(newCapacity);
                NativeMethods.MoveMemory(newPtr, ptr, length);
                Marshal.FreeHGlobal(ptr);

                ptr = newPtr;
                capacity = newCapacity;
            }

            Marshal.Copy(buffer, offset, (IntPtr)((long)ptr + position), count);
            position += count;
            length = Math.Max(length, position);
        }

        #endregion Methods

        #region Properties

        public override bool CanRead
        {
            get { throw new NotImplementedException(); }
        }

        public override bool CanSeek
        {
            get
            {
                return true;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }

        public bool FreeOnDispose { get; set; }

        public override long Length
        {
            get
            {
                return length;
            }
        }

        public IntPtr Pointer
        {
            get
            {
                return ptr;
            }
        }

        public override long Position
        {
            get
            {
                return position;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion Properties

        #region Constructors

        public NativeMemoryStream(int capacity)
        {
            if (capacity < 4)
                capacity = 4;

            this.capacity = capacity;
            this.ptr = Marshal.AllocHGlobal(capacity);
            FreeOnDispose = true;
        }

        #endregion Constructors
    }
}
