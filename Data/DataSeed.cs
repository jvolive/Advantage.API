using Advantage.API.Models;

namespace Advantage.API

{
    public class DataSeed
    {

        private readonly ApiContext _ctx;

        public DataSeed(ApiContext ctx)
        {
            _ctx = ctx;
        }

        public void SeedData(int nCustomers, int nOrders)
        {
            if (!_ctx.Customers.Any())
            {
                SeedCustomers(nCustomers);
                _ctx.SaveChanges();
            }
            if (!_ctx.Orders.Any())
            {
                SeedOrders(nOrders);

            }
            _ctx.SaveChanges();
        }
        private void SeedCustomers(int n)
        {
            List<Customer> customers = BuildCustomerList(n);
            foreach (var customer in customers)
            {
                _ctx.Customers.Add(customer);
            }
        }
        private void SeedOrders(int n)
        {
            List<Order> orders = BuildOrderList(n);
            foreach (var order in orders)
            {
                _ctx.Orders.Add(order);
            }
        }
        private List<Customer> BuildCustomerList(int nCustomers)
        {
            var customers = new List<Customer>();
            var names = new List<string>();


            for (var i = 1; i <= nCustomers; i++)
            {
                var name = Helpers.MakeUniqueCustomerName(names);
                names.Add(name);

                customers.Add(new Customer
                {
                    Id = i,
                    Name = name,
                    Email = Helpers.MakeCustomerEmail(name),
                    State = Helpers.MakeCustomerState()
                });
            }
            return customers;
        }
        private List<Order> BuildOrderList(int nOrders)
        {
            var orders = new List<Order>();
            var rand = new Random();

            for (var i = 1; i <= nOrders; i++)
            {

                var placed = Helpers.GetRandomOrderPlaced();
                var completed = Helpers.GetRandomOrderCompleted(placed);

                orders.Add(new Order
                {
                    Id = i,
                    Customer = Helpers.GetRandomCustomer(_ctx),
                    Total = Helpers.GetRandomOrderTotal(),
                    Placed = placed,
                    Completed = completed
                });
            }
            return orders;
        }
    }
}