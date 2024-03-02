using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace SharpOutcome
{
    /// <summary>
    /// Represents an outcome that can either be a good outcome of type <typeparamref name="TGoodOutcome"/> or a bad outcome of type <typeparamref name="TBadOutcome"/>.
    /// </summary>
    /// <typeparam name="TGoodOutcome">The type of the good outcome.</typeparam>
    /// <typeparam name="TBadOutcome">The type of the bad outcome.</typeparam>
    public readonly struct Outcome<TGoodOutcome, TBadOutcome>
        where TGoodOutcome : notnull
        where TBadOutcome : notnull
    {
        private readonly TGoodOutcome? _goodOutcome;
        private readonly TBadOutcome? _badOutcome;
        private readonly bool _isBadOutcome;

        private Outcome(TGoodOutcome goodOutcome)
        {
            _isBadOutcome = false;
            _goodOutcome = goodOutcome;
            _badOutcome = default;
        }

        private Outcome(TBadOutcome badOutcome)
        {
            _isBadOutcome = true;
            _badOutcome = badOutcome;
            _goodOutcome = default;
        }


        public static implicit operator Outcome<TGoodOutcome, TBadOutcome>(TGoodOutcome goodOutcome)
        {
            return new Outcome<TGoodOutcome, TBadOutcome>(goodOutcome);
        }

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
            if (_isBadOutcome is false && _goodOutcome is not null)
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
            if (_isBadOutcome && _badOutcome is not null)
            {
                badOutcome = _badOutcome;
                return true;
            }

            badOutcome = default;
            return false;
        }


        public bool TryPickGoodOutcome([NotNullWhen(true)] out TGoodOutcome? goodOutcome,
            [NotNullWhen(false)] out TBadOutcome? badOutcome)
        {
            if (_isBadOutcome is false && _goodOutcome is not null)
            {
                goodOutcome = _goodOutcome;
                badOutcome = default;
                return _goodOutcome is not null;
            }

            badOutcome = _badOutcome;
            goodOutcome = default;
            return _badOutcome is not null;
        }

        public bool TryPickBadOutcome([NotNullWhen(true)] out TBadOutcome? badOutcome,
            [NotNullWhen(false)] out TGoodOutcome? goodOutcome)
        {
            if (_isBadOutcome && _badOutcome is not null)
            {
                badOutcome = _badOutcome;
                goodOutcome = default;
                return _badOutcome is not null;
            }

            goodOutcome = _goodOutcome;
            badOutcome = default;
            return _goodOutcome is not null;
        }


        public TOutput Match<TOutput>(Func<TGoodOutcome, TOutput> onGoodOutcome,
            Func<TBadOutcome, TOutput> onBadOutcome)
        {
            return _isBadOutcome
                ? onBadOutcome(_badOutcome ?? throw new InvalidOperationException())
                : onGoodOutcome(_goodOutcome ?? throw new InvalidOperationException());
        }

        public async Task<TOutput> MatchAsync<TOutput>(Func<TGoodOutcome, Task<TOutput>> onGoodOutcome,
            Func<TBadOutcome, TOutput> onBadOutcome)
        {
            return _isBadOutcome
                ? onBadOutcome(_badOutcome ?? throw new InvalidOperationException())
                : await onGoodOutcome(_goodOutcome ?? throw new InvalidOperationException());
        }

        public async Task<TOutput> MatchAsync<TOutput>(Func<TGoodOutcome, Task<TOutput>> onGoodOutcome,
            Func<TBadOutcome, Task<TOutput>> onBadOutcome)
        {
            return _isBadOutcome
                ? await onBadOutcome(_badOutcome ?? throw new InvalidOperationException())
                : await onGoodOutcome(_goodOutcome ?? throw new InvalidOperationException());
        }


        public async Task<TOutput> MatchAsync<TOutput>(Func<TGoodOutcome, TOutput> onGoodOutcome,
            Func<TBadOutcome, Task<TOutput>> onBadOutcome)
        {
            return _isBadOutcome
                ? await onBadOutcome(_badOutcome ?? throw new InvalidOperationException())
                : onGoodOutcome(_goodOutcome ?? throw new InvalidOperationException());
        }

        public void Switch(Action<TGoodOutcome> onGoodOutcome, Action<TBadOutcome> onBadOutcome)
        {
            if (_isBadOutcome) onBadOutcome(_badOutcome ?? throw new InvalidOperationException());
            else onGoodOutcome(_goodOutcome ?? throw new InvalidOperationException());
        }

        public async Task SwitchAsync(Func<TGoodOutcome, Task> onGoodOutcome, Action<TBadOutcome> onBadOutcome)
        {
            if (_isBadOutcome) onBadOutcome(_badOutcome ?? throw new InvalidOperationException());
            else await onGoodOutcome(_goodOutcome ?? throw new InvalidOperationException());
        }

        public async Task SwitchAsync(Func<TGoodOutcome, Task> onGoodOutcome, Func<TBadOutcome, Task> onBadOutcome)
        {
            if (_isBadOutcome) await onBadOutcome(_badOutcome ?? throw new InvalidOperationException());
            else await onGoodOutcome(_goodOutcome ?? throw new InvalidOperationException());
        }

        public async Task SwitchAsync(Action<TGoodOutcome> onGoodOutcome, Func<TBadOutcome, Task> onBadOutcome)
        {
            if (_isBadOutcome) await onBadOutcome(_badOutcome ?? throw new InvalidOperationException());
            else onGoodOutcome(_goodOutcome ?? throw new InvalidOperationException());
        }
    }
}