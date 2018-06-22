﻿using AbstractWorkService.BindingModels;
using AbstractWorkService.Interfaces;
using AbstractWorkService.ViewModels;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbstractWorkView
{
    public partial class FormWorker : Form
    {
        public int Id { set { id = value; } }

        private int? id;

        public FormWorker()
        {
            InitializeComponent();
        }

        private void FormWorker_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var implementer = Task.Run(() => APICustomer.GetRequestData<WorkerViewModel>("api/Worker/Get/" + id.Value)).Result;
                    textBoxFIO.Text = implementer.WorkerFIO;
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string fio = textBoxFIO.Text;
            Task task;
            if (id.HasValue)
            {
                task = Task.Run(() => APICustomer.PostRequestData("api/Worker/UpdElement", new WorkerBindingModel
                {
                    Id = id.Value,
                    WorkerFIO = fio
                }));
            }
            else
            {
                task = Task.Run(() => APICustomer.PostRequestData("api/Worker/AddElement", new WorkerBindingModel
                {
                    WorkerFIO = fio
                }));
            }

            task.ContinueWith((prevTask) => MessageBox.Show("Сохранение прошло успешно. Обновите список", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information),
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
