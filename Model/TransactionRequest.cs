
using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Bson;
namespace DotNetBank
{

    public class Transactions
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        public string TransactionType { get; set; } = null!;
        public double TransactionAmount { get; set; }
        public string? IFSC { get; set; }
        public int AccountNumber { get; set; }
        public DateTime Date { get; set; }


    }
    public class TransactionByDate
    {

        public string From { get; set; } = null!;
        public string To { get; set; } = null!;



    }


}