using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
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
            } else if (string.Equals(version, Settings.version)) {
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
                    JsonDocument json = JsonDocument.Parse(jsonStr);
                    latestReleaseTag = json.RootElement.GetProperty("tag_name").GetString();
                } catch (Exception ex) {
                    return null;
                }
            }

            return latestReleaseTag;
        }
    }
}
