using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectLayer.Dtos
{
    public class BOL_ChartData
    {
        public IEnumerable<string> Labels { get; set; }
        public IEnumerable<double> Values { get; set; }


    }
}
