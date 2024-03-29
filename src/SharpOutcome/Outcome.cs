using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace SharpOutcome
{
    /// <summary>
    /// Represents an outcome that can either be a good outcome of type <typeparamref name="TGoodOutcome"/> or a bad outcome of type <typeparamref name="TBadOutcome"/>.
    /// </summary>
    /// <typeparam name="TGoodOutcome">The type of the good outcome.</typeparam>
    /// <typeparam name="TBadOutcome">The type of the bad outcome.</typeparam>
    public class Outcome<TGoodOutcome, TBadOutcome>
        where TGoodOutcome : notnull
        where TBadOutcome : notnull

    {
        private readonly TGoodOutcome? _goodOutcome;
        private readonly TBadOutcome? _badOutcome;

        /// <summary>
        /// Gets a boolean value indicating whether the outcome is a good outcome.
        /// </summary>
        public bool IsGoodOutcome { get; }

        /// <summary>
        /// Gets a boolean value indicating whether the outcome is a bad outcome.
        /// </summary>
        public bool IsBadOutcome { get; }

        private Outcome(TGoodOutcome goodOutcome)
        {
            IsGoodOutcome = true;
            IsBadOutcome = false;
            _goodOutcome = goodOutcome;
            _badOutcome = default;
        }

        private Outcome(TBadOutcome badOutcome)
        {
            IsGoodOutcome = false;
            IsBadOutcome = true;
            _badOutcome = badOutcome;
            _goodOutcome = default;
        }

        /// <summary>
        /// Implicitly converts a value of type <typeparamref name="TGoodOutcome"/> to an <see cref="Outcome{TGoodOutcome, TBadOutcome}"/> with a good outcome.
        /// </summary>
        /// <param name="goodOutcome">The value of type <typeparamref name="TGoodOutcome"/> to convert.</param>
        /// <returns>An <see cref="Outcome{TGoodOutcome, TBadOutcome}"/> with a good outcome.</returns>
        public static implicit operator Outcome<TGoodOutcome, TBadOutcome>(TGoodOutcome goodOutcome)
        {
            return new Outcome<TGoodOutcome, TBadOutcome>(goodOutcome);
        }

        /// <summary>
        /// Implicitly converts a value of type <typeparamref name="TBadOutcome"/> to an <see cref="Outcome{TGoodOutcome, TBadOutcome}"/> with a bad outcome.
        /// </summary>
        /// <param name="badOutcome">The value of type <typeparamref name="TBadOutcome"/> to convert.</param>
        /// <returns>An <see cref="Outcome{TGoodOutcome, TBadOutcome}"/> with a bad outcome.</returns>
        public static implicit operator Outcome<TGoodOutcome, TBadOutcome>(TBadOutcome badOutcome)
        {
            return new Outcome<TGoodOutcome, TBadOutcome>(badOutcome);
        }

        /// <summary>
        /// Tries to pick the good outcome from the <see cref="Outcome{TGoodOutcome, TBadOutcome}"/>.
        /// </summary>
        /// <param name="goodOutcome">When this method returns, contains the good outcome if it exists; otherwise, the default value.</param>
        /// <returns><c>true</c> if the good outcome exists; otherwise, <c>false</c>.</returns>
        public bool TryPickGoodOutcome([NotNullWhen(true)] out TGoodOutcome? goodOutcome)
        {
            if (IsBadOutcome is false && _goodOutcome is not null)
            {
                goodOutcome = _goodOutcome;
                return true;
            }

            goodOutcome = default;
            return false;
        }

        /// <summary>
        /// Tries to pick the bad outcome from the <see cref="Outcome{TGoodOutcome, TBadOutcome}"/>.
        /// </summary>
        /// <param name="badOutcome">When this method returns, contains the bad outcome if it exists; otherwise, the default value.</param>
        /// <returns><c>true</c> if the bad outcome exists; otherwise, <c>false</c>.</returns>
        public bool TryPickBadOutcome([NotNullWhen(true)] out TBadOutcome? badOutcome)
        {
            if (IsBadOutcome && _badOutcome is not null)
            {
                badOutcome = _badOutcome;
                return true;
            }

            badOutcome = default;
            return false;
        }

        /// <summary>
        /// Tries to pick the good outcome from the <see cref="Outcome{TGoodOutcome, TBadOutcome}"/>.
        /// </summary>
        /// <param name="goodOutcome">When this method returns, contains the good outcome if it exists; otherwise, the default value.</param>
        /// <param name="badOutcome">When this method returns, contains the bad outcome if the good outcome does not exists; otherwise, the default value.</param>
        /// <returns><c>true</c> if the good outcome exists; otherwise, <c>false</c>.</returns>
        public bool TryPickGoodOutcome([NotNullWhen(true)] out TGoodOutcome? goodOutcome,
            [NotNullWhen(false)] out TBadOutcome? badOutcome)
        {
            if (IsBadOutcome is false && _goodOutcome is not null)
            {
                goodOutcome = _goodOutcome;
                badOutcome = default;
                return _goodOutcome is not null;
            }

            badOutcome = _badOutcome;
            goodOutcome = default;
            return EqualityComparer<TBadOutcome>.Default.Equals(_badOutcome, default);
        }

        /// <summary>
        /// Tries to pick the bad outcome from the <see cref="Outcome{TGoodOutcome, TBadOutcome}"/>.
        /// </summary>
        /// <param name="badOutcome">When this method returns, contains the bad outcome if it exists; otherwise, the default value.</param>
        /// <param name="goodOutcome">When this method returns, contains the good outcome if the bad outcome does not exists; otherwise, the default value.</param>
        /// <returns><c>true</c> if the bad outcome exists; otherwise, <c>false</c>.</returns>
        public bool TryPickBadOutcome([NotNullWhen(true)] out TBadOutcome? badOutcome,
            [NotNullWhen(false)] out TGoodOutcome? goodOutcome)
        {
            if (IsBadOutcome && _badOutcome is not null)
            {
                badOutcome = _badOutcome;
                goodOutcome = default;
                return _badOutcome is not null;
            }

            goodOutcome = _goodOutcome;
            badOutcome = default;
            return EqualityComparer<TGoodOutcome>.Default.Equals(_goodOutcome, default);
        }

        /// <summary>
        /// Matches the outcome and executes the appropriate delegate based on whether the outcome is good or bad.
        /// </summary>
        /// <typeparam name="TOutput">The type of the output.</typeparam>
        /// <param name="onGoodOutcome">The delegate to execute if the outcome is good.</param>
        /// <param name="onBadOutcome">The delegate to execute if the outcome is bad.</param>
        /// <returns>The result of executing the appropriate delegate.</returns>
        public TOutput Match<TOutput>(Func<TGoodOutcome, TOutput> onGoodOutcome,
            Func<TBadOutcome, TOutput> onBadOutcome)
        {
            return IsBadOutcome
                ? onBadOutcome(_badOutcome ?? throw new InvalidOperationException())
                : onGoodOutcome(_goodOutcome ?? throw new InvalidOperationException());
        }


        /// <summary>
        /// Matches the outcome and asynchronously executes the appropriate delegate based on whether the outcome is good or bad.
        /// </summary>
        /// <typeparam name="TOutput">The type of the output.</typeparam>
        /// <param name="onGoodOutcome">The asynchronous delegate to execute if the outcome is good.</param>
        /// <param name="onBadOutcome">The asynchronous delegate to execute if the outcome is bad.</param>
        /// <returns>A task that represents the asynchronous operation and contains the result of executing the appropriate delegate.</returns>
        public async Task<TOutput> MatchAsync<TOutput>(Func<TGoodOutcome, Task<TOutput>> onGoodOutcome,
            Func<TBadOutcome, Task<TOutput>> onBadOutcome)
        {
            return IsBadOutcome
                ? await onBadOutcome(_badOutcome ?? throw new InvalidOperationException())
                : await onGoodOutcome(_goodOutcome ?? throw new InvalidOperationException());
        }


        /// <summary>
        /// Matches the outcome and asynchronously executes the appropriate delegate based on whether the outcome is good or bad.
        /// </summary>
        /// <typeparam name="TOutput">The type of the output.</typeparam>
        /// <param name="onGoodOutcome">The asynchronous delegate to execute if the outcome is good.</param>
        /// <param name="onBadOutcome">The delegate to execute if the outcome is bad.</param>
        /// <returns>A task that represents the asynchronous operation and contains the result of executing the appropriate delegate.</returns>
        public async Task<TOutput> MatchAsync<TOutput>(Func<TGoodOutcome, Task<TOutput>> onGoodOutcome,
            Func<TBadOutcome, TOutput> onBadOutcome)
        {
            return IsBadOutcome
                ? onBadOutcome(_badOutcome ?? throw new InvalidOperationException())
                : await onGoodOutcome(_goodOutcome ?? throw new InvalidOperationException());
        }


        /// <summary>
        /// Matches the outcome and asynchronously executes the appropriate delegate based on whether the outcome is good or bad.
        /// </summary>
        /// <typeparam name="TOutput">The type of the output.</typeparam>
        /// <param name="onGoodOutcome">The delegate to execute if the outcome is good.</param>
        /// <param name="onBadOutcome">The asynchronous delegate to execute if the outcome is bad.</param>
        /// <returns>A task that represents the asynchronous operation and contains the result of executing the appropriate delegate.</returns>
        public async Task<TOutput> MatchAsync<TOutput>(Func<TGoodOutcome, TOutput> onGoodOutcome,
            Func<TBadOutcome, Task<TOutput>> onBadOutcome)
        {
            return IsBadOutcome
                ? await onBadOutcome(_badOutcome ?? throw new InvalidOperationException())
                : onGoodOutcome(_goodOutcome ?? throw new InvalidOperationException());
        }

        /// <summary>
        /// Executes the appropriate delegate based on whether the outcome is good or bad.
        /// </summary>
        /// <param name="onGoodOutcome">The delegate to execute if the outcome is good.</param>
        /// <param name="onBadOutcome">The delegate to execute if the outcome is bad.</param>
        public void Switch(Action<TGoodOutcome> onGoodOutcome, Action<TBadOutcome> onBadOutcome)
        {
            if (IsBadOutcome) onBadOutcome(_badOutcome ?? throw new InvalidOperationException());
            else onGoodOutcome(_goodOutcome ?? throw new InvalidOperationException());
        }


        /// <summary>
        /// Asynchronously executes the appropriate delegate based on whether the outcome is good or bad.
        /// </summary>
        /// <param name="onGoodOutcome">The asynchronous delegate to execute if the outcome is good.</param>
        /// <param name="onBadOutcome">The asynchronous delegate to execute if the outcome is bad.</param>
        public async Task SwitchAsync(Func<TGoodOutcome, Task> onGoodOutcome, Func<TBadOutcome, Task> onBadOutcome)
        {
            if (IsBadOutcome) await onBadOutcome(_badOutcome ?? throw new InvalidOperationException());
            else await onGoodOutcome(_goodOutcome ?? throw new InvalidOperationException());
        }


        /// <summary>
        /// Asynchronously executes the appropriate delegate based on whether the outcome is good or bad.
        /// </summary>
        /// <param name="onGoodOutcome">The asynchronous delegate to execute if the outcome is good.</param>
        /// <param name="onBadOutcome">The delegate to execute if the outcome is bad.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task SwitchAsync(Func<TGoodOutcome, Task> onGoodOutcome, Action<TBadOutcome> onBadOutcome)
        {
            if (IsBadOutcome) onBadOutcome(_badOutcome ?? throw new InvalidOperationException());
            else await onGoodOutcome(_goodOutcome ?? throw new InvalidOperationException());
        }


        /// <summary>
        /// Asynchronously executes the appropriate delegate based on whether the outcome is good or bad.
        /// </summary>
        /// <param name="onGoodOutcome">The delegate to execute if the outcome is good.</param>
        /// <param name="onBadOutcome">The asynchronous delegate to execute if the outcome is bad.</param>
        public async Task SwitchAsync(Action<TGoodOutcome> onGoodOutcome, Func<TBadOutcome, Task> onBadOutcome)
        {
            if (IsBadOutcome) await onBadOutcome(_badOutcome ?? throw new InvalidOperationException());
            else onGoodOutcome(_goodOutcome ?? throw new InvalidOperationException());
        }
    }
}