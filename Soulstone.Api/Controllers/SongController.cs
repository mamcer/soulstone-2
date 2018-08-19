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
    [RoutePrefix("v1/songs")]
    public class SongController : ApiController
    {
        [Route("")]
        public IEnumerable<SongDto> GetPaged(int page, int pageSize)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    var result = unitOfWork.SongRepository.Get().Select(s => new SongDto
                    {
                        Id = s.Id,
                        Album = s.Album,
                        Artist = s.Artist,
                        Bitrate = s.Bitrate,
                        Duration = s.Duration,
                        Genre = s.Genre,
                        Title = s.Title,
                        Year = s.Year.HasValue ? s.Year.Value : 0
                    }).OrderBy(s => s.Artist).Skip(page * pageSize).Take(pageSize);

                    return result.ToList();
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
                    var result = unitOfWork.SongRepository.Get(id);

                    var songDto = new SongDto
                    {
                        Id = result.Id,
                        Album = result.Album,
                        Artist = result.Artist,
                        Bitrate = result.Bitrate,
                        Duration = result.Duration,
                        Genre = result.Genre,
                        Title = result.Title,
                        Year = result.Year.HasValue ? result.Year.Value : 0
                    };

                    return Request.CreateResponse(HttpStatusCode.OK, songDto);
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