using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AbstractWorkModel
{
    /// <summary>
    /// Компонент, требуемый для изготовления изделия
    /// </summary>
    public class Material
    {
        public int Id { get; set; }

        [Required]
        public string MaterialName { get; set; }

        [ForeignKey("MaterialId")]
        public virtual List<RemontMaterial> RemontMaterials { get; set; }

        [ForeignKey("MaterialId")]
        public virtual List<SkladMaterial> SkladMaterials { get; set; }
    }
}
