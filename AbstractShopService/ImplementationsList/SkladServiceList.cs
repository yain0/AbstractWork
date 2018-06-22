using AbstractWorkModel;
using AbstractWorkService.BindingModels;
using AbstractWorkService.Interfaces;
using AbstractWorkService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractWorkService.ImplementationsList
{
    public class SkladServiceList : ISkladService
    {
        private DataListSingleton source;

        public SkladServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<SkladViewModel> GetList()
        {
            List<SkladViewModel> result = source.Sklad
                .Select(rec => new SkladViewModel
                {
                    Id = rec.Id,
                    SkladName = rec.SkladName,
                    SkladMaterials = source.SkladMaterial
                            .Where(recPC => recPC.SkladId == rec.Id)
                            .Select(recPC => new SkladMaterialViewModel
                            {
                                Id = recPC.Id,
                                SkladId = recPC.SkladId,
                                MaterialId = recPC.MaterialId,
                                MaterialName = source.Material
                                    .FirstOrDefault(recC => recC.Id == recPC.MaterialId)?.MaterialName,
                                Koll = recPC.Koll
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public SkladViewModel GetElement(int id)
        {
            Sklad element = source.Sklad.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                // требуется дополнительно получить список компонентов на складе и их количество
                return new SkladViewModel
                {
                    Id = element.Id,
                    SkladName = element.SkladName,
                    SkladMaterials = source.SkladMaterial
                            .Where(recPC => recPC.SkladId == element.Id)
                            .Select(recPC => new SkladMaterialViewModel
                            {
                                Id = recPC.Id,
                                SkladId = recPC.SkladId,
                                MaterialId = recPC.MaterialId,
                                MaterialName = source.Material
                                    .FirstOrDefault(recC => recC.Id == recPC.MaterialId)?.MaterialName,
                                Koll = recPC.Koll
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(SkladBindingModel model)
        {
            Sklad element = source.Sklad.FirstOrDefault(rec => rec.SkladName == model.SkladName);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            int maxId = source.Sklad.Count > 0 ? source.Sklad.Max(rec => rec.Id) : 0;
            source.Sklad.Add(new Sklad
            {
                Id = maxId + 1,
                SkladName = model.SkladName
            });
        }

        public void UpdElement(SkladBindingModel model)
        {
            Sklad element = source.Sklad.FirstOrDefault(rec =>
rec.SkladName == model.SkladName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            element = source.Sklad.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.SkladName = model.SkladName;
        }

        public void DelElement(int id)
        {
            Sklad element = source.Sklad.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                // при удалении удаляем все записи о компонентах на удаляемом складе
                source.SkladMaterial.RemoveAll(rec => rec.SkladId == id);
                source.Sklad.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }

        }
    }
}
