using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S2Hub_App.Models
{
    public class DirectoryNode
    {
        public string Name { get; }
        public string FullPath { get; }
        public ObservableCollection<DirectoryNode> Children { get; } = new();
        public bool IsDummy { get; private set; }

        public DirectoryNode(string path)
        {
            FullPath = path;
            Name = string.IsNullOrEmpty(Path.GetFileName(path)) ? path : Path.GetFileName(path);
            if (HasSubDirectory(path))
                Children.Add(new DirectoryNode("__DUMMY__", true));
        }

        public void Expand()
        {
            if (Children.Count == 1 && Children[0].IsDummy)
            {
                Children.Clear();
                try
                {
                    foreach (var dir in Directory.GetDirectories(FullPath))
                    {
                        try { Children.Add(new DirectoryNode(dir)); } catch { }
                    }
                }
                catch { }
            }
        }

        private static bool HasSubDirectory(string path)
        {
            try { return Directory.EnumerateDirectories(path).GetEnumerator().MoveNext(); }
            catch { return false; }
        }

        private DirectoryNode(string dummy, bool isDummy)
        {
            Name = dummy;
            FullPath = dummy;
            IsDummy = isDummy;
        }
    }
}
