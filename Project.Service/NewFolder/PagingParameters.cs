using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class PagingParameters : IPagingParameters
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
