using System;

namespace Soulstone.Entities
{
    public class SongDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Album { get; set; }

        public string Artist { get; set; }

        public int Year { get; set; }

        public string Genre { get; set; }

        public TimeSpan Duration { get; set; }

        public int Bitrate { get; set; }
    }
}
