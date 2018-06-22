using AbstractWorkModel;
using AbstractWorkService.BindingModels;
using AbstractWorkService.Interfaces;
using AbstractWorkService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

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
            List<RemontViewModel> result = source.Remont
                .Select(rec => new RemontViewModel
                {
                    Id = rec.Id,
                    RemontName = rec.RemontName,
                    Cost = rec.Cost,
                    RemontMaterial = source.RemontMaterial
                            .Where(recPC => recPC.RemontId == rec.Id)
                            .Select(recPC => new RemontMaterialViewModel
                            {
                                Id = recPC.Id,
                                RemontId = recPC.RemontId,
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

        public RemontViewModel GetElement(int id)
        {
            Remont element = source.Remont.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new RemontViewModel
                {
                    Id = element.Id,
                    RemontName = element.RemontName,
                    Cost = element.Cost,
                    RemontMaterial = source.RemontMaterial
                            .Where(recPC => recPC.RemontId == element.Id)
                            .Select(recPC => new RemontMaterialViewModel
                            {
                                Id = recPC.Id,
                                RemontId = recPC.RemontId,
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

        public void AddElement(RemontBindingModel model)
        {
            Remont element = source.Remont.FirstOrDefault(rec => rec.RemontName == model.RemontName);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            int maxId = source.Remont.Count > 0 ? source.Remont.Max(rec => rec.Id) : 0;
            source.Remont.Add(new Remont
            {
                Id = maxId + 1,
                RemontName = model.RemontName,
                Cost = model.Cost
            });
            // компоненты для изделия
            int maxPCId = source.RemontMaterial.Count > 0 ?
source.RemontMaterial.Max(rec => rec.Id) : 0;
            // убираем дубли по компонентам
            var groupMaterials = model.RemontMaterial
                                        .GroupBy(rec => rec.MaterialId)
                                        .Select(rec => new
                                        {
                                            MaterialId = rec.Key,
                                            Koll = rec.Sum(r => r.Koll)
                                        });
            // добавляем компоненты
            foreach (var groupMaterial in groupMaterials)
            {
                source.RemontMaterial.Add(new RemontMaterial
                {
                    Id = ++maxPCId,
                    RemontId = maxId + 1,
                    MaterialId = groupMaterial.MaterialId,
                    Koll = groupMaterial.Koll
                });
            }
        }

        public void UpdElement(RemontBindingModel model)
        {
            Remont element = source.Remont.FirstOrDefault(rec =>
rec.RemontName == model.RemontName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            element = source.Remont.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.RemontName = model.RemontName;
            element.Cost = model.Cost;

            int maxPCId = source.RemontMaterial.Count > 0 ? source.RemontMaterial.Max(rec => rec.Id) : 0;
            // обновляем существуюущие компоненты
            var compIds = model.RemontMaterial.Select(rec => rec.MaterialId).Distinct();
            var updateMaterials = source.RemontMaterial
                                            .Where(rec => rec.RemontId == model.Id &&
compIds.Contains(rec.MaterialId));
            foreach (var updateMaterial in updateMaterials)
            {
                updateMaterial.Koll = model.RemontMaterial
                                                 .FirstOrDefault(rec => rec.Id == updateMaterial.Id).Koll;
            }
            // новые записи
            var groupMaterials = model.RemontMaterial
                                        .Where(rec => rec.Id == 0)
                                        .GroupBy(rec => rec.MaterialId)
                                        .Select(rec => new
                                        {
                                            MaterialId = rec.Key,
                                            Koll = rec.Sum(r => r.Koll)
                                        });
            foreach (var groupMaterial in groupMaterials)
            {
                RemontMaterial elementPC = source.RemontMaterial
                                        .FirstOrDefault(rec => rec.RemontId == model.Id &&
rec.MaterialId == groupMaterial.MaterialId);
                if (elementPC != null)
                {
                    elementPC.Koll += groupMaterial.Koll;
                }
                else
                {
                    source.RemontMaterial.Add(new RemontMaterial
                    {
                        Id = ++maxPCId,
                        RemontId = model.Id,
                        MaterialId = groupMaterial.MaterialId,
                        Koll = groupMaterial.Koll
                    });
                }
            }
        }


        public void DelElement(int id)
        {
            Remont element = source.Remont.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                // удаяем записи по компонентам при удалении изделия
                source.RemontMaterial.RemoveAll(rec => rec.RemontId == id);
                source.Remont.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }

        }
    }
}
