using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractWorkService.ViewModels
{
   public class SkladLoadViewModel
    {
        public string SkladName { get; set; }
        public int TotalKoll { get; set; }
        public IEnumerable<Tuple<string,int>> Materials { get; set; }
    }
}
