using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DotNetBank
{

    public class ResponseAccount
    {
        public string Message { get; set; } = null!;
        public int AccountNumber { get; set; }
        public string? IFSC { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PAN { get; set; } = null!;
        public long ContactNumber { get; set; }
        public string DOB { get; set; } = null!;
        public string Email { get; set; } = null!;






    }



}