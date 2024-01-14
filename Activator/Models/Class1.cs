namespace Activator.Models
{
    public abstract class BaseInfo
    {
        public abstract override string ToString();
    }

    public class Category : BaseInfo
    {
        public string Type1 { get; set; }
        public string Type2 { get; set; }

        public override string ToString()
        {
            return $"Category: Type1 - {Type1}, Type2 - {Type2}";
        }
    }

    public class Employee : BaseInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string ToString()
        {
            return $"Employee: FirstName - {FirstName}, LastName - {LastName}";
        }
    }

    public class Order : BaseInfo
    {
        public string OrderId { get; set; }
        public string CompanyName { get; set; }

        public override string ToString()
        {
            return $"Order: OrderId - {OrderId}, CompanyName - {CompanyName}";
        }
    }

    public class Product : BaseInfo
    {
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public string UnitPrice { get; set; }

        public override string ToString()
        {
            return $"Product: ProductName - {ProductName}, QuantityPerUnit - {QuantityPerUnit}, UnitPrice - {UnitPrice}";
        }
    }
}
