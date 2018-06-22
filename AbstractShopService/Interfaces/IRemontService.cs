using AbstractWorkService.BindingModels;
using AbstractWorkService.ViewModels;
using System.Collections.Generic;

namespace AbstractWorkService.Interfaces
{
    public interface IRemontService
    {
        List<RemontViewModel> GetList();

        RemontViewModel GetElement(int id);

        void AddElement(RemontBindingModel model);

        void UpdElement(RemontBindingModel model);

        void DelElement(int id);
    }
}
