using AbstractWorkModel;
using AbstractWorkService.BindingModels;
using AbstractWorkService.Interfaces;
using AbstractWorkService.ViewModels;
using System;
using System.Collections.Generic;

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
            List<ActivityViewModel> result = new List<ActivityViewModel>();
            for (int i = 0; i < source.Activity.Count; ++i)
            {
                string customerFIO = string.Empty;
                for (int j = 0; j < source.Customer.Count; ++j)
                {
                    if(source.Customer[j].Id == source.Activity[i].CustomerId)
                    {
                        customerFIO = source.Customer[j].CustomerFIO;
                        break;
                    }
                }
                string RemontName = string.Empty;
                for (int j = 0; j < source.Remont.Count; ++j)
                {
                    if (source.Remont[j].Id == source.Activity[i].RemontId)
                    {
                        RemontName = source.Remont[j].RemontName;
                        break;
                    }
                }
                string WorkerFIO = string.Empty;
                if(source.Activity[i].WorkerId.HasValue)
                {
                    for (int j = 0; j < source.Worker.Count; ++j)
                    {
                        if (source.Worker[j].Id == source.Activity[i].WorkerId.Value)
                        {
                            WorkerFIO = source.Worker[j].WorkerFIO;
                            break;
                        }
                    }
                }
                result.Add(new ActivityViewModel
                {
                    Id = source.Activity[i].Id,
                    CustomerId = source.Activity[i].CustomerId,
                    CustomerFIO = customerFIO,
                    RemontId = source.Activity[i].RemontId,
                    RemontName = RemontName,
                    WorkerId = source.Activity[i].WorkerId,
                    WorkerName = WorkerFIO,
                    Koll = source.Activity[i].Koll,
                    Summa = source.Activity[i].Summa,
                    DateCreate = source.Activity[i].DateCreate.ToLongDateString(),
                    DateWork = source.Activity[i].DateWork?.ToLongDateString(),
                    Status = source.Activity[i].Status.ToString()
                });
            }
            return result;
        }

        public void CreateActivity(ActivityBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Activity.Count; ++i)
            {
                if (source.Activity[i].Id > maxId)
                {
                    maxId = source.Customer[i].Id;
                }
            }
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
            int index = -1;
            for (int i = 0; i < source.Activity.Count; ++i)
            {
                if (source.Activity[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            // смотрим по количеству компонентов на складах
            for(int i = 0; i < source.RemontMaterial.Count; ++i)
            {
                if(source.RemontMaterial[i].RemontId == source.Activity[index].RemontId)
                {
                    int KollOnSklads = 0;
                    for(int j = 0; j < source.SkladMaterial.Count; ++j)
                    {
                        if(source.SkladMaterial[j].MaterialId == source.RemontMaterial[i].MaterialId)
                        {
                            KollOnSklads += source.SkladMaterial[j].Koll;
                        }
                    }
                    if(KollOnSklads < source.RemontMaterial[i].Koll * source.Activity[index].Koll)
                    {
                        for (int j = 0; j < source.Material.Count; ++j)
                        {
                            if (source.Material[j].Id == source.RemontMaterial[i].MaterialId)
                            {
                                throw new Exception("Не достаточно компонента " + source.Material[j].MaterialName + 
                                    " требуется " + source.RemontMaterial[i].Koll + ", в наличии " + KollOnSklads);
                            }
                        }
                    }
                }
            }
            // списываем
            for (int i = 0; i < source.RemontMaterial.Count; ++i)
            {
                if (source.RemontMaterial[i].RemontId == source.Activity[index].RemontId)
                {
                    int KollOnSklads = source.RemontMaterial[i].Koll * source.Activity[index].Koll;
                    for (int j = 0; j < source.SkladMaterial.Count; ++j)
                    {
                        if (source.SkladMaterial[j].MaterialId == source.RemontMaterial[i].MaterialId)
                        {
                            // компонентов на одном слкаде может не хватать
                            if (source.SkladMaterial[j].Koll >= KollOnSklads)
                            {
                                source.SkladMaterial[j].Koll -= KollOnSklads;
                                break;
                            }
                            else
                            {
                                KollOnSklads -= source.SkladMaterial[j].Koll;
                                source.SkladMaterial[j].Koll = 0;
                            }
                        }
                    }
                }
            }
            source.Activity[index].WorkerId = model.WorkerId;
            source.Activity[index].DateWork = DateTime.Now;
            source.Activity[index].Status = ActivityStatus.Выполняется;
        }

        public void FinishActivity(int id)
        {
            int index = -1;
            for (int i = 0; i < source.Activity.Count; ++i)
            {
                if (source.Customer[i].Id == id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Activity[index].Status = ActivityStatus.Готов;
        }

        public void PayActivity(int id)
        {
            int index = -1;
            for (int i = 0; i < source.Activity.Count; ++i)
            {
                if (source.Customer[i].Id == id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Activity[index].Status = ActivityStatus.Оплачен;
        }

        public void PutMaterialOnSklad(SkladMaterialBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.SkladMaterial.Count; ++i)
            {
                if(source.SkladMaterial[i].SkladId == model.SkladId && 
                    source.SkladMaterial[i].MaterialId == model.MaterialId)
                {
                    source.SkladMaterial[i].Koll += model.Koll;
                    return;
                }
                if (source.SkladMaterial[i].Id > maxId)
                {
                    maxId = source.SkladMaterial[i].Id;
                }
            }
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
