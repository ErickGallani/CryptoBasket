namespace CryptoBasket.Domain.Core.Tests
{
    using CryptoBasket.Domain.Core.ValueObjects;
    using FluentValidation;
    using System.Collections.Generic;
    using Xunit;

    public class ValueObjectsTests
    {
        [Fact]
        public void GivenTwoEqualsObjectsWhenUsingTheEqualOperatorReturnsTrue()
        {
            // arrange
            var firstName = "John";
            var lastName = "Doe";
            var valueObjectTest1 = new ValueObjectTest(firstName, lastName);
            var valueObjectTest2 = new ValueObjectTest(firstName, lastName);

            // act
            var comparisonResult = valueObjectTest1 == valueObjectTest2;

            // assert
            Assert.True(comparisonResult);
        }

        [Fact]
        public void GivenTwoDifferentObjectsWhenUsingTheEqualOperatorReturnsFalse()
        {
            // arrange
            var firstName = "John";
            var lastName = "Doe";
            var valueObjectTest1 = new ValueObjectTest(firstName, lastName);
            var valueObjectTest2 = new ValueObjectTest(firstName, "Jhon");

            // act
            var comparisonResult = valueObjectTest1 == valueObjectTest2;

            // assert
            Assert.False(comparisonResult);
        }

        [Theory]
        [InlineData("John", "Testing")]
        [InlineData("Testing", "Doe")]
        [InlineData("Testing", "Tested")]
        public void GivenTwoDifferentObjectsWhenUsingTheNotEqualOperatorReturnsTrue(string firstName2, string lastName2)
        {
            // arrange
            var firstName1 = "John";
            var lastName1 = "Doe";
            var valueObjectTest1 = new ValueObjectTest(firstName1, lastName1);
            var valueObjectTest2 = new ValueObjectTest(firstName2, lastName2);

            // act
            var comparisonResult = valueObjectTest1 != valueObjectTest2;

            // assert
            Assert.True(comparisonResult);
        }

        private class ValueObjectTest : ValueObject<ValueObjectTest>
        {
            public ValueObjectTest(string firstName, string lastName)
            {
                FirstName = firstName;
                LastName = lastName;
            }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public override IValidator<ValueObjectTest> GetValidator() =>
                null;

            public override IEnumerable<object> GetValues()
            {
                yield return FirstName;
                yield return LastName;
            }
        }
    }
}
