using MongoDB.Driver;
namespace DotNetBank
{
    public interface ITranscationDatabase
    {
        public ResponseTransactions Transaction(Transactions transactions);
        public List<Transactions> GetTransactionbyDate(string from, string to);
        public List<Transactions> GetTransactionbyMonth();
    }
}
