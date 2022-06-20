// Defines the Bank class and is used by the BankingApplication module
namespace Namespace {
    
    using os;
    
    using Account = AccountModule.Account;
    
    using InvalidValue = AccountModule.InvalidValue;
    
    using ChecquingAccount = ChecquingAccountModule.ChecquingAccount;
    
    using SavingsAccount = SavingsAccountModule.SavingsAccount;
    
    using System.Collections.Generic;
    
    using System;
    
    using System.Linq;
    
    public static class Module {
        

        public class OperationCancel
            : Exception {
        }
        
        public class Bank {
            
            public Bank() {
                this._accountList = new List<object>();
                this.DEFAULT_ACCT_NO_START = 100;
            }
            

            public virtual object loadAccountData() {
                var dataDirectory = os.path.join(os.getcwd(), "BankingData");
                if (os.path.exists(dataDirectory)) {

                    var acctFileList = os.listdir(dataDirectory);

                    foreach (var acctFileName in acctFileList) {
                        var acctFile = open(os.path.join(dataDirectory, acctFileName));
                        try {

                            var acctType = acctFile.readline().rstrip("\n");
                            if (acctType == "Account") {
                                var acct = Account();
                            } else if (acctType == "ChecquingAccount") {
                                acct = ChecquingAccount();
                            } else if (acctType == "SavingsAccount") {
                                acct = SavingsAccount();
                            }

                            acct.load(acctFile);

                            this._accountList.append(acct);
                        } finally {

                            acctFile.close();
                        }
                    }
                }

                if (this._accountList.Count == 0) {
                    this.createDefaultAccounts();
                }
            }
            

            public virtual object saveAccountData() {
                var dataDirectory = os.path.join(os.getcwd(), "BankingData");

                if (!os.path.exists(dataDirectory)) {
                    os.mkdir(dataDirectory);
                }

                foreach (var acct in this._accountList) {
                    var acctType = type(acct).@__name__;
                    var prefix = acctType == "Account" ? "acct" : acctType == "ChecquingAccount" ? "chqacct" : "savacct";
                    var acctFileName = "{0}{1}.dat".format(prefix, acct.getAccountNumber());

                    using (var acctFile = open(os.path.join(dataDirectory, acctFileName), "w")) {
                        acctFile.write(acctType + "\n");
                        acct.save(acctFile);
                    }
                }
            }
            

            public virtual object createDefaultAccounts() {
                foreach (var iAccount in Enumerable.Range(0, 10)) {

                    var newDefAcct = Account(this.DEFAULT_ACCT_NO_START + iAccount, "DefaultAccount{0}".format(iAccount));
                    newDefAcct.deposit(100);
                    newDefAcct.setAnnualIntrRate(2.5);

                    this._accountList.append(newDefAcct);
                }
            }
            

            public virtual object findAccount(object acctNo) {

                foreach (var acct in this._accountList) {
                    if (acct.getAccountNumber() == acctNo) {
                        return acct;
                    }
                }

                return null;
            }
            
            public virtual object determineAccountNumber() {

                while (true) {
                    try {
                        //ask the user for input
                        var acctNoInput = input("Please enter the account number [100 - 1000] or press [ENTER] to cancel: ");
                        if (acctNoInput.Count == 0) {
                            throw OperationCancel("User has selected to terminate the program after invalid input");
                        }
                        //check the input to ensure correctness and deal with incorrect input
                        var acctNo = Convert.ToInt32(acctNoInput);
                        if (acctNo < 100 || acctNo > 1000) {
                            throw InvalidValue("The account number you have entered is not valid. Please enter a valid account number");
                        }
                        //check that the account number is not in use
                        foreach (var account in this._accountList) {
                            if (acctNo == account.getAccountNumber()) {
                                throw InvalidValue("The account number you have entered already exists. Please enter a different account number");
                            }
                        }
                        //the account number has been generated successfully
                        return acctNo;
                    } catch {
                        Console.WriteLine(err, "\n");
                    }
                }
            }
            

            public virtual object openAccount(object clientName, object acctType) {
                object newAccount;
                //prompt the user for an account number
                var acctNo = this.determineAccountNumber();
                //create and store an account object with the required attributes
                if (acctType == Account.ACCOUNT_TYPE_CHECQUING) {
                    newAccount = ChecquingAccount(acctNo, clientName);
                } else if (acctType == Account.ACCOUNT_TYPE_SAVINGS) {
                    newAccount = SavingsAccount(acctNo, clientName);
                }
                //add the new account to the list of the accounts
                this._accountList.append(newAccount);
                //return the account to the caller so other properties can be set
                return newAccount;
            }
        }
    }
}
