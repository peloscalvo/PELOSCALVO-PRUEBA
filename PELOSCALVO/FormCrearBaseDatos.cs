﻿using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PELOSCALVO
{
    public partial class FormCrearBaseDatos : Form
    {
        public FormCrearBaseDatos()
        {
            InitializeComponent();
        }

        private void ServidorCrear_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (this.ServidorCrear.Tag.ToString() == "PARAR")
            {
                return;
            }
            string Cadena2 = "master";
            this.InfoProcesoText.BackColor = Color.FromArgb(234, 210, 8);
            this.InfoProcesoText.Text = " Consultando Datos.............. .......Espere Un Momento !!!!!!!";
            this.InfoProcesoText.Refresh();
            this.ListaBaseDatos.Items.Clear();
            this.NombreBaseDatos.Text = string.Empty;
            this.ServidorCrear.Refresh();
            Application.DoEvents();
            if (this.ServidorCrear.Text != string.Empty)
            {
                string cadenaConexion = "Data Source=" + this.ServidorCrear.Text + ";Initial Catalog=" + Cadena2 + ";Integrated Security=True";
                try
                {
                    using (SqlConnection NuevaConexion = new SqlConnection(cadenaConexion))
                    {
                        using (SqlCommand comando = new SqlCommand("Select Name From sysdatabases;", NuevaConexion))
                        {
                            NuevaConexion.Open();
                            using (SqlDataReader rd = comando.ExecuteReader())
                            {

                                while (rd.Read() == true)
                                {
                                    // Servidor.Items.Add(rd[1]);

                                    this.ListaBaseDatos.Items.Add(rd[0]);
                                }
                                BuscarServidor();
                            }

                        }

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString());
                }
            }

            if (this.TabCrear.SelectedIndex == 0)
            {
                this.InfoProcesoText.Text = "Indica La Base Datos a Crear";
            }
            if (this.TabCrear.SelectedIndex == 1)
            {
                this.InfoProcesoText.Text = "Indica La Tabla a Crear";
            }
            if (this.TabCrear.SelectedIndex == 2)
            {
                this.InfoProcesoText.Text = "Indica La Base Datos a Crear Copia De Seguridad";
            }
            this.InfoProcesoText.BackColor = this.BackColor;
            this.InfoProcesoText.Refresh();
        }

        private void BtnBuscarServidorCrear_Click(object sender, EventArgs e)
        {
            string Cadena2 = "master";
            this.InfoProcesoText.BackColor = Color.FromArgb(234, 210, 8);
            this.InfoProcesoText.Text = " Consultando Datos.............. .......Espere Un Momento !!!!!!!";
            this.ListaBaseDatos.Items.Clear();
            this.NombreBaseDatos.Text = string.Empty;
            this.InfoProcesoText.Refresh();
            if (this.ServidorCrear.Text != string.Empty)
            {
                string cadenaConexion = "Data Source=" + this.ServidorCrear.Text + ";Initial Catalog=" + Cadena2 + ";Integrated Security=True";
                try
                {
                    using (SqlConnection NuevaConexion = new SqlConnection(cadenaConexion))
                    {
                        using (SqlCommand comando = new SqlCommand("Select Name From sysdatabases;", NuevaConexion))
                        {
                            NuevaConexion.Open();
                            using (SqlDataReader rd = comando.ExecuteReader())
                            {

                                while (rd.Read() == true)
                                {
                                    // Servidor.Items.Add(rd[1]);

                                    this.ListaBaseDatos.Items.Add(rd[0]);
                                }
                                BuscarServidor();
                            }

                        }

                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString());
                }
            }

            if (this.TabCrear.SelectedIndex == 0)
            {
                this.InfoProcesoText.Text = "Indica La Base Datos a Crear";
            }
            if (this.TabCrear.SelectedIndex == 1)
            {
                this.InfoProcesoText.Text = "Indica La Tabla a Crear";
            }
            if (this.TabCrear.SelectedIndex == 2)
            {
                this.InfoProcesoText.Text = "Indica La Base Datos a Crear Copia De Seguridad";
            }
            this.InfoProcesoText.BackColor = this.BackColor;
            this.InfoProcesoText.Refresh();
        }

        private void FormCrearBaseDatos_Load(object sender, EventArgs e)
        {

            if (!File.Exists(Directory.GetCurrentDirectory() + "\\" + ClasDatos.RutaDatosPrincipal + "\\" + "Servidores.Xml"))
            {

                FormMenuPrincipal.menu2principal.CrearArchivosXml(Directory.GetCurrentDirectory() + "\\" + ClasDatos.RutaDatosPrincipal + "\\" + "Servidores.Xml");

            }
            if (FormMenuPrincipal.menu2principal.dsServidor != null)
            {
                this.dtServidorBindingSource.DataSource = FormMenuPrincipal.menu2principal.dsServidor;
            }
            try
            {


                if (FormMenuPrincipal.menu2principal.dsServidor.DtServidor.Count < 4)
                {
                    FormMenuPrincipal.menu2principal.dsServidor.DtServidor.Rows.Add("(Local)");
                    FormMenuPrincipal.menu2principal.dsServidor.DtServidor.Rows.Add(".");
                    FormMenuPrincipal.menu2principal.dsServidor.DtServidor.Rows.Add("(localdb)\\MSSQLLocalDB");
                    FormMenuPrincipal.menu2principal.dsServidor.DtServidor.Rows.Add("(localdb)\\SQLVICTOR");
                    FormMenuPrincipal.menu2principal.dsServidor.DtServidor.Rows.Add(string.Format(@"{0}\SQLEXPRESS", Environment.MachineName));
                    FormMenuPrincipal.menu2principal.dsServidor.WriteXml(Directory.GetCurrentDirectory() + "\\" + ClasDatos.RutaDatosPrincipal + "\\" + "Servidores.Xml");
                    if (File.Exists(Directory.GetCurrentDirectory() + "\\" + ClasDatos.RutaDatosPrincipal + "\\" + "Servidores.Xml"))
                    {
                        FormMenuPrincipal.menu2principal.dsServidor.WriteXml(Directory.GetCurrentDirectory() + "\\" + ClasDatos.RutaDatosPrincipal + "\\" + "Servidores.Xml");

                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "ERROR");
            }
            if(SerieArticulosTabla.Items.Count > 0)
            {
                this.SerieArticulosTabla.SelectedIndex=0;
            }
            if (SerieClientesTabla.Items.Count > 0)
            {
                this.SerieClientesTabla.SelectedIndex = 0;
            }
            this.AutenticacionCrear.Text = "Autenticacion De Windows";
            string[] instancias;
            instancias = ClsOjetos.instanciasInstaladas();
            try
            {
                if (instancias != null)
                {

                    foreach (string s in instancias)
                    {
                        MessageBox.Show(s.ToString());
                        // this.dtServidorBindingSource.AddNew();
                        //  this.dsServidor.Tables["DtServidor"].Rows[this.dtServidorBindingSource.Count]["Servidores"] = @"(LocalDb)\" + s;
                        // ServidorCrear.Items.Add(@"(LocalDb)\" + s);
                    }
                }
            }
            catch (Exception)
            {

                //throw;
            }
            LeerInstancias();
            this.ServidorCrear.Tag = "SEGUIR";
        }

        private void LeerInstancias()
        {
            this.ListaNueva.Items.Clear();
            string Salida = string.Format("/C " + @"SqlLocalDB.exe Info """ + this.InstanciaNueva.Text + "");
            var pc = Process.Start("cmd.exe", Salida);
            //StreamReader leer = new StreamReader(pc.StartInfo.ToString());
            StringReader Lector = new StringReader(pc.StartInfo.ToString());// pc.StartInfo.ToString();

            while (Lector.ReadLine() != null)
            {
                this.ListaNueva.Items.Add("(LOCALDB)\\" + Lector.ToString());

                //this.ListaBaseDatos.Items.Add(rd[0]);
            }
            DirectoryInfo dire1 = new DirectoryInfo(@"C:\Users\Victor Pais\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances");
            DirectoryInfo dire2 = new DirectoryInfo(@"C:\Program Files\Microsoft SQL Server");

            //FileInfo[] files = directory.GetFiles(@"C:\Users\Victor Pais\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances");

            try
            {
                bool SiEsnumero = false;
                int NumeroValue;
                DirectoryInfo[] directorios = dire1.GetDirectories();
                foreach (DirectoryInfo datos in directorios)
                {
                    this.ListaNueva.Items.Add(@"(localDb)\" + datos.Name);
                }
                directorios = dire2.GetDirectories();
                foreach (var datos2 in directorios)
                {

                    SiEsnumero = int.TryParse(datos2.Name, out NumeroValue);
                    if (SiEsnumero == false)
                    {
                        this.ListaNueva.Items.Add(Environment.MachineName + @"\" + datos2.Name);
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void BtnCrearBaseDatos_Click(object sender, EventArgs e)
        {
            if (this.NombreBaseDatos.Text == string.Empty)
            {
                this.NombreBaseDatos.SelectAll();
                MessageBox.Show("Nombre De base Datos a Crear Vacio", "CAMPOS VACIOS");
                return;
            }
            if (this.ServidorCrear.Text == string.Empty)
            {
                this.ServidorCrear.SelectAll();
                MessageBox.Show("Elija Un Servidor Correcto", "CAMPOS VACIOS");
                return;
            }

            foreach (string ItemBuscar in this.ListaBaseDatos.Items)
            {
                if (ItemBuscar.ToString() == this.NombreBaseDatos.Text)
                {
                    MessageBox.Show("Esta Base Datos Ya Existe" + "\n" + this.NombreBaseDatos.Text, "CREAR BASE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;

                }
            }
            this.InfoProcesoText.BackColor = Color.FromArgb(234, 210, 8);
            this.InfoProcesoText.Text = " Creando Base Datos: " + this.NombreBaseDatos.Text + " .............. .......Espere Un Momento !!!!!!!";
            this.InfoProcesoText.Refresh();
            Application.DoEvents();

            string Cadena2 = "master";
            string Consuta = "CREATE DATABASE [" + this.NombreBaseDatos.Text + "];";
            if (this.ServidorCrear.Text != string.Empty)
            {
                string cadenaConexion = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password{3};", this.ServidorCrear.Text, this.NombreBaseDatos.Text, this.UsuarioSql2.Text, this.ContraseñaSql2.Text);
                if (this.AutenticacionCrear.SelectedIndex == 0)
                {
                    cadenaConexion = "Data Source=" + this.ServidorCrear.Text + ";Initial Catalog=" + Cadena2 + ";Integrated Security=True";

                }


                try
                {
                    using (SqlConnection NuevaConexion = new SqlConnection(cadenaConexion))
                    {
                        using (SqlCommand comando = new SqlCommand(Consuta, NuevaConexion))
                        {
                            NuevaConexion.Open();
                            comando.ExecuteNonQuery();
                            this.ListaBaseDatos.Items.Add(this.NombreBaseDatos.Text);
                            MessageBox.Show("Se Creo Base Datos Correctamente" + "\n" + this.NombreBaseDatos.Text, "CREAR BASE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            CrearTablasSql();
                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString());
                }

                this.InfoProcesoText.Text = "Indica La Tabla a Crear";
                this.InfoProcesoText.BackColor = this.BackColor;
                this.InfoProcesoText.Refresh();
                this.ServidorCrear.Tag = "SEGUIR";
            }
        }
        private void BuscarServidor()
        {
            try
            {
                int TotalServidores = this.ServidorCrear.Items.Count;
                this.ServidorCrear.Tag = "PARAR";
                bool SiExisteServidor = true;
                for (int i = 0; i < TotalServidores; i++)
                {

                    if (this.dsServidor.Tables["DtServidor"].Rows[i]["Servidores"].ToString().Equals(this.ServidorCrear.Text, StringComparison.OrdinalIgnoreCase))
                    {
                        SiExisteServidor = false;
                    }

                }
                if (SiExisteServidor)
                {
                    if (!string.IsNullOrEmpty(this.ServidorCrear.Text))
                    {
                        FormMenuPrincipal.menu2principal.dsServidor.DtServidor.Rows.Add(this.ServidorCrear.Text);
                        Validate();
                        if (File.Exists(Directory.GetCurrentDirectory() + "\\" + ClasDatos.RutaDatosPrincipal + "\\" + "Servidores.Xml"))
                        {
                            this.dsServidor.WriteXml(Directory.GetCurrentDirectory() + "\\" + ClasDatos.RutaDatosPrincipal + "\\" + "Servidores.Xml");

                        }
                        else
                        {
                            MessageBox.Show("Archivo No Existe" + "\n" + Directory.GetCurrentDirectory() + "\\" + ClasDatos.RutaDatosPrincipal + "\\" + "Servidores.Xml", "ARCHIVO NO EXISTE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                this.ServidorCrear.Tag = "SEGUIR";
            }
        }
        private void CrearTablasSql()
        {
            string ConsultaFacturacion = "";
            string Resultado = "Se Crearon Nuevas Tablas" + "\n" + "\n";
            string NombreBaseDatos = this.NombreBaseTablas.Text;
            /// ARTICULOS ARTICULOS
            string TablaArticulos = this.SerieArticulosTabla.Text;
            string TablaClientes = this.SerieClientesTabla.Text;
            string ConsutaArticulos = "IF not  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[" + TablaArticulos + "]') AND type in (N'U'))" +
            "CREATE TABLE [" + TablaArticulos + "] ( [Id][int] primary key NOT NULL, [Referencia] [varchar](50)NULL, [Oem] [varchar](50)NULL,[Descripcci] [varchar](60)NULL,[Coste] [numeric](19, 3) NULL," +
            "[Ganancia] [numeric](19, 3) NULL,[Pvp1] [numeric](19, 3) NULL,[PvpIva] [numeric](19, 3) NULL,[Desc2] [numeric](19, 3) NULL,[Pvp2] [numeric](19, 3) NULL," +
            "[Desc3] [numeric](19, 3) NULL,[Pvp3] [numeric](19, 3) NULL,[Desc4] [numeric](19, 3) NULL,[Pvp4] [numeric](19, 3) NULL,[Desc5] [numeric](19, 3) NULL," +
            "[Pvp5] [numeric](19, 3) NULL,[Desc6] [numeric](19, 3) NULL,[Pvp6] [numeric](19, 3) NULL,[DescPlus] [numeric](19, 3) NULL,[Plus] [numeric](19, 3) NULL," +
            "[UnidadPale] [numeric](19, 3) NULL,[MinimosSto] [numeric](19, 3) NULL,[Stock] [numeric](19, 3) NULL,[Familia] [varchar](50) NULL,[Fecha] [date] NULL," +
            "[BAJA] [bit]DEFAULT 0 NOT NULL, [Fatu] [bit]DEFAULT 0 NOT NULL)";
            /////consulta  CLIENTES           CLIENTES   /////////////////
            string ConsultaClientes = "IF not  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[" + TablaClientes + "]') AND type in (N'U'))" +
                " CREATE TABLE [" + TablaClientes + "]( [Id][int]primary key NOT NULL,[APODOCLIEN] [varchar](255) NULL,[NOMBRECLIE] [varchar](255) NULL,[DIRECCIONC] [varchar](255) NULL," +
                "[TELEFONOCL] [varchar](30) NULL,[MOVILCLIEN] [varchar](50) NULL,[CORREOCLIE] [varchar](255) NULL,[DNICLIENTE] [varchar](30) NULL,[LOCALIDADC] [varchar](255) NULL," +
                "[CODIGOPOST] [varchar](50) NULL,[PAISCLIENT] [varchar](255) NULL,[FECHAALTAC] [date] NULL,[CALLECLIEN] [varchar](255) NULL,[NUMEROCALL] [varchar](40) NULL," +
                "[PROVINCIAC] [varchar](100) NULL,[TARIFATIPO] [varchar](50) NULL,[TIPODNI] [varchar](50) NULL,[TIPOCLIENT] [varchar](50) NULL,[DESCUENTOC] [numeric](19, 3) NULL," +
                "[NUMEROCUEN] [varchar](100) NULL,[PORTES] [varchar](100) NULL,[BANCOOFICI] [varchar](100) NULL,[BANCOPROVI] [varchar](100) NULL,[BANCODIREC] [varchar](150) NULL," +
                "[BANCOLOCAL] [varchar](100) NULL,[BANCOIBAN] [varchar](100) NULL,[BANCOCODIG] [varchar](100) NULL,[BANCOENTID] [varchar](100) NULL,[BANCOOFIC2] [varchar](100) NULL," +
                "[BANCODC] [varchar](100) NULL,[BANCON_CUE] [varchar](100) NULL,[BAJA] [bit]default(0) NOT NULL)";
            //////consulta  FACTURANCION         FACTURACION   /////////////////
            ///
            string TablaFactu = "DtNota";
            string Tabladetalle = "DtDetalles_Nota";


            string ConsultaEmpresas = "IF not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DtConfiguracionPrincipal]') AND type in (N'U'))" +
          "CREATE TABLE [DtConfiguracionPrincipal]( [IdEmpresa] [int] primary key Not NULL, [EmpresaConfi][varchar](60) NULL, [NombreEmpresa] [varchar](60) NULL," +
           "[DireccionEmpresa] [varchar](60) NULL,[LocalidadEmpresa] [varchar](50) NULL,[CodigoPostalEmpresa] [varchar](25) NULL,[ProvinciaEmpresa] [varchar](30) NULL," +
           "[TelefonoEmpresa] [varchar](20) NULL,[CorreoEmpresa] [varchar](30) NULL,[WepEmpresa] [varchar](40) NULL,[RegimenIvaEmpresa] [varchar](30) NULL," +
           " [PaisEmpresa] [varchar](40) NULL,[SerieDeFacturacionEmpresa] [varchar](10) NULL,[Telefono2Empresa] [varchar](25) NULL,[MovilEmpresa] [varchar](25) NULL," +
            " [CifEmpresa] [varchar](20) NULL,[ImagenEmpresa] [image] NULL) " +

            "IF NOT  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DtConfi]') AND type in (N'U')) " +
            "CREATE TABLE[DtConfi]([IdEnlace][int] primary key Not NULL, [ConfiguraccionBasica][varchar](60) NULL, [TipoInpuestoIVA] INT NULL, [EjerciciosDeAño] [varchar](60) NULL," +
           "[EmpresaENLACE] [int] NOT NULL,[IdConexionConfi] [int] NULL, [AñoDeEjercicio] [varchar](60) NULL," +
            " CONSTRAINT FK_DTCONFI FOREIGN KEY(EmpresaENLACE)REFERENCES DtConfiguracionPrincipal(IdEmpresa) ON UPDATE CASCADE ON DELETE CASCADE )";

            string ConsultaTablas = "IF not EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DtProveedores]') AND type in (N'U'))" +
                  "   CREATE TABLE [DtProveedores]( [Id][int] not null,[Proveedores][varchar](60) NULL ,[Enlace_Proveedores] [int] Not NULL, " +
                  " CONSTRAINT F_DtProveedores FOREIGN KEY(Enlace_Proveedores)REFERENCES DtConfiguracionPrincipal(IdEmpresa) ON UPDATE CASCADE ON DELETE CASCADE )" +

                  "IF not EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DtTarifaTipo]') AND type in (N'U'))" +
                  "   CREATE TABLE [DtTarifa]( [Id]int null,[TarifaTipo][varchar](60) NULL ,[TarifaReal][varchar](60) NULL,[EnlaceTarifa] [int] Not NULL, " +
                  " CONSTRAINT F_DtTarifaTipo FOREIGN KEY(EnlaceTarifa)REFERENCES DtConfiguracionPrincipal(IdEmpresa) ON UPDATE CASCADE ON DELETE CASCADE )" +

            "IF not EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DtAlmacenes]') AND type in (N'U'))" +
           " CREATE TABLE [DtAlmacenes]( [Id][int] NOT NULL,[Almacenes] varchar(80) NULL ,[Enlace_Almacenes] [int] not  NULL, " +
                 " CONSTRAINT F_DtALMACENES FOREIGN KEY(Enlace_Almacenes)REFERENCES DtConfiguracionPrincipal(IdEmpresa) ON UPDATE CASCADE ON DELETE CASCADE )" +

                 "IF not EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DtFamiliaProductos]') AND type in (N'U'))" +
          " CREATE TABLE [DtFamiliaProductos]( [Id][int] NOT NULL,[Familia] varchar(80) NULL)" +

            "IF not EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DtPaises]') AND type in (N'U'))" +
        " CREATE TABLE [DtPaises]( [Id][int] primary key NOT NULL,[Paises] varchar(80)  NULL)" +

            "IF not EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DtProvincias]') AND type in (N'U'))" +
             " CREATE TABLE [DtProvincias]( [Id][int] NOT NULL,[Provincias] varchar(80) NULL,[IdEnlace][int] NOT NULL ," +
          " CONSTRAINT F_DtProvincias FOREIGN KEY (IdEnlace)REFERENCES DtPaises(Id) ON UPDATE CASCADE ON DELETE CASCADE )" +

          "IF not EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DtObras]') AND type in (N'U'))" +
               " CREATE TABLE [DtObras]( [Id][int] primary key NOT NULL,[Obras] varchar(80) NULL)" +

                 "IF not EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DtInicioMulti]') AND type in (N'U'))" +
               " CREATE TABLE [DtInicioMulti]( [Id][int]primary key NOT NULL,[EmpresaInicio] varchar(200) NULL" +
               ",[EjercicioInicio] varchar(80) NULL,[SerieInicio] varchar(5) NULL,[NombreArchivoDatos] varchar(80) NULL" +
               ",[RutaArchivoDatos] varchar(200) NULL,[SerieProvinciaInicio] varchar(80) NULL,[SeriePaisInicio] varchar(80) NULL," +
               "[ArchivoArticulos] varchar(80) NULL,[TipoExtensionArticulos] varchar(3) NULL,[ArchivoClientes] varchar(80) NULL)" +

                  "IF not EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DtCorreos]') AND type in (N'U'))" +
               " CREATE TABLE [DtCorreos]( [Id][int] NOT NULL,[NombreEmpresa] varchar(150) NULL,[CorreoEletronico] varchar(150) NULL" +
               ",[Usuario] varchar(150) NULL,[Contraseña] varchar(150) NULL,[smtp] varchar(150) NULL,[Puerto] [int] NULL" +
               ",[Timeof] [int] NULL)" +

            "IF not EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DtCorreosCliente]') AND type in (N'U'))" +
              " CREATE TABLE [DtCorreosCliente]( [Id][int]primary key NOT NULL,[RazonSocial] varchar(200) NULL,[EmpresaNombre] varchar(200) NULL" +
             ",[Direcion] varchar(150) NULL,[CorreoEletronico_cli] varchar(200) NULL)" +

            "IF not EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DtUsuario]') AND type in (N'U'))" +
       " CREATE TABLE [DtUsuario]( [Id][int]primary key NOT NULL,[Usuario] varchar(200) NULL,[Nombre] varchar(200) NULL" +
      ",[Direcion] varchar(150) NULL,[Cargo] varchar(200) NULL,[Varios] varchar(150) NULL,[CorreoEletronico] varchar(200) NULL)" +
       "IF not EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DtMovimientos]') AND type in (N'U'))" +
         "CREATE TABLE [DtMovimientos]([Id][int] IDENTITY(1,1) primary key,[Referencia] varchar(250) null, [Stock] [int] default 0, [Enlace] varchar(250) not null," +
               "[Id_Empresa] [int] null, [Id_Almacen] [int] null, [Varios] varchar(250) null,[contar] varchar(250) null)";
            string cadenaConexion = "Data Source=" + this.ServidorCrear.Text + ";Initial Catalog=" + NombreBaseDatos + ";Integrated Security=True";
            try
            {
                using (SqlConnection NuevaConexion = new SqlConnection(cadenaConexion))
                {
                    NuevaConexion.Open();
                    if (this.checkEmpresas.Checked == true)
                    {
                        using (SqlCommand comando3 = new SqlCommand(ConsultaEmpresas, NuevaConexion))
                        {
                            comando3.ExecuteNonQuery();
                            comando3.Parameters.Clear();
                            Resultado += "EMPRESAS" + "\n";
                        }

                    }
                    if (this.checkTablas.Checked == true)
                    {
                        using (SqlCommand comando3 = new SqlCommand(ConsultaTablas, NuevaConexion))
                        {
                            comando3.ExecuteNonQuery();
                            comando3.Parameters.Clear();
                            Resultado += "DTCONFI" + "\n"; Resultado += "DTPROVEEDORES" + "\n"; Resultado += "DTALMACENES" + "\n";
                        }

                    }
                    if (this.CheckTablaArticulos.Checked == true)
                    {
                        using (SqlCommand comando = new SqlCommand(ConsutaArticulos, NuevaConexion))
                        {
                            comando.ExecuteNonQuery();
                            Resultado += "ARTICULOS" + "\n";
                        }
                    }

                    if (this.CheckClientes.Checked == true)
                    {
                        using (SqlCommand comando2 = new SqlCommand(ConsultaClientes, NuevaConexion))
                        {
                            comando2.ExecuteNonQuery();
                            comando2.Parameters.Clear();
                            Resultado += "CLIENTES" + "\n";
                        }

                    }
                    if (this.CheckFacturacion.Checked == true)
                    {
                        Random r = new Random();
                        int VALORid = r.Next(10000, 900000);
                        for (int i = 1; i <= 7; i++)
                        {
                            VALORid = r.Next(500, 90000);
                            ConsultaFacturacion = "IF not  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[" + TablaFactu + "]') AND type in (N'U')) " +
                            "CREATE TABLE [" + TablaFactu + "]([EnlaceFactura] [int] primary key Not NULL, [NumeroFactura] int NOT NULL,[Apodo] [varchar](50) NULL,[Nombre] [varchar](60) NULL," +
                            "[Direccion] [varchar](50) NULL,[Calle] [varchar](30) NULL,[NumeroCalle] [varchar](20) NULL,[Dni] [Varchar](16) NULL,[Localidad] [varchar](50) NULL," +
                            "[Provincia] [varchar](50) NULL,[CodigoPostal] [varchar](20) NULL,[NonbreAlmacen] [varchar](30) NULL,[Marca] [varchar](50) NULL,[FechaFactura] [date] NULL," +
                            "[IvaImpuesto] [int] NULL,[SubTotal] [numeric](19, 3) NULL,[BaseIva] [numeric](19, 3) NULL,[TotalFactura] [numeric](19, 3) NULL,[CobradaFactura] [varchar](20) NULL," +
                            "[FechaCobro] [date] NULL,[Pais_Fact] [varchar](50) NULL,[TotalFactura2] [numeric](19, 3) NULL,[TipoNOTA] [varchar](30) NULL,[Obra_factu] [varchar](50) NULL," +
                            "[EjercicioTipo] [int]Not NULL,[SerieTipo] [varchar](4) NULL,[EmpresaEnlace] [int] NULL,[EnlaceDtconfi][int] NOT NULL," +
                            "CONSTRAINT F_" + TablaFactu + VALORid.ToString() + " FOREIGN KEY(EnlaceDtconfi)REFERENCES DtConfi(IdEnlace) ON UPDATE CASCADE ON DELETE CASCADE )" +

                            "IF not  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[" + Tabladetalle + "]') AND type in (N'U')) " +
                           "CREATE TABLE [" + Tabladetalle + "]( [ReferenciaDetalle][varchar](50) NULL,[CantidadDetalle] [numeric](13, 3) NULL,[DescripccionDetalle] [varchar](60) NULL," +
                           "[DescuentoDetalle] [numeric](19, 3) NULL,[PrecioDetalle] [numeric](19, 3) NULL,[IvaDetalle] [numeric](10, 3) NULL,[ImporteDetalle] [numeric](19, 3) NULL," +
                           "[EnlaceDetalle] [int] not NULL, CONSTRAINT FK_" + i + TablaFactu + VALORid + " FOREIGN KEY(EnlaceDetalle)REFERENCES " + TablaFactu + "(EnlaceFactura) ON UPDATE CASCADE ON DELETE CASCADE" + ")";


                            using (SqlCommand comando3 = new SqlCommand(ConsultaFacturacion, NuevaConexion))
                            {
                                if (i == 2)
                                {
                                    TablaFactu = "DtNota2";
                                    Tabladetalle = "DtDetalles_Nota2";
                                }
                                if (i == 3)
                                {

                                    Tabladetalle = "DtDetalles2_Nota2";
                                }
                                if (i == 4)
                                {
                                    TablaFactu = "DtAlbaran";
                                    Tabladetalle = "DtDetalles_Albaran";
                                }
                                if (i == 5)
                                {
                                    TablaFactu = "DtPresupuesto";
                                    Tabladetalle = "DtDetalles_Presupuesto";
                                }
                                if (i == 6)
                                {
                                    TablaFactu = "DtFactura";
                                    Tabladetalle = "DtDetalles_Fatura";
                                }

                                comando3.ExecuteNonQuery();
                                comando3.Parameters.Clear();
                            }


                        }
                        Resultado += "FACTURACION" + "\n";
                    }

                }
                MessageBox.Show(Resultado, "TABLAS CREADAS BASE DATOS LISTA PARA USAR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void BtnCrearTablas_Click(object sender, EventArgs e)
        {
            if (this.NombreBaseTablas.Text == string.Empty)
            {

                MessageBox.Show("debe Selecionar Base Datos A Crear en Lista", "SELECIONAR");
                return;
            }
            if (this.NombreBaseTablas.Text == "master" || this.NombreBaseTablas.Text == "msdb" || this.NombreBaseTablas.Text == "model" || this.NombreBaseTablas.Text == "tempdb")
            {
                MessageBox.Show("Bases De Datos No Validas" + "\n" + "[master]" + "\n" + "[model]" + "\n" + "[msdb]" + "\n" + "[tempdb]", "SELECION NO VALIDA");
                return;

            }


            if (this.CheckTablaArticulos.Checked == false && this.CheckClientes.Checked == false && this.CheckFacturacion.Checked == false && this.checkEmpresas.Checked == false)
            {

                MessageBox.Show("debe Selecionar Al Menos Una Tabla A Crear", "SELECIONAR");
                return;
            }
            this.InfoProcesoText.BackColor = Color.FromArgb(234, 210, 8);
            this.InfoProcesoText.Text = "Creando Tabla en Base Datos: " + this.NombreBaseTablas.Text + " .............. .......Espere Un Momento !!!!!!!";
            this.InfoProcesoText.Refresh();
            Application.DoEvents();
            if (this.ServidorCrear.Text != string.Empty)
            {

                CrearTablasSql();

            }
            else
            {
                this.NombreBaseDatos.SelectAll();
                MessageBox.Show("Nombre De base Datos a Crear Vacio", "CAMPOS VACIOS");
            }

            this.InfoProcesoText.Text = "Indica La Tabla a Crear";
            this.InfoProcesoText.BackColor = this.BackColor;
            this.InfoProcesoText.Refresh();
        }

        private void ListaBaseDatos_DoubleClick(object sender, EventArgs e)
        {
            if (this.ListaBaseDatos.SelectedIndex >= 0)
            {
                this.NombreBaseDatos.Text = this.ListaBaseDatos.SelectedItem.ToString();
                this.Backuptext.Text = this.ListaBaseDatos.SelectedItem.ToString();
                this.NombreBaseTablas.Text = this.ListaBaseDatos.SelectedItem.ToString();
                this.ServidoresLabel3.Text = this.ListaBaseDatos.SelectedItem.ToString();
                this.ServidoresLabel4.Text = this.ListaBaseDatos.SelectedItem.ToString();
                this.labelRestaurar.Text = this.ListaBaseDatos.SelectedItem.ToString();
            }

        }

        private void ServidorCrear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLower(e.KeyChar))
            {
                e.KeyChar = Char.ToUpper(e.KeyChar);
                e.Handled = false;
            }
        }

        private void BtnCrearBackup_Click(object sender, EventArgs e)
        {
            if (this.Backuptext.Text != string.Empty || this.Backuptext.Text != "0")
            {

                FolderBrowserDialog CarpetaElegir = new FolderBrowserDialog();
                if (CarpetaElegir.ShowDialog() == DialogResult.OK)
                {
                    this.InfoProcesoText.BackColor = Color.FromArgb(234, 210, 8);
                    this.InfoProcesoText.Text = " Creando Copia De Seguridad de Base Datos: " + this.NombreBaseDatos.Text + " .............. .......Espere Un Momento !!!!!!!";
                    this.InfoProcesoText.Refresh();
                    Application.DoEvents();
                    // string RutaOrigen = Directory.GetCurrentDirectory() + "\\" + ClasDatos.RutaDatosPrincipal;
                    string RutaDestino = CarpetaElegir.SelectedPath;
                    string CarpetaCopia = "Copia Seguridad De PeloscalvoApp";
                    string NonbreBackup = "Backup De " + this.Backuptext.Text + " " + String.Format("{0:dd-MM-yyyy hhmmss}", DateTime.Now);
                    //string BACKUP = RutaDestino + "\\" + CarpetaCopia;


                    if (!Directory.Exists(RutaDestino + "\\" + CarpetaCopia))
                    {
                        DirectoryInfo DirectiorioCopia = new DirectoryInfo(RutaDestino);
                        DirectiorioCopia.CreateSubdirectory(CarpetaCopia);

                    }


                    string Cadena2 = "master";
                    string consulta = "BACKUP DATABASE [" + this.Backuptext.Text + "] TO  DISK = N'" + RutaDestino + "\\" + CarpetaCopia + "\\" + "\\" + NonbreBackup + ".bak' WITH NOFORMAT, NOINIT,  NAME = N'" + this.Backuptext.Text + " Copia de seguridad', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
                    string cadenaConexion = "Data Source=" + this.ServidorCrear.Text + ";Initial Catalog=" + Cadena2 + ";Integrated Security=True";
                    try
                    {
                        using (SqlConnection NuevaConexion = new SqlConnection(cadenaConexion))
                        {
                            using (SqlCommand comando = new SqlCommand(consulta, NuevaConexion))
                            {
                                NuevaConexion.Open();
                                comando.ExecuteNonQuery();
                                MessageBox.Show("Se Creo Base Datos Correctamente" + "\n" + this.NombreBaseDatos, "CREAR BASE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message.ToString());
                    }
                }

                this.InfoProcesoText.Text = "Indica La Base Datos a Crear Copia De Seguridad";
                this.InfoProcesoText.BackColor = this.BackColor;
                this.InfoProcesoText.Refresh();
            }
        }

        private void ListaBaseDatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ListaBaseDatos.SelectedIndex >= 0)
            {
                this.NombreBaseDatos.Text = this.ListaBaseDatos.SelectedItem.ToString();
                this.Backuptext.Text = this.ListaBaseDatos.SelectedItem.ToString();
                this.NombreBaseTablas.Text = this.ListaBaseDatos.SelectedItem.ToString();
                this.ServidoresLabel3.Text = this.ListaBaseDatos.SelectedItem.ToString();
                this.ServidoresLabel4.Text = this.ListaBaseDatos.SelectedItem.ToString();
                this.labelRestaurar.Text = this.ListaBaseDatos.SelectedItem.ToString();
            }
        }

        private void TabCrear_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.InfoProcesoText.BackColor = this.BackColor;
            if (this.TabCrear.SelectedIndex == 0)
            {
                this.InfoProcesoText.Text = "Indica La Base Datos a Crear";
            }
            if (this.TabCrear.SelectedIndex == 1)
            {
                this.InfoProcesoText.Text = "Indica La Tabla a Crear";
            }
            if (this.TabCrear.SelectedIndex == 2)
            {
                this.InfoProcesoText.Text = "Indica La Base Datos a Crear Copia De Seguridad";
            }
            if (this.TabCrear.SelectedIndex == 3)
            {

                this.InfoProcesoText.Text = "Indica Instancia A Crear";
            }
            if (this.TabCrear.SelectedIndex == 4)
            {
                this.InfoProcesoText.Text = "Indica La Base Datos a Crear Restaurar";
            }
            this.InfoProcesoText.Refresh();
        }

        private void BtnCrearInstancia_Click(object sender, EventArgs e)
        {
            if (this.InstanciaNueva.Text != string.Empty)
            {
                if (MessageBox.Show("Crear La Nueva Instancia" + "\n" + this.InstanciaNueva.Text, "CREAR INSTANCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string Salida = string.Format("/C " + @"SqlLocalDB.exe Create """ + this.InstanciaNueva.Text + "");

                        Process.Start("cmd.exe", Salida);
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                }
            }
            else
            {
                this.InstanciaNueva.SelectAll();
                MessageBox.Show("Nombre Instancia Vacio o Falta Servidor", "CAMPOS VACIOS");
                return;
            }
        }

        private void BtnEliminarInstancia_Click(object sender, EventArgs e)
        {
            if (this.InstanciaNueva.Text != string.Empty)
            {
                if (MessageBox.Show("Eliminar Nueva Instancia" + "\n" + this.InstanciaNueva.Text, "ELIMINAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string Salida = string.Format("/C " + @"SqlLocalDB.exe Delete """ + this.InstanciaNueva.Text + "");
                        Process.Start("cmd.exe", Salida);
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                }
            }
            else
            {
                this.InstanciaNueva.SelectAll();
                MessageBox.Show("Nombre Instancia Vacio o Falta Servidor", "CAMPOS VACIOS");
                return;
            }
        }

        private void BtnActivarInstancia_Click(object sender, EventArgs e)
        {
            if (this.InstanciaNueva.Text != string.Empty)
            {
                if (MessageBox.Show("Activa Instancia" + "\n" + this.InstanciaNueva.Text, "ACTIVAR INSTANCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string Salida = string.Format("/C " + @"SqlLocalDB.exe Start """ + this.InstanciaNueva.Text + "");
                        Process.Start("cmd.exe", Salida); ;
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                }
            }
            else
            {
                this.InstanciaNueva.SelectAll();
                MessageBox.Show("Nombre Instancia Vacio o Falta Servidor", "CAMPOS VACIOS");
                return;
            }
        }

        private void BtnDesactivarInstancia_Click(object sender, EventArgs e)
        {
            if (this.InstanciaNueva.Text != string.Empty)
            {
                if (MessageBox.Show("Desactivar Instancia" + "\n" + this.InstanciaNueva.Text, "DESACTIVAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string Salida = string.Format("/C " + @"SqlLocalDB.exe Stop """ + this.InstanciaNueva.Text + "");
                        Process.Start("cmd.exe", Salida);
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                }
            }
            else
            {
                this.InstanciaNueva.SelectAll();
                MessageBox.Show("Nombre Instancia Vacio o Falta Servidor", "CAMPOS VACIOS");
                return;
            }
        }

        private void ServidorCrear_TextChanged(object sender, EventArgs e)
        {

        }

        private void ServidorCrear_Validated(object sender, EventArgs e)
        {
            //(BuscarServidor();
        }

        private void BtnRestaurarBases_Click(object sender, EventArgs e)
        {
            if (this.ServidorCrear.Text == string.Empty)
            {
                this.ServidorCrear.SelectAll();
                MessageBox.Show("Elija Un Servidor Correcto", "CAMPOS VACIOS");
                return;
            }
            if (this.labelRestaurar.Text == "master" || this.labelRestaurar.Text == "msdb" || this.labelRestaurar.Text == "model" || this.labelRestaurar.Text == "tempdb")
            {
                MessageBox.Show("Bases De Datos No Validas" + "\n" + "[master]" + "\n" + "[model]" + "\n" + "[msdb]" + "\n" + "[tempdb]", "SELECION NO VALIDA");
                return;

            }
            if (this.labelRestaurar.Text != string.Empty)
            {
                if (MessageBox.Show("Restaurar Todos Los Archivos" + "\n" + this.InstanciaNueva.Text, "RESTAURAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.InfoProcesoText.BackColor = Color.FromArgb(234, 210, 8);
                    this.InfoProcesoText.Text = " Restaurando Base Datos: " + this.NombreBaseDatos.Text + " .............. .......Espere Un Momento !!!!!!!";
                    this.InfoProcesoText.Refresh();
                    Application.DoEvents();
                    OpenFileDialog ArchivoElegir = new OpenFileDialog();
                    if (ArchivoElegir.ShowDialog() == DialogResult.OK)
                    {
                        //string archivo2 = ArchivoElegir.FileName;
                        //string Cadena2 = "master";
                        string Consuta = "RESTORE DATABASE [" + this.labelRestaurar.Text + "] FILE = N'" + this.labelRestaurar.Text + "' FROM  DISK = N'" + ArchivoElegir.FileName + ".bak' WITH  FILE = 1,  NOUNLOAD,  STATS = 10";
                        if (this.ServidorCrear.Text != string.Empty)
                        {

                            string cadenaConexion = "Data Source=" + this.ServidorCrear.Text + ";Initial Catalog=" + this.labelRestaurar.Text + ";Integrated Security=True";

                            try
                            {
                                using (SqlConnection NuevaConexion = new SqlConnection(cadenaConexion))
                                {
                                    using (SqlCommand comando = new SqlCommand(Consuta, NuevaConexion))
                                    {
                                        NuevaConexion.Open();
                                        comando.ExecuteNonQuery();
                                        this.ListaBaseDatos.Items.Add(this.NombreBaseDatos.Text);
                                        MessageBox.Show("Restauracion Con Exito" + "\n" + this.NombreBaseDatos.Text, "RESTAURAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message.ToString());
                            }

                            this.InfoProcesoText.Text = "Indica Base Datos a Restaurar";
                            this.InfoProcesoText.BackColor = this.BackColor;
                            this.InfoProcesoText.Refresh();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selecine Una Base Datos a Restaurar", "CAMPOS VACIOS");
                        return;
                    }
                }
            }
        }

        private void BtnLeerInstancias_Click(object sender, EventArgs e)
        {
            LeerInstancias();
        }

        private void AutenticacionCrear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.AutenticacionCrear.SelectedIndex == 0)
            {
                this.UsuarioSql2.Text = string.Format(@"{0}\" + Environment.UserName, Environment.MachineName);
                this.panelSql2.Enabled = false;
            }
            else
            {
                this.UsuarioSql2.Text = string.Empty;
                this.panelSql2.Enabled = true;
            }
        }

        private void checkPass2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkPass2.Checked == true)
            {
                this.checkPass2.Text = "Ocultar";
                this.ContraseñaSql2.PasswordChar = '\0';
            }
            else
            {
                this.checkPass2.Text = "Mostrar";
                this.ContraseñaSql2.PasswordChar = '*';

            }
        }

        private void ServidorCrear_Enter(object sender, EventArgs e)
        {
            this.ServidorCrear.Tag = "SEGUIR";
        }

        private void ListaNueva_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.ListaNueva.SelectedItem.ToString()))
            {
                this.ServidorCrear.Text = this.ListaNueva.SelectedItem.ToString();
            }

        }
    }
}


