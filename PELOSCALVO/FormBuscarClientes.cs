﻿using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace PELOSCALVO
{
    public partial class FormBuscarClientes : Form
    {
        DataView view2 = default;
        public FormBuscarClientes()
        {
            InitializeComponent();

        }
        public void AñadirId()
        {
            int ii = 0;
            foreach (DataGridViewRow fila in this.dtClientes2DataGridView.Rows)
            {
                fila.Cells["IdFila"].Value = ii.ToString();
                ii++;
            }

        }
        private void FormBuscarClientes_Load(object sender, EventArgs e)
        {
  
 
                if (FormMenuPrincipal.menu2principal.dsClientes != null)
                {
                    dtClientesBindingSource.DataSource = FormMenuPrincipal.menu2principal.dsClientes;
                this.view2 = FormMenuPrincipal.menu2principal.articulos.DtArticulos.DefaultView;
            }

            AñadirId();
            this.ContadorDatos.Text = string.Format("{0:N0" + "}", (Convert.ToInt32(this.dtClientes2DataGridView.Rows.Count).ToString()));

        }

        private void BuscarClientesText_TextChanged(object sender, EventArgs e)
        {
            int NumeroValidar = 1;

            if (this.TIPObuscarCLIENTESfoma.Text == "Razon Social")
            {
                NumeroValidar = 1;
            }
            if (this.TIPObuscarCLIENTESfoma.Text == "Nombre")
            {
                NumeroValidar = 2;
            }
            if (this.TIPObuscarCLIENTESfoma.Text == "Direccion")
            {
                NumeroValidar = 3;
            }
            if (this.TIPObuscarCLIENTESfoma.Text == "Dni")
            {
                NumeroValidar = 7;
            }
            if (this.TIPObuscarCLIENTESfoma.Text == "Telefono")
            {
                NumeroValidar = 4;
            }
            if (this.TIPObuscarCLIENTESfoma.Text == "Movil")
            {
                NumeroValidar = 5;
            }

            string fieldName = string.Concat("[", this.dsClientes.DtClientes.Columns[NumeroValidar].ColumnName, "]");
            this.dsClientes.DtClientes.DefaultView.Sort = fieldName;

            view2.RowFilter = string.Empty;
            view2.RowFilter = fieldName + " LIKE '%" + this.BuscarClientesText.Text + "%'";
            this.dtClientes2DataGridView.DataSource = view2;
            if (this.TIPObuscarCLIENTESfoma.Text == "Todos")
            {
                string fieldName2 = string.Concat("[", this.dsClientes.DtClientes.Columns[1].ColumnName, "]");
                string fieldName3 = string.Concat("[", this.dsClientes.DtClientes.Columns[2].ColumnName, "]");
                string fieldName4 = string.Concat("[", this.dsClientes.DtClientes.Columns[3].ColumnName, "]");
                string sumarfield = fieldName2 + fieldName3 + fieldName4;
                // dsClientes.DtClientes.DefaultView.Sort = sumarfield;
                this.dsClientes.DtClientes.DefaultView.Sort = fieldName2;
                view2.RowFilter = string.Empty;
                if (this.BuscarClientesText.Text != string.Empty)
                    // string A == fieldName2 + " LIKE '%" + BuscarClientesText.Text + "%'";
                    view2.RowFilter = fieldName2 + " LIKE '%" + this.BuscarClientesText.Text + "%'";
                // view2.RowFilter += fieldName3 + " LIKE '%" + BuscarClientesText.Text + "%'";
                //view2.RowFilter = fieldName3 + view2.RowFilter;
                this.dtClientes2DataGridView.DataSource = view2;
                //return;
            }

            this.ContadorDatos.Text = string.Format("{0:N0" + "}", (Convert.ToInt32(this.dtClientes2DataGridView.Rows.Count).ToString()));
        }

        private void TIPObuscarCLIENTESfoma_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView view = this.dsClientes.DtClientes.DefaultView;
            view.RowFilter = string.Empty;
            this.BuscarClientesText.Text = "";
        }

        private void FormBuscarClientes_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void BuscarClientesText_Click(object sender, EventArgs e)
        {


        }


        private void BtnCancelarBCliente_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dtClientes2DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (ClasDatos.OkFacturar == false)
                {
                    ClasDatos.ValorBuscado = Convert.ToInt32(this.dtClientes2DataGridView.Rows[e.RowIndex].Cells["IdFila"].Value);
                    if (ClasDatos.ValorBuscado >= 0)
                    {

                        // FormClientes.menu2CLIENTES2.dtClientesDataGridView.Rows[ClasDatos.ValorBuscado].Selected = true;
                       // FormClientes.menu2CLIENTES2.dtClientesDataGridView.CurrentCell = dtClientes2DataGridView.Rows[ClasDatos.ValorBuscado].Cells[0];
                        FormClientes.menu2CLIENTES2.dtClientesDataGridView.CurrentCell = dtClientes2DataGridView.Rows[ClasDatos.ValorBuscado].Cells[0];
                        FormClientes.menu2CLIENTES2.dtClientesDataGridView.CurrentCell.Selected = true;
                    }
                }

                if (ClasDatos.OkFacturar == true)
                {

                    ClasDatos.ValorBuscado = Convert.ToInt32(e.RowIndex);

                    if (ClasDatos.ValorBuscado >= 0)
                    {
                        if (this.dtClientes2DataGridView.Rows[ClasDatos.ValorBuscado].Cells[1].Value != null)
                        {
                            FormFacturar.menu2FACTURAR.apodoTextBox.Text = this.dtClientes2DataGridView.Rows[ClasDatos.ValorBuscado].Cells[1].FormattedValue.ToString();
                        }
                        else
                        {
                            FormFacturar.menu2FACTURAR.apodoTextBox.Text = "";
                            MessageBox.Show("No Hay Ningun Cliente", "NO EXISTE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        FormFacturar.menu2FACTURAR.nombreTextBox.Text = this.dtClientes2DataGridView.Rows[ClasDatos.ValorBuscado].Cells[2].FormattedValue.ToString();
                        FormFacturar.menu2FACTURAR.direccionTextBox.Text = this.dtClientes2DataGridView.Rows[ClasDatos.ValorBuscado].Cells[3].FormattedValue.ToString();
                        FormFacturar.menu2FACTURAR.localidadTextBox.Text = this.dtClientes2DataGridView.Rows[ClasDatos.ValorBuscado].Cells[8].FormattedValue.ToString();
                        FormFacturar.menu2FACTURAR.calleTextBox.Text = this.dtClientes2DataGridView.Rows[ClasDatos.ValorBuscado].Cells[13].FormattedValue.ToString();
                        // FormFACTURAR.menu2FACTURAR.direccionTextBox.Text = dtClientes2DataGridView.Rows[ClasDatos.ValorBuscado].Cells[3].FormattedValue.ToString();
                        FormFacturar.menu2FACTURAR.provinciaComboBox.Text = this.dtClientes2DataGridView.Rows[ClasDatos.ValorBuscado].Cells[15].FormattedValue.ToString();
                        FormFacturar.menu2FACTURAR.numeroCalleTextBox.Text = this.dtClientes2DataGridView.Rows[ClasDatos.ValorBuscado].Cells[14].FormattedValue.ToString();
                        FormFacturar.menu2FACTURAR.dniTextBox.Text = this.dtClientes2DataGridView.Rows[ClasDatos.ValorBuscado].Cells[7].FormattedValue.ToString();
                        FormFacturar.menu2FACTURAR.pais_FactComboBox.Text = this.dtClientes2DataGridView.Rows[ClasDatos.ValorBuscado].Cells[11].FormattedValue.ToString();
                        FormFacturar.menu2FACTURAR.apodoTextBox.Select();
                        FormFacturar.menu2FACTURAR.nombreTextBox.Select();
                        FormFacturar.menu2FACTURAR.direccionTextBox.Select();
                        FormFacturar.menu2FACTURAR.localidadTextBox.Select();
                        FormFacturar.menu2FACTURAR.provinciaComboBox.Select();
                        FormFacturar.menu2FACTURAR.calleTextBox.Select();
                        FormFacturar.menu2FACTURAR.nombreTextBox.Focus();
                    }
                }

            }

            Close();
            // ClaseCompartida.ValorBuscado= e.RowIndex;
        }
    }
}