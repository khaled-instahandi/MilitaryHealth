using System.Linq.Expressions;

public class Sort<T>
{
    public Expression<Func<T, object>> KeySelector { get; set; } = default!;
    public bool Descending { get; set; } = false;
}