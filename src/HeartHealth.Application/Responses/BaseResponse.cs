using Ardalis.GuardClauses;
using System.Collections.Generic;

namespace HeartHealth.Application.Responses
{
    public class BaseResponse
    {
        public bool WasSuccessful { get; set; }
        private readonly List<string> _validationErrors = new List<string>();
        public IEnumerable<string> ValidationErrors => _validationErrors.AsReadOnly();

        public BaseResponse()
        {
            WasSuccessful = true;
        }
        public BaseResponse(bool wasSuccessful)
        {
            WasSuccessful = wasSuccessful;
        }

        public virtual void AddError(string error)
        {
            Guard.Against.NullOrWhiteSpace(error, nameof(error));
            _validationErrors.Add(error);
        }
    }
}
