using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Shell;

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

            Icon = null; // 아이콘 제거
        }

        private void OnGithubClick(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/daesue/S2Hub",
                UseShellExecute = true
            });
        }

        private void OnCloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // 이제 BuildNumber.txt에서 읽는 버전
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

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // Win32 API 로 TitleBar Icon 제거
            IntPtr hwnd = new WindowInteropHelper(this).Handle;
            const int GCL_HICON = -14;
            const int GCL_HICONSM = -34;

            SetClassLongPtr(hwnd, GCL_HICON, IntPtr.Zero);
            SetClassLongPtr(hwnd, GCL_HICONSM, IntPtr.Zero);
        }

        [DllImport("user32.dll", EntryPoint = "SetClassLongPtrW")]
        static extern IntPtr SetClassLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetClassLongW")]
        static extern uint SetClassLongPtr32(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        private static IntPtr SetClassLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 8)
                return SetClassLongPtr64(hWnd, nIndex, dwNewLong);
            else
                return (IntPtr)SetClassLongPtr32(hWnd, nIndex, dwNewLong);
        }

    }
}
