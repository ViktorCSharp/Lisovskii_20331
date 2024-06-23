using System.Text.Json;

namespace Lisovskii_20331.UI.Data
{
    public static class SessionExtension
    {
        public static void Set<T>(this ISession session, string key, T item)
        {
            var serializedItem = JsonSerializer.Serialize(item);
            session.SetString(key, serializedItem);
        }

        public static T Get<T>(this ISession session, string key)
        {
            var item = session.GetString(key);
            return item == null
                ? Activator.CreateInstance<T>() // или default(T) 
                : JsonSerializer.Deserialize<T>(item);
        }
    }
}
