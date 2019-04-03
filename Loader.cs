// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="Loader.cs" company="Zeroit Dev">
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
using System.Runtime.InteropServices;
using System.Text;

namespace Zeroit.Framework.CodeBox
{
    internal sealed class Loader : ILoader
    {
        private readonly IntPtr self;
        private readonly NativeMethods.ILoaderVTable32 loader32;
        private readonly NativeMethods.ILoaderVTable64 loader64;
        private readonly Encoding encoding;

        public unsafe bool AddData(char[] data, int length)
        {
            if (data != null)
            {
                length = Helpers.Clamp(length, 0, data.Length);
                var bytes = Helpers.GetBytes(data, length, encoding, zeroTerminated: false);
                fixed (byte* bp = bytes)
                {
                    var status = (IntPtr.Size == 4 ? loader32.AddData(self, bp, bytes.Length) : loader64.AddData(self, bp, bytes.Length));
                    if (status != NativeMethods.SC_STATUS_OK)
                        return false;
                }
            }

            return true;
        }

        public Document ConvertToDocument()
        {
            var ptr = (IntPtr.Size == 4 ? loader32.ConvertToDocument(self) : loader64.ConvertToDocument(self));
            var document = new Document { Value = ptr };
            return document;
        }

        public int Release()
        {
            var count = (IntPtr.Size == 4 ? loader32.Release(self) : loader64.Release(self));
            return count;
        }

        public unsafe Loader(IntPtr ptr, Encoding encoding)
        {
            this.self = ptr;
            this.encoding = encoding;

            // http://stackoverflow.com/a/985820/2073621
            // http://stackoverflow.com/a/2094715/2073621
            // http://en.wikipedia.org/wiki/Virtual_method_table
            // http://www.openrce.org/articles/full_view/23
            // Because I know that I'm not going to remember all this... In C++, the first
            // variable of an object is a pointer (v[f]ptr) to the virtual table (v[f]table)
            // containing the addresses of each function. The first call below gets the vtable
            // address by following the object ptr to the vptr to the vtable. The second call
            // casts the vtable to a structure with the same memory layout so we can easily
            // invoke each function without having to do any pointer arithmetic. Depending on the
            // architecture, the function calling conventions can be different.

            IntPtr vfptr = *(IntPtr*)ptr;
            if(IntPtr.Size == 4)
                loader32 = (NativeMethods.ILoaderVTable32)Marshal.PtrToStructure(vfptr, typeof(NativeMethods.ILoaderVTable32));
            else
                loader64 = (NativeMethods.ILoaderVTable64)Marshal.PtrToStructure(vfptr, typeof(NativeMethods.ILoaderVTable64));
        }
    }
}
