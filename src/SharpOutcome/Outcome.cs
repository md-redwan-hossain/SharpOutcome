using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SharpOutcome;

/// <summary>
/// Represents an outcome that can either be a good outcome of type <typeparamref name="TGoodOutcome"/> 
/// or a bad outcome of type <typeparamref name="TBadOutcome"/>.
/// </summary>
/// <typeparam name="TGoodOutcome">The type of the good outcome.</typeparam>
/// <typeparam name="TBadOutcome">The type of the bad outcome.</typeparam>
public sealed record Outcome<TGoodOutcome, TBadOutcome>
    where TGoodOutcome : notnull
    where TBadOutcome : notnull
{
    private const byte StateUninitialized = 0;
    private const byte StateGood = 1;
    private const byte StateBad = 2;

    private readonly TGoodOutcome? _goodOutcome;
    private readonly TBadOutcome? _badOutcome;
    private readonly byte _state;

    private Outcome(byte state, TGoodOutcome? goodOutcome, TBadOutcome? badOutcome)
    {
        _state = state;
        _goodOutcome = goodOutcome;
        _badOutcome = badOutcome;
    }

    public static Outcome<TGoodOutcome, TBadOutcome> Good(TGoodOutcome goodOutcome)
    {
        ArgumentNullException.ThrowIfNull(goodOutcome);
        return new Outcome<TGoodOutcome, TBadOutcome>(StateGood, goodOutcome, default);
    }

    public static Outcome<TGoodOutcome, TBadOutcome> Bad(TBadOutcome badOutcome)
    {
        ArgumentNullException.ThrowIfNull(badOutcome);
        return new Outcome<TGoodOutcome, TBadOutcome>(StateBad, default, badOutcome);
    }

    public Outcome(TGoodOutcome goodOutcome)
    {
        ArgumentNullException.ThrowIfNull(goodOutcome);
        _goodOutcome = goodOutcome;
        _badOutcome = default;
        _state = StateGood;
    }

    public Outcome(TBadOutcome badOutcome)
    {
        ArgumentNullException.ThrowIfNull(badOutcome);
        _badOutcome = badOutcome;
        _goodOutcome = default;
        _state = StateBad;
    }

    /// <summary>
    /// Gets a value indicating whether the outcome is a good outcome.
    /// </summary>
    public bool IsGoodOutcome
    {
        get
        {
            AssertInitialized();
            return _state == StateGood;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the outcome is a bad outcome.
    /// </summary>
    public bool IsBadOutcome
    {
        get
        {
            AssertInitialized();
            return _state == StateBad;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the instance has been properly initialized.
    /// </summary>
    public bool IsInitialized => _state != StateUninitialized;

    public static implicit operator Outcome<TGoodOutcome, TBadOutcome>(TGoodOutcome goodOutcome)
    {
        return new Outcome<TGoodOutcome, TBadOutcome>(goodOutcome);
    }

    public static implicit operator Outcome<TGoodOutcome, TBadOutcome>(TBadOutcome badOutcome)
    {
        return new Outcome<TGoodOutcome, TBadOutcome>(badOutcome);
    }

    /// <summary>
    /// Tries to pick the good outcome.
    /// </summary>
    public bool TryPickGoodOutcome([NotNullWhen(true)] out TGoodOutcome? goodOutcome)
    {
        AssertInitialized();

        if (_state == StateGood)
        {
            goodOutcome = GetGoodOutcome();
            return true;
        }

        goodOutcome = default;
        return false;
    }

    /// <summary>
    /// Tries to pick the bad outcome.
    /// </summary>
    public bool TryPickBadOutcome([NotNullWhen(true)] out TBadOutcome? badOutcome)
    {
        AssertInitialized();

        if (_state == StateBad)
        {
            badOutcome = GetBadOutcome();
            return true;
        }

        badOutcome = default;
        return false;
    }

    /// <summary>
    /// Tries to pick the good outcome; if unsuccessful, outputs the bad outcome.
    /// </summary>
    public bool TryPickGoodOutcome(
        [NotNullWhen(true)] out TGoodOutcome? goodOutcome,
        [NotNullWhen(false)] out TBadOutcome? badOutcome)
    {
        AssertInitialized();

        if (_state == StateGood)
        {
            goodOutcome = GetGoodOutcome();
            badOutcome = default;
            return true;
        }

        goodOutcome = default;
        badOutcome = GetBadOutcome();
        return false;
    }

    /// <summary>
    /// Tries to pick the bad outcome; if unsuccessful, outputs the good outcome.
    /// </summary>
    public bool TryPickBadOutcome(
        [NotNullWhen(true)] out TBadOutcome? badOutcome,
        [NotNullWhen(false)] out TGoodOutcome? goodOutcome)
    {
        AssertInitialized();

        if (_state == StateBad)
        {
            badOutcome = GetBadOutcome();
            goodOutcome = default;
            return true;
        }

        badOutcome = default;
        goodOutcome = GetGoodOutcome();
        return false;
    }

    /// <summary>
    /// Deconstructs the outcome into its status and values.
    /// </summary>
    public void Deconstruct(
        out bool isGoodOutcome,
        out TGoodOutcome? goodOutcome,
        out TBadOutcome? badOutcome)
    {
        AssertInitialized();
        isGoodOutcome = _state == StateGood;
        goodOutcome = _goodOutcome;
        badOutcome = _badOutcome;
    }

    public TOutput Match<TOutput>(
        Func<TGoodOutcome, TOutput> onGoodOutcome,
        Func<TBadOutcome, TOutput> onBadOutcome)
    {
        ArgumentNullException.ThrowIfNull(onGoodOutcome);
        ArgumentNullException.ThrowIfNull(onBadOutcome);
        AssertInitialized();

        return _state == StateGood
            ? onGoodOutcome(GetGoodOutcome())
            : onBadOutcome(GetBadOutcome());
    }

    public async Task<TOutput> MatchAsync<TOutput>(
        Func<TGoodOutcome, Task<TOutput>> onGoodOutcome,
        Func<TBadOutcome, Task<TOutput>> onBadOutcome)
    {
        ArgumentNullException.ThrowIfNull(onGoodOutcome);
        ArgumentNullException.ThrowIfNull(onBadOutcome);
        AssertInitialized();

        return _state == StateGood
            ? await onGoodOutcome(GetGoodOutcome()).ConfigureAwait(false)
            : await onBadOutcome(GetBadOutcome()).ConfigureAwait(false);
    }

    public async Task<TOutput> MatchAsync<TOutput>(
        Func<TGoodOutcome, Task<TOutput>> onGoodOutcome,
        Func<TBadOutcome, TOutput> onBadOutcome)
    {
        ArgumentNullException.ThrowIfNull(onGoodOutcome);
        ArgumentNullException.ThrowIfNull(onBadOutcome);
        AssertInitialized();

        return _state == StateGood
            ? await onGoodOutcome(GetGoodOutcome()).ConfigureAwait(false)
            : onBadOutcome(GetBadOutcome());
    }

    public async Task<TOutput> MatchAsync<TOutput>(
        Func<TGoodOutcome, TOutput> onGoodOutcome,
        Func<TBadOutcome, Task<TOutput>> onBadOutcome)
    {
        ArgumentNullException.ThrowIfNull(onGoodOutcome);
        ArgumentNullException.ThrowIfNull(onBadOutcome);
        AssertInitialized();

        return _state == StateGood
            ? onGoodOutcome(GetGoodOutcome())
            : await onBadOutcome(GetBadOutcome()).ConfigureAwait(false);
    }

    public void Switch(
        Action<TGoodOutcome> onGoodOutcome,
        Action<TBadOutcome> onBadOutcome)
    {
        ArgumentNullException.ThrowIfNull(onGoodOutcome);
        ArgumentNullException.ThrowIfNull(onBadOutcome);
        AssertInitialized();

        if (_state == StateGood)
        {
            onGoodOutcome(GetGoodOutcome());
        }
        else
        {
            onBadOutcome(GetBadOutcome());
        }
    }

    public async Task SwitchAsync(
        Func<TGoodOutcome, Task> onGoodOutcome,
        Func<TBadOutcome, Task> onBadOutcome)
    {
        ArgumentNullException.ThrowIfNull(onGoodOutcome);
        ArgumentNullException.ThrowIfNull(onBadOutcome);
        AssertInitialized();

        if (_state == StateGood)
        {
            await onGoodOutcome(GetGoodOutcome()).ConfigureAwait(false);
        }
        else
        {
            await onBadOutcome(GetBadOutcome()).ConfigureAwait(false);
        }
    }

    public async Task SwitchAsync(
        Func<TGoodOutcome, Task> onGoodOutcome,
        Action<TBadOutcome> onBadOutcome)
    {
        ArgumentNullException.ThrowIfNull(onGoodOutcome);
        ArgumentNullException.ThrowIfNull(onBadOutcome);
        AssertInitialized();

        if (_state == StateGood)
        {
            await onGoodOutcome(GetGoodOutcome()).ConfigureAwait(false);
        }
        else
        {
            onBadOutcome(GetBadOutcome());
        }
    }

    public async Task SwitchAsync(
        Action<TGoodOutcome> onGoodOutcome,
        Func<TBadOutcome, Task> onBadOutcome)
    {
        ArgumentNullException.ThrowIfNull(onGoodOutcome);
        ArgumentNullException.ThrowIfNull(onBadOutcome);
        AssertInitialized();

        if (_state == StateGood)
        {
            onGoodOutcome(GetGoodOutcome());
        }
        else
        {
            await onBadOutcome(GetBadOutcome()).ConfigureAwait(false);
        }
    }

    public override string ToString()
    {
        return _state switch
        {
            StateGood => $"Good({_goodOutcome})",
            StateBad => $"Bad({_badOutcome})",
            _ => "Uninitialized"
        };
    }

    private TGoodOutcome GetGoodOutcome()
    {
        if (_goodOutcome is { } good)
        {
            return good;
        }

        throw new InvalidOperationException("Good outcome value was null or uninitialized.");
    }

    private TBadOutcome GetBadOutcome()
    {
        if (_badOutcome is { } bad)
        {
            return bad;
        }

        throw new InvalidOperationException("Bad outcome value was null or uninitialized.");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void AssertInitialized()
    {
        if (_state == StateUninitialized)
        {
            ThrowUninitialized();
        }
    }

    [DoesNotReturn]
    private static void ThrowUninitialized()
    {
        throw new InvalidOperationException("The Outcome instance is uninitialized.");
    }
}