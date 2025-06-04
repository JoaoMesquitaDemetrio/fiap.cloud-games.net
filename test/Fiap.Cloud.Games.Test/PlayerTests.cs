using Fiap.Cloud.Games.Core.Application.DataTransferObjects;
using Fiap.Cloud.Games.Core.Application.Validations;
using FluentValidation.TestHelper;

namespace Fiap.Cloud.Games.Test;

public abstract class PlayerTests
{
    
    public sealed class InsertTests : PlayerTests
    {
        private readonly PlayerInsertValidation _validator;

        public InsertTests()
        {
            _validator = new PlayerInsertValidation();
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var model = new PlayerInsert(Name:"", Email: "", Password: "");
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Exceeds_Max_Length()
        {
            var model = new PlayerInsert(Name: new string('A', 501), Email: "", Password: "");
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Email_Is_Empty()
        {
            var model = new PlayerInsert(Email: "", Name: "", Password: "");
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Have_Error_When_Email_Is_Invalid()
        {
            var model = new PlayerInsert(Email: "invalid-email", Name: "", Password: "");
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Have_Error_When_Email_Exceeds_Max_Length()
        {
            var model = new PlayerInsert(Email: new string('a', 500) + "@test.com", Name: "", Password: "");
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Have_Error_When_Password_Is_Empty()
        {
            var model = new PlayerInsert(Password: "", Name: "", Email: "");
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Should_Have_Error_When_Password_Is_Short()
        {
            var model = new PlayerInsert(Password: "Ab1!", Name: "", Email: "");
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Theory]
        [InlineData("abcdefghi!1")] // Missing uppercase
        [InlineData("ABCDEFGHI!1")] // Missing lowercase
        [InlineData("Abcdefghi!")]  // Missing number
        [InlineData("Abcdefghi1")]  // Missing special character
        public void Should_Have_Error_When_Password_Missing_Requirements(string password)
        {
            var model = new PlayerInsert(Password: password, Name: "", Email: "");
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Should_Have_Error_When_Password_Exceeds_Max_Length()
        {
            var model = new PlayerInsert(Password: new string('A', 501) + "a1!", Name: "", Email: "");
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Should_Not_Have_Error_For_Valid_Model()
        {
            var model = new PlayerInsert(Name: "Valid Name", Email: "test@example.com", Password: "ValidPass1!");
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}