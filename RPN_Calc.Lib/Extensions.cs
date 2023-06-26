using System;
using System.Collections.Generic;
using System.Text;

namespace RPN_Calc.Lib;

internal static class Extensions
{
    public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)
        => self?.Select((item, index) => (item, index)) ?? new List<(T, int)>();
}
