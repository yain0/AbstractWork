using System.Collections.Generic;

namespace AbstractWorkService.ViewModels
{
    public class RemontViewModel
    {
        public int Id { get; set; }

        public string RemontName { get; set; }

        public decimal Cost { get; set; }

        public List<RemontMaterialViewModel> RemontMaterial { get; set; }
    }
}
