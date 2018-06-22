using AbstractWorkModel;
using AbstractWorkService.BindingModels;
using AbstractWorkService.Interfaces;
using AbstractWorkService.ViewModels;
using System;
using System.Collections.Generic;

namespace AbstractWorkService.ImplementationsList
{
    public class RemontServiceList : IRemontService
    {
        private DataListSingleton source;

        public RemontServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<RemontViewModel> GetList()
        {
            List<RemontViewModel> result = new List<RemontViewModel>();
            for (int i = 0; i < source.Remont.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
                List<RemontMaterialViewModel> RemontMaterial = new List<RemontMaterialViewModel>();
                for (int j = 0; j < source.RemontMaterial.Count; ++j)
                {
                    if (source.RemontMaterial[j].RemontId == source.Remont[i].Id)
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
                        RemontMaterial.Add(new RemontMaterialViewModel
                        {
                            Id = source.RemontMaterial[j].Id,
                            RemontId = source.RemontMaterial[j].RemontId,
                            MaterialId = source.RemontMaterial[j].MaterialId,
                            MaterialName = materialName,
                            Koll = source.RemontMaterial[j].Koll
                        });
                    }
                }
                result.Add(new RemontViewModel
                {
                    Id = source.Remont[i].Id,
                    RemontName = source.Remont[i].RemontName,
                    Cost = source.Remont[i].Cost,
                    RemontMaterial = RemontMaterial
                });
            }
            return result;
        }

        public RemontViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Remont.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
                List<RemontMaterialViewModel> RemontMaterial = new List<RemontMaterialViewModel>();
                for (int j = 0; j < source.RemontMaterial.Count; ++j)
                {
                    if (source.RemontMaterial[j].RemontId == source.Remont[i].Id)
                    {
                        string MaterialName = string.Empty;
                        for (int k = 0; k < source.Material.Count; ++k)
                        {
                            if (source.RemontMaterial[j].MaterialId == source.Material[k].Id)
                            {
                                MaterialName = source.Material[k].MaterialName;
                                break;
                            }
                        }
                        RemontMaterial.Add(new RemontMaterialViewModel
                        {
                            Id = source.RemontMaterial[j].Id,
                            RemontId = source.RemontMaterial[j].RemontId,
                            MaterialId = source.RemontMaterial[j].MaterialId,
                            MaterialName = MaterialName,
                            Koll = source.RemontMaterial[j].Koll
                        });
                    }
                }
                if (source.Remont[i].Id == id)
                {
                    return new RemontViewModel
                    {
                        Id = source.Remont[i].Id,
                        RemontName = source.Remont[i].RemontName,
                        Cost = source.Remont[i].Cost,
                        RemontMaterial = RemontMaterial
                    };
                }
            }

            throw new Exception("Элемент не найден");
        }

        public void AddElement(RemontBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Remont.Count; ++i)
            {
                if (source.Remont[i].Id > maxId)
                {
                    maxId = source.Remont[i].Id;
                }
                if (source.Remont[i].RemontName == model.RemontName)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            source.Remont.Add(new Remont
            {
                Id = maxId + 1,
                RemontName = model.RemontName,
                Cost = model.Cost
            });
            // компоненты для изделия
            int maxPCId = 0;
            for (int i = 0; i < source.RemontMaterial.Count; ++i)
            {
                if (source.RemontMaterial[i].Id > maxPCId)
                {
                    maxPCId = source.RemontMaterial[i].Id;
                }
            }
            // убираем дубли по компонентам
            for (int i = 0; i < model.RemontMaterial.Count; ++i)
            {
                for (int j = 1; j < model.RemontMaterial.Count; ++j)
                {
                    if(model.RemontMaterial[i].MaterialId ==
                        model.RemontMaterial[j].MaterialId)
                    {
                        model.RemontMaterial[i].Koll +=
                            model.RemontMaterial[j].Koll;
                        model.RemontMaterial.RemoveAt(j--);
                    }
                }
            }
            // добавляем компоненты
            for (int i = 0; i < model.RemontMaterial.Count; ++i)
            {
                source.RemontMaterial.Add(new RemontMaterial
                {
                    Id = ++maxPCId,
                    RemontId = maxId + 1,
                    MaterialId = model.RemontMaterial[i].MaterialId,
                    Koll = model.RemontMaterial[i].Koll
                });
            }
        }

        public void UpdElement(RemontBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Remont.Count; ++i)
            {
                if (source.Remont[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Remont[i].RemontName == model.RemontName && 
                    source.Remont[i].Id != model.Id)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Remont[index].RemontName = model.RemontName;
            source.Remont[index].Cost = model.Cost;
            int maxPCId = 0;
            for (int i = 0; i < source.RemontMaterial.Count; ++i)
            {
                if (source.RemontMaterial[i].Id > maxPCId)
                {
                    maxPCId = source.RemontMaterial[i].Id;
                }
            }
            // обновляем существуюущие компоненты
            for (int i = 0; i < source.RemontMaterial.Count; ++i)
            {
                if (source.RemontMaterial[i].RemontId == model.Id)
                {
                    bool flag = true;
                    for (int j = 0; j < model.RemontMaterial.Count; ++j)
                    {
                        // если встретили, то изменяем количество
                        if (source.RemontMaterial[i].Id == model.RemontMaterial[j].Id)
                        {
                            source.RemontMaterial[i].Koll = model.RemontMaterial[j].Koll;
                            flag = false;
                            break;
                        }
                    }
                    // если не встретили, то удаляем
                    if(flag)
                    {
                        source.RemontMaterial.RemoveAt(i--);
                    }
                }
            }
            // новые записи
            for(int i = 0; i < model.RemontMaterial.Count; ++i)
            {
                if(model.RemontMaterial[i].Id == 0)
                {
                    // ищем дубли
                    for (int j = 0; j < source.RemontMaterial.Count; ++j)
                    {
                        if (source.RemontMaterial[j].RemontId == model.Id &&
                            source.RemontMaterial[j].MaterialId == model.RemontMaterial[i].MaterialId)
                        {
                            source.RemontMaterial[j].Koll += model.RemontMaterial[i].Koll;
                            model.RemontMaterial[i].Id = source.RemontMaterial[j].Id;
                            break;
                        }
                    }
                    // если не нашли дубли, то новая запись
                    if (model.RemontMaterial[i].Id == 0)
                    {
                        source.RemontMaterial.Add(new RemontMaterial
                        {
                            Id = ++maxPCId,
                            RemontId = model.Id,
                            MaterialId = model.RemontMaterial[i].MaterialId,
                            Koll = model.RemontMaterial[i].Koll
                        });
                    }
                }
            }
        }

        public void DelElement(int id)
        {
            // удаяем записи по компонентам при удалении изделия
            for (int i = 0; i < source.RemontMaterial.Count; ++i)
            {
                if (source.RemontMaterial[i].RemontId == id)
                {
                    source.RemontMaterial.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Remont.Count; ++i)
            {
                if (source.Remont[i].Id == id)
                {
                    source.Remont.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
