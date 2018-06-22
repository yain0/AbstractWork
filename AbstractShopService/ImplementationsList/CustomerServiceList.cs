using AbstractWorkModel;
using AbstractWorkService.BindingModels;
using AbstractWorkService.Interfaces;
using AbstractWorkService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractWorkService.ImplementationsList
{
    public class CustomerServiceList : ICustomerService
    {
        private DataListSingleton source;

        public CustomerServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<CustomerViewModel> GetList()
        {
            List<CustomerViewModel> result = source.Customer
                .Select(rec => new CustomerViewModel
                {
                    Id = rec.Id,
                    CustomerFIO = rec.CustomerFIO
                })
               .ToList();
            
            return result;
        }

        public CustomerViewModel GetElement(int id)
        {
            Сustomer element = source.Customer.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new CustomerViewModel
                {
                    Id = element.Id,
                    CustomerFIO = element.CustomerFIO
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(CustomerBindingModel model)
        {
            Сustomer element = source.Customer.FirstOrDefault(rec => rec.CustomerFIO == model.CustomerFIO);
            if (element != null)
            { 
                    throw new Exception("Уже есть клиент с таким ФИО");
            }
            int maxId = source.Customer.Count > 0 ? source.Customer.Max(rec => rec.Id) : 0;
            source.Customer.Add(new Сustomer
            {
                Id = maxId + 1,
                CustomerFIO = model.CustomerFIO
            });
        }

        public void UpdElement(CustomerBindingModel model)
        {
            Сustomer element = source.Customer.FirstOrDefault(rec =>
            rec.CustomerFIO == model.CustomerFIO && rec.Id != model.Id);
            if (element != null)
            {
                    throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = source.Customer.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
           element.CustomerFIO = model.CustomerFIO;
        }

        public void DelElement(int id)
        {
            Сustomer element = source.Customer.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.Customer.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
            
        }
    }
}
