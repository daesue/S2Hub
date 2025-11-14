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
using S2Hub_App.Models; // DirectoryNode의 실제 네임스페이스로 수정
using S2Hub_App.ViewModels; // MainViewModel의 실제 네임스페이스로 수정

namespace S2Hub_App.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnTreeItemExpanded(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is TreeViewItem tvi && tvi.DataContext is DirectoryNode node)
            {
                (DataContext as MainViewModel)?.ExpandCommand.Execute(node);
            }
        }

        private void DirectoryTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (DataContext is MainViewModel vm)
                vm.SelectedNode = e.NewValue as DirectoryNode;
        }

        private void MenuInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Smart Stow Hub\n\nVersion: 1.0.0\nDeveloper: Daesue\n\n© 2025 All Rights Reserved.",
                "S2Hub Information",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}