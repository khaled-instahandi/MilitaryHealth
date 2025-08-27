using System.Linq.Expressions;
using System.Reflection;

namespace Application.Common.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> OrderByProperty<T>(this IQueryable<T> source, string propertyName, bool descending = false)
    {
        if (string.IsNullOrEmpty(propertyName))
            return source;

        var parameter = Expression.Parameter(typeof(T), "x");
        Expression property = parameter;
        foreach (var prop in propertyName.Split('.'))
        {
            property = Expression.PropertyOrField(property, prop);
        }

        var lambda = Expression.Lambda(property, parameter);

        string methodName = descending ? "OrderByDescending" : "OrderBy";

        var result = typeof(Queryable).GetMethods()
            .First(m => m.Name == methodName
                     && m.GetParameters().Length == 2)
            .MakeGenericMethod(typeof(T), property.Type)
            .Invoke(null, new object[] { source, lambda });

        return (IQueryable<T>)result;
    }
}
