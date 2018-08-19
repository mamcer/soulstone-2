using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using Soulstone.Entities;
using ConnectionState = Microsoft.AspNet.SignalR.Client.ConnectionState;

namespace Soulstone.ControlPanel
{
    public partial class Main : Form
    {
        private List<SongDto> _allSongs;
        private List<SongDto> _actualSongs;
        private List<PlaylistSongDto> _playlist;
        private List<PlaylistSongDto> _actualPlaylist;
        private List<HostDto> _hosts;
        private List<PlaylistDto> _playlists;
        private IHubProxy _soulstoneHub;
        private bool _isPlayerStopped = true;
        private int _muteVolume;
        private HostDto _selectedHost;
        private PlaylistDto _selectedPlaylist;
        private int _userId;

        private string SoulstoneRelayUrl { get; set; }

        private string SoulstoneApiUrl { get; set; }

        private void InitializeClient(HttpClient client)
        {
            client.BaseAddress = new Uri(SoulstoneApiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public Main()
        {
            InitializeComponent();

            InitializeApplication();
        }

        private void InitializeApplication()
        {
            InitializeConfigKeys();

            LoadUserInfo();

            LoadHosts();

            lblHost.Text = string.Empty;
            lblPlaylistName.Text = string.Empty;
            lblHostPlayingStatus.Text = string.Empty;
            lblCurrentlyPlaying.Text = string.Empty;

            _allSongs = new List<SongDto>();
            _playlist = new List<PlaylistSongDto>();
            _hosts = new List<HostDto>();

            InitializeSignalR(SoulstoneRelayUrl);

            _soulstoneHub.On<int, PlayerStatus>("PlayerStatus", (hostId, playerStatus) =>
            {
                if (hostId == _selectedHost.Id)
                {
                    if (playerStatus.IsPlaying)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lblHostPlayingStatus.Text = "Currently Playing";
                            lblCurrentlyPlaying.Text = string.Format("{0} - {1} - {2}", playerStatus.Artist, playerStatus.Album, playerStatus.Title);
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            lblHostPlayingStatus.Text = "There is no playback in progress";
                            lblCurrentlyPlaying.Text = string.Empty;
                        }));
                    }

                    volumeTrackBar.Value = Convert.ToInt32(playerStatus.Volume * 10);
                    chkShuffle.Checked = playerStatus.IsShuffleEnabled;
                }
            });
        }

        private void InitializeConfigKeys()
        {
            if (ConfigurationManager.AppSettings["SoulstoneRelayUrl"] == null)
            {
                ShowErrorMessage("SoulstoneRelayUrl configuration key not found");
                Close();
            }

            if (ConfigurationManager.AppSettings["SoulstoneApiUrl"] == null)
            {
                ShowErrorMessage("SoulstoneApiUrl configuration key not found");
                Close();
            }

            SoulstoneRelayUrl = ConfigurationManager.AppSettings["SoulstoneRelayUrl"];
            SoulstoneApiUrl = ConfigurationManager.AppSettings["SoulstoneApiUrl"];
        }

        private async void LoadHosts()
        {
            lstHosts.Enabled = false;
            lstHosts.Items.Add("loading...");
            try
            {
                var hosts = await GetHosts();
                _hosts = hosts.ToList();
                
                lstHosts.Items.Clear();
                foreach (var searchResult in _hosts)
                {
                    lstHosts.Items.Add(searchResult.Name);
                }

                lstHosts.Enabled = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(string.Format("{0} : {1}", "LoadHosts", ex.Message));
                lstHosts.Items.Clear();
            }
        }

        private void ShowErrorMessage(string message)
        {
            ConsoleLog(message);
        }

        private async Task<IEnumerable<HostDto>> GetHosts()
        {
            using (var client = new HttpClient())
            {
                InitializeClient(client);
                var response = await client.GetAsync("hosts").ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var hosts = await response.Content.ReadAsAsync<IEnumerable<HostDto>>().ConfigureAwait(false);
                    return hosts;
                }
                else
                {
                    throw new Exception(GetErrorMessageFromResponse(response));
                }
            }
        }

        private async void LoadUserInfo()
        {
            lblUserNickName.Text = "loading...";
            try
            {
                var userInfo = await GetUserInfo().ConfigureAwait(false);
                _userId = userInfo.Id;
                lblUserNickName.Text = userInfo.NickName;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(string.Format("{0} : {1}", "LoadUserInfo", ex.Message));
                lblUserNickName.Text = string.Empty;
            }
        }

        private async Task<UserDto> GetUserInfo()
        {
            using (var client = new HttpClient())
            {
                InitializeClient(client);

                var response = await client.GetAsync("users/1").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var userInfo = await response.Content.ReadAsAsync<UserDto>().ConfigureAwait(false);
                    return userInfo;
                }
                else
                {
                    throw new Exception(GetErrorMessageFromResponse(response));
                }
            }
        }

        private string GetErrorMessageFromResponse(HttpResponseMessage response)
        {
            return string.Format("{0} {1} : {2}", response.RequestMessage.Method, response.RequestMessage.RequestUri, response.ReasonPhrase);
        }

        private async void InitializeSignalR(string url)
        {
            var hubConnection = new HubConnection(url);
            _soulstoneHub = hubConnection.CreateHubProxy("soulstoneHub");

            try
            {
                await hubConnection.Start();
            }
            catch
            {
                ConsoleLog("SignalR connection could not be established");
                throw;
            }

            ConsoleLog(hubConnection.State != ConnectionState.Connected
                                ? "SignalR connection could not be established"
                                : "SignalR connection established");
        }

        private string FormatPlaylist(PlaylistDto playlist)
        {
            return playlist.PlaylistSongsCount > 0 ? string.Format("{0} [{1} songs]", playlist.Name, playlist.PlaylistSongsCount) : string.Format("{0} [empty]", playlist.Name);
        }

        private void ShowSongList(IEnumerable<SongDto> songs)
        {
            lstSongs.Items.Clear();
            songs = songs.OrderBy(s => s.Artist);
            foreach (var song in songs)
            {
                lstSongs.Items.Add(FormatSong(song));
            }
        }

        private string FormatSong(SongDto song)
        {
            return string.Format("{0} - {1} - {2} | {3:c}, {4}kbps, {5}, {6} ", song.Artist, song.Album, song.Title, song.Duration,
                                 song.Bitrate, song.Genre, song.Year);
        }

        private void HostsWebClientOnDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs downloadStringCompletedEventArgs)
        {
            if (downloadStringCompletedEventArgs.Error == null)
            {
                string data = downloadStringCompletedEventArgs.Result;

                var searchResults = JsonConvert.DeserializeObject<IEnumerable<HostDto>>(data);
                _hosts = searchResults.ToList();
                foreach (var searchResult in _hosts)
                {
                    lstHosts.Items.Add(searchResult.Name);
                }
            }
            else
            {
                ConsoleLog("Unable to connect to Soulstone API");
            }
        }

        private void ShowPlaylistsList()
        {
            lstPlaylists.Items.Clear();
            lstPlaylists.Enabled = true;
            foreach (var playlist in _playlists)
            {
                lstPlaylists.Items.Add(FormatPlaylist(playlist));
            }
        }

        private async void lstHosts_DoubleClick(object sender, EventArgs e)
        {
            tabControlPanel.SelectedIndex = 1;
            _selectedHost = _hosts[lstHosts.SelectedIndex];
            _selectedPlaylist = null;
            UpdateHeaderText();

            lstPlaylists.Items.Clear();
            lstPlaylists.Enabled = false;
            lstPlaylists.Items.Add("loading...");

            await GetUserPlaylists();

            await _soulstoneHub.Invoke("GetPlayerStatus", _selectedHost.Id);

            ShowPlaylistsList();
        }

        private async Task GetUserPlaylists()
        {
            using (var client = new HttpClient())
            {
                InitializeClient(client);
                var response = await client.GetAsync(string.Format("playlists?hostId={0}&userId={1}", _selectedHost.Id, _userId)).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var playlists = await response.Content.ReadAsAsync<IEnumerable<PlaylistDto>>().ConfigureAwait(false);
                    _playlists = playlists.ToList();
                }
                else
                {
                    ShowErrorMessage(GetErrorMessageFromResponse(response));
                }
            }
        }

        private void lstSongs_DoubleClick(object sender, EventArgs e)
        {
            AddSongToPlaylist();
        }

        private async void AddSongToPlaylist()
        {
            var indices = lstSongs.SelectedIndices;
            if (indices.Count > 0)
            {
                foreach(int index in indices)
                {
                    var item = lstSongs.Items[index];
                    if (!lstPlaylist.Items.Contains(item))
                    {
                        SongDto selectedSong = _actualSongs[index];
                        await AddSong(item, selectedSong);
                    }
                }
            }
        }

        private async Task AddSong(object item, SongDto selectedSong)
        {
            var playlistSongDto = new PlaylistSongDto
            {
                Song = selectedSong,
                Position = _playlist.Count + 1
            };

            using (var client = new HttpClient())
            {
                InitializeClient(client);
                var response = await client.PostAsJsonAsync<PlaylistSongDto>(string.Format("playlists/{0}/songs", _selectedPlaylist.Id), playlistSongDto).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    lstPlaylist.Items.Add(item);
                    _playlist.Add(playlistSongDto);
                    lblPlaylistSongs.Text = string.Format("Total {0} songs", _playlist.Count);
                }
                else
                {
                    ShowErrorMessage(GetErrorMessageFromResponse(response));
                }
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (_isPlayerStopped)
            {
                PlaySelectedFile();
                _isPlayerStopped = false;
            }
            else
            {
                _soulstoneHub.Invoke("Play", _selectedHost.Id);
            }
        }

        private void PlaySelectedFile()
        {
            if (lstPlaylist.SelectedItem != null)
            {
                _soulstoneHub.Invoke("PlayFile", _selectedHost.Id, _selectedPlaylist.Id, _actualPlaylist[lstPlaylist.SelectedIndex].Song.Id);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _soulstoneHub.Invoke("Stop", _selectedHost.Id);
            _isPlayerStopped = true;
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Down)
            {
                lstSongs.Focus();
                if (lstSongs.Items.Count > 0)
                {
                    lstSongs.SelectedIndex = 0;
                }
                
                return;
            }

            if (txtSearch.Text != string.Empty)
            {
                var songs = _allSongs.Where(s => s.Title.ToLower().StartsWith(txtSearch.Text.ToLower())).ToList();
                var songsContains = _allSongs.Where(s => s.Title.ToLower().Contains(txtSearch.Text.ToLower()) && !songs.Contains(s)).ToList();
                songs.AddRange(songsContains);
                ShowSongList(songs);
                _actualSongs = songs;
            }
            else
            {
                if (_actualSongs.Count != _allSongs.Count)
                {
                    _actualSongs = _allSongs;
                    ShowSongList(_allSongs);
                }
            }
        }

        private void lstSongs_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                AddSongToPlaylist();
            }
        }

        private void lstPlaylist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                PlaySelectedFile();
            }
        }

        private void lstPlaylist_DoubleClick(object sender, EventArgs e)
        {
            PlaySelectedFile();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            _soulstoneHub.Invoke("Pause", _selectedHost.Id);
        }

        private void volumeTrackBar_ValueChanged(object sender, EventArgs e)
        {
            _soulstoneHub.Invoke("Volume", _selectedHost.Id, volumeTrackBar.Value);
            lblVolume.Text = string.Format("{0}%", volumeTrackBar.Value*10);
        }

        private void btnMute_Click(object sender, EventArgs e)
        {
            if (volumeTrackBar.Enabled)
            {
                _soulstoneHub.Invoke("Mute", _selectedHost.Id);
                volumeTrackBar.Enabled = false;
                _muteVolume = volumeTrackBar.Value;
                volumeTrackBar.ValueChanged -= volumeTrackBar_ValueChanged;
                volumeTrackBar.Value = 0;
                lblVolume.Text = "-";
            }
            else
            {
                _soulstoneHub.Invoke("Mute", _selectedHost.Id);
                volumeTrackBar.Enabled = true;
                volumeTrackBar.Value = _muteVolume;
                volumeTrackBar.ValueChanged += volumeTrackBar_ValueChanged;
                lblVolume.Text = string.Format("{0}%", volumeTrackBar.Value * 10);
            }
        }

        private async void btnClearPlaylist_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This option will remove all items on the current playlist. Are you sure?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                lstPlaylist.Enabled = false;
                using (var client = new HttpClient())
                { 
                    InitializeClient(client);
                    var response = await client.DeleteAsync(string.Format("playlists/{0}/songs", _selectedPlaylist.Id)).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        _playlist.Clear();
                        lstPlaylist.Items.Clear();
                    }
                    else
                    {
                        ShowErrorMessage(GetErrorMessageFromResponse(response));
                    }
                }

                lstPlaylist.Enabled = true;
            }
        }

        private async void lstPlaylists_DoubleClick(object sender, EventArgs e)
        {
            tabControlPanel.SelectedIndex = 2;
            _selectedPlaylist = _playlists[lstPlaylists.SelectedIndex];
            txtSearch.Text = string.Empty;
            txtPlaylistSearch.Text = string.Empty;

            lstSongs.Items.Clear();
            lstSongs.Enabled = false;
            lnkAdvancedSearch.Enabled = false;
            lstSongs.Items.Add("loading...");
            lblTotalSongs.Text = "loading...";

            lstPlaylist.Items.Clear();
            lstPlaylist.Enabled = false;
            lstPlaylist.Items.Add("loading...");
            lblPlaylistSongs.Text = "loading...";

            using (var client = new HttpClient())
            {
                InitializeClient(client);
                var response = await client.GetAsync("songs?page=0&pageSize=5000");

                if (response.IsSuccessStatusCode)
                {
                    var songs = await response.Content.ReadAsAsync<IEnumerable<SongDto>>();
                    _allSongs = songs.ToList();
                    ShowSongList(_allSongs);
                    lblTotalSongs.Text = string.Format("Total {0} songs", _allSongs.Count);
                    _actualSongs = _allSongs;
                }
                else
                {
                    ShowErrorMessage(GetErrorMessageFromResponse(response));
                }
            }

            using (var client = new HttpClient())
            {
                InitializeClient(client);
                var response = await client.GetAsync(string.Format("playlists/{0}", _selectedPlaylist.Id));

                if (response.IsSuccessStatusCode)
                {
                    var playlist = await response.Content.ReadAsAsync<PlaylistDto>();
                    _playlist = playlist.Songs.ToList();
                    _actualPlaylist = _playlist;
                    _playlist.Sort((p1, p2) => p1.Position.CompareTo(p2.Position));
                    _selectedPlaylist = playlist;
                    UpdateHeaderText();

                    ShowPlaylist(_playlist);
                }
                else
                {
                    ShowErrorMessage(GetErrorMessageFromResponse(response));
                }
            }

            lstSongs.Enabled = true;
            lnkAdvancedSearch.Enabled = true;
        }

        private void UpdateHeaderText()
        {
            if (_selectedPlaylist != null)
            {
                lblHost.Text = _selectedHost.Name;
                lblPlaylistName.Text = _selectedPlaylist.Name;
            }
            else
            {
                lblHost.Text = _selectedHost.Name;
            }
        }

        private async void btnAddPlaylist_Click(object sender, EventArgs e)
        {
            Playlist playlist = new Playlist();
            if (playlist.ShowDialog() == DialogResult.OK)
            {
                var playlistDto = new PlaylistDto
                    {
                        HostId = _selectedHost.Id,
                        Name = playlist.PlaylistName,
                        UserId = _userId
                    };

                using (var client = new HttpClient())
                {
                    InitializeClient(client);
                    var response = await client.PostAsJsonAsync<PlaylistDto>("playlists", playlistDto).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        playlistDto = await response.Content.ReadAsAsync<PlaylistDto>().ConfigureAwait(false);
                        _playlists.Add(playlistDto);
                        lstPlaylists.Items.Add(FormatPlaylist(playlistDto));
                    }
                    else
                    {
                        ShowErrorMessage(GetErrorMessageFromResponse(response));
                    }
                }
            }
        }

        private async void btnEditPlaylist_Click(object sender, EventArgs e)
        {
            if (lstPlaylists.SelectedItem != null)
            {
                Playlist playlistForm = new Playlist();
                var updatedPlaylist = _playlists[lstPlaylists.SelectedIndex];
                playlistForm.PlaylistName = updatedPlaylist.Name;
                if (playlistForm.ShowDialog() == DialogResult.OK)
                {
                    var oldName = updatedPlaylist.Name;
                    updatedPlaylist.Name = playlistForm.PlaylistName;

                    using (var client = new HttpClient())
                    {
                        InitializeClient(client);
                        var response = await client.PutAsJsonAsync<PlaylistDto>(string.Format("playlists/{0}", updatedPlaylist.Id), updatedPlaylist).ConfigureAwait(false);

                        if (response.IsSuccessStatusCode)
                        {
                            lstPlaylists.Items.Clear();
                            foreach (var playlist in _playlists)
                            {
                                lstPlaylists.Items.Add(FormatPlaylist(playlist));
                            }
                        }
                        else
                        {
                            updatedPlaylist.Name = oldName;
                            ShowErrorMessage(GetErrorMessageFromResponse(response));
                        }
                    }
                }
            }
        }

        private async void btnRemovePlaylist_Click(object sender, EventArgs e)
        {
            var removedPlaylist = _playlists[lstPlaylists.SelectedIndex];
            if (MessageBox.Show(string.Format("This action is permanent. Are you sure to delete '{0}'", removedPlaylist.Name), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (var client = new HttpClient())
                {
                    InitializeClient(client);
                    var response = await client.DeleteAsync(string.Format("playlists/{0}", removedPlaylist.Id)).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        _playlists.Remove(removedPlaylist);
                        lstPlaylists.Items.Clear();
                        foreach (var playlist in _playlists)
                        {
                            lstPlaylists.Items.Add(FormatPlaylist(playlist));
                        }
                    }
                    else
                    {
                        ShowErrorMessage(GetErrorMessageFromResponse(response));
                    }
                }
            }
        }

        private async void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstPlaylist.SelectedIndex >= 0)
            {
                var selectedSongId = _playlist[lstPlaylist.SelectedIndex].Song.Id;

                using (var client = new HttpClient())
                {
                    InitializeClient(client);
                    var response = await client.DeleteAsync(string.Format("playlists/{0}/songs/{1}", _selectedPlaylist.Id, selectedSongId)).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        _playlist.RemoveAt(lstPlaylist.SelectedIndex);
                        lstPlaylist.Items.RemoveAt(lstPlaylist.SelectedIndex);
                    }
                    else
                    {
                        ShowErrorMessage(GetErrorMessageFromResponse(response));
                    }
                }
            }
        }

        private void btnAddAll_Click(object sender, EventArgs e)
        {
            AddAllSongs();
        }

        private async void AddAllSongs()
        {
            using (var client = new HttpClient())
            {
                InitializeClient(client);
                HttpResponseMessage response;

                if (_actualSongs.Count < _allSongs.Count)
                {
                    response = await client.PostAsJsonAsync<List<SongDto>>(string.Format("playlists/{0}/allsongs", _selectedPlaylist.Id), _actualSongs.ToList());
                }
                else
                {
                    response = await client.PostAsync(string.Format("playlists/{0}/allsongs", _selectedPlaylist.Id), null);
                }

                if (response.IsSuccessStatusCode)
                {
                    foreach (var song in _actualSongs)
                    {
                        if (!_playlist.Any(p => p.Song.Id == song.Id))
                        {
                            _playlist.Add(new PlaylistSongDto
                            {
                                Song = song,
                                Position = _playlist.Count + 1
                            });

                            lstPlaylist.Items.Add(lstSongs.Items[_actualSongs.IndexOf(song)]);
                        }
                    }
                }
                else
                {
                    ShowErrorMessage(GetErrorMessageFromResponse(response));
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _soulstoneHub.Invoke("NextSong", _selectedHost.Id);
        }

        private void txtPlaylistSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Down)
            {
                lstPlaylist.Focus();
                if (lstPlaylist.Items.Count > 0)
                {
                    lstPlaylist.SelectedIndex = 0;
                }

                return;
            }

            if (txtPlaylistSearch.Text != string.Empty)
            {
                var songs = _playlist.Where(s => s.Song.Title.ToLower().StartsWith(txtPlaylistSearch.Text.ToLower())).ToList();
                var songsContains = _playlist.Where(s => s.Song.Title.ToLower().Contains(txtPlaylistSearch.Text.ToLower()) && !songs.Contains(s)).ToList();
                songs.AddRange(songsContains);
                _actualPlaylist = songs;
                ShowPlaylist(_actualPlaylist);
            }
            else
            {
                if (_actualPlaylist.Count != _playlist.Count)
                {
                    _actualPlaylist = _playlist;
                    ShowPlaylist(_playlist);
                }
            }
        }

        private void ShowPlaylist(List<PlaylistSongDto> playlist)
        {
            lstPlaylist.Items.Clear();
            lstPlaylist.Enabled = true;
            foreach (var playlistItem in playlist)
            {
                lstPlaylist.Items.Add(FormatSong(playlistItem.Song));
            }
            
            lblPlaylistSongs.Text = string.Format("Total {0} songs", playlist.Count);
        }

        private void chkShuffle_Click(object sender, EventArgs e)
        {
            _soulstoneHub.Invoke("Shuffle", _selectedHost.Id);
        }

        private void ConsoleLog(string message)
        {
            txtConsole.Text += string.Format("{0} - {1}{2}", DateTime.Now.ToString("yyyy.dd.MM-hh:mm:ss"), message, Environment.NewLine);
            txtConsole.SelectionStart = txtConsole.Text.Length;
            txtConsole.ScrollToCaret();
        }

        private void lnkReconnect_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ConsoleLog("reconnecting...");
            InitializeApplication();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddSongToPlaylist();
        }

        private async void lnkAdvancedSearch_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var advancedSearch = new AdvancedSearch(_allSongs);
            if (advancedSearch.ShowDialog(this) == DialogResult.OK)
            {
                var userSelection = advancedSearch.UserSelection;
                foreach (var song in userSelection)
                {
                    var item = FormatSong(song);
                    if (!lstPlaylist.Items.Contains(item))
                    {
                        await AddSong(item, song);
                    }
                }
            }
        }
    }
}