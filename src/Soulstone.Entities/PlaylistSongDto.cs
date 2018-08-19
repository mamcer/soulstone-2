namespace Soulstone.Entities
{
    public class PlaylistSongDto
    {
        public int Id { get; set; }

        public SongDto Song { get; set; }

        public int Position { get; set; }
    }
}