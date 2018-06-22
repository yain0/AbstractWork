using AbstractWorkService.BindingModels;
using AbstractWorkService.ViewModels;
using System.Collections.Generic;

namespace AbstractWorkService.Interfaces
{
    public interface IMyService
    {
        List<ActivityViewModel> GetList();

        void CreateActivity(ActivityBindingModel model);

        void TakeActivityInWork(ActivityBindingModel model);

        void FinishActivity(int id);

        void PayActivity(int id);

        void PutMaterialOnSklad(SkladMaterialBindingModel model);
    }
}
