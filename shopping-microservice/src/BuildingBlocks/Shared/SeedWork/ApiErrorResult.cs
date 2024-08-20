namespace BuildingBlocks.Core.SeedWork;

public class ApiErrorResult<T> : ApiResult<T>{
    
    public List<string> Errors { get; set; }
    public ApiErrorResult(List<string> errors) : base(false){
        Errors = errors;
    }
}