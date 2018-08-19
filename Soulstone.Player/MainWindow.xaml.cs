using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;
using CrossCutting.Core.Logging;
using CrossCutting.MainModule.IOC;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using Soulstone.Data;
using Soulstone.Entities;
using ConnectionState = Microsoft.AspNet.SignalR.Client.ConnectionState;

namespace Soulstone.Player
{
    public partial class MainWindow
    {
        private IHubProxy _soulstoneHub;

        private HubConnection _hubConnection;

        private double _muteVolume;

        private int HostId { get; set; }

        private PlayerStatus _playerStatus;

        private Song NextSong { get; set; }

        private string SoulstoneRelayUrl { get; set; }

        private string SoulstoneApiUrl { get; set; }
        
        private string SoulstonePath { get; set; }

        public MainWindow()
        {
            _playerStatus = new PlayerStatus();

            InitializeComponent();

            CheckConfigurationInformation();

            HostId = Convert.ToInt32(ConfigurationManager.AppSettings["HostId"]);
            SoulstoneRelayUrl = ConfigurationManager.AppSettings["SoulstoneRelayUrl"];
            SoulstonePath = ConfigurationManager.AppSettings["SoulstonePath"];
            SoulstoneApiUrl = ConfigurationManager.AppSettings["SoulstoneApiUrl"];

            SetWindowTitle();

            Player.LoadedBehavior = MediaState.Manual;
            Player.MediaEnded += PlayerOnMediaEnded;

            InitializeSignalR(SoulstoneRelayUrl);
        }

        private async void SetWindowTitle()
        {
            using (var client = new HttpClient())
            { 
                InitializeClient(client);
                var response = await client.GetAsync(string.Format("{0}hosts/{1}", SoulstoneApiUrl, HostId));
                if (response.IsSuccessStatusCode)
                {
                    var host = await response.Content.ReadAsAsync<HostDto>();
                    Title = string.Format("Soulstone Player - {0}", host.Name);
                    ConsoleLog("Hostname: " + host.Name);
                }
            }
        }

        private void CheckConfigurationInformation()
        {
            CheckConfigurationKey("HostId");

            CheckConfigurationKey("SoulstoneRelayUrl");

            CheckConfigurationKey("SoulstonePath");

            CheckConfigurationKey("SoulstoneApiUrl");
        }

