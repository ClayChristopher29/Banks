using System;
using System.Collections.Generic;
using System.Linq;

namespace review
{

    class Program
    {
        static void Main(string[] args)
        {

            List<Bank> banks = new List<Bank>() {
            new Bank(){ Name="First Tennessee", Symbol="FTB"},
            new Bank(){ Name="Wells Fargo", Symbol="WF"},
            new Bank(){ Name="Bank of America", Symbol="BOA"},
            new Bank(){ Name="Citibank", Symbol="CITI"},
        };


            List<Customer> customers = new List<Customer>() {
            new Customer(){ Name="Bob Lesman", Balance=80345.66, Bank="FTB"},
            new Customer(){ Name="Joe Landy", Balance=9284756.21, Bank="WF"},
            new Customer(){ Name="Meg Ford", Balance=487233.01, Bank="BOA"},
            new Customer(){ Name="Peg Vale", Balance=7001449.92, Bank="BOA"},
            new Customer(){ Name="Mike Johnson", Balance=790872.12, Bank="WF"},
            new Customer(){ Name="Les Paul", Balance=8374892.54, Bank="WF"},
            new Customer(){ Name="Sid Crosby", Balance=957436.39, Bank="FTB"},
            new Customer(){ Name="Sarah Ng", Balance=56562389.85, Bank="FTB"},
            new Customer(){ Name="Tina Fey", Balance=1000000.00, Bank="CITI"},
            new Customer(){ Name="Sid Brown", Balance=49582.68, Bank="CITI"}
        };




            var millionairesPerBank =
            from customer in customers
            where customer.Balance >= 1000000 // find the millionaires
            // we want to see the bank in the left column, so that's what we'll group by
            group customer by customer.Bank into customerBank // customerBank is an IGrouping
            select new { name = customerBank.Key, count = customerBank.Count() };

            foreach (var bank in millionairesPerBank)
            {
                Console.WriteLine($"{bank.name} - {bank.count}");
            }



            Console.WriteLine("--- List of Millionaires: ---");
            IEnumerable<ReportItem> millionariesReport =
              from customer in customers
              where customer.Balance >= 1000000 // find the millionaires
              orderby customer.Name.Split(' ')[1] // order them by last name
              join bank in banks on customer.Bank equals bank.Symbol // join on the banks collectioin
              select new ReportItem() // create a new report item
              {
                  CustomerName = customer.Name,
                  BankName = bank.Name
              };

            foreach (ReportItem item in millionariesReport)
            {
                Console.WriteLine($"{item.CustomerName} at {item.BankName}");
            }

        }
    }
}