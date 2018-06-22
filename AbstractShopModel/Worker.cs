using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AbstractWorkModel
{
    /// <summary>
    /// Исполнитель, выполняющий заказы клиентов
    /// </summary>
    public class Worker
    {
        public int Id { get; set; }

        [Required]
        public string WorkerFIO { get; set; }
        [ForeignKey("WorkerId")]
        public virtual List<Activity> Activitys { get; set; }
    }
}
