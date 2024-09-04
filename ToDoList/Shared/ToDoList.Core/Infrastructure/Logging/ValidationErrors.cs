using FluentValidation;
using Newtonsoft.Json;

namespace ToDoList.Core.Infrastructure.Logging;

internal class ValidationErrors
{
    [JsonProperty("errors")]
    public string[] Errors { get; private set; }

    public static ValidationErrors Create(ValidationException exception)
    {
        var errors = exception.Errors.Select(error => error.ErrorMessage).ToArray();
        return new ValidationErrors { Errors = errors };
    }
}
