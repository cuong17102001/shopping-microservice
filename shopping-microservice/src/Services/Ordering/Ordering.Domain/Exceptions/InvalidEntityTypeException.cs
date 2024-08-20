namespace Ordering.Domain.Exceptions;

public class InvalidEntityTypeException : ApplicationException{
    public InvalidEntityTypeException(string entity, string type) : base($"Entity {entity} is not of type {type}."){
    }
}