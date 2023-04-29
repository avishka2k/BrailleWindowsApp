using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrailleApp
{
    public class History
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int Parameter1 { get; set; }
        public string Parameter2 { get; set; }
        public string Braille { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
