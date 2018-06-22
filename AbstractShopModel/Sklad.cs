using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AbstractWorkModel
{
    /// <summary>
    /// Хранилиище компонентов в магазине
    /// </summary>
    public class Sklad
    {
        public int Id { get; set; }
        [Required]
        public string SkladName { get; set; }
        [ForeignKey("SkladId")]
        public virtual List<SkladMaterial> SkladMaterial { get; set; }
    }
}
