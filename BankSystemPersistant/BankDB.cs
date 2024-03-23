using MongoDB.Driver;
namespace DotNetBank
{
    public class BankDatabase : IBankDatabase
    {
        MongoClient dbClient = new MongoClient("mongodb://localhost:27017");
        private readonly Iifsc _ifsc;
        public BankDatabase(Iifsc iifsc)
        {
            _ifsc=iifsc;
        }

        public ResponseBank CreateBank(CreateBankRequest createBankRequest)
        {
            //Initializing Response Values
            ResponseBank responseBank = new ResponseBank
            {
                BankName = createBankRequest.BankName,
                Email = createBankRequest.Email,
                ContactNumber = createBankRequest.ContactNumber,
                message = "Bank Successfully Created"
            };

            //Generating IFSC code
            responseBank.IFSC = _ifsc.CreateIFSC(createBankRequest.BankName, "Bank");
            createBankRequest.IFSC = responseBank.IFSC;


            var mongodb = dbClient.GetDatabase("dotnetbanking");
            var account = mongodb.GetCollection<CreateBankRequest>("bankdetails");
            IMongoCollection<CreateBankRequest> collection = mongodb.GetCollection<CreateBankRequest>("bankdetails");
            // Duplicate Bank Nmae check
            var GetBank = collection.Find(acc => acc.BankName == createBankRequest.BankName).FirstOrDefault();
            if (GetBank != null) throw new ArgumentException($"Bank name already exists");

            // Duplicate IFSC check
            var GetBankIFSC = collection.Find(acc => acc.IFSC == createBankRequest.IFSC).FirstOrDefault();
            if (GetBankIFSC != null) throw new ArgumentException($"Bank IFSC already exists");

            // Duplicate Email check
            var GetBankEmail = collection.Find(acc => acc.Email == createBankRequest.Email).FirstOrDefault();
            if (GetBankEmail != null) throw new ArgumentException($"Email already in use , Try differnt Email");

            // Duplicate Contact Number Check
            var GetBankContactNumber = collection.Find(acc => acc.ContactNumber == createBankRequest.ContactNumber).FirstOrDefault();
            if (GetBankContactNumber != null) throw new ArgumentException($"Contact Number already in use , Try differnt Contact Number");


            account.InsertOne(createBankRequest);

            return responseBank;
        }
        public CreateBankRequest GetBankbyIFSC(string ifscCode)
        {
            var database = dbClient.GetDatabase("dotnetbanking");


            IMongoCollection<CreateBankRequest> bankcollection = database.GetCollection<CreateBankRequest>("bankdetails");
            var GetAccount = bankcollection.Find(acc => acc.IFSC == ifscCode).FirstOrDefault();
            //Existing Bank check
            if (GetAccount == null) throw new ArgumentException($"No Bank Present with this " + ifscCode + " BankName");
            return GetAccount;
        }

    }
}