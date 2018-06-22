using AbstractWorkModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace AbstractWorkService
{
    [Table("AbstractDatabase")]
    public class AbstractDbContext : DbContext
    {
        public AbstractDbContext()
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
