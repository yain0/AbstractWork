using AbstractWorkModel;
using AbstractWorkService.BindingModels;
using AbstractWorkService.Interfaces;
using AbstractWorkService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractWorkService.ImplementationsBD
{
    public class SkladServiceBD : ISkladService
    {
        private AbstractDbContext context;

        public SkladServiceBD(AbstractDbContext context)
        {
            this.context = context;
        }

        public List<SkladViewModel> GetList()
        {
            List<SkladViewModel> result = context.Sklads
                .Select(rec => new SkladViewModel
                {
                    Id = rec.Id,
                    SkladName = rec.SkladName,
                    SkladMaterial = context.SkladMaterials
                            .Where(recPC => recPC.SkladId == rec.Id)
                            .Select(recPC => new SkladMaterialViewModel
                            {
                                Id = recPC.Id,
                                SkladId = recPC.SkladId,
                                MaterialId = recPC.MaterialId,
                                MaterialName = recPC.Material.MaterialName,
                                Koll = recPC.Koll
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public SkladViewModel GetElement(int id)
        {
            Sklad element = context.Sklads.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new SkladViewModel
                {
                    Id = element.Id,
                    SkladName = element.SkladName,
                    SkladMaterial = context.SkladMaterials
                            .Where(recPC => recPC.SkladId == element.Id)
                            .Select(recPC => new SkladMaterialViewModel
                            {
                                Id = recPC.Id,
                                SkladId = recPC.SkladId,
                                MaterialId = recPC.MaterialId,
                                MaterialName = recPC.Material.MaterialName,
                                Koll = recPC.Koll
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(SkladBindingModel model)
        {
            Sklad element = context.Sklads.FirstOrDefault(rec => rec.SkladName == model.SkladName);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            context.Sklads.Add(new Sklad
            {
                SkladName = model.SkladName
            });
            context.SaveChanges();
        }

        public void UpdElement(SkladBindingModel model)
        {
            Sklad element = context.Sklads.FirstOrDefault(rec =>
                                        rec.SkladName == model.SkladName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            element = context.Sklads.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.SkladName = model.SkladName;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Sklad element = context.Sklads.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        // при удалении удаляем все записи о компонентах на удаляемом складе
                        context.SkladMaterials.RemoveRange(
                                            context.SkladMaterials.Where(rec => rec.SkladId == id));
                        context.Sklads.Remove(element);
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
