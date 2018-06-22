using AbstractWorkModel;
using AbstractWorkService.BindingModels;
using AbstractWorkService.Interfaces;
using AbstractWorkService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractWorkService.ImplementationsList
{
    public class MaterialServiceList : IMaterialService
    {
        private DataListSingleton source;

        public MaterialServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<MaterialViewModel> GetList()
        {
            List<MaterialViewModel> result = source.Material
                .Select(rec => new MaterialViewModel
                {
                    Id = rec.Id,
                    MaterialName = rec.MaterialName
                })
                  .ToList(); 
            return result;
        }

        public MaterialViewModel GetElement(int id)
        {
            Material element = source.Material.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new MaterialViewModel
                {
                        Id = element.Id,
                        MaterialName = element.MaterialName
                    };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(MaterialBindingModel model)
        {
            Material element = source.Material.FirstOrDefault(rec => rec.MaterialName == model.MaterialName);
            if (element != null)
            {
                    throw new Exception("Уже есть компонент с таким названием");
            }
            int maxId = source.Material.Count > 0 ? source.Material.Max(rec => rec.Id) : 0;
            source.Material.Add(new Material
            {
                Id = maxId + 1,
                MaterialName = model.MaterialName
            });
        }

        public void UpdElement(MaterialBindingModel model)
        {
            Material element = source.Material.FirstOrDefault(rec =>
rec.MaterialName == model.MaterialName && rec.Id != model.Id);
            if (element != null)
            {
                    throw new Exception("Уже есть компонент с таким названием");
            }
            element = source.Material.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.MaterialName = model.MaterialName;
        }

        public void DelElement(int id)
        {
            Material element = source.Material.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.Material.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }  
        }
    }
}
