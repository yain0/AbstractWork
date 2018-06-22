using AbstractWorkModel;
using AbstractWorkService.BindingModels;
using AbstractWorkService.Interfaces;
using AbstractWorkService.ViewModels;
using System;
using System.Collections.Generic;

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
            List<MaterialViewModel> result = new List<MaterialViewModel>();
            for (int i = 0; i < source.Material.Count; ++i)
            {
                result.Add(new MaterialViewModel
                {
                    Id = source.Material[i].Id,
                    MaterialName = source.Material[i].MaterialName
                });
            }
            return result;
        }

        public MaterialViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Material.Count; ++i)
            {
                if (source.Material[i].Id == id)
                {
                    return new MaterialViewModel
                    {
                        Id = source.Material[i].Id,
                        MaterialName = source.Material[i].MaterialName
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(MaterialBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Material.Count; ++i)
            {
                if (source.Material[i].Id > maxId)
                {
                    maxId = source.Material[i].Id;
                }
                if (source.Material[i].MaterialName == model.MaterialName)
                {
                    throw new Exception("Уже есть компонент с таким названием");
                }
            }
            source.Material.Add(new Material
            {
                Id = maxId + 1,
                MaterialName = model.MaterialName
            });
        }

        public void UpdElement(MaterialBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Material.Count; ++i)
            {
                if (source.Material[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Material[i].MaterialName == model.MaterialName && 
                    source.Material[i].Id != model.Id)
                {
                    throw new Exception("Уже есть компонент с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Material[index].MaterialName = model.MaterialName;
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.Material.Count; ++i)
            {
                if (source.Material[i].Id == id)
                {
                    source.Material.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
