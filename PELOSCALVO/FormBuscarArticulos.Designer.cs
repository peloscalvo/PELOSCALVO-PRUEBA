﻿
namespace PELOSCALVO
{
    partial class FormBuscarArticulos
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBuscarArticulos));
            this.TIPObuscarArticulos = new System.Windows.Forms.ComboBox();
            this.BuscarArticulosText = new System.Windows.Forms.TextBox();
            this.DataGridViewBuscarArticulos = new System.Windows.Forms.DataGridView();
            this.referenciaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcciDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pvp1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PvpIva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.familiaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdFILA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtArticulosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.articulos = new PELOSCALVO.Articulos();
            this.label1 = new System.Windows.Forms.Label();
            this.ContadorDatos2 = new System.Windows.Forms.Label();
            this.BtnCancelarBArticulo = new System.Windows.Forms.Button();
            this.FiltrarBajasBuscar = new System.Windows.Forms.ComboBox();
            this.labelfiltrarBUSCAR = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewBuscarArticulos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtArticulosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.articulos)).BeginInit();
            this.SuspendLayout();
            // 
            // TIPObuscarArticulos
            // 
            this.TIPObuscarArticulos.FormattingEnabled = true;
            this.TIPObuscarArticulos.Items.AddRange(new object[] {
            "Todos",
            "Referencia",
            "Descripccion",
            "Familia"});
            this.TIPObuscarArticulos.Location = new System.Drawing.Point(12, 12);
            this.TIPObuscarArticulos.Name = "TIPObuscarArticulos";
            this.TIPObuscarArticulos.Size = new System.Drawing.Size(204, 21);
            this.TIPObuscarArticulos.TabIndex = 2;
            this.TIPObuscarArticulos.Text = "Todos";
            this.TIPObuscarArticulos.SelectedIndexChanged += new System.EventHandler(this.TIPObuscarArticulos_SelectedIndexChanged);
            // 
            // BuscarArticulosText
            // 
            this.BuscarArticulosText.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.BuscarArticulosText.Location = new System.Drawing.Point(248, 12);
            this.BuscarArticulosText.Name = "BuscarArticulosText";
            this.BuscarArticulosText.Size = new System.Drawing.Size(324, 20);
            this.BuscarArticulosText.TabIndex = 1;
            this.BuscarArticulosText.Click += new System.EventHandler(this.BuscarArticulosText_Click);
            this.BuscarArticulosText.TextChanged += new System.EventHandler(this.BuscarArticulosText_TextChanged);
            // 
            // DataGridViewBuscarArticulos
            // 
            this.DataGridViewBuscarArticulos.AllowUserToAddRows = false;
            this.DataGridViewBuscarArticulos.AllowUserToDeleteRows = false;
            this.DataGridViewBuscarArticulos.AllowUserToResizeRows = false;
            this.DataGridViewBuscarArticulos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridViewBuscarArticulos.AutoGenerateColumns = false;
            this.DataGridViewBuscarArticulos.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DataGridViewBuscarArticulos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DataGridViewBuscarArticulos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            this.DataGridViewBuscarArticulos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ButtonShadow;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridViewBuscarArticulos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridViewBuscarArticulos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridViewBuscarArticulos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.referenciaDataGridViewTextBoxColumn,
            this.descripcciDataGridViewTextBoxColumn,
            this.pvp1DataGridViewTextBoxColumn,
            this.PvpIva,
            this.familiaDataGridViewTextBoxColumn,
            this.fechaDataGridViewTextBoxColumn,
            this.IdFILA});
            this.DataGridViewBuscarArticulos.DataSource = this.dtArticulosBindingSource;
            this.DataGridViewBuscarArticulos.Location = new System.Drawing.Point(12, 52);
            this.DataGridViewBuscarArticulos.Name = "DataGridViewBuscarArticulos";
            this.DataGridViewBuscarArticulos.ReadOnly = true;
            this.DataGridViewBuscarArticulos.RowHeadersVisible = false;
            this.DataGridViewBuscarArticulos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridViewBuscarArticulos.Size = new System.Drawing.Size(802, 318);
            this.DataGridViewBuscarArticulos.TabIndex = 6;
            this.DataGridViewBuscarArticulos.TabStop = false;
            this.DataGridViewBuscarArticulos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewBuscarArticulos_CellDoubleClick);
            // 
            // referenciaDataGridViewTextBoxColumn
            // 
            this.referenciaDataGridViewTextBoxColumn.DataPropertyName = "Referencia";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.referenciaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.referenciaDataGridViewTextBoxColumn.FillWeight = 150F;
            this.referenciaDataGridViewTextBoxColumn.HeaderText = "Referencia";
            this.referenciaDataGridViewTextBoxColumn.Name = "referenciaDataGridViewTextBoxColumn";
            this.referenciaDataGridViewTextBoxColumn.ReadOnly = true;
            this.referenciaDataGridViewTextBoxColumn.Width = 150;
            // 
            // descripcciDataGridViewTextBoxColumn
            // 
            this.descripcciDataGridViewTextBoxColumn.DataPropertyName = "Descripcci";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.descripcciDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.descripcciDataGridViewTextBoxColumn.FillWeight = 300F;
            this.descripcciDataGridViewTextBoxColumn.HeaderText = "Descripccion";
            this.descripcciDataGridViewTextBoxColumn.Name = "descripcciDataGridViewTextBoxColumn";
            this.descripcciDataGridViewTextBoxColumn.ReadOnly = true;
            this.descripcciDataGridViewTextBoxColumn.Width = 300;
            // 
            // pvp1DataGridViewTextBoxColumn
            // 
            this.pvp1DataGridViewTextBoxColumn.DataPropertyName = "Pvp1";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = null;
            this.pvp1DataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.pvp1DataGridViewTextBoxColumn.FillWeight = 80F;
            this.pvp1DataGridViewTextBoxColumn.HeaderText = "Pvp1";
            this.pvp1DataGridViewTextBoxColumn.Name = "pvp1DataGridViewTextBoxColumn";
            this.pvp1DataGridViewTextBoxColumn.ReadOnly = true;
            this.pvp1DataGridViewTextBoxColumn.Width = 80;
            // 
            // PvpIva
            // 
            this.PvpIva.DataPropertyName = "PvpIva";
            this.PvpIva.HeaderText = "C/iIva";
            this.PvpIva.Name = "PvpIva";
            this.PvpIva.ReadOnly = true;
            // 
            // familiaDataGridViewTextBoxColumn
            // 
            this.familiaDataGridViewTextBoxColumn.DataPropertyName = "Familia";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.familiaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.familiaDataGridViewTextBoxColumn.FillWeight = 140F;
            this.familiaDataGridViewTextBoxColumn.HeaderText = "Familia";
            this.familiaDataGridViewTextBoxColumn.Name = "familiaDataGridViewTextBoxColumn";
            this.familiaDataGridViewTextBoxColumn.ReadOnly = true;
            this.familiaDataGridViewTextBoxColumn.Width = 140;
            // 
            // fechaDataGridViewTextBoxColumn
            // 
            this.fechaDataGridViewTextBoxColumn.DataPropertyName = "Fecha";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle6.Format = "d";
            dataGridViewCellStyle6.NullValue = null;
            this.fechaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.fechaDataGridViewTextBoxColumn.FillWeight = 85F;
            this.fechaDataGridViewTextBoxColumn.HeaderText = "Fecha";
            this.fechaDataGridViewTextBoxColumn.Name = "fechaDataGridViewTextBoxColumn";
            this.fechaDataGridViewTextBoxColumn.ReadOnly = true;
            this.fechaDataGridViewTextBoxColumn.Width = 85;
            // 
            // IdFILA
            // 
            this.IdFILA.DataPropertyName = "IdFILA";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.DarkRed;
            dataGridViewCellStyle7.Format = "N0";
            dataGridViewCellStyle7.NullValue = null;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Maroon;
            this.IdFILA.DefaultCellStyle = dataGridViewCellStyle7;
            this.IdFILA.HeaderText = "I.D";
            this.IdFILA.Name = "IdFILA";
            this.IdFILA.ReadOnly = true;
            // 
            // dtArticulosBindingSource
            // 
            this.dtArticulosBindingSource.DataMember = "DtArticulos";
            this.dtArticulosBindingSource.DataSource = this.articulos;
            // 
            // articulos
            // 
            this.articulos.DataSetName = "Articulos";
            this.articulos.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(435, 376);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Reguistros #";
            // 
            // ContadorDatos2
            // 
            this.ContadorDatos2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ContadorDatos2.AutoSize = true;
            this.ContadorDatos2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ContadorDatos2.Location = new System.Drawing.Point(545, 376);
            this.ContadorDatos2.Name = "ContadorDatos2";
            this.ContadorDatos2.Size = new System.Drawing.Size(19, 20);
            this.ContadorDatos2.TabIndex = 9;
            this.ContadorDatos2.Text = "0";
            this.ContadorDatos2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BtnCancelarBArticulo
            // 
            this.BtnCancelarBArticulo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancelarBArticulo.BackColor = System.Drawing.Color.Transparent;
            this.BtnCancelarBArticulo.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancelarBArticulo.FlatAppearance.BorderSize = 0;
            this.BtnCancelarBArticulo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RosyBrown;
            this.BtnCancelarBArticulo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.BtnCancelarBArticulo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCancelarBArticulo.Font = new System.Drawing.Font("Bodoni MT", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancelarBArticulo.Image = global::PELOSCALVO.Properties.Resources.iconmonstr_x_mark_8_24;
            this.BtnCancelarBArticulo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnCancelarBArticulo.Location = new System.Drawing.Point(725, 376);
            this.BtnCancelarBArticulo.Name = "BtnCancelarBArticulo";
            this.BtnCancelarBArticulo.Size = new System.Drawing.Size(89, 42);
            this.BtnCancelarBArticulo.TabIndex = 34;
            this.BtnCancelarBArticulo.Text = "Cancelar";
            this.BtnCancelarBArticulo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnCancelarBArticulo.UseVisualStyleBackColor = false;
            this.BtnCancelarBArticulo.Click += new System.EventHandler(this.BtnCancelarBArticulo_Click);
            // 
            // FiltrarBajasBuscar
            // 
            this.FiltrarBajasBuscar.DisplayMember = "Articulos De Alta";
            this.FiltrarBajasBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FiltrarBajasBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FiltrarBajasBuscar.FormattingEnabled = true;
            this.FiltrarBajasBuscar.Items.AddRange(new object[] {
            "Articulos De Alta",
            "Articulos De Baja",
            "Todos"});
            this.FiltrarBajasBuscar.Location = new System.Drawing.Point(72, 386);
            this.FiltrarBajasBuscar.Name = "FiltrarBajasBuscar";
            this.FiltrarBajasBuscar.Size = new System.Drawing.Size(188, 24);
            this.FiltrarBajasBuscar.Sorted = true;
            this.FiltrarBajasBuscar.TabIndex = 39;
            this.FiltrarBajasBuscar.Tag = "";
            this.FiltrarBajasBuscar.ValueMember = "Articulos De Alta";
            this.FiltrarBajasBuscar.Visible = false;
            this.FiltrarBajasBuscar.SelectedIndexChanged += new System.EventHandler(this.FiltrarBajasBuscar_SelectedIndexChanged);
            // 
            // labelfiltrarBUSCAR
            // 
            this.labelfiltrarBUSCAR.AutoSize = true;
            this.labelfiltrarBUSCAR.Location = new System.Drawing.Point(12, 391);
            this.labelfiltrarBUSCAR.Name = "labelfiltrarBUSCAR";
            this.labelfiltrarBUSCAR.Size = new System.Drawing.Size(35, 13);
            this.labelfiltrarBUSCAR.TabIndex = 40;
            this.labelfiltrarBUSCAR.Text = "Filtrar:";
            this.labelfiltrarBUSCAR.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelfiltrarBUSCAR.Visible = false;
            // 
            // FormBuscarArticulos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 430);
            this.ControlBox = false;
            this.Controls.Add(this.labelfiltrarBUSCAR);
            this.Controls.Add(this.FiltrarBajasBuscar);
            this.Controls.Add(this.BtnCancelarBArticulo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ContadorDatos2);
            this.Controls.Add(this.TIPObuscarArticulos);
            this.Controls.Add(this.BuscarArticulosText);
            this.Controls.Add(this.DataGridViewBuscarArticulos);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormBuscarArticulos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Buscar Articulos";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormBuscarArticulosEnFacturas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewBuscarArticulos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtArticulosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.articulos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox TIPObuscarArticulos;
        private System.Windows.Forms.TextBox BuscarArticulosText;
        private System.Windows.Forms.DataGridView DataGridViewBuscarArticulos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ContadorDatos2;
        private System.Windows.Forms.Button BtnCancelarBArticulo;
        private Articulos articulos;
        public System.Windows.Forms.BindingSource dtArticulosBindingSource;
        private System.Windows.Forms.ComboBox FiltrarBajasBuscar;
        private System.Windows.Forms.Label labelfiltrarBUSCAR;
        private System.Windows.Forms.DataGridViewTextBoxColumn referenciaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcciDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pvp1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PvpIva;
        private System.Windows.Forms.DataGridViewTextBoxColumn familiaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdFILA;
    }
}