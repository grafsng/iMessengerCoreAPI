using iMessengerCoreAPI.DataContract;
using iMessengerCoreAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace iMessengerCoreAPI.Controllers
{
    [ApiController]
    [Route("api/v1/RGDialogs")]
    public class RGDialogsController: ControllerBase
    {
        private IRGDialogsService _service;

        public RGDialogsController(IRGDialogsService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("")]
        public IActionResult GetDialogId(Sdk_ClientIdsList li)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddDebug();
            });
            ILogger logger = loggerFactory.CreateLogger<RGDialogsController>();

            try
            {
                if (!li.ClientIds.Any())
                {
                    throw new BadHttpRequestException("Parameter value is empty!");
                }
                var result = _service.FindDialog(li.ClientIds);

                return Ok(result);
            }
            catch(Exception ex) 
            {
                logger.LogError(ex.Message);

                var code = ex switch
                {
                    BadHttpRequestException => HttpStatusCode.BadRequest,
                    _ => HttpStatusCode.InternalServerError
                };

                ApiResult apiResult = new ApiResult() { Code = (int)code, Message = ex.Message};

                return new ObjectResult(apiResult);
            }
        }
    }
}
