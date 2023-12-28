using MongoDB.Bson.Serialization.Attributes;

namespace ticket_support_api.Models
{
    public class InventoryItemModel
    {
        [BsonElement]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? _id { get; set; }
        [BsonElement]
        public string? Code { get; set; }
        [BsonElement]
        public int Quantity { get; set; }
        [BsonElement]
        public string? Tag { get; set; }
        [BsonElement]
        public string? Brand { get; set; }
        [BsonElement]
        public string? RoomLocation { get; set; }
        [BsonElement]
        public string? Category { get; set; }
        [BsonElement]
        public string? CurrentStatus { get; set; }
    }
}
