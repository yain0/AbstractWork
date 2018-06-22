using AbstractWorkService.ImplementationsList;
using AbstractWorkService.Interfaces;
using System;
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
            currentContainer.RegisterType<ICustomerService, CustomerServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMaterialService, MaterialServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IWorkerService, WorkerServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IRemontService, RemontServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISkladService, SkladServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMyService, MyServiceList>(new HierarchicalLifetimeManager());
            
            return currentContainer;
        }
    }
}
