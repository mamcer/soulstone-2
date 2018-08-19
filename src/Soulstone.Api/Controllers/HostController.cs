using CrossCutting.Core.Logging;
using CrossCutting.MainModule.IOC;
using Soulstone.Data;
using Soulstone.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Soulstone.Api.Controllers
{
    [RoutePrefix("v1/hosts")]
    public class HostController : ApiController
    {
        [Route("")]
        public IEnumerable<HostDto> Get()
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                {

                    return unitOfWork.HostRepository.GetAll().Select(h => new HostDto { Id = h.Id, Name = h.Name });
                }
            }
            catch (Exception ex)
            {
                IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Error.Write(ex.Message, ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            } 
        }

        [Route("{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    var host = unitOfWork.HostRepository.Get(id);
                    if (host == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Host id not found");
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, new HostDto { Id = host.Id, Name = host.Name });
                }
            }
            catch (Exception ex)
            {
                IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Error.Write(ex.Message, ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [Route("")]
        public HttpResponseMessage Post(HostDto hostDto)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    var host = new Host
                    {
                        Name = hostDto.Name
                    };

                    unitOfWork.HostRepository.Save(host);
                    unitOfWork.Commit();

                    hostDto.Id = host.Id;
                    return Request.CreateResponse(HttpStatusCode.Created, hostDto);
                }
            }
            catch (Exception ex)
            {
                IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Error.Write(ex.Message, ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [Route("{id:int}")]
        public HttpResponseMessage Put(int id, HostDto hostDto)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    var host = unitOfWork.HostRepository.Get(id);

                    if (host == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Host id not found");
                    }

                    host.Name = hostDto.Name;

                    unitOfWork.Commit();

                    return Request.CreateResponse(HttpStatusCode.OK, hostDto);
                }
            }
            catch (Exception ex)
            {
                IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Error.Write(ex.Message, ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [Route("{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    var host = unitOfWork.HostRepository.Get(id);

                    if (host == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Host id not found");
                    }

                    unitOfWork.HostRepository.Delete(host);
                    unitOfWork.Commit();

                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Error.Write(ex.Message, ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }
    }
}