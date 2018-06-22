using AbstractWorkModel;
using AbstractWorkService.BindingModels;
using AbstractWorkService.Interfaces;
using AbstractWorkService.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace AbstractWorkService.ImplementationsBD
{
    public class RemontServiceBD : IRemontService
    {
        private AbstractDbContext context;

        public RemontServiceBD(AbstractDbContext context)
        {
            this.context = context;
        }

        public List<RemontViewModel> GetList()
        {
            List<RemontViewModel> result = context.Remonts
                .Select(rec => new RemontViewModel
                {
                    Id = rec.Id,
                    RemontName = rec.RemontName,
                    Cost = rec.Cost,
                    RemontMaterial = context.RemontMaterials
                            .Where(recPC => recPC.RemontId == rec.Id)
                            .Select(recPC => new RemontMaterialViewModel
                            {
                                Id = recPC.Id,
                                RemontId = recPC.RemontId,
                                MaterialId = recPC.MaterialId,
                                MaterialName = recPC.Material.MaterialName,
                                Koll = recPC.Koll
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public RemontViewModel GetElement(int id)
        {
            Remont element = context.Remonts.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new RemontViewModel
                {
                    Id = element.Id,
                    RemontName = element.RemontName,
                    Cost = element.Cost,
                    RemontMaterial = context.RemontMaterials
                            .Where(recPC => recPC.RemontId == element.Id)
                            .Select(recPC => new RemontMaterialViewModel
                            {
                                Id = recPC.Id,
                                RemontId = recPC.RemontId,
                                MaterialId = recPC.MaterialId,
                                MaterialName = recPC.Material.MaterialName,
                                Koll = recPC.Koll
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(RemontBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Remont element = context.Remonts.FirstOrDefault(rec => rec.RemontName == model.RemontName);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = new Remont
                    {
                        RemontName = model.RemontName,
                        Cost = model.Cost
                    };
                    context.Remonts.Add(element);
                    context.SaveChanges();
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
                        context.RemontMaterials.Add(new RemontMaterial
                        {
                            RemontId = element.Id,
                            MaterialId = groupMaterial.MaterialId,
                            Koll = groupMaterial.Koll
                        });
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void UpdElement(RemontBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Remont element = context.Remonts.FirstOrDefault(rec =>
                                        rec.RemontName == model.RemontName && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = context.Remonts.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.RemontName = model.RemontName;
                    element.Cost = model.Cost;
                    context.SaveChanges();

                    // обновляем существуюущие компоненты
                    var compIds = model.RemontMaterial.Select(rec => rec.MaterialId).Distinct();
                    var updateMaterials = context.RemontMaterials
                                                    .Where(rec => rec.RemontId == model.Id &&
                                                        compIds.Contains(rec.MaterialId));
                    foreach (var updateMaterial in updateMaterials)
                    {
                        updateMaterial.Koll = model.RemontMaterial
                                                        .FirstOrDefault(rec => rec.Id == updateMaterial.Id).Koll;
                    }
                    context.SaveChanges();
                    context.RemontMaterials.RemoveRange(
                                        context.RemontMaterials.Where(rec => rec.RemontId == model.Id &&
                                                                            !compIds.Contains(rec.MaterialId)));
                    context.SaveChanges();
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
                        RemontMaterial elementPC = context.RemontMaterials
                                                .FirstOrDefault(rec => rec.RemontId == model.Id &&
                                                                rec.MaterialId == groupMaterial.MaterialId);
                        if (elementPC != null)
                        {
                            elementPC.Koll += groupMaterial.Koll;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.RemontMaterials.Add(new RemontMaterial
                            {
                                RemontId = model.Id,
                                MaterialId = groupMaterial.MaterialId,
                                Koll = groupMaterial.Koll
                            });
                            context.SaveChanges();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Remont element = context.Remonts.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.RemontMaterials.RemoveRange(
                                            context.RemontMaterials.Where(rec => rec.RemontId == id));
                        context.Remonts.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
