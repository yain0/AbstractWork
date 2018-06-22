using AbstractWorkService.Attributies;
using AbstractWorkService.BindingModels;
using AbstractWorkService.ViewModels;
using System.Collections.Generic;

namespace AbstractWorkService.Interfaces
{
    [CustomInterface("Интерфейс для работы с изделиями")]
    public interface IRemontService
    {
        [CustomMethod("Метод получения списка изделий")]
        List<RemontViewModel> GetList();

        [CustomMethod("Метод получения изделия по id")]
        RemontViewModel GetElement(int id);

        [CustomMethod("Метод добавления изделия")]
        void AddElement(RemontBindingModel model);

        [CustomMethod("Метод изменения данных по изделию")]
        void UpdElement(RemontBindingModel model);

        [CustomMethod("Метод удаления изделия")]
        void DelElement(int id);
    }
}
