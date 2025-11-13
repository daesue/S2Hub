using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel; // 추가: INotifyPropertyChanged를 사용하기 위한 using 지시문
using System.Collections.ObjectModel; // 추가: ObservableCollection을 사용하기 위한 using 지시문
using System.IO; // 추가: DriveInfo, Directory, FileInfo를 사용하기 위한 using 지시문
using System.Runtime.CompilerServices; // 추가: CallerMemberName을 사용하기 위한 using 지시문
using S2Hub_App.Models; // DirectoryNode가 정의된 네임스페이스로 수정
using S2Hub_App.Commands; // RelayCommand<T>가 정의된 네임스페이스로 수정 또는 추가

namespace S2Hub_App.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<DirectoryNode> Drives { get; } = new();
        public ObservableCollection<FileItem> Files { get; } = new();

        private DirectoryNode? _selectedNode;
        public DirectoryNode? SelectedNode
        {
            get => _selectedNode;
            set
            {
                if (_selectedNode == value) return;
                _selectedNode = value;
                OnPropertyChanged();
                LoadFilesForSelectedNode();
            }
        }

        public RelayCommand<DirectoryNode> ExpandCommand { get; }

        public MainViewModel()
        {
            foreach (var drive in DriveInfo.GetDrives())
                Drives.Add(new DirectoryNode(drive.RootDirectory.FullName));

            ExpandCommand = new RelayCommand<DirectoryNode>(n => n?.Expand());
        }

        private void LoadFilesForSelectedNode()
        {
            Files.Clear();
            if (_selectedNode == null || !Directory.Exists(_selectedNode.FullPath)) return;

            try
            {
                foreach (var f in Directory.GetFiles(_selectedNode.FullPath).Select(p => new FileInfo(p)))
                {
                    Files.Add(new FileItem
                    {
                        Name = f.Name,
                        SizeKB = f.Length / 1024,
                        Modified = f.LastWriteTime
                    });
                }
            }
            catch { }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
