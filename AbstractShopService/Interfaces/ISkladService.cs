using AbstractWorkService.Attributies;
using AbstractWorkService.BindingModels;
using AbstractWorkService.ViewModels;
using System.Collections.Generic;

namespace AbstractWorkService.Interfaces
{
    [CustomInterface("Интерфейс для работы со складами")]
    public interface ISkladService
    {
        [CustomMethod("Метод получения списка складов")]
        List<SkladViewModel> GetList();

        [CustomMethod("Метод получения склада по id")]
        SkladViewModel GetElement(int id);

        [CustomMethod("Метод добавления склада")]
        void AddElement(SkladBindingModel model);

        [CustomMethod("Метод изменения данных по складу")]
        void UpdElement(SkladBindingModel model);

        [CustomMethod("Метод удаления склада")]
        void DelElement(int id);
    }
}
