using Shouldly;

namespace SharpOutcome.Tests;

public class OutcomeTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_WithGoodOutcome_ShouldCreateGoodOutcome()
    {
        // Arrange & Act
        var outcome = new Outcome<string, int>("success");

        // Assert
        outcome.IsGoodOutcome().ShouldBeTrue();
        outcome.IsBadOutcome().ShouldBeFalse();
    }

    [Fact]
    public void Constructor_WithBadOutcome_ShouldCreateBadOutcome()
    {
        // Arrange & Act
        var outcome = new Outcome<string, int>(404);

        // Assert
        outcome.IsGoodOutcome().ShouldBeFalse();
        outcome.IsBadOutcome().ShouldBeTrue();
    }

    #endregion

    #region Implicit Operator Tests

    [Fact]
    public void ImplicitOperator_FromGoodOutcome_ShouldCreateGoodOutcome()
    {
        // Arrange & Act
        Outcome<string, int> outcome = "success";

        // Assert
        outcome.IsGoodOutcome().ShouldBeTrue();
        outcome.IsBadOutcome().ShouldBeFalse();
    }

    [Fact]
    public void ImplicitOperator_FromBadOutcome_ShouldCreateBadOutcome()
    {
        // Arrange & Act
        Outcome<string, int> outcome = 404;

        // Assert
        outcome.IsGoodOutcome().ShouldBeFalse();
        outcome.IsBadOutcome().ShouldBeTrue();
    }

    #endregion

    #region IsGoodOutcome Tests

    [Fact]
    public void IsGoodOutcome_WhenGoodOutcome_ShouldReturnTrue()
    {
        // Arrange
        var outcome = new Outcome<string, int>("success");

        // Act & Assert
        outcome.IsGoodOutcome().ShouldBeTrue();
    }

    [Fact]
    public void IsGoodOutcome_WhenBadOutcome_ShouldReturnFalse()
    {
        // Arrange
        var outcome = new Outcome<string, int>(404);

        // Act & Assert
        outcome.IsGoodOutcome().ShouldBeFalse();
    }

    #endregion

    #region IsBadOutcome Tests

    [Fact]
    public void IsBadOutcome_WhenBadOutcome_ShouldReturnTrue()
    {
        // Arrange
        var outcome = new Outcome<string, int>(404);

        // Act & Assert
        outcome.IsBadOutcome().ShouldBeTrue();
    }

    [Fact]
    public void IsBadOutcome_WhenGoodOutcome_ShouldReturnFalse()
    {
        // Arrange
        var outcome = new Outcome<string, int>("success");

        // Act & Assert
        outcome.IsBadOutcome().ShouldBeFalse();
    }

    #endregion

    #region TryPickGoodOutcome Tests

    [Fact]
    public void TryPickGoodOutcome_WhenGoodOutcome_ShouldReturnTrueAndValue()
    {
        // Arrange
        var outcome = new Outcome<string, int>("success");

        // Act
        var result = outcome.TryPickGoodOutcome(out var goodOutcome);

        // Assert
        result.ShouldBeTrue();
        goodOutcome.ShouldBe("success");
    }

    [Fact]
    public void TryPickGoodOutcome_WhenBadOutcome_ShouldReturnFalseAndDefault()
    {
        // Arrange
        var outcome = new Outcome<string, int>(404);

        // Act
        var result = outcome.TryPickGoodOutcome(out var goodOutcome);

        // Assert
        result.ShouldBeFalse();
        goodOutcome.ShouldBeNull();
    }

    [Fact]
    public void TryPickGoodOutcome_WithBothOutParams_WhenGoodOutcome_ShouldReturnTrueAndGoodValue()
    {
        // Arrange
        var outcome = new Outcome<string, int>("success");

        // Act
        var result = outcome.TryPickGoodOutcome(out var goodOutcome, out var badOutcome);

        // Assert
        result.ShouldBeTrue();
        goodOutcome.ShouldBe("success");
        badOutcome.ShouldBe(0);
    }

    [Fact]
    public void TryPickGoodOutcome_WithBothOutParams_WhenBadOutcome_ShouldReturnFalseAndBadValue()
    {
        // Arrange
        var outcome = new Outcome<string, int>(404);

        // Act
        var result = outcome.TryPickGoodOutcome(out var goodOutcome, out var badOutcome);

        // Assert
        result.ShouldBeFalse();
        goodOutcome.ShouldBeNull();
        badOutcome.ShouldBe(404);
    }

    #endregion

    #region TryPickBadOutcome Tests

    [Fact]
    public void TryPickBadOutcome_WhenBadOutcome_ShouldReturnTrueAndValue()
    {
        // Arrange
        var outcome = new Outcome<string, int>(404);

        // Act
        var result = outcome.TryPickBadOutcome(out var badOutcome);

        // Assert
        result.ShouldBeTrue();
        badOutcome.ShouldBe(404);
    }

    [Fact]
    public void TryPickBadOutcome_WhenGoodOutcome_ShouldReturnFalseAndDefault()
    {
        // Arrange
        var outcome = new Outcome<string, int>("success");

        // Act
        var result = outcome.TryPickBadOutcome(out var badOutcome);

        // Assert
        result.ShouldBeFalse();
        badOutcome.ShouldBe(0);
    }

    [Fact]
    public void TryPickBadOutcome_WithBothOutParams_WhenBadOutcome_ShouldReturnTrueAndBadValue()
    {
        // Arrange
        var outcome = new Outcome<string, int>(404);

        // Act
        var result = outcome.TryPickBadOutcome(out var badOutcome, out var goodOutcome);

        // Assert
        result.ShouldBeTrue();
        badOutcome.ShouldBe(404);
        goodOutcome.ShouldBeNull();
    }

    [Fact]
    public void TryPickBadOutcome_WithBothOutParams_WhenGoodOutcome_ShouldReturnFalseAndGoodValue()
    {
        // Arrange
        var outcome = new Outcome<string, int>("success");

        // Act
        var result = outcome.TryPickBadOutcome(out var badOutcome, out var goodOutcome);

        // Assert
        result.ShouldBeFalse();
        badOutcome.ShouldBe(0);
        goodOutcome.ShouldBe("success");
    }

    #endregion

    #region Match Tests

    [Fact]
    public void Match_WhenGoodOutcome_ShouldExecuteGoodOutcomeDelegate()
    {
        // Arrange
        var outcome = new Outcome<string, int>("success");

        // Act
        var result = outcome.Match(
            onGoodOutcome: good => $"Good: {good}",
            onBadOutcome: bad => $"Bad: {bad}"
        );

        // Assert
        result.ShouldBe("Good: success");
    }

    [Fact]
    public void Match_WhenBadOutcome_ShouldExecuteBadOutcomeDelegate()
    {
        // Arrange
        var outcome = new Outcome<string, int>(404);

        // Act
        var result = outcome.Match(
            onGoodOutcome: good => $"Good: {good}",
            onBadOutcome: bad => $"Bad: {bad}"
        );

        // Assert
        result.ShouldBe("Bad: 404");
    }

    [Fact]
    public void Match_WithComplexTransformation_ShouldWorkCorrectly()
    {
        // Arrange
        var outcome = new Outcome<int, string>(42);

        // Act
        var result = outcome.Match(
            onGoodOutcome: num => num * 2,
            onBadOutcome: err => err.Length
        );

        // Assert
        result.ShouldBe(84);
    }

    #endregion

    #region MatchAsync Tests

    [Fact]
    public async Task MatchAsync_BothAsync_WhenGoodOutcome_ShouldExecuteGoodOutcomeDelegate()
    {
        // Arrange
        var outcome = new Outcome<string, int>("success");

        // Act
        var result = await outcome.MatchAsync(
            onGoodOutcome: async good => await Task.FromResult($"Good: {good}"),
            onBadOutcome: async bad => await Task.FromResult($"Bad: {bad}")
        );

        // Assert
        result.ShouldBe("Good: success");
    }

    [Fact]
    public async Task MatchAsync_BothAsync_WhenBadOutcome_ShouldExecuteBadOutcomeDelegate()
    {
        // Arrange
        var outcome = new Outcome<string, int>(404);

        // Act
        var result = await outcome.MatchAsync(
            onGoodOutcome: async good => await Task.FromResult($"Good: {good}"),
            onBadOutcome: async bad => await Task.FromResult($"Bad: {bad}")
        );

        // Assert
        result.ShouldBe("Bad: 404");
    }

    [Fact]
    public async Task MatchAsync_GoodAsync_BadSync_WhenGoodOutcome_ShouldExecuteGoodOutcomeDelegate()
    {
        // Arrange
        var outcome = new Outcome<string, int>("success");

        // Act
        var result = await outcome.MatchAsync(
            onGoodOutcome: async good => await Task.FromResult($"Good: {good}"),
            onBadOutcome: bad => $"Bad: {bad}"
        );

        // Assert
        result.ShouldBe("Good: success");
    }

    [Fact]
    public async Task MatchAsync_GoodAsync_BadSync_WhenBadOutcome_ShouldExecuteBadOutcomeDelegate()
    {
        // Arrange
        var outcome = new Outcome<string, int>(404);

        // Act
        var result = await outcome.MatchAsync(
            onGoodOutcome: async good => await Task.FromResult($"Good: {good}"),
            onBadOutcome: bad => $"Bad: {bad}"
        );

        // Assert
        result.ShouldBe("Bad: 404");
    }

    [Fact]
    public async Task MatchAsync_GoodSync_BadAsync_WhenGoodOutcome_ShouldExecuteGoodOutcomeDelegate()
    {
        // Arrange
        var outcome = new Outcome<string, int>("success");

        // Act
        var result = await outcome.MatchAsync(
            onGoodOutcome: good => $"Good: {good}",
            onBadOutcome: async bad => await Task.FromResult($"Bad: {bad}")
        );

        // Assert
        result.ShouldBe("Good: success");
    }

    [Fact]
    public async Task MatchAsync_GoodSync_BadAsync_WhenBadOutcome_ShouldExecuteBadOutcomeDelegate()
    {
        // Arrange
        var outcome = new Outcome<string, int>(404);

        // Act
        var result = await outcome.MatchAsync(
            onGoodOutcome: good => $"Good: {good}",
            onBadOutcome: async bad => await Task.FromResult($"Bad: {bad}")
        );

        // Assert
        result.ShouldBe("Bad: 404");
    }

    #endregion

    #region Switch Tests

    [Fact]
    public void Switch_WhenGoodOutcome_ShouldExecuteGoodOutcomeAction()
    {
        // Arrange
        var outcome = new Outcome<string, int>("success");
        var result = "";

        // Act
        outcome.Switch(
            onGoodOutcome: good => result = $"Good: {good}",
            onBadOutcome: bad => result = $"Bad: {bad}"
        );

        // Assert
        result.ShouldBe("Good: success");
    }

    [Fact]
    public void Switch_WhenBadOutcome_ShouldExecuteBadOutcomeAction()
    {
        // Arrange
        var outcome = new Outcome<string, int>(404);
        var result = "";

        // Act
        outcome.Switch(
            onGoodOutcome: good => result = $"Good: {good}",
            onBadOutcome: bad => result = $"Bad: {bad}"
        );

        // Assert
        result.ShouldBe("Bad: 404");
    }

    #endregion

    #region SwitchAsync Tests

    [Fact]
    public async Task SwitchAsync_BothAsync_WhenGoodOutcome_ShouldExecuteGoodOutcomeAction()
    {
        // Arrange
        var outcome = new Outcome<string, int>("success");
        var result = "";

        // Act
        await outcome.SwitchAsync(
            onGoodOutcome: async good =>
            {
                await Task.Delay(1);
                result = $"Good: {good}";
            },
            onBadOutcome: async bad =>
            {
                await Task.Delay(1);
                result = $"Bad: {bad}";
            }
        );

        // Assert
        result.ShouldBe("Good: success");
    }

    [Fact]
    public async Task SwitchAsync_BothAsync_WhenBadOutcome_ShouldExecuteBadOutcomeAction()
    {
        // Arrange
        var outcome = new Outcome<string, int>(404);
        var result = "";

        // Act
        await outcome.SwitchAsync(
            onGoodOutcome: async good =>
            {
                await Task.Delay(1);
                result = $"Good: {good}";
            },
            onBadOutcome: async bad =>
            {
                await Task.Delay(1);
                result = $"Bad: {bad}";
            }
        );

        // Assert
        result.ShouldBe("Bad: 404");
    }

    [Fact]
    public async Task SwitchAsync_GoodAsync_BadSync_WhenGoodOutcome_ShouldExecuteGoodOutcomeAction()
    {
        // Arrange
        var outcome = new Outcome<string, int>("success");
        var result = "";

        // Act
        await outcome.SwitchAsync(
            onGoodOutcome: async good =>
            {
                await Task.Delay(1);
                result = $"Good: {good}";
            },
            onBadOutcome: bad => result = $"Bad: {bad}"
        );

        // Assert
        result.ShouldBe("Good: success");
    }

    [Fact]
    public async Task SwitchAsync_GoodAsync_BadSync_WhenBadOutcome_ShouldExecuteBadOutcomeAction()
    {
        // Arrange
        var outcome = new Outcome<string, int>(404);
        var result = "";

        // Act
        await outcome.SwitchAsync(
            onGoodOutcome: async good =>
            {
                await Task.Delay(1);
                result = $"Good: {good}";
            },
            onBadOutcome: bad => result = $"Bad: {bad}"
        );

        // Assert
        result.ShouldBe("Bad: 404");
    }

    [Fact]
    public async Task SwitchAsync_GoodSync_BadAsync_WhenGoodOutcome_ShouldExecuteGoodOutcomeAction()
    {
        // Arrange
        var outcome = new Outcome<string, int>("success");
        var result = "";

        // Act
        await outcome.SwitchAsync(
            onGoodOutcome: good => result = $"Good: {good}",
            onBadOutcome: async bad =>
            {
                await Task.Delay(1);
                result = $"Bad: {bad}";
            }
        );

        // Assert
        result.ShouldBe("Good: success");
    }

    [Fact]
    public async Task SwitchAsync_GoodSync_BadAsync_WhenBadOutcome_ShouldExecuteBadOutcomeAction()
    {
        // Arrange
        var outcome = new Outcome<string, int>(404);
        var result = "";

        // Act
        await outcome.SwitchAsync(
            onGoodOutcome: good => result = $"Good: {good}",
            onBadOutcome: async bad =>
            {
                await Task.Delay(1);
                result = $"Bad: {bad}";
            }
        );

        // Assert
        result.ShouldBe("Bad: 404");
    }

    #endregion

    #region Complex Type Tests

    [Fact]
    public void Outcome_WithComplexTypes_ShouldWorkCorrectly()
    {
        // Arrange
        var goodData = new List<string> { "item1", "item2" };
        var outcome = new Outcome<List<string>, Exception>(goodData);

        // Act
        var result = outcome.TryPickGoodOutcome(out var data);

        // Assert
        result.ShouldBeTrue();
        data.ShouldBeEquivalentTo(goodData);
    }

    [Fact]
    public void Outcome_WithException_ShouldWorkCorrectly()
    {
        // Arrange
        var exception = new InvalidOperationException("Test error");
        var outcome = new Outcome<string, Exception>(exception);

        // Act
        var result = outcome.TryPickBadOutcome(out var error);

        // Assert
        result.ShouldBeTrue();
        error.ShouldBe(exception);
        error?.Message.ShouldBe("Test error");
    }

    #endregion

    #region Integration Tests

    [Fact]
    public async Task Outcome_AsyncChainedOperations_ShouldWorkCorrectly()
    {
        // Arrange
        var outcome = new Outcome<int, string>(42);

        // Act
        var intermediateResult = await outcome.MatchAsync(
            onGoodOutcome: async num =>
            {
                await Task.Delay(1);
                return num * 2;
            },
            onBadOutcome: _ => 0
        );

        var finalOutcome = new Outcome<int, string>(intermediateResult);

        // Assert
        finalOutcome.IsGoodOutcome().ShouldBeTrue();
        finalOutcome.TryPickGoodOutcome(out var value);
        value.ShouldBe(84);
    }

    #endregion
}