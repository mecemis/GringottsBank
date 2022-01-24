using System;
using System.Threading.Tasks;
using BankAccount.Application.Common.Interfaces;
using BankAccount.Application.Features.BankAccounts.Commands.CreateBankAccount;
using GringottsBank.Shared.Models.Responses;
using Moq;
using Xunit;

namespace BankAccount.ApplicationTests.Features.BankAccounts
{
    public class CreateBankAccountCommandTests : IClassFixture<TestingFixture>
    {
        private readonly TestingFixture _testingFixture;

        public CreateBankAccountCommandTests(TestingFixture testingFixture)
        {
            _testingFixture = testingFixture;
        }

        [Fact]
        public async Task Should_Add_New_Bank_Account()
        {
            CreateBankAccountCommand command = new CreateBankAccountCommand();

            var result = await _testingFixture.SendAsync(command);

            Assert.True(result.IsSuccessful);
            Assert.Equal(200, result.StatusCode);
        }
    }
}