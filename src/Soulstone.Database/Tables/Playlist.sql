CREATE TABLE [dbo].[Playlist] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [HostId] INT            NOT NULL,
    [UserId] INT            NOT NULL,
    [Name]   NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_Playlist] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Playlist_Host] FOREIGN KEY ([HostId]) REFERENCES [dbo].[Host] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Playlist_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);









