using System.Collections.Generic;

namespace Soulstone.Entities
{
    public class PlaylistDto
    {
        public PlaylistDto()
        {
            Songs = new List<PlaylistSongDto>();
        }

        public int Id { get; set; }

        public int HostId { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public List<PlaylistSongDto> Songs { get; set; }

        public int PlaylistSongsCount { get; set; }
    }
}