using System.Reflection;

namespace cLibrary.Models
{
    public abstract class cEnum : IComparable
    {
        public cEnum() { }
        public string Label { get; private set; } = string.Empty;
        public dynamic Value { get; private set; } = string.Empty;

        protected cEnum(dynamic value, string label) => (Value, Label) = (value, label);

        public override string ToString() => Label;

        public static IEnumerable<T> GetAll<T>() where T : cEnum =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                     .Select(f => f.GetValue(null))
                     .Cast<T>();

        public override bool Equals(object? obj)
        {
            if (!(obj is cEnum))
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Value.Equals(((cEnum)obj).Value);

            return typeMatches && valueMatches;
        }

        public int CompareTo(object? other) => Value.CompareTo(((cEnum)(other?? new ())).Value);
        public static T FromValue<T>(int value) where T : cEnum, new()
        {
            var matchingItem = parse<T, int>(value, "value", item => item.Value == value);
            return matchingItem;
        }
        private static T parse<T, K>(K value, string description, Func<T, bool> predicate) where T : cEnum, new()
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
            {
                var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(T));
                throw new ApplicationException(message);
            }

            return matchingItem;
        }
    }   
}
