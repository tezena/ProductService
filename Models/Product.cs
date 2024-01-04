using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ProductService.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("UserId")]
        public string UserId { get; set; }

        [BsonElement("PCode")]
        public string Pcode { get; set; }

        [BsonElement("Name")]
        public string ProductName { get; set; }

        [BsonElement("Category")]
        public string Category { get; set; }

        [BsonElement("Price")]
        public double Price { get; set; }

        [BsonElement("Quantity")]
        public int Quantity { get; set; }

        [BsonElement("Supplier")]
        public string Supplier { get; set; }

        [BsonElement("CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [BsonElement("ExpireDate")]
        public DateTime ExpireDate { get; set; }


    }
}
