using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace CustomerAndOrder
{
    public class Customer
    {
        public int CustomerID { get; set; } // Unique identifier for a customer
        public string FirstName { get; set; } // First name of the customer
        public string LastName { get; set; } // Last name of the customer
        public string Country { get; set; } // Country where the customer is located

        public override string ToString()
        {
            return $"Customer id: {CustomerID}, Name: {FirstName} {LastName}, Country: {Country}";
        }
    }


    // This is the Order class, representing an order placed by a customer.
    public class Order
    {
        public int OrderID { get; set; } // Unique identifier for an order
        public int CustomerID { get; set; } // Identifier for the customer who placed the order
        public DateTime OrderDate { get; set; } // Date on which the order was placed
        public DateTime? ShippedDate { get; set; } // Date on which the order was shipped, nullable

        public override string ToString()
        {
            return $"Order id: {OrderID}, Customer id: {CustomerID}, Order date: {OrderDate.ToShortDateString()}, Shipped date: {ShippedDate?.ToShortDateString()}";
        }
    }


    // This is the Product class, representing a product that may be ordered.
    public class Product
    {
        public int ProductID { get; set; } // Unique identifier for a product
        public string ProductName { get; set; } // Name of the product
        public decimal UnitPrice { get; set; } // Price of a single unit of the product

        public override string ToString()
        {
            return $"Product Id {ProductID}, Product Name {ProductName}, Unit Price {UnitPrice}";
        }
    }


    // This is the OrderDetail class, representing a line item in an order.
    public class OrderDetail
    {
        public int OrderDetailID { get; set; } // Unique identifier for an order detail
        public int OrderID { get; set; } // Identifier for the order to which the detail belongs
        public int ProductID { get; set; } // Identifier for the product in the order detail
        public int Quantity { get; set; } // Number of units of the product ordered
        public decimal Discount { get; set; } // Discount applied to the product in the order detail
    }
    public class Program
    {

        private List<Customer> customers;
        private List<Product> products;
        private List<Order> orders;
        private List<OrderDetail> orderDetails;
        static void Main(string[] args)
        {
            Program program = new Program();
            program.AllCustomersInAlphabeticOrder();
            Console.WriteLine();
            program.AllProductsInDescUnitPrice();
            Console.WriteLine();
            program.AllOrdersShippedBySpecificDate(new DateTime(2021, 10, 1));
            Console.WriteLine();
            program.AllOrdersShipToCustomersIn("USA");
            Console.WriteLine();
            program.AllProductsOrderedAtLeastOnce();
            Console.WriteLine();
            program.CustomersOrderedByAmtThenNumOrderThenAvgAmountDesc();
            Console.WriteLine();
            program.MostPopularProduct();


            Console.ReadKey();
        }

        public Program()
        {
            InitializeData();
        }

        public void InitializeData()
        {
            customers = new List<Customer>
            {
                new Customer { CustomerID = 1, FirstName = "John", LastName = "Doe", Country = "USA" },
                new Customer { CustomerID = 2, FirstName = "Jane", LastName = "Doe", Country = "USA" },
                new Customer { CustomerID = 3, FirstName = "Alice", LastName = "Smith", Country = "Canada" },
                new Customer { CustomerID = 4, FirstName = "Bob", LastName = "Smith", Country = "Canada" },
                new Customer { CustomerID = 5, FirstName = "Charlie", LastName = "Brown", Country = "USA" }
            };

            products = new List<Product>
        {
            new Product { ProductID = 1, ProductName = "Chai", UnitPrice = 18.00M },
            new Product { ProductID = 2, ProductName = "Chang", UnitPrice = 19.00M },
            new Product { ProductID = 3, ProductName = "Aniseed Syrup", UnitPrice = 10.00M },
            new Product { ProductID = 4, ProductName = "Chef Anton's Cajun Seasoning", UnitPrice = 22.00M },
            new Product { ProductID = 5, ProductName = "Chef Anton's Gumbo Mix", UnitPrice = 21.35M },
            new Product { ProductID = 6, ProductName = "Grandma's Boysenberry Spread", UnitPrice = 25.00M },
            new Product { ProductID = 7, ProductName = "Uncle Bob's Organic Dried Pears", UnitPrice = 30.00M },
            new Product { ProductID = 8, ProductName = "Northwoods Cranberry Sauce", UnitPrice = 40.00M },
            new Product { ProductID = 9, ProductName = "Mishi Kobe Niku", UnitPrice = 97.00M },
            new Product { ProductID = 10, ProductName = "Ikura", UnitPrice = 31.00M },
            new Product { ProductID = 11, ProductName = "Queso Cabrales", UnitPrice = 21.00M },
            new Product { ProductID = 12, ProductName = "Queso Manchego La Pastora", UnitPrice = 38.00M },
            new Product { ProductID = 13, ProductName = "Konbu", UnitPrice = 6.00M },
            new Product { ProductID = 14, ProductName = "Tofu", UnitPrice = 23.25M },
            new Product { ProductID = 15, ProductName = "Genen Shouyu", UnitPrice = 15.50M }
        };
            orders = new List<Order>
            {
                new Order { OrderID = 1, CustomerID = 1, OrderDate = new DateTime(2022, 1, 1), ShippedDate = new DateTime(2022, 1, 10) },
                new Order { OrderID = 2, CustomerID = 1, OrderDate = new DateTime(2022, 2, 1), ShippedDate = new DateTime(2022, 2, 10) },
                new Order { OrderID = 3, CustomerID = 2, OrderDate = new DateTime(2022, 3, 1), ShippedDate = new DateTime(2022, 3, 10) },
                new Order { OrderID = 4, CustomerID = 3, OrderDate = new DateTime(2022, 4, 1), ShippedDate = new DateTime(2022, 4, 10) },
                new Order { OrderID = 5, CustomerID = 4, OrderDate = new DateTime(2022, 5, 1), ShippedDate = new DateTime(2022, 5, 10) },
                new Order { OrderID = 6, CustomerID = 5, OrderDate = new DateTime(2022, 6, 1), ShippedDate = new DateTime(2022, 6, 10) },
                new Order { OrderID = 7, CustomerID = 1, OrderDate = new DateTime(2022, 7, 1), ShippedDate = new DateTime(2022, 7, 10) },
                new Order { OrderID = 8, CustomerID = 2, OrderDate = new DateTime(2022, 8, 1), ShippedDate = new DateTime(2022, 8, 10) },
                new Order { OrderID = 9, CustomerID = 3, OrderDate = new DateTime(2022, 9, 1), ShippedDate = new DateTime(2022, 9, 10) },
                new Order { OrderID = 10, CustomerID = 4, OrderDate = new DateTime(2022, 10, 1), ShippedDate = new DateTime(2022, 10, 10) }
                };

            orderDetails = new List<OrderDetail>()
            {
                new OrderDetail() { OrderDetailID = 1, OrderID = 1, ProductID = 1, Quantity = 5, Discount = 0.1m },
                new OrderDetail() { OrderDetailID = 2, OrderID = 1, ProductID = 2, Quantity = 3, Discount = 0m },
                new OrderDetail() { OrderDetailID = 3, OrderID = 2, ProductID = 3, Quantity = 2, Discount = 0.05m },
                new OrderDetail() { OrderDetailID = 4, OrderID = 2, ProductID = 4, Quantity = 1, Discount = 0m },
                new OrderDetail() { OrderDetailID = 5, OrderID = 3, ProductID = 5, Quantity = 4, Discount = 0m },
                new OrderDetail() { OrderDetailID = 6, OrderID = 4, ProductID = 6, Quantity = 2, Discount = 0m },
                new OrderDetail() { OrderDetailID = 7, OrderID = 4, ProductID = 7, Quantity = 1, Discount = 0m },
                new OrderDetail() { OrderDetailID = 8, OrderID = 5, ProductID = 8, Quantity = 3, Discount = 0m },
                new OrderDetail() { OrderDetailID = 9, OrderID = 5, ProductID = 9, Quantity = 2, Discount = 0m },
                new OrderDetail() { OrderDetailID = 10, OrderID = 5, ProductID = 10, Quantity = 1, Discount = 0m },
                new OrderDetail() { OrderDetailID = 11, OrderID = 6, ProductID = 1, Quantity = 4, Discount = 0m },
                new OrderDetail() { OrderDetailID = 12, OrderID = 7, ProductID = 2, Quantity = 1, Discount = 0m },
                new OrderDetail() { OrderDetailID = 13, OrderID = 7, ProductID = 3, Quantity = 2, Discount = 0m },
                new OrderDetail() { OrderDetailID = 14, OrderID = 7, ProductID = 4, Quantity = 3, Discount = 0.1m },
                new OrderDetail() { OrderDetailID = 15, OrderID = 8, ProductID = 5, Quantity = 7, Discount = 0m },
                new OrderDetail() { OrderDetailID = 16, OrderID = 8, ProductID = 6, Quantity = 1, Discount = 0m },
                new OrderDetail() { OrderDetailID = 17, OrderID = 9, ProductID = 7, Quantity = 2, Discount = 0m },
                new OrderDetail() { OrderDetailID = 18, OrderID = 10, ProductID = 8, Quantity = 3, Discount = 0m },
                new OrderDetail() { OrderDetailID = 19, OrderID = 10, ProductID = 9, Quantity = 1, Discount = 0m },
                new OrderDetail() { OrderDetailID = 20, OrderID = 10, ProductID = 10, Quantity = 4, Discount = 0m }
            };
        }

        public void AllCustomersInAlphabeticOrder()
        {
            Console.WriteLine("all customers sorted by last name in asc order");
            var query = customers.OrderBy(x => x.LastName);
            foreach (Customer customer in query)
            {
                Console.WriteLine(customer);
            }
        }

        public void AllProductsInDescUnitPrice()
        {
            Console.WriteLine("all products sorted by unit price in desc order");
            var query = products.OrderByDescending(x => x.UnitPrice);
            foreach (Product prod in query)
            {
                Console.WriteLine(prod);
            }
        }

        public void AllOrdersShippedBySpecificDate(DateTime date)
        {
            Console.WriteLine($"all orders shipped by {date.Month}, {date.Year}");
            var query = orders.Where(x => x.ShippedDate?.Month == date.Month && x.ShippedDate?.Year == date.Year);
            foreach (Order order in query)
            {
                Console.WriteLine(order);
            }
        }

        public void AllOrdersShipToCustomersIn(string country)
        {
            Console.WriteLine($"all orders shipped to customers in {country}");
            var query = orders.Where(x => customers.Where(cus => cus.CustomerID == x.CustomerID && cus.Country == country).Any());
            foreach (Order order in query)
            {
                Console.WriteLine(order);
            }
        }

        public void AllProductsOrderedAtLeastOnce()
        {

            Console.WriteLine("All products ordered at least once");
            var query = products
                .Where(product => orderDetails.Where(orderDetail => product.ProductID == orderDetail.ProductID).Any())
                .Select(prod =>
                {
                    int ProdQuantity = orderDetails.Where(detail => detail.ProductID == prod.ProductID).ToList().Sum(detail => detail.Quantity);
                    double ProdRevenue = ProdQuantity * Convert.ToDouble(prod.UnitPrice);
                    return new
                    {
                        ProdName = prod.ProductName,
                        Quantity = ProdQuantity,
                        Revenue = ProdRevenue
                    };
                });

            foreach (var product in query)
            {
                Console.WriteLine($"Product: {product.ProdName}");
                Console.WriteLine($"Total Quantity Ordered: {product.Quantity}");
                Console.WriteLine($"Total Revenue Generated: Rs. {product.Revenue}");
                Console.WriteLine();
            }
        }

        public void CustomersOrderedByAmtThenNumOrderThenAvgAmountDesc()
        {

            // step1 : find customers and their respective orders, number of orders by combining customers and orders
            // example : customer with ID = 4
            // orders = [(OrderID = 5, CustomerID = 4), (OrderID = 10, CustomerID = 4)]

            // step2 : find orderDetails associated with orders

            // step3 : find products associcated with orderDetails

            // use group by
            var customerAndOrders = orders
                .Join(orderDetails,
                order => order.OrderID,
                orderDetail => orderDetail.OrderID,
                (order, orderDetail) => new
                {
                    CusId = order.CustomerID,
                    OrderId = order.OrderID,
                    OrderDetailId = orderDetail.OrderDetailID,
                    ProdId = orderDetail.ProductID,
                    Count = orderDetail.Quantity,
                    Discount = orderDetail.Discount,
                }).Join(customers,
                   cusAndOrder => cusAndOrder.CusId,
                   customer => customer.CustomerID,
                   (cusAndOrder, customer) =>
                   {
                       return new
                       {
                           cusAndOrder.CusId,
                           cusAndOrder.OrderId,
                           cusAndOrder.OrderDetailId,
                           cusName = customer.FirstName + " " + customer.LastName,
                           cusAndOrder.ProdId,
                           cusAndOrder.Count,
                           cusAndOrder.Discount,
                       };
                   }).Join(products,
                        cusAndOrder => cusAndOrder.ProdId,
                        prod => prod.ProductID,
                        (cusAndOrder, prod) =>
                        {
                            double count = Convert.ToDouble(cusAndOrder.Count);
                            double price = Convert.ToDouble(prod.UnitPrice);
                            double amtSpend = (price * count) * (1 - Convert.ToDouble(cusAndOrder.Discount));
                            return new
                            {
                                cusAndOrder.CusId,
                                cusAndOrder.OrderId,
                                cusAndOrder.OrderDetailId,
                                cusAndOrder.cusName,
                                cusAndOrder.ProdId,
                                cusAndOrder.Count,
                                amtSpend = amtSpend,
                            };
                        }).GroupBy(cusAndOrder => cusAndOrder.cusName, cusAndOrder => cusAndOrder, (cus, products) =>
                        {
                            int orderCount = products.Select(x => x.OrderId).Distinct().Count();
                            double sum = products.Sum(x => x.amtSpend);
                            double averageSpent = products.Sum(x => x.amtSpend) / orderCount;
                            return new
                            {
                                name = cus,
                                amtSpend = sum,
                                numOrder = orderCount,
                                averageSpent
                            };
                        }).OrderByDescending(record => record.amtSpend).ThenByDescending(record => record.numOrder).ThenByDescending(record=>record.averageSpent);
            foreach (var record in customerAndOrders)
            {
                Console.WriteLine(record);

            }

        }

        public void MostPopularProduct()
        {
            var query = products.Join(
                orderDetails,
                product => product.ProductID,
                orderDetail => orderDetail.ProductID,
                (prod, orderDetail) =>
                {
                    return new
                    {
                        prod, orderDetail,
                    };
                }).GroupBy(orderProds=>orderProds.prod.ProductName, orderProds => orderProds, (prod, orderProds) =>
                {
                    string name = prod;
                    int count = orderProds.Sum(x=>x.orderDetail.Quantity);
                    return new
                    {
                        name,    
                        count
                    };
                }).OrderByDescending(group => group.count).First();
            var mostPopularProduct = query;
            Console.WriteLine("Most popular product is " + mostPopularProduct.name);
        }

    }
}
