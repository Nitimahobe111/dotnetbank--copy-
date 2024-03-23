
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace DotNetBank
{

    public class CreateBankRequest
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        public string BankName { get; set; } = null!;


        public string Email { get; set; } = null!;

        public double BankFund { get; set; }

        public string? IFSC { get; set; }

        public long ContactNumber { get; set; }
        public Address BankAddress { get; set; } = null!;


    }

    public class Address
    {
        public string Line1 { get; set; } = null!;
        public string Line2 { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public int PinCode { get; set; }
        public string Country { get; set; } = null!;
    }
    public class Employee
    {
       public string EmpId {get; set;} =null;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public  string Deptartment { get; set; }
        public string add { get; set; } = null!;
        public  string DAte { get; set; }
        public  int Salary { get; set; }

    }

}