namespace Namespace {
    //Finished
    using Account = AccountModule.Account;
    
    using InvalidValue = AccountModule.InvalidValue;
    
    using InvalidTransaction = AccountModule.InvalidTransaction;
    
    using OperationCancel = BankModule.OperationCancel;
    
    using ChecquingAccount = ChecquingAccountModule.ChecquingAccount;
    
    using SavingsAccount = SavingsAccountModule.SavingsAccount;
    
    using System;
    
    public static class Module {
        
        public class Atm {
            
            public Atm(object bank) {
                //the bank this ATM object is working with
                this._bank = bank;
                //create the MAIN MENU options
                this.SELECT_ACCOUNT_OPTION = 1;
                this.CREATE_ACCOUNT_OPTION = 2;
                this.EXIT_ATM_APPLICATION_OPTION = 3;
                //create ACCOUNT MENU option
                this.CHECK_BALANCE_OPTION = 1;
                this.WITHDRAW_OPTION = 2;
                this.DEPOSIT_OPTION = 3;
                this.EXIT_ACCOUNT_OPTION = 4;
            }
            

            public virtual object start() {

                while (true) {

                    var selectedOption = this.showMainMenu();
                    if (selectedOption == this.SELECT_ACCOUNT_OPTION) {
                        var acct = this.selectAccount();
                        if (acct != null) {
                            this.manageAccount(acct);
                        }
                    } else if (selectedOption == this.CREATE_ACCOUNT_OPTION) {
                        this.onCreateAccount();
                    } else if (selectedOption == this.EXIT_ATM_APPLICATION_OPTION) {

                        this._bank.saveAccountData();
                        return;
                    } else {

                        Console.WriteLine("Please enter a valid menu option", "\n");
                    }
                }
            }
            

            public virtual object showMainMenu() {
                while (true) {
                    try {
                        return Convert.ToInt32(input("\nMain Menu\n\n1: Select Account\n2: Create Account\n3: Exit\n\nEnter a choice: "));
                    } catch (ValueError) {

                        Console.WriteLine("Please enter a valid menu option.", "\n");
                    }
                }
            }
            


            public virtual object showAccountMenu() {
                while (true) {
                    try {
                        return Convert.ToInt32(input("\nAccount Menu\n\n1: Check Balance\n2: Withdraw\n3: Deposit\n4: Exit\n\nEnter a choice: "));
                    } catch (ValueError) {

                        Console.WriteLine("Please enter a valid menu option.", "\n");
                    }
                }
            }
            

            public virtual object onCreateAccount() {
                while (true) {
                    try {

                        var clientName = this.promptForClientName();

                        var initDepositAmount = this.promptForDepositAmount();

                        var annIntrRate = this.promptForAnnualIntrRate();

                        var acctType = this.promptForAccountType();

                        var newAccount = this._bank.openAccount(clientName, acctType);

                        newAccount.deposit(initDepositAmount);
                        newAccount.setAnnualIntrRate(annIntrRate);

                        return;
                    } catch (InvalidValue) {
                        Console.WriteLine(err, "\n");
                    } catch (OperationCancel) {
                        Console.WriteLine(err, "\n");
                        return;
                    }
                }
            }
            

            public virtual object selectAccount() {
                while (true) {
                    try {
                        var acctNoInput = input("Please enter your account ID or press [ENTER] to cancel: ");
                
                        if (acctNoInput.Count == 0) {
                            return null;
                        }

                        var acctNo = Convert.ToInt32(acctNoInput);

                        var acct = this._bank.findAccount(acctNo);
                        if (acct != null) {
                            return acct;
                        } else {
                            Console.WriteLine("The account was not found. Please select another account.");
                        }
                    } catch (ValueError) {

                        Console.WriteLine("Please enter a valid account number (e.g. 100)", "\n");
                    }
                }
            }
            

            public virtual object manageAccount(object account) {
                while (true) {
                    var selAcctMenuOpt = this.showAccountMenu();
                    if (selAcctMenuOpt == this.CHECK_BALANCE_OPTION) {
                        this.onCheckBalance(account);
                    } else if (selAcctMenuOpt == this.WITHDRAW_OPTION) {
                        this.onWithdraw(account);
                    } else if (selAcctMenuOpt == this.DEPOSIT_OPTION) {
                        this.onDeposit(account);
                    } else if (selAcctMenuOpt == this.EXIT_ACCOUNT_OPTION) {
                        return;
                    } else {
                        Console.WriteLine("Please enter a valid menu option");
                    }
                }
            }
            

            public virtual object promptForClientName() {
                var clientName = input("Please enter the client name or press [ENTER] to cancel: ");
                if (clientName.Count == 0) {

                    throw OperationCancel("The user has selected to cancel the current operation");
                }
                return clientName;
            }
            

            public virtual object promptForDepositAmount() {
                while (true) {
                    try {
                        var initAmount = (input("Please enter your initial account balance: "));
                        if (initAmount >= 0) {
                            return initAmount;
                        } else {

                            Console.WriteLine("Cannot create an account with a negative initial balance. Please enter a valid amount");
                        }
                    } catch (ValueError) {
                        Console.WriteLine(err, "\n");
                    }
                }
            }
            

            public virtual object promptForAnnualIntrRate() {
                while (true) {
                    try {
                        var intrRate = (input("Please enter the interest rate for this account: "));

                        if (intrRate >= 0) {
                            return intrRate;
                        } else {

                            Console.WriteLine("Cannot create an account with a negative interest rate.");
                        }
                    } catch (ValueError) {
                        Console.WriteLine(err, "\n");
                    }
                }
            }
            
        
            public virtual object promptForAccountType() {
                while (true) {
                    var acctTypeInput = input("Please enter the account type [c/s: chequing / savings]: ").upper();
                    if (acctTypeInput == "C" || acctTypeInput == "CHECQUING" || acctTypeInput == "CHECKING") {
                        return Account.ACCOUNT_TYPE_CHECQUING;
                    } else if (acctTypeInput == "S" || acctTypeInput == "SAVINGS" || acctTypeInput == "SAVING") {
                        return Account.ACCOUNT_TYPE_SAVINGS;
                    } else {
                        Console.WriteLine("Answer not supported. Please enter one of the supported answers.");
                    }
                }
            }
                 
            public virtual object onCheckBalance(object account) {
                Console.WriteLine("The balance is {0}\n".format(account.getBalance()));
            }
            
            public virtual object onDeposit(object account) {
                while (true) {
                    try {
                        var inputAmount = input("Please enter an amount to deposit or type [ENTER] to exit: ");

                        if (inputAmount.Count > 0) {
                            var amount = (inputAmount);

                            account.deposit(amount);
                        }

                        return;
                    } catch (ValueError) {

                        Console.WriteLine("Invalid entry. Please enter a number for your amount.", "\n");
                    } catch (InvalidTransaction) {

                        Console.WriteLine(err, "\n");
                    }
                }
            }
        
            public virtual object onWithdraw(object account) {
                while (true) {
                    try {
                        var inputAmount = input("Please enter an amount to withdraw or type [ENTER] to exit: ");

                        if (inputAmount.Count > 0) {
                            var amount = (inputAmount);

                            account.withdraw(amount);
                        }

                        return;
                    } catch (ValueError) {

                        Console.WriteLine("Invalid entry. Please enter a number for your amount.", "\n");
                    } catch (InvalidTransaction) {

                        Console.WriteLine(err, "\n");
                    }
                }
            }
        }
    }
}
