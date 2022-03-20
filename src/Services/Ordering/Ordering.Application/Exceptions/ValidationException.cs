using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {

        public ValidationException() : base("One or more validations exception")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            failures
                .GroupBy(i => i.PropertyName, i => i.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToList());
        }




        public IDictionary<string, string[]> Errors { get; }
    }
}
