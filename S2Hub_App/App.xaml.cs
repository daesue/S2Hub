using System.Configuration;
using System.Data;
using System.Windows;

namespace S2Hub_App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

#if DEBUG
            var ver = System.Reflection.Assembly
                .GetExecutingAssembly()
                .GetName().Version;

            //File.AppendAllText("runlog.txt",
            //    $"[{DateTime.Now}] Version {ver} 실행됨\n");
#endif
        }


    }

}
