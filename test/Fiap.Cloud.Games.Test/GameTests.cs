using Fiap.Cloud.Games.Core.Application.DataTransferObjects;
using Fiap.Cloud.Games.Core.Application.Validations;
using FluentValidation.TestHelper;

namespace Fiap.Cloud.Games.Test;

public abstract class GameTests
{
    public sealed class GameInsertValidationTests : GameTests
    {
        private readonly GameInsertValidation _validator;

        public GameInsertValidationTests()
        {
            _validator = new GameInsertValidation();
        }

        // Name Tests
        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var model = new GameInsert(Name: "", Studio: "", Price: 0, AgeRating: 0);
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Exceeds_Max_Length()
        {
            var model = new GameInsert(Name: new string('A', 501), Studio: "", Price: 0, AgeRating: 0);
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        // Studio Tests
        [Fact]
        public void Should_Have_Error_When_Studio_Is_Empty()
        {
            var model = new GameInsert(Studio: "", Name: "", Price: 0, AgeRating: 0);
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Studio);
        }

        [Fact]
        public void Should_Have_Error_When_Studio_Exceeds_Max_Length()
        {
            var model = new GameInsert(Studio: new string('B', 501), Name: "", Price: 0, AgeRating: 0);
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Studio);
        }

        // Price Tests
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Should_Have_Error_When_Price_Is_Zero_Or_Negative(decimal price)
        {
            var model = new GameInsert(Price: price, Name: "", Studio: "", AgeRating: 0);
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        // Valid Model
        [Fact]
        public void Should_Not_Have_Error_For_Valid_Model()
        {
            var model = new GameInsert(Name: "Valid Game", Studio: "Valid Studio", Price: 59.99m, AgeRating: 18);
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }

    public sealed class GameUpdateValidationTests : GameTests
    {
        private readonly GameUpdateValidation _validator;

        public GameUpdateValidationTests()
        {
            _validator = new GameUpdateValidation();
        }

        // Id Tests
        [Fact]
        public void Should_Have_Error_When_Id_Is_NullGuid()
        {
            var model = new GameUpdate(Id: Guid.Empty, Name: "", Studio: "", Price: 0);
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage("Jogo inválido.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Id_Is_Valid()
        {
            var model = new GameUpdate(Id: Guid.NewGuid(), Name: "", Studio: "", Price: 0);
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Id);
        }

        // Name Tests
        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var model = new GameUpdate(Name: "", Studio: "", Price: 0, Id: Guid.NewGuid());
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Exceeds_Max_Length()
        {
            var model = new GameUpdate(Name: new string('A', 501), Studio: "", Price: 0, Id: Guid.NewGuid());
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        // Studio Tests
        [Fact]
        public void Should_Have_Error_When_Studio_Is_Empty()
        {
            var model = new GameUpdate(Studio: "", Name: "", Price: 0, Id: Guid.NewGuid());
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Studio);
        }

        [Fact]
        public void Should_Have_Error_When_Studio_Exceeds_Max_Length()
        {
            var model = new GameUpdate(Studio: new string('B', 501), Name: "", Price: 0, Id: Guid.NewGuid());
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Studio);
        }

        // Price Tests
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Should_Have_Error_When_Price_Is_Zero_Or_Negative(decimal price)
        {
            var model = new GameUpdate(Price: price, Name: "", Studio: "", Id: Guid.NewGuid());
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        // Valid Model Test
        [Fact]
        public void Should_Not_Have_Error_For_Valid_Model()
        {
            var model = new GameUpdate(Id: Guid.NewGuid(), Name: "Valid Game", Studio: "Valid Studio", Price: 99.99m);
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}


