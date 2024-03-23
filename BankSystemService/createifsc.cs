using System.Text.RegularExpressions;
namespace DotNetBank
{
    public class ifsc : Iifsc
    {
        public string CreateIFSC(string BankName, string Check)
        {

            string SecondPart = "";
            if (Check == "Bank")
                SecondPart = "M";
            else
                SecondPart = "B";
            string BankCode = BankName.Substring(0, 3);
            BankCode = BankCode.ToUpper();
            string ThirdPart = "0";
            Random Number = new Random();
            int FourthInt = Number.Next(100000, 999999);
            string FourthPart = FourthInt.ToString();

            string IFSC = BankCode + SecondPart + ThirdPart + FourthPart;
            return IFSC;
        }
        

    }
}