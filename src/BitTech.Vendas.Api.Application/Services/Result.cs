namespace BitTech.Vendas.Api.Application.Services;

public class Result<T>
{
    public bool IsSuccess { get; private set; }
    public bool IsFailure => !IsSuccess;
    public T? Data { get; private set; }
    public List<string> Errors { get; private set; } = [];

    private Result(bool isSuccess, T? data, List<string> errors)
    {
        IsSuccess = isSuccess;
        Data = data;
        Errors = errors;
    }

    public static Result<T> Success(T data) => new(true, data, []);
    public static Result<T> Failure(List<string> errors) => new(false, default, errors);
    public static Result<T> Failure(string error) => new(false, default, [error]);
}