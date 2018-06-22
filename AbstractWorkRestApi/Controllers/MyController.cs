using AbstractWorkService.BindingModels;
using AbstractWorkService.Interfaces;
using System;
using System.Web.Http;

namespace AbstractWorkRestApi.Controllers
{
    public class MyController : ApiController
    {
        private readonly IMyService _service;

        public MyController(IMyService service)
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

        [HttpPost]
        public void CreateActivity(ActivityBindingModel model)
        {
            _service.CreateActivity(model);
        }

        [HttpPost]
        public void TakeActivityInWork(ActivityBindingModel model)
        {
            _service.TakeActivityInWork(model);
        }

        [HttpPost]
        public void FinishActivity(ActivityBindingModel model)
        {
            _service.FinishActivity(model.Id);
        }

        [HttpPost]
        public void PayActivity(ActivityBindingModel model)
        {
            _service.PayActivity(model.Id);
        }

        [HttpPost]
        public void PutMaterialOnSklad(SkladMaterialBindingModel model)
        {
            _service.PutMaterialOnSklad(model);
        }
    }
}
