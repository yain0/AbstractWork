using AbstractWorkModel;
using AbstractWorkService.BindingModels;
using AbstractWorkService.Interfaces;
using AbstractWorkService.ViewModels;
using System;
using System.Collections.Generic;

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
            List<SkladViewModel> result = new List<SkladViewModel>();
            for (int i = 0; i < source.Sklad.Count; ++i)
            {
                // требуется дополнительно получить список компонентов на складе и их количество
                List<SkladMaterialViewModel> SkladMaterial = new List<SkladMaterialViewModel>();
                for (int j = 0; j < source.SkladMaterial.Count; ++j)
                {
                    if (source.SkladMaterial[j].SkladId == source.Sklad[i].Id)
                    {
                        string materialName = string.Empty;
                        for (int k = 0; k < source.Material.Count; ++k)
                        {
                            if (source.RemontMaterial[j].MaterialId == source.Material[k].Id)
                            {
                                materialName = source.Material[k].MaterialName;
                                break;
                            }
                        }
                        SkladMaterial.Add(new SkladMaterialViewModel
                        {
                            Id = source.SkladMaterial[j].Id,
                            SkladId = source.SkladMaterial[j].SkladId,
                            MaterialId = source.SkladMaterial[j].MaterialId,
                            MaterialName = materialName,
                            Koll = source.SkladMaterial[j].Koll
                        });
                    }
                }
                result.Add(new SkladViewModel
                {
                    Id = source.Sklad[i].Id,
                    SkladName = source.Sklad[i].SkladName,
                    SkladMaterial = SkladMaterial
                });
            }
            return result;
        }

        public SkladViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Sklad.Count; ++i)
            {
                // требуется дополнительно получить список компонентов на складе и их количество
                List<SkladMaterialViewModel> SkladMaterial = new List<SkladMaterialViewModel>();
                for (int j = 0; j < source.SkladMaterial.Count; ++j)
                {
                    if (source.SkladMaterial[j].SkladId == source.Sklad[i].Id)
                    {
                        string materialName = string.Empty;
                        for (int k = 0; k < source.Material.Count; ++k)
                        {
                            if (source.RemontMaterial[j].MaterialId == source.Material[k].Id)
                            {
                                materialName = source.Material[k].MaterialName;
                                break;
                            }
                        }
                        SkladMaterial.Add(new SkladMaterialViewModel
                        {
                            Id = source.SkladMaterial[j].Id,
                            SkladId = source.SkladMaterial[j].SkladId,
                            MaterialId = source.SkladMaterial[j].MaterialId,
                            MaterialName = materialName,
                            Koll = source.SkladMaterial[j].Koll
                        });
                    }
                }
                if (source.Sklad[i].Id == id)
                {
                    return new SkladViewModel
                    {
                        Id = source.Sklad[i].Id,
                        SkladName = source.Sklad[i].SkladName,
                        SkladMaterial = SkladMaterial
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(SkladBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Sklad.Count; ++i)
            {
                if (source.Sklad[i].Id > maxId)
                {
                    maxId = source.Sklad[i].Id;
                }
                if (source.Sklad[i].SkladName == model.SkladName)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
            }
            source.Sklad.Add(new Sklad
            {
                Id = maxId + 1,
                SkladName = model.SkladName
            });
        }

        public void UpdElement(SkladBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Sklad.Count; ++i)
            {
                if (source.Sklad[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Sklad[i].SkladName == model.SkladName && 
                    source.Sklad[i].Id != model.Id)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Sklad[index].SkladName = model.SkladName;
        }

        public void DelElement(int id)
        {
            // при удалении удаляем все записи о компонентах на удаляемом складе
            for (int i = 0; i < source.SkladMaterial.Count; ++i)
            {
                if (source.SkladMaterial[i].SkladId == id)
                {
                    source.SkladMaterial.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Sklad.Count; ++i)
            {
                if (source.Sklad[i].Id == id)
                {
                    source.Sklad.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
