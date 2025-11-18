using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace S2Hub_App.Views
{
    /// <summary>
    /// ChatDialog.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ChatDialog : Window
    {

        public ChatDialog()
        {
            InitializeComponent();
        }

        // 전체 창 드래그 이동
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        // 캐릭터에 마우스 올라오면 X 표시
        private void Character_MouseEnter(object sender, MouseEventArgs e)
        {
            CloseButton.Visibility = Visibility.Visible;
        }

        // 캐릭터에서 벗어나도 → X 버튼으로 가는 경우는 숨기지 않음
        private void Character_MouseLeave(object sender, MouseEventArgs e)
        {
            // 실제로 Grid_MouseLeave와 다르게,
            // X 버튼에 올라가는지 확인
            if (!CloseButton.IsMouseOver)
            {
                CloseButton.Visibility = Visibility.Collapsed;
            }
        }

        // X 버튼 영역에서도 계속 보이게 하기
        private void CloseButton_MouseEnter(object sender, MouseEventArgs e)
        {
            CloseButton.Visibility = Visibility.Visible;
        }

        private void CloseButton_MouseLeave(object sender, MouseEventArgs e)
        {
            // X에서 벗어나도 캐릭터 위라면 유지
            if (!CharacterImage.IsMouseOver)
                CloseButton.Visibility = Visibility.Collapsed;
        }

        private void CloseButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

    }
}
