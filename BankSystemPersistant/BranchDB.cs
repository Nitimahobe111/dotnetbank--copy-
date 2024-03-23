using MongoDB.Driver;
namespace DotNetBank
{
    public class BranchDatabase :IBranchDatabase
    {
        MongoClient dbClient = new MongoClient("mongodb://localhost:27017");
        private readonly Iifsc _ifsc;
        public BranchDatabase(Iifsc iifsc)
        {
            _ifsc=iifsc;
        }
        public ResponseBranch CreateBranch(Branch branch)
        {
            var database = dbClient.GetDatabase("dotnetbanking");
            var accounts = database.GetCollection<Branch>("branchdetails");
            //Initializing Response Values
            ResponseBranch responseBranch = new ResponseBranch
            {
                BankName = branch.BankName,
                BranchName = branch.BranchName,
                Email = branch.Email,
                ContactNumber = branch.ContactNumber,
                Message = "Successfully Branch Created"
            };

            //Generating IFSC code
            responseBranch.IFSC = _ifsc.CreateIFSC(responseBranch.BankName, "Branch");
            branch.IFSC = responseBranch.IFSC;

            IMongoCollection<Branch> Branchcollection = database.GetCollection<Branch>("branchdetails");
            IMongoCollection<CreateBankRequest> Bankcollection = database.GetCollection<CreateBankRequest>("bankdetails");

            //Existing Bank check
            var GetBank = Bankcollection.Find(acc => acc.BankName == branch.BankName).FirstOrDefault();
            if (GetBank == null) throw new ArgumentException($"Main branch does not exist for creating branch");
            responseBranch.MainBranchIFSC = GetBank.IFSC;
            branch.MainBranchIFSC = GetBank.IFSC;


            //Existing Branch Name check 
            var GetBankName = Branchcollection.Find(acc => acc.BranchName == branch.BranchName).FirstOrDefault();
            if (GetBankName != null) throw new ArgumentException($"This Branch Name already Exist , Try different Branch Name");


            //Existing Email Check
            var GetEmail = Branchcollection.Find(acc => acc.Email == branch.Email).FirstOrDefault();
            if (GetEmail != null) throw new ArgumentException($"Email is already in use , Try different Email");
            var GetBankEmail = Bankcollection.Find(acc => acc.Email == branch.Email).FirstOrDefault();
            if (GetBankEmail != null) throw new ArgumentException($"Email is already in use , Try different Email");

            //Existing Contact Number Check
            var GetContactNumber = Branchcollection.Find(acc => acc.ContactNumber == branch.ContactNumber).FirstOrDefault();
            if (GetContactNumber != null) throw new ArgumentException($"Contact Number is already in use , Try different Contact NUmber");
            var GetContactNumberBank = Bankcollection.Find(acc => acc.ContactNumber == branch.ContactNumber).FirstOrDefault();
            if (GetContactNumberBank != null) throw new ArgumentException($"Contact Number is already in use , Try different Contact NUmber");

            accounts.InsertOne(branch);

            return responseBranch;
        }
        public List<Branch> GetBranch(string BankName)
        {
            var dbClient = new MongoClient(Environment.GetEnvironmentVariable("String"));
            var database = dbClient.GetDatabase("dotnetbanking");
            IMongoCollection<Branch> branchcollection = database.GetCollection<Branch>("branchdetails");

            List<Branch> GetBranch = new List<Branch>();
            GetBranch = branchcollection.Find(Branch => Branch.BankName == BankName).ToList();

            return GetBranch;
        }

    }
}