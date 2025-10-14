using FluentAssertions;

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
        outcome.IsGoodOutcome().Should().BeTrue();
        outcome.IsBadOutcome().Should().BeFalse();
    }

    [Fact]
    public void Constructor_WithBadOutcome_ShouldCreateBadOutcome()
    {
        // Arrange & Act
        var outcome = new Outcome<string, int>(404);

        // Assert
        outcome.IsGoodOutcome().Should().BeFalse();
        outcome.IsBadOutcome().Should().BeTrue();
    }

    #endregion

    #region Implicit Operator Tests

    [Fact]
    public void ImplicitOperator_FromGoodOutcome_ShouldCreateGoodOutcome()
    {
        // Arrange & Act
        Outcome<string, int> outcome = "success";

        // Assert
        outcome.IsGoodOutcome().Should().BeTrue();
        outcome.IsBadOutcome().Should().BeFalse();
    }

    [Fact]
    public void ImplicitOperator_FromBadOutcome_ShouldCreateBadOutcome()
    {
        // Arrange & Act
        Outcome<string, int> outcome = 404;

        // Assert
        outcome.IsGoodOutcome().Should().BeFalse();
        outcome.IsBadOutcome().Should().BeTrue();
    }

    #endregion

    #region IsGoodOutcome Tests

    [Fact]
    public void IsGoodOutcome_WhenGoodOutcome_ShouldReturnTrue()
    {
        // Arrange
        var outcome = new Outcome<string, int>("success");

        // Act & Assert
        outcome.IsGoodOutcome().Should().BeTrue();
    }

    [Fact]
    public void IsGoodOutcome_WhenBadOutcome_ShouldReturnFalse()
    {
        // Arrange
        var outcome = new Outcome<string, int>(404);

        // Act & Assert
        outcome.IsGoodOutcome().Should().BeFalse();
    }

    #endregion

    #region IsBadOutcome Tests

    [Fact]
    public void IsBadOutcome_WhenBadOutcome_ShouldReturnTrue()
    {
        // Arrange
        var outcome = new Outcome<string, int>(404);

        // Act & Assert
        outcome.IsBadOutcome().Should().BeTrue();
    }

    [Fact]
    public void IsBadOutcome_WhenGoodOutcome_ShouldReturnFalse()
    {
        // Arrange
        var outcome = new Outcome<string, int>("success");

        // Act & Assert
        outcome.IsBadOutcome().Should().BeFalse();
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
        result.Should().BeTrue();
        goodOutcome.Should().Be("success");
    }

    [Fact]
    public void TryPickGoodOutcome_WhenBadOutcome_ShouldReturnFalseAndDefault()
    {
        // Arrange
        var outcome = new Outcome<string, int>(404);

        // Act
        var result = outcome.TryPickGoodOutcome(out var goodOutcome);

        // Assert
        result.Should().BeFalse();
        goodOutcome.Should().BeNull();
    }

    [Fact]
    public void TryPickGoodOutcome_WithBothOutParams_WhenGoodOutcome_ShouldReturnTrueAndGoodValue()
    {
        // Arrange
        var outcome = new Outcome<string, int>("success");

        // Act
        var result = outcome.TryPickGoodOutcome(out var goodOutcome, out var badOutcome);

        // Assert
        result.Should().BeTrue();
        goodOutcome.Should().Be("success");
        badOutcome.Should().Be(0);
    }

    [Fact]
    public void TryPickGoodOutcome_WithBothOutParams_WhenBadOutcome_ShouldReturnFalseAndBadValue()
    {
        // Arrange
        var outcome = new Outcome<string, int>(404);

        // Act
        var result = outcome.TryPickGoodOutcome(out var goodOutcome, out var badOutcome);

        // Assert
        result.Should().BeFalse();
        goodOutcome.Should().BeNull();
        badOutcome.Should().Be(404);
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
        result.Should().BeTrue();
        badOutcome.Should().Be(404);
    }

    [Fact]
    public void TryPickBadOutcome_WhenGoodOutcome_ShouldReturnFalseAndDefault()
    {
        // Arrange
        var outcome = new Outcome<string, int>("success");

        // Act
        var result = outcome.TryPickBadOutcome(out var badOutcome);

        // Assert
        result.Should().BeFalse();
        badOutcome.Should().Be(0);
    }

    [Fact]
    public void TryPickBadOutcome_WithBothOutParams_WhenBadOutcome_ShouldReturnTrueAndBadValue()
    {
        // Arrange
        var outcome = new Outcome<string, int>(404);

        // Act
        var result = outcome.TryPickBadOutcome(out var badOutcome, out var goodOutcome);

        // Assert
        result.Should().BeTrue();
        badOutcome.Should().Be(404);
        goodOutcome.Should().BeNull();
    }

    [Fact]
    public void TryPickBadOutcome_WithBothOutParams_WhenGoodOutcome_ShouldReturnFalseAndGoodValue()
    {
        // Arrange
        var outcome = new Outcome<string, int>("success");

        // Act
        var result = outcome.TryPickBadOutcome(out var badOutcome, out var goodOutcome);

        // Assert
        result.Should().BeFalse();
        badOutcome.Should().Be(0);
        goodOutcome.Should().Be("success");
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
        result.Should().Be("Good: success");
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
        result.Should().Be("Bad: 404");
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
        result.Should().Be(84);
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
        result.Should().Be("Good: success");
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
        result.Should().Be("Bad: 404");
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
        result.Should().Be("Good: success");
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
        result.Should().Be("Bad: 404");
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
        result.Should().Be("Good: success");
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
        result.Should().Be("Bad: 404");
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
        result.Should().Be("Good: success");
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
        result.Should().Be("Bad: 404");
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
        result.Should().Be("Good: success");
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
        result.Should().Be("Bad: 404");
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
        result.Should().Be("Good: success");
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
        result.Should().Be("Bad: 404");
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
        result.Should().Be("Good: success");
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
        result.Should().Be("Bad: 404");
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
        result.Should().BeTrue();
        data.Should().BeEquivalentTo(goodData);
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
        result.Should().BeTrue();
        error.Should().Be(exception);
        error?.Message.Should().Be("Test error");
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
        finalOutcome.IsGoodOutcome().Should().BeTrue();
        finalOutcome.TryPickGoodOutcome(out var value);
        value.Should().Be(84);
    }

    #endregion
}