using AbstractWorkModel;
using AbstractWorkService.BindingModels;
using AbstractWorkService.Interfaces;
using AbstractWorkService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractWorkService.ImplementationsList
{
    public class MyServiceList : IMyService
    {
        private DataListSingleton source;

        public MyServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<ActivityViewModel> GetList()
        {
            List<ActivityViewModel> result = source.Activity
                .Select(rec => new ActivityViewModel
                {
                    Id = rec.Id,
                    CustomerId = rec.CustomerId,
                    RemontId = rec.RemontId,
                    WorkerId = rec.WorkerId,
                    DateCreate = rec.DateCreate.ToLongDateString(),
                    DateWork = rec.DateWork?.ToLongDateString(),
                    Status = rec.Status.ToString(),
                    Koll = rec.Koll,
                    Summa = rec.Summa,
                    CustomerFIO = source.Customer
                                    .FirstOrDefault(recC => recC.Id == rec.CustomerId)?.CustomerFIO,
                    RemontName = source.Remont
                                    .FirstOrDefault(recP => recP.Id == rec.RemontId)?.RemontName,
                    WorkerName = source.Worker
                                    .FirstOrDefault(recI => recI.Id == rec.WorkerId)?.WorkerFIO
                })
                .ToList();
            return result;
        }

        public void CreateActivity(ActivityBindingModel model)
        {
            int maxId = source.Activity.Count > 0 ? source.Activity.Max(rec => rec.Id) : 0;
            source.Activity.Add(new Activity
            {
                Id = maxId + 1,
                CustomerId = model.CustomerId,
                RemontId = model.RemontId,
                DateCreate = DateTime.Now,
                Koll = model.Koll,
                Summa = model.Summa,
                Status = ActivityStatus.Принят
            });
        }

        public void TakeActivityInWork(ActivityBindingModel model)
        {
            Activity element = source.Activity.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            // смотрим по количеству компонентов на складах
            var remontMaterials = source.RemontMaterial.Where(rec => rec.RemontId == element.RemontId);
            foreach (var remontMaterial in remontMaterials)
            {
                int countOnSklads = source.SkladMaterial
                                            .Where(rec => rec.MaterialId == remontMaterial.MaterialId)
                                            .Sum(rec => rec.Koll);
                if (countOnSklads < remontMaterial.Koll * element.Koll)
                {
                    var materialName = source.Material
                                     .FirstOrDefault(rec => rec.Id == remontMaterial.MaterialId);
                    throw new Exception("Не достаточно компонента " + materialName?.MaterialName +
" требуется " + remontMaterial.Koll + ", в наличии " + countOnSklads);
                }
            }
            // списываем
            foreach (var remontMaterial in remontMaterials)
            {
                int countOnSklads = remontMaterial.Koll * element.Koll;
                var skladMaterials = source.SkladMaterial
                                            .Where(rec => rec.MaterialId == remontMaterial.MaterialId);
                foreach (var skladMaterial in skladMaterials)
                {
                    // компонентов на одном слкаде может не хватать
                    if (skladMaterial.Koll >= countOnSklads)
                    {
                        skladMaterial.Koll -= countOnSklads;
                        break;
                    }
                    else
                    {
                        countOnSklads -= skladMaterial.Koll;
                        skladMaterial.Koll = 0;
                    }
                }
            }
            element.WorkerId = model.WorkerId;
            element.DateWork = DateTime.Now;
            element.Status = ActivityStatus.Выполняется;
        }

        public void FinishActivity(int id)
        {
            Activity element = source.Activity.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = ActivityStatus.Готов;
        }

        public void PayActivity(int id)
        {
            Activity element = source.Activity.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = ActivityStatus.Оплачен;
        }

        public void PutMaterialOnSklad(SkladMaterialBindingModel model)
        {
            SkladMaterial element = source.SkladMaterial
                                                .FirstOrDefault(rec => rec.SkladId == model.SkladId &&
rec.MaterialId == model.MaterialId);
            if (element != null)
            {
                element.Koll += model.Koll;
            }
            else
            {
                int maxId = source.SkladMaterial.Count > 0 ? source.SkladMaterial.Max(rec => rec.Id) : 0;
                source.SkladMaterial.Add(new SkladMaterial
                {
                    Id = ++maxId,
                    SkladId = model.SkladId,
                    MaterialId = model.MaterialId,
                    Koll = model.Koll
                });
            }
        }
    }
}
