using System;

namespace AbstractWorkModel
{
    /// <summary>
    /// Заказ клиента
    /// </summary>
    public class Activity
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int RemontId { get; set; }

        public int? WorkerId { get; set; }

        public int Koll { get; set; }

        public decimal Summa { get; set; }

        public ActivityStatus Status { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime? DateWork { get; set; }
        public virtual Сustomer Customer { get; set; }

        public virtual Remont Remont { get; set; }

        public virtual Worker Worker { get; set; }
    }
}
