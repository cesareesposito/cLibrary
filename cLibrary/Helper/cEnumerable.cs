﻿using cLibrary.Models;
using cLibrary.Models.Base;

namespace cLibrary.Helper
{
    public static class cEnumerableExtensions
    { 
        public static TResult cForEach<T, TResult>(this IEnumerable<T> source, Func<T, TResult> action)
        {
            foreach (var element in source)
            {
                var result = action(element);
                if (result != null)
                {
                    return result;
                }
            }
            return default;
        }
       
        public static void cForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T element in source)
                action(element);
        }        
    }
}
