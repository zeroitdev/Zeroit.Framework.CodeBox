// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="Tuple.cs" company="Zeroit Dev">
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
using System.Diagnostics;
using System.Globalization;

namespace Zeroit.Framework.CodeBox
{
  static class Tuple
  {
    public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
    {
      return new Tuple<T1, T2>(item1, item2);
    }
  }

  [DebuggerDisplay("Item1={Item1};Item2={Item2}")]
  class Tuple<T1, T2> : IFormattable
  {
    public T1 Item1 { get; private set; }
    public T2 Item2 { get; private set; }

    public Tuple(T1 item1, T2 item2)
    {
      Item1 = item1;
      Item2 = item2;
    }

    #region Optional - If you need to use in dictionaries or check equality
    private static readonly IEqualityComparer<T1> Item1Comparer = EqualityComparer<T1>.Default;
    private static readonly IEqualityComparer<T2> Item2Comparer = EqualityComparer<T2>.Default;

    public override int GetHashCode()
    {
      var hc = 0;
      if (!object.ReferenceEquals(Item1, null))
        hc = Item1Comparer.GetHashCode(Item1);
      if (!object.ReferenceEquals(Item2, null))
        hc = (hc << 3) ^ Item2Comparer.GetHashCode(Item2);
      return hc;
    }
    public override bool Equals(object obj)
    {
      var other = obj as Tuple<T1, T2>;
      if (object.ReferenceEquals(other, null))
        return false;
      else
        return Item1Comparer.Equals(Item1, other.Item1) && Item2Comparer.Equals(Item2, other.Item2);
    }
    #endregion

    #region Optional - If you need to do string-based formatting
    public override string ToString() { return ToString(null, CultureInfo.CurrentCulture); }
    public string ToString(string format, IFormatProvider formatProvider)
    {
      return string.Format(formatProvider, format ?? "{0},{1}", Item1, Item2);
    }
    #endregion
  }
}
