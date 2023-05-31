namespace cLibrary.Helper
{
    public static class cEnumerableExtensions
    {
        public static void cForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T element in source)
                action(element);
        }
        //public static IEnumerable<T> cForEach<T>(this IEnumerable<T> source, Action<T> action)
        //{
        //    foreach (T element in source)
        //    {
        //        action(element);
        //        yield return element;
        //    }
        //}
    }
}
