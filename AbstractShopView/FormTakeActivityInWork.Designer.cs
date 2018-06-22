namespace AbstractWorkView
{
    partial class FormTakeActivityInWork
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.comboBoxImplementer = new System.Windows.Forms.ComboBox();
            this.labelImplementer = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(206, 54);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click_1);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(125, 54);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 12;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click_1);
            // 
            // comboBoxImplementer
            // 
            this.comboBoxImplementer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxImplementer.FormattingEnabled = true;
            this.comboBoxImplementer.Location = new System.Drawing.Point(98, 18);
            this.comboBoxImplementer.Name = "comboBoxImplementer";
            this.comboBoxImplementer.Size = new System.Drawing.Size(217, 21);
            this.comboBoxImplementer.TabIndex = 11;
            this.comboBoxImplementer.SelectedIndexChanged += new System.EventHandler(this.comboBoxImplementer_SelectedIndexChanged);
            // 
            // labelImplementer
            // 
            this.labelImplementer.AutoSize = true;
            this.labelImplementer.Location = new System.Drawing.Point(15, 21);
            this.labelImplementer.Name = "labelImplementer";
            this.labelImplementer.Size = new System.Drawing.Size(77, 13);
            this.labelImplementer.TabIndex = 10;
            this.labelImplementer.Text = "Исполнитель:";
            this.labelImplementer.Click += new System.EventHandler(this.labelImplementer_Click);
            // 
            // FormTakeActivityInWork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 94);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.comboBoxImplementer);
            this.Controls.Add(this.labelImplementer);
            this.Name = "FormTakeActivityInWork";
            this.Text = "Выполнить заказ";
            this.Load += new System.EventHandler(this.FormTakeActivityInWork_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ComboBox comboBoxImplementer;
        private System.Windows.Forms.Label labelImplementer;
    }
}