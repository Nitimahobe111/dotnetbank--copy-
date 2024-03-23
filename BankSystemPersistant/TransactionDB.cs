using MongoDB.Driver;
namespace DotNetBank
{
    public class TranscationDatabase :ITranscationDatabase
    {
        MongoClient dbClient = new MongoClient("mongodb://localhost:27017");


        public ResponseTransactions Transaction(Transactions transactions)
        {
            //Initializing Response Values
            ResponseTransactions responseTransactions = new ResponseTransactions
            {
                TransactionType = transactions.TransactionType,
                IFSC = transactions.IFSC,
                AccountNumber = transactions.AccountNumber,
                TransactionAmount = transactions.TransactionAmount,
                Message = "Transaction Successfull",
                Date = DateTime.Now

            };

            transactions.Date = DateTime.Now;

            var database = dbClient.GetDatabase("dotnetbanking");
            IMongoCollection<CreateAccountRequest> collection = database.GetCollection<CreateAccountRequest>("accountdetails");
            IMongoCollection<CreateBankRequest> bankcollection = database.GetCollection<CreateBankRequest>("bankdetails");
            IMongoCollection<Branch> branchcollection = database.GetCollection<Branch>("branchdetails");
            IMongoCollection<Transactions> Transactions = database.GetCollection<Transactions>("TransactionDetails");

            //Existing Bank check
            var GetMainIfsc = branchcollection.Find(acc => acc.IFSC == transactions.IFSC).FirstOrDefault();
            if (GetMainIfsc == null) throw new ArgumentException($"Branch does not exist ,Please check IFSC code");
            string Mainifsc = GetMainIfsc.MainBranchIFSC;

            //Existing Branch check
            var GetBank = bankcollection.Find(acc => acc.IFSC == Mainifsc).FirstOrDefault();
            var GetBranch = branchcollection.Find(acc => acc.IFSC == transactions.IFSC).FirstOrDefault();
            if (GetBranch == null) throw new ArgumentException($"Branch does not exist");

            //Existing Account check
            var GetAccount = collection.Find(acc => acc.AccountNumber == transactions.AccountNumber).FirstOrDefault();
            if (GetAccount == null) throw new ArgumentException($"Account does not exist ,Please check your Account Number");
            if (GetAccount.AccountStatus == "Deactivated") throw new ArgumentException($"Your Account is Deactivated");
            double updatedbankfund = GetBank.BankFund;
            var mainifsc = GetBank.IFSC;

            //Deposit 
            if (transactions.TransactionType == "Deposit" || transactions.TransactionType == "deposit")
            {

                collection.UpdateOne(A => A.AccountNumber == transactions.AccountNumber, (Builders<CreateAccountRequest>.Update.Inc("Balance", transactions.TransactionAmount)));
                bankcollection.UpdateOne(b => b.IFSC == mainifsc, (Builders<CreateBankRequest>.Update.Inc("BankFund", transactions.TransactionAmount)));

                var databases = dbClient.GetDatabase("dotnetbanking");
                var tran = databases.GetCollection<Transactions>("TransactionDetails");
                tran.InsertOne(transactions);
            }
            //Withdrawal
            else if (transactions.TransactionType == "Withdrawal" || transactions.TransactionType == "withdrawal")
            {


                var GetAccountbalance = collection.Find(acc => acc.AccountNumber == transactions.AccountNumber).FirstOrDefault();
                if (GetAccountbalance == null) throw new ArgumentException($"Account does not exist");
                if (GetAccountbalance.Balance < transactions.TransactionAmount) throw new ArgumentException($"Not enough funds in Account");
                double wamount = transactions.TransactionAmount * -1;

                var updatedbank = collection.UpdateOne(A => A.AccountNumber == transactions.AccountNumber, (Builders<CreateAccountRequest>.Update.Inc("Balance", wamount)));
                bankcollection.UpdateOne(b => b.IFSC == mainifsc, (Builders<CreateBankRequest>.Update.Inc("BankFund", wamount)));

                var databases = dbClient.GetDatabase("dotnetbanking");
                var tran = databases.GetCollection<Transactions>("TransactionDetails");
                tran.InsertOne(transactions);

                return responseTransactions;
            }

            else
            {

                throw new ArgumentException($"Not a valid Transaction type , Deposit and Withdrawal are the ony valid Transaction type");
            }

            return responseTransactions;

        }

        public List<Transactions> GetTransactionbyDate(string from, string to)
        {
            var dbClient = new MongoClient(Environment.GetEnvironmentVariable("String"));
            var database = dbClient.GetDatabase("dotnetbanking");

            IMongoCollection<Transactions> Transactions = database.GetCollection<Transactions>("TransactionDetails");
            DateTime From = DateTime.Parse(from);
            DateTime To = DateTime.Parse(to);


            List<Transactions> GetBranch = new List<Transactions>();
            GetBranch = Transactions.Find(t => (t.Date >= From && t.Date <= To)).ToList();


            return GetBranch;
        }
        public List<Transactions> GetTransactionbyMonth()
        {
            var dbClient = new MongoClient(Environment.GetEnvironmentVariable("String"));
            var database = dbClient.GetDatabase("dotnetbanking");

            IMongoCollection<Transactions> Transactions = database.GetCollection<Transactions>("TransactionDetails");
            List<Transactions> GetTransactionList = new List<Transactions>();
            GetTransactionList = Transactions.Find(x => x.Date > DateTime.Now.AddDays(-90)).ToList();


            return GetTransactionList;
        }

    }
}
