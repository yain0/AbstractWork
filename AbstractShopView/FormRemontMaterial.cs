using AbstractWorkService.Interfaces;
using AbstractWorkService.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AbstractWorkView
{
    public partial class FormRemontMaterial : Form
    {
        public RemontMaterialViewModel Model { set { model = value; }  get { return model; } }
        
        private RemontMaterialViewModel model;

        public FormRemontMaterial()
        {
            InitializeComponent();
        }

        private void FormRemontMaterial_Load(object sender, EventArgs e)
        {
            try
            {
                var response = APICustomer.GetRequest("api/Material/GetList");
                if (response.Result.IsSuccessStatusCode)
                {
                    comboBoxComponent.DisplayMember = "MaterialName";
                    comboBoxComponent.ValueMember = "Id";
                    comboBoxComponent.DataSource = APICustomer.GetElement<List<MaterialViewModel>>(response);
                    comboBoxComponent.SelectedItem = null;
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
            if (model != null)
            {
                comboBoxComponent.Enabled = false;
                comboBoxComponent.SelectedValue = model.MaterialId;
                textBoxCount.Text = model.Koll.ToString();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxComponent.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new RemontMaterialViewModel
                    {
                        MaterialId = Convert.ToInt32(comboBoxComponent.SelectedValue),
                        MaterialName = comboBoxComponent.Text,
                        Koll = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else
                {
                    model.Koll = Convert.ToInt32(textBoxCount.Text);
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void comboBoxComponent_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void labelComponent_Click(object sender, EventArgs e)
        {

        }
    }
}
