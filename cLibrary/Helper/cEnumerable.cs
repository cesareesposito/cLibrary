using cLibrary.Models.Base;

namespace cLibrary.Helper
{
    public static class cEnumerableExtensions
    {
        public static void cForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T element in source)
                action(element);

        }

        public static cResult<TResult> cForEach<T, TResult>(this IEnumerable<T> source, Func<T, TResult> action)
        {
            foreach (T element in source)
            {
                TResult result = action(element);
                return new cResult<TResult>(result);
            }

            return new cResult<TResult>(default);
        }
    }
}
