using MongoDB.Driver;
namespace DotNetBank
{
    public interface IBankDatabase
    {
        public ResponseBank CreateBank(CreateBankRequest createBankRequest);
        public CreateBankRequest GetBankbyIFSC(string ifscCode);
     
    }
}