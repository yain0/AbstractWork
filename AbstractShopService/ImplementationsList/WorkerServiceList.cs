﻿using AbstractWorkModel;
using AbstractWorkService.BindingModels;
using AbstractWorkService.Interfaces;
using AbstractWorkService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

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
            List<WorkerViewModel> result = source.Worker
               .Select(rec => new WorkerViewModel
               {
                   Id = rec.Id,
                   WorkerFIO = rec.WorkerFIO
               })
                .ToList();
            return result;
        }

        public WorkerViewModel GetElement(int id)
        {
            Worker element = source.Worker.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                    return new WorkerViewModel
                    {
                        Id = element.Id,
                        WorkerFIO = element.WorkerFIO
                    };
                }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(WorkerBindingModel model)
        {
            Worker element = source.Worker.FirstOrDefault(rec => rec.WorkerFIO == model.WorkerFIO);
            if (element != null)
            {
                    throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            int maxId = source.Worker.Count > 0 ? source.Worker.Max(rec => rec.Id) : 0;
            source.Worker.Add(new Worker
            {
                Id = maxId + 1,
                WorkerFIO = model.WorkerFIO
            });
        }

        public void UpdElement(WorkerBindingModel model)
        {
            Worker element = source.Worker.FirstOrDefault(rec =>
rec.WorkerFIO == model.WorkerFIO && rec.Id != model.Id);
            if (element != null)
            {
                    throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            element = source.Worker.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.WorkerFIO = model.WorkerFIO;
        }

        public void DelElement(int id)
        {
            Worker element = source.Worker.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.Worker.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
