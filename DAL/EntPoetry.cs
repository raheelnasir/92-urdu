using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EntPoetry
    {
        public int UId { get; set; }    
        public int ContentId { get; set; }
        public string? ContentType { get; set; }
        public string? ContentName { get; set; }
        public string? ContentArrangement { get; set; }
        public string? Verses { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
    }
}
