using MongoDB.Driver;
namespace DotNetBank
{
    public interface IBranchDatabase
    {
        public ResponseBranch CreateBranch(Branch branch);
        public List<Branch> GetBranch(string BankName);
    }
}