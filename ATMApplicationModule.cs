namespace Namespace {
    //Finished
    using Bank = BankModule.Bank;
    
    using Account = AccountModule.Account;
    
    using Atm = ATMModule.Atm;
    
    public static class Module {
        
        public class ATMApplication {
            
            public virtual object run() {

                try {

                    var bank = Bank();
                    bank.loadAccountData();

                    var atm = Atm(bank);

                    atm.start();
                } catch (Exception) {
                    Console.WriteLine("An error occurred with the following message: ", e);
                }
            }
        }
    }
}
