using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace S2Hub_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadDrives();
        }

        private void LoadDrives()
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                TreeViewItem item = new TreeViewItem
                {
                    Header = drive.Name,
                    Tag = drive.RootDirectory.FullName
                };
                item.Items.Add(null);
                item.Expanded += Folder_Expanded;
                DirectoryTree.Items.Add(item);
            }
        }

        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;
            if (item.Items.Count == 1 && item.Items[0] == null)
            {
                item.Items.Clear();

                string path = (string)item.Tag;
                try
                {
                    foreach (var dir in Directory.GetDirectories(path))
                    {
                        TreeViewItem subItem = new TreeViewItem
                        {
                            Header = System.IO.Path.GetFileName(dir),
                            Tag = dir
                        };
                        subItem.Items.Add(null);
                        subItem.Expanded += Folder_Expanded;
                        item.Items.Add(subItem);
                    }
                }
                catch { /* 접근 거부 등 예외 무시 */ }
            }
        }

        private void DirectoryTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = DirectoryTree.SelectedItem as TreeViewItem;
            if (item == null) return;

            string selectedPath = (string)item.Tag;
            if (Directory.Exists(selectedPath))
            {
                var files = Directory.GetFiles(selectedPath)
                    .Select(f => new
                    {
                        Name = System.IO.Path.GetFileName(f),
                        Size = new FileInfo(f).Length / 1024,
                        Modified = File.GetLastWriteTime(f).ToString("yyyy-MM-dd HH:mm")
                    }).ToList();

                FileList.ItemsSource = files;
            }
        }
    }
}