using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Soulstone.Entities;

namespace Soulstone.ControlPanel
{
    public partial class AdvancedSearch : Form
    {
        List<SongDto> _allSongs;
        List<SongDto> _userSelection;
        private const string UnknownGenre = "Undefined";
        private const string UnknownYear = "Undefined";
        private const string NoneSelection = "[None]";

        public AdvancedSearch(List<SongDto> songs)
        {
            InitializeComponent();

            lblTotal.Text = string.Empty;
            _allSongs = songs;

            LoadYears();

            LoadGenres();
        }

        public List<SongDto> UserSelection
        {
            get
            {
                return _userSelection;
            }
        }

        private async void LoadGenres()
        {
            await Task.Run(() => {
                var genres = _allSongs.Select(s => s.Genre).Distinct().OrderBy(g => g);
                cmbGenre.Items.Add(NoneSelection);
                foreach (var genre in genres)
                {
                    if (string.IsNullOrEmpty(genre))
                    {
                        cmbGenre.Items.Add(UnknownGenre);
                    }
                    else
                    {
                        cmbGenre.Items.Add(genre);
                    }
                }

                cmbGenre.SelectedIndex = 0;
            });
        }

        private async void LoadYears()
        {
            await Task.Run(() =>
            {
                var years = _allSongs.Select(s => s.Year).Distinct().OrderBy(y => y);
                cmbYear.Items.Add(NoneSelection);
                foreach (var year in years)
                {
                    if (year == 0)
                    {
                        cmbYear.Items.Add(UnknownYear);
                    }
                    else
                    {
                        cmbYear.Items.Add(year);
                    }
                }

                cmbYear.SelectedIndex = 0;
            });
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DoSearch();
        }

        private void DoSearch()
        {
            if (!string.IsNullOrWhiteSpace(txtArtist.Text) || !string.IsNullOrWhiteSpace(txtTitle.Text) || !string.IsNullOrWhiteSpace(txtAlbum.Text) || cmbGenre.SelectedItem != null || cmbYear.SelectedItem != null)
            {
                IQueryable<SongDto> query = _allSongs.AsQueryable();
                Expression<Func<SongDto, bool>> filter;

                int? year = null;
                if (cmbYear.SelectedItem != null && cmbYear.SelectedItem.ToString() != NoneSelection)
                {
                    if (cmbYear.SelectedItem.ToString() != UnknownYear)
                    {
                        year = Convert.ToInt32(cmbYear.SelectedItem);
                    }
                    else
                    {
                        year = 0;
                    }
                }

                string genre = null;
                if (cmbGenre.SelectedItem != null && cmbGenre.SelectedItem.ToString() != NoneSelection)
                {
                    if (cmbGenre.SelectedItem.ToString() != UnknownGenre)
                    {
                        genre = cmbGenre.SelectedItem.ToString();
                    }
                    else
                    {
                        genre = string.Empty;
                    }
                }


                filter = s => ((s.Artist.ToLower().Contains(txtArtist.Text.ToLower()) || string.IsNullOrWhiteSpace(txtArtist.Text)) &&
                    (s.Title.ToLower().Contains(txtTitle.Text.ToLower()) || string.IsNullOrWhiteSpace(txtTitle.Text)) &&
                    (s.Album.ToLower().Contains(txtAlbum.Text.ToLower()) || string.IsNullOrWhiteSpace(txtAlbum.Text))) &&
                    (s.Year == year || !year.HasValue) &&
                    (s.Genre == genre || genre == null);

                query = query.Where(filter);

                _userSelection = query.ToList();

                ShowSongList(_userSelection);
            }

        }

        private void ShowSongList(IEnumerable<SongDto> songs)
        {
            lstSongs.Items.Clear();
            songs = songs.OrderBy(s => s.Artist);
            foreach (var song in songs)
            {
                lstSongs.Items.Add(FormatSong(song));
            }

            lblTotal.Text = string.Format("{0} matchs criteria from a total of {1} songs", songs.Count(), _allSongs.Count());
        }

        private string FormatSong(SongDto song)
        {
            return string.Format("{0} - {1} - {2} | {3:c}, {4}kbps, {5}, {6} ", song.Artist, song.Album, song.Title, song.Duration,
                                 song.Bitrate, song.Genre, song.Year);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddToPlaylist();
        }

        private void AddToPlaylist()
        {
            if (lstSongs.SelectedIndices.Count > 0)
            {
                List<SongDto> userSelection = new List<SongDto>();
                foreach (int index in lstSongs.SelectedIndices)
                {
                    userSelection.Add(_userSelection[index]);
                }

                _userSelection = userSelection;
                DialogResult = DialogResult.OK;
            }
        }

        private void txt_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                lstSongs.Focus();

                DoSearch();
            }
        }

        private void lstSongs_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                AddToPlaylist();
            }
        }

        private void lstSongs_DoubleClick(object sender, EventArgs e)
        {
            AddToPlaylist();
        }
    }
}
