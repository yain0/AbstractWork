﻿using AbstractWorkService.BindingModels;
using AbstractWorkService.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AbstractWorkView
{
    public partial class FormCreateActivity : Form
    {
        

        public FormCreateActivity()
        {
            InitializeComponent();
        }

        private void FormCreateActivity_Load(object sender, EventArgs e)
        {
            try
            {
                var responseC = APICustomer.GetRequest("api/Customer/GetList");
                if (responseC.Result.IsSuccessStatusCode)
                {
                    List<CustomerViewModel> list = APICustomer.GetElement<List<CustomerViewModel>>(responseC);
                    if (list != null)
                    {
                        comboBoxClient.DisplayMember = "CustomerFIO";
                        comboBoxClient.ValueMember = "Id";
                        comboBoxClient.DataSource = list;
                        comboBoxClient.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APICustomer.GetError(responseC));
                }
                var responseP = APICustomer.GetRequest("api/Remont/GetList");
                if (responseP.Result.IsSuccessStatusCode)
                {
                    List<RemontViewModel> list = APICustomer.GetElement<List<RemontViewModel>>(responseP);
                    if (list != null)
                    {
                        comboBoxProduct.DisplayMember = "RemontName";
                        comboBoxProduct.ValueMember = "Id";
                        comboBoxProduct.DataSource = list;
                        comboBoxProduct.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APICustomer.GetError(responseP));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcSum()
        {
            if (comboBoxProduct.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxProduct.SelectedValue);
                    var responseP = APICustomer.GetRequest("api/Remont/Get/" + id);
                    if (responseP.Result.IsSuccessStatusCode)
                    {
                        RemontViewModel product = APICustomer.GetElement<RemontViewModel>(responseP);
                        int count = Convert.ToInt32(textBoxCount.Text);
                        textBoxSum.Text = (count * (int)product.Cost).ToString();
                    }
                    else
                    {
                        throw new Exception(APICustomer.GetError(responseP));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBoxCount_TextChanged_1(object sender, EventArgs e)
        {
            
        }

        private void textBoxSum_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void comboBoxClient_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxProduct_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
        }

        private void buttonCancel_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FormCreateActivity_Load_1(object sender, EventArgs e)
        {
            
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxClient.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxProduct.SelectedValue == null)
            {
                MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var response = APICustomer.PostRequest("api/My/CreateActivity", new ActivityBindingModel
                {
                    CustomerId = Convert.ToInt32(comboBoxClient.SelectedValue),
                    RemontId = Convert.ToInt32(comboBoxProduct.SelectedValue),
                    Koll = Convert.ToInt32(textBoxCount.Text),
                    Summa = Convert.ToDecimal(textBoxSum.Text)
                });
                if (response.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    throw new Exception(APICustomer.GetError(response));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBoxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void textBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }
    }
}
