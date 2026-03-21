using Domain.Common.Enums;

namespace Domain.Model.ReturnEntity;

public abstract class EntityOfTResult
{
    public ErrorCode ErrorCode { get; set; }
    public bool IsCompleted { get; set; }
    public string? Message { get; set; }
    public Dictionary<string, string>? Details { get; set; }
}

public class TResult<T> : EntityOfTResult
{
    internal TResult() { }
    public T? Value { get; set; }
    public static TResult<T> CompletedOperation(T value)
        => new TResult<T> { Value = value, IsCompleted = true, ErrorCode = ErrorCode.Ok };
    public static TResult<T> FailedOperation(ErrorCode errorCode, string? message = null, Dictionary<string, string>? details = null)
        => new TResult<T> { IsCompleted = false, ErrorCode = errorCode, Message = message, Details = details };

    public static implicit operator TResult(TResult<T> value)
    {
        return new TResult { ErrorCode = value.ErrorCode, IsCompleted = value.IsCompleted };
    }

}

public class TResult : EntityOfTResult
{
    public static TResult CompletedOperation() => new TResult { IsCompleted = true, ErrorCode = ErrorCode.Ok };
    public static TResult FailedOperation(ErrorCode errorCode, string? message = null, Dictionary<string, string>? details = null)
        => new TResult { IsCompleted = false, ErrorCode = errorCode, Message = message, Details = details };

}
