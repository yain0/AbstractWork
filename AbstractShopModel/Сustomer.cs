using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AbstractWorkModel
{
    /// <summary>
    /// Клиент магазина
    /// </summary>
    public class Сustomer
    {
        public int Id { get; set; }
        [Required]
        public string CustomerFIO { get; set; }

        [ForeignKey("CustomerId")]
        public virtual List<Activity> Activitys { get; set; }
    }
}
