using FluentValidation.Results;

namespace AFI.Models
{
    public class HandlerResult<TResult>
    {
        public ValidationResult ValidationResult { get; set; }
        public TResult Result { get; set; }
    }
}
