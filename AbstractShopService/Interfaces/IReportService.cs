using AbstractWorkModel;
using AbstractWorkService.ViewModels;
using AbstractwWorkService.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractWorkService.Interfaces
{
    public interface IReportService
    {
        void SaveRemontCost(ReportBindingModel model);

        List<SkladsLoadViewModel> GetSkladsLoad();

        void SaveSkladsLoad(ReportBindingModel model);

        List<CustomerActivitysModel> GetCustomerActivitys(ReportBindingModel model);

        void SaveCustomerActivitys(ReportBindingModel model);
    }
}
