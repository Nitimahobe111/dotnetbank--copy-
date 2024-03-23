using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace DotNetBank
{

    public class CreateAccountRequest
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PAN { get; set; } = null!;
        public long ContactNumber { get; set; }
        public double Balance { get; set; }
        public string DOB { get; set; } = null!;
        public int Age { get; set; }
        public string Gender { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int AccountNumber { get; set; }
        public string? AccountType { get; set; }
        public string? AccountStatus { get; set; }
        public string? IFSC { get; set; }
        public string? MainBranchIFSC { get; set; }
        public Address AccountAddress { get; set; } = null!;

    }

}