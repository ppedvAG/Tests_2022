using System;
using Xunit;

namespace TDDBank.Tests
{
    public class BankAccountTests
    {
        [Fact]
        public void New_account_should_have_0_as_balance()
        {
            var ba = new BankAccount();

            Assert.Equal(0m, ba.Balance);
        }

        [Fact]
        public void Can_deposit()
        {
            var ba = new BankAccount();

            ba.Deposit(3m);

            Assert.Equal(3m, ba.Balance);
        }

        [Fact]
        public void Deposit_adds_balance()
        {
            var ba = new BankAccount();

            ba.Deposit(3m);
            ba.Deposit(3m);

            Assert.Equal(6m, ba.Balance);
        }

        [Fact]
        public void Deposit_a_negative_amount_throws_ArgumentException()
        {
            var ba = new BankAccount();

            Assert.Throws<ArgumentException>(() => ba.Deposit(-1m));
        }

        [Fact]
        public void Deposit_a_zero_amount_throws_ArgumentException()
        {
            var ba = new BankAccount();

            Assert.Throws<ArgumentException>(() => ba.Deposit(0m));
        }


        [Fact]
        public void Can_withdraw()
        {
            var ba = new BankAccount();
            ba.Deposit(10m);

            ba.Withdraw(3m);

            Assert.Equal(7m, ba.Balance);
        }

        [Fact]
        public void Withdraw_a_negative_amount_throws_ArgumentException()
        {
            var ba = new BankAccount();

            Assert.Throws<ArgumentException>(() => ba.Withdraw(-1m));
        }

        [Fact]
        public void Withdraw_a_zero_amount_throws_ArgumentException()
        {
            var ba = new BankAccount();

            Assert.Throws<ArgumentException>(() => ba.Withdraw(0m));
        }

        [Fact]
        public void Withdraw_below_zero_throws_InvaildOperationException()
        {
            var ba = new BankAccount();
            ba.Deposit(100m);

            Assert.Throws<InvalidOperationException>(() => ba.Withdraw(101m));
        }

        [Fact]
        public void Withdraw_balance_to_zero()
        {
            var ba = new BankAccount();
            ba.Deposit(100m);

            ba.Withdraw(100m);

            Assert.Equal(0m, ba.Balance);
        }


    }
}