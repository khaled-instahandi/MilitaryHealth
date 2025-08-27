using System.Linq.Expressions;

public class Filter<T>
{
    public Expression<Func<T, bool>>? Expression { get; set; }
}