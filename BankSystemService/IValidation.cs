using System.Text.RegularExpressions;
namespace DotNetBank
{ 
    public interface IValidations
    {
        public void EmailCheck(string Email);
        public void ContactNumberCheck(long ContactNumber);
        public void AddressCheck(Address address);
        public void NameCheck(string Name);
        public void PANCheck(string PAN, string FirstName, string LastName);
        public int DOBCheck(string DOB);
        public void CheckIFSC(string IFSC);
    }

}