

namespace DotNetBank
{

    public class ResponseUpdateAccount
    {

        public string Message { get; set; } = null!;
        public long ContactNumber { get; set; }
        public string Email { get; set; } = null!;
        public int AccountNumber { get; set; }
        public string? IFSC { get; set; }
        public Address AccountAddress { get; set; } = null!;

    }

}