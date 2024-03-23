
using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Bson;
namespace DotNetBank
{

    public class ResponseTransactions
    {

        public string Message { get; set; } = null!;
        public string TransactionType { get; set; } = null!;
        public double TransactionAmount { get; set; }
        public string? IFSC { get; set; }
        public int AccountNumber { get; set; }
        public DateTime Date { get; set; }



    }

}