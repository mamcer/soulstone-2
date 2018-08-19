using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Soulstone.Data;
using Soulstone.Mp3;

namespace Soulstone.Scanner
{
    //MemoryStream ms = new MemoryStream(song.Artwork);
    //Image image = Image.FromStream(ms);

    public partial class Main : Form
    {
        private DateTime _scanTime;
        private int _totalFiles;
        private StringDictionary _filesHash;

        public Main()
        {
            InitializeComponent();
            lblStatus.Text = string.Empty;
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtFolderPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            var folderPath = txtFolderPath.Text;
            ScanStarted();
            if (Directory.Exists(folderPath))
            {
                ConsoleLog("Computing hash of files...");
                _filesHash = Sha2Calculator.ComputeFolderHash(txtFolderPath.Text);
                ConsoleLog("Processing files...");
                var filePaths = Directory.GetFiles(folderPath, "*.mp3");
                var entities = new SoulstoneEntities();
                foreach (var filePath in filePaths)
                {
                    var fileName = Path.GetFileName(filePath).ToLower();
                    var fileHash = _filesHash[fileName];
                    if (!SongExists(entities, fileHash))
                    {
                        var musicTrack = Id3Reader.Instance.GetMusicTrackFromId3(filePath);
                        if (musicTrack == null)
                        {
                            continue;
                        }

                        AddSong(entities, musicTrack, fileHash, fileName);
                        entities.SaveChanges();
                        ConsoleLog(string.Format("{0} : added to the database", Path.GetFileName(filePath)));
                    }
                    else
                    {
                        ConsoleLog(string.Format("{0} : already exists on database", fileName));
                    }

                    _totalFiles += 1;
                }

            }

            ScanFinished();
        }

        private bool SongExists(SoulstoneEntities entities, string fileHash)
        {
            return entities.Songs.Any(s => s.Hash == fileHash);
        }

        private void AddSong(SoulstoneEntities entities, MusicTrack musicTrack, string fileHash, string fileName)
        {
            var song = new Song
                {
                    Artist = musicTrack.Artist,
                    Album = musicTrack.Album,
                    Title = musicTrack.Title,
                    Artwork = musicTrack.Artwork,
                    Bitrate = musicTrack.Bitrate,
                    Duration = musicTrack.Duration,
                    Genre = musicTrack.Genre,
                    Year = musicTrack.Year,
                    Hash = fileHash,
                    FileName = fileName
                };

            entities.Songs.Add(song);
        }

        private void ScanFinished()
        {
            txtFolderPath.Enabled = true;
            btnOpenFolder.Enabled = true;
            btnScan.Enabled = true;
            var message = string.Format("Scan Finished. Total time: {0}. Total {1} files scanned.",
                                        DateTime.Now.Subtract(_scanTime).ToString("hh\\:mm\\:ss"), _totalFiles);
            ConsoleLog(message);
            lblStatus.Text = message;
        }

        private void ScanStarted()
        {
            _totalFiles = 0;
            lblStatus.Text = string.Format("Scan Started: {0}", DateTime.Now.ToString("hh\\:mm\\:ss")); ;
            _scanTime = DateTime.Now;
            txtFolderPath.Enabled = false;
            btnOpenFolder.Enabled = false;
            btnScan.Enabled = false;
        }
       
        private void ConsoleLog(string msg)
        {
            txtConsole.Text += DateTime.Now.ToString("yyyy.dd.MM-hh:mm:ss") + " - " + msg + Environment.NewLine;
            txtConsole.SelectionStart = txtConsole.Text.Length;
            txtConsole.ScrollToCaret();
        }
    }
}