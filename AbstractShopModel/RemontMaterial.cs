namespace AbstractWorkModel
{
    /// <summary>
    /// Сколько компонентов, требуется при изготовлении изделия
    /// </summary>
    public class RemontMaterial
    {
        public int Id { get; set; }

        public int RemontId { get; set; }

        public int MaterialId { get; set; }

        public int Koll { get; set; }
    }
}
