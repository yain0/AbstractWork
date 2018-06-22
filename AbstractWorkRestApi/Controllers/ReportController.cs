using AbstractWorkService.Interfaces;
using AbstractwWorkService.BindingModels;
using System;
using System.Web.Http;

namespace AbstractWorkRestApi.Controllers
{
    public class ReportController : ApiController
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult GetSkladsLoad()
        {
            var list = _service.GetSkladsLoad();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public IHttpActionResult GetCustomerActivitys(ReportBindingModel model)
        {
            var list = _service.GetCustomerActivitys(model);
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public void SaveRemontCost(ReportBindingModel model)
        {
            _service.SaveRemontCost(model);
        }

        [HttpPost]
        public void SaveSkladsLoad(ReportBindingModel model)
        {
            _service.SaveSkladsLoad(model);
        }

        [HttpPost]
        public void SaveCustomerActivitys(ReportBindingModel model)
        {
            _service.SaveCustomerActivitys(model);
        }
    }
}
