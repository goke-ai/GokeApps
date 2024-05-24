﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using static Microsoft.AspNetCore.Mvc.OkObjectResult;

namespace Microsoft.AspNetCore.Mvc
{
    namespace Infrastructure
    {
        public interface IConvertToActionResult : IActionResult
        {
            public IActionResult Convert();
        }

        public interface IStatusCodeActionResult : IActionResult
        {

        }
        public interface IClientErrorActionResult
        {
        }
    }

    public interface IActionResult
    {
    }

    public class ActionResult : IActionResult
    {
    }

    public class ActionResult<T> : IConvertToActionResult
    {
        public IActionResult Convert()
        {
            throw new System.NotImplementedException();
        }

        public static implicit operator ActionResult<T>(T value)
        {
            throw new NotImplementedException();
        }
        public static implicit operator ActionResult<T>(ActionResult result)
        {
            throw new NotImplementedException();
        }
    }


    public class StatusCodeResult : ActionResult, IActionResult, IClientErrorActionResult
    {
    }

    public class OkObjectResult : ObjectResult
    {
    }
    public class BadRequestResult : StatusCodeResult
    {
    }
    public class NotFoundResult : StatusCodeResult
    {
    }
    public class NoContentResult : StatusCodeResult
    {
    }
    public class ObjectResult : ActionResult, IStatusCodeActionResult
    { 
    }
    public class CreatedAtActionResult : ObjectResult
    {
    }
    public abstract class FileResult : ActionResult
    {
    }

    }