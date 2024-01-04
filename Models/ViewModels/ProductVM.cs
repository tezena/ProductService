using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ProductService.Models.ViewModels
{
    public class ProductVM
    {

   

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

      
    }
}
