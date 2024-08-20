using FluentValidation.Results;

namespace Ordering.Application.Common.Exceptions;

public class OrderingException : ApplicationException{
    public IDictionary<string, string[]> Errors { get; set; }
    public OrderingException() { 
        Errors = new Dictionary<string, string[]>();
    }

    public OrderingException(IEnumerable<ValidationFailure> failures) : this() {
        Errors = failures
        .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
        .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }
    public OrderingException(string message) : base(message) { }
    public OrderingException(string message, Exception innerException) : base(message, innerException) { }
}