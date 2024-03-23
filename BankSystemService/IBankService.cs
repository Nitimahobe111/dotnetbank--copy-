using System.Text.RegularExpressions;
namespace DotNetBank
{
    public interface IAccountUpdateService
    {
        public ResponseUpdateAccount AccountUpdateServices(UpdateAccountRequest updateAccountRequest);
        public ResponseAccount CreateAccountServices(CreateAccountRequest createAccountRequest);
        public ResponseBranch CreateBranchServices(Branch branch);
        public DeleteAccountResponse DeleteAccountServices(DeleteAccountRequest deleteAccountRequest);
        public EMICalculatorResponse EMICalculatorServices(EMICalculatorRequest eMICalculatorRequest);
        public CreateAccountRequest GetAccountDetailsbyAccountNumber(int accountNumber);
        public CreateBankRequest GetBankbyIFSC(string ifscCode);
        public List<Branch> GetBranch(string bankName);
        public List<Transactions> GetTransactionByMonth();
        public List<Transactions> GetTransactionbyDateService(string From, string To);
        public ResponseTransactions TransactionServices(Transactions transactions);
         public ResponseBank CreateBankServices(CreateBankRequest createBankRequest);

    }
}