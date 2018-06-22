namespace AbstractWorkService.BindingModels
{
    public class ActivityBindingModel
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int RemontId { get; set; }

        public int? WorkerId { get; set; }

        public int Koll { get; set; }

        public decimal Summa { get; set; }
    }
}
