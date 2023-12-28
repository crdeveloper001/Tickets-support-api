using System;
using MongoDB.Bson.Serialization.Attributes;

namespace ticket_support_api.Models
{
    public class EmailMessageModel
    {
        [BsonElement]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string _id { get; set; }
        [BsonElement]
        public string? Subject { get; set; }
        [BsonElement]
        public string? EmailAddress { get; set; }
        [BsonElement]
        public string? Message { get; set; }
        [BsonElement]
        public DateTime DateSended { get; set; }
    }
}
