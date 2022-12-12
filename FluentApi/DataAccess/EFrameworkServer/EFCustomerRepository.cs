using FluentApi.Domain.Abstractions;
using FluentApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentApi.DataAccess.EFrameworkServer
{
    public class EFCustomerRepository : ICustomerRepository
    {
        public void AddData(Customer data)
        {
            using (var context = new MyContext())
            {
                context.Customers.Add(data);
                context.SaveChanges();
            }
        }

        public void DeleteData(Customer data)
        {
            using (var context = new MyContext())
            {
                context.Entry(data).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public ICollection<Customer> GetAll()
        {
            List<Customer> customers = null;
            using (var context = new MyContext())
            {
                customers = context.Customers.ToList();
            }
            return customers;
        }

        public Customer GetData(int id)
        {
            using (var context = new MyContext())
            {
                var data = context.Customers.Include(nameof(Customer.Orders)).FirstOrDefault(c => c.Id == id);
                return data;
            }
        }

        public void UpdateData(Customer data)
        {
            using (var context = new MyContext())
            {
                context.Entry(data).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
