using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageConverter.src.windows {
    public partial class UpdateCheck : Form {
        public UpdateCheck() {
            InitializeComponent();
        }

        private void UpdateCheck_Load(object sender, EventArgs e) {
            Icon = Icon.FromHandle(Properties.Resources.IconInverted.GetHicon());
            Location = new Point(Settings.xPos, Settings.yPos);
            string version = GetLatestVersion();

            if (version == null) {
                StatusLabel.Text = "An error occurred.";
            } else if (string.Equals(version, Settings.tag)) {
                StatusLabel.Text = "Currently on latest version.";
            } else {
                StatusLabel.Text = $"Newer version available. ({version})";
            }
        }

        private void OpenLatestRelease_Click(object sender, EventArgs e) {
            Process.Start("https://github.com/Alyosha015/Stormworks-Image-To-Paintblock/releases/latest");
        }

        private static string GetLatestVersion() {
            string latestReleaseTag;

            using (WebClient client = new WebClient()) {
                try {
                    client.Headers.Add("user-agent", "request");
                    string jsonStr = client.DownloadString("https://api.github.com/repos/Alyosha015/Stormworks-Image-To-Paintblock/releases/latest");
                    //I don't want to have an entire json library worth of dlls in the release, so I this is meant to get the tag_name value instead.
                    int index = jsonStr.IndexOf("\"tag_name\":");
                    string afterTagName = jsonStr.Substring(index + 11);
                    string afterTagValue = afterTagName.Substring(afterTagName.IndexOf('\"') + 1);
                    latestReleaseTag = afterTagValue.Substring(0, afterTagValue.IndexOf('\"'));
                } catch (Exception) {
                    return null;
                }
            }

            return latestReleaseTag;
        }
    }
}
