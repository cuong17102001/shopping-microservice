namespace BuildingBlocks.Core.SeedWork;

public class ApiResult<T>
{
    public ApiResult(){

    }
    public ApiResult(bool isSuccess){
        IsSuccess = isSuccess;
    }
    public ApiResult(string message){
        this.Message = message;
    }
    public ApiResult(T result){
        Data = result;
    }
    public ApiResult(T result, string message){
        Data = result;
        Message = message;
    }

    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}