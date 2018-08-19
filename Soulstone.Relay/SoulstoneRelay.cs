using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Soulstone.Entities;

namespace Soulstone.Relay
{
    [HubName("soulstoneHub")]
	public class SoulstoneRelay : Hub
	{
        public void PlayFile(int hostId, int playlistId, int songId)
        {
            Clients.Others.PlaySong(hostId, playlistId, songId);
        }

        public void Play(int hostId)
        {
            Clients.Others.Play(hostId);
        }

        public void Stop(int hostId)
        {
            Clients.Others.Stop(hostId);
        }

        public void Pause(int hostId)
        {
            Clients.Others.Pause(hostId);
        }

        public void Volume(int hostId, int value)
        {
            Clients.Others.Volume(hostId, value);
        }

        public void Mute(int hostId)
        {
            Clients.Others.Mute(hostId);
        }

        public void Shuffle(int hostId)
        {
            Clients.Others.Shuffle(hostId);
        }

        public void NextSong(int hostId)
        {
            Clients.Others.NextSong(hostId);
        }

        public void PreviousSong(int hostId)
        {
            Clients.Others.PreviousSong(hostId);
        }

        public void GetPlayerStatus(int hostId)
        {
            Clients.Others.GetPlayerStatus(hostId);
        }

        public void PlayerStatus(int hostId, PlayerStatus playerStatus)
        {
            Clients.Others.PlayerStatus(hostId, playerStatus);
        }
    }
}