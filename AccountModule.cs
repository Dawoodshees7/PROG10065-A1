
namespace Namespace {
    
    using System;
    //finihsed
    public static class Module {
        

        public class InvalidTransaction
            : Exception {
        }
        

        public class InvalidValue
            : Exception {
        }
        
        public class Account {
            
            static Account() {
                @"constant representing a checquing account type";
                @"constant representing a savings account type";
            }
            
            public object ACCOUNT_TYPE_CHECQUING = 1;
            
            public object ACCOUNT_TYPE_SAVINGS = 2;
            
            public Account(object acctNo = -1, object acctHolderName = "") {
                this._acctNo = acctNo;
                this._acctHolderName = acctHolderName;
                this._balance = 0.0;
                this._annualIntrRate = 0.0;
            }
            

            public virtual object getAccountNumber() {
                return this._acctNo;
            }
            

            public virtual object getAcctHolderName() {
                return this._acctHolderName;
            }
            

            public virtual object getBalance() {
                return this._balance;
            }
            

            public virtual object getAnnualIntrRate() {
                return this._annualIntrRate;
            }
            public virtual object setAnnualIntrRate(object newAnnualIntrRatePercentage) {
                this._annualIntrRate = newAnnualIntrRatePercentage / 100;
            }
            

            public virtual object getMonthlyIntrRate() {
                return this._annualIntrRate / 12;
            }
            

            public virtual object deposit(object amount) {

                if (amount < 0) {
                    throw InvalidTransaction("Invalid amount provided. Cannot deposit a negative amount.");
                }

                var oldBalance = this._balance;
                this._balance += amount;

                return this._balance;
            }
            
            public virtual object withdraw(object amount) {

                if (amount < 0) {
                    throw InvalidTransaction("Invalid amount provided. Cannot withdraw a negative amount.");
                }
                if (amount > this._balance) {
                    throw InvalidTransaction("Insufficient funds. Cannot withdraw the provided amount.");
                }

                var oldBalance = this._balance;
                this._balance -= amount;

                return this._balance;
            }
            

            public virtual object load(object file) {

                this._acctNo = Convert.ToInt32(file.readline().rstrip("\n"));
                this._acctHolderName = file.readline().rstrip("\n");
                this._balance = (file.readline().rstrip("\n"));
                this._annualIntrRate =(file.readline().rstrip("\n"));
            }
            
            public virtual object save(object file) {

                file.write(this._acctNo.ToString() + "\n");
                file.write(this._acctHolderName.ToString() + "\n");
                file.write(this._balance.ToString() + "\n");
                file.write(this._annualIntrRate.ToString() + "\n");
            }
        }
    }
}
