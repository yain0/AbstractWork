using AbstractWorkService.BindingModels;
using AbstractWorkService.ViewModels;
using System.Collections.Generic;

namespace AbstractWorkService.Interfaces
{
    public interface ISkladService
    {
        List<SkladViewModel> GetList();

        SkladViewModel GetElement(int id);

        void AddElement(SkladBindingModel model);

        void UpdElement(SkladBindingModel model);

        void DelElement(int id);
    }
}
