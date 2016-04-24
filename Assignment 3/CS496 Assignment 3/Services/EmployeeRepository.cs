using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CS496_Assignment_3.Models;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System.Configuration;
using Microsoft.Azure;

namespace CS496_Assignment_3.Services
{
    public class EmployeeRepository
    {
        private const string CacheKey = "ContactStore";

        public EmployeeRepository()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    var contacts = new Employee[]
                    {
                        new Employee
                        {
                            Id = 1,
                            Name = "Glenn Block"
                        },
                        new Employee
                        {
                            Id = 2,
                            Name = "Dan Roth"
                        }
                    };

                    ctx.Cache[CacheKey] = contacts;
                }
            }
        }
        public Employee[] GetAllContacts()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Employee[])ctx.Cache[CacheKey];
            }

            return new Employee[]
            {
                new Employee
                {
                    Id = 0,
                    Name = "Placeholder"
                }
            };
        }

        public bool SaveContact(Employee contact)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    var currentData = ((Employee[])ctx.Cache[CacheKey]).ToList();
                    currentData.Add(contact);
                    ctx.Cache[CacheKey] = currentData.ToArray();

                    return true;
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }

            return false;
        }
    }
}