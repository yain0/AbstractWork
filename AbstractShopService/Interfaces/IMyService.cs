using AbstractWorkService.Attributies;
using AbstractWorkService.BindingModels;
using AbstractWorkService.ViewModels;
using System.Collections.Generic;

namespace AbstractWorkService.Interfaces
{
    [CustomInterface("Интерфейс для работы с заказами")]
    public interface IMyService
    {
        [CustomMethod("Метод получения списка заказов")]
        List<ActivityViewModel> GetList();

        [CustomMethod("Метод создания заказа")]
        void CreateActivity(ActivityBindingModel model);

        [CustomMethod("Метод передачи заказа в работу")]
        void TakeActivityInWork(ActivityBindingModel model);

        [CustomMethod("Метод передачи заказа на оплату")]
        void FinishActivity(int id);

        [CustomMethod("Метод фиксирования оплаты по заказу")]
        void PayActivity(int id);

        [CustomMethod("Метод пополнения компонент на складе")]
        void PutMaterialOnSklad(SkladMaterialBindingModel model);
    }
}
