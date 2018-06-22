using AbstractWorkService.BindingModels;
using AbstractWorkService.ViewModels;
using System.Collections.Generic;

namespace AbstractWorkService.Interfaces
{
    public interface IMaterialService
    {
        List<MaterialViewModel> GetList();

        MaterialViewModel GetElement(int id);

        void AddElement(MaterialBindingModel model);

        void UpdElement(MaterialBindingModel model);

        void DelElement(int id);
    }
}
