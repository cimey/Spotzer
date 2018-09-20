using Spotzer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace Spotzer.API
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            if (context.Exception is CustomException)
            {

                switch ((context.Exception as CustomException).CustomExceptionType)
                {
                    case CustomExceptionTypeEnum.BadRequest:
                        context.Result = new ErrorWrapper(context.Request, new HttpResponseMessage(HttpStatusCode.BadRequest)
                        {
                            Content = new StringContent(context.Exception.Message),
                            ReasonPhrase = "BadRequest"
                        });
                        break;
                    case CustomExceptionTypeEnum.Unauthorized:
                        context.Result = new ErrorWrapper(context.Request, new HttpResponseMessage(HttpStatusCode.Unauthorized)
                        {
                            Content = new StringContent(context.Exception.Message),
                            ReasonPhrase = "Unauthorized"
                        });
                        break;
                    case CustomExceptionTypeEnum.Forbidden:
                        context.Result = new ErrorWrapper(context.Request, new HttpResponseMessage(HttpStatusCode.Forbidden)
                        {
                            Content = new StringContent(context.Exception.Message),
                            ReasonPhrase = "Forbidden"
                        });
                        break;
                    case CustomExceptionTypeEnum.NotFound:
                        context.Result = new ErrorWrapper(context.Request, new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(context.Exception.Message),
                            ReasonPhrase = "NotFound"
                        });
                        break;
                    case CustomExceptionTypeEnum.Conflict:
                        context.Result = new ErrorWrapper(context.Request, new HttpResponseMessage(HttpStatusCode.Conflict)
                        {
                            Content = new StringContent(context.Exception.Message),
                            ReasonPhrase = "Conflict"
                        });
                        break;
                    case CustomExceptionTypeEnum.InternalServerError:
                        context.Result = new ErrorWrapper(context.Request, new HttpResponseMessage(HttpStatusCode.InternalServerError)
                        {
                            Content = new StringContent("An error occured during request!"),
                            ReasonPhrase = "InternalServerError"
                        });
                        break;
                    default:
                        context.Result = new ErrorWrapper(context.Request, new HttpResponseMessage(HttpStatusCode.InternalServerError)
                        {
                            Content = new StringContent("An error occured during request!"),
                            ReasonPhrase = "InternalServerError"
                        });
                        break;
                }  
            }
            else
            {
                var result = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occured"),
                    ReasonPhrase = "InternalServerError"
                };

                context.Result = new ErrorWrapper(context.Request, result);
            }
            // Handle other exceptions, do other things

        }

        public class ErrorWrapper : IHttpActionResult
        {
            private HttpRequestMessage _request;
            private HttpResponseMessage _httpResponseMessage;


            public ErrorWrapper(HttpRequestMessage request, HttpResponseMessage httpResponseMessage)
            {
                _request = request;
                _httpResponseMessage = httpResponseMessage;
            }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                return Task.FromResult(_httpResponseMessage);
            }
        }
    }
}