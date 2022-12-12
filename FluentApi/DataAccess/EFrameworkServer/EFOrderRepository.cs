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
    public class EFOrderRepository : IOrderRepository
    {
        public void AddData(Order data)
        {
            using (var context=new MyContext())
            {
                context.Orders.Add(data);
                context.SaveChanges();
            }
        }

        public void DeleteData(Order data)
        {
            using (var context = new MyContext())
            {
                context.Entry(data).State=EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public ICollection<Order> GetAll()
        {
            using (var context=new MyContext())
            {
                return context.Orders.Include(nameof(Order.Customer)).ToList();
            }
        }

        public Order GetData(int id)
        {
            using (var context = new MyContext())
            {
                var data = context.Orders.Include(nameof(Order.Customer)).FirstOrDefault(c => c.Id == id);
                return data;
            }
        }

        public void UpdateData(Order data)
        {
            using (var context = new MyContext())
            {
                context.Entry(data).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
