using System.Collections.Generic;

namespace AbstractWorkService.ViewModels
{
    public class SkladViewModel
    {
        public int Id { get; set; }

        public string SkladName { get; set; }

        public List<SkladMaterialViewModel> SkladMaterial { get; set; }
    }
}
