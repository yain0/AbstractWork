using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AbstractWorkModel
{
    /// <summary>
    /// Изделие, изготавливаемое в магазине
    /// </summary>
    public class Remont
    {
        public int Id { get; set; }
        [Required]
        public string RemontName { get; set; }
        [Required]
        public decimal Cost { get; set; }
        [ForeignKey("RemontId")]
        public virtual List<Activity> Activitys { get; set; }

        [ForeignKey("RemontId")]
        public virtual List<RemontMaterial> RemontMaterials { get; set; }
    }
}
