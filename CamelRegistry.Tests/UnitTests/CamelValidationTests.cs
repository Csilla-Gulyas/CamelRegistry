using CamelRegistry.DTOs.Requests;
using CamelRegistry.Validators;
using FluentAssertions;
using Xunit;

namespace CamelRegistry.Tests.UnitTests
{
    public class CamelDtoValidationTests
    {
        private readonly CreateCamelValidator _createValidator = new CreateCamelValidator();
        private readonly UpdateCamelValidator _updateValidator = new UpdateCamelValidator();

        [Fact]
        public void CreateCamel_HumpCount_Should_Only_Allow_1_or_2()
        {
            var validCamel1 = new CreateCamelDto { Name = "Alex", Color = "Brown", HumpCount = 1 };
            var validCamel2 = new CreateCamelDto { Name = "Bob", Color = "White", HumpCount = 2 };
            var invalidCamelLow = new CreateCamelDto { Name = "Low", Color = "Gray", HumpCount = 0 };
            var invalidCamelHigh = new CreateCamelDto { Name = "High", Color = "Black", HumpCount = 3 };

            _createValidator.Validate(validCamel1).IsValid.Should().BeTrue();
            _createValidator.Validate(validCamel2).IsValid.Should().BeTrue();

            _createValidator.Validate(invalidCamelLow).IsValid.Should().BeFalse();
            _createValidator.Validate(invalidCamelHigh).IsValid.Should().BeFalse();
        }

        [Fact]
        public void UpdateCamel_HumpCount_Should_Only_Allow_1_or_2_When_Set()
        {
            var validUpdate = new UpdateCamelDto { HumpCount = 1 };
            var invalidUpdateLow = new UpdateCamelDto { HumpCount = 0 };
            var invalidUpdateHigh = new UpdateCamelDto { HumpCount = 3 };

            _updateValidator.Validate(validUpdate).IsValid.Should().BeTrue();
            _updateValidator.Validate(invalidUpdateLow).IsValid.Should().BeFalse();
            _updateValidator.Validate(invalidUpdateHigh).IsValid.Should().BeFalse();
        }
    }
}


