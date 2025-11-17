using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace S2Hub_App
{
    public partial class InfoDialog : Window
    {
        public InfoDialog()
        {
            InitializeComponent();

            var version = Assembly.GetExecutingAssembly().GetName().Version;
            var build = ReadBuildNumber();

            VersionText.Text = $"Version {version.Major}.{version.Minor}.{version.Build} (Build {build})";
        }

        private void OnGithubClick(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/daesue",
                UseShellExecute = true
            });
        }

        private void OnCloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // 🔥 이제 BuildNumber.txt에서 읽는 버전
        private string ReadBuildNumber()
        {
            try
            {
                string path = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "BuildNumber.txt"
                );

                if (!File.Exists(path))
                    return "0";

                string val = File.ReadAllText(path).Trim();

                // 숫자 아닌 경우 예외 방지
                if (!int.TryParse(val, out _))
                    return "0";

                return val;
            }
            catch
            {
                return "0";
            }
        }
    }
}
