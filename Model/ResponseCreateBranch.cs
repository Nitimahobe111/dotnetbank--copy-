
using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Bson;
namespace DotNetBank
{


    public class ResponseBranch
    {

        public string Message { get; set; } = null!;
        public string BankName { get; set; } = null!;
        public string BranchName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public long ContactNumber { get; set; }
        public string? IFSC { get; set; }
        public string? MainBranchIFSC { get; set; }
    }
}