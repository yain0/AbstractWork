using AbstractWorkService;
using AbstractWorkService.ImplementationsBD;
using AbstractWorkService.ImplementationsList;
using AbstractWorkService.Interfaces;
using AbstractWorkService.WorkationsBD;
using System;
using System.Data.Entity;
using System.Windows.Forms;

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
            APICustomer.Connect();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMy());
        }

    }
}
