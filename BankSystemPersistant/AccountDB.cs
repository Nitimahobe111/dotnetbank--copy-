using MongoDB.Driver;
namespace DotNetBank
{
    public class AccountDatabase : IAccountDatabase 
    {
      
        MongoClient dbClient = new MongoClient("mongodb://localhost:27017");
        public ResponseAccount CreateAccount(CreateAccountRequest createAccountRequest)
        {
            var mongodb = dbClient.GetDatabase("dotnetbanking");
            var account = mongodb.GetCollection<CreateAccountRequest>("accountdetails");

            //Initializing Response Values
            ResponseAccount responseAccount = new ResponseAccount
            {
                FirstName = createAccountRequest.FirstName,
                LastName = createAccountRequest.LastName,
                PAN = createAccountRequest.PAN,
                ContactNumber = createAccountRequest.ContactNumber,
                DOB = createAccountRequest.DOB,
                Email = createAccountRequest.Email,
                Message = "Successfully Account Created",
                IFSC = createAccountRequest.IFSC,

            };

            //Generating Account Number
            responseAccount.AccountNumber = CreateAccountNumber();
            createAccountRequest.AccountNumber = responseAccount.AccountNumber;


            IMongoCollection<Branch> collection = mongodb.GetCollection<Branch>("branchdetails");
            IMongoCollection<CreateAccountRequest> AccountCollection = mongodb.GetCollection<CreateAccountRequest>("accountdetails");

            //Getting IFSC code of Main Branch
            var GetBranch = collection.Find(acc => acc.IFSC == createAccountRequest.IFSC).FirstOrDefault();
            createAccountRequest.MainBranchIFSC = GetBranch.MainBranchIFSC;
            createAccountRequest.AccountStatus = "Activated";


            //Existing Account Check
            var GetPan = AccountCollection.Find(acc => acc.PAN == createAccountRequest.PAN).FirstOrDefault();
            if (GetPan != null) throw new ArgumentException($"Account Already Exist");

            //Existing Branch Check
            var GetIfsc = collection.Find(acc => acc.IFSC == createAccountRequest.IFSC).FirstOrDefault();
            if (GetIfsc == null) throw new ArgumentException($"Branch with this IFSC code " + createAccountRequest.IFSC + " does not exist , Please check IFSC ");


            // Duplicate Email check
            var GetEmail = AccountCollection.Find(acc => acc.Email == createAccountRequest.Email).FirstOrDefault();
            if (GetEmail != null) throw new ArgumentException($"Email already in use , Try differnt Email");

            // Duplicate Contact Number Check
            var GetContactNumber = AccountCollection.Find(acc => acc.ContactNumber == createAccountRequest.ContactNumber).FirstOrDefault();
            if (GetContactNumber != null) throw new ArgumentException($"Contact Number already in use , Try differnt Contact Number");

            account.InsertOne(createAccountRequest);
            return responseAccount;


        }
        public ResponseUpdateAccount UpdateAccount(UpdateAccountRequest updateAccountRequest)
        {

            var mongodb = dbClient.GetDatabase("dotnetbanking");
            var account = mongodb.GetCollection<CreateAccountRequest>("accountdetails");
            IMongoCollection<CreateAccountRequest> collection = mongodb.GetCollection<CreateAccountRequest>("accountdetails");
            var GetAccount = collection.Find(acc => acc.AccountNumber == updateAccountRequest.AccountNumber).FirstOrDefault();
            //Accout Exists or not Check
            if (GetAccount == null) throw new ArgumentException($"Account does not exist ,Please check your Account Number");
            //Account Activate check
            if (GetAccount.AccountStatus == "Deactivated") throw new ArgumentException($"Your Account is Deactivated");

            ResponseUpdateAccount responseUpdateAccount = new ResponseUpdateAccount();
            responseUpdateAccount.Message = "Account Updated";

            //Updating Email
            if (updateAccountRequest.Email != "")
            {
                collection.UpdateOne(A => A.AccountNumber == updateAccountRequest.AccountNumber, (Builders<CreateAccountRequest>.Update.Set("Email", updateAccountRequest.Email)));
                responseUpdateAccount.Email = updateAccountRequest.Email;
            };
            //Updating Contact Number
            if (updateAccountRequest.ContactNumber != 0)
            {
                collection.UpdateOne(A => A.AccountNumber == updateAccountRequest.AccountNumber, (Builders<CreateAccountRequest>.Update.Set("ContactNumber", updateAccountRequest.ContactNumber)));
                responseUpdateAccount.ContactNumber = updateAccountRequest.ContactNumber;
            };
            //Updating Address
            if (updateAccountRequest.AccountAddress != null)
            {
                collection.UpdateOne(A => A.AccountNumber == updateAccountRequest.AccountNumber, (Builders<CreateAccountRequest>.Update.Set("AccountAddress", updateAccountRequest.AccountAddress)));
                responseUpdateAccount.AccountAddress = updateAccountRequest.AccountAddress;
            }

            responseUpdateAccount.IFSC = updateAccountRequest.IFSC;
            responseUpdateAccount.AccountNumber = updateAccountRequest.AccountNumber;

            return responseUpdateAccount;
        }
        public CreateAccountRequest GetAccountbyAccountNumber(int AccountNumber)
        {
            var database = dbClient.GetDatabase("dotnetbanking");


            IMongoCollection<CreateAccountRequest> accountcollection = database.GetCollection<CreateAccountRequest>("accountdetails");
            var GetAccount = accountcollection.Find(acc => acc.AccountNumber == AccountNumber).FirstOrDefault();
            // Existing Account Check
            if (GetAccount == null) throw new ArgumentException($"No Account Present with this " + AccountNumber + " AcountNumber");
            return GetAccount;
        }
        public DeleteAccountResponse DeleteAccount(DeleteAccountRequest deleteAccountRequest)
        {
            var database = dbClient.GetDatabase("dotnetbanking");
            IMongoCollection<CreateAccountRequest> accountcollection = database.GetCollection<CreateAccountRequest>("accountdetails");
            var GetAccount = accountcollection.Find(acc => acc.AccountNumber == deleteAccountRequest.AccountNumber).FirstOrDefault();
            var GetPAN = accountcollection.Find(acc => acc.PAN == deleteAccountRequest.PAN).FirstOrDefault();
            if (GetAccount == null || GetPAN == null) throw new ArgumentException($"No Account Present with this Account Number and PAN");

            accountcollection.UpdateOne(A => A.AccountNumber == deleteAccountRequest.AccountNumber, (Builders<CreateAccountRequest>.Update.Set("AccountStatus", "Deactivated")));
            DeleteAccountResponse deleteAccountResponse = new DeleteAccountResponse
            {
                Message = "Account Deleted",
                PAN = deleteAccountRequest.PAN,
                AccountNumber = deleteAccountRequest.AccountNumber
            };

            return deleteAccountResponse;
        }

        private  int CreateAccountNumber()
        {

            Random Number = new Random();
            int AccountNumber = Number.Next(100000000, 999999999);
            return AccountNumber;
        }
    }
}