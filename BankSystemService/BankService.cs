using System.Text.RegularExpressions;
namespace DotNetBank
{
    public class AccountUpdateService :IAccountUpdateService
    {
        private readonly IAccountDatabase _accountDatabase;
        private readonly IBankDatabase _bankDatabase;
        private readonly IBranchDatabase _branchDatabase;
        private readonly ITranscationDatabase _transcationDatabase;
        private readonly IValidations _validations;


        public AccountUpdateService(IAccountDatabase accountDatabase,
                                     IBankDatabase bankDatabase,
                                     IBranchDatabase branchDatabase,
                                     ITranscationDatabase transcationDatabase,
                                     IValidations validations)
        {
            _accountDatabase=accountDatabase;
            _bankDatabase= bankDatabase;
            _branchDatabase=branchDatabase;
            _transcationDatabase=transcationDatabase;
            _validations=validations;
        }

        public ResponseUpdateAccount AccountUpdateServices(UpdateAccountRequest updateAccountRequest)
        {
            //Valid Email validate
            _validations.EmailCheck(updateAccountRequest.Email);

            //Valid Address Check
            _validations.AddressCheck(updateAccountRequest.AccountAddress);

            //Valid Contact Number
            _validations.ContactNumberCheck(updateAccountRequest.ContactNumber);

            //IFSC check
            _validations.CheckIFSC(updateAccountRequest.IFSC);

            ResponseUpdateAccount result = _accountDatabase.UpdateAccount(updateAccountRequest);
            return result;
        }
       
      
        public ResponseAccount CreateAccountServices(CreateAccountRequest createAccountRequest)
        {
            //Removing White Spaces
            createAccountRequest.FirstName = createAccountRequest.FirstName.Trim();
            createAccountRequest.LastName = createAccountRequest.LastName.Trim();
            createAccountRequest.Email = createAccountRequest.Email.Trim();
            createAccountRequest.PAN = createAccountRequest.PAN.Trim();
            createAccountRequest.Gender = createAccountRequest.Gender.Trim();
            createAccountRequest.DOB = createAccountRequest.DOB.Trim();
            createAccountRequest.IFSC = createAccountRequest.IFSC.Trim();
            createAccountRequest.AccountType = createAccountRequest.AccountType.Trim();
            createAccountRequest.AccountAddress.Line1 = createAccountRequest.AccountAddress.Line1.Trim();
            createAccountRequest.AccountAddress.Line2 = createAccountRequest.AccountAddress.Line2.Trim();
            createAccountRequest.AccountAddress.City = createAccountRequest.AccountAddress.City.Trim();
            createAccountRequest.AccountAddress.State = createAccountRequest.AccountAddress.State.Trim();
            createAccountRequest.AccountAddress.Country = createAccountRequest.AccountAddress.Country.Trim();

            //Email verification
            _validations.EmailCheck(createAccountRequest.Email);

            //ContactNumber check
            _validations.ContactNumberCheck(createAccountRequest.ContactNumber);

            //Address  check
            _validations.AddressCheck(createAccountRequest.AccountAddress);

            //Name check
            _validations.NameCheck(createAccountRequest.FirstName);
            _validations.NameCheck(createAccountRequest.LastName);

            // validpancheck
            _validations.PANCheck(createAccountRequest.PAN, createAccountRequest.FirstName, createAccountRequest.LastName);

            //DOB check 
            int age = _validations.DOBCheck(createAccountRequest.DOB);
            createAccountRequest.Age = age;

            //IFSC check
            _validations.CheckIFSC(createAccountRequest.IFSC);

            //Validating Account Type  using Enums
            if (!(Enum.IsDefined(typeof(AccountType), createAccountRequest.AccountType))) throw new ArgumentException($" Account Type  is Not Valid. Savings, Current and Salary are the only bank type options");

            //Validating Gender  using Enums
            if (!(Enum.IsDefined(typeof(Gender), createAccountRequest.Gender))) throw new ArgumentException($"Gender field is not valid");

            ResponseAccount responseAccount = _accountDatabase.CreateAccount(createAccountRequest);

            return responseAccount;
        }
        public ResponseBank CreateBankServices(CreateBankRequest createBankRequest)
        {
            //Removing White Spaces
            createBankRequest.BankName = createBankRequest.BankName.Trim();
            createBankRequest.Email = createBankRequest.Email.Trim();
            createBankRequest.BankAddress.Line1 = createBankRequest.BankAddress.Line1.Trim();
            createBankRequest.BankAddress.Line2 = createBankRequest.BankAddress.Line2.Trim();
            createBankRequest.BankAddress.City = createBankRequest.BankAddress.City.Trim();
            createBankRequest.BankAddress.State = createBankRequest.BankAddress.State.Trim();
            createBankRequest.BankAddress.Country = createBankRequest.BankAddress.Country.Trim();

            //EMail check
            _validations.EmailCheck(createBankRequest.Email);

            //BankFund check
            if (createBankRequest.BankFund > 300000000000000 || createBankRequest.BankFund < 100000)
                throw new ArgumentException($" Bank Funds Cannot be more than 300 Trillion Rupees and Cannot be smaller than 1 lakh Rupees");

            //ContactNumber check
            _validations.ContactNumberCheck(createBankRequest.ContactNumber);

            //Address  check
            _validations.AddressCheck(createBankRequest.BankAddress);

            ResponseBank responseBank = _bankDatabase.CreateBank(createBankRequest);
            return responseBank;
        }

