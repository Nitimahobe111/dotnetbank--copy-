
using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DotNetBank
{

    public class ResponseBank
    {

        public string message { get; set; } = null!;
        public string BankName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? IFSC{ get; set; }

        public long ContactNumber { get; set; }


    }



}