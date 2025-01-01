﻿

using MongoDB.Bson.Serialization.Attributes;

namespace eCommerce.OrderService.DataAccessLayer.Entities;

public class Order
{

    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public int _id { get; set; }

    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid OrderID { get; set; }

    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid UserID { get; set; }

    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public DateTime OrderDate { get; set; }

    [BsonRepresentation(MongoDB.Bson.BsonType.Double)]
    public  decimal TotalBill { get; set; }
    public List<OrderItem> OrderItem { get; set; } = new List<OrderItem>();
}