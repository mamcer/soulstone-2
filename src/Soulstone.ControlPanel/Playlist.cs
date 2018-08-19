using System;
using System.Windows.Forms;

namespace Soulstone.ControlPanel
{
    public partial class Playlist : Form
    {
        public Playlist()
        {
            InitializeComponent();
        }

        public string PlaylistName
        {
            get { return txtPlaylistName.Text; }
            set { txtPlaylistName.Text = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ok();
        }

        private void Ok()
        {
            if (!string.IsNullOrEmpty(txtPlaylistName.Text))
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Playlist name cannot be empty", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPlaylistName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Ok();
            }
        }
    }
}