CREATE TABLE [dbo].[Song] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Title]    NVARCHAR (255) NOT NULL,
    [Album]    NVARCHAR (255) NOT NULL,
    [Artist]   NVARCHAR (255) NOT NULL,
    [Year]     INT            NULL,
    [Genre]    NVARCHAR (255) NULL,
    [Duration] TIME (7)       NOT NULL,
    [Artwork]  IMAGE          NULL,
    [Bitrate]  INT            NOT NULL,
    [Hash]     NVARCHAR (255) NOT NULL,
    [FileName] NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK_Song] PRIMARY KEY CLUSTERED ([Id] ASC)
);







