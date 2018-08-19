
CREATE PROCEDURE [dbo].[AddAllSongs]
	@playlistId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @position INT
	SELECT @position = MAX(Position) 
	FROM PlaylistSong 
	WHERE PlaylistId = @playlistId

	IF(@position IS NULL)
	BEGIN
		SET @position = 0
	END

	INSERT INTO PlaylistSong (PlaylistId, SongId, Position) 
	SELECT @playlistId, Id, @position + ROW_NUMBER() OVER (order by id)
	FROM Song AS S
	WHERE NOT EXISTS (SELECT Id FROM PlaylistSong AS PS WHERE PS.PlaylistId = @playlistId AND PS.SongId = S.Id)
END