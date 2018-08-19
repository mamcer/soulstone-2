CREATE TABLE [dbo].[PlaylistSong] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [PlaylistId] INT NOT NULL,
    [SongId]     INT NOT NULL,
    [Position]   INT NOT NULL,
    CONSTRAINT [PK_PlaylistSong] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PlaylistSong_Playlist] FOREIGN KEY ([PlaylistId]) REFERENCES [dbo].[Playlist] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PlaylistSong_Song] FOREIGN KEY ([SongId]) REFERENCES [dbo].[Song] ([Id]) ON DELETE CASCADE
);





