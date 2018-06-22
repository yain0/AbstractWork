using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractWorkService.ViewModels
{
    public class CustomerActivityModel
    {
        public string CustomerName { get; set; }
        public string DateCreate { get; set; }
        public string RemontName { get; set; }
        public int Koll { get; set; }
        public decimal Summa { get; set; }
        public string Status { get; set; }
    }
}
