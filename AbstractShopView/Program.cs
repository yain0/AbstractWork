using AbstractWorkService;
using AbstractWorkService.ImplementationsBD;
using AbstractWorkService.ImplementationsList;
using AbstractWorkService.Interfaces;
using AbstractWorkService.WorkationsBD;
using System;
using System.Data.Entity;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace AbstractWorkView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMy>());
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, AbstractDbContext>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICustomerService, CustomerServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMaterialService, MaterialServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IWorkerService, WorkerServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IRemontService, RemontServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISkladService, SkladServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMyService, MyServiceBD>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
