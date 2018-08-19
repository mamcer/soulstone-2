namespace Soulstone.Entities
{
    public class PlayerStatus
    {
        public int SongId { get; set; }

        public int PlaylistId { get; set; }

        public string Title { get; set; }

        public string Album { get; set; }

        public string Artist { get; set; }

        public double Volume { get; set; }

        public bool IsPlaying { get; set; }

        public bool IsShuffleEnabled { get; set; }
    }
}