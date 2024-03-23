
namespace DotNetBank
{

    public class UpdateAccountRequest
    {

        public long ContactNumber { get; set; }
        public string? Email { get; set; }
        public int AccountNumber { get; set; }
        public string? IFSC { get; set; }
        public Address? AccountAddress { get; set; }


    }



}