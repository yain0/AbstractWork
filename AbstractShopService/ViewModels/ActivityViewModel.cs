namespace AbstractWorkService.ViewModels
{
    public class ActivityViewModel
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string CustomerFIO { get; set; }

        public int RemontId { get; set; }

        public string RemontName { get; set; }

        public int? WorkerId { get; set; }

        public string WorkerName { get; set; }

        public int Koll { get; set; }

        public decimal Summa { get; set; }

        public string Status { get; set; }

        public string DateCreate { get; set; }

        public string DateWork { get; set; }
    }
}
