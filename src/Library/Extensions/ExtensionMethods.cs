namespace Library.Extensions;

public static class ExtensionMethods
{
    public static T? Clone<T>(this T obj)
    {
        var instance = obj?
            .GetType()
            .GetMethod("MemberwiseClone", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

        return (T)instance?.Invoke(obj, null)!;
    }
}