namespace Ordering.Infrastructure.Data.Extensions
{
    internal class InitialData
    {
        public static IEnumerable<Customer> Customers => new List<Customer>
        {
            Customer.Create(CustomerId.Of(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")), "John", "John@Info.com"),
            Customer.Create(CustomerId.Of(new Guid ("b1c72859-640f-4d44-a7ad-bdeea8fa92e4")), "Haytham", "Haytham@Info.com")
        };

        public static IEnumerable<Product> Products => new List<Product>
        {
            Product.Create(ProductId.Of(new Guid("9e4e47b1-8f7e-4d78-b6a3-18a51389d8e2")), "IPhone X", 500),
            Product.Create(ProductId.Of(new Guid("3f6f8d46-b9a4-4bbd-bc91-403bcf00e7cd")), "Samsung 10", 400),
            Product.Create(ProductId.Of(new Guid("d3c76a91-9a54-4c4f-8997-9b8e8cf5bb0a")), "Huawei Plus", 650),
            Product.Create(ProductId.Of(new Guid("7a3d0c59-6f8a-4f9c-8a5e-4c43c91f95e3")), "Xiaomi Mi", 450)
        };

        public static IEnumerable<Order> OrdersWithItems 
        {
            get
            {
                var address1 = Address.Of("John", "Doe", "John@Info.com", "123 Main St", "USA", "NY", "10001");
                var address2 = Address.Of("Haytham", "Smith", "Haytham@Info.com", "456 Market St", "USA", "CA", "94105");

                var payment1 = Payment.Of("4111111111111111", "John Doe", "12/26", "123", 1);
                var payment2 = Payment.Of("5555444433332222", "Haytham Smith", "11/27", "456", 2);

                var order1 = Order.Create(

                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")),
                    OrderName.Of("John's First Order"),
                    shippingAddress: address1,
                    billingAddress: address1,
                    payment: payment1
                );

                order1.Add(ProductId.Of(new Guid("9e4e47b1-8f7e-4d78-b6a3-18a51389d8e2")), 2, 500);
                order1.Add(ProductId.Of(new Guid("3f6f8d46-b9a4-4bbd-bc91-403bcf00e7cd")), 1, 400);

                var order2 = Order.Create(

                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(new Guid("b1c72859-640f-4d44-a7ad-bdeea8fa92e4")),
                    OrderName.Of("Haytham's First Order"),
                    shippingAddress: address2,
                    billingAddress: address2,
                    payment: payment2
                );

                order2.Add(ProductId.Of(new Guid("d3c76a91-9a54-4c4f-8997-9b8e8cf5bb0a")), 1, 650);
                order2.Add(ProductId.Of(new Guid("7a3d0c59-6f8a-4f9c-8a5e-4c43c91f95e3")), 3, 450);

                return new List<Order> { order1, order2 };
            } 
        }
    }
}