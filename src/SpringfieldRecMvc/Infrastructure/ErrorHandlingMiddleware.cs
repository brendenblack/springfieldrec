﻿//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace SpringfieldRecMvc.Infrastructure
//{
//    public class ErrorHandlingMiddleware
//    {
//        private readonly RequestDelegate next;

//        public ErrorHandlingMiddleware(RequestDelegate next)
//        {
//            this.next = next;
//        }

//        public async Task Invoke(HttpContext context)
//        {
//            try
//            {
//                await next(context);
//            }
//            catch (Exception ex)
//            {
//                await HandleExceptionAsync(context, ex);
//            }
//        }

//        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
//        {
//            if (exception is HttpException e)
//            {

//                context.Response.StatusCode = (int)e.Code;
//            }
//            else
//            {
//                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
//                if (!string.IsNullOrWhiteSpace(exception.Message))
//                {
//                    context.Response.ContentType = "application/json";
//                    var result = JsonConvert.SerializeObject(new
//                    {
//                        errors = exception.Message
//                    });
//                    await context.Response.WriteAsync(result);
//                }
//            }

//        }
//    }
//}
