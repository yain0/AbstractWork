﻿using AbstractWorkService.BindingModels;
using AbstractWorkService.Interfaces;
using AbstractWorkService.ViewModels;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace AbstractWorkView
{
    public partial class FormCustomer : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly ICustomerService service;

        private int? id;

        public FormCustomer(ICustomerService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormCustomer_Load(object sender, EventArgs e)
        {
           
        }

        private void textBoxFIO_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    service.UpdElement(new CustomerBindingModel
                    {
                        Id = id.Value,
                        CustomerFIO = textBoxFIO.Text
                    });
                }
                else
                {
                    service.AddElement(new CustomerBindingModel
                    {
                        CustomerFIO = textBoxFIO.Text
                    });
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FormCustomer_Load_1(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    CustomerViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxFIO.Text = view.CustomerFIO;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
