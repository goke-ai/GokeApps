#nullable enable

using System.Collections.Generic;
using System;
using System.Security.Claims;

namespace Microsoft.AspNetCore.Mvc
{


    public class ControllerBase
    {
        public virtual VirtualFileResult File(string virtualPath, string contentType, string? fileDownloadName)
        {
            throw new NotImplementedException();
        }

        public ClaimsPrincipal? User { get; }

        public virtual OkObjectResult Ok(object? v)
        {
            throw new NotImplementedException();
        }
        public virtual BadRequestResult BadRequest()
        {
            throw new NotImplementedException();
        }
        public virtual NotFoundResult NotFound()
        {
            throw new NotImplementedException();
        }
        public virtual NoContentResult NoContent()
        {
            throw new NotImplementedException();
        }
        public virtual ObjectResult Problem(
            string? detail = null,
            string? instance = null,
            int? statusCode = null,
            string? title = null,
            string? type = null)
        {
            throw new NotImplementedException();
        }

        public virtual CreatedAtActionResult CreatedAtAction(string? actionName, 
            object? routeValues, /*[ActionResultObjectValue]*/ object? value)
        {
            throw new NotImplementedException();
        }


    }
}