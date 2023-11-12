﻿using System;
using System.IO;
using System.Windows.Forms;

namespace PELOSCALVO
{
    public partial class FormCorreos : Form
    {
        string Rutacorreos = Directory.GetCurrentDirectory() + "\\" + ClasDatos.RutaDatosPrincipal + "\\" + "correos.Xml";
        public FormCorreos()
        {
            InitializeComponent();
        }

        private void FormCorreos_Load(object sender, EventArgs e)
        {
            try
            {
                if (FormMenuPrincipal.menu2principal.dsCorreos != null)
                {
                    this.DatagridCorreosEmpresa.DataSource = FormMenuPrincipal.menu2principal.dsCorreos;
                    this.DataGridCorreoCliente.DataSource = FormMenuPrincipal.menu2principal.dsCorreos;
                    //  if(DatagriCorreosEmpresa.RowCount<= 0)
                    //{
                    //    DataGridCorreoCliente.Rows[0].Cells[0].Value=""
                    // }
                }
            }
            catch (Exception)
            {

               // throw;
            }
        }
        private bool EspacioDiscosCorreo_EMP(string nombreDisco, int Espacio)
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
        private void BtnGuardarCorreo_EMP_Click(object sender, EventArgs e)
        {
            if (EspacioDiscosCorreo_EMP(ClasDatos.RutaConfiguracionXml, 30))
            {
                try
                {
                    if (File.Exists(this.Rutacorreos))
                    {
                        this.DatagridCorreosEmpresa.EndEdit();
                        Validate();

                        FormMenuPrincipal.menu2principal.dsCorreos.WriteXml(this.Rutacorreos);
                        MessageBox.Show("Se Actualizo Con Exito", "correos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No Se Guardo", "FALTAN ARCHIVOS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void DatagriCorreosEmpresa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string Correo = this.DatagridCorreosEmpresa.Rows[e.RowIndex].Cells[0].Value.ToString();
                try
                {
                    if (this.DatagridCorreosEmpresa.Rows[e.RowIndex].Cells[0].Value.ToString() != string.Empty)
                    {
                        Correo = this.DatagridCorreosEmpresa.Rows[e.RowIndex].Cells[0].Value.ToString();
                    }

                    if (e.ColumnIndex == 7)
                    {
                        if (e.RowIndex < this.DatagridCorreosEmpresa.RowCount - 1)
                        {
                            if (MessageBox.Show("Desea Eliminar Este Correo ?? " + "\n" + "\n" + Correo, "ELIMINAR ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                this.DatagridCorreosEmpresa.Rows.Remove(this.DatagridCorreosEmpresa.CurrentRow);
                                this.DatagridCorreosEmpresa.Refresh();
                                if (File.Exists(this.Rutacorreos))
                                {
                                    FormMenuPrincipal.menu2principal.dsCorreos.WriteXml(this.Rutacorreos);
                                }
                                MessageBox.Show(Correo + "\n" + "\n" + "Eliminado Con Exito ", "ELIMINAR ", MessageBoxButtons.OK);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void DataGridCorreoCliente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    string Correo = this.DatagridCorreosEmpresa.Rows[e.RowIndex].Cells[1].Value.ToString();
                    if (this.DatagridCorreosEmpresa.Rows[e.RowIndex].Cells[1].Value.ToString() != string.Empty)
                    {
                        Correo = this.DatagridCorreosEmpresa.Rows[e.RowIndex].Cells[1].Value.ToString();
                    }


                    if (e.ColumnIndex == 7)
                    {
                        if (e.RowIndex < this.DatagridCorreosEmpresa.RowCount - 1)
                        {
                            if (MessageBox.Show("Desea Eliminar Este Correo ?? " + "\n" + "\n" + Correo, "ELIMINAR ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                this.DatagridCorreosEmpresa.Rows.Remove(this.DatagridCorreosEmpresa.CurrentRow);
                                this.DatagridCorreosEmpresa.Refresh();
                                if (File.Exists(this.Rutacorreos))
                                {
                                    FormMenuPrincipal.menu2principal.dsCorreos.WriteXml(this.Rutacorreos);
                                }
                                MessageBox.Show(Correo + "\n" + "\n" + "Eliminado Con Exito ", "ELIMINAR ", MessageBoxButtons.OK);
                            }
                        }

                    }
                }


                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnGuardarCorreoCli_Click(object sender, EventArgs e)
        {
            if (EspacioDiscosCorreo_EMP(ClasDatos.RutaConfiguracionXml, 30))
            {
                if (File.Exists(ClasDatos.RutaMulti2))
                {
                    this.DataGridCorreoCliente.EndEdit();
                    Validate();
                    FormMenuPrincipal.menu2principal.dsCorreos.WriteXml(Rutacorreos);
                    MessageBox.Show("Se Actualizo Con Exito", "CORREO EMPRESA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No Se Guardo", "FALTAN ARCHIVOS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
    }
}



