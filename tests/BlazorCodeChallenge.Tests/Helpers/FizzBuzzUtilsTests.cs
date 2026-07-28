using BlazorCodeChallenge.Helpers;

namespace BlazorCodeChallenge.Tests.Helpers;

public class FizzBuzzUtilsTests
{
    [Fact]
    public void GenerateResults_ReturnsResultForEveryNumber()
    {
        // Act
        var result =
            FizzBuzzUtils.GenerateResults(
                fizzValue: 3,
                buzzValue: 5,
                stopValue: 100);

        // Assert
        Assert.Equal(100, result.Count);
    }

    [Fact]
    public void GenerateResults_ReturnsNumberWhenNotDivisible()
    {
        // Act
        var result =
            FizzBuzzUtils.GenerateResults(
                fizzValue: 3,
                buzzValue: 5,
                stopValue: 5);

        // Assert
        Assert.Equal("1", result[0]);
        Assert.Equal("2", result[1]);
        Assert.Equal("4", result[3]);
    }

    [Fact]
    public void GenerateResults_ReturnsFizzForFizzMultiple()
    {
        // Act
        var result =
            FizzBuzzUtils.GenerateResults(
                fizzValue: 3,
                buzzValue: 5,
                stopValue: 3);

        // Assert
        Assert.Equal("Fizz", result[2]);
    }

    [Fact]
    public void GenerateResults_ReturnsBuzzForBuzzMultiple()
    {
        // Act
        var result =
            FizzBuzzUtils.GenerateResults(
                fizzValue: 3,
                buzzValue: 5,
                stopValue: 5);

        // Assert
        Assert.Equal("Buzz", result[4]);
    }

    [Fact]
    public void GenerateResults_ReturnsFizzBuzzForCommonMultiple()
    {
        // Act
        var result =
            FizzBuzzUtils.GenerateResults(
                fizzValue: 3,
                buzzValue: 5,
                stopValue: 15);

        // Assert
        Assert.Equal("FizzBuzz", result[14]);
    }

    [Fact]
    public void GenerateResults_UsesCustomFizzAndBuzzValues()
    {
        // Act
        var result =
            FizzBuzzUtils.GenerateResults(
                fizzValue: 2,
                buzzValue: 7,
                stopValue: 14);

        // Assert
        Assert.Equal("Fizz", result[1]);
        Assert.Equal("Buzz", result[6]);
        Assert.Equal("FizzBuzz", result[13]);
    }
}
