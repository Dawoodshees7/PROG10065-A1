namespace Namespace {
    
    using Account = AccountModule.Account;
    
    using InvalidValue = AccountModule.InvalidValue;
    
    public static class Module {
        

        public class SavingsAccount
            : Account {
            
            static SavingsAccount() {
                @"The matching deposit ratio. For every dollar deposit this account will
    automatically be credited with 0.5 dollars. Defined as a class variable and accessible
    through the name of the class along with the DOT notation";
                @"The minimmum interest rate for savings accounts. Defined as a class variable and accessible
    through the name of the class along with the DOT notation ";
            }
            
            public object MATCHING_DEPOSIT_RATIO = 0.5;
            
            public object MIN_INTEREST_RATE = 3.0;
            
            public SavingsAccount(object acctNo = -1, object acctHolderName = "")
                : base(acctNo, acctHolderName) {
            }
            

            public virtual object setAnnualIntrRate(object newAnnualIntrRatePercentage) {

                if (newAnnualIntrRatePercentage < SavingsAccount.MIN_INTEREST_RATE) {
                    throw InvalidValue("A savings account cannot have an interest rate less than {0}".format(SavingsAccount.MIN_INTEREST_RATE));
                }

                Account.setAnnualIntrRate(this, newAnnualIntrRatePercentage);
            }
            

            public virtual object deposit(object amount) {
                Account.deposit(this, amount + amount * SavingsAccount.MATCHING_DEPOSIT_RATIO);
            }
        }
    }
}