        private void CheckConfigurationKey(string configurationKey)
        {
            if (ConfigurationManager.AppSettings[configurationKey] == null)
            {
                string message = string.Format("Missing {0} configuration key", configurationKey);
                IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Error.Write(message);
                MessageBox.Show(message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        private void PlayerOnMediaEnded(object sender, RoutedEventArgs routedEventArgs)
        {
            _playerStatus.IsPlaying = false;
            if (NextSong != null)
            {
                PlayNextFile();
            }

            LookForNextSong();
        }

        private void PlayNextFile()
        {
            SetPlayerStatusInfo(NextSong);

            PlayFile(NextSong.FileName);
        }

        private async void InitializeSignalR(string url)
        {
            if (_hubConnection != null)
            {
                _hubConnection.Stop();
            }

            _hubConnection = new HubConnection(url);
            _soulstoneHub = _hubConnection.CreateHubProxy("soulstoneHub");

            _soulstoneHub.On<int, int, int>("PlaySong", (hostId, playlistId, songId) =>
            {
                try
                {
                    if (hostId == HostId)
                    {
                        var entities = new SoulstoneEntities();
                        var song = entities.Songs.Find(songId);
                        SetPlayerStatusInfo(song);
                        _playerStatus.PlaylistId = playlistId;
                        _playerStatus.IsPlaying = true;
                        if (song != null)
                        {
                            PlayFile(song.FileName);

                            LookForNextSong();
                        }
                    }
                }
                catch (Exception ex)
                {
                    IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Error.Write(ex.Message, ex);
                    ConsoleLog("An error has occurred: " + ex.Message);
                }
            });

            _soulstoneHub.On<int>("Play", hostId =>
            {
                try
                {
                    if (hostId == HostId)
                    {
                        ConsoleLog("Play");
                        Player.Dispatcher.BeginInvoke(new Action(() => Player.Play()));
                        _playerStatus.IsPlaying = true;
                        _soulstoneHub.Invoke("PlayerStatus", HostId, _playerStatus);
                    }
                }
                catch (Exception ex)
                {
                    IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Error.Write(ex.Message, ex);
                    ConsoleLog("An error has occurred: " + ex.Message);
                }
            });

            _soulstoneHub.On<int>("Stop", hostId =>
            {
                try
                {
                    if (hostId == HostId)
                    {
                        Player.Dispatcher.BeginInvoke(new Action(() => Player.Stop()));
                        ConsoleLog("Stop");
                        _playerStatus.IsPlaying = false;
                        _soulstoneHub.Invoke("PlayerStatus", HostId, _playerStatus);
                    }
                }
                catch (Exception ex)
                {
                    IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Error.Write(ex.Message, ex);
                    ConsoleLog("An error has occurred: " + ex.Message);
                }
            });

            _soulstoneHub.On<int>("Pause", hostId =>
            {
                try
                {
                    if (hostId == HostId)
                    {
                        Player.Dispatcher.BeginInvoke(new Action(() => Player.Pause()));
                        _playerStatus.IsPlaying = false;
                        ConsoleLog("Pause");
                        _soulstoneHub.Invoke("PlayerStatus", HostId, _playerStatus);
                    }
                }
                catch (Exception ex)
                {
                    IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Error.Write(ex.Message, ex);
                    ConsoleLog("An error has occurred: " + ex.Message);
                }
            });

            _soulstoneHub.On<int, int>("Volume", (hostId, volumeValue) =>
            {
                try
                {
                    if (hostId == HostId)
                    {
                        Player.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                UpdateVolume(volumeValue*0.1);
                            }));
                    }
                }
                catch (Exception ex)
                {
                    IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Error.Write(ex.Message, ex);
                    ConsoleLog("An error has occurred: " + ex.Message);
                }
            });

            _soulstoneHub.On<int>("Mute", hostId =>
            {
                try
                {
                    if (hostId == HostId)
                    {
                        Player.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                ToogleMute();
                            }));
                    }
                }
                catch (Exception ex)
                {
                    IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Error.Write(ex.Message, ex);
                    ConsoleLog("An error has occurred: " + ex.Message);
                }
            });

            _soulstoneHub.On<int>("GetPlayerStatus", hostId =>
            {
                try
                {
                    if (hostId == HostId)
                    {
                        _soulstoneHub.Invoke("PlayerStatus", hostId, _playerStatus);
                    }
                }
                catch (Exception ex)
                {
                    IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Error.Write(ex.Message, ex);
                    ConsoleLog("An error has occurred: " + ex.Message);
                }
            });


            _soulstoneHub.On<int>("Shuffle", hostId =>
            {
                try
                {
                    if (hostId == HostId)
                    {
                        Player.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            _playerStatus.IsShuffleEnabled = !_playerStatus.IsShuffleEnabled;
                            ConsoleLog(string.Format("Shuffle:{0}", _playerStatus.IsShuffleEnabled));
                            LookForNextSong();
                        }));
                    }
                }
                catch (Exception ex)
                {
                    IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Error.Write(ex.Message, ex);
                    ConsoleLog("An error has occurred: " + ex.Message);
                }
            });

            _soulstoneHub.On<int>("NextSong", hostId =>
            {
                try
                {
                    if (hostId == HostId)
                    {
                        Player.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            PlayNextFile();
                            LookForNextSong();
                        }));
                    }
                }
                catch (Exception ex)
                {
                    IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Error.Write(ex.Message, ex);
                    ConsoleLog("An error has occurred: " + ex.Message);
                }
            });

            try
            {
                await _hubConnection.Start();
            }
            catch
            {
                ConsoleLog("SignalR connection could not be established");
                throw;
            }

            ConsoleLog("SignalR connection established");
        }

        private void ToogleMute()
        {
            if (Player.Volume < 0.1)
            {
                UpdateVolume(_muteVolume);
                ConsoleLog("Unmute");
            }
            else
            {
                _muteVolume = Player.Volume;
                UpdateVolume(0);
                ConsoleLog("Mute");
            }
        }

        private void SetPlayerStatusInfo(Song song)
        {
            _playerStatus.IsPlaying = true;
            _playerStatus.Artist = song.Artist;
            _playerStatus.Album = song.Album;
            _playerStatus.SongId = song.Id;
            _playerStatus.Title = song.Title;
            Player.Dispatcher.BeginInvoke(
                new Action(
                    () =>
                    {
                        _playerStatus.Volume = Player.Volume;
                    }
                    ));
        }

        private void PlayFile(string fileName)
        {
            Player.Dispatcher.BeginInvoke(new Action(() =>
            {
                Uri uri = new Uri(Path.Combine(SoulstonePath, fileName), UriKind.Absolute);
                Player.Source = uri;
                Player.Stop();
                Player.Play();
                _playerStatus.IsPlaying = true;
                lblSongName.Content = string.Format("{0} - {1} - {2}", _playerStatus.Artist, _playerStatus.Album, _playerStatus.Title);
                ConsoleLog("Playing " + fileName);
            }));

            _soulstoneHub.Invoke("PlayerStatus", HostId, _playerStatus);

            _playerStatus.IsPlaying = true;
        }

        private void ConsoleLog(string message)
        {
            Player.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                txtConsole.Text += DateTime.Now.ToString("yyyy.dd.MM-hh:mm:ss") + " - " + message + Environment.NewLine;
                                txtConsole.ScrollToEnd();
                            }));
        }

        private void InitializeClient(HttpClient client)
        {
            client.BaseAddress = new Uri(SoulstoneApiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async void LookForNextSong()
        {
            string uri;
            if (_playerStatus.IsShuffleEnabled)
            {
                uri = string.Format("playlists/{0}/songs/{1}/nextshuffle", _playerStatus.PlaylistId, _playerStatus.SongId);
            }
            else
            {
                uri = string.Format("playlists/{0}/songs/{1}/next", _playerStatus.PlaylistId, _playerStatus.SongId);
            }

            using (var client = new HttpClient())
            {
                InitializeClient(client);
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var playlistSongDto = await response.Content.ReadAsAsync<PlaylistSongDto>();

                    if (playlistSongDto != null)
                    {
                        var entities = new SoulstoneEntities();
                        var song = entities.Songs.Find(playlistSongDto.Song.Id);
                        NextSong = song;
                        var message = string.Format("Next song file name found: {0}, Next song id: {1}", NextSong.FileName, NextSong.Id);
                        IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Information.Write(message);
                        ConsoleLog(message);

                        if (!_playerStatus.IsPlaying)
                        {
                            message = "Play Next file since there is no playing in progress";
                            IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Information.Write(message);
                            ConsoleLog(message);
                            PlayNextFile();
                        }

                        return;
                    }
                }

                NextSong = null;
                var msg = "There are no other songs in the playlist";
                ConsoleLog(msg);
                IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Information.Write(msg);
            }     
        }

        private void btnPlay_click(object sender, RoutedEventArgs e)
        {
            Player.Play();
            _playerStatus.IsPlaying = true;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            Player.Stop();
            _playerStatus.IsPlaying = false;
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            Player.Pause();
        }

        private void UpdateVolume(double value)
        {
            Player.Volume = value;
            _playerStatus.Volume = Player.Volume;
            var volumePercentageValue = Player.Volume * 100;
            lblVolume.Content = string.Format("{0:N00}%", volumePercentageValue);
            ConsoleLog(string.Format("Set Volume to {0:N00}%", volumePercentageValue));
            volumeSlider.Value = Player.Volume;
        }

        private void volumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateVolume(volumeSlider.Value);
        }

        private void lblReconnect_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ConsoleLog("reconnecting...");
            InitializeSignalR(SoulstoneRelayUrl);
        }

        private void btnToogleMute_Click(object sender, RoutedEventArgs e)
        {
            ToogleMute();
        }
    }
}