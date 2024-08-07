using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace SharpOutcome;

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
    private readonly bool _isGoodOutcome;
    private readonly bool _isBadOutcome;

    private const string InvalidStateErrorMsg =
        "IsGoodOutcome and IsBadOutcome can't be false at the same time.";


    public Outcome(TGoodOutcome goodOutcome)
    {
        ArgumentNullException.ThrowIfNull(goodOutcome);
        _isGoodOutcome = true;
        _isBadOutcome = false;
        _goodOutcome = goodOutcome;
        _badOutcome = default;
    }

    public Outcome(TBadOutcome badOutcome)
    {
        ArgumentNullException.ThrowIfNull(badOutcome);
        _isGoodOutcome = false;
        _isBadOutcome = true;
        _badOutcome = badOutcome;
        _goodOutcome = default;
    }

    /// <summary>
    /// Implicitly converts a value of type <typeparamref name="TGoodOutcome"/> to an <see cref="Outcome{TGoodOutcome, TBadOutcome}"/> with a good outcome.
    /// </summary>
    /// <param name="goodOutcome">The value of type <typeparamref name="TGoodOutcome"/> to convert.</param>
    /// <returns>A <see cref="Outcome{TGoodOutcome, TBadOutcome}"/> with a good outcome.</returns>
    public static implicit operator Outcome<TGoodOutcome, TBadOutcome>(TGoodOutcome goodOutcome)
    {
        return new Outcome<TGoodOutcome, TBadOutcome>(goodOutcome);
    }

    /// <summary>
    /// Implicitly converts a value of type <typeparamref name="TBadOutcome"/> to an <see cref="Outcome{TGoodOutcome, TBadOutcome}"/> with a bad outcome.
    /// </summary>
    /// <param name="badOutcome">The value of type <typeparamref name="TBadOutcome"/> to convert.</param>
    /// <returns>A <see cref="Outcome{TGoodOutcome, TBadOutcome}"/> with a bad outcome.</returns>
    public static implicit operator Outcome<TGoodOutcome, TBadOutcome>(TBadOutcome badOutcome)
    {
        return new Outcome<TGoodOutcome, TBadOutcome>(badOutcome);
    }


    /// <summary>
    /// Gets a boolean value indicating whether the outcome is a good outcome.
    /// </summary>
    public bool IsGoodOutcome()
    {
        CheckInvalidState();
        return _isGoodOutcome;
    }


    /// <summary>
    /// Gets a boolean value indicating whether the outcome is a bad outcome.
    /// </summary>
    public bool IsBadOutcome()
    {
        CheckInvalidState();
        return _isBadOutcome;
    }


    /// <summary>
    /// Tries to pick the good outcome from the <see cref="Outcome{TGoodOutcome, TBadOutcome}"/>.
    /// </summary>
    /// <param name="goodOutcome">When this method returns, contains the good outcome if it exists; otherwise, the default value.</param>
    /// <returns><c>true</c> if the good outcome exists; otherwise, <c>false</c>.</returns>
    public bool TryPickGoodOutcome([NotNullWhen(true)] out TGoodOutcome? goodOutcome)
    {
        CheckInvalidState();

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
        CheckInvalidState();

        if (_isBadOutcome && _badOutcome is not null)
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
        CheckInvalidState();

        if (_isBadOutcome is false && _goodOutcome is not null)
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
        CheckInvalidState();

        if (_isBadOutcome && _badOutcome is not null)
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
        if (_isBadOutcome)
        {
            return onBadOutcome(_badOutcome ?? throw new InvalidOperationException());
        }

        if (_isGoodOutcome)
        {
            return onGoodOutcome(_goodOutcome ?? throw new InvalidOperationException());
        }

        throw new InvalidOperationException(InvalidStateErrorMsg);
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
        if (_isBadOutcome)
        {
            return await onBadOutcome(_badOutcome ?? throw new InvalidOperationException()).ConfigureAwait(false);
        }

        if (_isGoodOutcome)
        {
            return await onGoodOutcome(_goodOutcome ?? throw new InvalidOperationException()).ConfigureAwait(false);
        }

        throw new InvalidOperationException(InvalidStateErrorMsg);
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
        if (_isBadOutcome)
        {
            return onBadOutcome(_badOutcome ?? throw new InvalidOperationException());
        }

        if (_isGoodOutcome)
        {
            return await onGoodOutcome(_goodOutcome ?? throw new InvalidOperationException()).ConfigureAwait(false);
        }

        throw new InvalidOperationException(InvalidStateErrorMsg);
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
        return _isBadOutcome
            ? await onBadOutcome(_badOutcome ?? throw new InvalidOperationException()).ConfigureAwait(false)
            : onGoodOutcome(_goodOutcome ?? throw new InvalidOperationException());
    }

    /// <summary>
    /// Executes the appropriate delegate based on whether the outcome is good or bad.
    /// </summary>
    /// <param name="onGoodOutcome">The delegate to execute if the outcome is good.</param>
    /// <param name="onBadOutcome">The delegate to execute if the outcome is bad.</param>
    public void Switch(Action<TGoodOutcome> onGoodOutcome, Action<TBadOutcome> onBadOutcome)
    {
        if (_isBadOutcome)
        {
            onBadOutcome(_badOutcome ?? throw new InvalidOperationException());
        }
        else if (_isGoodOutcome)
        {
            onGoodOutcome(_goodOutcome ?? throw new InvalidOperationException());
        }

        else
        {
            throw new InvalidOperationException(InvalidStateErrorMsg);
        }
    }


    /// <summary>
    /// Asynchronously executes the appropriate delegate based on whether the outcome is good or bad.
    /// </summary>
    /// <param name="onGoodOutcome">The asynchronous delegate to execute if the outcome is good.</param>
    /// <param name="onBadOutcome">The asynchronous delegate to execute if the outcome is bad.</param>
    public async Task SwitchAsync(Func<TGoodOutcome, Task> onGoodOutcome, Func<TBadOutcome, Task> onBadOutcome)
    {
        if (_isBadOutcome)
        {
            await onBadOutcome(_badOutcome ?? throw new InvalidOperationException()).ConfigureAwait(false);
        }
        else if (_isGoodOutcome)
        {
            await onGoodOutcome(_goodOutcome ?? throw new InvalidOperationException()).ConfigureAwait(false);
        }

        else
        {
            throw new InvalidOperationException(InvalidStateErrorMsg);
        }
    }


    /// <summary>
    /// Asynchronously executes the appropriate delegate based on whether the outcome is good or bad.
    /// </summary>
    /// <param name="onGoodOutcome">The asynchronous delegate to execute if the outcome is good.</param>
    /// <param name="onBadOutcome">The delegate to execute if the outcome is bad.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task SwitchAsync(Func<TGoodOutcome, Task> onGoodOutcome, Action<TBadOutcome> onBadOutcome)
    {
        if (_isBadOutcome)
        {
            onBadOutcome(_badOutcome ?? throw new InvalidOperationException());
        }
        else if (_isGoodOutcome)
        {
            await onGoodOutcome(_goodOutcome ?? throw new InvalidOperationException()).ConfigureAwait(false);
        }

        else
        {
            throw new InvalidOperationException(InvalidStateErrorMsg);
        }
    }


    /// <summary>
    /// Asynchronously executes the appropriate delegate based on whether the outcome is good or bad.
    /// </summary>
    /// <param name="onGoodOutcome">The delegate to execute if the outcome is good.</param>
    /// <param name="onBadOutcome">The asynchronous delegate to execute if the outcome is bad.</param>
    public async Task SwitchAsync(Action<TGoodOutcome> onGoodOutcome, Func<TBadOutcome, Task> onBadOutcome)
    {
        if (_isBadOutcome)
        {
            await onBadOutcome(_badOutcome ?? throw new InvalidOperationException()).ConfigureAwait(false);
        }
        else if (_isGoodOutcome)
        {
            onGoodOutcome(_goodOutcome ?? throw new InvalidOperationException());
        }

        else
        {
            throw new InvalidOperationException(InvalidStateErrorMsg);
        }
    }

    private void CheckInvalidState()
    {
        if (_isGoodOutcome is false && _isBadOutcome is false)
        {
            throw new InvalidOperationException(InvalidStateErrorMsg);
        }
    }
}