        public ResponseBranch CreateBranchServices(Branch branch)
        {
            Validations ValidateCreateBranchRequest = new Validations();

            //Removing White Spaces
            branch.BankName = branch.BankName.Trim();
            branch.Email = branch.Email.Trim();
            branch.BranchName = branch.BranchName.Trim();

            //Email check
            ValidateCreateBranchRequest.EmailCheck(branch.Email);

            //Address  check
            ValidateCreateBranchRequest.AddressCheck(branch.BranchAddress);

            //Name check
            ValidateCreateBranchRequest.NameCheck(branch.BankName);

            //ContactNumber check
            ValidateCreateBranchRequest.ContactNumberCheck(branch.ContactNumber);

            ResponseBranch responseBranch = _branchDatabase.CreateBranch(branch);
            return responseBranch;
        }

        public DeleteAccountResponse DeleteAccountServices(DeleteAccountRequest deleteAccountRequest)
        {

            deleteAccountRequest.PAN = deleteAccountRequest.PAN.Trim();
            if (deleteAccountRequest.PAN == "") throw new ArgumentException($"PAN fiels is Empty");

            DeleteAccountResponse deleteAccountResponse = _accountDatabase.DeleteAccount(deleteAccountRequest);

            return deleteAccountResponse;
        }
        public EMICalculatorResponse EMICalculatorServices(EMICalculatorRequest eMICalculatorRequest)
        {

            if (eMICalculatorRequest.Amount < 1000 || eMICalculatorRequest.Amount > 1000000000) throw new ArgumentException($"Loan Amount Cannot be more than 100 crore and cannot be less than 1000 rupees");
            if (eMICalculatorRequest.DurationInMonth < 2 || eMICalculatorRequest.DurationInMonth > 100) throw new ArgumentException($"Duration ranges from 1 month to 100 months");
            if (eMICalculatorRequest.MonthlyInterestRate < 0 || eMICalculatorRequest.MonthlyInterestRate > 100000) throw new ArgumentException($"Valid Interest Rate ranges from 0  to 100000 percent");

            EMICalculatorResponse eMICalculatorResponse = new EMICalculatorResponse();
            eMICalculatorResponse.MonthlyInterestRate = eMICalculatorRequest.MonthlyInterestRate;
            eMICalculatorResponse.TotalAmountToBePaid = Math.Round(eMICalculatorRequest.Amount + (((eMICalculatorRequest.Amount * eMICalculatorRequest.MonthlyInterestRate) / 100) * eMICalculatorRequest.DurationInMonth));
            eMICalculatorResponse.EMI = Math.Round(eMICalculatorResponse.TotalAmountToBePaid / eMICalculatorRequest.DurationInMonth);
            eMICalculatorResponse.Amount = eMICalculatorRequest.Amount;
            eMICalculatorResponse.DurationInMonth = eMICalculatorRequest.DurationInMonth;
            eMICalculatorResponse.Message = "EMI Calculator";

            return eMICalculatorResponse;
        }
        public CreateAccountRequest GetAccountDetailsbyAccountNumber(int accountNumber)
        {
            CreateAccountRequest createAccountRequest = _accountDatabase.GetAccountbyAccountNumber(accountNumber);
            return createAccountRequest;
        }

        public CreateBankRequest GetBankbyIFSC(string ifscCode)
        {
            //Null value check
            if (ifscCode == null) throw new ArgumentException($"Please peovide IFSC code of Bank");

            CreateBankRequest createBankRequest = _bankDatabase.GetBankbyIFSC(ifscCode);
            return createBankRequest;
        }

        public List<Branch> GetBranch(string bankName)
        {
            //Null value check
            if (bankName == null) throw new ArgumentException($"Please peovide Name of the Bank");

            List<Branch> result = _branchDatabase.GetBranch(bankName);
            return result;
        }
        public List<Transactions> GetTransactionByMonth()
        {
            List<Transactions> transactionList = _transcationDatabase.GetTransactionbyMonth();
            return transactionList;
        }
        public List<Transactions> GetTransactionbyDateService(string From, string To)
        {
            //Null value check
            if (From == null || To == null) throw new ArgumentException($"Please peovide IFSC code of Bank");
            TranscationDatabase transactionService = new TranscationDatabase();
            List<Transactions> result = _transcationDatabase.GetTransactionbyDate(From, To);
            return result;
        }
        public ResponseTransactions TransactionServices(Transactions transactions)
        {
            ResponseTransactions result = _transcationDatabase.Transaction(transactions);
            //White space check
            transactions.TransactionType = transactions.TransactionType.Trim();

            //Validating Transaction Type
            if (!(Enum.IsDefined(typeof(TransactionType), transactions.TransactionType))) throw new ArgumentException($"Transaction Type is not valid , try Deposit or Withdrawal");

            //Transaction Amount check
            if (transactions.TransactionAmount > 10000000000)
                throw new ArgumentException($" Transaction Amount cannot be more than 1000 crore");

            if (transactions.TransactionAmount <= 100)
                throw new ArgumentException($"Deposit amount is not valid ");

            return result;
        }
    }

}