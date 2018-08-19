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
    [RoutePrefix("v1/playlists")]
    public class PlaylistController : ApiController
    {
        [Route("")]
        public IEnumerable<PlaylistDto> Get(int hostId, int userId )
        {
            try
            {
                List<PlaylistDto> output;

                using (var unitOfWork = new UnitOfWork())
                {
                    output = unitOfWork.PlaylistRepository.Get(filter: p => p.HostId == hostId && p.UserId == userId)
                                .Select(p => new PlaylistDto 
                                {
                                    Id = p.Id, 
                                    Name = p.Name, 
                                    HostId = hostId, 
                                    UserId = userId, 
                                    PlaylistSongsCount = p.PlaylistSongs.Count()
                                }).ToList();
                }

                return output;
            }
            catch (Exception ex)
            {
                IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Error.Write(ex.Message, ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            } 
        }

        [Route("{id:int}")]
        public PlaylistDto Get(int id)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    var playlist = unitOfWork.PlaylistRepository.Get(id);
                    if (playlist != null)
                    {
                        return new PlaylistDto
                            {
                                Id = playlist.Id,
                                Name = playlist.Name,
                                PlaylistSongsCount = playlist.PlaylistSongs.Count(),
                                Songs =
                                    playlist.PlaylistSongs.Select(
                                        ps =>
                                        new PlaylistSongDto
                                            {
                                                Position = ps.Position,
                                                Song =
                                                    new SongDto
                                                        {
                                                            Album = ps.Song.Album,
                                                            Artist = ps.Song.Artist,
                                                            Bitrate = ps.Song.Bitrate,
                                                            Duration = ps.Song.Duration,
                                                            Genre = ps.Song.Genre,
                                                            Id = ps.Song.Id,
                                                            Title = ps.Song.Title,
                                                            Year = ps.Song.Year ?? 0
                                                        }
                                            }).ToList()
                            };
                    }
                }

                return new PlaylistDto();
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
                    var playlist = unitOfWork.PlaylistRepository.Get(id);
                    unitOfWork.PlaylistRepository.Delete(playlist);
                    unitOfWork.Commit();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Error.Write(ex.Message, ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                                                                            ex.Message));
            }
        }

        [Route("{id:int}")]
        public HttpResponseMessage Put(int id, PlaylistDto modifiedPlaylist)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    var playlist = unitOfWork.PlaylistRepository.Get(id);

                    playlist.Name = modifiedPlaylist.Name;
                    unitOfWork.PlaylistRepository.Update(playlist);
                    unitOfWork.Commit();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Error.Write(ex.Message, ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                                                                            ex.Message));
            }
        }

        [Route("")]
        public HttpResponseMessage Post(PlaylistDto playlist)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    var existingPlaylist = unitOfWork.PlaylistRepository.Get(filter: p => p.Name == playlist.Name && p.HostId == playlist.HostId && p.UserId == playlist.UserId).FirstOrDefault();
                    if (existingPlaylist != null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "A playlist with the same name already exists");
                    }

                    var host = unitOfWork.HostRepository.Get(playlist.HostId);
                    var user = unitOfWork.UserRepository.Get(playlist.UserId);

                    var newPlaylist = new Playlist
                        {
                            Host = host,
                            User = user,
                            Name = playlist.Name
                        };

                    unitOfWork.PlaylistRepository.Save(newPlaylist);
                    unitOfWork.Commit();

                    playlist.Id = newPlaylist.Id;
                    return Request.CreateResponse(HttpStatusCode.Created, playlist);
                }
            }
            catch (Exception ex)
            {
                IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Error.Write(ex.Message, ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                                                                            ex.Message));
            }
        }

        [HttpPost]
        [Route("{id:int}/songs")]
        public HttpResponseMessage AddSong(int id, PlaylistSongDto playlistSongDto)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    var playlist = unitOfWork.PlaylistRepository.Get(id);
                    var song = unitOfWork.SongRepository.Get(playlistSongDto.Song.Id);
                    
                    var playlistSong = new PlaylistSong 
                    { 
                        Playlist = playlist, 
                        Song = song, 
                        Position = playlistSongDto.Position 
                    };

                    unitOfWork.PlaylistSongRepository.Save(playlistSong);
                    unitOfWork.Commit();
                    playlistSongDto.Id = playlistSong.Id;

                    return Request.CreateResponse(HttpStatusCode.Created, playlistSongDto);
                }
            }
            catch (Exception ex)
            {
                IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Error.Write(ex.Message, ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                                                                            ex.Message));
            }
        }

        [HttpPost]
        [Route("{id:int}/allsongs")]
        public HttpResponseMessage AddAllSongs(int id, List<SongDto> songs)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    if (songs == null)
                    {
                        unitOfWork.ExecuteSqlCommand(string.Format("exec AddAllSongs {0}", id));
                    }
                    else
                    {
                        var playlistSongs = unitOfWork.PlaylistSongRepository.Get(filter: ps => ps.PlaylistId == id);
                        int position = 1;
                        if (playlistSongs.Count() > 0)
                        {
                            position = playlistSongs.Max(ps => ps.Position) + 1;
                        }

                        var playlist = unitOfWork.PlaylistRepository.Get(id);
                        foreach (var songDto in songs)
                        {
                            if (!unitOfWork.PlaylistSongRepository.Get(filter: ps => ps.PlaylistId == id && ps.SongId == songDto.Id).Any())
                            {
                                var song = unitOfWork.SongRepository.Get(songDto.Id);
                                
                                var playlistSong = new PlaylistSong 
                                { 
                                    Playlist = playlist, 
                                    Song = song, 
                                    Position = position++ 
                                };

                                unitOfWork.PlaylistSongRepository.Save(playlistSong);
                                unitOfWork.Commit();
                            }
                        }
                    }

                    return Request.CreateResponse(HttpStatusCode.Created);
                }
            }
            catch (Exception ex)
            {
                IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Error.Write(ex.Message, ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                                                                            ex.Message));
            }
        }

        [HttpGet]
        [Route("{id:int}/songs/{songId:int}/next")]
        public HttpResponseMessage NextSong(int id, int songId)
        {
            try
            {
                PlaylistSongDto playlistSongDto = null;

                using (var unitOfWork = new UnitOfWork())
                {
                    var playlistSong = unitOfWork.PlaylistSongRepository.Get(filter: p => p.SongId == songId && p.PlaylistId == id).FirstOrDefault();
                    var nextSong = unitOfWork.PlaylistSongRepository.Get(filter: p => p.PlaylistId == id && p.Position == playlistSong.Position + 1).FirstOrDefault();
                    if (nextSong != null)
                    {
                        playlistSongDto = new PlaylistSongDto
                            {
                                Id = nextSong.Id,
                                Song = new SongDto
                                    {
                                        Id = nextSong.Song.Id,
                                        Album = nextSong.Song.Album,
                                        Artist = nextSong.Song.Artist,
                                        Bitrate = nextSong.Song.Bitrate,
                                        Duration = nextSong.Song.Duration,
                                        Genre = nextSong.Song.Genre,
                                        Title = nextSong.Song.Title,
                                        Year = nextSong.Song.Year ?? 0
                                    },
                                Position = nextSong.Position
                            };
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK, playlistSongDto);
            }
            catch (Exception ex)
            {
                IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Error.Write(ex.Message, ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                                                                            ex.Message));
            }
        }

        [HttpGet]
        [Route("{id:int}/songs/{songId:int}/nextshuffle")]
        public HttpResponseMessage NextShuffleSong(int id, int songId)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    var playlist = unitOfWork.PlaylistRepository.Get(id);
                    var songCount = unitOfWork.PlaylistSongRepository.Count();
                    PlaylistSong nextSong = null;
                    while (nextSong == null)
                    {
                        int nextSongPosition = new Random().Next(0, songCount - 1);
                        nextSong = playlist.PlaylistSongs.ElementAt(nextSongPosition);
                        if (nextSong.Song.Id == songId)
                        {
                            nextSong = null;
                        }
                    }

                    PlaylistSongDto playlistSongDto = null;
                    if (nextSong != null)
                    {
                        playlistSongDto = new PlaylistSongDto
                        {
                            Id = nextSong.Id,
                            Song = new SongDto
                            {
                                Id = nextSong.Song.Id,
                                Album = nextSong.Song.Album,
                                Artist = nextSong.Song.Artist,
                                Bitrate = nextSong.Song.Bitrate,
                                Duration = nextSong.Song.Duration,
                                Genre = nextSong.Song.Genre,
                                Title = nextSong.Song.Title,
                                Year = nextSong.Song.Year ?? 0
                            },
                            Position = nextSong.Position
                        };
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, playlistSongDto);
                }
            }
            catch (Exception ex)
            {
                IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Error.Write(ex.Message, ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                                                                            ex.Message));
            }
        }

        [HttpPut]
        [Route("{id:int}/songs")]
        public HttpResponseMessage UpdateSong(int id, PlaylistSongDto playlistSongDto)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    var playlistSong = unitOfWork.PlaylistSongRepository.Get(playlistSongDto.Id);
                    playlistSong.Position = playlistSongDto.Position;

                    unitOfWork.PlaylistSongRepository.Update(playlistSong);
                    unitOfWork.Commit();

                    return Request.CreateResponse(HttpStatusCode.OK, playlistSongDto);
                }
            }
            catch (Exception ex)
            {
                IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Error.Write(ex.Message, ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                                                                            ex.Message));
            }
        }

        [HttpDelete]
        [Route("{id:int}/songs/{songId}")]
        public HttpResponseMessage DeleteSong(int id, int songId)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    var playlistSong = unitOfWork.PlaylistSongRepository.Get(filter: ps => (ps.SongId == songId && ps.PlaylistId == id)).FirstOrDefault();
                    unitOfWork.PlaylistSongRepository.Delete(playlistSong);
                    unitOfWork.Commit();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Error.Write(ex.Message, ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                                                                            ex.Message));
            }
        }

        [HttpDelete]
        [Route("{id:int}/songs")]
        public HttpResponseMessage DeleteAllSongs(int id)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    var allSongs = unitOfWork.PlaylistSongRepository.Get(filter: ps => ps.PlaylistId == id);
                    foreach (var playlistSong in allSongs)
                    {
                        unitOfWork.PlaylistSongRepository.Delete(playlistSong);
                    }
                    
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