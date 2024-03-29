﻿
namespace PELOSCALVO
{
    partial class FormEjercicios
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
            System.Windows.Forms.Label tipoInpuestoIVALabel;
            System.Windows.Forms.Label configuraccionBasicaLabel;
            System.Windows.Forms.Label ejerciciosDeAñoLabel;
            System.Windows.Forms.Label empresaENLACELabel;
            System.Windows.Forms.Label idConexionConfiLabel;
            System.Windows.Forms.Label añoDeEjercicioLabel;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label enlaceDtconfiLabel;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEjercicios));
            this.dsCONFIGURACCION = new PELOSCALVO.DsCONFIGURACCION();
            this.dtConfiguracionPrincipalBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dtInicioMultiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsMultidatos = new PELOSCALVO.DsMultidatos();
            this.ErrorEjercicios = new System.Windows.Forms.ErrorProvider(this.components);
            this.dtConfiguracionPrincipalDtConfiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.BtnCancelarEjercicio = new System.Windows.Forms.Button();
            this.BtnGuardarEjercicio = new System.Windows.Forms.Button();
            this.panel1Ejercicio = new System.Windows.Forms.Panel();
            this.BtnEliminarEjercicio = new System.Windows.Forms.Button();
            this.BtnBuscarEjercicio = new System.Windows.Forms.Button();
            this.BtnNuevoEjercicio = new System.Windows.Forms.Button();
            this.BtnModificarEjercicio = new System.Windows.Forms.Button();
            this.IvaEjercicioTxt = new System.Windows.Forms.NumericUpDown();
            this.DescripicionEjer = new System.Windows.Forms.TextBox();
            this.EjercicioTxt = new System.Windows.Forms.TextBox();
            this.AñoTxt = new System.Windows.Forms.TextBox();
            this.dtConfiDataGridView = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AñoDeEjercicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpresaEjercicioTxt = new System.Windows.Forms.ComboBox();
            this.BtnSalirEjerc = new System.Windows.Forms.Button();
            this.IdEmpresa = new System.Windows.Forms.Label();
            this.IdConfi = new System.Windows.Forms.Label();
            this.IdEnlace = new System.Windows.Forms.Label();
            tipoInpuestoIVALabel = new System.Windows.Forms.Label();
            configuraccionBasicaLabel = new System.Windows.Forms.Label();
            ejerciciosDeAñoLabel = new System.Windows.Forms.Label();
            empresaENLACELabel = new System.Windows.Forms.Label();
            idConexionConfiLabel = new System.Windows.Forms.Label();
            añoDeEjercicioLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            enlaceDtconfiLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dsCONFIGURACCION)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtConfiguracionPrincipalBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtInicioMultiBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsMultidatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorEjercicios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtConfiguracionPrincipalDtConfiBindingSource)).BeginInit();
            this.panel1Ejercicio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IvaEjercicioTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtConfiDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tipoInpuestoIVALabel
            // 
            tipoInpuestoIVALabel.AutoSize = true;
            tipoInpuestoIVALabel.Location = new System.Drawing.Point(6, 401);
            tipoInpuestoIVALabel.Name = "tipoInpuestoIVALabel";
            tipoInpuestoIVALabel.Size = new System.Drawing.Size(71, 13);
            tipoInpuestoIVALabel.TabIndex = 69;
            tipoInpuestoIVALabel.Text = "Inpuesto IVA:";
            // 
            // configuraccionBasicaLabel
            // 
            configuraccionBasicaLabel.AutoSize = true;
            configuraccionBasicaLabel.Location = new System.Drawing.Point(9, 348);
            configuraccionBasicaLabel.Name = "configuraccionBasicaLabel";
            configuraccionBasicaLabel.Size = new System.Drawing.Size(72, 13);
            configuraccionBasicaLabel.TabIndex = 59;
            configuraccionBasicaLabel.Text = "Descripccion:";
            // 
            // ejerciciosDeAñoLabel
            // 
            ejerciciosDeAñoLabel.AutoSize = true;
            ejerciciosDeAñoLabel.Location = new System.Drawing.Point(22, 374);
            ejerciciosDeAñoLabel.Name = "ejerciciosDeAñoLabel";
            ejerciciosDeAñoLabel.Size = new System.Drawing.Size(55, 13);
            ejerciciosDeAñoLabel.TabIndex = 61;
            ejerciciosDeAñoLabel.Text = "Ejercicios:";
            // 
            // empresaENLACELabel
            // 
            empresaENLACELabel.AutoSize = true;
            empresaENLACELabel.Location = new System.Drawing.Point(502, 326);
            empresaENLACELabel.Name = "empresaENLACELabel";
            empresaENLACELabel.Size = new System.Drawing.Size(19, 13);
            empresaENLACELabel.TabIndex = 63;
            empresaENLACELabel.Text = "Id:";
            // 
            // idConexionConfiLabel
            // 
            idConexionConfiLabel.AutoSize = true;
            idConexionConfiLabel.Location = new System.Drawing.Point(59, 321);
            idConexionConfiLabel.Name = "idConexionConfiLabel";
            idConexionConfiLabel.Size = new System.Drawing.Size(19, 13);
            idConexionConfiLabel.TabIndex = 65;
            idConexionConfiLabel.Text = "Id:";
            // 
            // añoDeEjercicioLabel
            // 
            añoDeEjercicioLabel.AutoSize = true;
            añoDeEjercicioLabel.Location = new System.Drawing.Point(316, 374);
            añoDeEjercicioLabel.Name = "añoDeEjercicioLabel";
            añoDeEjercicioLabel.Size = new System.Drawing.Size(29, 13);
            añoDeEjercicioLabel.TabIndex = 67;
            añoDeEjercicioLabel.Text = "Año:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(470, 295);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(51, 13);
            label1.TabIndex = 56;
            label1.Text = "Empresa:";
            // 
            // enlaceDtconfiLabel
            // 
            enlaceDtconfiLabel.AutoSize = true;
            enlaceDtconfiLabel.Location = new System.Drawing.Point(6, 278);
            enlaceDtconfiLabel.Name = "enlaceDtconfiLabel";
            enlaceDtconfiLabel.Size = new System.Drawing.Size(47, 13);
            enlaceDtconfiLabel.TabIndex = 78;
            enlaceDtconfiLabel.Text = "Numero:";
            // 
            // dsCONFIGURACCION
            // 
            this.dsCONFIGURACCION.DataSetName = "DsCONFIGURACCION";
            this.dsCONFIGURACCION.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dtConfiguracionPrincipalBindingSource
            // 
            this.dtConfiguracionPrincipalBindingSource.DataMember = "DtConfiguracionPrincipal";
            this.dtConfiguracionPrincipalBindingSource.DataSource = this.dsCONFIGURACCION;
            // 
            // dtInicioMultiBindingSource
            // 
            this.dtInicioMultiBindingSource.DataMember = "DtInicioMulti";
            this.dtInicioMultiBindingSource.DataSource = this.dsMultidatos;
            // 
            // dsMultidatos
            // 
            this.dsMultidatos.DataSetName = "DsMultidatos";
            this.dsMultidatos.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ErrorEjercicios
            // 
            this.ErrorEjercicios.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
            this.ErrorEjercicios.ContainerControl = this;
            // 
            // dtConfiguracionPrincipalDtConfiBindingSource
            // 
            this.dtConfiguracionPrincipalDtConfiBindingSource.DataMember = "FK_DtConfiguracionPrincipal_DtConfi";
            this.dtConfiguracionPrincipalDtConfiBindingSource.DataSource = this.dtConfiguracionPrincipalBindingSource;
            // 
            // BtnCancelarEjercicio
            // 
            this.BtnCancelarEjercicio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancelarEjercicio.BackColor = System.Drawing.Color.Transparent;
            this.BtnCancelarEjercicio.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancelarEjercicio.Enabled = false;
            this.BtnCancelarEjercicio.FlatAppearance.BorderSize = 0;
            this.BtnCancelarEjercicio.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RosyBrown;
            this.BtnCancelarEjercicio.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.BtnCancelarEjercicio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCancelarEjercicio.Font = new System.Drawing.Font("Bodoni MT", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancelarEjercicio.Image = global::PELOSCALVO.Properties.Resources.Cancelar;
            this.BtnCancelarEjercicio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnCancelarEjercicio.Location = new System.Drawing.Point(675, 458);
            this.BtnCancelarEjercicio.Name = "BtnCancelarEjercicio";
            this.BtnCancelarEjercicio.Size = new System.Drawing.Size(89, 42);
            this.BtnCancelarEjercicio.TabIndex = 73;
            this.BtnCancelarEjercicio.Text = "Cancelar";
            this.BtnCancelarEjercicio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnCancelarEjercicio.UseVisualStyleBackColor = false;
            this.BtnCancelarEjercicio.Click += new System.EventHandler(this.BtnCancelarEjercicio_Click);
            // 
            // BtnGuardarEjercicio
            // 
            this.BtnGuardarEjercicio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnGuardarEjercicio.BackColor = System.Drawing.Color.Transparent;
            this.BtnGuardarEjercicio.Enabled = false;
            this.BtnGuardarEjercicio.FlatAppearance.BorderSize = 0;
            this.BtnGuardarEjercicio.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RosyBrown;
            this.BtnGuardarEjercicio.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.BtnGuardarEjercicio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnGuardarEjercicio.Font = new System.Drawing.Font("Bodoni MT", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGuardarEjercicio.Image = global::PELOSCALVO.Properties.Resources.Aceptar;
            this.BtnGuardarEjercicio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnGuardarEjercicio.Location = new System.Drawing.Point(460, 458);
            this.BtnGuardarEjercicio.Name = "BtnGuardarEjercicio";
            this.BtnGuardarEjercicio.Size = new System.Drawing.Size(89, 42);
            this.BtnGuardarEjercicio.TabIndex = 72;
            this.BtnGuardarEjercicio.Text = "Aceptar";
            this.BtnGuardarEjercicio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnGuardarEjercicio.UseVisualStyleBackColor = false;
            this.BtnGuardarEjercicio.Click += new System.EventHandler(this.BtnGuardarEjercicio_Click);
            // 
            // panel1Ejercicio
            // 
            this.panel1Ejercicio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1Ejercicio.BackColor = System.Drawing.Color.Transparent;
            this.panel1Ejercicio.Controls.Add(this.BtnEliminarEjercicio);
            this.panel1Ejercicio.Controls.Add(this.BtnBuscarEjercicio);
            this.panel1Ejercicio.Controls.Add(this.BtnNuevoEjercicio);
            this.panel1Ejercicio.Controls.Add(this.BtnModificarEjercicio);
            this.panel1Ejercicio.Location = new System.Drawing.Point(13, 437);
            this.panel1Ejercicio.Name = "panel1Ejercicio";
            this.panel1Ejercicio.Size = new System.Drawing.Size(396, 63);
            this.panel1Ejercicio.TabIndex = 71;
            // 
            // BtnEliminarEjercicio
            // 
            this.BtnEliminarEjercicio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnEliminarEjercicio.BackColor = System.Drawing.Color.Transparent;
            this.BtnEliminarEjercicio.FlatAppearance.BorderSize = 0;
            this.BtnEliminarEjercicio.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RosyBrown;
            this.BtnEliminarEjercicio.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.BtnEliminarEjercicio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnEliminarEjercicio.Font = new System.Drawing.Font("Bodoni MT", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEliminarEjercicio.Image = global::PELOSCALVO.Properties.Resources.Papelera;
            this.BtnEliminarEjercicio.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.BtnEliminarEjercicio.Location = new System.Drawing.Point(238, 3);
            this.BtnEliminarEjercicio.Name = "BtnEliminarEjercicio";
            this.BtnEliminarEjercicio.Size = new System.Drawing.Size(77, 63);
            this.BtnEliminarEjercicio.TabIndex = 9;
            this.BtnEliminarEjercicio.Text = "Eliminar";
            this.BtnEliminarEjercicio.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BtnEliminarEjercicio.UseVisualStyleBackColor = false;
            this.BtnEliminarEjercicio.Click += new System.EventHandler(this.BtnEliminarEjercicio_Click);
            // 
            // BtnBuscarEjercicio
            // 
            this.BtnBuscarEjercicio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnBuscarEjercicio.BackColor = System.Drawing.Color.Transparent;
            this.BtnBuscarEjercicio.FlatAppearance.BorderSize = 0;
            this.BtnBuscarEjercicio.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RosyBrown;
            this.BtnBuscarEjercicio.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.BtnBuscarEjercicio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBuscarEjercicio.Font = new System.Drawing.Font("Bodoni MT", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBuscarEjercicio.Image = global::PELOSCALVO.Properties.Resources.Lupa_Grande;
            this.BtnBuscarEjercicio.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.BtnBuscarEjercicio.Location = new System.Drawing.Point(161, 3);
            this.BtnBuscarEjercicio.Name = "BtnBuscarEjercicio";
            this.BtnBuscarEjercicio.Size = new System.Drawing.Size(77, 63);
            this.BtnBuscarEjercicio.TabIndex = 8;
            this.BtnBuscarEjercicio.Text = "Buscar";
            this.BtnBuscarEjercicio.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BtnBuscarEjercicio.UseVisualStyleBackColor = false;
            this.BtnBuscarEjercicio.Click += new System.EventHandler(this.BtnBuscarEjercicio_Click);
            // 
            // BtnNuevoEjercicio
            // 
            this.BtnNuevoEjercicio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnNuevoEjercicio.BackColor = System.Drawing.Color.Transparent;
            this.BtnNuevoEjercicio.FlatAppearance.BorderSize = 0;
            this.BtnNuevoEjercicio.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RosyBrown;
            this.BtnNuevoEjercicio.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.BtnNuevoEjercicio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnNuevoEjercicio.Font = new System.Drawing.Font("Bodoni MT", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNuevoEjercicio.Image = global::PELOSCALVO.Properties.Resources.ArchivoFile;
            this.BtnNuevoEjercicio.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.BtnNuevoEjercicio.Location = new System.Drawing.Point(7, 3);
            this.BtnNuevoEjercicio.Name = "BtnNuevoEjercicio";
            this.BtnNuevoEjercicio.Size = new System.Drawing.Size(77, 63);
            this.BtnNuevoEjercicio.TabIndex = 6;
            this.BtnNuevoEjercicio.Tag = "stop";
            this.BtnNuevoEjercicio.Text = "Nuevo";
            this.BtnNuevoEjercicio.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BtnNuevoEjercicio.UseVisualStyleBackColor = false;
            this.BtnNuevoEjercicio.Click += new System.EventHandler(this.BtnNuevoEjercicio_Click);
            // 
            // BtnModificarEjercicio
            // 
            this.BtnModificarEjercicio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnModificarEjercicio.BackColor = System.Drawing.Color.Transparent;
            this.BtnModificarEjercicio.FlatAppearance.BorderSize = 0;
            this.BtnModificarEjercicio.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RosyBrown;
            this.BtnModificarEjercicio.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.BtnModificarEjercicio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnModificarEjercicio.Font = new System.Drawing.Font("Bodoni MT", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnModificarEjercicio.Image = global::PELOSCALVO.Properties.Resources.Editar;
            this.BtnModificarEjercicio.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.BtnModificarEjercicio.Location = new System.Drawing.Point(84, 3);
            this.BtnModificarEjercicio.Name = "BtnModificarEjercicio";
            this.BtnModificarEjercicio.Size = new System.Drawing.Size(77, 63);
            this.BtnModificarEjercicio.TabIndex = 7;
            this.BtnModificarEjercicio.Text = "Modificar";
            this.BtnModificarEjercicio.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BtnModificarEjercicio.UseVisualStyleBackColor = false;
            this.BtnModificarEjercicio.Click += new System.EventHandler(this.BtnModificarEjercicio_Click);
            // 
            // IvaEjercicioTxt
            // 
            this.IvaEjercicioTxt.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.dtConfiguracionPrincipalDtConfiBindingSource, "TipoInpuestoIVA", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, null, "N0"));
            this.IvaEjercicioTxt.Enabled = false;
            this.IvaEjercicioTxt.Location = new System.Drawing.Point(83, 397);
            this.IvaEjercicioTxt.Name = "IvaEjercicioTxt";
            this.IvaEjercicioTxt.Size = new System.Drawing.Size(100, 20);
            this.IvaEjercicioTxt.TabIndex = 70;
            this.IvaEjercicioTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // DescripicionEjer
            // 
            this.DescripicionEjer.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.dtConfiguracionPrincipalDtConfiBindingSource, "ConfiguraccionBasica", true));
            this.DescripicionEjer.Location = new System.Drawing.Point(83, 345);
            this.DescripicionEjer.MaxLength = 50;
            this.DescripicionEjer.Name = "DescripicionEjer";
            this.DescripicionEjer.ReadOnly = true;
            this.DescripicionEjer.Size = new System.Drawing.Size(377, 20);
            this.DescripicionEjer.TabIndex = 60;
            // 
            // EjercicioTxt
            // 
            this.EjercicioTxt.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.dtConfiguracionPrincipalDtConfiBindingSource, "EjerciciosDeAño", true));
            this.EjercicioTxt.Location = new System.Drawing.Point(83, 371);
            this.EjercicioTxt.MaxLength = 50;
            this.EjercicioTxt.Name = "EjercicioTxt";
            this.EjercicioTxt.ReadOnly = true;
            this.EjercicioTxt.Size = new System.Drawing.Size(221, 20);
            this.EjercicioTxt.TabIndex = 62;
            // 
            // AñoTxt
            // 
            this.AñoTxt.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.dtConfiguracionPrincipalDtConfiBindingSource, "AñoDeEjercicio", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "d"));
            this.AñoTxt.Location = new System.Drawing.Point(351, 371);
            this.AñoTxt.MaxLength = 4;
            this.AñoTxt.Name = "AñoTxt";
            this.AñoTxt.ReadOnly = true;
            this.AñoTxt.Size = new System.Drawing.Size(109, 20);
            this.AñoTxt.TabIndex = 68;
            // 
            // dtConfiDataGridView
            // 
            this.dtConfiDataGridView.AllowUserToAddRows = false;
            this.dtConfiDataGridView.AllowUserToResizeRows = false;
            this.dtConfiDataGridView.AutoGenerateColumns = false;
            this.dtConfiDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtConfiDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtConfiDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtConfiDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn6,
            this.AñoDeEjercicio});
            this.dtConfiDataGridView.DataSource = this.dtConfiguracionPrincipalDtConfiBindingSource;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(90)))), ((int)(((byte)(1)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtConfiDataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.dtConfiDataGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.dtConfiDataGridView.Location = new System.Drawing.Point(0, 0);
            this.dtConfiDataGridView.Name = "dtConfiDataGridView";
            this.dtConfiDataGridView.ReadOnly = true;
            this.dtConfiDataGridView.RowHeadersVisible = false;
            this.dtConfiDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtConfiDataGridView.Size = new System.Drawing.Size(927, 265);
            this.dtConfiDataGridView.TabIndex = 58;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "IdConexionConfi";
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            this.Id.DefaultCellStyle = dataGridViewCellStyle2;
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "ConfiguraccionBasica";
            this.dataGridViewTextBoxColumn4.FillWeight = 180F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Nombre";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "EjerciciosDeAño";
            this.dataGridViewTextBoxColumn6.FillWeight = 130F;
            this.dataGridViewTextBoxColumn6.HeaderText = "Ejercicios";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // AñoDeEjercicio
            // 
            this.AñoDeEjercicio.DataPropertyName = "AñoDeEjercicio";
            this.AñoDeEjercicio.HeaderText = "AñoDeEjercicio";
            this.AñoDeEjercicio.Name = "AñoDeEjercicio";
            this.AñoDeEjercicio.ReadOnly = true;
            this.AñoDeEjercicio.Visible = false;
            // 
            // EmpresaEjercicioTxt
            // 
            this.EmpresaEjercicioTxt.DataSource = this.dtConfiguracionPrincipalBindingSource;
            this.EmpresaEjercicioTxt.DisplayMember = "EmpresaConfi";
            this.EmpresaEjercicioTxt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EmpresaEjercicioTxt.FormattingEnabled = true;
            this.EmpresaEjercicioTxt.Location = new System.Drawing.Point(527, 292);
            this.EmpresaEjercicioTxt.Name = "EmpresaEjercicioTxt";
            this.EmpresaEjercicioTxt.Size = new System.Drawing.Size(388, 21);
            this.EmpresaEjercicioTxt.TabIndex = 57;
            // 
            // BtnSalirEjerc
            // 
            this.BtnSalirEjerc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSalirEjerc.BackColor = System.Drawing.Color.Transparent;
            this.BtnSalirEjerc.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnSalirEjerc.FlatAppearance.BorderSize = 0;
            this.BtnSalirEjerc.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RosyBrown;
            this.BtnSalirEjerc.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.BtnSalirEjerc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSalirEjerc.Font = new System.Drawing.Font("Bodoni MT", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSalirEjerc.Image = global::PELOSCALVO.Properties.Resources.Salir;
            this.BtnSalirEjerc.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.BtnSalirEjerc.Location = new System.Drawing.Point(825, 430);
            this.BtnSalirEjerc.Name = "BtnSalirEjerc";
            this.BtnSalirEjerc.Size = new System.Drawing.Size(77, 63);
            this.BtnSalirEjerc.TabIndex = 74;
            this.BtnSalirEjerc.Text = "Salir";
            this.BtnSalirEjerc.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BtnSalirEjerc.UseVisualStyleBackColor = false;
            this.BtnSalirEjerc.Click += new System.EventHandler(this.BtnSalirEjerc_Click);
            // 
            // IdEmpresa
            // 
            this.IdEmpresa.AutoSize = true;
            this.IdEmpresa.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.dtConfiguracionPrincipalBindingSource, "IdEmpresa", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N0"));
            this.IdEmpresa.Location = new System.Drawing.Point(535, 326);
            this.IdEmpresa.Name = "IdEmpresa";
            this.IdEmpresa.Size = new System.Drawing.Size(0, 13);
            this.IdEmpresa.TabIndex = 77;
            // 
            // IdConfi
            // 
            this.IdConfi.AutoSize = true;
            this.IdConfi.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.dtConfiguracionPrincipalDtConfiBindingSource, "IdConexionConfi", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N0"));
            this.IdConfi.Location = new System.Drawing.Point(84, 321);
            this.IdConfi.Name = "IdConfi";
            this.IdConfi.Size = new System.Drawing.Size(13, 13);
            this.IdConfi.TabIndex = 78;
            this.IdConfi.Text = "1";
            // 
            // IdEnlace
            // 
            this.IdEnlace.AutoSize = true;
            this.IdEnlace.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.dtConfiguracionPrincipalDtConfiBindingSource, "IdEnlace", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N0"));
            this.IdEnlace.Location = new System.Drawing.Point(63, 278);
            this.IdEnlace.Name = "IdEnlace";
            this.IdEnlace.Size = new System.Drawing.Size(0, 13);
            this.IdEnlace.TabIndex = 79;
            // 
            // FormEjercicios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 530);
            this.Controls.Add(enlaceDtconfiLabel);
            this.Controls.Add(this.IdEnlace);
            this.Controls.Add(this.IdConfi);
            this.Controls.Add(this.IdEmpresa);
            this.Controls.Add(this.BtnSalirEjerc);
            this.Controls.Add(this.BtnCancelarEjercicio);
            this.Controls.Add(this.BtnGuardarEjercicio);
            this.Controls.Add(this.panel1Ejercicio);
            this.Controls.Add(tipoInpuestoIVALabel);
            this.Controls.Add(this.IvaEjercicioTxt);
            this.Controls.Add(configuraccionBasicaLabel);
            this.Controls.Add(this.DescripicionEjer);
            this.Controls.Add(this.EjercicioTxt);
            this.Controls.Add(this.AñoTxt);
            this.Controls.Add(ejerciciosDeAñoLabel);
            this.Controls.Add(empresaENLACELabel);
            this.Controls.Add(idConexionConfiLabel);
            this.Controls.Add(añoDeEjercicioLabel);
            this.Controls.Add(this.dtConfiDataGridView);
            this.Controls.Add(label1);
            this.Controls.Add(this.EmpresaEjercicioTxt);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormEjercicios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ejercicios";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEjercicios_FormClosing);
            this.Load += new System.EventHandler(this.FormEjercicios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dsCONFIGURACCION)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtConfiguracionPrincipalBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtInicioMultiBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsMultidatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorEjercicios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtConfiguracionPrincipalDtConfiBindingSource)).EndInit();
            this.panel1Ejercicio.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.IvaEjercicioTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtConfiDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DsCONFIGURACCION dsCONFIGURACCION;
        private System.Windows.Forms.BindingSource dtConfiguracionPrincipalBindingSource;
        private System.Windows.Forms.BindingSource dtInicioMultiBindingSource;
        private DsMultidatos dsMultidatos;
        private System.Windows.Forms.ErrorProvider ErrorEjercicios;
        private System.Windows.Forms.BindingSource dtConfiguracionPrincipalDtConfiBindingSource;
        private System.Windows.Forms.Button BtnCancelarEjercicio;
        public System.Windows.Forms.Button BtnGuardarEjercicio;
        private System.Windows.Forms.Panel panel1Ejercicio;
        private System.Windows.Forms.Button BtnEliminarEjercicio;
        private System.Windows.Forms.Button BtnBuscarEjercicio;
        private System.Windows.Forms.Button BtnNuevoEjercicio;
        private System.Windows.Forms.Button BtnModificarEjercicio;
        private System.Windows.Forms.NumericUpDown IvaEjercicioTxt;
        private System.Windows.Forms.TextBox DescripicionEjer;
        private System.Windows.Forms.TextBox EjercicioTxt;
        private System.Windows.Forms.TextBox AñoTxt;
        private System.Windows.Forms.DataGridView dtConfiDataGridView;
        private System.Windows.Forms.ComboBox EmpresaEjercicioTxt;
        private System.Windows.Forms.Button BtnSalirEjerc;
        private System.Windows.Forms.Label IdEmpresa;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn AñoDeEjercicio;
        private System.Windows.Forms.Label IdConfi;
        private System.Windows.Forms.Label IdEnlace;
    }
}