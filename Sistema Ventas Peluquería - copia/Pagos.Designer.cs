namespace SistemaPeluquería
{
    partial class Pagos
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbCódigo = new System.Windows.Forms.ComboBox();
            this.cmbNombre = new System.Windows.Forms.ComboBox();
            this.txtPago = new System.Windows.Forms.TextBox();
            this.dateFecha = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbApellido = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre Cliente";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Pago";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Fecha";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Código Cliente";
            // 
            // cmbCódigo
            // 
            this.cmbCódigo.FormattingEnabled = true;
            this.cmbCódigo.Location = new System.Drawing.Point(36, 69);
            this.cmbCódigo.Name = "cmbCódigo";
            this.cmbCódigo.Size = new System.Drawing.Size(121, 21);
            this.cmbCódigo.TabIndex = 4;
            // 
            // cmbNombre
            // 
            this.cmbNombre.FormattingEnabled = true;
            this.cmbNombre.Location = new System.Drawing.Point(36, 124);
            this.cmbNombre.Name = "cmbNombre";
            this.cmbNombre.Size = new System.Drawing.Size(121, 21);
            this.cmbNombre.TabIndex = 5;
            // 
            // txtPago
            // 
            this.txtPago.Location = new System.Drawing.Point(36, 174);
            this.txtPago.Name = "txtPago";
            this.txtPago.Size = new System.Drawing.Size(100, 20);
            this.txtPago.TabIndex = 6;
            // 
            // dateFecha
            // 
            this.dateFecha.Location = new System.Drawing.Point(36, 231);
            this.dateFecha.Name = "dateFecha";
            this.dateFecha.Size = new System.Drawing.Size(203, 20);
            this.dateFecha.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(82, 282);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(157, 33);
            this.button1.TabIndex = 8;
            this.button1.Text = "Ingresar Pago";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbApellido
            // 
            this.cmbApellido.FormattingEnabled = true;
            this.cmbApellido.Location = new System.Drawing.Point(178, 124);
            this.cmbApellido.Name = "cmbApellido";
            this.cmbApellido.Size = new System.Drawing.Size(121, 21);
            this.cmbApellido.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(175, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Nombre Cliente";
            // 
            // Pagos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 344);
            this.Controls.Add(this.cmbApellido);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dateFecha);
            this.Controls.Add(this.txtPago);
            this.Controls.Add(this.cmbNombre);
            this.Controls.Add(this.cmbCódigo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Pagos";
            this.Text = "Pagos recibidos";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbCódigo;
        private System.Windows.Forms.ComboBox cmbNombre;
        private System.Windows.Forms.TextBox txtPago;
        private System.Windows.Forms.DateTimePicker dateFecha;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbApellido;
        private System.Windows.Forms.Label label5;
    }
}