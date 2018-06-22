using AbstractWorkModel;
using AbstractWorkService.Attributies;
using AbstractWorkService.ViewModels;
using AbstractwWorkService.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractWorkService.Interfaces
{
    [CustomInterface("Интерфейс для работы с отчетами")]
    public interface IReportService
    {
        [CustomMethod("Метод сохранения списка изделий в doc-файл")]
        void SaveRemontCost(ReportBindingModel model);

        [CustomMethod("Метод получения списка складов с количество материалов на них")]
        List<SkladsLoadViewModel> GetSkladsLoad();

        [CustomMethod("Метод сохранения списка списка складов с количество материалов на них в xls-файл")]
        void SaveSkladsLoad(ReportBindingModel model);

        [CustomMethod("Метод получения списка заказов клиентов")]
        List<CustomerActivitysModel> GetCustomerActivitys(ReportBindingModel model);

        [CustomMethod("Метод сохранения списка заказов клиентов в pdf-файл")]
        void SaveCustomerActivitys(ReportBindingModel model);
    }
}
