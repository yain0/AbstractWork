﻿using AbstractWorkService.BindingModels;
using AbstractWorkService.ViewModels;
using System.Collections.Generic;

namespace AbstractWorkService.Interfaces
{
    public interface ICustomerService
    {
        List<CustomerViewModel> GetList();

        CustomerViewModel GetElement(int id);

        void AddElement(CustomerBindingModel model);

        void UpdElement(CustomerBindingModel model);

        void DelElement(int id);
    }
}
