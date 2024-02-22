namespace Cmms.Api.Common.Extensions;


/// <summary>
/// Extension methods for IEnumerable
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// If the condition is true, the action is executed
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enumerable"></param>
    /// <param name="condition"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static IEnumerable<T> If<T>(
        this IEnumerable<T> enumerable,
        bool condition,
        Func<IEnumerable<T>, IEnumerable<T>> action)
    {
        ArgumentNullException.ThrowIfNull(enumerable);
        ArgumentNullException.ThrowIfNull(action);

        if (condition)
        {
            return action(enumerable);
        }

        return enumerable;
    }
}
