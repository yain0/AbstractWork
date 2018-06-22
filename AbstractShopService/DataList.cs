using AbstractWorkModel;
using System.Collections.Generic;

namespace AbstractWorkService
{
    class DataListSingleton
    {
        private static DataListSingleton instance;

        public List<Сustomer> Customers { get; set; }

        public List<Material> Materials { get; set; }

        public List<Worker> Workers { get; set; }

        public List<Activity> Activitys { get; set; }

        public List<Remont> Remonts { get; set; }

        public List<RemontMaterial> RemontMaterials { get; set; }

        public List<Sklad> Sklads { get; set; }

        public List<SkladMaterial> SkladMaterials { get; set; }

        private DataListSingleton()
        {
            Customers = new List<Сustomer>();
            Materials = new List<Material>();
            Workers = new List<Worker>();
            Activitys = new List<Activity>();
            Remonts = new List<Remont>();
            RemontMaterials = new List<RemontMaterial>();
            Sklads = new List<Sklad>();
            SkladMaterials = new List<SkladMaterial>();
        }

        public static DataListSingleton GetInstance()
        {
            if(instance == null)
            {
                instance = new DataListSingleton();
            }

            return instance;
        }
    }
}
