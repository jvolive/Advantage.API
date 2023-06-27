using Advantage.API.Models;

namespace Advantage.API
{
    public class Helpers
    {
        private static Random _rand = new Random();
        private static string GetRandom(IList<string> items)
        {

            return items[_rand.Next(items.Count)];
        }
        internal static string MakeUniqueCustomerName(List<string> names)
        {
            var prefix = GetRandom(bizPrefix);
            var suffix = GetRandom(bizSuffix);
            var maxNames = bizPrefix.Count * bizSuffix.Count;
            if (names.Count >= maxNames)
            {
                throw new System.InvalidOperationException("Maximo de numeros de nomes excedido");
            }


            var bizName = prefix + suffix;

            if (names.Contains(bizName))
            {
                MakeUniqueCustomerName(names);
            }

            return bizName;
        }
        internal static string MakeCustomerEmail(string customerName)
        {
            return $"contact@{customerName.ToLower()}.com";
        }
        internal static string MakeCustomerState()
        {
            return GetRandom(brState);
        }

        internal static decimal GetRandomOrderTotal()
        {
            return _rand.Next(100, 5000);
        }
        internal static DateTime GetRandomOrderPlaced()
        {
            var end = DateTime.Now;
            var start = end.AddDays(-90);

            TimeSpan possibleSpan = end - start;
            TimeSpan newSpan = new TimeSpan(0, _rand.Next(0, (int)possibleSpan.TotalMinutes), 0);

            return start + newSpan;
        }
        internal static DateTime? GetRandomOrderCompleted(DateTime orderPlaced)
        {
            var now = DateTime.Now;
            var minLeadTime = TimeSpan.FromDays(7);
            var timePassed = now - orderPlaced;

            if (timePassed < minLeadTime)
            {
                return null;
            }
            return orderPlaced.AddDays(_rand.Next(7, 14));
        }
        internal static Customer GetRandomCustomer(ApiContext ctx)
        {
            var randomId = _rand.Next(1, ctx.Customers.Count());
            return ctx.Customers.First(c => c.Id == randomId);
        }



        private static readonly List<string> brState = new List<string>()
        {
            "AC",
            "AL",
            "AP",
            "AM",
            "BA",
            "CE",
            "DF",
            "ES",
            "GO",
            "MA",
            "MT",
            "MS",
            "MG",
            "PA",
            "PB",
            "PR",
            "PE",
            "PI",
            "RJ",
            "RN",
            "RS",
            "RO",
            "RR",
            "SC",
            "SP",
            "SE",
            "TO"
        };

        private static readonly List<string> bizPrefix = new List<string>(){
            "ABC",
            "XYZ",
            "JVO",
            "Acme",
            "MainSt",
            "Ready",
            "Magic",
            "Fluent",
            "Peak",
            "Forward",
            "Enterprise",
            "Sales"
        };

        private static readonly List<string> bizSuffix = new List<string>(){
            "Co",
            "Corp",
            "Holdings",
            "Corporation",
            "Movers",
            "Cleaners",
            "Bakery",
            "Apparel",
            "Rentals",
            "Storage",
            "Transit",
            "Logistics"
        };
    }
}