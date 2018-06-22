using System.Collections.Generic;

namespace AbstractWorkService.BindingModels
{
    public class RemontBindingModel
    {
        public int Id { get; set; }

        public string RemontName { get; set; }

        public decimal Cost { get; set; }

        public List<RemontMaterialBindingModel> RemontMaterial { get; set; }
    }
}
