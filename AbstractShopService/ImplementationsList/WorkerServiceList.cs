using AbstractWorkModel;
using AbstractWorkService.BindingModels;
using AbstractWorkService.Interfaces;
using AbstractWorkService.ViewModels;
using System;
using System.Collections.Generic;

namespace AbstractWorkService.ImplementationsList
{
    public class WorkerServiceList : IWorkerService
    {
        private DataListSingleton source;

        public WorkerServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<WorkerViewModel> GetList()
        {
            List<WorkerViewModel> result = new List<WorkerViewModel>();
            for (int i = 0; i < source.Worker.Count; ++i)
            {
                result.Add(new WorkerViewModel
                {
                    Id = source.Worker[i].Id,
                    WorkerFIO = source.Worker[i].WorkerFIO
                });
            }
            return result;
        }

        public WorkerViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Worker.Count; ++i)
            {
                if (source.Worker[i].Id == id)
                {
                    return new WorkerViewModel
                    {
                        Id = source.Worker[i].Id,
                        WorkerFIO = source.Worker[i].WorkerFIO
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(WorkerBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Worker.Count; ++i)
            {
                if (source.Worker[i].Id > maxId)
                {
                    maxId = source.Worker[i].Id;
                }
                if (source.Worker[i].WorkerFIO == model.WorkerFIO)
                {
                    throw new Exception("Уже есть сотрудник с таким ФИО");
                }
            }
            source.Worker.Add(new Worker
            {
                Id = maxId + 1,
                WorkerFIO = model.WorkerFIO
            });
        }

        public void UpdElement(WorkerBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Worker.Count; ++i)
            {
                if (source.Worker[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Worker[i].WorkerFIO == model.WorkerFIO && 
                    source.Worker[i].Id != model.Id)
                {
                    throw new Exception("Уже есть сотрудник с таким ФИО");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Worker[index].WorkerFIO = model.WorkerFIO;
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.Worker.Count; ++i)
            {
                if (source.Worker[i].Id == id)
                {
                    source.Worker.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
