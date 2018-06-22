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
        public virtual Remont Remont { get; set; }
        public virtual Material Material { get; set; }
    }
}
