namespace SistemaPeluquería
{
    partial class frmVentas
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
            this.label5 = new System.Windows.Forms.Label();
            this.cmbCódigoCliente = new System.Windows.Forms.ComboBox();
            this.cmbNombreCliente = new System.Windows.Forms.ComboBox();
            this.cmbCódigoArtículo = new System.Windows.Forms.ComboBox();
            this.cmbNombreArtículo = new System.Windows.Forms.ComboBox();
            this.numCantidad = new System.Windows.Forms.NumericUpDown();
            this.btnIngresarVenta = new System.Windows.Forms.Button();
            this.dateFechaVenta = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.numHoras = new System.Windows.Forms.NumericUpDown();
            this.numMinutos = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dataGridViewVentas = new System.Windows.Forms.DataGridView();
            this.idCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idArtículo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pagos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Saldo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtPagos = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnModificarVenta = new System.Windows.Forms.Button();
            this.btnEliminarVenta = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoras)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinutos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVentas)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Código cliente";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(128, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nombre Cliente";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(349, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Código artículo";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(478, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Nombre artículo";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(264, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Cantidad";
            // 
            // cmbCódigoCliente
            // 
            this.cmbCódigoCliente.FormattingEnabled = true;
            this.cmbCódigoCliente.Location = new System.Drawing.Point(17, 34);
            this.cmbCódigoCliente.Name = "cmbCódigoCliente";
            this.cmbCódigoCliente.Size = new System.Drawing.Size(95, 21);
            this.cmbCódigoCliente.TabIndex = 0;
            // 
            // cmbNombreCliente
            // 
            this.cmbNombreCliente.FormattingEnabled = true;
            this.cmbNombreCliente.Location = new System.Drawing.Point(131, 33);
            this.cmbNombreCliente.Name = "cmbNombreCliente";
            this.cmbNombreCliente.Size = new System.Drawing.Size(106, 21);
            this.cmbNombreCliente.TabIndex = 1;
            // 
            // cmbCódigoArtículo
            // 
            this.cmbCódigoArtículo.FormattingEnabled = true;
            this.cmbCódigoArtículo.Location = new System.Drawing.Point(352, 32);
            this.cmbCódigoArtículo.Name = "cmbCódigoArtículo";
            this.cmbCódigoArtículo.Size = new System.Drawing.Size(93, 21);
            this.cmbCódigoArtículo.TabIndex = 3;
            // 
            // cmbNombreArtículo
            // 
            this.cmbNombreArtículo.FormattingEnabled = true;
            this.cmbNombreArtículo.Location = new System.Drawing.Point(481, 34);
            this.cmbNombreArtículo.Name = "cmbNombreArtículo";
            this.cmbNombreArtículo.Size = new System.Drawing.Size(102, 21);
            this.cmbNombreArtículo.TabIndex = 4;
            // 
            // numCantidad
            // 
            this.numCantidad.Location = new System.Drawing.Point(270, 34);
            this.numCantidad.Name = "numCantidad";
            this.numCantidad.Size = new System.Drawing.Size(58, 20);
            this.numCantidad.TabIndex = 2;
            this.numCantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnIngresarVenta
            // 
            this.btnIngresarVenta.Location = new System.Drawing.Point(314, 145);
            this.btnIngresarVenta.Name = "btnIngresarVenta";
            this.btnIngresarVenta.Size = new System.Drawing.Size(131, 31);
            this.btnIngresarVenta.TabIndex = 6;
            this.btnIngresarVenta.Text = "Ingresar Venta";
            this.btnIngresarVenta.UseVisualStyleBackColor = true;
            // 
            // dateFechaVenta
            // 
            this.dateFechaVenta.Location = new System.Drawing.Point(267, 86);
            this.dateFechaVenta.Name = "dateFechaVenta";
            this.dateFechaVenta.Size = new System.Drawing.Size(200, 20);
            this.dateFechaVenta.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(264, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Fecha de la Venta:";
            // 
            // numHoras
            // 
            this.numHoras.Location = new System.Drawing.Point(506, 86);
            this.numHoras.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numHoras.Name = "numHoras";
            this.numHoras.Size = new System.Drawing.Size(34, 20);
            this.numHoras.TabIndex = 22;
            // 
            // numMinutos
            // 
            this.numMinutos.Location = new System.Drawing.Point(561, 86);
            this.numMinutos.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numMinutos.Name = "numMinutos";
            this.numMinutos.Size = new System.Drawing.Size(34, 20);
            this.numMinutos.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(545, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(10, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = ":";
            // 
            // txtPrecio
            // 
            this.txtPrecio.Location = new System.Drawing.Point(17, 89);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(101, 20);
            this.txtPrecio.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "Precio";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(503, 64);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "Hora de la Venta:";
            // 
            // dataGridViewVentas
            // 
            this.dataGridViewVentas.AllowUserToAddRows = false;
            this.dataGridViewVentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewVentas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idCliente,
            this.Cantidad,
            this.idArtículo,
            this.Precio,
            this.Total,
            this.Pagos,
            this.Saldo});
            this.dataGridViewVentas.Location = new System.Drawing.Point(17, 217);
            this.dataGridViewVentas.Name = "dataGridViewVentas";
            this.dataGridViewVentas.Size = new System.Drawing.Size(744, 256);
            this.dataGridViewVentas.TabIndex = 28;
            // 
            // idCliente
            // 
            this.idCliente.HeaderText = "Cliente";
            this.idCliente.Name = "idCliente";
            // 
            // Cantidad
            // 
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.Name = "Cantidad";
            // 
            // idArtículo
            // 
            this.idArtículo.HeaderText = "Artículo";
            this.idArtículo.Name = "idArtículo";
            // 
            // Precio
            // 
            this.Precio.HeaderText = "Precio";
            this.Precio.Name = "Precio";
            // 
            // Total
            // 
            this.Total.HeaderText = "Total (Debe)";
            this.Total.Name = "Total";
            // 
            // Pagos
            // 
            this.Pagos.HeaderText = "Pagos (Haber)";
            this.Pagos.Name = "Pagos";
            // 
            // Saldo
            // 
            this.Saldo.HeaderText = "Saldo";
            this.Saldo.Name = "Saldo";
            // 
            // txtPagos
            // 
            this.txtPagos.Location = new System.Drawing.Point(135, 88);
            this.txtPagos.Name = "txtPagos";
            this.txtPagos.Size = new System.Drawing.Size(95, 20);
            this.txtPagos.TabIndex = 29;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(133, 70);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 13);
            this.label10.TabIndex = 30;
            this.label10.Text = "Pagos (Haber)";
            // 
            // btnModificarVenta
            // 
            this.btnModificarVenta.Location = new System.Drawing.Point(460, 495);
            this.btnModificarVenta.Name = "btnModificarVenta";
            this.btnModificarVenta.Size = new System.Drawing.Size(113, 31);
            this.btnModificarVenta.TabIndex = 31;
            this.btnModificarVenta.Text = "Modificar Venta";
            this.btnModificarVenta.UseVisualStyleBackColor = true;
            // 
            // btnEliminarVenta
            // 
            this.btnEliminarVenta.Location = new System.Drawing.Point(590, 496);
            this.btnEliminarVenta.Name = "btnEliminarVenta";
            this.btnEliminarVenta.Size = new System.Drawing.Size(107, 29);
            this.btnEliminarVenta.TabIndex = 32;
            this.btnEliminarVenta.Text = "Eliminar Venta";
            this.btnEliminarVenta.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(339, 259);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(95, 21);
            this.comboBox1.TabIndex = 33;
            // 
            // frmVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(773, 538);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnEliminarVenta);
            this.Controls.Add(this.btnModificarVenta);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtPagos);
            this.Controls.Add(this.dataGridViewVentas);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtPrecio);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numMinutos);
            this.Controls.Add(this.numHoras);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dateFechaVenta);
            this.Controls.Add(this.btnIngresarVenta);
            this.Controls.Add(this.numCantidad);
            this.Controls.Add(this.cmbNombreArtículo);
            this.Controls.Add(this.cmbCódigoArtículo);
            this.Controls.Add(this.cmbNombreCliente);
            this.Controls.Add(this.cmbCódigoCliente);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmVentas";
            this.Text = "Ventas";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmVentas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoras)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinutos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVentas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbCódigoCliente;
        private System.Windows.Forms.ComboBox cmbNombreCliente;
        private System.Windows.Forms.ComboBox cmbCódigoArtículo;
        private System.Windows.Forms.ComboBox cmbNombreArtículo;
        private System.Windows.Forms.NumericUpDown numCantidad;
        private System.Windows.Forms.Button btnIngresarVenta;
        private System.Windows.Forms.DateTimePicker dateFechaVenta;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numHoras;
        private System.Windows.Forms.NumericUpDown numMinutos;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dataGridViewVentas;
        private System.Windows.Forms.TextBox txtPagos;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Artículo;
        private System.Windows.Forms.DataGridViewTextBoxColumn idCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn idArtículo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pagos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Saldo;
        private System.Windows.Forms.Button btnModificarVenta;
        private System.Windows.Forms.Button btnEliminarVenta;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

