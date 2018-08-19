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
    [RoutePrefix("v1/users")]
    public class UserController : ApiController
    {
        [Route("")]
        public IEnumerable<UserDto> Get()
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    return unitOfWork.UserRepository.Get().Select(user => new UserDto { Id = user.Id, NickName = user.NickName });
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
                    var user = unitOfWork.UserRepository.Get(id);
                    if (user == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "User Id not Found");
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, new UserDto
                    {
                        Id = user.Id,
                        NickName = user.NickName
                    });
                }
            }
            catch (Exception ex)
            {
                IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Error.Write(ex.Message, ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [Route("")]
        public HttpResponseMessage Post(UserDto newUser)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    var user = new User
                    {
                        NickName = newUser.NickName
                    };

                    unitOfWork.UserRepository.Save(user);
                    unitOfWork.Commit();
                    newUser.Id = user.Id;

                    return Request.CreateResponse(HttpStatusCode.Created, newUser);
                }
            }
            catch (Exception ex)
            {
                IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Error.Write(ex.Message, ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [Route("{id:int}")]
        public HttpResponseMessage Put(int id, [FromBody]UserDto modifiedUser)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    var user = unitOfWork.UserRepository.Get(id);
                    if (user == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "User Id not Found");
                    }

                    user.NickName = modifiedUser.NickName;
                    unitOfWork.UserRepository.Update(user);
                    unitOfWork.Commit();

                    return Request.CreateResponse(HttpStatusCode.OK);
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
                    var user = unitOfWork.UserRepository.Get(id);
                    if (user == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "User Id not Found");
                    }

                    unitOfWork.UserRepository.Delete(user);
                    unitOfWork.Commit();

                    return Request.CreateResponse(HttpStatusCode.OK);
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