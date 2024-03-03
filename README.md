### Rationale

`SharpOutcome` offers an implementation of the Result pattern featuring a straightforward API, enabling seamless code flow management without the need for exceptions.

### Installation

To install, run `dotnet add package SharpOutcome` or from [Nuget](https://www.nuget.org/packages/SharpOutcome/)

### Outcome<TGoodOutcome, TBadOutcome>

The `record struct Outcome<TGoodOutcome, TBadOutcome>` represents an outcome that can either be a good outcome of type `TGoodOutcome` or a bad outcome of type `TBadOutcome`.

For `TGoodOutcome` and `TBadOutcome`, you can use any **non-nullable type**. For convenience, `IGoodOutcome` and `IBadOutcome` are provided along with their concrete implementations `GoodOutcome` and `BadOutcome`. **Non-nullable** means you can use `FooBar` but not `FooBar?` or `Nullable<FooBar>`. This is intentional because nullability can be treated as a bad outcome.

### Match and MatchAsync

The `Match` and `MatchAsync` methods execute a function on the good or bad outcome and return the result. If the outcome is bad, the function for the bad outcome is executed; otherwise, the function for the good outcome is executed.

### Switch and SwitchAsync

The `Switch` and `SwitchAsync` methods execute an action on the good or bad outcome. If the outcome is bad, the action for the bad outcome is executed; otherwise, the action for the good outcome is executed. These methods do not return anything.

### TryPickGoodOutcome and TryPickBadOutcome

The `TryPickGoodOutcome` and `TryPickBadOutcome` methods try to extract the good or bad outcome from the `Outcome` respectively. These methods are overloaded.

#### TryPickGoodOutcome(out TGoodOutcome? goodOutcome)

This method tries to extract the good outcome from the `Outcome` instance. If the instance represents a good outcome, it assigns the good outcome to the `goodOutcome` out parameter and returns true. If the instance represents a bad outcome, it assigns the default value to the `goodOutcome` out parameter and returns false.

#### TryPickGoodOutcome(out TGoodOutcome? goodOutcome, out TBadOutcome? badOutcome)

This overload of `TryPickGoodOutcome` tries to extract both the good and bad outcomes from the `Outcome` instance. If the instance represents a good outcome, it assigns the good outcome to the `goodOutcome` out parameter, the default value to the `badOutcome` out parameter, and returns true. If the instance represents a bad outcome, it assigns the default value to the `goodOutcome` out parameter, the bad outcome to the `badOutcome` out parameter, and returns false.

The `TryPickBadOutcome` methods work in a similar way, but they try to extract the bad outcome instead of the good outcome.
