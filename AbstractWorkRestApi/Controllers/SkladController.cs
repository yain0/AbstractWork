﻿using AbstractWorkService.BindingModels;
using AbstractWorkService.Interfaces;
using System;
using System.Web.Http;

namespace AbstractWorkRestApi.Controllers
{
    public class SkladController : ApiController
    {
        private readonly ISkladService _service;

        public SkladController(ISkladService service)
        {
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult GetList()
        {
            var list = _service.GetList();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var element = _service.GetElement(id);
            if (element == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(element);
        }

        [HttpPost]
        public void AddElement(SkladBindingModel model)
        {
            _service.AddElement(model);
        }

        [HttpPost]
        public void UpdElement(SkladBindingModel model)
        {
            _service.UpdElement(model);
        }

        [HttpPost]
        public void DelElement(SkladBindingModel model)
        {
            _service.DelElement(model.Id);
        }
    }
}
