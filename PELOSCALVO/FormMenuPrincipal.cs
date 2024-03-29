﻿
using Comun;
using Conexiones;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace PELOSCALVO
{
    public partial class FormMenuPrincipal : Form
    {
        // public BindingSource CorreosBindisource = new BindingSource();
        public static FormMenuPrincipal menu2principal;
        public byte SiOpenFatu = 0, SiOpenArti = 0, SiOpenClie = 0, SiOpenConfi = 0, SiOpenBuscArti = 0;
        // byte SiOpenUser = 0;
        int V1, PX, PV;
        public FormMenuPrincipal()
        {
            InitializeComponent();
            ToolTip Info = new ToolTip();
            Info.SetToolTip(this.BtnSql, "Configurar Conexion A Datos");
            Info.SetToolTip(this.btnSalir, "Cerrar Aplicacionn");
            Info.SetToolTip(this.btnCerrar, "Cerrar Aplicacionn");
            Info.IsBalloon = true;
            Info.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            ToolTip Info2 = new ToolTip();
            Info2.SetToolTip(this.BtnCalculadora, "Calculadora");
            Info2.SetToolTip(this.BtnAbrirChrome, "Navegador Chrome");
            Info2.SetToolTip(this.BtnArchivos, "Abrir Archivos");
            Info2.SetToolTip(this.BtnCarpeteDatos, "Abrir Explorador " + "\n" + Directory.GetCurrentDirectory() + "\\" + "Datos" + "\\");
            Info2.SetToolTip(this.BtnCarpetasPdf, "Abrir Explorador Archivos P.D.F");
            FormMenuPrincipal.menu2principal = this;
            SetStyle(ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;
            this.PanelInfo_P.Width = this.PanelInfo_P.Width = 0;
            ClassCorreosDB Col = new ClassCorreosDB();
            Col.AdColunnas();
        }
        private int tolerance = 15;
        private const int WM_NCHITTEST = 132;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;
        public bool EspacioDiscosPrincipal(string nombreDisco, int Espacio)
        {

            bool ok = true;
            DriveInfo discoBuscar = new DriveInfo(nombreDisco);
            if (discoBuscar.AvailableFreeSpace / 1024 / 1024 < Espacio)
            {
                MessageBox.Show("Libere Espacio en disco", "Espacio Insuficiente en :" + discoBuscar.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ok = false;
            }
            return ok;
        }
        public void CrearArchivosXml(string ArchivoXml)
        {
            XmlDocument DOC = new XmlDocument();
            XmlElement RAIZ = DOC.CreateElement("a");
            DOC.AppendChild(RAIZ);
            DOC.Save(ArchivoXml);
            MessageBox.Show("Archivo Nuevo Creado" + "\n" + ArchivoXml.ToString(), "SISTEMA GENERAL");
        }
        private void CrearArchivos_Xml_Principal()
        {
            if (EspacioDiscosPrincipal(Directory.GetCurrentDirectory(), 30))
            {
                try
                {
                    if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\" + ClasDatos.RutaDatosPrincipal))
                    {
                        DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory());
                        dir.CreateSubdirectory(ClasDatos.RutaDatosPrincipal);
                        MessageBox.Show("Sistema Restructurado Con Exito", "SISTEMA GENERAL", MessageBoxButtons.OK);
                    }

                    for (int i = 1; i <= 5; i++)
                    {
                        if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\" + ClasDatos.RutaDatosPrincipal + "\\" + ClasDatos.RutaDatosPrincipal + " FN" + i))
                        {
                            DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\" + ClasDatos.RutaDatosPrincipal);
                            dir.CreateSubdirectory(ClasDatos.RutaDatosPrincipal + " FN" + i);
                            MessageBox.Show("Sistema Restructurado Con Exito", "SISTEMA FACTURACION", MessageBoxButtons.OK);
                        }
                    }


                    if (File.Exists(Directory.GetCurrentDirectory() + "\\" + ClasDatos.RutaDatosPrincipal + "\\" + "Servidores.Xml"))
                    {
                        this.dsServidor.ReadXml(Directory.GetCurrentDirectory() + "\\" + ClasDatos.RutaDatosPrincipal + "\\" + "Servidores.Xml");


                    }
                    else
                    {
                        CrearArchivosXml(Directory.GetCurrentDirectory() + "\\" + ClasDatos.RutaDatosPrincipal + "\\" + "Servidores.Xml");

                    }

                    if (File.Exists(ClasDatos.RutaMultidatos))
                    {
                        this.dsMultidatos.ReadXml(ClasDatos.RutaMultidatos);


                    }
                    else
                    {
                        CrearArchivosXml(ClasDatos.RutaMultidatos);

                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString(), "SISTEMA ERROR", MessageBoxButtons.OK);

                }
            }
        }
        protected override void WndProc(ref Message m)
        {
            // return;////cambie para provar como fvafdasd dassssssssssssssssssssssss
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    var hitPoint = PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
                    if (this.sizeGripRectangle.Contains(hitPoint))
                        m.Result = new IntPtr(HTBOTTOMRIGHT);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        //----------------DIBUJAR RECTANGULO / EXCLUIR ESQUINA PANEL 
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            var region = new Region(new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));

            this.sizeGripRectangle = new Rectangle(this.ClientRectangle.Width - this.tolerance, this.ClientRectangle.Height - this.tolerance, this.tolerance, this.tolerance);

            region.Exclude(this.sizeGripRectangle);
            this.panelContenedorPrincipal.Region = region;
            Invalidate();
        }
        //----------------COLOR Y GRIP DE RECTANGULO INFERIOR
        protected override void OnPaint(PaintEventArgs e)
        {

            SolidBrush blueBrush = new SolidBrush(Color.FromArgb(55, 61, 69));
            e.Graphics.FillRectangle(blueBrush, this.sizeGripRectangle);

            base.OnPaint(e);
            ControlPaint.DrawSizeGrip(e.Graphics, Color.DarkGoldenrod, this.sizeGripRectangle);
        }


        private void PanelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            this.V1 = 1;
            this.PX = e.X;
            this.PV = e.Y;

        }

        int lx, ly;
        int sw, sh;
        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            if (this.btnMaximizar.Tag.ToString() == "MIN")
            {
                this.btnMaximizar.Tag = "MAX";
                this.Size = new Size(this.sw, this.sh);
                this.Location = new Point(this.lx, this.ly);
                this.btnMaximizar.Image = Properties.Resources.maximizar;
            }
            else
            {
                this.btnMaximizar.Tag = "MIN";
                this.lx = this.Location.X;
                this.ly = this.Location.Y;
                this.sw = this.Size.Width;
                this.sh = this.Size.Height;
                this.Size = Screen.PrimaryScreen.WorkingArea.Size;
                this.Location = Screen.PrimaryScreen.WorkingArea.Location;
                this.btnMaximizar.Image = Properties.Resources.Normal;
            }

        }

        private void btnNormal_Click(object sender, EventArgs e)
        {
            this.Size = new Size(this.sw, this.sh);
            this.Location = new Point(this.lx, this.ly);
            // this.btnNormal.Visible = false;
            // this.btnMaximizar.Visible = true;
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if (this.PanelForms.Controls.Count <= 3)
            {
                if (MessageBox.Show("Cerrar Toda La Aplicacion", "CERRAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Application.Exit();
                }

            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (this.PanelForms.Controls.Count <= 3)
            {
                if (MessageBox.Show("Cerrar Toda La Aplicacion", "CERRAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    Application.Exit();
                    // Close();
                    //  Hide();

                }

            }

        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (this.panelMenu.Width == 230)
            {
                this.InfoConectado.Visible = false;
                this.tmContraerMenu.Start();
            }
            else if (this.panelMenu.Width == 55)
            {
                this.tmExpandirMenu.Start();

            }


        }

        private void tmExpandirMenu_Tick(object sender, EventArgs e)
        {
            if (this.panelMenu.Width >= 230)
            {
                this.tmExpandirMenu.Stop();
                this.InfoConectado.Visible = true;
            }
            else
            {
                this.panelMenu.Width = this.panelMenu.Width + 5;
            }
        }

        private void tmContraerMenu_Tick(object sender, EventArgs e)
        {
            if (this.panelMenu.Width <= 55)
                this.tmContraerMenu.Stop();
            else
                this.panelMenu.Width = this.panelMenu.Width - 5;
        }

        private void FormMenuPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                this.PanelInfo_P.Visible = true;
                this.PanelInfo_P.Width = this.PanelInfo_P.Width = 0;
                CrearArchivos_Xml_Principal();
            }
            catch (Exception)
            {

                // throw;
            }

        }

        private void btnListaClientes_Click(object sender, EventArgs e)
        {
            if (ClsConexionSql.SibaseDatosSql)
            {
                if (ClsConexionSql.CadenaConexion == string.Empty)
                {
                    MessageBox.Show("Debe Configurar Una Conexion A Archivos", "CONEXION", MessageBoxButtons.OK);
                    return;
                }
            }
            else
            {
                if (ClsConexionDb.CadenaConexion == string.Empty)
                {
                    MessageBox.Show("Debe Configurar Una Conexion A Archivos", "CONEXION", MessageBoxButtons.OK);
                    return;
                }
            }
            FormClientes frm = new FormClientes();
            if (this.SiOpenClie == 0)
            {

                frm.TopLevel = false;
                //  frm.Dock = DockStyle.Fill;
                this.PanelForms.Controls.Add(frm);
                //frm.FormBorderStyle = FormBorderStyle.None;
                frm.FormClosed += (o, args) => this.SiOpenClie = 0;
                frm.Show();
                frm.BringToFront();
                this.SiOpenClie = 1;
            }
            else
            {
                if (FormClientes.menu2CLIENTES2.WindowState == FormWindowState.Minimized)
                {
                    FormClientes.menu2CLIENTES2.WindowState = FormWindowState.Normal;
                    FormClientes.menu2CLIENTES2.BringToFront();
                }
            }
        }

        private void btnMembresia_Click(object sender, EventArgs e)
        {
            FormEnviarCorreos frmCorreo = new FormEnviarCorreos();
            frmCorreo.ShowDialog();
        }

        private void btnARTICULOS_Click(object sender, EventArgs e)
        {
            if (ClsConexionSql.SibaseDatosSql)
            {
                if (ClsConexionSql.CadenaConexion == string.Empty)
                {
                    MessageBox.Show("Debe Configurar Una Conexion A Archivos", "CONEXION", MessageBoxButtons.OK);
                    return;
                }
            }
            else
            {
                if (ClsConexionDb.CadenaConexion == string.Empty)
                {
                    MessageBox.Show("Debe Configurar Una Conexion A Archivos", "CONEXION", MessageBoxButtons.OK);
                    return;
                }
            }
            if (this.SiOpenArti == 0)
            {

                FormArticulos frm = new FormArticulos();
                frm.TopLevel = false;
                // frm.Dock = DockStyle.Fill;
              //  frm.WindowState = FormWindowState.Maximized;
                //  frm.Anchor = System.Windows.Forms.AnchorStyles.None;
                this.PanelForms.Controls.Add(frm);
                frm.FormClosed += (o, args) => this.SiOpenArti = 0;
                frm.Show();
                frm.BringToFront();
                this.SiOpenArti = 1;
            }
            else
            {

                if (FormArticulos.menu2Articulos.WindowState == FormWindowState.Minimized)
                {
                    FormArticulos.menu2Articulos.WindowState = FormWindowState.Normal;
                    FormArticulos.menu2Articulos.BringToFront();
                }
            }
        }

        private void BtnVentas_MouseEnter(object sender, EventArgs e)
        {
            this.panelventas.Visible = true;
            this.panelventas.BringToFront();

        }

        private void btnMembresia_MouseEnter(object sender, EventArgs e)
        {
            this.panelventas.Visible = false;
            this.panelSUBventas.Visible = false;
        }

        private void btnUsuarios_MouseEnter(object sender, EventArgs e)
        {
            this.panelventas.Visible = false;
            this.panelSUBventas.Visible = false;
        }

        private void btnConfiguracion_MouseEnter(object sender, EventArgs e)
        {
            this.panelventas.Visible = false;
            this.panelSUBventas.Visible = false;

        }

        private void panelContenedorForm_MouseEnter(object sender, EventArgs e)
        {
            this.panelventas.Visible = false;
            this.panelSUBventas.Visible = false;
        }

        private void btnCrearFactura_Click(object sender, EventArgs e)
        {
            if (ClsConexionSql.SibaseDatosSql)
            {
                if (ClsConexionSql.CadenaConexion == string.Empty)
                {
                    MessageBox.Show("Debe Configurar Una Conexion A Archivos", "CONEXION", MessageBoxButtons.OK);
                    return;
                }
            }
            else
            {
                if (ClsConexionDb.CadenaConexion == string.Empty)
                {
                    MessageBox.Show("Debe Configurar Una Conexion A Archivos", "CONEXION", MessageBoxButtons.OK);
                    return;
                }
            }
            ClasDatos.NombreFactura = "Factura";
            ClasDatos.RutaFactura = ClasDatos.RutaDatosPrincipal + " FN5";
            FormFacturar frm = new FormFacturar();
            this.InfoConectado.Visible = false;
            if (this.SiOpenFatu == 0)
            {

                this.panelMenu.Width = this.panelMenu.Width = 55;
                this.panel1.Height = this.panel1.Height = 0;
                frm.TopLevel = false;
                //  frm.Dock = DockStyle.Fill;
                this.PanelForms.Controls.Add(frm);
                frm.FormClosed += (o, args) => this.SiOpenFatu = 0;
                frm.FormClosed += (o, args) => this.panel1.Height = this.panel1.Height = 25;
                frm.FormClosed += (o, args) => this.panelMenu.Width = this.panelMenu.Width = 230;
                frm.FormClosed += (o, args) => this.InfoConectado.Visible = true;
                frm.Show();
                frm.BringToFront();
                this.SiOpenFatu = 1;
            }
            else
            {
                if (FormFacturar.menu2FACTURAR.WindowState == FormWindowState.Minimized)
                {
                    FormFacturar.menu2FACTURAR.WindowState = FormWindowState.Maximized;
                    FormFacturar.menu2FACTURAR.BringToFront();
                }

            }
        }

        private void BtnCompras_MouseEnter(object sender, EventArgs e)
        {
            this.panelventas.Visible = false;
            this.panelSUBventas.Visible = false;
        }

        private void btnARTICULOS_MouseEnter(object sender, EventArgs e)
        {
            // panelventas.Visible = false;
            // panelSUBventas.Visible = false;
        }

        private void btnConfiguracion_Click(object sender, EventArgs e)
        {

            if (this.SiOpenConfi == 0)
            {
                FormComfiguracion frm = new FormComfiguracion();
                frm.TopLevel = false;
                // frm.Dock = DockStyle.Fill;
                this.PanelForms.Controls.Add(frm);
                //frm.FormBorderStyle = FormBorderStyle.None;
                frm.FormClosed += (o, args) => this.SiOpenFatu = 0;
                frm.Show();
                frm.BringToFront();
                this.SiOpenFatu = 1;
            }
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {


        }



        private void BtnCrearNotas_Click(object sender, EventArgs e)
        {
            if (ClsConexionSql.SibaseDatosSql)
            {
                if (ClsConexionSql.CadenaConexion == string.Empty)
                {
                    MessageBox.Show("Debe Configurar Una Conexion A Archivos", "CONEXION", MessageBoxButtons.OK);
                    return;
                }
            }
            else
            {
                if (ClsConexionDb.CadenaConexion == string.Empty)
                {
                    MessageBox.Show("Debe Configurar Una Conexion A Archivos", "CONEXION", MessageBoxButtons.OK);
                    return;
                }
            }
            if (this.SiOpenFatu == 0)
            {
                ClasDatos.RutaFactura = ClasDatos.RutaDatosPrincipal + " FN1";
                ClasDatos.NombreFactura = "Nota";
               // ClasDatos.QUEform = "FATU";
                FormFacturar frm = new FormFacturar();
                this.InfoConectado.Visible = false;
                this.panelMenu.Width = this.panelMenu.Width = 55;
                this.panel1.Height = this.panel1.Height = 0;
                frm.TopLevel = false;
                // frm.Dock = DockStyle.Fill;
                this.PanelForms.Controls.Add(frm);
                frm.FormClosed += (o, args) => this.SiOpenFatu = 0;
                frm.FormClosed += (o, args) => this.panel1.Height = this.panel1.Height = 25;
                frm.FormClosed += (o, args) => this.panelMenu.Width = this.panelMenu.Width = 230;
                frm.FormClosed += (o, args) => this.InfoConectado.Visible = true;
                frm.BringToFront();
                this.SiOpenFatu = 1;
                frm.Show();

            }
            else
            {
                if (FormFacturar.menu2FACTURAR.WindowState == FormWindowState.Minimized)
                {
                    FormFacturar.menu2FACTURAR.WindowState = FormWindowState.Maximized;
                    FormFacturar.menu2FACTURAR.BringToFront();
                }

            }
        }

        private void BtnCrearPresupustos_Click(object sender, EventArgs e)
        {
            if (ClsConexionSql.SibaseDatosSql)
            {
                if (ClsConexionSql.CadenaConexion == string.Empty)
                {
                    MessageBox.Show("Debe Configurar Una Conexion A Archivos", "CONEXION", MessageBoxButtons.OK);
                    return;
                }
            }
            else
            {
                if (ClsConexionDb.CadenaConexion == string.Empty)
                {
                    MessageBox.Show("Debe Configurar Una Conexion A Archivos", "CONEXION", MessageBoxButtons.OK);
                    return;
                }
            }
            if (this.SiOpenFatu == 0)
            {
                ClasDatos.NombreFactura = "Presupuesto";
                ClasDatos.RutaFactura = ClasDatos.RutaDatosPrincipal + " FN4";
                FormFacturar frm = new FormFacturar();
                this.InfoConectado.Visible = false;
                this.panelMenu.Width = this.panelMenu.Width = 55;
                this.panel1.Height = this.panel1.Height = 0;
                frm.TopLevel = false;
                // frm.Dock = DockStyle.Fill;
                this.PanelForms.Controls.Add(frm);
                frm.FormClosed += (o, args) => this.SiOpenFatu = 0;
                frm.FormClosed += (o, args) => this.panel1.Height = this.panel1.Height = 25;
                frm.FormClosed += (o, args) => this.panelMenu.Width = this.panelMenu.Width = 230;
                frm.FormClosed += (o, args) => this.InfoConectado.Visible = true;
                frm.Show();
                frm.BringToFront();
                this.SiOpenFatu = 1;

            }
            else
            {
                if (FormFacturar.menu2FACTURAR.WindowState == FormWindowState.Minimized)
                {
                    FormFacturar.menu2FACTURAR.WindowState = FormWindowState.Maximized;
                    FormFacturar.menu2FACTURAR.BringToFront();
                }

            }
        }

        private void BtnCrearNotas2_Click(object sender, EventArgs e)
        {
            if (ClsConexionSql.SibaseDatosSql)
            {
                if (ClsConexionSql.CadenaConexion == string.Empty)
                {
                    MessageBox.Show("Debe Configurar Una Conexion A Archivos", "CONEXION", MessageBoxButtons.OK);
                    return;
                }
            }
            else
            {
                if (ClsConexionDb.CadenaConexion == string.Empty)
                {
                    MessageBox.Show("Debe Configurar Una Conexion A Archivos", "CONEXION", MessageBoxButtons.OK);
                    return;
                }
            }
            if (this.SiOpenFatu == 0)
            {
                ClasDatos.NombreFactura = "Nota2";
                ClasDatos.RutaFactura = ClasDatos.RutaDatosPrincipal + " FN2";
                FormFacturar frm = new FormFacturar();
                this.InfoConectado.Visible = false;
                this.panelMenu.Width = this.panelMenu.Width = 55;
                this.panel1.Height = this.panel1.Height = 0;
                frm.TopLevel = false;
                // frm.Dock = DockStyle.Fill;
                this.PanelForms.Controls.Add(frm);
                frm.FormClosed += (o, args) => this.SiOpenFatu = 0;
                frm.FormClosed += (o, args) => this.panel1.Height = this.panel1.Height = 25;
                frm.FormClosed += (o, args) => this.panelMenu.Width = this.panelMenu.Width = 230;
                frm.FormClosed += (o, args) => this.InfoConectado.Visible = true;
                frm.Show();
                frm.BringToFront();
                this.SiOpenFatu = 1;

            }
            else
            {
                if (FormFacturar.menu2FACTURAR.WindowState == FormWindowState.Minimized)
                {
                    FormFacturar.menu2FACTURAR.WindowState = FormWindowState.Maximized;
                    FormFacturar.menu2FACTURAR.BringToFront();
                }

            }
        }

        private void BtnCrearAlbaranes_Click(object sender, EventArgs e)
        {
            if (ClsConexionSql.SibaseDatosSql)
            {
                if (ClsConexionSql.CadenaConexion == string.Empty)
                {
                    MessageBox.Show("Debe Configurar Una Conexion A Archivos", "CONEXION", MessageBoxButtons.OK);
                    return;
                }
            }
            else
            {
                if (ClsConexionDb.CadenaConexion == string.Empty)
                {
                    MessageBox.Show("Debe Configurar Una Conexion A Archivos", "CONEXION", MessageBoxButtons.OK);
                    return;
                }
            }
            if (this.SiOpenFatu == 0)
            {
                ClasDatos.NombreFactura = "Albaran";
                ClasDatos.RutaFactura = ClasDatos.RutaDatosPrincipal + " FN3";
                FormFacturar frm = new FormFacturar();
                this.InfoConectado.Visible = false;
                this.panelMenu.Width = this.panelMenu.Width = 55;
                this.panel1.Height = this.panel1.Height = 0;
                frm.TopLevel = false;
                frm.Dock = DockStyle.Fill;
                // this.panelContenedorForm.Controls.Add(frm);
                this.PanelForms.Parent = frm;
                frm.FormClosed += (o, args) => this.SiOpenFatu = 0;
                frm.FormClosed += (o, args) => this.panel1.Height = this.panel1.Height = 25;
                frm.FormClosed += (o, args) => this.panelMenu.Width = this.panelMenu.Width = 230;
                frm.FormClosed += (o, args) => this.InfoConectado.Visible = true;
                frm.Show();
                frm.BringToFront();
                this.SiOpenFatu = 1;

            }
            else
            {
                if (FormFacturar.menu2FACTURAR.WindowState == FormWindowState.Minimized)
                {
                    FormFacturar.menu2FACTURAR.WindowState = FormWindowState.Maximized;
                    FormFacturar.menu2FACTURAR.BringToFront();
                }

            }
        }

        private void BtnCalculadora_Click_1(object sender, EventArgs e)
        {

            this.panelAplicaciones.Visible = false;
            this.panelAplicaciones.Tag = "stop";
            System.Diagnostics.Process.Start("Calc");
        }


        private void BtnCarpeteDatos_Click(object sender, EventArgs e)
        {
            this.panelAplicaciones.Visible = false;
            System.Diagnostics.Process.Start("explorer.exe", Directory.GetCurrentDirectory() + "\\" + "Datos");
        }


        private void Logo2_MouseEnter(object sender, EventArgs e)
        {
            if (this.panelAplicaciones.Visible == false)
            {
                this.PanelForms.Tag = "SEGUIR";
                this.panelAplicaciones.Visible = true;
            }
            else
            {
                this.panelAplicaciones.Visible = false;
            }
        }



        private void PanelBarraTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            if (Cursor.Position.Y >= 39)
            {
                this.panelAplicaciones.Visible = false;
            }

            if (this.V1 == 1)
            {
                SetDesktopLocation(MousePosition.X - this.PX, MousePosition.Y - this.PV);
            }
        }

        private void PanelBarraTitulo_MouseUp(object sender, MouseEventArgs e)
        {
            this.V1 = 0;
        }

        private void BtnInfo_Click(object sender, EventArgs e)
        {
            if (ClsConexionSql.SibaseDatosSql)
            {
                if (ClsConexionSql.CadenaConexion == string.Empty)
                {
                    MessageBox.Show("Debe Configurar Una Conexion A Archivos", "CONEXION", MessageBoxButtons.OK);
                    return;
                }
            }
            else
            {
                if (ClsConexionDb.CadenaConexion == string.Empty)
                {
                    MessageBox.Show("Debe Configurar Una Conexion A Archivos", "CONEXION", MessageBoxButtons.OK);
                    return;
                }
            }
            // this.PanelAcesosDire.Width = this.PanelForms.Width;
            // this.PanelAcesosDire.Height = this.PanelForms.Height;
            if (this.BtnArchivos.Tag.ToString() == "SI")
            {
                this.PanelAcesosDire.BringToFront();
                this.BtnArchivos.Tag = "NO";
                this.PanelAcesosDire.Visible = true;


            }
            else
            {
                this.BtnArchivos.Tag = "SI";
                this.PanelAcesosDire.Visible = false;
            }

        }

        private void TimerCerrarPanel_Tick(object sender, EventArgs e)
        {
            try
            {

                if (this.PanelInfo_P.Tag.ToString() == "ABRIR")
                {

                    if (this.PanelInfo_P.Width >= 864)
                    {
                        this.PanelInfo_P.Tag = "CERRAR";
                        this.TimerCerrarPanel.Stop();
                    }
                    else
                    {
                        this.PanelInfo_P.Width = this.PanelInfo_P.Width + 5;
                    }

                }
                else
                {
                    if (this.PanelInfo_P.Width <= 0)
                    {
                        this.TimerCerrarPanel.Stop();
                        this.PanelInfo_P.Tag = "ABRIR";
                    }
                    else
                    {
                        this.PanelInfo_P.Width = this.PanelInfo_P.Width - 5;
                    }
                }
            }
            catch (Exception)
            {

                // throw;
            }




        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            FormBienvenida formulario = new FormBienvenida();
            formulario.Show();
        }

        private void BtnAbrirChrome_Click(object sender, EventArgs e)
        {
            try
            {
                this.panelAplicaciones.Visible = false;
                Process.Start("https://www.google.es/");
            }
            catch (Exception)
            {


            }
        }

        private void panelContenedorForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormMenuPrincipal_MouseEnter(object sender, EventArgs e)
        {
            FormBienvenida.menu2.Close();
        }

        private void BtnArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                this.PanelInfo_P.BringToFront();
                if (this.PanelInfo_P.Tag.ToString() == "ABRIR")
                {
                    this.ContadorFactu.Text = FormMenuPrincipal.menu2principal.dsCONFIGURACCION.DtConfiguracionPrincipal.Count.ToString();
                    this.ContadorArticulos.Text = FormMenuPrincipal.menu2principal.articulos.DtArticulos.Count.ToString();
                    this.ContadorClientes.Text = FormMenuPrincipal.menu2principal.dsClientes.DtClientes.Count.ToString();

                }
                this.TimerCerrarPanel.Start();
            }
            catch (Exception)
            {

                // throw;
            }

        }

        private void PanelInfo_P_MouseLeave(object sender, EventArgs e)
        {
            try
            {

                if (this.PanelInfo_P.Tag.ToString() == "ABRIR")
                {
                    this.ContadorFactu.Text = FormMenuPrincipal.menu2principal.dsCONFIGURACCION.DtConfiguracionPrincipal.Count.ToString();
                    this.ContadorArticulos.Text = FormMenuPrincipal.menu2principal.articulos.DtArticulos.Count.ToString();
                    this.ContadorClientes.Text = FormMenuPrincipal.menu2principal.dsClientes.DtClientes.Count.ToString();
                    this.PanelInfo_P.Visible = true;
                    this.TimerCerrarPanel.Start();
                    this.PanelInfo_P.Tag = "CERRAR";
                }
                else
                {
                    this.PanelInfo_P.Tag = "ABRIR";
                }
            }
            catch (Exception)
            {

                // throw;
            }


        }

        private void PanelAcesosDire_MouseEnter(object sender, EventArgs e)
        {
            this.panelventas.Visible = false;
            this.panelSUBventas.Visible = false;
        }

        private void BotonConsultaEmpresa_Click(object sender, EventArgs e)
        {
            FormListarDatos frm = new FormListarDatos();
            frm.ListadoDatos2.DataSource = dsCONFIGURACCION.DtConfiguracionPrincipal;
            frm.ListadoDatos2.DisplayMember = "EmpresaConfi";
            frm.ShowDialog();
            frm.BringToFront();
        }

        private void BotonEmpresas_Click(object sender, EventArgs e)
        {
            FormEmpresas frm = new FormEmpresas();
            frm.TopLevel = false;
            // frm.Dock = DockStyle.Fill;
            // frm.WindowState = FormWindowState.Maximized;
            //frm.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PanelForms.Controls.Add(frm);
            frm.FormClosed += (o, args) => this.SiOpenArti = 0;
            frm.Show();
            frm.BringToFront();
        }

        private void BotonConsultaAlmacenes_Click(object sender, EventArgs e)
        {
            FormListarDatos frm = new FormListarDatos();
            frm.ListadoDatos2.DataSource = dsCONFIGURACCION.DtAlmacenes;
            frm.ListadoDatos2.DisplayMember = "Almacenes";
            frm.ShowDialog();
            frm.BringToFront();
        }

        private void BotonDatosInicio_Click(object sender, EventArgs e)
        {
            FormDatosInicio frm = new FormDatosInicio();
            frm.TopLevel = false;
            this.PanelForms.Controls.Add(frm);
            frm.Show();
            frm.BringToFront();
        }

        private void BotonConsultaFamilia_Click(object sender, EventArgs e)
        {
            FormListarDatos frm = new FormListarDatos();
            frm.ListadoDatos2.DataSource = dsMulti2.DtFamiliaProductos;
            frm.ListadoDatos2.DisplayMember = "Familia";
            frm.ShowDialog();
            frm.BringToFront();
        }

        private void BotonCorreosEletronicos_Click(object sender, EventArgs e)
        {
            FormCrearCorreos frm = new FormCrearCorreos();
            frm.TopLevel = false;
            this.PanelForms.Controls.Add(frm);
            frm.Show();
            frm.BringToFront();
        }

        private void BotonDescuentos_Click(object sender, EventArgs e)
        {
            FormDescuentos frm = new FormDescuentos();
            frm.TopLevel = false;
            this.PanelForms.Controls.Add(frm);
            frm.Show();
            frm.BringToFront();
        }

        private void BotonObras_Click(object sender, EventArgs e)
        {
            FormObras frm = new FormObras();
            frm.TopLevel = false;
            this.PanelForms.Controls.Add(frm);
            frm.Show();
            frm.BringToFront();
        }

        private void BotonAbrirProvincias_Click(object sender, EventArgs e)
        {
            FormProvincias frm = new FormProvincias();
            frm.TopLevel = false;
            frm.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PanelForms.Controls.Add(frm);
            frm.Show();
            // frm.WindowState = FormWindowState.Maximized;
            frm.BringToFront();
        }

        private void BotonPaises_Click(object sender, EventArgs e)
        {
            FormPaises frm = new FormPaises();
            frm.TopLevel = false;
            frm.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PanelForms.Controls.Add(frm);
            frm.Show();
            frm.BringToFront();
        }

        private void BotonFamilias_Click(object sender, EventArgs e)
        {
            FormFamiliaProductos frm = new FormFamiliaProductos();
            frm.TopLevel = false;
            this.PanelForms.Controls.Add(frm);
            frm.Show();
            frm.BringToFront();
        }

        private void BotonEjecicios_Click(object sender, EventArgs e)
        {
            FormEjercicios frm = new FormEjercicios();
            frm.TopLevel = false;
            this.PanelForms.Controls.Add(frm);
            frm.Show();
            frm.BringToFront();
        }

        private void BotonConsultasEjercicios_Click(object sender, EventArgs e)
        {
            FormListarDatos frm = new FormListarDatos();
            frm.ListadoDatos2.DataSource = dsCONFIGURACCION.DtConfi;
            frm.ListadoDatos2.DisplayMember = "EjerciciosDeAño";
            frm.ShowDialog();
            frm.BringToFront();
        }

        private void BotonProveedores_Click(object sender, EventArgs e)
        {
            FormProveedores frm = new FormProveedores();
            frm.TopLevel = false;
            this.PanelForms.Controls.Add(frm);
            frm.Show();
            frm.BringToFront();
        }

        private void BotonConsultadescuentos_Click(object sender, EventArgs e)
        {
            FormListarDatos frm = new FormListarDatos();
            frm.ListadoDatos2.DataSource = dsCONFIGURACCION.DtTarifaTipo;
            frm.ListadoDatos2.DisplayMember = "TarifaTipo";
            frm.ShowDialog();
            frm.BringToFront();
        }

        private void BotonConsultaProveedores_Click(object sender, EventArgs e)
        {
            FormListarDatos frm = new FormListarDatos();
            frm.ListadoDatos2.DataSource = dsCONFIGURACCION.DtProveedores;
            frm.ListadoDatos2.DisplayMember = "Proveedores";
            frm.ShowDialog();
            frm.BringToFront();
        }

        private void BotonConsultaObras_Click(object sender, EventArgs e)
        {
            FormListarDatos frm = new FormListarDatos();
            frm.ListadoDatos2.DataSource = dsMulti2.DtObras;
            frm.ListadoDatos2.DisplayMember = "Obras";
            frm.ShowDialog();
            frm.BringToFront();

        }

        private void BotonconsultaPaises_Click(object sender, EventArgs e)
        {
            FormListarDatos frm = new FormListarDatos();
            frm.ListadoDatos2.DataSource = dsMulti2.DtPaises;
            frm.ListadoDatos2.DisplayMember = "Paises";
            frm.ShowDialog();
            frm.BringToFront();
        }

        private void BotonconsultaProvincias_Click(object sender, EventArgs e)
        {
            FormListarDatos frm = new FormListarDatos();
            frm.ListadoDatos2.DataSource = dsMulti2.DtProvincias;
            frm.ListadoDatos2.DisplayMember = "Provincias";
            frm.ShowDialog();
            frm.BringToFront();
        }

        private void BotonAlmacenes_Click(object sender, EventArgs e)
        {
            FormAlmacenes frm = new FormAlmacenes();
            frm.TopLevel = false;
            //frm.WindowState = FormWindowState.Maximized;
            this.PanelForms.Controls.Add(frm);
            frm.Show();
            frm.BringToFront();
        }

        private void BotonCodigoBarras_Click(object sender, EventArgs e)
        {
            FormBuscarArticulos frm = new FormBuscarArticulos();
            if (this.SiOpenBuscArti == 0 && this.SiOpenFatu == 0 && this.SiOpenArti == 0)
            {
                ClasDatos.OkFacturar = false;
                ClasDatos.QUEform = "QR";
                this.InfoConectado.Visible = false;
                this.panelMenu.Width = this.panelMenu.Width = 55;
                this.panel1.Height = this.panel1.Height = 0;
                frm.TopLevel = false;
                this.PanelForms.Controls.Add(frm);
                frm.FormClosed += (o, args) => this.SiOpenBuscArti = 0;

                frm.FormClosed += (o, args) => this.panel1.Height = this.panel1.Height = 25;
                frm.FormClosed += (o, args) => this.panelMenu.Width = this.panelMenu.Width = 230;
                frm.FormClosed += (o, args) => this.InfoConectado.Visible = true;
                frm.Show();
                frm.BringToFront();
                this.SiOpenBuscArti = 1;
            }
        }

        private void BotonPrueba_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.TopLevel = false;
            //frm.WindowState = FormWindowState.Maximized;
            this.PanelForms.Controls.Add(frm);
            frm.Show();
            frm.BringToFront();

        }


        private void FormMenuPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.PanelForms.Controls.Count > 3)
            {
                e.Cancel = true;
            }
        }
        private void BtnSql_Click(object sender, EventArgs e)
        {

            if (this.SiOpenFatu == 0 & this.SiOpenArti == 0 & this.SiOpenClie == 0 & this.SiOpenConfi == 0)
            {
                this.panelAplicaciones.Visible = false;
                FormBaseDatos frm = new FormBaseDatos();
                frm.ShowDialog();
                frm.BringToFront();

            }

        }

        private void BtnCarpetasPdf_Click(object sender, EventArgs e)
        {
            this.panelAplicaciones.Visible = false;
            string VariableDt = "Archivos Pdf Del Año ";
            if (Directory.Exists(Directory.GetCurrentDirectory() + "\\" + "Datos" + "\\" + VariableDt + DateTime.Now.Year))
            {
                System.Diagnostics.Process.Start("explorer.exe", Directory.GetCurrentDirectory() + "\\" + "Datos" + "\\" + VariableDt + DateTime.Now.Year);
            }
            else
            {
                System.Diagnostics.Process.Start("explorer.exe", Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            }

        }

        private void BtnFacturar_MouseEnter(object sender, EventArgs e)
        {
            if (this.panelSUBventas.Visible == false)
            {
                this.panelSUBventas.Visible = true;
                this.panelSUBventas.BringToFront();
            }
            else
            {
                this.panelSUBventas.Visible = false;

            }
        }

        private void tmFechaHora_Tick(object sender, EventArgs e)
        {
            if (this.PanelForms.Tag.ToString() == "SEGUIR")
            {
                this.lbFecha.Text = DateTime.Now.ToLongDateString();
                this.lblHora.Text = DateTime.Now.ToString("HH:mm:ssss");
            }

            //brea;
        }

    }
}
