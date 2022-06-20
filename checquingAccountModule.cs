namespace Namespace {
    
    using Account = AccountModule.Account;
    
    using InvalidValue = AccountModule.InvalidValue;
    
    using InvalidTransaction = AccountModule.InvalidTransaction;
    
    public static class Module {
        

        public class ChecquingAccount
            : Account {
            
            static ChecquingAccount() {
                @"The amount of overdraft is constant. Defined as a class variable and accessible
                through the name of the class along with the DOT notation";
                @"The maximum interest rate for checquing accounts. Defined as a class variable and accessible
                through the name of the class along with the DOT notation ";
            }
            
            public object OVERDRAFT_LIMIT = 500;
            
            public object MAX_INTEREST_RATE = 1.0;
            
            public ChecquingAccount(object acctNo = -1, object acctHolderName = "")
                : base(acctNo, acctHolderName) {
            }
            
            public virtual object setAnnualIntrRate(object newAnnualIntrRatePercentage) {
                if (newAnnualIntrRatePercentage > ChecquingAccount.MAX_INTEREST_RATE) {
                    throw InvalidValue("A checquing account cannot have an interest rate greater than {0}".format(ChecquingAccount.MAX_INTEREST_RATE));
                }

                Account.setAnnualIntrRate(this, newAnnualIntrRatePercentage);
            }
            
            public virtual object withdraw(object amount) {

                if (amount < 0) {
                    throw InvalidTransaction("Invalid amount provided. Cannot withdraw a negative amount.");
                }

                if (amount > this._balance + ChecquingAccount.OVERDRAFT_LIMIT) {
                    throw InvalidTransaction("Insufficient funds. Cannot withdraw the provided amount.");
                }

                var oldBalance = this._balance;
                this._balance -= amount;

                return this._balance;
            }
        }
    }
}
