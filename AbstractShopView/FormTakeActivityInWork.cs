using AbstractWorkService.BindingModels;
using AbstractWorkService.Interfaces;
using AbstractWorkService.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbstractWorkView
{
    public partial class FormTakeActivityInWork : Form
    {
        public int Id { set { id = value; } }
        
        private int? id;

        public FormTakeActivityInWork()
        {
            InitializeComponent();
        }

        private void buttonSave_Click_1(object sender, EventArgs e)
        {
            if (comboBoxImplementer.SelectedValue == null)
            {
                MessageBox.Show("Выберите исполнителя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                int implementerId = Convert.ToInt32(comboBoxImplementer.SelectedValue);
                Task task = Task.Run(() => APICustomer.PostRequestData("api/My/TakeActivityInWork", new ActivityBindingModel
                {
                    Id = id.Value,
                    WorkerId = implementerId
                }));

                task.ContinueWith((prevTask) => MessageBox.Show("Заказ передан в работу. Обновите список", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information),
                    TaskContinuationOptions.OnlyOnRanToCompletion);
                task.ContinueWith((prevTask) =>
                {
                    var ex = (Exception)prevTask.Exception;
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                    }
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }, TaskContinuationOptions.OnlyOnFaulted);

                Close();
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

            private void buttonCancel_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBoxImplementer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void labelImplementer_Click(object sender, EventArgs e)
        {

        }

        private void FormTakeActivityInWork_Load_1(object sender, EventArgs e)
        {
            try
            {
                if (!id.HasValue)
                {
                    MessageBox.Show("Не указан заказ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                List<WorkerViewModel> list = Task.Run(() => APICustomer.GetRequestData<List<WorkerViewModel>>("api/Worker/GetList")).Result;
                if (list != null)
                {
                    comboBoxImplementer.DisplayMember = "WorkerFIO";
                    comboBoxImplementer.ValueMember = "Id";
                    comboBoxImplementer.DataSource = list;
                    comboBoxImplementer.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
