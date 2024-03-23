using System.Text.RegularExpressions;
namespace DotNetBank
{
    public interface Iifsc
    {
        public string CreateIFSC(string BankName, string Check);

    }
}