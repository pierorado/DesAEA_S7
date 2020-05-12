using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        public static DataClasses1DataContext context = new DataClasses1DataContext();
        static void Main(string[] args)
        {
            Filtering();
            Console.Read();
        }
        static void IntroToLINQ()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };
            var numQuery =
            from num in numbers
            where (num % 2) == 0
            select num;

            foreach (int num in numQuery)
            {
                Console.Write("{0,1}", num);
            }

            Console.WriteLine("Modo lambda");
            int[] numeros2 = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            var numQuery2 = numeros2.Where(c => c % 2 == 0);

            foreach (int num2 in numQuery2)
            {
                Console.Write("{0, 1}", num2);
            }
        }

        static void DataSource()
        {
            var queryAllCustomers = from cust in context.clientes
                                    select cust;

            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }

            Console.WriteLine("/////////////Modo lambda////////////");
            var Allcustomers2 = context.clientes.ToList();
            foreach (var i in Allcustomers2)
            {
                Console.WriteLine(i.NombreCompañia);
            }

        }

        static void Filtering()
        {
            var queryLondonCustomers = from cust in context.clientes
                                       where cust.Ciudad == "Londres"
                                       select cust;

            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }

            Console.WriteLine("/////////////Modo lambda////////////");
            var LqueryLondonCustomers = context.clientes.Where(c => c.Ciudad == "Londres");
            foreach (var Litem in LqueryLondonCustomers)
            {
                Console.WriteLine(Litem.NombreContacto);
            }
        }

        static void Ordering()
        {
            var queryLondonCustomers3 = from cust in context.clientes
                                        where cust.Ciudad == "Londres"
                                        orderby cust.NombreCompañia ascending
                                        select cust;
            foreach (var item in queryLondonCustomers3)
            {

                Console.WriteLine(item.NombreCompañia);
            }

            Console.WriteLine("/////////////Modo lambda////////////");
            var LqueryLondonCustomers3 = context.clientes.Where(x => x.Ciudad == "Londres").OrderBy(x => x.NombreCompañia);

            foreach (var cliente in queryLondonCustomers3)
            {
                Console.WriteLine(cliente.NombreCompañia);
            }
        }

        static void Grouping()
        {
            var queryCustomersByCity =
              from cust in context.clientes
              group cust by cust.Ciudad;

            foreach (var group in queryCustomersByCity)
            {
                Console.WriteLine(group.Key);
                foreach (clientes cus in group)
                {
                    Console.WriteLine(" {0}", cus.NombreCompañia);
                }
            }
            Console.WriteLine("/////////////Modo lambda////////////");
            var LqueryCustomersByCity = context.clientes.GroupBy(c => c.Ciudad);

            foreach (var LcustomerGroup in LqueryCustomersByCity)
            {
                Console.WriteLine(LcustomerGroup.Key);

                foreach (clientes Lcustomer in LcustomerGroup)
                {
                    Console.WriteLine(" {0}", Lcustomer.NombreCompañia);
                }
            }
        }

        static void Grouping2()
        {
            var custQuery = from cust in context.clientes
                            group cust by cust.Ciudad into custGroup
                            where custGroup.Count() > 2
                            orderby custGroup.Key
                            select custGroup;

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
            Console.WriteLine("/////////////Modo lambda////////////");
            var LcustQuery = context.clientes
                .GroupBy(c => c.Ciudad)
                .Where(c => c.Key.Count() > 2)
                .OrderBy(c => c.Key);

            foreach (var Litem in LcustQuery)
            {
                Console.WriteLine(Litem.Key);
            }

        }

        static void Joining()
        {
            var innerJoinQuery = from cust in context.clientes
                                 join dist in context.Pedidos on cust.idCliente equals dist.IdCliente
                                 select new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario };

            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
            Console.WriteLine("Modo lambda");

            var joinlambda = context.clientes.Join(context.Pedidos, a => a.idCliente, b => b.IdCliente,
                (a, b) => new { a.NombreCompañia, b.PaisDestinatario });
            foreach (var item in joinlambda)
            {
                Console.WriteLine($" NOMBRE : {item.NombreCompañia}; DESTINATARIO :{item.PaisDestinatario}");
            }
        }
    }
}
