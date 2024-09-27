using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commons.Bases.Request
{
    public class BasePaginationRequest
    {
        public int NumPage { get; set; } = 1;
        public int NumRecordsPAge { get; set; } = 10;
        public readonly int NumMaxRecordsPage = 50;
        public string Order { get; set; } = "asc";
        public string? Sort { get; set; } = null;
        public int Records
        {
            get => NumRecordsPAge;
            set
            {
                NumRecordsPAge = value > NumMaxRecordsPage ? NumMaxRecordsPage : value;
            }
        }
    }
}
