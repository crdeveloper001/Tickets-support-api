using MongoDB.Bson.Serialization.Attributes;

namespace ticket_support_api.Models
{
    public class ContactModel
    {
        [BsonElement]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? _id { get; set; }
        [BsonElement]
        public string? Name { get; set; }
        [BsonElement]
        public string? LastName { get; set; }
        [BsonElement]
        public string? Email { get; set; }
        [BsonElement]
        public string? Phone { get; set; }
        [BsonElement]
        public string? Description { get; set; }
    }
}
