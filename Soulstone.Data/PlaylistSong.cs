//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Soulstone.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class PlaylistSong
    {
        public int Id { get; set; }
        public int PlaylistId { get; set; }
        public int SongId { get; set; }
        public int Position { get; set; }
    
        public virtual Song Song { get; set; }
        public virtual Playlist Playlist { get; set; }
    }
}