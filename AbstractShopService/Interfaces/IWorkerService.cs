using AbstractWorkService.BindingModels;
using AbstractWorkService.Attributies;
using AbstractWorkService.ViewModels;
using System.Collections.Generic;

namespace AbstractWorkService.Interfaces
{
    [CustomInterface("Интерфейс для работы с работниками")]
    public interface IWorkerService
    {
        [CustomMethod("Метод получения списка работников")]
        List<WorkerViewModel> GetList();

        [CustomMethod("Метод получения работника по id")]
        WorkerViewModel GetElement(int id);

        [CustomMethod("Метод добавления работника")]
        void AddElement(WorkerBindingModel model);

        [CustomMethod("Метод изменения данных по работнику")]
        void UpdElement(WorkerBindingModel model);

        [CustomMethod("Метод удаления работника")]
        void DelElement(int id);
    }
}
