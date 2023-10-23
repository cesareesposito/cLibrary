namespace cLibrary.Helper
{
    public static class cEnumerableExtensions
    {
        public static TResult? cForEach<T, TResult>(this IEnumerable<T> source, Func<T, TResult> action)
        {
            if (source == null) return default;

            foreach (var element in source)
            {
                var result = action(element);
                if (result != null)
                    return result;
            }
            return default;
        }
        public static async Task<TResult?> cForEachAsync<T, TResult>(this IEnumerable<T> source, Func<T, TResult> action)
        {
            if (source == null) return default;

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
            if (source == null) return;

            foreach (T element in source)
                action(element);
        }
       
        public static async Task cForEachAsync<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null) return;

            await Task.WhenAll(source.Select(element => Task.Run(() => action(element))));
        }
    }
}
