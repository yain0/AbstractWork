using AbstractWorkService.Interfaces;
using AbstractWorkService.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace AbstractWorkView
{
    public partial class FormRemontMaterial : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public RemontMaterialViewModel Model { set { model = value; }  get { return model; } }

        private readonly IMaterialService service;

        private RemontMaterialViewModel model;

        public FormRemontMaterial(IMaterialService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormRemontMaterial_Load(object sender, EventArgs e)
        {
            try
            {
                List<MaterialViewModel> list = service.GetList();
                if (list != null)
                {
                    comboBoxComponent.DisplayMember = "MaterialName";
                    comboBoxComponent.ValueMember = "Id";
                    comboBoxComponent.DataSource = list;
                    comboBoxComponent.SelectedItem = null;
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
