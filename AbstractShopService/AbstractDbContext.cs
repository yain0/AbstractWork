using AbstractWorkModel;
using System.Data.Entity;

namespace AbstractWorkService
{
    public class AbstractDbContext : DbContext
    {
        public AbstractDbContext() : base("AbstractDatabase")
        {
            //настройки конфигурации для entity
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public virtual DbSet<Сustomer> Customers { get; set; }

        public virtual DbSet<Material> Materials { get; set; }

        public virtual DbSet<Worker> Workers { get; set; }

        public virtual DbSet<Activity> Activitys { get; set; }

        public virtual DbSet<Remont> Remonts { get; set; }

        public virtual DbSet<RemontMaterial> RemontMaterials { get; set; }

        public virtual DbSet<Sklad> Sklads { get; set; }

        public virtual DbSet<SkladMaterial> SkladMaterials { get; set; }
    }
}
