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
            CloseHitBox.Visibility = Visibility.Visible;
        }
        // 캐릭터에서 벗어나도 → X 영역으로 들어가면 유지
        private void Character_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!CloseHitBox.IsMouseOver)
                CloseHitBox.Visibility = Visibility.Collapsed;
        }

        // X 주변 확장 영역 Hover 시작
        private void CloseHitBox_MouseEnter(object sender, MouseEventArgs e)
        {
            CloseHitBox.Visibility = Visibility.Visible;
        }

        // X 주변 확장 영역 벗어나면 숨김
        private void CloseHitBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!CharacterImage.IsMouseOver)
                CloseHitBox.Visibility = Visibility.Collapsed;
        }

        private void CloseButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void InputChatBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Enter로 전송
            if (e.Key == Key.Enter)
            {
                // 엔터 입력 시 현재 Placeholder 제거된 상태라면
                if (InputChatBox.Text != InputChatBox.Tag.ToString())
                {
                    MessageBox.Show("전송: " + InputChatBox.Text); // 임시 디버깅
                    InputChatBox.Text = ""; // 입력창 비우기
                }

                e.Handled = true;
            }
        }

    }
}
