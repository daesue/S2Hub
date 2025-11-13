using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S2Hub_App.Models
{
    public class FileItem
    {
        public string Name { get; init; } = "";
        public long SizeKB { get; init; }
        public DateTime Modified { get; init; }
    }
}
