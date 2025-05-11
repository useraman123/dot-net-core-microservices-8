using FluentValidation.Results;
using System.Collections;

namespace Order.Application.Exception;

public class ValidationException:ApplicationException
{
    public Dictionary<string, string[]> Errors { get;  }
    public ValidationException():base("One or more validation error(s) occurred")
    {
        Errors = new Dictionary<string, string[]>();
    }
    /// <summary>
    /// ValidationFailure this going to hold the collection of error
    /// </summary>
    /// <param name="failures"></param>
    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures.GroupBy(x=>x.PropertyName,x=>x.ErrorMessage)
                    .ToDictionary(failures=>failures.Key,failures=>failures.ToArray());
    }
}
