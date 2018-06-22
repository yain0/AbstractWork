using AbstractWorkService.BindingModels;
using AbstractWorkService.ViewModels;
using System.Collections.Generic;

namespace AbstractWorkService.Interfaces
{
    public interface IWorkerService
    {
        List<WorkerViewModel> GetList();

        WorkerViewModel GetElement(int id);

        void AddElement(WorkerBindingModel model);

        void UpdElement(WorkerBindingModel model);

        void DelElement(int id);
    }
}
