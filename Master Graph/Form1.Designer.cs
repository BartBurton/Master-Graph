
namespace MasterGraph
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Plane = new System.Windows.Forms.Panel();
            this.LabelCenters = new System.Windows.Forms.Label();
            this.LabelMarks = new System.Windows.Forms.Label();
            this.LabelGridSize = new System.Windows.Forms.Label();
            this.LabelSize = new System.Windows.Forms.Label();
            this.LabelIncrement = new System.Windows.Forms.Label();
            this.LabelHelp = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Plane
            // 
            this.Plane.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Plane.Location = new System.Drawing.Point(9, 84);
            this.Plane.Margin = new System.Windows.Forms.Padding(0);
            this.Plane.Name = "Plane";
            this.Plane.Size = new System.Drawing.Size(960, 340);
            this.Plane.TabIndex = 0;
            // 
            // LabelCenters
            // 
            this.LabelCenters.AutoSize = true;
            this.LabelCenters.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelCenters.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.LabelCenters.Location = new System.Drawing.Point(5, 9);
            this.LabelCenters.Name = "LabelCenters";
            this.LabelCenters.Size = new System.Drawing.Size(83, 28);
            this.LabelCenters.TabIndex = 1;
            this.LabelCenters.Text = "Центры";
            // 
            // LabelMarks
            // 
            this.LabelMarks.AutoSize = true;
            this.LabelMarks.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelMarks.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.LabelMarks.Location = new System.Drawing.Point(143, 9);
            this.LabelMarks.Name = "LabelMarks";
            this.LabelMarks.Size = new System.Drawing.Size(89, 28);
            this.LabelMarks.TabIndex = 2;
            this.LabelMarks.Text = "Отметки";
            // 
            // LabelGridSize
            // 
            this.LabelGridSize.AutoSize = true;
            this.LabelGridSize.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelGridSize.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.LabelGridSize.Location = new System.Drawing.Point(281, 9);
            this.LabelGridSize.Name = "LabelGridSize";
            this.LabelGridSize.Size = new System.Drawing.Size(62, 28);
            this.LabelGridSize.TabIndex = 3;
            this.LabelGridSize.Text = "Cетка";
            // 
            // LabelSize
            // 
            this.LabelSize.AutoSize = true;
            this.LabelSize.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelSize.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.LabelSize.Location = new System.Drawing.Point(395, 9);
            this.LabelSize.Name = "LabelSize";
            this.LabelSize.Size = new System.Drawing.Size(78, 28);
            this.LabelSize.TabIndex = 4;
            this.LabelSize.Text = "Размер";
            // 
            // LabelIncrement
            // 
            this.LabelIncrement.AutoSize = true;
            this.LabelIncrement.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelIncrement.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.LabelIncrement.Location = new System.Drawing.Point(522, 9);
            this.LabelIncrement.Name = "LabelIncrement";
            this.LabelIncrement.Size = new System.Drawing.Size(115, 28);
            this.LabelIncrement.TabIndex = 5;
            this.LabelIncrement.Text = "Инкремент";
            // 
            // LabelHelp
            // 
            this.LabelHelp.AutoSize = true;
            this.LabelHelp.Font = new System.Drawing.Font("Segoe UI Black", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LabelHelp.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.LabelHelp.Location = new System.Drawing.Point(694, 2);
            this.LabelHelp.Name = "LabelHelp";
            this.LabelHelp.Size = new System.Drawing.Size(29, 38);
            this.LabelHelp.TabIndex = 6;
            this.LabelHelp.Text = "?";
            this.LabelHelp.Click += new System.EventHandler(this.LabelHelp_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(982, 438);
            this.Controls.Add(this.LabelHelp);
            this.Controls.Add(this.LabelIncrement);
            this.Controls.Add(this.LabelSize);
            this.Controls.Add(this.LabelGridSize);
            this.Controls.Add(this.LabelMarks);
            this.Controls.Add(this.LabelCenters);
            this.Controls.Add(this.Plane);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "WORK";
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Plane;
        private System.Windows.Forms.Label LabelCenters;
        private System.Windows.Forms.Label LabelMarks;
        private System.Windows.Forms.Label LabelGridSize;
        private System.Windows.Forms.Label LabelSize;
        private System.Windows.Forms.Label LabelIncrement;
        private System.Windows.Forms.Label LabelHelp;
    }
}

