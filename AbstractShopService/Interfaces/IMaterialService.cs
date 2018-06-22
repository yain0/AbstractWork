using AbstractWorkService.BindingModels;
using AbstractWorkService.Attributies;
using AbstractWorkService.ViewModels;
using System.Collections.Generic;

namespace AbstractWorkService.Interfaces
{
    [CustomInterface("Интерфейс для работы с материалами")]
    public interface IMaterialService
    {
        [CustomMethod("Метод получения списка материалов")]
        List<MaterialViewModel> GetList();

        [CustomMethod("Метод получения материала по id")]
        MaterialViewModel GetElement(int id);

        [CustomMethod("Метод добавления материала")]
        void AddElement(MaterialBindingModel model);

        [CustomMethod("Метод изменения данных по материалу")]
        void UpdElement(MaterialBindingModel model);

        [CustomMethod("Метод удаления материала")]
        void DelElement(int id);
    }
}
