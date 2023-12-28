using MongoDB.Bson.Serialization.Attributes;

namespace ticket_support_api.Models
{
    public class LogTicketModel
    {
        [BsonElement]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? _id { get; set; }
        [BsonElement]
        public string? Name { get; set; }
        [BsonElement]
        public int TicketNumber { get; set; }
        [BsonElement]
        public string? TypeRequest { get; set; }
        [BsonElement]
        public string? Details { get; set; }
        [BsonElement]
        public string? SolutionDetails { get; set; }
        [BsonElement]
        public string? EmailToNotifitication { get; set; }
    }
}
