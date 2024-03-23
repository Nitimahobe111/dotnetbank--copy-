
using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DotNetBank
{


    public class Branch
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        public string BankName { get; set; } = null!;
        public string BranchName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public long ContactNumber { get; set; }
        public string? IFSC { get; set; }
        public string? MainBranchIFSC { get; set; }
        public Address BranchAddress { get; set; } = null!;
    }
}