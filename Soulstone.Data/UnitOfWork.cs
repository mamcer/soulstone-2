using System;
using CrossCutting.Core.Data;

namespace Soulstone.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private SoulstoneEntities _context = new SoulstoneEntities();
        private BaseRepository<Host> _hostRepository;
        private BaseRepository<Playlist> _playlistRepository;
        private BaseRepository<PlaylistSong> _playlistSongRepository;
        private BaseRepository<Song> _songRepository;
        private BaseRepository<User> _userRepository;
        private bool disposed = false;

        public BaseRepository<Host> HostRepository
        {
            get
            {

                if (_hostRepository == null)
                {
                    _hostRepository = new BaseRepository<Host>(_context);
                }
                return _hostRepository;
            }
        }

        public BaseRepository<Playlist> PlaylistRepository
        {
            get
            {

                if (_playlistRepository == null)
                {
                    _playlistRepository = new BaseRepository<Playlist>(_context);
                }
                return _playlistRepository;
            }
        }

        public BaseRepository<PlaylistSong> PlaylistSongRepository
        {
            get
            {

                if (_playlistSongRepository == null)
                {
                    _playlistSongRepository = new BaseRepository<PlaylistSong>(_context);
                }
                return _playlistSongRepository;
            }
        }

        public BaseRepository<Song> SongRepository
        {
            get
            {

                if (_songRepository == null)
                {
                    _songRepository = new BaseRepository<Song>(_context);
                }
                return _songRepository;
            }
        }

        public BaseRepository<User> UserRepository
        {
            get
            {

                if (_userRepository == null)
                {
                    _userRepository = new BaseRepository<User>(_context);
                }
                return _userRepository;
            }
        }        
        public void Commit()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Rollback()
        {
        }

        public void RegisterDirty(object entity)
        {
        }

        public void ExecuteSqlCommand(string command)
        {
            _context.Database.ExecuteSqlCommand(command);
        }
    }
}