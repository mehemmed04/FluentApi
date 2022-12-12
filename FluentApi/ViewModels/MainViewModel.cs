using FluentApi.Commands;
using FluentApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FluentApi.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        public RelayCommand SelectCustomerCommand { get; set; }
        public RelayCommand SelectOrderCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand ResetCommand { get; set; }
        public RelayCommand DeleteCustomerCommand { get; set; }
        public RelayCommand OrderNowCommand { get; set; }
        public RelayCommand DeleteOrderCommand { get; set; }

        public MainViewModel()
        {
            Customer = new Customer();
            AllCustomers = new ObservableCollection<Customer>(App.DB.CustomerRepository.GetAll());

            AllOrders =new ObservableCollection<Order>(App.DB.OrderRepository.GetAll());

            AddCommand = new RelayCommand((o) =>
            {
                if (Customer.Id >= 1)
                {
                    var item = App.DB.CustomerRepository
                    .GetAll()
                    .FirstOrDefault(c => c.ContactName == Customer.ContactName);
                    if (item != null)
                    {
                        MessageBox.Show("Customer is already exists");
                    }
                    else
                    {
                        App.DB.CustomerRepository.AddData(Customer); 
                        AllCustomers = new ObservableCollection<Customer>(App.DB.CustomerRepository.GetAll());
                        MessageBox.Show($" customer was added successfully");
                        ClearForm();
                    }
                }
                else
                {
                    App.DB.CustomerRepository.AddData(Customer);
                    AllCustomers = new ObservableCollection<Customer>(App.DB.CustomerRepository.GetAll());
                    MessageBox.Show($" customer was added successfully");
                    ClearForm();
                }

            });


            ResetCommand = new RelayCommand((o) =>
            {
                ClearForm();
            });



            DeleteCustomerCommand = new RelayCommand((o) =>
            {
                   App.DB.CustomerRepository.DeleteData(Customer);
                AllOrders = new ObservableCollection<Order>(App.DB.OrderRepository.GetAll());
                AllCustomers = new ObservableCollection<Customer>(App.DB.CustomerRepository.GetAll());

            },
            (p) =>
            {
                if(Customer!=null && Customer.Id != 0)
                {
                    return true;
                }
                return false;
            });

            OrderNowCommand = new RelayCommand((o) =>
            {
                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    CustomerId = Customer.Id
                };

                App.DB.OrderRepository.AddData(order);
                AllOrders = new ObservableCollection<Order>(App.DB.OrderRepository.GetAll());
                MessageBox.Show("Order completed successfully");
            },
         (p) =>
         {
             if (Customer != null && Customer.Id != 0)
             {
                 return true;
             }
             return false;
         });



            UpdateCommand = new RelayCommand((o) =>
            {
                App.DB.CustomerRepository.UpdateData(Customer);
                MessageBox.Show("Updated successfully");
                ClearForm();
            });



        }

        public void ClearForm()
        {
            Customer = new Customer();
        }


        private Customer customer;

        public Customer Customer
        {
            get { return customer; }
            set { customer = value; OnPropertyChanged(); }
        }

        private Order selectedOrder;

        public Order SelectedOrder
        {
            get { return selectedOrder; }
            set { selectedOrder = value; OnPropertyChanged(); }
        }


        private ObservableCollection<Customer> allCustomers;

        public ObservableCollection<Customer> AllCustomers
        {
            get { return allCustomers; }
            set { allCustomers = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Order> allOrders;

        public ObservableCollection<Order> AllOrders
        {
            get { return allOrders; }
            set { allOrders = value; OnPropertyChanged(); }
        }

    }
}