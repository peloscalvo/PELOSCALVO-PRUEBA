﻿using Comun;
using ComunApp;
using Conexiones;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace PELOSCALVO
{
    public partial class FormFacturar : Form
    {
        public static FormFacturar menu2FACTURAR;
        int FilaGrid = 0;
      //  string SoloNumerosText = "";
        string RazonSocial;
        string Nombre;
        string Direcion;
        string Calle;
        string NumeroCalle;
        string Dni;
        string Poblacion;/// <summary>
        /// /
        /// </summary>
        string Localidad;
        string Provincia;
        string Pais;
        string CodigoPostal;
        string Obra;
        string Almacen;
        string Proveedor;
        string Fecha;
        bool Cobrado;
        string[] ListaTarifas = new string[] { "PVP1", "PVP2", "PVP3", "PVP4", "PVP5", "PVP6", "PLUS", "IVA", "salto" };
        public FormFacturar()
        {
            InitializeComponent();
            FormFacturar.menu2FACTURAR = this;
            ToolTip InfoEliminar = new ToolTip();
            InfoEliminar.SetToolTip(this.BtnEliminarFactura, "Eliminar Factura");
            InfoEliminar.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            InfoEliminar.IsBalloon = true;
            ToolTip Info = new ToolTip();
            Info.SetToolTip(this.BtnNuevoFactura, "Nueva Factura");
            Info.SetToolTip(this.BtnModificarFactura, "Moodificar Factura");
            Info.SetToolTip(this.BtnBuscarFactura, "Buscar Factura");
            // Info.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
        }
        public void AñadirIdFATU()
        {


            int ii = 0;
            foreach (var fila in this.dsFacturas.DtNuevaFactura)
            {
                fila["IdFila"] = ii.ToString();
                ii++;
            }

        }
        private void CalcularTotales(DataGridView DatagriCalcular)
        {
            if (this.dtDetallesFacturaBindingSource.Count > 0)
            {


                try
                {
                    double sumaIva = 0;
                    int columna = 7;
                    int columna2 = 6;

                    if (this.TabControlFactura.SelectedIndex == 2)
                    {
                        columna = 6;
                        columna2 = 6;
                    }
                    double Importe = 0;
                    double TTotalSuma = 0;
                    // int I = DatagriCalcular.CurrentCell.RowIndex;
                    foreach (DataGridViewRow Fila in DatagriCalcular.Rows)
                    {
                        if (Fila.Cells[4].Value != DBNull.Value && Fila.Cells[4].Value != null && Fila.Cells[4].Value.ToString() != string.Empty)
                        {
                            Importe = (Double)Fila.Cells[4].Value;
                            TTotalSuma = +(Double)Fila.Cells[columna].Value;
                            Fila.Cells[columna].Value = Importe.ToString();
                        }


                        if (Fila.Cells[columna2].Value != DBNull.Value && Fila.Cells[columna2].Value != null && Fila.Cells[columna2].Value.ToString() != string.Empty)
                        {

                            sumaIva += (Double)Fila.Cells[columna].Value - ((Double)Fila.Cells[columna].Value * (Convert.ToDouble(Fila.Cells[columna2].Value) / 100));
                        }

                    }

                    if (TTotalSuma == 0)
                    {
                        this.subTotal.Text = string.Format("{0:C" + this.NumPrecio.Value + "}", (0));
                        this.baseIva.Text = string.Format("{0:C" + this.NumPrecio.Value + "}", (0));
                        this.TotalFactura1.Text = string.Format("{0:C" + this.NumPrecio.Value + "}", (0));
                    }
                    else
                    {
                        this.subTotal.Text = string.Format("{0:C" + this.NumPrecio.Value + "}", (TTotalSuma));
                        this.baseIva.Text = string.Format("{0:C" + this.NumPrecio.Value + "}", (TTotalSuma - sumaIva));
                        this.TotalFactura1.Text = string.Format("{0:C" + this.NumPrecio.Value + "}", (TTotalSuma + (TTotalSuma - sumaIva)));
                    }

                }
                catch (Exception ex)
                {


                    MessageBox.Show(ex.Message, "ERROR CALCULAR TOTALES", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        private void CalcularImportes(DataGridView DatagriCalcular)
        {
            try
            {
                int FILAcelda = DatagriCalcular.CurrentCell.RowIndex;
                if (string.IsNullOrEmpty(DatagriCalcular.Rows[FILAcelda].Cells[2].Value.ToString()) | string.IsNullOrEmpty(DatagriCalcular.Rows[FILAcelda].Cells[4].Value.ToString()))
                {
                    return;
                }
                double cantidad = 0;
                double precio = 0;
                double descuento = 0;
                double importe;
                double TTotalSuma = 0;
                double sumaIva = 0;
                int columna = 7;
                int columna2 = 6;

                if (this.TabControlFactura.SelectedIndex == 2)
                {
                    columna = 6;
                    columna2 = 6;
                }
                if (DatagriCalcular.CurrentCell.RowIndex > -1)
                {
                    if (ClasDatos.NombreFactura == "Albaranes")
                    {
                        if (DatagriCalcular.CurrentCell.RowIndex >= 40)
                        {
                            DatagriCalcular.AllowUserToAddRows = false;
                        }
                    }

                    if (DatagriCalcular.Rows[FILAcelda].Cells[2].Value != DBNull.Value)
                    {
                        cantidad = Convert.ToDouble(DatagriCalcular.Rows[FILAcelda].Cells[2].Value);
                    }
                    if (DatagriCalcular.Rows[FILAcelda].Cells[4].Value != DBNull.Value && DatagriCalcular.Rows[FILAcelda].Cells[4].Value.ToString() != string.Empty && DatagriCalcular.Rows[FILAcelda].Cells[4].Value.ToString() != null)
                    {
                        precio = Convert.ToDouble(DatagriCalcular.Rows[FILAcelda].Cells[4].Value);

                    }
                    if (DatagriCalcular.Rows[FILAcelda].Cells[5].Value != DBNull.Value && DatagriCalcular.Rows[FILAcelda].Cells[5].Value.ToString() != string.Empty)
                    {
                        descuento = Convert.ToDouble(DatagriCalcular.Rows[FILAcelda].Cells[5].Value);
                    }
                    if (DatagriCalcular.Rows[FILAcelda].Cells[6].Value == DBNull.Value || DatagriCalcular.Rows[FILAcelda].Cells[6].Value.ToString() == string.Empty)
                    {
                        DatagriCalcular.Rows[FILAcelda].Cells[6].Value = this.IvaFactuTxt.Value;
                    }

                    importe = precio * cantidad - (precio * cantidad) * ((descuento / 100));
                    DatagriCalcular.CurrentRow.Cells[columna].Value = importe.ToString();




                    foreach (DataGridViewRow row in DatagriCalcular.Rows)
                    {

                        if (row.Cells[columna].Value != null && row.Cells[columna].Value != DBNull.Value && row.Cells[columna].Value.ToString() != string.Empty)
                        {

                            TTotalSuma += (Double)row.Cells[columna].Value;


                            if (row.Cells[columna2].Value != DBNull.Value && row.Cells[columna2].Value != null && row.Cells[columna2].Value.ToString() != string.Empty)
                            {

                                sumaIva += (Double)row.Cells[columna].Value - ((Double)row.Cells[columna].Value * (Convert.ToDouble(row.Cells[columna2].Value) / 100));
                            }

                        }
                    }
                    if (DatagriCalcular.Rows[FILAcelda].Cells[4].Value == DBNull.Value || DatagriCalcular.Rows[FILAcelda].Cells[2].Value == DBNull.Value)
                    {
                        DatagriCalcular.Rows[FILAcelda].Cells[columna].Value = DBNull.Value;
                    }
                    if (this.TabControlFactura.SelectedIndex == 2)
                    {
                        this.TotalFactura2.Text = string.Format("{0:C" + this.NumPrecio.Value + "}", (TTotalSuma));
                    }
                    else
                    {
                        if (TTotalSuma == 0)
                        {
                            this.subTotal.Text = string.Format("{0:C" + this.NumPrecio.Value + "}", (0));
                            this.baseIva.Text = string.Format("{0:C" + this.NumPrecio.Value + "}", (0));
                            this.TotalFactura1.Text = string.Format("{0:C" + this.NumPrecio.Value + "}", (0));
                        }
                        else
                        {
                            this.subTotal.Text = string.Format("{0:C" + this.NumPrecio.Value + "}", (TTotalSuma));
                            this.baseIva.Text = string.Format("{0:C" + this.NumPrecio.Value + "}", (TTotalSuma - sumaIva));
                            this.TotalFactura1.Text = string.Format("{0:C" + this.NumPrecio.Value + "}", (TTotalSuma + (TTotalSuma - sumaIva)));
                        }
                    }


                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString(), "CALCULAR IMPORTES");
            }

        }
        private void LlenarGrid(DataGridView dataGrid, int valor)
        {
            int II = 0;
            try
            {
                // dtDetallesFacturaBindingSource.Clear();
                foreach (var item in ClasDetalleGrid.Listadetalle1.lista)
                {
                    if (valor == 1)
                    {
                        if (dataGrid.RowCount > ClasDetalleGrid.Listadetalle1.lista.Count + 1)
                        {
                            // this.dtDetallesFacturaBindingSource.RemoveCurrent();
                            this.dtDetallesFacturaDataGridView.Rows.RemoveAt(this.dtDetallesFacturaDataGridView.RowCount);
                        }
                        if (this.dtDetallesFacturaDataGridView.RowCount < ClasDetalleGrid.Listadetalle1.lista.Count + 1)
                        {
                            this.dtDetallesFacturaBindingSource.AddNew();
                        }

                    }
                    else
                    {
                        if (dataGrid.RowCount > ClasDetalleGrid.Listadetalle2.lista.Count + 1)
                        {
                            // this.dtDetallesFactura2BindingSource.RemoveCurrent();
                            this.dtDetallesFacturaDataGridView2.Rows.RemoveAt(this.dtDetallesFacturaDataGridView2.RowCount);
                        }
                        if (dataGrid.RowCount < ClasDetalleGrid.Listadetalle2.lista.Count + 1)
                        {
                            this.dtDetallesFactura2BindingSource.AddNew();
                        }

                    }


                    if (!string.IsNullOrEmpty(item.Referencia))
                    {
                        dataGrid.Rows[II].Cells[0].Value = item.Referencia;
                    }
                    if (!string.IsNullOrEmpty(item.Cantidad))
                    {
                        dataGrid.Rows[II].Cells[2].Value = item.Cantidad;
                    }
                    if (!string.IsNullOrEmpty(item.Descripcci))
                    {
                        dataGrid.Rows[II].Cells[3].Value = item.Descripcci;
                    }
                    if (!string.IsNullOrEmpty(item.Precio))
                    {
                        dataGrid.Rows[II].Cells[4].Value = item.Precio;
                    }
                    if (!string.IsNullOrEmpty(item.Descuento))
                    {
                        dataGrid.Rows[II].Cells[5].Value = item.Descuento;
                    }
                    if (!string.IsNullOrEmpty(item.Iva))
                    {
                        dataGrid.Rows[II].Cells[6].Value = item.Iva;
                    }
                    if (!string.IsNullOrEmpty(item.Importe))
                    {
                        dataGrid.Rows[II].Cells[7].Value = item.Importe;
                    }
                    II++;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void ExtraerGrid(DataGridView dataGrid, int valor)
        {
            try
            {
                int V = 0;
                if (valor == 1)
                {
                    ClasDetalleGrid.Listadetalle1.lista.Clear();
                }
                else
                {
                    ClasDetalleGrid.Listadetalle2.lista.Clear();
                }

                foreach (DataGridViewRow row in dataGrid.Rows)
                {
                    if (V >= dataGrid.RowCount - 1)
                    {
                        break;
                    }
                    ClasDetalleGrid.Detalle item = new ClasDetalleGrid.Detalle();
                    if (row.Cells[0].Value.ToString() != string.Empty)
                    {
                        item.Referencia = row.Cells[0].Value.ToString();
                    }
                    if (row.Cells[2].Value.ToString() != string.Empty)
                    {
                        item.Cantidad = row.Cells[2].Value.ToString();
                    }
                    if (row.Cells[3].Value.ToString() != string.Empty)
                    {
                        item.Descripcci = row.Cells[3].Value.ToString();
                    }
                    if (row.Cells[4].Value.ToString() != string.Empty)
                    {
                        item.Precio = row.Cells[4].Value.ToString();
                    }
                    if (row.Cells[5].Value.ToString() != string.Empty)
                    {
                        item.Descuento = row.Cells[5].Value.ToString();
                    }
                    if (valor == 1)
                    {
                        if (row.Cells[6].Value.ToString() != string.Empty)
                        {
                            item.Iva = row.Cells[6].Value.ToString();
                        }
                    }
                    if (valor == 1)
                    {
                        if (row.Cells[7].Value.ToString() != string.Empty)
                        {
                            item.Importe = row.Cells[7].Value.ToString();
                        }
                    }
                    else
                    {
                        if (row.Cells[6].Value.ToString() != string.Empty)
                        {
                            item.Importe = row.Cells[6].Value.ToString();
                        }
                    }

                    if (valor == 1)
                    {
                        ClasDetalleGrid.Listadetalle1.lista.Add(item);
                    }
                    else
                    {
                        ClasDetalleGrid.Listadetalle2.lista.Add(item);
                    }

                    V++;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
        }
        public void CargarClienteFatu(int IdCliente)
        {
            foreach (Control Texbox2 in tabPage1Factura.Controls)
            {
                if (Texbox2 is TextBox | Texbox2 is ComboBox)
                {
                    if (Texbox2.Name != NumeroFactura.Name | Texbox2.Name != SerieFatu.Name |Texbox2.Name != ProveedorTxt.Name | Texbox2.Name != ObraFactuTxt.Name)
                    {
                        Texbox2.Text = string.Empty;
                    }
             
                    //MessageBox.Show(Texbox2.Name);
                }

            }

            //FechaAltaCliente.Text = string.Empty;
            string Tabla = FormMenuPrincipal.menu2principal.InfoClientes.Text;

            string ConsultaCli = "SELECT [Id],[APODOCLIEN],[NOMBRECLIE] ,[DIRECCIONC],[TELEFONOCL],[MOVILCLIEN]" +
                ",[CORREOCLIE],[DNICLIENTE],[LOCALIDADC],[CODIGOPOST],[PAISCLIENT],[FECHAALTAC],[CALLECLIEN]" +
                ",[NUMEROCALL],[PROVINCIAC],[TARIFATIPO],[TIPODNI],[TIPOCLIENT],[DESCUENTOC],[NUMEROCUEN]" +
                ",[PORTES],[BANCOOFICI],[BANCOPROVI],[BANCODIREC],[BANCOLOCAL],[BANCOIBAN],[BANCOCODIG]" +
                ",[BANCOENTID],[BANCOOFIC2],[BANCODC],[BANCON_CUE],[BAJA] FROM[" + Tabla + "]  WHERE Id=" + IdCliente;

            if (ClsConexionSql.SibaseDatosSql)
            {
                ClsConexionSql NuevaConexion = new ClsConexionSql(ConsultaCli);
                if (NuevaConexion.SiConexionSql)
                {
                    SqlDataReader leer = NuevaConexion.ComandoSql.ExecuteReader();
                    if (leer.HasRows)
                    {
                        if (leer.Read())
                        {
                            //  dtClientesBindingSource.Current. = leer.ToString();
                            if (!string.IsNullOrEmpty((leer["APODOCLIEN"]).ToString()))
                            {
                                RazonSocialFatu.Text = Convert.ToString(leer["APODOCLIEN"]);
                            }
                            if (!string.IsNullOrEmpty((leer["NOMBRECLIE"]).ToString()))
                            {
                                NombreClienteFatu.Text = Convert.ToString(leer["NOMBRECLIE"]);
                            }
                            if (!string.IsNullOrEmpty((leer["DIRECCIONC"]).ToString()))
                            {
                                DirecionClienteFatu.Text = Convert.ToString(leer["DIRECCIONC"]);
                            }
                            if (!string.IsNullOrEmpty((leer["TELEFONOCL"]).ToString()))
                            {
                               // TLEF.Text = Convert.ToString(leer["TELEFONOCL"]);
                            }
                            if (!string.IsNullOrEmpty((leer["MOVILCLIEN"]).ToString()))
                            {
                              //  MOVI.Text = Convert.ToString(leer["MOVILCLIEN"]);
                            }
                            if (!string.IsNullOrEmpty((leer["CORREOCLIE"]).ToString()))
                            {
                                //CORRE.Text = Convert.ToString(leer["CORREOCLIE"]);
                            }
                            if (!string.IsNullOrEmpty((leer["DNICLIENTE"]).ToString()))
                            {
                                DniTextBox.Text = Convert.ToString(leer["DNICLIENTE"]);
                            }
                            if (!string.IsNullOrEmpty((leer["LOCALIDADC"]).ToString()))
                            {
                                LocalidadTxt.Text = Convert.ToString(leer["LOCALIDADC"]);
                            }
                            if (!string.IsNullOrEmpty((leer["CODIGOPOST"]).ToString()))
                            {
                                CodigoPostalTxt.Text = Convert.ToString(leer["CODIGOPOST"]);
                            }
                            if (!string.IsNullOrEmpty((leer["PAISCLIENT"]).ToString()))
                            {
                                PaisFatuTxt.Text = Convert.ToString(leer["PAISCLIENT"]);
                            }
                            if (!string.IsNullOrEmpty((leer["FECHAALTAC"]).ToString()))
                            {
                               // FechaAltaCliente.Text = Convert.ToString(leer["FECHAALTAC"]);
                            }
                            if (!string.IsNullOrEmpty((leer["CALLECLIEN"]).ToString()))
                            {
                                CalleTex.Text = Convert.ToString(leer["CALLECLIEN"]);
                            }
                            if (!string.IsNullOrEmpty((leer["NUMEROCALL"]).ToString()))
                            {
                                NumeroCalleTxt.Text = Convert.ToString(leer["NUMEROCALL"]);
                            }
                            if (!string.IsNullOrEmpty((leer["PROVINCIAC"]).ToString()))
                            {
                                ProvinciaTxt.Text = Convert.ToString(leer["PROVINCIAC"]);
                            }
                            if (!string.IsNullOrEmpty((leer["TARIFATIPO"]).ToString()))
                            {
                               // TipoTarifa.Text = Convert.ToString(leer["TARIFATIPO"]);
                            }
                            if (!string.IsNullOrEmpty((leer["TIPODNI"]).ToString()))
                            {
                                //TipoDocumento.Text = Convert.ToString(leer["TIPODNI"]);
                            }
                            if (!string.IsNullOrEmpty((leer["TIPOCLIENT"]).ToString()))
                            {
                               // TipoCliente.Text = Convert.ToString(leer["TIPOCLIENT"]);
                            }
                            if (!string.IsNullOrEmpty((leer["DESCUENTOC"]).ToString()))
                            {
                               // DescuentoCliente.Text = Convert.ToString(leer["DESCUENTOC"]);
                            }
                            if (!string.IsNullOrEmpty((leer["NUMEROCUEN"]).ToString()))
                            {
                               // NUMEROCUENTextBox.Text = Convert.ToString(leer["NUMEROCUEN"]);
                            }
                            if (!string.IsNullOrEmpty((leer["PORTES"]).ToString()))
                            {
                               // PortesTxt.Text = Convert.ToString(leer["PORTES"]);
                            }
                            if (!string.IsNullOrEmpty((leer["BANCOOFICI"]).ToString()))
                            {
                               // bANCOOFICITextBox.Text = Convert.ToString(leer["BANCOOFICI"]);
                            }
                            if (!string.IsNullOrEmpty((leer["BANCOPROVI"]).ToString()))
                            {
                                //BancoProvincia.Text = Convert.ToString(leer["BANCOPROVI"]);
                            }
                            if (!string.IsNullOrEmpty((leer["BANCODIREC"]).ToString()))
                            {
                               // bANCODIRECTextBox.Text = Convert.ToString(leer["BANCODIREC"]);
                            }
                            if (!string.IsNullOrEmpty((leer["BANCOLOCAL"]).ToString()))
                            {
                               // bANCOLOCALTextBox.Text = Convert.ToString(leer["BANCOLOCAL"]);
                            }
                            if (!string.IsNullOrEmpty((leer["BANCOIBAN"]).ToString()))
                            {
                              //  bANCOIBANTextBox.Text = Convert.ToString(leer["BANCOIBAN"]);
                            }
                            if (!string.IsNullOrEmpty((leer["BANCOCODIG"]).ToString()))
                            {
                              //  bANCOCODIGTextBox.Text = Convert.ToString(leer["BANCOCODIG"]);
                            }
                            if (!string.IsNullOrEmpty((leer["BANCOENTID"]).ToString()))
                            {
                               // bANCOENTIDTextBox.Text = Convert.ToString(leer["BANCOENTID"]);
                            }
                            if (!string.IsNullOrEmpty((leer["BANCOOFIC2"]).ToString()))
                            {
                              //  BANCOOFIC2TextBox.Text = Convert.ToString(leer["BANCOOFIC2"]);
                            }
                            if (!string.IsNullOrEmpty((leer["BANCODC"]).ToString()))
                            {
                               // bANCODCTextBox.Text = Convert.ToString(leer["BANCODC"]);
                            }
                            if (!string.IsNullOrEmpty((leer["BANCON_CUE"]).ToString()))
                            {
                             //   bANCON_CUETextBox.Text = Convert.ToString(leer["BANCON_CUE"]);
                            }
                            if (!string.IsNullOrEmpty((leer["BAJA"]).ToString()))
                            {
                                // ApodoClienteTex.Text = Convert.ToBoolean(leer["BAJA"]);
                            }
                            leer.Close();
                        }
                    }

                }
            }
            else
            {
                if (File.Exists(ClasDatos.RutaBaseDatosDb))
                {
                    ClsConexionDb NuevaConexion = new ClsConexionDb(ConsultaCli);
                    if (NuevaConexion.SiConexionDb)
                    {
                        OleDbDataReader leer = NuevaConexion.ComandoDb.ExecuteReader();
                        if (leer.HasRows)
                        {
                            if (leer.Read())
                            {
                                //  dtClientesBindingSource.Current. = leer.ToString();
                                if (!string.IsNullOrEmpty((leer["APODOCLIEN"]).ToString()))
                                {
                                    RazonSocialFatu.Text = Convert.ToString(leer["APODOCLIEN"]);
                                }
                                if (!string.IsNullOrEmpty((leer["NOMBRECLIE"]).ToString()))
                                {
                                    NombreClienteFatu.Text = Convert.ToString(leer["NOMBRECLIE"]);
                                }
                                if (!string.IsNullOrEmpty((leer["DIRECCIONC"]).ToString()))
                                {
                                    DirecionClienteFatu.Text = Convert.ToString(leer["DIRECCIONC"]);
                                }
                                if (!string.IsNullOrEmpty((leer["TELEFONOCL"]).ToString()))
                                {
                                    // TLEF.Text = Convert.ToString(leer["TELEFONOCL"]);
                                }
                                if (!string.IsNullOrEmpty((leer["MOVILCLIEN"]).ToString()))
                                {
                                    //  MOVI.Text = Convert.ToString(leer["MOVILCLIEN"]);
                                }
                                if (!string.IsNullOrEmpty((leer["CORREOCLIE"]).ToString()))
                                {
                                    //CORRE.Text = Convert.ToString(leer["CORREOCLIE"]);
                                }
                                if (!string.IsNullOrEmpty((leer["DNICLIENTE"]).ToString()))
                                {
                                    DniTextBox.Text = Convert.ToString(leer["DNICLIENTE"]);
                                }
                                if (!string.IsNullOrEmpty((leer["LOCALIDADC"]).ToString()))
                                {
                                    LocalidadTxt.Text = Convert.ToString(leer["LOCALIDADC"]);
                                }
                                if (!string.IsNullOrEmpty((leer["CODIGOPOST"]).ToString()))
                                {
                                    CodigoPostalTxt.Text = Convert.ToString(leer["CODIGOPOST"]);
                                }
                                if (!string.IsNullOrEmpty((leer["PAISCLIENT"]).ToString()))
                                {
                                    PaisFatuTxt.Text = Convert.ToString(leer["PAISCLIENT"]);
                                }
                                if (!string.IsNullOrEmpty((leer["FECHAALTAC"]).ToString()))
                                {
                                    // FechaAltaCliente.Text = Convert.ToString(leer["FECHAALTAC"]);
                                }
                                if (!string.IsNullOrEmpty((leer["CALLECLIEN"]).ToString()))
                                {
                                    CalleTex.Text = Convert.ToString(leer["CALLECLIEN"]);
                                }
                                if (!string.IsNullOrEmpty((leer["NUMEROCALL"]).ToString()))
                                {
                                    NumeroCalleTxt.Text = Convert.ToString(leer["NUMEROCALL"]);
                                }
                                if (!string.IsNullOrEmpty((leer["PROVINCIAC"]).ToString()))
                                {
                                    ProvinciaTxt.Text = Convert.ToString(leer["PROVINCIAC"]);
                                }
                                if (!string.IsNullOrEmpty((leer["TARIFATIPO"]).ToString()))
                                {
                                    // TipoTarifa.Text = Convert.ToString(leer["TARIFATIPO"]);
                                }
                                if (!string.IsNullOrEmpty((leer["TIPODNI"]).ToString()))
                                {
                                    //TipoDocumento.Text = Convert.ToString(leer["TIPODNI"]);
                                }
                                if (!string.IsNullOrEmpty((leer["TIPOCLIENT"]).ToString()))
                                {
                                    // TipoCliente.Text = Convert.ToString(leer["TIPOCLIENT"]);
                                }
                                if (!string.IsNullOrEmpty((leer["DESCUENTOC"]).ToString()))
                                {
                                    // DescuentoCliente.Text = Convert.ToString(leer["DESCUENTOC"]);
                                }
                                if (!string.IsNullOrEmpty((leer["NUMEROCUEN"]).ToString()))
                                {
                                    // NUMEROCUENTextBox.Text = Convert.ToString(leer["NUMEROCUEN"]);
                                }
                                if (!string.IsNullOrEmpty((leer["PORTES"]).ToString()))
                                {
                                    // PortesTxt.Text = Convert.ToString(leer["PORTES"]);
                                }
                                if (!string.IsNullOrEmpty((leer["BANCOOFICI"]).ToString()))
                                {
                                    // bANCOOFICITextBox.Text = Convert.ToString(leer["BANCOOFICI"]);
                                }
                                if (!string.IsNullOrEmpty((leer["BANCOPROVI"]).ToString()))
                                {
                                    //BancoProvincia.Text = Convert.ToString(leer["BANCOPROVI"]);
                                }
                                if (!string.IsNullOrEmpty((leer["BANCODIREC"]).ToString()))
                                {
                                    // bANCODIRECTextBox.Text = Convert.ToString(leer["BANCODIREC"]);
                                }
                                if (!string.IsNullOrEmpty((leer["BANCOLOCAL"]).ToString()))
                                {
                                    // bANCOLOCALTextBox.Text = Convert.ToString(leer["BANCOLOCAL"]);
                                }
                                if (!string.IsNullOrEmpty((leer["BANCOIBAN"]).ToString()))
                                {
                                    //  bANCOIBANTextBox.Text = Convert.ToString(leer["BANCOIBAN"]);
                                }
                                if (!string.IsNullOrEmpty((leer["BANCOCODIG"]).ToString()))
                                {
                                    //  bANCOCODIGTextBox.Text = Convert.ToString(leer["BANCOCODIG"]);
                                }
                                if (!string.IsNullOrEmpty((leer["BANCOENTID"]).ToString()))
                                {
                                    // bANCOENTIDTextBox.Text = Convert.ToString(leer["BANCOENTID"]);
                                }
                                if (!string.IsNullOrEmpty((leer["BANCOOFIC2"]).ToString()))
                                {
                                    //  BANCOOFIC2TextBox.Text = Convert.ToString(leer["BANCOOFIC2"]);
                                }
                                if (!string.IsNullOrEmpty((leer["BANCODC"]).ToString()))
                                {
                                    // bANCODCTextBox.Text = Convert.ToString(leer["BANCODC"]);
                                }
                                if (!string.IsNullOrEmpty((leer["BANCON_CUE"]).ToString()))
                                {
                                    //   bANCON_CUETextBox.Text = Convert.ToString(leer["BANCON_CUE"]);
                                }
                                if (!string.IsNullOrEmpty((leer["BAJA"]).ToString()))
                                {
                                    // ApodoClienteTex.Text = Convert.ToBoolean(leer["BAJA"]);
                                }
                                leer.Close();
                            }
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Archivo No Se Encuentra", " FALLO AL GUARDAR ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    // this.panelBotonesClientes.Enabled = false;
                }
            }
        }
        private void analizarStock(DataGridView DatagriStock)
        {
            int Fila2 = 0;
            foreach (DataGridViewRow filaStock in DatagriStock.Rows)
            {
                if (filaStock.Cells[0].Value.ToString() != string.Empty)
                {
                    Fila2 = (int)filaStock.Cells[0].Value;
                }

            }

        }
        private bool VALIDARcampos()
        {
            bool ok = true;
            if (this.RazonSocialFatu.Text.Length < 4)
            {
                ok = false;
                this.errorProvider1.SetError(this.RazonSocialFatu, "_ingresar Razon Social (( minimo 4 Caracteres))");
            }
            if (this.NombreClienteFatu.Text.Length < 4)
            {
                ok = false;
                this.errorProvider1.SetError(this.NombreClienteFatu, "_ingresar NOMBRE (( minimo 4 Caracteres))");
            }
            if (this.DirecionClienteFatu.Text.Length < 4)
            {
                ok = false;
                this.errorProvider1.SetError(this.DirecionClienteFatu, "_ingresar Direcion (( minimo 4 Caracteres))");
            }
            if (this.DniTextBox.Text.Length < 4)
            {
                ok = false;
                this.errorProvider1.SetError(this.DniTextBox, "_ingresar Dni (( minimo 4 Caracteres))");
            }
            if (String.IsNullOrEmpty(this.AlmacenTxt.Text))
            {
                ok = false;
                this.errorProvider1.SetError(this.AlmacenTxt, "_ingresar Almacen (( minimo 1 Caracteres))");
            }
            return ok;

        }
        private void BORRARerrores()
        {
            this.errorProvider1.SetError(this.RazonSocialFatu, "");
            this.errorProvider1.SetError(this.NombreClienteFatu, "");
            this.errorProvider1.SetError(this.DirecionClienteFatu, "");
            this.errorProvider1.SetError(this.DniTextBox, "");
            this.errorProvider1.SetError(this.NumeroFactura, "");
            this.errorProvider1.SetError(this.AlmacenTxt, "");
        }


        public void GuardarFactuDB()
        {
            Random r = new Random();
            int Id_valor = r.Next(3, 99999);
            string Consulta = "";
            Int32 EnlaceDtconfi = 0;
            // Int32 Id_Ejercicio = 0;
            int Id = this.ejerciciosDeAñoComboBox.SelectedIndex;
            try
            {
                if (Id > this.dtConfiBindingSource.Count - 1)
                {
                    MessageBox.Show("Falta Id De Ejercicios", "ERROR APP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!String.IsNullOrEmpty(FormMenuPrincipal.menu2principal.dsCONFIGURACCION.Tables["DtConfi"].Rows[Id]["IdEnlace"].ToString()))
                {
                    EnlaceDtconfi = Convert.ToInt32(FormMenuPrincipal.menu2principal.dsCONFIGURACCION.Tables["DtConfi"].Rows[Id]["IdEnlace"].ToString());
                }
                else
                {
                    this.panelBotones.Enabled = false;
                    MessageBox.Show("No Se Puede Continuar", "ERROR FALTAN DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!String.IsNullOrEmpty(FormMenuPrincipal.menu2principal.dsCONFIGURACCION.Tables["DtConfi"].Rows[Id]["IdEnlace"].ToString()))
                {
                    //  Id_Ejercicio = Convert.ToInt32(FormMenuPrincipal.menu2principal.dsCONFIGURACCION.Tables["DtConfi"].Rows[Id]["IdEnlace"].ToString());
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "NO GUARDO NADA FALTAN DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            string ConsultaEliminar = "DELETE FROM [DtDetalles_" + ClasDatos.NombreFactura + "] WHERE [EnlaceDetalle]= @EnlaceFactu";
            string ConsultaEliminar2 = "DELETE FROM [DtDetalles2_" + ClasDatos.NombreFactura + "] WHERE [EnlaceDetalle]=@EnlaceFactu";
            string ConsultaDetalle = "INSERT INTO [DtDetalles_" + ClasDatos.NombreFactura + "] ([ReferenciaDetalle],[CantidadDetalle],[DescripccionDetalle]," +
             " [PrecioDetalle],[DescuentoDetalle],[IvaDetalle] ,[ImporteDetalle],[EnlaceDetalle])" +
             " VALUES( @ReferenciaDetalle, @CantidadDetalle, @DescripccionDetalle, @PrecioDetalle, " +
             " @DescuentoDetalle, @IvaDetalle, @ImporteDetalle, @EnlaceDetalle) ";

            string ConsultaDetalle2 = "INSERT INTO [DtDetalles2_" + ClasDatos.NombreFactura + "] ([ReferenciaDetalle],[CantidadDetalle],[DescripccionDetalle]" +
                         " ,[PrecioDetalle],[DescuentoDetalle],[ImporteDetalle],[EnlaceDetalle])" +
                         " VALUES( @ReferenciaDetalle, @CantidadDetalle, @DescripccionDetalle, @PrecioDetalle, " +
                         " @DescuentoDetalle, @ImporteDetalle, @EnlaceDetalle) ";
            if (this.panelBotones.Tag.ToString() == "Nuevo")
            {
                Consulta = "INSERT INTO [Dt" + ClasDatos.NombreFactura + "]([EnlaceFactura],[NumeroFactura],[Apodo] ,[Nombre],[Direccion],[Calle]" +
                ",[NumeroCalle] ,[Dni],[Localidad],[Provincia],[CodigoPostal],[NonbreAlmacen],[Marca]" +
                ",[IvaImpuesto],[SubTotal],[BaseIva] ,[TotalFactura],[TotalFactura2],[Pais_Fact]" +
                " ,[TipoNOTA],[Obra_factu],[EjercicioTipo],[SerieTipo],[EmpresaEnlace],[FechaFactura],[FechaCobro],[CobradaFactura],[EnlaceDtconfi])" +
               " VALUES(@EnlaceFactura, @NumeroFactura, @Apodo, @Nombre, @Direccion, @Calle, " +
               "@NumeroCalle, @Dni, @Localidad, @Provincia, @CodigoPostal, @NonbreAlmacen,@Marca," +
                " @IvaImpuesto, @SubTotal, @BaseIva, @TotalFactura, @TotalFactura2," +
                " @Pais_Fact, @TipoNOTA, @Obra_factu, @EjercicioTipo,@SerieTipo,@EmpresaEnlace, @FechaFactura, @FechaCobro, @CobradaFactura, @EnlaceDtconfi)";

            }
            else
            {
                Consulta = "UPDATE [Dt" + ClasDatos.NombreFactura + "] SET [EnlaceFactura]= @EnlaceFactura, [NumeroFactura] = @NumeroFactura,[Apodo] = @Apodo,[Nombre] = @Nombre," +
               " [Direccion] = @Direccion, [Calle] = @Calle, [NumeroCalle] = @NumeroCalle, [Dni] = @Dni," +
               " [Localidad] = @Localidad, [Provincia] = @Provincia,[CodigoPostal] = @CodigoPostal, " +
               " [NonbreAlmacen] = @NonbreAlmacen, [Marca] = @Marca,[IvaImpuesto] = @IvaImpuesto, [SubTotal] = @SubTotal," +
               " [BaseIva] = @BaseIva, [TotalFactura] = @TotalFactura, [TotalFactura2] = @TotalFactura2, " +
               " [Pais_Fact] = @Pais_Fact,[TipoNOTA] = @TipoNOTA, [Obra_factu] = @Obra_factu, [EjercicioTipo] = @EjercicioTipo," +
               "[SerieTipo]= @SerieTipo,[EmpresaEnlace]=@EmpresaEnlace, [FechaFactura] = @FechaFactura ,[FechaCobro] = @FechaCobro , [CobradaFactura] = @CobradaFactura, [EnlaceDtconfi] = @EnlaceDtconfi WHERE [EnlaceFactura] = @EnlaceFactura";

            }

            ClsConexionDb NuevaConexion = new ClsConexionDb(Consulta);
            ClsConexionDb NuevaConexionDetalle = new ClsConexionDb(ConsultaDetalle);
            ClsConexionDb NuevaConexionDetalle2 = new ClsConexionDb(ConsultaDetalle2);
            ClsConexionDb ConexionDetalleEliminar = new ClsConexionDb(ConsultaEliminar);
            if (NuevaConexion.SiConexionDb)
            {
                try
                {
                    NuevaConexion.ComandoDb.Parameters.AddWithValue("@EnlaceFactura", string.IsNullOrEmpty(this.EnlaceFactu.Text) ? (object)DBNull.Value : Convert.ToInt32(this.EnlaceFactu.Text));
                    NuevaConexion.ComandoDb.Parameters.AddWithValue("@NumeroFactura", string.IsNullOrEmpty(this.NumeroFactura.Text) ? (object)DBNull.Value : Convert.ToInt32(this.NumeroFactura.Text));
                    NuevaConexion.ComandoDb.Parameters.AddWithValue("@Apodo", string.IsNullOrEmpty(this.RazonSocialFatu.Text) ? (object)DBNull.Value : this.RazonSocialFatu.Text);
                    NuevaConexion.ComandoDb.Parameters.AddWithValue("@Nombre", string.IsNullOrEmpty(this.NombreClienteFatu.Text) ? (object)DBNull.Value : this.NombreClienteFatu.Text);
                    NuevaConexion.ComandoDb.Parameters.AddWithValue("@Direccion", string.IsNullOrEmpty(this.DirecionClienteFatu.Text) ? (object)DBNull.Value : this.DirecionClienteFatu.Text);
                    NuevaConexion.ComandoDb.Parameters.AddWithValue("@Calle", string.IsNullOrEmpty(this.CalleTex.Text) ? (object)DBNull.Value : this.CalleTex.Text);
                    NuevaConexion.ComandoDb.Parameters.AddWithValue("@NumeroCalle", string.IsNullOrEmpty(this.NumeroCalleTxt.Text) ? (object)DBNull.Value : this.NumeroCalleTxt.Text);
                    NuevaConexion.ComandoDb.Parameters.AddWithValue("@Dni", string.IsNullOrEmpty(this.DniTextBox.Text) ? (object)DBNull.Value : this.DniTextBox.Text);
                    NuevaConexion.ComandoDb.Parameters.AddWithValue("@Localidad", string.IsNullOrEmpty(this.LocalidadTxt.Text) ? (object)DBNull.Value : this.LocalidadTxt.Text);
                    NuevaConexion.ComandoDb.Parameters.AddWithValue("@Provincia", string.IsNullOrEmpty(this.ProvinciaTxt.Text) ? (object)DBNull.Value : this.ProvinciaTxt.Text);
                    NuevaConexion.ComandoDb.Parameters.AddWithValue("@CodigoPostal", string.IsNullOrEmpty(this.CodigoPostalTxt.Text) ? (object)DBNull.Value : this.CodigoPostalTxt.Text);
                    NuevaConexion.ComandoDb.Parameters.AddWithValue("@NonbreAlmacen", string.IsNullOrEmpty(this.AlmacenTxt.Text) ? (object)DBNull.Value : this.AlmacenTxt.Text);
                    NuevaConexion.ComandoDb.Parameters.AddWithValue("@Marca", string.IsNullOrEmpty(this.ProveedorTxt.Text) ? (object)DBNull.Value : this.ProveedorTxt.Text);
                    NuevaConexion.ComandoDb.Parameters.AddWithValue("@IvaImpuesto", string.IsNullOrEmpty(this.IvaFactuTxt.Value.ToString()) ? (object)DBNull.Value : Convert.ToInt32(this.IvaFactuTxt.Value.ToString()));
                    NuevaConexion.ComandoDb.Parameters.AddWithValue("@SubTotal", string.IsNullOrEmpty(this.subTotal.Text) ? (object)DBNull.Value : Convert.ToDouble(this.subTotal.Text.Replace("€", "")));
                    NuevaConexion.ComandoDb.Parameters.AddWithValue("@BaseIva", string.IsNullOrEmpty(this.baseIva.Text) ? (object)DBNull.Value : Convert.ToDouble(this.baseIva.Text.Replace("€", "")));
                    NuevaConexion.ComandoDb.Parameters.AddWithValue("@TotalFactura", string.IsNullOrEmpty(this.TotalFactura1.Text) ? (object)DBNull.Value : Convert.ToDouble(this.TotalFactura1.Text.Replace("€", "")));
                    NuevaConexion.ComandoDb.Parameters.AddWithValue("@TotalFactura2", string.IsNullOrEmpty(this.TotalFactura2.Text) ? (object)DBNull.Value : Convert.ToDouble(this.TotalFactura2.Text.Replace("€", "")));
                    NuevaConexion.ComandoDb.Parameters.AddWithValue("@Pais_Fact", string.IsNullOrEmpty(this.PaisFatuTxt.Text) ? (object)DBNull.Value : this.PaisFatuTxt.Text);
                    NuevaConexion.ComandoDb.Parameters.AddWithValue("@TipoNOTA", string.IsNullOrEmpty(this.TipoNota.Text) ? (object)DBNull.Value : this.TipoNota.Text);
                    NuevaConexion.ComandoDb.Parameters.AddWithValue("@Obra_factu", string.IsNullOrEmpty(this.ObraFactuTxt.Text) ? (object)DBNull.Value : this.ObraFactuTxt.Text);
                    NuevaConexion.ComandoDb.Parameters.AddWithValue("@EjercicioTipo", string.IsNullOrEmpty(EnlaceDtconfi.ToString()) ? (object)DBNull.Value : EnlaceDtconfi);
                    NuevaConexion.ComandoDb.Parameters.AddWithValue("@SerieTipo", string.IsNullOrEmpty(this.SerieFatu.Text) ? (object)DBNull.Value : this.SerieFatu.Text);
                    NuevaConexion.ComandoDb.Parameters.AddWithValue("@EmpresaEnlace", string.IsNullOrEmpty(this.Id_Empresa.Text) ? (object)DBNull.Value : Convert.ToInt32(this.Id_Empresa.Text));
                    NuevaConexion.ComandoDb.Parameters.AddWithValue("@FechaFactura", string.IsNullOrEmpty(this.FechaFactura.Text) ? (object)DBNull.Value : this.FechaFactura.Text);
                    if (this.cobradaFacturaCheckBox.Checked)
                    {
                        NuevaConexion.ComandoDb.Parameters.AddWithValue("@FechaCobro", string.IsNullOrEmpty(this.fechaCobroText.Text) ? (object)DBNull.Value : this.fechaCobroText.Text);
                        NuevaConexion.ComandoDb.Parameters.AddWithValue("@CobradaFactura", "Cobrado");///canbiar valor a cobrada
                    }
                    else
                    {
                        NuevaConexion.ComandoDb.Parameters.AddWithValue("@FechaCobro", DBNull.Value);
                        NuevaConexion.ComandoDb.Parameters.AddWithValue("@CobradaFactura", (object)DBNull.Value);///canbiar valor a cobrada
                    }
                    NuevaConexion.ComandoDb.Parameters.AddWithValue("@EnlaceDtconfi", string.IsNullOrEmpty(EnlaceDtconfi.ToString()) ? (object)DBNull.Value : EnlaceDtconfi);
                    NuevaConexion.ComandoDb.ExecuteNonQuery();
                    this.dtNuevaFacturaBindingSource.EndEdit();
                    if (this.dtNuevaFacturaBindingSource.Count > 0)
                    {

                        int FILAcelda = this.dtNuevaFacturaDataGridView.CurrentCell.RowIndex;
                        if (this.cobradaFacturaCheckBox.Checked == true)
                        {
                            this.dsFacturas.DtNuevaFactura.Rows[FILAcelda]["CobradaFactura"] = "Cobrado";
                        }
                        else
                        {
                            //this.dtNuevaFacturaDataGridView.Rows[FILAcelda].Cells[13].Value = "";
                            this.dsFacturas.DtNuevaFactura.Rows[FILAcelda]["FechaCobro"] = "";

                        }
                    }
                    if (NuevaConexion.CerrarConexionDB)
                    {

                    }

                    NuevaConexion.ComandoDb.Parameters.Clear();

                    if (this.panelBotones.Tag.ToString() == "Modificar")
                    {

                        if (this.dtNuevaFacturaDataGridView.RowCount >= 0)
                        {
                            if (ConexionDetalleEliminar.SiConexionDb)
                            {
                                ConexionDetalleEliminar.ComandoDb.Parameters.AddWithValue("@EnlaceFactu", Convert.ToInt32(this.EnlaceFactu.Text));
                                ConexionDetalleEliminar.ComandoDb.ExecuteNonQuery();
                                if (ConexionDetalleEliminar.CerrarConexionDB)
                                {
                                    ConexionDetalleEliminar.ComandoDb.Parameters.Clear();
                                }
                            }

                        }
                    }

                    if (this.dtDetallesFacturaDataGridView.RowCount >= 0)
                    {

                        if (NuevaConexionDetalle.SiConexionDb)
                        {

                            foreach (DataGridViewRow FilaGri in this.dtDetallesFacturaDataGridView.Rows)
                            {
                                if (this.dtDetallesFacturaDataGridView.AllowUserToAddRows == true)
                                {
                                    if (FilaGri.Index == this.dtDetallesFacturaDataGridView.RowCount - 1)
                                    {
                                        break;
                                    }
                                }

                                NuevaConexionDetalle.ComandoDb.Parameters.AddWithValue("@ReferenciaDetalle", string.IsNullOrEmpty(FilaGri.Cells[0].Value.ToString()) ? (object)DBNull.Value : FilaGri.Cells[0].Value.ToString());
                                NuevaConexionDetalle.ComandoDb.Parameters.AddWithValue("@CantidadDetalle", string.IsNullOrEmpty(FilaGri.Cells[2].Value.ToString()) ? (object)DBNull.Value : Convert.ToDouble(FilaGri.Cells[2].Value.ToString()));
                                NuevaConexionDetalle.ComandoDb.Parameters.AddWithValue("@DescripccionDetalle", string.IsNullOrEmpty(FilaGri.Cells[3].Value.ToString()) ? (object)DBNull.Value : FilaGri.Cells[3].Value.ToString());
                                NuevaConexionDetalle.ComandoDb.Parameters.AddWithValue("@PrecioDetalle", string.IsNullOrEmpty(FilaGri.Cells[4].Value.ToString()) ? (object)DBNull.Value : Convert.ToDouble(FilaGri.Cells[4].Value.ToString()));
                                NuevaConexionDetalle.ComandoDb.Parameters.AddWithValue("@DescuentoDetalle", string.IsNullOrEmpty(FilaGri.Cells[5].Value.ToString()) ? (object)DBNull.Value : Convert.ToDouble(FilaGri.Cells[5].Value.ToString()) * 100);
                                NuevaConexionDetalle.ComandoDb.Parameters.AddWithValue("@IvaDetalle", string.IsNullOrEmpty(FilaGri.Cells[6].Value.ToString()) ? (object)DBNull.Value : Convert.ToDouble(FilaGri.Cells[6].Value.ToString()));
                                NuevaConexionDetalle.ComandoDb.Parameters.AddWithValue("@ImporteDetalle", string.IsNullOrEmpty(FilaGri.Cells[7].Value.ToString()) ? (object)DBNull.Value : Convert.ToDouble(FilaGri.Cells[7].Value.ToString()));
                                NuevaConexionDetalle.ComandoDb.Parameters.AddWithValue("@EnlaceDetalle", string.IsNullOrEmpty(this.EnlaceFactu.Text) ? (object)DBNull.Value : Convert.ToInt32(this.EnlaceFactu.Text));
                                NuevaConexionDetalle.ComandoDb.ExecuteNonQuery();
                                NuevaConexionDetalle.ComandoDb.Parameters.Clear();
                            }
                            if (NuevaConexionDetalle.CerrarConexionDB)
                            {
                                NuevaConexionDetalle.ComandoDb.Parameters.Clear();
                            }

                        }
                    }

                    if (this.dtDetallesFacturaDataGridView2.RowCount >= 0 && ClasDatos.NombreFactura == "Nota2")
                    {
                        if (this.panelBotones.Tag.ToString() == "Modificar")
                        {

                            if (this.dtNuevaFacturaDataGridView.RowCount >= 0)
                            {
                                ClsConexionDb ConexionDetalleEliminar2 = new ClsConexionDb(ConsultaEliminar2);
                                if (ConexionDetalleEliminar2.SiConexionDb)
                                {
                                    ConexionDetalleEliminar2.ComandoDb.Parameters.AddWithValue("@EnlaceFactu", Convert.ToInt32(this.EnlaceFactu.Text));
                                    ConexionDetalleEliminar2.ComandoDb.ExecuteNonQuery();

                                    if (ConexionDetalleEliminar2.CerrarConexionDB)
                                    {
                                        ConexionDetalleEliminar2.ComandoDb.Parameters.Clear();
                                    }
                                }
                            }
                        }

                        if (NuevaConexionDetalle2.SiConexionDb)
                        {
                            foreach (DataGridViewRow FilaGri in this.dtDetallesFacturaDataGridView2.Rows)
                            {
                                if (this.dtDetallesFacturaDataGridView2.AllowUserToAddRows == true)
                                {
                                    if (FilaGri.Index == this.dtDetallesFacturaDataGridView2.RowCount - 1)
                                    {
                                        break;
                                    }
                                }

                                NuevaConexionDetalle2.ComandoDb.Parameters.AddWithValue("@ReferenciaDetalle", string.IsNullOrEmpty(FilaGri.Cells[0].Value.ToString()) ? (object)DBNull.Value : FilaGri.Cells[0].Value.ToString());
                                NuevaConexionDetalle2.ComandoDb.Parameters.AddWithValue("@CantidadDetalle", string.IsNullOrEmpty(FilaGri.Cells[2].Value.ToString()) ? (object)DBNull.Value : Convert.ToDouble(FilaGri.Cells[2].Value.ToString()));
                                NuevaConexionDetalle2.ComandoDb.Parameters.AddWithValue("@DescripccionDetalle", string.IsNullOrEmpty(FilaGri.Cells[3].Value.ToString()) ? (object)DBNull.Value : FilaGri.Cells[3].Value.ToString());
                                NuevaConexionDetalle2.ComandoDb.Parameters.AddWithValue("@PrecioDetalle", string.IsNullOrEmpty(FilaGri.Cells[4].Value.ToString()) ? (object)DBNull.Value : Convert.ToDouble(FilaGri.Cells[4].Value.ToString()));
                                NuevaConexionDetalle2.ComandoDb.Parameters.AddWithValue("@DescuentoDetalle", string.IsNullOrEmpty(FilaGri.Cells[5].Value.ToString()) ? (object)DBNull.Value : Convert.ToDouble(FilaGri.Cells[5].Value.ToString()) * 100);
                                NuevaConexionDetalle2.ComandoDb.Parameters.AddWithValue("@ImporteDetalle", string.IsNullOrEmpty(FilaGri.Cells[6].Value.ToString()) ? (object)DBNull.Value : Convert.ToDouble(FilaGri.Cells[6].Value.ToString()));
                                NuevaConexionDetalle2.ComandoDb.Parameters.AddWithValue("@EnlaceDetalle", string.IsNullOrEmpty(this.EnlaceFactu.Text) ? (object)DBNull.Value : Convert.ToInt32(this.EnlaceFactu.Text));
                                NuevaConexionDetalle2.ComandoDb.ExecuteNonQuery();
                                NuevaConexionDetalle2.ComandoDb.Parameters.Clear();

                            }

                            if (NuevaConexionDetalle2.CerrarConexionDB)
                            {
                                NuevaConexionDetalle2.ComandoDb.Parameters.Clear();
                            }


                        }
                    }
                    this.dtDetallesFacturaBindingSource.EndEdit();
                    this.dtDetallesFactura2BindingSource.EndEdit();
                    Validate();

                    MessageBox.Show("Guardado Correctamente", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RestaraurarOjetosFatu();

                }
                catch (Exception ex)
                {
                    if (NuevaConexion.CerrarConexionDB)
                    {
                        NuevaConexion.ComandoDb.Parameters.Clear();
                    }
                    if (NuevaConexionDetalle.CerrarConexionDB)
                    {
                        NuevaConexionDetalle.ComandoDb.Parameters.Clear();
                    }
                    if (NuevaConexionDetalle2.CerrarConexionDB)
                    {
                        NuevaConexionDetalle2.ComandoDb.Parameters.Clear();
                    }
                    if (ConexionDetalleEliminar.CerrarConexionDB)
                    {
                        ConexionDetalleEliminar.ComandoDb.Parameters.Clear();
                    }
                    MessageBox.Show(ex.Message, "ERROR AL GUARDAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }



        }

        public void GuardarFactuSql()
        {
            Random r = new Random();
            int Id_valor = r.Next(3, 99999);
            string VALIDAR_DATOS = "";
            string VALIDAR_Dtfactura = "  Se Guardo Correctamente";
            string VALIDAR_Dtdetalle = "  Se Guardo Correctamente";
            string VALIDAR_Dtdetalle2 = "";
            string Consulta = "";
            int EnlaceDtconfi = 0;
            Int32 Id_Ejercicio = 0;
            int Id = this.ejerciciosDeAñoComboBox.SelectedIndex + 1;
            if (Id > this.dtConfiBindingSource.Count - 1)
            {
                MessageBox.Show("Falta Id De Ejercicios", "ERROR APP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!String.IsNullOrEmpty(FormMenuPrincipal.menu2principal.dsCONFIGURACCION.Tables["DtConfi"].Rows[Id]["EnlaceDtconfi"].ToString()))
            {
                EnlaceDtconfi = Convert.ToInt32(FormMenuPrincipal.menu2principal.dsCONFIGURACCION.Tables["DtConfi"].Rows[Id]["EnlaceDtconfi"].ToString());
            }
            else
            {
                this.panelBotones.Enabled = false;
                MessageBox.Show("No Se Puede Continuar", "ERROR FALTAN DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!String.IsNullOrEmpty(FormMenuPrincipal.menu2principal.dsCONFIGURACCION.Tables["DtConfi"].Rows[Id]["EjercicioTipo"].ToString()))
            {
                Id_Ejercicio = Convert.ToInt32(FormMenuPrincipal.menu2principal.dsCONFIGURACCION.Tables["DtConfi"].Rows[Id]["EjercicioTipo"].ToString());
            }
            string ConsultaEliminar = "DELETE FROM [DtDetalles_" + ClasDatos.NombreFactura + "] WHERE [EnlaceDetalle]= @EnlaceFactu";
            string ConsultaEliminar2 = "DELETE FROM [DtDetalles2_" + ClasDatos.NombreFactura + "] WHERE [EnlaceDetalle]=@EnlaceFactu";
            string ConsultaDetalle = "INSERT INTO [DtDetalles_" + ClasDatos.NombreFactura + "] ([ReferenciaDetalle],[CantidadDetalle],[DescripccionDetalle]," +
              " [PrecioDetalle],[DescuentoDetalle],[IvaDetalle] ,[ImporteDetalle],[EnlaceDetalle])" +
              " VALUES( @ReferenciaDetalle, @CantidadDetalle, @DescripccionDetalle, @PrecioDetalle, " +
              " @DescuentoDetalle, @IvaDetalle, @ImporteDetalle, @EnlaceDetalle) ";

            string ConsultaDetalle2 = "INSERT INTO [DtDetalles2_" + ClasDatos.NombreFactura + "] ([ReferenciaDetalle],[CantidadDetalle],[DescripccionDetalle]" +
                 " ,[PrecioDetalle],[DescuentoDetalle],[ImporteDetalle],[EnlaceDetalle])" +
                 " VALUES( @ReferenciaDetalle, @CantidadDetalle, @DescripccionDetalle, @PrecioDetalle, " +
                 " @DescuentoDetalle, @ImporteDetalle, @EnlaceDetalle) ";
            if (this.panelBotones.Tag.ToString() == "Nuevo")
            {
                Consulta = "INSERT INTO [Dt" + ClasDatos.NombreFactura + "]([EnlaceFactura],[NumeroFactura],[Apodo] ,[Nombre],[Direccion],[Calle]" +
                ",[NumeroCalle] ,[Dni],[Localidad],[Provincia],[CodigoPostal],[NonbreAlmacen],[Marca]" +
                ",[IvaImpuesto],[SubTotal],[BaseIva] ,[TotalFactura],[TotalFactura2],[Pais_Fact]" +
                " ,[TipoNOTA],[Obra_factu],[EjercicioTipo],[SerieTipo],[EmpresaEnlace],[FechaFactura],[FechaCobro],[CobradaFactura],[EnlaceDtconfi])" +
               " VALUES(@EnlaceFactura, @NumeroFactura, @Apodo, @Nombre, @Direccion, @Calle, " +
               "@NumeroCalle, @Dni, @Localidad, @Provincia, @CodigoPostal, @NonbreAlmacen,@Marca," +
                " @IvaImpuesto, @SubTotal, @BaseIva, @TotalFactura, @TotalFactura2," +
                " @Pais_Fact, @TipoNOTA, @Obra_factu, @EjercicioTipo,@SerieTipo,@EmpresaEnlace, @FechaFactura, @FechaCobro, @CobradaFactura, @EnlaceDtconfi)";

            }
            else
            {
                Consulta = "UPDATE [Dt" + ClasDatos.NombreFactura + "] SET [EnlaceFactura]= @EnlaceFactura, [NumeroFactura] = @NumeroFactura,[Apodo] = @Apodo,[Nombre] = @Nombre," +
               " [Direccion] = @Direccion, [Calle] = @Calle, [NumeroCalle] = @NumeroCalle, [Dni] = @Dni," +
               " [Localidad] = @Localidad, [Provincia] = @Provincia,[CodigoPostal] = @CodigoPostal, " +
               " [NonbreAlmacen] = @NonbreAlmacen, [Marca] = @Marca,[IvaImpuesto] = @IvaImpuesto, [SubTotal] = @SubTotal," +
               " [BaseIva] = @BaseIva, [TotalFactura] = @TotalFactura, [TotalFactura2] = @TotalFactura2, " +
               " [Pais_Fact] = @Pais_Fact,[TipoNOTA] = @TipoNOTA, [Obra_factu] = @Obra_factu, [EjercicioTipo] = @EjercicioTipo," +
               "[SerieTipo]= @SerieTipo,[EmpresaEnlace]=@EmpresaEnlace, [FechaFactura] = @FechaFactura ,[FechaCobro] = @FechaCobro , [CobradaFactura] = @CobradaFactura, [EnlaceDtconfi] = @EnlaceDtconfi WHERE [EnlaceFactura] = @EnlaceFactura";

            }

            ClsConexionSql NuevaConexion = new ClsConexionSql(Consulta);


            if (NuevaConexion.SiConexionSql)
            {
                NuevaConexion.ComandoSql.Parameters.AddWithValue("@EnlaceFactura", string.IsNullOrEmpty(this.EnlaceFactu.Text) ? (object)DBNull.Value : Convert.ToInt32(this.EnlaceFactu.Text));
                NuevaConexion.ComandoSql.Parameters.AddWithValue("@NumeroFactura", string.IsNullOrEmpty(this.NumeroFactura.Text) ? (object)DBNull.Value : Convert.ToInt32(this.NumeroFactura.Text));
                NuevaConexion.ComandoSql.Parameters.AddWithValue("@Apodo", string.IsNullOrEmpty(this.RazonSocialFatu.Text) ? (object)DBNull.Value : this.RazonSocialFatu.Text);
                NuevaConexion.ComandoSql.Parameters.AddWithValue("@Nombre", string.IsNullOrEmpty(this.NombreClienteFatu.Text) ? (object)DBNull.Value : this.NombreClienteFatu.Text);
                NuevaConexion.ComandoSql.Parameters.AddWithValue("@Direccion", string.IsNullOrEmpty(this.DirecionClienteFatu.Text) ? (object)DBNull.Value : this.DirecionClienteFatu.Text);
                NuevaConexion.ComandoSql.Parameters.AddWithValue("@Calle", string.IsNullOrEmpty(this.CalleTex.Text) ? (object)DBNull.Value : this.CalleTex.Text);
                NuevaConexion.ComandoSql.Parameters.AddWithValue("@NumeroCalle", string.IsNullOrEmpty(this.NumeroCalleTxt.Text) ? (object)DBNull.Value : this.NumeroCalleTxt.Text);
                NuevaConexion.ComandoSql.Parameters.AddWithValue("@Dni", string.IsNullOrEmpty(this.DniTextBox.Text) ? (object)DBNull.Value : this.DniTextBox.Text);
                NuevaConexion.ComandoSql.Parameters.AddWithValue("@Localidad", string.IsNullOrEmpty(this.LocalidadTxt.Text) ? (object)DBNull.Value : this.LocalidadTxt.Text);
                NuevaConexion.ComandoSql.Parameters.AddWithValue("@Provincia", string.IsNullOrEmpty(this.ProvinciaTxt.Text) ? (object)DBNull.Value : this.ProvinciaTxt.Text);
                NuevaConexion.ComandoSql.Parameters.AddWithValue("@CodigoPostal", string.IsNullOrEmpty(this.CodigoPostalTxt.Text) ? (object)DBNull.Value : this.CodigoPostalTxt.Text);
                NuevaConexion.ComandoSql.Parameters.AddWithValue("@NonbreAlmacen", string.IsNullOrEmpty(this.AlmacenTxt.Text) ? (object)DBNull.Value : this.AlmacenTxt.Text);
                NuevaConexion.ComandoSql.Parameters.AddWithValue("@Marca", string.IsNullOrEmpty(this.ProveedorTxt.Text) ? (object)DBNull.Value : this.ProveedorTxt.Text);
                NuevaConexion.ComandoSql.Parameters.AddWithValue("@IvaImpuesto", string.IsNullOrEmpty(this.IvaFactuTxt.Value.ToString()) ? (object)DBNull.Value : Convert.ToInt32(this.IvaFactuTxt.Value.ToString()));
                NuevaConexion.ComandoSql.Parameters.AddWithValue("@SubTotal", string.IsNullOrEmpty(this.subTotal.Text) ? (object)DBNull.Value : Convert.ToDouble(this.subTotal.Text.Replace("€", "")));
                NuevaConexion.ComandoSql.Parameters.AddWithValue("@BaseIva", string.IsNullOrEmpty(this.baseIva.Text) ? (object)DBNull.Value : Convert.ToDouble(this.baseIva.Text.Replace("€", "")));
                NuevaConexion.ComandoSql.Parameters.AddWithValue("@TotalFactura", string.IsNullOrEmpty(this.TotalFactura1.Text) ? (object)DBNull.Value : Convert.ToDouble(this.TotalFactura1.Text.Replace("€", "")));
                NuevaConexion.ComandoSql.Parameters.AddWithValue("@TotalFactura2", string.IsNullOrEmpty(this.TotalFactura2.Text) ? (object)DBNull.Value : Convert.ToDouble(this.TotalFactura2.Text.Replace("€", "")));
                NuevaConexion.ComandoSql.Parameters.AddWithValue("@Pais_Fact", string.IsNullOrEmpty(this.PaisFatuTxt.Text) ? (object)DBNull.Value : this.PaisFatuTxt.Text);
                NuevaConexion.ComandoSql.Parameters.AddWithValue("@TipoNOTA", string.IsNullOrEmpty(this.TipoNota.Text) ? (object)DBNull.Value : this.TipoNota.Text);
                NuevaConexion.ComandoSql.Parameters.AddWithValue("@Obra_factu", string.IsNullOrEmpty(this.ObraFactuTxt.Text) ? (object)DBNull.Value : this.ObraFactuTxt.Text);
                NuevaConexion.ComandoSql.Parameters.AddWithValue("@EjercicioTipo", string.IsNullOrEmpty(Id_Ejercicio.ToString()) ? (object)DBNull.Value : Id_Ejercicio);
                NuevaConexion.ComandoSql.Parameters.AddWithValue("@SerieTipo", string.IsNullOrEmpty(this.SerieFatu.Text) ? (object)DBNull.Value : this.SerieFatu.Text);
                NuevaConexion.ComandoSql.Parameters.AddWithValue("@EmpresaEnlace", string.IsNullOrEmpty(this.Id_Empresa.Text) ? (object)DBNull.Value : Convert.ToInt32(this.Id_Empresa.Text));
                NuevaConexion.ComandoSql.Parameters.AddWithValue("@FechaFactura", string.IsNullOrEmpty(this.FechaFactura.Text) ? (object)DBNull.Value : this.FechaFactura.Text);


                if (this.cobradaFacturaCheckBox.Checked)
                {
                    NuevaConexion.ComandoSql.Parameters.AddWithValue("@FechaCobro", string.IsNullOrEmpty(this.fechaCobroText.Text) ? (object)DBNull.Value : this.fechaCobroText.Text);
                    NuevaConexion.ComandoSql.Parameters.AddWithValue("@CobradaFactura", "Cobrado");///canbiar valor a cobrada
                }
                else
                {
                    NuevaConexion.ComandoSql.Parameters.AddWithValue("@FechaCobro", DBNull.Value);
                    NuevaConexion.ComandoSql.Parameters.AddWithValue("@CobradaFactura", (object)DBNull.Value);///canbiar valor a cobrada
                }
                NuevaConexion.ComandoSql.Parameters.AddWithValue("@EnlaceDtconfi", string.IsNullOrEmpty(EnlaceDtconfi.ToString()) ? (object)DBNull.Value : EnlaceDtconfi);
                try
                {
                    NuevaConexion.ComandoSql.ExecuteNonQuery();
                    this.dtNuevaFacturaBindingSource.EndEdit();
                    if (this.dtNuevaFacturaBindingSource.Count > 0)
                    {

                        int FILAcelda = this.dtNuevaFacturaDataGridView.CurrentCell.RowIndex;
                        if (this.cobradaFacturaCheckBox.Checked == true)
                        {
                            this.dsFacturas.DtNuevaFactura.Rows[FILAcelda]["CobradaFactura"] = "Cobrado";
                        }
                        else
                        {
                            //this.dtNuevaFacturaDataGridView.Rows[FILAcelda].Cells[13].Value = "";
                            this.dsFacturas.DtNuevaFactura.Rows[FILAcelda]["FechaCobro"] = "";

                        }
                    }
                }
                catch (Exception ex)
                {
                    VALIDAR_DATOS = "ERROR";
                    VALIDAR_Dtfactura = " Tabla" + ClasDatos.NombreFactura + "  no Se guardo ((ERROR))";
                    MessageBox.Show(ex.Message, "ERROR AL GUARDAR DATOS TABLA PRINCIPAL");
                }
                finally
                {
                    if (NuevaConexion.CerrarConexionSql)
                    {

                    }
                }
                NuevaConexion.ComandoSql.Parameters.Clear();

                if (this.panelBotones.Tag.ToString() == "Modificar")
                {

                    if (this.dtNuevaFacturaDataGridView.RowCount > 0)
                    {
                        ClsConexionSql ConexionDetalleEliminar = new ClsConexionSql(ConsultaEliminar);
                        if (ConexionDetalleEliminar.SiConexionSql)
                        {
                            try
                            {
                                ConexionDetalleEliminar.ComandoSql.Parameters.AddWithValue("@EnlaceFactu", Convert.ToInt32(this.EnlaceFactu.Text));
                                ConexionDetalleEliminar.ComandoSql.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message, "ERROR AL GUARDAR ELIMINANDO DATOS1");
                            }
                            finally
                            {
                                if (ConexionDetalleEliminar.CerrarConexionSql)
                                {
                                    ConexionDetalleEliminar.ComandoSql.Parameters.Clear();
                                }
                            }
                        }

                    }
                }

                if (this.dtDetallesFacturaDataGridView.RowCount >= 0)
                {
                    ClsConexionSql NuevaConexionDetalle = new ClsConexionSql(ConsultaDetalle);
                    if (NuevaConexionDetalle.SiConexionSql)
                    {
                        try
                        {
                            int i = 0;
                            foreach (DataGridViewRow FilaGri in this.dtDetallesFacturaDataGridView.Rows)
                            {
                                if (i == this.dtDetallesFacturaDataGridView.RowCount - 1)
                                {
                                    break;
                                }
                                NuevaConexionDetalle.ComandoSql.Parameters.AddWithValue("@ReferenciaDetalle", string.IsNullOrEmpty(FilaGri.Cells[0].Value.ToString()) ? (object)DBNull.Value : FilaGri.Cells[0].Value.ToString());
                                NuevaConexionDetalle.ComandoSql.Parameters.AddWithValue("@CantidadDetalle", string.IsNullOrEmpty(FilaGri.Cells[2].Value.ToString()) ? (object)DBNull.Value : Convert.ToDouble(FilaGri.Cells[2].Value.ToString()));
                                NuevaConexionDetalle.ComandoSql.Parameters.AddWithValue("@DescripccionDetalle", string.IsNullOrEmpty(FilaGri.Cells[3].Value.ToString()) ? (object)DBNull.Value : FilaGri.Cells[3].Value.ToString());
                                NuevaConexionDetalle.ComandoSql.Parameters.AddWithValue("@PrecioDetalle", string.IsNullOrEmpty(FilaGri.Cells[4].Value.ToString()) ? (object)DBNull.Value : Convert.ToDouble(FilaGri.Cells[4].Value.ToString()));
                                NuevaConexionDetalle.ComandoSql.Parameters.AddWithValue("@DescuentoDetalle", string.IsNullOrEmpty(FilaGri.Cells[5].Value.ToString()) ? (object)DBNull.Value : Convert.ToDouble(FilaGri.Cells[5].Value.ToString()));
                                NuevaConexionDetalle.ComandoSql.Parameters.AddWithValue("@IvaDetalle", string.IsNullOrEmpty(FilaGri.Cells[6].Value.ToString()) ? (object)DBNull.Value : Convert.ToDouble(FilaGri.Cells[6].Value.ToString()));
                                NuevaConexionDetalle.ComandoSql.Parameters.AddWithValue("@ImporteDetalle", string.IsNullOrEmpty(FilaGri.Cells[7].Value.ToString()) ? (object)DBNull.Value : Convert.ToDouble(FilaGri.Cells[7].Value.ToString()));
                                NuevaConexionDetalle.ComandoSql.Parameters.AddWithValue("@EnlaceDetalle", string.IsNullOrEmpty(this.EnlaceFactu.Text) ? (object)DBNull.Value : Convert.ToInt32(this.EnlaceFactu.Text));
                                NuevaConexionDetalle.ComandoSql.ExecuteNonQuery();
                                NuevaConexionDetalle.ComandoSql.Parameters.Clear();
                                i++;
                            }

                        }
                        catch (Exception ex)
                        {
                            VALIDAR_DATOS = "ERROR";
                            VALIDAR_Dtdetalle = " Tabla Dtdetalle no Se guardo ((ERROR))";
                            MessageBox.Show(ex.Message, "ERROR AL GUARDAR dtdetalle1");
                        }
                        finally
                        {
                            if (NuevaConexionDetalle.CerrarConexionSql)
                            {

                            }
                        }
                    }
                }

                if (this.dtDetallesFacturaDataGridView2.RowCount >= 0 && ClasDatos.Datos1Datos2 == "Nota2")
                {
                    if (this.panelBotones.Tag.ToString() == "Modificar")
                    {

                        if (this.dtNuevaFacturaDataGridView.RowCount >= 0)
                        {
                            ClsConexionSql ConexionDetalleEliminar2 = new ClsConexionSql(ConsultaEliminar2);
                            if (ConexionDetalleEliminar2.SiConexionSql)
                            {
                                try
                                {
                                    ConexionDetalleEliminar2.ComandoSql.Parameters.AddWithValue("@EnlaceFactu", Convert.ToInt32(this.EnlaceFactu.Text));
                                    ConexionDetalleEliminar2.ComandoSql.ExecuteNonQuery();
                                }
                                catch (Exception ex)
                                {

                                    MessageBox.Show(ex.Message, "ERROR AL GUARDAR ELIMINANDO DATOS2");
                                }
                                finally
                                {
                                    if (ConexionDetalleEliminar2.CerrarConexionSql)
                                    {
                                        ConexionDetalleEliminar2.ComandoSql.Parameters.Clear();
                                    }
                                }
                            }
                        }
                    }
                    ClsConexionSql NuevaConexionDetalle = new ClsConexionSql(ConsultaDetalle2);
                    if (NuevaConexionDetalle.SiConexionSql)
                    {
                        try
                        {
                            int i = 0;
                            foreach (DataGridViewRow FilaGri in this.dtDetallesFacturaDataGridView2.Rows)
                            {
                                if (i == this.dtDetallesFacturaDataGridView2.RowCount - 1)
                                {
                                    break;
                                }
                                NuevaConexionDetalle.ComandoSql.Parameters.AddWithValue("@ReferenciaDetalle", string.IsNullOrEmpty(FilaGri.Cells[0].Value.ToString()) ? (object)DBNull.Value : FilaGri.Cells[0].Value.ToString());
                                NuevaConexionDetalle.ComandoSql.Parameters.AddWithValue("@CantidadDetalle", string.IsNullOrEmpty(FilaGri.Cells[2].Value.ToString()) ? (object)DBNull.Value : Convert.ToDouble(FilaGri.Cells[2].Value.ToString()));
                                NuevaConexionDetalle.ComandoSql.Parameters.AddWithValue("@DescripccionDetalle", string.IsNullOrEmpty(FilaGri.Cells[3].Value.ToString()) ? (object)DBNull.Value : FilaGri.Cells[3].Value.ToString());
                                NuevaConexionDetalle.ComandoSql.Parameters.AddWithValue("@DescuentoDetalle", string.IsNullOrEmpty(FilaGri.Cells[5].Value.ToString()) ? (object)DBNull.Value : Convert.ToDouble(FilaGri.Cells[5].Value.ToString()));
                                NuevaConexionDetalle.ComandoSql.Parameters.AddWithValue("@PrecioDetalle", string.IsNullOrEmpty(FilaGri.Cells[4].Value.ToString()) ? (object)DBNull.Value : Convert.ToDouble(FilaGri.Cells[4].Value.ToString()));
                                NuevaConexionDetalle.ComandoSql.Parameters.AddWithValue("@ImporteDetalle", string.IsNullOrEmpty(FilaGri.Cells[6].Value.ToString()) ? (object)DBNull.Value : Convert.ToDouble(FilaGri.Cells[6].Value.ToString()));
                                NuevaConexionDetalle.ComandoSql.Parameters.AddWithValue("@EnlaceDetalle", string.IsNullOrEmpty(this.EnlaceFactu.Text) ? (object)DBNull.Value : Convert.ToInt32(this.EnlaceFactu.Text));
                                NuevaConexionDetalle.ComandoSql.ExecuteNonQuery();
                                NuevaConexionDetalle.ComandoSql.Parameters.Clear();
                                i++;
                                VALIDAR_Dtdetalle2 = "  Se Guardo Correctamente";
                            }


                        }
                        catch (Exception ex)
                        {
                            VALIDAR_DATOS = "ERROR";
                            VALIDAR_Dtdetalle2 = " Tabla Dtdetalle2 no Se guardo ((ERROR))";
                            MessageBox.Show(ex.Message, "ERROR AL GUARDAR dtdetalle2");
                        }
                        finally
                        {
                            if (NuevaConexionDetalle.CerrarConexionSql)
                            {

                            }
                        }

                    }
                }
                if (ClasDatos.NombreFactura != "Presupuesto")
                {
                    if (this.panelBotones.Tag.ToString() == "Modificar")
                    {
                        /// GuardarRestaurarStockDb();
                    }
                    // GuardarStockSql(this.dtDetallesFacturaDataGridView);
                }
            }

            if (VALIDAR_DATOS == "ERROR")
            {
                MessageBox.Show(VALIDAR_Dtfactura + "\n" + VALIDAR_Dtdetalle + "\n" + VALIDAR_Dtdetalle2, "GUARDAR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // RestaraurarOjetosFatu();
                this.BtnCancelarfactura.Enabled = true;
            }
            else
            {
                this.dtDetallesFacturaBindingSource.EndEdit();
                this.dtDetallesFactura2BindingSource.EndEdit();
                Validate();
                int FILAcelda = this.dtNuevaFacturaBindingSource.Count - 1;
                if (this.cobradaFacturaCheckBox.Checked == true)
                {
                    this.dsFacturas.DtNuevaFactura.Rows[FILAcelda]["CobradaFactura"] = "Cobrado";
                }
                else
                {
                    //this.dtNuevaFacturaDataGridView.Rows[FILAcelda].Cells[13].Value = "";
                    this.dsFacturas.DtNuevaFactura.Rows[FILAcelda]["FechaCobro"] = "";

                }
                MessageBox.Show("Guardado Correctamente", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RestaraurarOjetosFatu();
            }
        }


        private void BtnNuevoFactura_Click(object sender, EventArgs e)
        {
            if (this.TipoTarifaFactu.Items.Count > 8)
            {

                MessageBox.Show("Debe Verificar Listado de Clientes", "FALTAN DATOS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); ;
            }
            int VALORid = 1;
           // int VALOR_MAS = 1;
            this.panelBotones.Tag = "Nuevo";
            this.TabControlFactura.SelectedIndex = 0;
            if (this.dtConfiguracionPrincipalBindingSource.Count <= 0)
            {
                MessageBox.Show("Debe al Menos Crear Una Empresa", "EMPRESA");
                return;
            }
            if (this.dtConfiguracionPrincipalDtConfiBindingSource.Count <= 0)
            {
                MessageBox.Show("Debe Crear Ejercicio De Esta Empresa", "Falta Ejercicio");
                return;
            }
            if (this.SerieText.Text == string.Empty)
            {
                MessageBox.Show("No Existe Ninguna Serie", "NO EXISTE SERIE");
                return;
            }
            try
            {
                this.dtNuevaFacturaDataGridView.Sort(this.dtNuevaFacturaDataGridView.Columns[0], ListSortDirection.Ascending);
                int numeroFILA = this.dtNuevaFacturaDataGridView.Rows.Count;
                this.dtNuevaFacturaBindingSource.AddNew();
                if (this.dtNuevaFacturaBindingSource.Count > 0)
                {
                    if (this.dtNuevaFacturaBindingSource.Count <= 1)
                    {
                        this.dtNuevaFacturaDataGridView.Rows[0].Cells[0].Value = "1";
                        this.NumeroFactura.Text = "1";
                    }
                    if (numeroFILA > 0)
                    {
                        if (this.dtNuevaFacturaDataGridView.Rows[numeroFILA - 1].Cells[0].Value.ToString() == string.Empty)
                        {
                            Random rR = new Random();
                            VALORid = rR.Next(5000, 100000000);
                            this.dtNuevaFacturaDataGridView.Rows[numeroFILA].Cells[0].Value = (VALORid);
                            this.NumeroFactura.Text = VALORid.ToString();
                        }
                        else
                        {
                            VALORid = Convert.ToInt32(this.dtNuevaFacturaDataGridView.Rows[numeroFILA - 1].Cells[0].Value) + 1;
                            this.dtNuevaFacturaDataGridView.Rows[numeroFILA].Cells[0].Value = (VALORid);
                            this.NumeroFactura.Text = VALORid.ToString();

                        }
                    }


                    ModificarOjetosFatu();
                    BORRARerrores();
                    Int32 Id_Enlace = 0;

                    // this.numeroFacturaTextBox.Enabled = false;
                    if (this.dtNuevaFacturaBindingSource.Count > 1)
                    {
                        string consulta = "Select max(EnlaceFactura) from [Dt" + ClasDatos.NombreFactura + "]";
                        if (ClsConexionSql.SibaseDatosSql)
                        {
                            ClsConexionSql NuevaConexion = new ClsConexionSql(consulta);
                            if (NuevaConexion.SiConexionSql)
                            {
                                SqlDataReader reader = NuevaConexion.ComandoSql.ExecuteReader();
                                if (reader.Read())
                                {
                                    if (!string.IsNullOrEmpty((reader[0]).ToString()))
                                    {

                                        Id_Enlace = Convert.ToInt32(reader[0].ToString());
                                        this.EnlaceFactu.Text = Convert.ToString(Id_Enlace + 1);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Falta Id Conexion", "ERROR FACTU 2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        // return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Falta Id Conexion", "ERROR FACTU", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    // return;
                                }
                            }

                        }
                        else
                        {
                            ClsConexionDb NuevaConexion = new ClsConexionDb(consulta);
                            if (NuevaConexion.SiConexionDb)
                            {
                                OleDbDataReader reader = NuevaConexion.ComandoDb.ExecuteReader();

                                if (reader.Read())
                                {
                                    if (!string.IsNullOrEmpty((reader[0]).ToString()))
                                    {
                                        Id_Enlace = Convert.ToInt32(reader[0].ToString());
                                        this.EnlaceFactu.Text = Convert.ToString(Id_Enlace + 1);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Falta Id Conexion", "ERROR 2 FACTU", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        //return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Falta Id Conexion", "ERROR FACTU", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    // return;
                                }
                            }

                        }
                        if (!ClasSi_Existe_Fatu.Buscar_Fatu_Sql(this.EnlaceFactu.Text, ClasDatos.NombreFactura))
                        {
                            // this.EnlaceFactu.Text = "";
                            //  goto Salto_Atras;
                            // return;
                        }
                    }
                    else
                    {
                        this.EnlaceFactu.Text = "1";

                    }

                    this.FechaFactura.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
                    this.dtNuevaFacturaDataGridView.Rows[this.dtNuevaFacturaDataGridView.Rows.Count - 1].Selected = true;

                    if (FormMenuPrincipal.menu2principal.dsMultidatos.DtInicioMulti.Count > 0)
                    {
                        if (FormMenuPrincipal.menu2principal.dsMultidatos.Tables["DtInicioMulti"].Rows[0]["SerieProvinciaInicio"].ToString() != string.Empty)
                        {
                            this.ProvinciaTxt.Text = FormMenuPrincipal.menu2principal.dsMultidatos.Tables["DtInicioMulti"].Rows[0]["SerieProvinciaInicio"].ToString();
                        }
                        if (FormMenuPrincipal.menu2principal.dsMultidatos.Tables["DtInicioMulti"].Rows[0]["SeriePaisInicio"].ToString() != string.Empty)
                        {
                            this.PaisFatuTxt.Text = FormMenuPrincipal.menu2principal.dsMultidatos.Tables["DtInicioMulti"].Rows[0]["SeriePaisInicio"].ToString();
                        }
                    }
                    if (this.AlmacenTxt.Items.Count > 0)
                    {
                        this.AlmacenTxt.SelectedIndex = 0;
                    }
                    if (this.ObraFactuTxt.Items.Count > 0)
                    {
                        this.ObraFactuTxt.SelectedIndex = 0;
                    }
                    this.SerieFatu.Text = this.SerieText.Text;
                }
                this.RazonSocialFatu.Focus();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }


        private void BtnGuardarFactura_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.Id_Empresa.Text) | string.IsNullOrEmpty(this.EmpresaPrincipal.Text))
            {
                MessageBox.Show("Falta  Empresa", "EMPRESA");
                return;
            }
            if (string.IsNullOrEmpty(this.ejerciciosDeAñoComboBox.Text))
            {
                MessageBox.Show("Debe Crear Ejercicio De Esta Empresa", "Falta Ejercicio");
                return;
            }
            if (string.IsNullOrEmpty(this.SerieText.Text))
            {
                MessageBox.Show("No Existe Ninguna Serie", "NO EXISTE SERIE");
                return;
            }
            if (string.IsNullOrEmpty(this.EnlaceFactu.Text))
            {
                MessageBox.Show("Datos Erradicos ", "FALTA ENLACE FACTURA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.TabControlFactura.SelectedIndex = 0;
            BORRARerrores();
            //email_bien_escrito();
            if (VALIDARcampos())
            {
                if (this.dtDetallesFacturaBindingSource.Count < 1)
                {
                    this.TabControlFactura.SelectedIndex = 1;
                    return;
                }
                if (ClasDatos.NombreFactura == "Nota 2")
                {
                    if (this.dtDetallesFactura2BindingSource.Count < 1)
                    {
                        this.TabControlFactura.SelectedIndex = 2;
                        return;
                    }
                }
                if (MessageBox.Show(" ¿Aceptar Guardar ? ", " GUARDAR ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    if (ClsConexionSql.SibaseDatosSql)
                    {
                        GuardarFactuSql();



                    }
                    else
                    {

                        if (File.Exists(ClasDatos.RutaBaseDatosDb))
                        {
                            GuardarFactuDB();

                            if (ClasDatos.NombreFactura != "Presupuesto")
                            {
                                if (this.panelBotones.Tag.ToString() == "Modificar")
                                {
                                    GuardarRestaurarStockDb();
                                }
                                GuardarStockDb(this.dtDetallesFacturaDataGridView);
                            }

                        }
                        else
                        {
                            MessageBox.Show(ClasDatos.RutaBaseDatosDb, "ARCHIVO NO SE ENCUENTRA", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                        // RestaraurarOjetosFatu();

                    }

                }

            }
        }
        private void RestaraurarOjetosFatu()
        {
            //this.numeroFacturaTextBox.Enabled = false;
            this.BtnGuardarFactura.Enabled = false;
            this.BtnCancelarfactura.Enabled = false;
            this.panelBotones.Enabled = true;
            this.PanelArriba.Enabled = true;
            this.BtnBuscarClientesFact.Enabled = false;
            this.BtnBuscarPais.Enabled = false;
            this.BtnBuscarProvi.Enabled = false;
            this.dtDetallesFacturaDataGridView.ReadOnly = true;
            this.dtDetallesFacturaDataGridView2.ReadOnly = true;
            this.dtNuevaFacturaDataGridView.Enabled = true;
            this.dtNuevaFacturaDataGridView.UseWaitCursor = false;
            this.cobradaFacturaCheckBox.Enabled = false;
            this.FechaFactura.Enabled = false;
            this.AlmacenTxt.Enabled = false;
            this.Btn_Iva.Visible = false;
            foreach (Control ctrl in this.tabPage1Factura.Controls)
            {
                if (ctrl is TextBox)
                {
                    ctrl.Enabled = false;
                    ctrl.ForeColor = Color.Black;
                }
                if (ctrl is ComboBox)
                {
                    ctrl.Enabled = false;
                    ctrl.ForeColor = Color.Black;
                }
            }
            foreach (Control ctrl in this.tabPage2Factura.Controls)
            {
                if (ctrl is TextBox)
                {
                    ctrl.Enabled = false;

                }
            }
        }
        private void ModificarOjetosFatu()
        {
            this.dtNuevaFacturaDataGridView.Refresh();
            this.dtDetallesFacturaDataGridView.Refresh();
            this.dtDetallesFacturaDataGridView2.Refresh();
            this.BtnGuardarFactura.Enabled = true;
            this.BtnCancelarfactura.Enabled = true;
            this.panelBotones.Enabled = false;
            this.PanelArriba.Enabled = false;
            this.BtnBuscarClientesFact.Enabled = true;
            this.dtNuevaFacturaDataGridView.Enabled = false;
            this.dtDetallesFacturaDataGridView.ReadOnly = false;
            this.dtDetallesFacturaDataGridView2.ReadOnly = false;
            this.dtNuevaFacturaDataGridView.UseWaitCursor = true;
            this.cobradaFacturaCheckBox.Enabled = true;
            this.FechaFactura.Enabled = true;
            this.AlmacenTxt.Enabled = true;
            this.BtnBuscarPais.Enabled = true;
            this.BtnBuscarProvi.Enabled = true;
            this.Btn_Iva.Visible = true;
            foreach (Control ctrl in this.tabPage1Factura.Controls)
            {
                if (ctrl is TextBox)
                {
                    ctrl.Enabled = true;
                    ctrl.ForeColor = Color.FromArgb(153, 40, 7);

                }
                if (ctrl is ComboBox)
                {
                    ctrl.Enabled = true;
                    ctrl.ForeColor = Color.FromArgb(153, 40, 7);
                }
            }
            foreach (Control ctrl in this.tabPage2Factura.Controls)
            {
                if (ctrl is TextBox)
                {
                    ctrl.Enabled = true;

                }
            }
            this.dtNuevaFacturaDataGridView.Focus();
        }
        private void BtnCancelarfactura_Click(object sender, EventArgs e)
        {

            BORRARerrores();
            try
            {
                if (this.dtNuevaFacturaBindingSource.Count > 0)
                {

                    if (this.panelBotones.Tag.ToString() == "Nuevo")
                    {
                        this.dtNuevaFacturaDataGridView.Rows.RemoveAt(this.dtNuevaFacturaDataGridView.CurrentCell.RowIndex);
                    }
                    else
                    {
                        RestaurarDatosFatu();
                        LlenarGrid(this.dtDetallesFacturaDataGridView, 1);
                        if (ClasDatos.NombreFactura == "Nota2")
                        {
                            LlenarGrid(this.dtDetallesFacturaDataGridView2, 2);
                            CalcularTotales(this.dtDetallesFacturaDataGridView2);
                        }
                        CalcularTotales(this.dtDetallesFacturaDataGridView);
                    }
                }
                this.dtDetallesFacturaDataGridView.Refresh();
                this.dtNuevaFacturaDataGridView.Focus();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
            RestaraurarOjetosFatu();


        }

        private void BtnSalirFactura_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(" ¿Salir Facturar ? ", " FACTURA ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
            }
        }
        private void RestaurarDatosFatu()
        {
            if (!string.IsNullOrEmpty(this.RazonSocial))
            {
                this.RazonSocialFatu.Text = this.RazonSocial;
            }
            else
            {
                this.RazonSocialFatu.Text = "";
            }
            if (!string.IsNullOrEmpty(this.Nombre))
            {
                this.NombreClienteFatu.Text = this.Nombre;
            }
            else
            {
                this.NombreClienteFatu.Text = "";
            }
            if (!string.IsNullOrEmpty(this.Direcion))
            {
                this.DirecionClienteFatu.Text = this.Direcion;
            }
            else
            {
                this.DirecionClienteFatu.Text = "";
            }
            if (!string.IsNullOrEmpty(this.Calle))
            {
                this.CalleTex.Text = this.Calle;
            }
            else
            {
                this.CalleTex.Text = "";
            }
            if (!string.IsNullOrEmpty(this.NumeroCalle))
            {
                this.NumeroCalleTxt.Text = this.NumeroCalle;
            }
            else
            {
                this.NumeroCalleTxt.Text = "";
            }
            if (!string.IsNullOrEmpty(this.Dni))
            {
                this.DniTextBox.Text = this.Dni;
            }
            else
            {
                this.DniTextBox.Text = "";
            }
            if (!string.IsNullOrEmpty(this.Localidad))
            {
                this.LocalidadTxt.Text = this.Localidad;
            }
            else
            {
                this.LocalidadTxt.Text = "";
            }
            if (!string.IsNullOrEmpty(this.Provincia))
            {
                this.ProvinciaTxt.Text = this.Provincia;
            }
            else
            {
                this.ProvinciaTxt.Text = "";
            }
            if (!string.IsNullOrEmpty(this.Pais))
            {
                this.PaisFatuTxt.Text = this.Pais;
            }
            else
            {
                this.PaisFatuTxt.Text = "";
            }
            if (!string.IsNullOrEmpty(this.CodigoPostal))
            {
                this.CodigoPostalTxt.Text = this.CodigoPostal;
            }
            else
            {
                this.CodigoPostalTxt.Text = "";
            }
            if (!string.IsNullOrEmpty(this.Obra))
            {
                this.ObraFactuTxt.Text = this.Obra;
            }
            else
            {
                this.ObraFactuTxt.Text = "";
            }
            if (!string.IsNullOrEmpty(this.Almacen))
            {
                this.AlmacenTxt.Text = this.Almacen;
            }
            else
            {
                this.AlmacenTxt.Text = "";
            }
            if (!string.IsNullOrEmpty(this.Proveedor))
            {
                this.ProveedorTxt.Text = this.Proveedor;
            }
            else
            {
                this.ProveedorTxt.Text = "";
            }
            if (!string.IsNullOrEmpty(this.Fecha))
            {
                this.FechaFactura.Text = this.Fecha;
            }
            else
            {
                this.FechaFactura.Text = "";
            }
            this.cobradaFacturaCheckBox.Checked = this.Cobrado;
        }
        private void CargarDatosFatu()
        {
            if (!string.IsNullOrEmpty(this.RazonSocialFatu.Text))
            {
                this.RazonSocial = this.RazonSocialFatu.Text;
            }
            if (!string.IsNullOrEmpty(this.NombreClienteFatu.Text))
            {
                this.Nombre = this.NombreClienteFatu.Text;
            }
            if (!string.IsNullOrEmpty(this.DirecionClienteFatu.Text))
            {
                this.Direcion = this.DirecionClienteFatu.Text;
            }
            if (!string.IsNullOrEmpty(this.CalleTex.Text))
            {
                this.Calle = this.CalleTex.Text;
            }
            if (!string.IsNullOrEmpty(this.NumeroCalleTxt.Text))
            {
                this.NumeroCalle = this.NumeroCalleTxt.Text;
            }
            if (!string.IsNullOrEmpty(this.DniTextBox.Text))
            {
                this.Dni = this.DniTextBox.Text;
            }

            if (!string.IsNullOrEmpty(this.LocalidadTxt.Text))
            {
                this.Localidad = this.LocalidadTxt.Text;
            }
            if (!string.IsNullOrEmpty(this.ProvinciaTxt.Text))
            {
                this.Provincia = this.ProvinciaTxt.Text;
            }
            if (!string.IsNullOrEmpty(this.PaisFatuTxt.Text))
            {
                this.Pais = this.PaisFatuTxt.Text;
            }
            if (!string.IsNullOrEmpty(this.CodigoPostalTxt.Text))
            {
                this.CodigoPostal = this.CodigoPostalTxt.Text;
            }
            if (!string.IsNullOrEmpty(this.ObraFactuTxt.Text))
            {
                this.Obra = this.ObraFactuTxt.Text;
            }
            if (!string.IsNullOrEmpty(this.AlmacenTxt.Text))
            {
                this.Almacen = this.AlmacenTxt.Text;
            }
            if (!string.IsNullOrEmpty(this.ProveedorTxt.Text))
            {
                this.Proveedor = this.ProveedorTxt.Text;
            }
            if (!string.IsNullOrEmpty(this.FechaFactura.Text.ToString()))
            {
                this.Fecha = this.FechaFactura.Text;
            }
            this.Cobrado = this.cobradaFacturaCheckBox.Checked;
        }
        private void BtnModificarFactura_Click(object sender, EventArgs e)
        {
            if (this.dtNuevaFacturaBindingSource.Count > 0)
            {
                if (this.TipoTarifaFactu.Items.Count > 8)
                {

                    MessageBox.Show("Debe Verificar Listado de Clientes", "FALTAN DATOS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); ;
                }

                this.panelBotones.Tag = "Modificar";
                this.TabControlFactura.SelectedIndex = 0;
                ModificarOjetosFatu();
                this.NumeroFactura.Enabled = false;
                ExtraerGrid(this.dtDetallesFacturaDataGridView, 1);
                if (ClasDatos.NombreFactura == "Nota2")
                {
                    ExtraerGrid(this.dtDetallesFacturaDataGridView2, 2);
                }
                CargarDatosFatu();
                this.RazonSocialFatu.Focus();
            }
        }
        public bool ActualizarArticuloFatu(int Fila, string Referencia, DataGridView Datagri)
        {
            bool ok = false;
            if (string.IsNullOrEmpty(this.TarifaRealTxt.Text))
            {
                MessageBox.Show("Faltan datos", "ERROR AL BUSCAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return ok;

            }

            string ColumnaPvp = "Pvp1";
            string ColumnaDesc = "";
            int FilaDesc = Convert.ToInt32(this.TipoTarifaFactu.SelectedIndex) + 1;
            // ColumnaPvp = this.ListaTarifas[FilaDesc].ToString();

            string ConsultaArtiFatu = "SELECT [Referencia],[Descripcci],[" + ColumnaPvp + "]" +
                 "FROM [" + FormMenuPrincipal.menu2principal.InfoArticulo.Text + "] where [Referencia] = @Referencia";
            try
            {
                if (this.CheckDescuentos.Checked)
                {
                    ColumnaPvp = this.TarifaRealTxt.Text;

                    if (this.TarifaRealTxt.Text.ToString() != "PVP1" || this.TarifaRealTxt.Text.ToString() != "IVA" || this.TarifaRealTxt.Text.ToString() != "PLUS")
                    {

                        ColumnaDesc = "Desc" + FilaDesc.ToString();

                    }
                    if (this.TarifaRealTxt.Text.ToString() == "PLUS")
                    {
                        ColumnaDesc = "DescPlus";
                    }
                    ConsultaArtiFatu = "SELECT [Referencia],[Descripcci],[" + ColumnaPvp + "],[" + ColumnaDesc + "]" +
                  "FROM [" + FormMenuPrincipal.menu2principal.InfoArticulo.Text + "] where [Referencia] = @Referencia";
                }

                if (ClsConexionSql.SibaseDatosSql)
                {

                }
                else
                {
                    ClsConexionDb NuevaConexion = new ClsConexionDb(ConsultaArtiFatu);
                    if (NuevaConexion.SiConexionDb)
                    {
                        NuevaConexion.ComandoDb.Parameters.AddWithValue("@Referencia", string.IsNullOrEmpty(Referencia) ? (object)DBNull.Value : Referencia);
                        OleDbDataReader Leer = NuevaConexion.ComandoDb.ExecuteReader();
                        if (Leer.HasRows)
                        {
                            if (Leer.Read())
                            {
                                Datagri.Rows[Fila].Cells[3].Value = Leer["Descripcci"];

                                if (this.CheckDescuentos.Checked)
                                {
                                    Datagri.Rows[Fila].Cells[4].Value = Leer[ColumnaPvp];
                                    if (!string.IsNullOrEmpty(Leer[ColumnaDesc].ToString()))
                                    {
                                        Datagri.Rows[Fila].Cells[5].Value = Leer[ColumnaDesc];
                                    }

                                }
                                else
                                {
                                    Datagri.Rows[Fila].Cells[4].Value = Leer[ColumnaPvp];
                                    Datagri.Rows[Fila].Cells[5].Value = DBNull.Value;
                                }
                                // Datagri.Focus();
                                // Datagri.CurrentCell.Value
                                ok = true;
                            }
                        }
                    }

                }

                return ok;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString(), "ERROR AL BUSCAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ok;
            }

        }
        private void ActualizarFacturaSql()
        {
            // string consulta = "SELECT * from DtNuevaFactura";
            if (!string.IsNullOrEmpty(this.Id_Empresa.Text) && !string.IsNullOrEmpty(this.ejerciciosDeAñoComboBox.Text) && !string.IsNullOrEmpty(this.SerieText.Text))
            {

                int Id = this.ejerciciosDeAñoComboBox.SelectedIndex;
                Int32 Id_Ejercicio = 1;
                if (!String.IsNullOrEmpty(FormMenuPrincipal.menu2principal.dsCONFIGURACCION.Tables["DtConfi"].Rows[Id]["EjercicioTipo"].ToString()))
                {
                    Id_Ejercicio = Convert.ToInt32(FormMenuPrincipal.menu2principal.dsCONFIGURACCION.Tables["DtConfi"].Rows[Id]["EjercicioTipo"].ToString());
                }
                string consulta = "select * FROM [Dt" + ClasDatos.NombreFactura + "]" + " where  [EmpresaEnlace] = " + Convert.ToInt32(this.Id_Empresa.Text) + "and" +
                   "[EjercicioTipo] = " + Id_Ejercicio;
                string consultaDetalle = "SELECT * from [DtDetalles_" + ClasDatos.NombreFactura + "]";
                string consultaDetalle2 = "SELECT * from [DtDetalles2_" + ClasDatos.NombreFactura + "]";
                ClsConexionSql NuevaConexion = new ClsConexionSql(consulta);

                try
                {
                    if (NuevaConexion.SiConexionSql)
                    {
                        this.dsFacturas.Clear();
                        //this.dtDetallesFacturaBindingSource.Clear();
                        //this.dtDetallesFactura2BindingSource.Clear();
                        this.dsFacturas.Clear();
                        SqlDataAdapter AdactaPelos = new SqlDataAdapter(consulta, ClsConexionSql.CadenaConexion);
                        AdactaPelos.Fill(this.dsFacturas.DtNuevaFactura);
                        FiltrarFactura();
                        AdactaPelos = new SqlDataAdapter(consultaDetalle, ClsConexionSql.CadenaConexion);
                        AdactaPelos.Fill(this.dsFacturas.DtDetallesFactura);

                        if (ClasDatos.NombreFactura == "Nota2")
                        {
                            AdactaPelos = new SqlDataAdapter(consultaDetalle2, ClsConexionSql.CadenaConexion);
                            AdactaPelos.Fill(this.dsFacturas.DtDetallesFactura2);
                        }
                        AdactaPelos.Dispose();
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString(), "ERROR AL CARGAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (NuevaConexion.CerrarConexionSql)
                    {

                    }
                }
            }
        }
        private void FormFACTURAR_Load(object sender, EventArgs e)
        {
            // dtNuevaFacturaDataGridView.SortModeDataGridViewColumnSortMode = false ;
            this.TipoNota.Text = ClasDatos.NombreFactura;
            this.Text = ClasDatos.NombreFactura;
            // ClasDatos.ArchivoInicioFacturas = Directory.GetCurrentDirectory() + "\\" + ClasDatos.RutaDatosPrincipal + "\\" + ClasDatos.NombreFactura + FormMenuPrincipal.menu2principal.InfoExtension.Text;
            try
            {
                if (FormMenuPrincipal.menu2principal.SiOpenBuscArti == 1)
                {
                    FormBuscarArticulos.MenuB.Close();
                    FormBuscarArticulos.MenuB.Dispose();
                }
                if (FormMenuPrincipal.menu2principal.articulos != null)
                {
                    this.dtArticulosBindingSource.DataSource = FormMenuPrincipal.menu2principal.articulos;
                }
                if (FormMenuPrincipal.menu2principal.dsClientes != null)
                {
                    this.dtClientesBindingSource.DataSource = FormMenuPrincipal.menu2principal.dsClientes;
                }
                if (FormMenuPrincipal.menu2principal.dsCONFIGURACCION != null)
                {
                    this.dtConfiguracionPrincipalBindingSource.DataSource = FormMenuPrincipal.menu2principal.dsCONFIGURACCION.DtConfiguracionPrincipal;
                    this.dtConfiBindingSource.DataSource = FormMenuPrincipal.menu2principal.dsCONFIGURACCION.DtConfi;
                    this.dtConfiDtTarifaTipoBindingSource.DataSource = FormMenuPrincipal.menu2principal.dsCONFIGURACCION.DtTarifaTipo;
                    this.dtAlmacenesBindingSource.DataSource = FormMenuPrincipal.menu2principal.dsCONFIGURACCION.DtAlmacenes;
                }
                if (FormMenuPrincipal.menu2principal.dsMulti2 != null)
                {
                    this.dtObrasBindingSource.DataSource = FormMenuPrincipal.menu2principal.dsMulti2.DtObras;
                    this.dtPaisesBindingSource.DataSource = FormMenuPrincipal.menu2principal.dsMulti2.DtPaises;
                    // this.dtProvinciasBindingSource.DataSource = FormMenuPrincipal.menu2principal.dsMulti2.DtProvincias;
                }
                if (FormMenuPrincipal.menu2principal.dsMultidatos != null)
                {
                    this.dtInicioMultiBindingSource.DataSource = FormMenuPrincipal.menu2principal.dsMultidatos.DtInicioMulti;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }

            if (File.Exists(ClasDatos.RutaBaseDatosDb))
            {
                if (FormMenuPrincipal.menu2principal.dsMultidatos.DtInicioMulti.Count > 0)
                {
                    // this.empresaENLACEComboBox.Focus();
                    if (FormMenuPrincipal.menu2principal.dsMultidatos.Tables["DtInicioMulti"].Rows[0]["EjercicioInicio"].ToString() != string.Empty)
                    {
                        this.EmpresaPrincipal.Text = FormMenuPrincipal.menu2principal.dsMultidatos.Tables["DtInicioMulti"].Rows[0]["EmpresaInicio"].ToString();
                    }
                    if (FormMenuPrincipal.menu2principal.dsMultidatos.Tables["DtInicioMulti"].Rows[0]["EjercicioInicio"].ToString() != string.Empty)
                    {
                        this.ejerciciosDeAñoComboBox.Text = FormMenuPrincipal.menu2principal.dsMultidatos.Tables["DtInicioMulti"].Rows[0]["EjercicioInicio"].ToString();
                    }
                    if (FormMenuPrincipal.menu2principal.dsMultidatos.Tables["DtInicioMulti"].Rows[0]["SerieInicio"].ToString() != string.Empty)
                    {
                        this.SerieText.Text = FormMenuPrincipal.menu2principal.dsMultidatos.Tables["DtInicioMulti"].Rows[0]["SerieInicio"].ToString();
                    }
                    else
                    {
                        this.SerieText.Text = "A";
                    }



                }


            }

            else
            {
                MessageBox.Show("Falta Archivo De Datos", "ARCHIVO NO EXISTE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (FormMenuPrincipal.menu2principal.dsCONFIGURACCION.DtTarifaTipo.Count > 0)
            {

            }

            if (this.dtConfiBindingSource.Count < 1)
            {

                this.panelBotones.Enabled = false;
                MessageBox.Show("Debe Crear Ejercicio", "NUEVO");
            }
            if (this.dtNuevaFacturaBindingSource.Count > 0)
            {

                if (this.subTotal.Text != string.Empty)
                {
                    this.subTotal.Text = string.Format("{0:C" + this.NumPrecio.Value + "}", (this.subTotal.Text));
                    this.subTotal.Text = this.subTotal.Text.ToString();
                    this.baseIva.Text = string.Format("{0:C" + this.NumPrecio.Value + "}", (this.baseIva.Text));
                    this.TotalFactura1.Text = string.Format("{0:C" + this.NumPrecio.Value + "}", (this.TotalFactura1.Text));
                }
                this.dtDetallesFacturaDataGridView.Columns["ImporteDetalle"].DefaultCellStyle.Format = "C" + this.NumPrecio.Value;

                if (this.dtNuevaFacturaDataGridView.Rows[0].Cells[13].Value.ToString() == "Cobrado")
                {
                    this.cobradaFacturaCheckBox.Checked = true;
                }
                else
                {
                    this.cobradaFacturaCheckBox.Checked = false;
                }

            }

            if (ClasDatos.NombreFactura == "Nota2")
            {
                // tabControl1Factura.TabPages.Insert(1, tabPage4Factura);
                this.dtDetallesFacturaDataGridView.Columns[6].DefaultCellStyle.Format = "C" + this.NumPrecio.Value;
                this.TotalFactura2.Visible = true;

            }
            else
            {
                this.tabPage4Factura.Parent = null;
                this.TotalFactura2.Visible = false;
            }
            if (this.Id_Empresa.Text == string.Empty)
            {
                MessageBox.Show("Faltan Datos o Datos Erradicos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.panelBotones.Enabled = false;
            }
            if (this.SerieText.Text == string.Empty)
            {
                this.SerieText.Text = "A";
            }

            // FiltrarFactura();
            if (ClsConexionSql.SibaseDatosSql)
            {
                ActualizarFacturaSql();

            }
            else
            {
                if (File.Exists(ClasDatos.RutaBaseDatosDb))
                {
                    ActualizarFaturas_DB();
                }
                else
                {
                    MessageBox.Show(ClasDatos.RutaBaseDatosDb, "ARCHIVO NO EXISTE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.panelBotones.Enabled = false;
                    return;
                }
            }
            // this.NombreEmpresaReguistro.Visible = false;
            this.PanelArriba.Tag = "SI";
            FiltrarFactura();
        }
        private void GuardarRestaurarStockDb()
        {

            int Id_Almacen = 0;
            int FilaALMACEN = Convert.ToInt32(this.AlmacenTxt.SelectedIndex);
            if (FormMenuPrincipal.menu2principal.dsCONFIGURACCION.Tables["DtAlmacenes"].Rows[FilaALMACEN]["Id"].ToString() != string.Empty)
            {
                Id_Almacen = Convert.ToInt32(FormMenuPrincipal.menu2principal.dsCONFIGURACCION.Tables["DtAlmacenes"].Rows[FilaALMACEN]["Id"].ToString());
            }
            string Enlace = this.Id_Empresa.Text + "/" + this.AlmacenTxt.Text;
            string consulta = "SELECT [Referencia],[Stock],[Enlace] from [DtMovimientos]" + "Where  Referencia= @Referencia  and  Enlace= @Enlace ";
            string Nueva = "INSERT INTO  [DtMovimientos] (Referencia,Stock,Enlace,Id_Empresa,Id_Almacen) VALUES(@Referencia,@Stock,@Enlace,@Id_Empresa,@Id_Almacen)";
            string Modificar = "UPDATE[DtMovimientos] SET[Referencia] = @Referencia,[Stock] = @Stock ,[Enlace] = @Enlace" +
               ",[Id_Empresa] = @Id_Empresa,[Id_Almacen] = @Id_Almacen WHERE  Referencia= @Referencia  and  Enlace= @Enlace ";
            int Stock;
            //int FilaStock = 0;
            OleDbDataReader reader = null;
            ClsConexionDb ConexionStock = new ClsConexionDb(consulta);
            ClsConexionDb ConexionNueva = new ClsConexionDb(Nueva);
            ClsConexionDb ConexionModificar = new ClsConexionDb(Modificar);
            try
            {
                if (ConexionStock.SiConexionDb)
                {
                    foreach (var Fila in ClasDetalleGrid.Listadetalle1.lista)
                    {
                        Stock = 0;
                        if (!string.IsNullOrEmpty(Fila.Referencia))
                        {
                            // ConexionStock = new ClsConexionDb(consulta);

                            ConexionStock.ComandoDb.Parameters.AddWithValue("@Referencia", Fila.Referencia);
                            ConexionStock.ComandoDb.Parameters.AddWithValue("@Enlace", string.IsNullOrEmpty(Enlace) ? (object)DBNull.Value : Enlace);
                            // ConexionStock.ComandoDb.Parameters.AddWithValue("@Stock", FilaStock);
                            reader = ConexionStock.ComandoDb.ExecuteReader();
                            if (reader.HasRows)
                            {
                                if (reader.HasRows)
                                {

                                    if (ConexionModificar.SiConexionDb)
                                    {
                                        //reader = ConexionStock.ComandoDb.ExecuteReader();

                                        if (!string.IsNullOrEmpty(Fila.Cantidad))
                                        {
                                            if (reader.HasRows)
                                            {
                                                if (reader.Read())
                                                {
                                                    Stock = Convert.ToInt32(reader["Stock"].ToString()) + Convert.ToInt32(Fila.Cantidad);
                                                    ConexionModificar.ComandoDb.Parameters.AddWithValue("@Referencia", string.IsNullOrEmpty(Fila.Referencia) ? (object)DBNull.Value : Fila.Referencia);
                                                    ConexionModificar.ComandoDb.Parameters.AddWithValue("@Stock", Stock);
                                                    ConexionModificar.ComandoDb.Parameters.AddWithValue("@Enlace", string.IsNullOrEmpty(Enlace) ? (object)DBNull.Value : Enlace);
                                                    ConexionModificar.ComandoDb.Parameters.AddWithValue("@Id_Empresa", string.IsNullOrEmpty(this.Id_Empresa.Text) ? (object)DBNull.Value : Convert.ToInt32(this.Id_Empresa.Text));
                                                    ConexionModificar.ComandoDb.Parameters.AddWithValue("@Id_Almacen", Id_Almacen);
                                                    ConexionModificar.ComandoDb.ExecuteNonQuery();
                                                    ConexionModificar.ComandoDb.Parameters.Clear();
                                                    reader.Close();
                                                }
                                            }
                                        }
                                    }
                                    reader.Close();
                                }
                                else
                                {

                                    if (ConexionNueva.SiConexionDb)
                                    {
                                        Stock = Convert.ToInt32(Fila.Cantidad);
                                        ConexionNueva.ComandoDb.Parameters.AddWithValue("@Referencia", string.IsNullOrEmpty(Fila.Referencia) ? (object)DBNull.Value : Fila.Referencia);
                                        ConexionNueva.ComandoDb.Parameters.AddWithValue("@Stock", Stock);
                                        ConexionNueva.ComandoDb.Parameters.AddWithValue("@Enlace", string.IsNullOrEmpty(Enlace) ? (object)DBNull.Value : Enlace);
                                        ConexionNueva.ComandoDb.Parameters.AddWithValue("@Id_Empresa", string.IsNullOrEmpty(this.Id_Empresa.Text) ? (object)DBNull.Value : Convert.ToInt32(this.Id_Empresa.Text));
                                        ConexionNueva.ComandoDb.Parameters.AddWithValue("@Id_Almacen", Id_Almacen);
                                        ConexionNueva.ComandoDb.ExecuteNonQuery();
                                        ConexionNueva.ComandoDb.Parameters.Clear();

                                    }
                                    reader.Close();
                                }

                            }

                        }

                        //ConexionStock.ComandoDb.Parameters.Clear();
                    }
                }

            }
            catch (Exception ex)
            {
                // reader.Close();
                MessageBox.Show(ex.Message.ToString(), "ERROR RESTAURAR STOCK", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ConexionStock.ComandoDb.Parameters.Clear();
            if (ConexionStock.CerrarConexionDB)
            {

            }
            if (ConexionNueva.CerrarConexionDB)
            {

            }
            if (ConexionModificar.CerrarConexionDB)
            {

            }
        }
        private void GuardarStockSql(DataGridView Datagri4)
        {
            int Id_Almacen = 0;
            int FilaALMACEN = Convert.ToInt32(this.AlmacenTxt.SelectedIndex);
            if (FormMenuPrincipal.menu2principal.dsCONFIGURACCION.Tables["DtAlmacenes"].Rows[FilaALMACEN]["Id"].ToString() != string.Empty)
            {
                Id_Almacen = Convert.ToInt32(FormMenuPrincipal.menu2principal.dsCONFIGURACCION.Tables["DtAlmacenes"].Rows[FilaALMACEN]["Id"].ToString());
            }
            string Enlace = this.Id_Empresa.Text + "/" + this.AlmacenTxt.Text;
            string consulta = "SELECT [Referencia],[Stock],[Enlace] from [DtMovimientos]" + "Where  Referencia= @Referencia  and  Enlace= @Enlace ";
            string Nueva = "INSERT INTO  [DtMovimientos] (Referencia,Stock,Enlace,Id_Empresa,Id_Almacen) VALUES(@Referencia,@Stock,@Enlace,@Id_Empresa,@Id_Almacen)";
            string Modificar = "UPDATE[DtMovimientos] SET[Referencia] = @Referencia,[Stock] = @Stock ,[Enlace] = @Enlace" +
               ",[Id_Empresa] = @Id_Empresa,[Id_Almacen] = @Id_Almacen WHERE  Referencia= @Referencia  and  Enlace= @Enlace ";
            int Stock = 0;
            //int FilaStock = 0;
            SqlDataReader reader;
            ClsConexionSql ConexionStock = new ClsConexionSql(consulta);
            ClsConexionSql ConexionNueva = new ClsConexionSql(Nueva);
            ClsConexionSql ConexionModificar = new ClsConexionSql(Modificar);
            try
            {
                if (ConexionStock.SiConexionSql)
                {
                    foreach (DataGridViewRow Fila in Datagri4.Rows)
                    {
                        if (Fila.Index == Datagri4.RowCount - 1)
                        {
                            break;
                        }
                        if (!string.IsNullOrEmpty(Fila.Cells[0].Value.ToString()))
                        {
                            // ConexionStock = new ClsConexionDb(consulta);

                            ConexionStock.ComandoSql.Parameters.AddWithValue("@Referencia", Fila.Cells[0].Value.ToString());
                            ConexionStock.ComandoSql.Parameters.AddWithValue("@Enlace", string.IsNullOrEmpty(Enlace) ? (object)DBNull.Value : Enlace);
                            // ConexionStock.ComandoDb.Parameters.AddWithValue("@Stock", FilaStock);
                            reader = ConexionStock.ComandoSql.ExecuteReader();

                            if (reader.HasRows)
                            {

                                if (ConexionModificar.SiConexionSql)
                                {
                                    //reader = ConexionStock.ComandoDb.ExecuteReader();
                                    if (reader.HasRows)
                                    {
                                        if (!string.IsNullOrEmpty(Fila.Cells[2].Value.ToString()))
                                        {
                                            while (reader.Read())
                                            {
                                                Stock = Convert.ToInt32(reader["Stock"]) - Convert.ToInt32(Fila.Cells[2].Value.ToString());
                                            }
                                        }
                                    }
                                    ConexionModificar.ComandoSql.Parameters.AddWithValue("@Referencia", string.IsNullOrEmpty(Fila.Cells[0].Value.ToString()) ? (object)DBNull.Value : Fila.Cells[0].Value.ToString());
                                    ConexionModificar.ComandoSql.Parameters.AddWithValue("@Stock", Stock);
                                    ConexionModificar.ComandoSql.Parameters.AddWithValue("@Enlace", string.IsNullOrEmpty(Enlace) ? (object)DBNull.Value : Enlace);
                                    ConexionModificar.ComandoSql.Parameters.AddWithValue("@Id_Empresa", string.IsNullOrEmpty(this.Id_Empresa.Text) ? (object)DBNull.Value : Convert.ToInt32(this.Id_Empresa.Text));
                                    ConexionModificar.ComandoSql.Parameters.AddWithValue("@Id_Almacen", Id_Almacen);
                                    ConexionModificar.ComandoSql.ExecuteNonQuery();
                                    ConexionModificar.ComandoSql.Parameters.Clear();
                                }
                            }
                            else
                            {

                                if (ConexionNueva.SiConexionSql)
                                {
                                    ConexionNueva.ComandoSql.Parameters.AddWithValue("@Referencia", string.IsNullOrEmpty(Fila.Cells[0].Value.ToString()) ? (object)DBNull.Value : Fila.Cells[0].Value.ToString());
                                    ConexionNueva.ComandoSql.Parameters.AddWithValue("@Stock", Stock);
                                    ConexionNueva.ComandoSql.Parameters.AddWithValue("@Enlace", string.IsNullOrEmpty(Enlace) ? (object)DBNull.Value : Enlace);
                                    ConexionNueva.ComandoSql.Parameters.AddWithValue("@Id_Empresa", string.IsNullOrEmpty(this.Id_Empresa.Text) ? (object)DBNull.Value : Convert.ToInt32(this.Id_Empresa.Text));
                                    ConexionNueva.ComandoSql.Parameters.AddWithValue("@Id_Almacen", Id_Almacen);
                                    ConexionNueva.ComandoSql.ExecuteNonQuery();
                                    ConexionNueva.ComandoSql.Parameters.Clear();


                                }
                            }
                        }
                        ConexionStock.ComandoSql.Parameters.Clear();
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString(), "ERROR STOCK", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ConexionStock.ComandoSql.Parameters.Clear();
            if (ConexionStock.CerrarConexionSql)
            {

            }
            if (ConexionNueva.CerrarConexionSql)
            {

            }
            if (ConexionModificar.CerrarConexionSql)
            {

            }
        }
        private void GuardarStockDb(DataGridView Datagri4)
        {
            int Id_Almacen = 0;

            string Enlace = this.Id_Empresa.Text + "/" + this.AlmacenTxt.Text;
            string consulta = "SELECT [Referencia],[Stock],[Enlace] from [DtMovimientos]" + " Where  Referencia= @Referencia  and  Enlace= @Enlace ";
            string Nueva = "INSERT INTO  [DtMovimientos] (Referencia,Stock,Enlace,Id_Empresa,Id_Almacen) VALUES(@Referencia,@Stock,@Enlace,@Id_Empresa,@Id_Almacen)";
            string Modificar = "UPDATE[DtMovimientos] SET[Referencia] = @Referencia,[Stock] = @Stock ,[Enlace] = @Enlace" +
               ",[Id_Empresa] = @Id_Empresa,[Id_Almacen] = @Id_Almacen WHERE  Referencia= @Referencia  and  Enlace= @Enlace ";
            int Stock;
            //int FilaStock = 0;
            OleDbDataReader reader = null;
            ClsConexionDb ConexionStock = new ClsConexionDb(consulta);
            ClsConexionDb ConexionNueva = new ClsConexionDb(Nueva);
            ClsConexionDb ConexionModificar = new ClsConexionDb(Modificar);
            try
            {
                int FilaALMACEN = Convert.ToInt32(this.AlmacenTxt.SelectedIndex);
                if (FilaALMACEN > this.dtAlmacenesBindingSource.Count - 1)
                {
                    MessageBox.Show("Falta Id De Almacen", "ERROR APP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (FormMenuPrincipal.menu2principal.dsCONFIGURACCION.Tables["DtAlmacenes"].Rows[FilaALMACEN]["Id"].ToString() != string.Empty)
                {
                    Id_Almacen = Convert.ToInt32(FormMenuPrincipal.menu2principal.dsCONFIGURACCION.Tables["DtAlmacenes"].Rows[FilaALMACEN]["Id"].ToString());
                }
                if (ConexionStock.SiConexionDb)
                {
                    foreach (DataGridViewRow Fila in Datagri4.Rows)
                    {
                        Stock = 0;
                        if (Fila.Index == Datagri4.RowCount - 1)
                        {
                            break;
                        }
                        if (!string.IsNullOrEmpty(Fila.Cells[0].Value.ToString()))
                        {
                            // ConexionStock = new ClsConexionDb(consulta);

                            ConexionStock.ComandoDb.Parameters.AddWithValue("@Referencia", Fila.Cells[0].Value.ToString());
                            ConexionStock.ComandoDb.Parameters.AddWithValue("@Enlace", string.IsNullOrEmpty(Enlace) ? (object)DBNull.Value : Enlace);
                            // ConexionStock.ComandoDb.Parameters.AddWithValue("@Stock", FilaStock);
                            reader = ConexionStock.ComandoDb.ExecuteReader();

                            if (reader.HasRows)
                            {

                                if (ConexionModificar.SiConexionDb)
                                {
                                    //reader = ConexionStock.ComandoDb.ExecuteReader();

                                    if (!string.IsNullOrEmpty(Fila.Cells[2].Value.ToString()))
                                    {
                                        if (reader.Read())
                                        {
                                            // MessageBox.Show(reader["Stock"].ToString());
                                            Stock = Convert.ToInt32(reader["Stock"].ToString()) - Convert.ToInt32(Fila.Cells[2].Value.ToString());

                                            ConexionModificar.ComandoDb.Parameters.AddWithValue("@Referencia", string.IsNullOrEmpty(Fila.Cells[0].Value.ToString()) ? (object)DBNull.Value : Fila.Cells[0].Value.ToString());
                                            ConexionModificar.ComandoDb.Parameters.AddWithValue("@Stock", Stock);
                                            ConexionModificar.ComandoDb.Parameters.AddWithValue("@Enlace", string.IsNullOrEmpty(Enlace) ? (object)DBNull.Value : Enlace);
                                            ConexionModificar.ComandoDb.Parameters.AddWithValue("@Id_Empresa", string.IsNullOrEmpty(this.Id_Empresa.Text) ? (object)DBNull.Value : Convert.ToInt32(this.Id_Empresa.Text));
                                            ConexionModificar.ComandoDb.Parameters.AddWithValue("@Id_Almacen", Id_Almacen);
                                            ConexionModificar.ComandoDb.ExecuteNonQuery();
                                            ConexionModificar.ComandoDb.Parameters.Clear();
                                            reader.Close();
                                        }
                                    }


                                }
                            }
                            else
                            {

                                if (ConexionNueva.SiConexionDb)
                                {
                                    Stock = Stock - Convert.ToInt32(Fila.Cells[2].Value.ToString());
                                    ConexionNueva.ComandoDb.Parameters.AddWithValue("@Referencia", string.IsNullOrEmpty(Fila.Cells[0].Value.ToString()) ? (object)DBNull.Value : Fila.Cells[0].Value.ToString());
                                    ConexionNueva.ComandoDb.Parameters.AddWithValue("@Stock", Stock);
                                    ConexionNueva.ComandoDb.Parameters.AddWithValue("@Enlace", string.IsNullOrEmpty(Enlace) ? (object)DBNull.Value : Enlace);
                                    ConexionNueva.ComandoDb.Parameters.AddWithValue("@Id_Empresa", string.IsNullOrEmpty(this.Id_Empresa.Text) ? (object)DBNull.Value : Convert.ToInt32(this.Id_Empresa.Text));
                                    ConexionNueva.ComandoDb.Parameters.AddWithValue("@Id_Almacen", Id_Almacen);
                                    ConexionNueva.ComandoDb.ExecuteNonQuery();
                                    ConexionNueva.ComandoDb.Parameters.Clear();
                                    reader.Close();


                                }
                            }
                        }

                        // reader.Close();
                        ConexionStock.ComandoDb.Parameters.Clear();
                    }
                }

            }
            catch (Exception ex)
            {
                // reader.Close();
                MessageBox.Show(ex.Message.ToString(), "ERROR STOCK", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ConexionStock.ComandoDb.Parameters.Clear();
            if (ConexionStock.CerrarConexionDB)
            {

            }
            if (ConexionNueva.CerrarConexionDB)
            {

            }
            if (ConexionModificar.CerrarConexionDB)
            {

            }
        }
        private void ActualizarFaturas_DB()
        {
            if (File.Exists(ClasDatos.RutaBaseDatosDb))
            {
                if (!string.IsNullOrEmpty(this.Id_Empresa.Text) && !string.IsNullOrEmpty(this.ejerciciosDeAñoComboBox.Text) && !string.IsNullOrEmpty(this.SerieText.Text))
                {
                    ClsConexionDb NuevaConexion = null;
                    OleDbDataAdapter AdactaPelos = null;
                    try
                    {


                        // dtNuevaFacturaBindingSource.Clear();
                        int Id = this.ejerciciosDeAñoComboBox.SelectedIndex;
                        Int32 Id_Ejercicio = 1;
                        if (!String.IsNullOrEmpty(FormMenuPrincipal.menu2principal.dsCONFIGURACCION.Tables["DtConfi"].Rows[Id]["IdEnlace"].ToString()))
                        {
                            Id_Ejercicio = Convert.ToInt32(FormMenuPrincipal.menu2principal.dsCONFIGURACCION.Tables["DtConfi"].Rows[Id]["IdEnlace"].ToString());
                        }
                        else
                        {
                            MessageBox.Show("No Se Encuentran Datos De Id");
                            return;
                        }
                        string consulta = "select * FROM [Dt" + ClasDatos.NombreFactura + "]" + " where  [EmpresaEnlace] = " + Convert.ToInt32(this.Id_Empresa.Text) + "and" +
                           "[EjercicioTipo] = " + Id_Ejercicio;
                        string consultaDetalle = "SELECT * from DtDetalles_" + ClasDatos.NombreFactura;
                        string consultaDetalle2 = "SELECT * from DtDetalles2_" + ClasDatos.NombreFactura;
                        NuevaConexion = new ClsConexionDb(consulta);
                        AdactaPelos = new OleDbDataAdapter(consulta, ClsConexionDb.CadenaConexion);

                        if (NuevaConexion.SiConexionDb)
                        {
                            //  this.dtNuevaFacturaBindingSource.Filter = "( [SerieTipo]   = '" NO "'" + ")";
                            this.dsFacturas.Clear();
                            this.dtDetallesFacturaBindingSource.Clear();
                            this.dtDetallesFactura2BindingSource.Clear();
                            FiltrarFactura();
                            AdactaPelos.Fill(this.dsFacturas.DtNuevaFactura);
                            // this.InfoTxt2.Text = this.dtNuevaFacturaBindingSource.Count.ToString();
                            if (this.dtNuevaFacturaBindingSource.Count > 0)
                            {
                                AdactaPelos = new OleDbDataAdapter(consultaDetalle, ClsConexionDb.CadenaConexion);
                                AdactaPelos.Fill(this.dsFacturas.DtDetallesFactura);
                                if (ClasDatos.NombreFactura == "Nota2")
                                {
                                    AdactaPelos = new OleDbDataAdapter(consultaDetalle2, ClsConexionDb.CadenaConexion);
                                    AdactaPelos.Fill(this.dsFacturas.DtDetallesFactura2);
                                }
                            }
                            AdactaPelos.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message.ToString(), "ERROR AL CARGAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        if (NuevaConexion.CerrarConexionDB)
                        {
                            AdactaPelos.Dispose();
                        }
                    }

                }
            }
            else
            {
                this.panelBotones.Enabled = false;
                MessageBox.Show("Archivo : " + ClasDatos.RutaBaseDatosDb, "Falta Archivo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }
        }
        private void EmpresaENLACEComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void EjerciciosDeAñoComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (this.PanelArriba.Tag.ToString() == "SI")
            {
                //  FiltrarFactura();
                if (ClsConexionSql.SibaseDatosSql)
                {
                    ActualizarFacturaSql();
                }
                else
                {
                    ActualizarFaturas_DB();
                }
            }
        }
        private void FiltrarFactura()
        {

            if (this.Id_Empresa.Text != string.Empty && this.ejerciciosDeAñoComboBox.Text != string.Empty && this.SerieText.Text != string.Empty)
            {
                try
                {
                    this.dtNuevaFacturaBindingSource.Filter = "( [SerieTipo]   = '" + this.SerieText.Text + "'" + ")";
                    this.dtNuevaFacturaDataGridView.Refresh();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString(), "ERROR AL FILTRAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }



        }
        private void SerieText_SelectedIndexChanged(object sender, EventArgs e)
        {

            FiltrarFactura();


        }

        private void DtDetallesFacturaDataGridView_KeyPress(object sender, KeyPressEventArgs e)
        {


            if (this.dtDetallesFacturaDataGridView.CurrentCell.ColumnIndex == 2 || this.dtDetallesFacturaDataGridView.CurrentCell.ColumnIndex == 4 || this.dtDetallesFacturaDataGridView.CurrentCell.ColumnIndex == 5)
            {
                // dtDetallesFacturaDataGridView.CurrentCell.EditedFormattedValue.ToString();
                if (e.KeyChar == 46)
                {
                    e.KeyChar = ',';
                }

                if (char.IsDigit(e.KeyChar))
                {
                    e.Handled = false;
                    // this.SoloNumerosText += (e.KeyChar.ToString());
                }
                else if (char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if ((e.KeyChar == ',') && (!this.dtDetallesFacturaDataGridView.CurrentCell.EditedFormattedValue.ToString().Contains(",")))
                {
                    e.Handled = false;
                    //  this.SoloNumerosText = (e.KeyChar.ToString());

                }

                else
                {
                    e.Handled = true;
                }
                if (this.dtDetallesFacturaDataGridView.CurrentCell.ColumnIndex == 2 || this.dtDetallesFacturaDataGridView.CurrentCell.ColumnIndex == 4)
                {
                    if ((e.KeyChar == '-') && (!this.dtDetallesFacturaDataGridView.CurrentCell.EditedFormattedValue.ToString().Contains("-")))
                    {
                        e.Handled = false;
                        // this.SoloNumerosText = (e.KeyChar.ToString());

                    }

                }

            }
            if (this.dtDetallesFacturaDataGridView.CurrentCell.ColumnIndex == 0)
            {


            }
        }

        private void DtDetallesFacturaDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {

                if (this.BtnGuardarFactura.Enabled == true)
                {
             
                    if (e.RowIndex == dtDetallesFacturaDataGridView.RowCount)
                    {
                        dtDetallesFacturaBindingSource.AddNew();
                    }
                    if (e.ColumnIndex == 1)
                    {
                        if (string.IsNullOrEmpty(this.TarifaRealTxt.Text))
                        {
                            MessageBox.Show("Faltan datos", "ERROR AL BUSCAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            TabControlFactura.SelectedIndex = 3;
                            this.TipoTarifaFactu.Focus();
                            return;

                        }
                        if (this.dtArticulosBindingSource.Count <= 0)
                        {
                            MessageBox.Show(" Archivo  VACIO ", " FALTA O VACIO ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            ClasDatos.ValorBuscado = e.RowIndex;
                            int numeroFILA = this.dtNuevaFacturaDataGridView.Rows.Count;
                            if (numeroFILA > 0)
                            {
                                if (FormMenuPrincipal.menu2principal.SiOpenBuscArti == 1)
                                {
                                    MessageBox.Show("Debe Cerrar Formulario ((CODIGO BARRAS)) ", " FORMILARIO ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                    // FormBuscarArticulos.MenuB.Close();
                                }
                                ClasDatos.OkFacturar = true;
                                ClasDatos.Datos1Datos2 = "Nota1";
                                // dtNuevaFacturaDataGridView.CurrentCell.Selected = false;
                                FormBuscarArticulos frm = new FormBuscarArticulos();
                                frm.FormClosed += (o, args) => numeroFILA = 1;
                                //frm.FormClosed += (o, args) => CalcularImportes(this.dtDetallesFacturaDataGridView);
                                frm.Show();
                                frm.BringToFront();



                            }
                        }



                    }
                    try
                    {
                        if (e.ColumnIndex == 5)
                        {
                            this.dtDetallesFacturaDataGridView.CurrentCell.Value = this.dtDetallesFacturaDataGridView.CurrentCell.Value.ToString().Replace("%", "");
                        }

                        if (e.ColumnIndex == 2 || e.ColumnIndex == 4 || e.ColumnIndex == 5)
                        {
                            if (string.IsNullOrEmpty(this.dtDetallesFacturaDataGridView.CurrentCell.Value.ToString()))
                            {
                                this.dtDetallesFacturaDataGridView.CurrentCell.Value = 0;
                            }
                            this.dtDetallesFacturaDataGridView.BeginEdit(true);
                        }
                    }
                    catch (Exception)
                    {

                        // throw;
                    }
                }


            }
        }

        private void DtDetallesFacturaDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                // AddHandler e.Control.KeyDown, AddressOf cell_KeyDown
                DataGridViewTextBoxEditingControl tb = e.Control as DataGridViewTextBoxEditingControl;
                tb.KeyPress -= DtDetallesFacturaDataGridView_KeyPress;
                tb.KeyPress += new KeyPressEventHandler(DtDetallesFacturaDataGridView_KeyPress);

            }
        }

        private void DtDetallesFacturaDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (this.BtnGuardarFactura.Enabled == false)
            {

                return;
            }
            if (this.dtArticulosBindingSource.Count > 0)
            {
                if (e.RowIndex > -1)
                {
                    if (e.ColumnIndex == 0)
                    {
                        if (string.IsNullOrEmpty(this.TarifaRealTxt.Text.ToString()))
                        {
                            MessageBox.Show("Falta Tipo Tarifa", "ELEGUIR TARIFA");
                            return;
                        }
                        if (this.dtDetallesFacturaDataGridView.Rows[e.RowIndex].Cells[0].EditedFormattedValue.ToString() != string.Empty)
                        {
                            string Referencia = this.dtDetallesFacturaDataGridView.Rows[e.RowIndex].Cells[0].EditedFormattedValue.ToString();
                            Referencia = Referencia.ToUpper();
                            this.dtDetallesFacturaDataGridView.Rows[e.RowIndex].Cells[0].Value = Referencia.ToUpper();
                            if (!ActualizarArticuloFatu(e.RowIndex, Referencia, this.dtDetallesFacturaDataGridView))
                            {
                                MessageBox.Show(this.dtDetallesFacturaDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString(), "NO SE ENCONTRO ARTICULO");
                                this.dtDetallesFacturaDataGridView.Rows[e.RowIndex].Cells[0].Value = string.Empty;
                                return;
                            }
                        }

                    }

                }

                try
                {
                    if (e.ColumnIndex == 2 || e.ColumnIndex == 4 || e.ColumnIndex == 5 || e.ColumnIndex == 5)
                    {
                        if (this.dtDetallesFacturaDataGridView.AllowUserToAddRows == true)
                        {
                            if (e.RowIndex == this.dtDetallesFacturaDataGridView.Rows.Count)
                            {
                                return;
                            }
                        }
                        if (this.dtDetallesFacturaDataGridView.CurrentCell.Value.ToString() == "0")
                        {
                            this.dtDetallesFacturaDataGridView.CurrentCell.Value = DBNull.Value;
                        }
                      //  CalcularImportes(this.dtDetallesFacturaDataGridView);
                    }

                }
                catch (Exception)
                {

                    // throw;
                }
            }
            CalcularImportes(this.dtDetallesFacturaDataGridView);
        }

        private void BtnBuscarFactura_Click(object sender, EventArgs e)
        {
            if (this.dtNuevaFacturaBindingSource.Count > 0)
            {

                ClasDatos.OkFacturar = true;
                ClasDatos.QUEform = "FACTURA";
                AñadirIdFATU();
                FormBuscar frm = new FormBuscar();
                frm.CargarDatos(1, " Apodo", "Apodo", "Apodo", 1);
                frm.BringToFront();
                frm.ShowDialog();

            }
        }

        private void DtDetallesFacturaDataGridView_MouseEnter(object sender, EventArgs e)
        {
            FormMenuPrincipal.menu2principal.panelventas.Visible = false;
            FormMenuPrincipal.menu2principal.panelSUBventas.Visible = false;
        }

        private void TabPage1_MouseEnter(object sender, EventArgs e)
        {
            FormMenuPrincipal.menu2principal.panelventas.Visible = false;
            FormMenuPrincipal.menu2principal.panelSUBventas.Visible = false;
        }

        private void CobradaFacturaCheckBox_CheckedChanged(object sender, EventArgs e)
        {


        }

        private void CobradaFacturaCheckBox_CheckStateChanged(object sender, EventArgs e)
        {

            if (this.dtNuevaFacturaBindingSource.Count >= 0)
            {

                //int FILAcelda = dtNuevaFacturaDataGridView.CurrentCell.RowIndex;

                if (this.cobradaFacturaCheckBox.Checked == true)
                {
                    this.fechaCobroText.Text = "";
                    this.fechaCobroText.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
                    // dtfacturas.DtNuevaFactura.Rows[FILAcelda]["CobradaFactura"] = "Cobrado";
                }
                else
                {
                    this.fechaCobroText.Text = "";
                }
            }


        }

        private void DtNuevaFacturaDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (this.panelBotones.Enabled == true)
                {
                    this.dtDetallesFacturaDataGridView.Columns["ImporteDetalle"].DefaultCellStyle.Format = "C" + this.NumPrecio.Value;
                    // dtNuevaFacturaDataGridView.Cursor = Cursors.AppStarting;
                    // Thread.Sleep(2000);
                    // dtNuevaFacturaDataGridView.Enabled = false;
                    // dtNuevaFacturaDataGridView.Cursor = Cursors.Default;
                    if (this.dtNuevaFacturaDataGridView.RowCount >= 0)
                    {
                        if (this.dtNuevaFacturaDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString() == "Cobrado")
                        {
                            this.cobradaFacturaCheckBox.Checked = true;
                        }
                        else
                        {
                            this.cobradaFacturaCheckBox.Checked = false;
                        }

                    }
                }

            }


        }

        private void DtNuevaFacturaDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DtNuevaFacturaDataGridView_MouseEnter(object sender, EventArgs e)
        {
            FormMenuPrincipal.menu2principal.panelventas.Visible = false;
            FormMenuPrincipal.menu2principal.panelSUBventas.Visible = false;
        }

        private void DtDetallesFacturaDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {

                this.dtDetallesFacturaDataGridView.Rows[e.RowIndex].Cells[2].Value = DBNull.Value;
                this.dtDetallesFacturaDataGridView.CancelEdit();
                this.dtDetallesFacturaDataGridView.EndEdit();
                //MessageBox.Show(" Datos no Validos ", " CANTIDAD ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (e.ColumnIndex == 4)
            {

                this.dtDetallesFacturaDataGridView.Rows[e.RowIndex].Cells[4].Value = DBNull.Value;
                this.dtDetallesFacturaDataGridView.CancelEdit();
                this.dtDetallesFacturaDataGridView.EndEdit();

            }
            else if (e.ColumnIndex == 5)
            {
                this.dtDetallesFacturaDataGridView.Rows[e.RowIndex].Cells[5].Value = DBNull.Value;
                this.dtDetallesFacturaDataGridView.CancelEdit();
                this.dtDetallesFacturaDataGridView.EndEdit();
            }
            else if (e.ColumnIndex == 6)
            {
                this.dtDetallesFacturaDataGridView.Rows[e.RowIndex].Cells[6].Value = DBNull.Value;
                this.dtDetallesFacturaDataGridView.CancelEdit();
                this.dtDetallesFacturaDataGridView.EndEdit();
            }
            else
            {

                // MessageBox.Show(" No Validos ", " ERROR ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void BtnImprimirFactura_Click(object sender, EventArgs e)
        {
            if (this.dtNuevaFacturaBindingSource.Count <= 0)
            {
                MessageBox.Show("Nada Que Imprimir ", " IMPRIMIR ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ClasDatos.OkFacturar = true;
            ClasDatos.QUEform = "Facturar";
            FormImprimirTodo frm = new FormImprimirTodo();
            //frm.FormClosed += (o, args) => this.a = "1";
            frm.ShowDialog();
            frm.BringToFront();
        }

        private void FormFACTURAR_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.PanelArriba.Tag = "N0";
            if (this.BtnGuardarFactura.Enabled == true)
            {
                e.Cancel = true;
            }

        }

        private void DtDetallesFacturaDataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (this.BtnGuardarFactura.Enabled == true)
                {
                    if (e.RowIndex == dtDetallesFacturaDataGridView2.RowCount)
                    {
                        dtDetallesFactura2BindingSource.AddNew();
                    }
                    if (e.ColumnIndex == 1)
                    {
                        if (!ClsConexionSql.SibaseDatosSql)
                        {
                            if (!File.Exists(ClasDatos.RutaBaseDatosDb))
                            {
                                MessageBox.Show(" Archivo ARTICULOS No Existe ", " FALTA ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                        if (string.IsNullOrEmpty(this.TarifaRealTxt.Text))
                        {
                            MessageBox.Show("Faltan datos", "ERROR AL BUSCAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            TabControlFactura.SelectedIndex = 3;
                            this.TipoTarifaFactu.Focus();
                            return;

                        }
                        ClasDatos.ValorBuscado = e.RowIndex;
                        int numeroFILA = this.dtNuevaFacturaDataGridView.Rows.Count;
                        if (numeroFILA > 0)
                        {
                            if (this.BtnGuardarFactura.Enabled == true)
                            {
                                if (FormMenuPrincipal.menu2principal.SiOpenBuscArti == 1)
                                {
                                    FormBuscarArticulos.MenuB.Close();
                                }
                                ClasDatos.OkFacturar = true;
                                ClasDatos.Datos1Datos2 = "Nota2";
                                this.dtNuevaFacturaDataGridView.CurrentCell.Selected = false;
                                FormBuscarArticulos frm = new FormBuscarArticulos();
                                frm.FormClosed += (o, args) => numeroFILA = 1;
                                frm.FormClosed += (o, args) => CalcularImportes(this.dtDetallesFacturaDataGridView2);
                                frm.ShowDialog();
                                frm.BringToFront();


                            }
                        }


                    }
                    if (e.ColumnIndex == 2 || e.ColumnIndex == 4 || e.ColumnIndex == 5)
                    {
                        if (string.IsNullOrEmpty(this.dtDetallesFacturaDataGridView2.CurrentCell.Value.ToString()))
                        {
                            this.dtDetallesFacturaDataGridView2.CurrentCell.Value = 0;
                            this.dtDetallesFacturaDataGridView2.BeginEdit(true);
                        }

                    }
                }
            }
        }

        private void DtDetallesFacturaDataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 2 || e.ColumnIndex == 4 || e.ColumnIndex == 5 || e.ColumnIndex == 5)
            {
               

                if (this.dtDetallesFacturaDataGridView2.CurrentCell.Value.ToString() == "0")
                {
                    this.dtDetallesFacturaDataGridView2.CurrentCell.Value = DBNull.Value;
                }

            }
            if (e.ColumnIndex == 5)
            {
                if (!String.IsNullOrEmpty(this.dtDetallesFacturaDataGridView2.CurrentCell.Value.ToString()))
                {
                    this.dtDetallesFacturaDataGridView2.CurrentCell.Value = Convert.ToDouble(this.dtDetallesFacturaDataGridView2.CurrentCell.Value.ToString()) / 100;
                }

            }
            CalcularImportes(this.dtDetallesFacturaDataGridView2);
        }

        private void DtDetallesFacturaDataGridView2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.dtDetallesFacturaDataGridView2.CurrentCell.ColumnIndex == 2 || this.dtDetallesFacturaDataGridView2.CurrentCell.ColumnIndex == 4 || this.dtDetallesFacturaDataGridView2.CurrentCell.ColumnIndex == 5)
            {
                if (char.IsDigit(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if ((e.KeyChar == ',') && (!this.dtDetallesFacturaDataGridView2.CurrentCell.EditedFormattedValue.ToString().Contains(",")))
                {
                    e.Handled = false;

                }

                else
                {
                    e.Handled = true;
                }
                if (this.dtDetallesFacturaDataGridView2.CurrentCell.ColumnIndex == 2 || this.dtDetallesFacturaDataGridView2.CurrentCell.ColumnIndex == 4)
                {
                    if ((e.KeyChar == '-') && (!this.dtDetallesFacturaDataGridView2.CurrentCell.EditedFormattedValue.ToString().Contains(",")))
                    {
                        e.Handled = false;

                    }

                }

            }
        }

        private void DtDetallesFacturaDataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                this.dtDetallesFacturaDataGridView2.Rows[e.RowIndex].Cells[2].Value = DBNull.Value;
                this.dtDetallesFacturaDataGridView2.CancelEdit();
                this.dtDetallesFacturaDataGridView2.EndEdit();
            }
            else if (e.ColumnIndex == 4)
            {

                this.dtDetallesFacturaDataGridView2.Rows[e.RowIndex].Cells[4].Value = DBNull.Value;
                this.dtDetallesFacturaDataGridView2.CancelEdit();
                this.dtDetallesFacturaDataGridView2.EndEdit();
            }
            else if (e.ColumnIndex == 5)
            {
                this.dtDetallesFacturaDataGridView2.Rows[e.RowIndex].Cells[5].Value = DBNull.Value;
                this.dtDetallesFacturaDataGridView2.CancelEdit();
                this.dtDetallesFacturaDataGridView2.EndEdit();
            }

            else
            {
                // MessageBox.Show(" No Validos ", " ERROR ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DtDetallesFacturaDataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                DataGridViewTextBoxEditingControl tb = e.Control as DataGridViewTextBoxEditingControl;
                tb.KeyPress -= DtDetallesFacturaDataGridView2_KeyPress;
                tb.KeyPress += new KeyPressEventHandler(DtDetallesFacturaDataGridView2_KeyPress);
            }
        }

        private void TabControl1Factura_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (this.TabControlFactura.TabPages.Count == 4)
            {
                if (this.TabControlFactura.SelectedIndex == 2)
                {
                    this.panelTotales.Visible = false;
                    this.TotalFactura1.Visible = false;
                    this.TotalFactura2.Visible = true;

                }
                else
                {
                    this.panelTotales.Visible = true;
                    this.TotalFactura1.Visible = true;
                    this.TotalFactura2.Visible = false;
                }

            }
        }

        private void BtnBuscarClientesFact_Click(object sender, EventArgs e)
        {
            if (this.dtDetallesFacturaBindingSource.Count < 0 || this.dtDetallesFactura2BindingSource.Count < 0)
            {
                return;
            }

            if (FormMenuPrincipal.menu2principal.dsClientes.DtClientes.Count <= 0)
            {

                MessageBox.Show("No Hay Clientes", " ARCHIVO NO EXISTE O VACIO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.dtNuevaFacturaBindingSource.Count > 0)
            {
                ClasDatos.ValorBuscado = this.dtNuevaFacturaDataGridView.CurrentCell.RowIndex;


                ClasDatos.OkFacturar = true;
                ClasDatos.Datos1Datos2 = "Nota1";
                // dtNuevaFacturaDataGridView.CurrentCell.Selected = false;
                FormBuscarClientes frm = new FormBuscarClientes();
                // frm.FormClosed += (o, args) => numeroFILA = 1;
                frm.ShowDialog();
                frm.BringToFront();
            }
        }

        private void DtDetallesFacturaDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.dtDetallesFacturaDataGridView.CurrentCell.RowIndex > -1)
            {
                if (e.KeyCode == Keys.Down)
                {
                    if (this.dtDetallesFacturaDataGridView.CurrentCell.ColumnIndex == 0)
                    {

                        if (this.dtDetallesFacturaDataGridView.CurrentCell.RowIndex == this.dtDetallesFacturaDataGridView.RowCount - 1)
                        {
                            this.dtDetallesFacturaBindingSource.AddNew();
                        }
                    }
                }
                // dtDetallesFacturaBindingSource.AddNew();
            }
        }


        private void BtnEnviarMailFactura_Click(object sender, EventArgs e)
        {

        }



        private void BuscarReferencia_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.dtNuevaFacturaDataGridView.Rows.Count < 0)
                {
                    return;
                }
                int fila = this.dtDetallesFacturaDataGridView.CurrentCell.RowIndex;
                if (fila == 0)
                {
                    if (this.dtArticulosBindingSource.Count > 0)
                    {
                        for (int I = 0; I <= this.dtArticulosBindingSource.Count - 1; I++)
                        {

                            if (FormMenuPrincipal.menu2principal.articulos.Tables["DtArticulos"].Rows[I]["Referencia"].ToString().Contains(this.dtDetallesFacturaDataGridView.Rows[fila].Cells[0].Value.ToString()))
                            {
                                this.dtDetallesFacturaDataGridView.Rows[fila].Cells[0].Value = FormMenuPrincipal.menu2principal.articulos.Tables["DtArticulos"].Rows[I]["Referencia"].ToString();
                                this.dtDetallesFacturaDataGridView.Rows[fila].Cells[3].Value = FormMenuPrincipal.menu2principal.articulos.Tables["DtArticulos"].Rows[I]["Descripcci"].ToString();
                                this.dtDetallesFacturaDataGridView.Rows[fila].Cells[4].Value = FormMenuPrincipal.menu2principal.articulos.Tables["DtArticulos"].Rows[I]["Pvp1"].ToString();
                            }
                        }

                    }
                }
            }
            catch (Exception)
            {

                // throw;
            }
        }




        private void NumDescuento_ValueChanged(object sender, EventArgs e)
        {
            this.dtDetallesFacturaDataGridView.Columns[5].DefaultCellStyle.Format = "N" + this.NumDescuento.Value;
            this.dtDetallesFacturaDataGridView2.Columns[5].DefaultCellStyle.Format = "N" + this.NumDescuento.Value;

        }

        private void Numimporte_ValueChanged(object sender, EventArgs e)
        {

            this.dtDetallesFacturaDataGridView.Columns["ImporteDetalle"].DefaultCellStyle.Format = "C" + this.Numimporte.Value;
            if (ClasDatos.NombreFactura == "Nota2")
            {
                this.dtDetallesFacturaDataGridView2.Columns[6].DefaultCellStyle.Format = "C" + this.Numimporte.Value;
            }


        }

        private void NumTotales_ValueChanged(object sender, EventArgs e)
        {
            this.baseIva.Text = string.Format("{0:C" + this.NumTotales.Value + "}", this.baseIva.Text);
            this.TotalFactura1.Text = string.Format("{0:C" + this.NumTotales.Value + "}", this.TotalFactura1.Text.ToString());
            this.subTotal.Text = string.Format("{0:C" + this.NumTotales.Value + "}", this.subTotal.Text);
            CalcularImportes(this.dtDetallesFacturaDataGridView);
            if (ClasDatos.NombreFactura == "Nota 2")
            {
                CalcularImportes(this.dtDetallesFacturaDataGridView2);
            }

            Validate();
        }

        private void NumPrecio_ValueChanged(object sender, EventArgs e)
        {
            this.dtDetallesFacturaDataGridView.Columns[4].DefaultCellStyle.Format = "C" + this.NumPrecio.Value;
            this.dtDetallesFacturaDataGridView2.Columns[4].DefaultCellStyle.Format = "C" + this.NumPrecio.Value;
        }

        public void AñadirId()
        {
            int ii = 0;
            foreach (DataRow fila in FormMenuPrincipal.menu2principal.articulos.DtArticulos.Rows)
            {
                FormMenuPrincipal.menu2principal.articulos.Tables["DtArticulos"].Rows[ii]["IdFILA"] = ii.ToString();
                ii++;
            }

        }
        private void CheckDescuentos_Click(object sender, EventArgs e)
        {
            if (this.CheckDescuentos.Checked)
            {
                if (this.TarifaRealTxt.Text.ToString() == "PVP1" | this.TarifaRealTxt.Text.ToString() == "PVPIVA" | this.TarifaRealTxt.Text.ToString() == "PLUS")
                {
                    this.CheckDescuentos.Checked = false;
                    MessageBox.Show("Tarifa No Valida", "ELEGIR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            if (this.CheckDescuentos.Checked == false)
            {
                this.CheckDescuentos.Text = "Añadiendo Descuento";
                this.CheckDescuentos.Checked = true;
            }
            else
            {
                this.CheckDescuentos.Text = "Añadiendo Precio";
                this.CheckDescuentos.Checked = false;
            }
        }

        private void TarifaTipoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.TipoTarifaFactu.SelectedIndex >= 0)
            {
                try
                {
                    int FilaDesc = Convert.ToInt32(this.TipoTarifaFactu.SelectedIndex) + 1;
                    if (!string.IsNullOrEmpty(this.ListaTarifas[FilaDesc].ToString()))
                    {
                        // this.TarifaRealTxt.Text = this.ListaTarifas[FilaDesc].ToString();
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void DtDetallesFacturaDataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void Pais_FactComboBox_Click(object sender, EventArgs e)
        {
            this.PaisFatuTxt.DroppedDown = true;
            this.PaisFatuTxt.SelectAll();
        }

        private void ProvinciaComboBox_Click(object sender, EventArgs e)
        {
            this.ProvinciaTxt.DroppedDown = true;
            // provinciaComboBox.SelectAll();
        }

        private void obrasComboBox_Click(object sender, EventArgs e)
        {
            this.ObraFactuTxt.DroppedDown = true;
        }

        private void dtDetallesFacturaDataGridView_Validated(object sender, EventArgs e)
        {
            try
            {
                if (this.BtnGuardarFactura.Enabled == true)
                {
                    if (this.dtDetallesFacturaDataGridView.CurrentCell.RowIndex >= 0)
                    {
                        if (this.dtDetallesFacturaDataGridView.CurrentCell.ColumnIndex == 2 || this.dtDetallesFacturaDataGridView.CurrentCell.ColumnIndex == 4)
                        {
                            int i = this.dtDetallesFacturaDataGridView.CurrentCell.RowIndex;
                            if (this.dtDetallesFacturaDataGridView.Rows[i].Cells[4].Value.ToString() == "0" || this.dtDetallesFacturaDataGridView.Rows[i].Cells[2].Value.ToString() == "0")
                            {
                                this.dtDetallesFacturaDataGridView.Rows[i].Cells[7].Value = DBNull.Value;
                            }
                        }

                        if (this.dtDetallesFacturaDataGridView.CurrentCell.ColumnIndex == 5)
                        {
                            if (!String.IsNullOrEmpty(this.dtDetallesFacturaDataGridView.CurrentCell.Value.ToString()))
                            {
                                this.dtDetallesFacturaDataGridView.CurrentCell.Value = Convert.ToDouble(this.dtDetallesFacturaDataGridView.CurrentCell.Value.ToString()) / 100;
                            }

                        }
                    }

                }
            }
            catch (Exception)
            {

                // throw;
            }

        }

        private void BtnEliminarFactura_Click(object sender, EventArgs e)
        {
            if (this.dtDetallesFacturaDataGridView.Rows.Count > 0)
            {
                if (MessageBox.Show(" ¿Eliminar: " + ClasDatos.NombreFactura, " ELIMINAR ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    String Consulta = "DELETE FROM [Dt" + ClasDatos.NombreFactura + "] WHERE EnlaceFactura= @EnlaceFactura ";
                    if (ClsConexionSql.SibaseDatosSql)
                    {

                        ClsConexionSql NuevaConexion = new ClsConexionSql(Consulta);
                        if (NuevaConexion.SiConexionSql)
                        {
                            try
                            {
                                NuevaConexion.ComandoSql.Parameters.AddWithValue("@EnlaceFactura", Convert.ToInt32(this.EnlaceFactu.Text));
                                NuevaConexion.ComandoSql.ExecuteNonQuery();
                                this.dtNuevaFacturaDataGridView.Rows.RemoveAt(this.dtNuevaFacturaDataGridView.CurrentCell.RowIndex);
                                this.dtNuevaFacturaBindingSource.EndEdit();
                                Validate();
                                MessageBox.Show("Eliminado con exito", "ELIMINAR");
                                this.dtNuevaFacturaDataGridView.Refresh();
                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message, "ERROR ELIMINAR FACTURA");
                            }
                            finally
                            {
                                if (NuevaConexion.CerrarConexionSql)
                                {

                                }
                            }
                        }
                    }
                    else
                    {
                        if (File.Exists(ClasDatos.RutaBaseDatosDb))
                        {
                            ClsConexionDb NuevaConexion = new ClsConexionDb(Consulta);
                            if (NuevaConexion.SiConexionDb)
                            {
                                try
                                {
                                    NuevaConexion.ComandoDb.Parameters.AddWithValue("@EnlaceFactura", Convert.ToInt32(this.EnlaceFactu.Text));
                                    NuevaConexion.ComandoDb.ExecuteNonQuery();
                                    this.dtNuevaFacturaDataGridView.Rows.RemoveAt(this.dtNuevaFacturaDataGridView.CurrentCell.RowIndex);
                                    this.dtNuevaFacturaBindingSource.EndEdit();
                                    Validate();
                                    MessageBox.Show("Eliminado con exito", "ELIMINAR");
                                    this.dtNuevaFacturaDataGridView.Refresh();
                                }
                                catch (Exception ex)
                                {

                                    MessageBox.Show(ex.Message, "ERROR ELIMINAR FACTURA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                finally
                                {
                                    if (NuevaConexion.CerrarConexionDB)
                                    {

                                    }
                                }
                            }

                        }
                    }


                }

            }
        }

        private void dtDetallesFacturaDataGridView2_Validated(object sender, EventArgs e)
        {
            try
            {
                if (this.dtDetallesFacturaDataGridView2.RowCount >= 0)
                {


                    if (this.dtDetallesFacturaDataGridView2.CurrentCell.RowIndex >= 0)
                    {

                        if (this.dtDetallesFacturaDataGridView2.CurrentCell.ColumnIndex == 2 || this.dtDetallesFacturaDataGridView2.CurrentCell.ColumnIndex == 4)
                        {
                            int i = this.dtDetallesFacturaDataGridView2.CurrentCell.RowIndex;
                            if (this.dtDetallesFacturaDataGridView2.Rows[i].Cells[4].Value.ToString() == "0" || this.dtDetallesFacturaDataGridView2.Rows[i].Cells[2].Value.ToString() == "0")
                            {
                                this.dtDetallesFacturaDataGridView2.Rows[i].Cells[6].Value = DBNull.Value;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                // throw;
            }
        }
        private void MenuDatagriClick(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                if (this.dtDetallesFacturaDataGridView.SelectedRows.Count > 0)
                {

                    string NombreItem = e.ClickedItem.Name.ToString();
                    // int Fila = this.dtDetallesFacturaDataGridView.CurrentCell.RowIndex;
                    // int I = dtDetallesFacturaDataGridView.CurrentCell.
                    if (NombreItem.Contains("DuplicarLinea"))
                    {
                        NombreItem = NombreItem.Replace("DuplicarLinea", "");
                        //  this.dtDetallesFacturaBindingSource.Insert(1, this.dtDetallesFacturaBindingSource.AddNew());


                        // this.dtDetallesFacturaBindingSource.Insert(Fila, this.dtDetallesFacturaDataGridView.Rows[Fila].Cells[2].Value.ToString());
                        //dtDetallesFacturaDataGridView.Rows.Insert(0, dtDetallesFacturaDataGridView.Rows[Fila].Cells[2].Value );
                        // borrar(int.Parse(id));
                        //  DataTable Dt2 = (DataTable) this.dtDetallesFacturaBindingSource.DataSource;
                        // this.dtDetallesFacturaDataGridView.Rows.Add(Fila, row);
                        // DataRow row = Dt2.NewRow();

                        //  DataTable dt2 = new DataTable();
                        // dt2 = dtDetallesFacturaDataGridView.DataSource as DataTable;

                        DataRow datarow;
                        datarow = this.dsFacturas.DtDetallesFactura.NewRow(); //Con esto le indica que es una nueva fila.
                        datarow["DescripccionDetalle"] = this.NumeroFactura.Text;
                        //datarow["Direccion"] = Direcion.Text;
                        this.dsFacturas.DtDetallesFactura.Rows.InsertAt(datarow, this.FilaGrid);
                        //  this.dtDetallesFacturaBindingSource.Insert(Fila, datarow);
                    }
                    if (NombreItem.Contains("Lineablanco"))
                    {
                        NombreItem = NombreItem.Replace("Lineablanco", "");
                        // Fila = this.dtDetallesFacturaDataGridView.CurrentCell.RowIndex + 1;
                        // DataTable dt = (DataTable)dsFacturas.DtDetallesFactura;
                        // DataTable dt = new DataTable();
                        DataGridViewRow row = this.dtDetallesFacturaDataGridView.CurrentRow;
                        // this.dtDetallesFacturaDataGridView.Rows.Add(Fila, row);
                        this.dtDetallesFacturaBindingSource.Insert(this.FilaGrid + 1, row);
                    }
                    if (NombreItem.Contains("EliminarLinea"))
                    {
                        // NombreItem = NombreItem.Replace("EliminarLinea", "");
                        // Fila = this.dtDetallesFacturaDataGridView.CurrentCell.RowIndex;
                        // dtDetallesFacturaBindingSource.RemoveCurrent();
                        // this.dtDetallesFacturaDataGridView.Rows.RemoveAt(Fila);
                        DataTable dt = this.dtDetallesFacturaDataGridView.DataSource as DataTable;
                        dt.Rows.Add(new object[] { "222" });
                    }
                    if (NombreItem.Contains("DuplicarArticulo"))
                    {
                        NombreItem = NombreItem.Replace("DuplicarArticulo", "");
                        //  Fila = this.dtDetallesFacturaDataGridView.CurrentCell.RowIndex + 1;
                        // var row = this.dsFacturas.DtDetallesFactura.NewRow();
                        // DataTable dt = dtDetallesFacturaDataGridView.DataSource as DataTable;
                        this.dtDetallesFacturaDataGridView.Rows.InsertCopy(this.FilaGrid + 1, this.FilaGrid);
                        //  var row = dt.NewRow();
                        //  row["DescripccionDetalle"] = NumeroFactura.Text;
                        // row["Nombres"] = dgvr.Cells["Nombres"].Value;
                        //resto
                        // dt.Rows.Add(row);

                        //  dtDetallesFacturaDataGridView.DataSource = dt;
                        //this.dtDetallesFacturaBindingSource.Insert(Fila, row);
                        // this.dtDetallesFacturaBindingSource.Insert(Fila, row);
                        if (!string.IsNullOrEmpty(this.dtDetallesFacturaDataGridView.Rows[this.FilaGrid].Cells[3].Value.ToString()))
                        {
                            //  this.dtDetallesFacturaDataGridView.Rows[Fila + 1].Cells[3].Value = this.dtDetallesFacturaDataGridView.Rows[Fila].Cells[3].Value;
                        }

                    }
                    if (NombreItem.Contains("NuevoArticulo"))
                    {
                        this.dtDetallesFacturaBindingSource.AddNew();

                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void dtDetallesFacturaDataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.BtnGuardarFactura.Enabled == true)
            {
                this.FilaGrid = this.dtDetallesFacturaDataGridView.CurrentCell.RowIndex;
                if (e.Button == MouseButtons.Right)
                {
                    ContextMenuStrip menu = new ContextMenuStrip();
                    int posicion = this.dtDetallesFacturaDataGridView.HitTest(e.X, e.Y).RowIndex;
                    if (posicion > -1)
                    {
                        menu.Items.Add("Duplicar Linea").Name = "DuplicarLinea" + posicion;
                        menu.Items.Add("nueva Linea en Blanco").Name = "Lineablanco" + posicion;
                        menu.Items.Add("Eliminar Linea").Name = "EliminarLinea" + posicion;
                        menu.Items.Add("Duplicar Esta Linea").Name = "DuplicarArticulo" + posicion;
                        menu.Items.Add("Nuevo Articulo").Name = "NuevoArticulo" + posicion;
                    }
                    menu.Show(this.dtDetallesFacturaDataGridView, e.X, e.Y);
                    menu.ItemClicked += new ToolStripItemClickedEventHandler(MenuDatagriClick);
                }
            }
        }
        public Boolean email_bien_escritoFactu()
        {
            String expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(this.DniTextBox.Text, expresion))
            {
                if (Regex.Replace(this.DniTextBox.Text, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        private void dniTextBox_Validating(object sender, CancelEventArgs e)
        {
            ClasValidarDni.ValidarDni(this.DniTextBox.Text);
        }

        private void ejerciciosDeAñoComboBox_Click(object sender, EventArgs e)
        {

        }

        private void SerieText_Click(object sender, EventArgs e)
        {
            // this.PanelArriba.Tag = "SI";
        }

        private void tipoInpuestoIVALabel1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void EmpresaPrincipal_SelectedIndexChanged(object sender, EventArgs e)
        {
            // FiltrarFactura();

        }

        private void EmpresaPrincipal_Enter(object sender, EventArgs e)
        {
            // this.PanelArriba.Tag = "SI";
        }

        private void NombreEmpresaReguistro_TextChanged(object sender, EventArgs e)
        {
            FiltrarFactura();
            if (this.PanelArriba.Tag.ToString() == "SI")
            {


                if (ClsConexionSql.SibaseDatosSql)
                {
                    ActualizarFacturaSql();
                }
                else
                {
                    ActualizarFaturas_DB();
                }
            }
        }

        private void EmpresaPrincipal_MouseEnter(object sender, EventArgs e)
        {
            this.Id_Empresa.Width = 246;

        }

        private void EmpresaPrincipal_SelectedValueChanged(object sender, EventArgs e)
        {
            // FiltrarFactura();
        }

        private void EmpresaPrincipal_MouseLeave(object sender, EventArgs e)
        {
            this.Id_Empresa.Width = 0;
        }

        private void ejerciciosDeAñoComboBox_Click_1(object sender, EventArgs e)
        {
            // this.PanelArriba.Tag = "SI";
        }

        private void EmpresaPrincipal_Click(object sender, EventArgs e)
        {
            //this.PanelArriba.Tag = "SI";
        }


        private void AlmacenTxt_Click(object sender, EventArgs e)
        {
            this.AlmacenTxt.DroppedDown = true;
        }

        private void dtDetallesFacturaDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void PanelArriba_MouseEnter(object sender, EventArgs e)
        {
            this.Id_Empresa.Width = 246;
        }

        private void PanelArriba_MouseLeave(object sender, EventArgs e)
        {
            this.Id_Empresa.Width = 0;
        }
        public void AñadirIdProvinciaFatu()
        {
            int ii = 0;
            foreach (var fila in FormMenuPrincipal.menu2principal.dsMulti2.DtProvincias)
            {
                fila["IdFila"] = ii.ToString();
                ii++;
            }

        }
        private void BtnBuscarProvi_Click(object sender, EventArgs e)
        {
            if (this.fKDtPaisesDtProvinciasBindingSource.Count > 0)
            {
                int Id2 = this.PaisFatuTxt.SelectedIndex;
                int IdPais = 1;
                if (!String.IsNullOrEmpty(FormMenuPrincipal.menu2principal.dsMulti2.Tables["DtPaises"].Rows[Id2]["Id"].ToString()))
                {
                    IdPais = Convert.ToInt32(FormMenuPrincipal.menu2principal.dsMulti2.Tables["DtPaises"].Rows[Id2]["Id"].ToString());
                }
                ClasDatos.OkFacturar = true;
                ClasDatos.QUEform = "Provincias";
                AñadirIdProvinciaFatu();
                FormBuscar frm = new FormBuscar();
                frm.CargarDatos(1, "Provincias", "Provincias", "IdEnlace", IdPais);
                frm.BringToFront();
                frm.ShowDialog();

            }
        }
        public void AñadirIdPaisFatu()
        {
            int ii = 0;
            foreach (var fila in FormMenuPrincipal.menu2principal.dsMulti2.DtPaises)
            {
                fila["IdFila"] = ii.ToString();
                ii++;
            }

        }
        private void BtnBuscarPais_Click(object sender, EventArgs e)
        {
            if (this.dtPaisesBindingSource.Count > 0)
            {
                int Id2 = this.PaisFatuTxt.SelectedIndex;
                int IdPais = 1;
                if (!String.IsNullOrEmpty(FormMenuPrincipal.menu2principal.dsMulti2.Tables["DtPaises"].Rows[Id2]["Id"].ToString()))
                {
                    IdPais = Convert.ToInt32(FormMenuPrincipal.menu2principal.dsMulti2.Tables["DtPaises"].Rows[Id2]["Id"].ToString());
                }
                AñadirIdPaisFatu();
                ClasDatos.OkFacturar = true;
                ClasDatos.QUEform = "Paises";
                FormBuscar frm = new FormBuscar();
                frm.CargarDatos(1, " Paises", "Paises", "Paises", IdPais);
                frm.BringToFront();
                frm.ShowDialog();

            }
        }

        private void TarifaRealTxt_TextChanged(object sender, EventArgs e)
        {
            if (this.CheckDescuentos.Checked)
            {


                if (this.TarifaRealTxt.Text.ToString() == "PVP1" | this.TarifaRealTxt.Text.ToString() == "PVPIVA" | this.TarifaRealTxt.Text.ToString() == "PLUS" | this.TarifaRealTxt.Text.ToString() == "IVA")
                {
                    this.CheckDescuentos.Checked = false;
                    MessageBox.Show("Tarifa No Valida", "ELEGIR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void TarifaRealTxt_Click(object sender, EventArgs e)
        {

        }

        private void CheckDescuentos_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Btn_Iva_Click(object sender, EventArgs e)
        {
            if (this.dtDetallesFacturaBindingSource.Count > 0)
            {
                string InfoMas = "Sumar Iva A Precios";
                if (this.MasMenosTxt.Text == "-")
                {
                    InfoMas = "Restar Iva A Precios";
                }
                else
                {
                    InfoMas = "Sumar Iva A Precios";
                }
                if (MessageBox.Show(InfoMas, " MODIFICAR ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    try
                    {
                        bool SiSalto = false;
                        double TTotalSuma = 0;
                        int I = this.dtDetallesFacturaDataGridView.CurrentCell.RowIndex;
                        foreach (DataGridViewRow Fila in this.dtDetallesFacturaDataGridView.Rows)
                        {
                            if (SiSalto == false)
                            {
                                if (this.dtDetallesFacturaDataGridView.AllowUserToAddRows == true)
                                {
                                    if (Fila.Index == this.dtDetallesFacturaDataGridView.RowCount)
                                    {
                                        goto saltoAbajo;
                                    }
                                }
                                dtDetallesFacturaDataGridView.CurrentCell = dtDetallesFacturaDataGridView.Rows[Fila.Index].Cells[4];
                                // dtDetallesFacturaDataGridView.BeginEdit(true);
                                // dtDetallesFacturaDataGridView.Rows[Fila.Index].DefaultCellStyle.BackColor = Color.Red;
                                dtDetallesFacturaDataGridView.Rows[Fila.Index].Cells[4].Style.BackColor = Color.Red;
                                Application.DoEvents();
                                if (MessageBox.Show("Modificar >>" + Fila.Cells[4].FormattedValue.ToString(), " MODIFICAR ", MessageBoxButtons.YesNoCancel) == DialogResult.Cancel)
                                {
                       
                                    SiSalto = true;
                                }
                            }
               
                            if (Fila.Cells[4].Value.ToString() != string.Empty && Fila.Cells[4].Value != DBNull.Value && Fila.Cells[4].Value.ToString() != null)
                            {
     
                                TTotalSuma = (Double)Fila.Cells[4].Value;
                                if (this.MasMenosTxt.Text == "+")
                                {
                                    TTotalSuma = TTotalSuma + (TTotalSuma * Convert.ToInt32(this.IvaFactuTxt.Value) / 100);
                                }
                                else
                                {
                                    TTotalSuma = TTotalSuma - (TTotalSuma * Convert.ToInt32(this.IvaFactuTxt.Value) / 100);
                                }
                                dtDetallesFacturaDataGridView.Rows[Fila.Index].Cells[4].Style.BackColor = SystemColors.Window;
                                Fila.Cells[4].Value = TTotalSuma.ToString();
                            }
                            saltoAbajo:
                            CalcularTotales(this.dtDetallesFacturaDataGridView);

                        }
                    }
                    catch (Exception ex)
                    {


                        MessageBox.Show(ex.Message, "ERROR AL LISTAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        int II = this.dtDetallesFacturaDataGridView.CurrentCell.RowIndex;
                        dtDetallesFacturaDataGridView.Rows[II].Cells[4].Style.BackColor = SystemColors.Control;
                        // dtDetallesFacturaDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.Beige;
                    }
                }
            }
        }

        private void MasMenosTxt_Click(object sender, EventArgs e)
        {
            if (this.MasMenosTxt.Text == "+")
            {
                this.MasMenosTxt.Text = "-";
            }
            else
            {
                this.MasMenosTxt.Text = "+";
            }
        }

        private void NumeroFactura_TextChanged(object sender, EventArgs e)
        {

        }
    }
}








