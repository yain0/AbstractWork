using AbstractWorkService.BindingModels;
using AbstractWorkService.Interfaces;
using System;
using System.Web.Http;

namespace AbstractWorkRestApi.Controllers
{
    public class RemontController : ApiController
    {
        private readonly IRemontService _service;

        public RemontController(IRemontService service)
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
        public void AddElement(RemontBindingModel model)
        {
            _service.AddElement(model);
        }

        [HttpPost]
        public void UpdElement(RemontBindingModel model)
        {
            _service.UpdElement(model);
        }

        [HttpPost]
        public void DelElement(RemontBindingModel model)
        {
            _service.DelElement(model.Id);
        }
    }
}
