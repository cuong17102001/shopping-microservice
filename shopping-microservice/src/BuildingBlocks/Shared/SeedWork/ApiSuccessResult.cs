namespace BuildingBlocks.Core.SeedWork;

public class ApiSuccessResult<T> : ApiResult<T>
{
    public ApiSuccessResult(T result) : base(result){
        IsSuccess = true;
    }
}