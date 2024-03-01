using System;
using System.Threading.Tasks;

namespace SharpOutcome
{
    public readonly struct Outcome<TValue, TError>
    {
        private readonly TValue? _value;
        private readonly TError? _error;
        private bool IsError { get; }

        private Outcome(TValue value)
        {
            IsError = false;
            _value = value;
            _error = default;
        }

        private Outcome(TError error)
        {
            IsError = true;
            _error = error;
            _value = default;
        }

        public static implicit operator Outcome<TValue, TError>(TValue value) => new(value);

        public static implicit operator Outcome<TValue, TError>(TError error) => new(error);

        public TResult Match<TResult>(Func<TValue, TResult> success, Func<TError, TResult> failure)
        {
            return IsError
                ? failure(_error ?? throw new ArgumentNullException(nameof(failure)))
                : success(_value ?? throw new ArgumentNullException(nameof(success)));
        }

        public async Task<TResult> MatchAsync<TResult>(Func<TValue, Task<TResult>> success,
            Func<TError, TResult> failure)
        {
            return IsError
                ? failure(_error ?? throw new ArgumentNullException(nameof(failure)))
                : await success(_value ?? throw new ArgumentNullException(nameof(success)));
        }

        public void Switch(Action<TValue> success, Action<TError> failure)
        {
            if (IsError)
            {
                failure(_error ?? throw new ArgumentNullException(nameof(failure)));
            }
            else
            {
                success(_value ?? throw new ArgumentNullException(nameof(success)));
            }
        }

        public async Task SwitchAsync(Func<TValue, Task> success, Action<TError> failure)
        {
            if (IsError)
            {
                failure(_error ?? throw new ArgumentNullException(nameof(failure)));
            }
            else
            {
                await success(_value ?? throw new ArgumentNullException(nameof(success)));
            }
        }
    }
}