namespace Soulstone.ControlPanel
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControlPanel = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lstHosts = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnEditPlaylist = new System.Windows.Forms.Button();
            this.btnRemovePlaylist = new System.Windows.Forms.Button();
            this.btnAddPlaylist = new System.Windows.Forms.Button();
            this.lstPlaylists = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lnkAdvancedSearch = new System.Windows.Forms.LinkLabel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtPlaylistSearch = new System.Windows.Forms.TextBox();
            this.btnAddAll = new System.Windows.Forms.Button();
            this.lblPlaylistName = new System.Windows.Forms.Label();
            this.lblPlaylistSongs = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnClearPlaylist = new System.Windows.Forms.Button();
            this.btnMute = new System.Windows.Forms.Button();
            this.lblVolume = new System.Windows.Forms.Label();
            this.volumeTrackBar = new System.Windows.Forms.TrackBar();
            this.btnPause = new System.Windows.Forms.Button();
            this.lblTotalSongs = new System.Windows.Forms.Label();
            this.lstPlaylist = new System.Windows.Forms.ListBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.lstSongs = new System.Windows.Forms.ListBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.chkShuffle = new System.Windows.Forms.CheckBox();
            this.lblHost = new System.Windows.Forms.Label();
            this.lblUserNickName = new System.Windows.Forms.Label();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.lnkReconnect = new System.Windows.Forms.LinkLabel();
            this.lblHostPlayingStatus = new System.Windows.Forms.Label();
            this.lblCurrentlyPlaying = new System.Windows.Forms.Label();
            this.tabControlPanel.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volumeTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlPanel
            // 
            this.tabControlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlPanel.Controls.Add(this.tabPage1);
            this.tabControlPanel.Controls.Add(this.tabPage3);
            this.tabControlPanel.Controls.Add(this.tabPage2);
            this.tabControlPanel.Font = new System.Drawing.Font("Segoe UI Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlPanel.Location = new System.Drawing.Point(0, 71);
            this.tabControlPanel.Name = "tabControlPanel";
            this.tabControlPanel.SelectedIndex = 0;
            this.tabControlPanel.Size = new System.Drawing.Size(943, 564);
            this.tabControlPanel.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lstHosts);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(935, 531);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Hosts";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lstHosts
            // 
            this.lstHosts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstHosts.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstHosts.FormattingEnabled = true;
            this.lstHosts.ItemHeight = 25;
            this.lstHosts.Location = new System.Drawing.Point(3, 3);
            this.lstHosts.Name = "lstHosts";
            this.lstHosts.Size = new System.Drawing.Size(929, 504);
            this.lstHosts.TabIndex = 0;
            this.lstHosts.DoubleClick += new System.EventHandler(this.lstHosts_DoubleClick);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnEditPlaylist);
            this.tabPage3.Controls.Add(this.btnRemovePlaylist);
            this.tabPage3.Controls.Add(this.btnAddPlaylist);
            this.tabPage3.Controls.Add(this.lstPlaylists);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(935, 531);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Playlists";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnEditPlaylist
            // 
            this.btnEditPlaylist.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditPlaylist.Location = new System.Drawing.Point(67, 501);
            this.btnEditPlaylist.Margin = new System.Windows.Forms.Padding(0);
            this.btnEditPlaylist.Name = "btnEditPlaylist";
            this.btnEditPlaylist.Size = new System.Drawing.Size(54, 27);
            this.btnEditPlaylist.TabIndex = 4;
            this.btnEditPlaylist.Text = "Edit";
            this.btnEditPlaylist.UseVisualStyleBackColor = true;
            this.btnEditPlaylist.Click += new System.EventHandler(this.btnEditPlaylist_Click);
            // 
            // btnRemovePlaylist
            // 
            this.btnRemovePlaylist.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemovePlaylist.Location = new System.Drawing.Point(131, 501);
            this.btnRemovePlaylist.Margin = new System.Windows.Forms.Padding(0);
            this.btnRemovePlaylist.Name = "btnRemovePlaylist";
            this.btnRemovePlaylist.Size = new System.Drawing.Size(56, 27);
            this.btnRemovePlaylist.TabIndex = 3;
            this.btnRemovePlaylist.Text = "Remove";
            this.btnRemovePlaylist.UseVisualStyleBackColor = true;
            this.btnRemovePlaylist.Click += new System.EventHandler(this.btnRemovePlaylist_Click);
            // 
            // btnAddPlaylist
            // 
            this.btnAddPlaylist.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddPlaylist.Location = new System.Drawing.Point(4, 501);
            this.btnAddPlaylist.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddPlaylist.Name = "btnAddPlaylist";
            this.btnAddPlaylist.Size = new System.Drawing.Size(54, 27);
            this.btnAddPlaylist.TabIndex = 2;
            this.btnAddPlaylist.Text = "Add";
            this.btnAddPlaylist.UseVisualStyleBackColor = true;
            this.btnAddPlaylist.Click += new System.EventHandler(this.btnAddPlaylist_Click);
            // 
            // lstPlaylists
            // 
            this.lstPlaylists.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPlaylists.FormattingEnabled = true;
            this.lstPlaylists.ItemHeight = 25;
            this.lstPlaylists.Location = new System.Drawing.Point(4, 13);
            this.lstPlaylists.Name = "lstPlaylists";
            this.lstPlaylists.Size = new System.Drawing.Size(380, 479);
            this.lstPlaylists.TabIndex = 0;
            this.lstPlaylists.DoubleClick += new System.EventHandler(this.lstPlaylists_DoubleClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lnkAdvancedSearch);
            this.tabPage2.Controls.Add(this.btnAdd);
            this.tabPage2.Controls.Add(this.txtPlaylistSearch);
            this.tabPage2.Controls.Add(this.btnAddAll);
            this.tabPage2.Controls.Add(this.lblPlaylistName);
            this.tabPage2.Controls.Add(this.lblPlaylistSongs);
            this.tabPage2.Controls.Add(this.btnNext);
            this.tabPage2.Controls.Add(this.btnRemove);
            this.tabPage2.Controls.Add(this.btnClearPlaylist);
            this.tabPage2.Controls.Add(this.btnMute);
            this.tabPage2.Controls.Add(this.lblVolume);
            this.tabPage2.Controls.Add(this.volumeTrackBar);
            this.tabPage2.Controls.Add(this.btnPause);
            this.tabPage2.Controls.Add(this.lblTotalSongs);
            this.tabPage2.Controls.Add(this.lstPlaylist);
            this.tabPage2.Controls.Add(this.btnStop);
            this.tabPage2.Controls.Add(this.btnPlay);
            this.tabPage2.Controls.Add(this.lstSongs);
            this.tabPage2.Controls.Add(this.txtSearch);
            this.tabPage2.Controls.Add(this.chkShuffle);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(935, 531);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Songs";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lnkAdvancedSearch
            // 
            this.lnkAdvancedSearch.AutoSize = true;
            this.lnkAdvancedSearch.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkAdvancedSearch.Location = new System.Drawing.Point(363, 18);
            this.lnkAdvancedSearch.Name = "lnkAdvancedSearch";
            this.lnkAdvancedSearch.Size = new System.Drawing.Size(93, 13);
            this.lnkAdvancedSearch.TabIndex = 26;
            this.lnkAdvancedSearch.TabStop = true;
            this.lnkAdvancedSearch.Text = "Advanced search";
            this.lnkAdvancedSearch.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkAdvancedSearch_LinkClicked);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(342, 494);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(54, 27);
            this.btnAdd.TabIndex = 25;
            this.btnAdd.Text = "add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtPlaylistSearch
            // 
            this.txtPlaylistSearch.Location = new System.Drawing.Point(477, 43);
            this.txtPlaylistSearch.Name = "txtPlaylistSearch";
            this.txtPlaylistSearch.Size = new System.Drawing.Size(450, 27);
            this.txtPlaylistSearch.TabIndex = 22;
            this.txtPlaylistSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPlaylistSearch_KeyUp);
            // 
            // btnAddAll
            // 
            this.btnAddAll.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddAll.Location = new System.Drawing.Point(402, 494);
            this.btnAddAll.Name = "btnAddAll";
            this.btnAddAll.Size = new System.Drawing.Size(54, 27);
            this.btnAddAll.TabIndex = 21;
            this.btnAddAll.Text = "add all";
            this.btnAddAll.UseVisualStyleBackColor = true;
            this.btnAddAll.Click += new System.EventHandler(this.btnAddAll_Click);
            // 
            // lblPlaylistName
            // 
            this.lblPlaylistName.AutoSize = true;
            this.lblPlaylistName.Font = new System.Drawing.Font("Segoe UI Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlaylistName.Location = new System.Drawing.Point(6, 9);
            this.lblPlaylistName.Name = "lblPlaylistName";
            this.lblPlaylistName.Size = new System.Drawing.Size(85, 25);
            this.lblPlaylistName.TabIndex = 20;
            this.lblPlaylistName.Text = "All Songs";
            // 
            // lblPlaylistSongs
            // 
            this.lblPlaylistSongs.AutoSize = true;
            this.lblPlaylistSongs.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlaylistSongs.Location = new System.Drawing.Point(474, 498);
            this.lblPlaylistSongs.Name = "lblPlaylistSongs";
            this.lblPlaylistSongs.Size = new System.Drawing.Size(108, 17);
            this.lblPlaylistSongs.TabIndex = 19;
            this.lblPlaylistSongs.Text = "Total 3224 songs";
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(657, 6);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(54, 27);
            this.btnNext.TabIndex = 17;
            this.btnNext.Text = "next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Location = new System.Drawing.Point(813, 494);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(54, 27);
            this.btnRemove.TabIndex = 15;
            this.btnRemove.Text = "remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnClearPlaylist
            // 
            this.btnClearPlaylist.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearPlaylist.Location = new System.Drawing.Point(873, 494);
            this.btnClearPlaylist.Name = "btnClearPlaylist";
            this.btnClearPlaylist.Size = new System.Drawing.Size(54, 27);
            this.btnClearPlaylist.TabIndex = 12;
            this.btnClearPlaylist.Text = "clear";
            this.btnClearPlaylist.UseVisualStyleBackColor = true;
            this.btnClearPlaylist.Click += new System.EventHandler(this.btnClearPlaylist_Click);
            // 
            // btnMute
            // 
            this.btnMute.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMute.Location = new System.Drawing.Point(722, 6);
            this.btnMute.Name = "btnMute";
            this.btnMute.Size = new System.Drawing.Size(54, 27);
            this.btnMute.TabIndex = 11;
            this.btnMute.Text = "mute";
            this.btnMute.UseVisualStyleBackColor = true;
            this.btnMute.Click += new System.EventHandler(this.btnMute_Click);
            // 
            // lblVolume
            // 
            this.lblVolume.AutoSize = true;
            this.lblVolume.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVolume.Location = new System.Drawing.Point(890, 10);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(39, 17);
            this.lblVolume.TabIndex = 10;
            this.lblVolume.Text = "100%";
            // 
            // volumeTrackBar
            // 
            this.volumeTrackBar.AutoSize = false;
            this.volumeTrackBar.BackColor = System.Drawing.Color.White;
            this.volumeTrackBar.LargeChange = 1;
            this.volumeTrackBar.Location = new System.Drawing.Point(782, 9);
            this.volumeTrackBar.Name = "volumeTrackBar";
            this.volumeTrackBar.Size = new System.Drawing.Size(110, 25);
            this.volumeTrackBar.TabIndex = 9;
            this.volumeTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.volumeTrackBar.Value = 5;
            this.volumeTrackBar.ValueChanged += new System.EventHandler(this.volumeTrackBar_ValueChanged);
            // 
            // btnPause
            // 
            this.btnPause.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPause.Location = new System.Drawing.Point(537, 6);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(54, 27);
            this.btnPause.TabIndex = 8;
            this.btnPause.Text = "pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // lblTotalSongs
            // 
            this.lblTotalSongs.AutoSize = true;
            this.lblTotalSongs.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalSongs.Location = new System.Drawing.Point(3, 498);
            this.lblTotalSongs.Name = "lblTotalSongs";
            this.lblTotalSongs.Size = new System.Drawing.Size(108, 17);
            this.lblTotalSongs.TabIndex = 7;
            this.lblTotalSongs.Text = "Total 3224 songs";
            // 
            // lstPlaylist
            // 
            this.lstPlaylist.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPlaylist.FormattingEnabled = true;
            this.lstPlaylist.ItemHeight = 17;
            this.lstPlaylist.Location = new System.Drawing.Point(477, 76);
            this.lstPlaylist.Name = "lstPlaylist";
            this.lstPlaylist.Size = new System.Drawing.Size(450, 412);
            this.lstPlaylist.TabIndex = 6;
            this.lstPlaylist.DoubleClick += new System.EventHandler(this.lstPlaylist_DoubleClick);
            this.lstPlaylist.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstPlaylist_KeyDown);
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(597, 6);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(54, 27);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlay.Location = new System.Drawing.Point(477, 6);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(54, 27);
            this.btnPlay.TabIndex = 4;
            this.btnPlay.Text = "play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // lstSongs
            // 
            this.lstSongs.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstSongs.FormattingEnabled = true;
            this.lstSongs.ItemHeight = 17;
            this.lstSongs.Location = new System.Drawing.Point(6, 76);
            this.lstSongs.Name = "lstSongs";
            this.lstSongs.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstSongs.Size = new System.Drawing.Size(450, 412);
            this.lstSongs.TabIndex = 2;
            this.lstSongs.DoubleClick += new System.EventHandler(this.lstSongs_DoubleClick);
            this.lstSongs.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstSongs_KeyUp);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(6, 43);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(450, 27);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyUp);
            // 
            // chkShuffle
            // 
            this.chkShuffle.AutoSize = true;
            this.chkShuffle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShuffle.Location = new System.Drawing.Point(745, 500);
            this.chkShuffle.Name = "chkShuffle";
            this.chkShuffle.Size = new System.Drawing.Size(62, 17);
            this.chkShuffle.TabIndex = 23;
            this.chkShuffle.Text = "shuffle";
            this.chkShuffle.UseVisualStyleBackColor = true;
            this.chkShuffle.Click += new System.EventHandler(this.chkShuffle_Click);
            // 
            // lblHost
            // 
            this.lblHost.AutoSize = true;
            this.lblHost.Font = new System.Drawing.Font("Segoe UI Light", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHost.Location = new System.Drawing.Point(1, 10);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(225, 50);
            this.lblHost.TabIndex = 0;
            this.lblHost.Text = "Host | Playlist";
            // 
            // lblUserNickName
            // 
            this.lblUserNickName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserNickName.AutoSize = true;
            this.lblUserNickName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserNickName.Location = new System.Drawing.Point(787, 3);
            this.lblUserNickName.Name = "lblUserNickName";
            this.lblUserNickName.Size = new System.Drawing.Size(86, 15);
            this.lblUserNickName.TabIndex = 2;
            this.lblUserNickName.Text = "Mario Moreno";
            this.lblUserNickName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtConsole
            // 
            this.txtConsole.BackColor = System.Drawing.Color.Black;
            this.txtConsole.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtConsole.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtConsole.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsole.ForeColor = System.Drawing.Color.Lime;
            this.txtConsole.Location = new System.Drawing.Point(0, 637);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ReadOnly = true;
            this.txtConsole.Size = new System.Drawing.Size(943, 123);
            this.txtConsole.TabIndex = 3;
            // 
            // lnkReconnect
            // 
            this.lnkReconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkReconnect.AutoSize = true;
            this.lnkReconnect.Location = new System.Drawing.Point(879, 3);
            this.lnkReconnect.Name = "lnkReconnect";
            this.lnkReconnect.Size = new System.Drawing.Size(60, 15);
            this.lnkReconnect.TabIndex = 4;
            this.lnkReconnect.TabStop = true;
            this.lnkReconnect.Text = "reconnect";
            this.lnkReconnect.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkReconnect_LinkClicked);
            // 
            // lblHostPlayingStatus
            // 
            this.lblHostPlayingStatus.AutoSize = true;
            this.lblHostPlayingStatus.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHostPlayingStatus.Location = new System.Drawing.Point(478, 20);
            this.lblHostPlayingStatus.Name = "lblHostPlayingStatus";
            this.lblHostPlayingStatus.Size = new System.Drawing.Size(94, 13);
            this.lblHostPlayingStatus.TabIndex = 5;
            this.lblHostPlayingStatus.Text = "Currently Playing";
            this.lblHostPlayingStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurrentlyPlaying
            // 
            this.lblCurrentlyPlaying.AutoSize = true;
            this.lblCurrentlyPlaying.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentlyPlaying.Location = new System.Drawing.Point(476, 35);
            this.lblCurrentlyPlaying.Name = "lblCurrentlyPlaying";
            this.lblCurrentlyPlaying.Size = new System.Drawing.Size(282, 21);
            this.lblCurrentlyPlaying.TabIndex = 6;
            this.lblCurrentlyPlaying.Text = "Metallica - Re Load - Whiskey in the Jar";
            this.lblCurrentlyPlaying.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 760);
            this.Controls.Add(this.lblCurrentlyPlaying);
            this.Controls.Add(this.lblHostPlayingStatus);
            this.Controls.Add(this.lnkReconnect);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.lblUserNickName);
            this.Controls.Add(this.tabControlPanel);
            this.Controls.Add(this.lblHost);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Main";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Soulstone Control Panel";
            this.tabControlPanel.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volumeTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlPanel;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox lstHosts;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.ListBox lstSongs;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblHost;
        private System.Windows.Forms.ListBox lstPlaylist;
        private System.Windows.Forms.Label lblTotalSongs;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.TrackBar volumeTrackBar;
        private System.Windows.Forms.Button btnMute;
        private System.Windows.Forms.Button btnClearPlaylist;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListBox lstPlaylists;
        private System.Windows.Forms.Label lblUserNickName;
        private System.Windows.Forms.Button btnEditPlaylist;
        private System.Windows.Forms.Button btnRemovePlaylist;
        private System.Windows.Forms.Button btnAddPlaylist;
        private System.Windows.Forms.Label lblPlaylistSongs;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label lblPlaylistName;
        private System.Windows.Forms.Button btnAddAll;
        private System.Windows.Forms.TextBox txtPlaylistSearch;
        private System.Windows.Forms.CheckBox chkShuffle;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.LinkLabel lnkReconnect;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblHostPlayingStatus;
        private System.Windows.Forms.Label lblCurrentlyPlaying;
        private System.Windows.Forms.LinkLabel lnkAdvancedSearch;
    }
}

