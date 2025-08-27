public sealed record ApiResult(
    bool Succeeded,
    int Status,
    string Message,
    object? Data,
    Dictionary<string, string[]>? Errors,
    string? TraceId)
{
    public static ApiResult Ok(object? data, string msg = "OK", int status = 200, string? traceId = null)
        => new(true, status, msg, data, null, traceId);
    public static ApiResult Fail(string msg, int status, Dictionary<string, string[]>? errors = null, string? traceId = null)
        => new(false, status, msg, null, errors, traceId);
}
