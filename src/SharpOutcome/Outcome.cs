using System;
using System.Threading.Tasks;

namespace SharpOutcome
{
    public readonly struct Outcome<TGoodOutcome, TBadOutcome>
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

        public TOutput Match<TOutput>(Func<TGoodOutcome, TOutput> onSuccess, Func<TBadOutcome, TOutput> onFailure)
        {
            return _isBadOutcome
                ? onFailure(_badOutcome ?? throw new InvalidOperationException())
                : onSuccess(_goodOutcome ?? throw new InvalidOperationException());
        }

        public async Task<TOutput> MatchAsync<TOutput>(Func<TGoodOutcome, Task<TOutput>> onSuccess,
            Func<TBadOutcome, TOutput> onFailure)
        {
            return _isBadOutcome
                ? onFailure(_badOutcome ?? throw new InvalidOperationException())
                : await onSuccess(_goodOutcome ?? throw new InvalidOperationException());
        }

        public async Task<TOutput> MatchAsync<TOutput>(Func<TGoodOutcome, Task<TOutput>> onSuccess,
            Func<TBadOutcome, Task<TOutput>> onFailure)
        {
            return _isBadOutcome
                ? await onFailure(_badOutcome ?? throw new InvalidOperationException())
                : await onSuccess(_goodOutcome ?? throw new InvalidOperationException());
        }

        public void Switch(Action<TGoodOutcome> onSuccess, Action<TBadOutcome> onFailure)
        {
            if (_isBadOutcome)
            {
                onFailure(_badOutcome ?? throw new InvalidOperationException());
            }
            else
            {
                onSuccess(_goodOutcome ?? throw new InvalidOperationException());
            }
        }

        public async Task SwitchAsync(Func<TGoodOutcome, Task> onSuccess, Action<TBadOutcome> onFailure)
        {
            if (_isBadOutcome)
            {
                onFailure(_badOutcome ?? throw new InvalidOperationException());
            }
            else
            {
                await onSuccess(_goodOutcome ?? throw new InvalidOperationException());
            }
        }

        public async Task SwitchAsync(Func<TGoodOutcome, Task> onSuccess, Func<TBadOutcome, Task> onFailure)
        {
            if (_isBadOutcome)
            {
                await onFailure(_badOutcome ?? throw new InvalidOperationException());
            }
            else
            {
                await onSuccess(_goodOutcome ?? throw new InvalidOperationException());
            }
        }
    }
}