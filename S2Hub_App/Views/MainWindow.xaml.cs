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

        private void OnS2HubInfoClick(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(
            //    "Smart Stow Hub\n\nVersion: 1.0.0\nDeveloper: Daesue\n\n© 2025 All Rights Reserved.",
            //    "S2Hub Information",
            //    MessageBoxButton.OK,
            //    MessageBoxImage.Information);
            var dlg = new InfoDialog();
            dlg.Owner = this;   // 부모 창 중앙에 띄우기
            dlg.ShowDialog();
        }

        private void OnFileCloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnViewUiLanguageClick(object sender, RoutedEventArgs e)
        {
            // UI 언어 변경 로직을 여기에 구현하세요.
            MessageBox.Show("UI 언어 변경 기능은 아직 구현되지 않았습니다.", "알림", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnHelpTopicsClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("도움말 항목을 여는 기능이 아직 구현되지 않았습니다.", "도움말", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnHelpChatClick(object sender, RoutedEventArgs e)
        {
            // LLM 도움말 대화창 열기
            var dlg = new ChatDialog();
            dlg.Owner = this;

            // MainWindow 기준 좌표 계산
            dlg.Loaded += (s, e) =>
            {
                var main = this; // MainWindow

                dlg.Left = main.Left + main.Width - dlg.Width - 20;
                dlg.Top = main.Top + main.Height - dlg.Height - 40;
            };

            dlg.Show();
        }
    }
}