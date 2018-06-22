using AbstractWorkModel;
using System.Collections.Generic;

namespace AbstractWorkService
{
    class DataListSingleton
    {
        private static DataListSingleton instance;

        public List<Сustomer> Customer { get; set; }

        public List<Material> Material { get; set; }

        public List<Worker> Worker { get; set; }

        public List<Activity> Activity { get; set; }

        public List<Remont> Remont { get; set; }

        public List<RemontMaterial> RemontMaterial { get; set; }

        public List<Sklad> Sklad { get; set; }

        public List<SkladMaterial> SkladMaterial { get; set; }

        private DataListSingleton()
        {
            Customer = new List<Сustomer>();
            Material = new List<Material>();
            Worker = new List<Worker>();
            Activity = new List<Activity>();
            Remont = new List<Remont>();
            RemontMaterial = new List<RemontMaterial>();
            Sklad = new List<Sklad>();
            SkladMaterial = new List<SkladMaterial>();
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
