using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;
using WebService_WebAPI.Models;

namespace WebApi
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarCategorias();
            DeshabilitarCampos();
        }

        private void DeshabilitarCampos()
        {
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
        }
        private void LimpiarCampos()
        {
            txtId.Text = "";
            txtLong.Text = "";
            txtShort.Text = "";
        }
        private void EliminarCategorias()
        {
            // Configurar la url para realizar la petición HTTP
            string url = "https://localhost:44325/api/Categories/"+txtId.Text;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync(client.BaseAddress);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    CargarCategorias();
                   
                    ShowMessage("Se elimino", MessageType.Success);
                }
                else
                {
                    ShowMessage("Intentelo mas tarde..", MessageType.Error);
                }
            }
        }

        private void CargarCategorias()
        {
            // Configurar la url para realizar la petición HTTP
            string url = "https://localhost:44325/api/Categories/";
            HttpWebRequest solicitud = (HttpWebRequest)WebRequest.Create(url);
            solicitud.Method = "GET";
            solicitud.ContentType = "text/xml; encoding='utf-8'";
            // Enviar la solicitud, obtener la respuesta con los datos en formato XML
            // y convertir el XML a un objeto de tipo: Stream
            WebResponse response = solicitud.GetResponse();
            Stream stream = response.GetResponseStream();
            // Leer los datos de tipo: Stream con el método ReadXml(), y pasar
            // los datos a un DataSet para vincularlos a un GridView
            DataSet ds = new DataSet();
            ds.ReadXml(stream);
            // Vincular los datos a un GridView
            gvCategories.DataSource = ds.Tables[0];
            gvCategories.DataBind();
        }

        protected void gvCategories_PreRender(object sender, EventArgs e)
        {
            gvCategories.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void gvCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            HabilitarCampos();
            //btnInsertar.Attributes.Add("readonly", "readonly");
            GridViewRow row = gvCategories.SelectedRow;
            txtId.Text = row.Cells[1].Text;
            txtShort.Text = row.Cells[3].Text;
            txtLong.Text = row.Cells[2].Text;
            DeshabilitarInsertarID();
        }

        private void HabilitarCampos()
        {
            btnActualizar.Enabled = true;
            btnEliminar.Enabled = true;
        }

        private void DeshabilitarInsertarID()
        {
            txtId.ReadOnly = true;
            btnInsertar.Enabled = false;
        }

        private void HabilitarInsertarID()
        {
            txtId.ReadOnly = false;
            btnInsertar.Enabled = true;
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            HabilitarInsertarID();
            DeshabilitarCampos();
            LimpiarCampos();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarCategorias();
            HabilitarInsertarID();
            DeshabilitarCampos();
            LimpiarCampos();
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarCategoria();
            HabilitarInsertarID();
            DeshabilitarCampos();
            LimpiarCampos();
        }

        private void ActualizarCategoria()
        {
            if (ValidarCampos())
            {
                Category cat = new Category();
                cat.CategoryID = txtId.Text;
                cat.ShortName = txtShort.Text;
                cat.LongName = txtLong.Text;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44325/api/Categories/" + txtId.Text);
                    //HTTP POST
                    var putTask = client.PutAsJsonAsync<Category>(client.BaseAddress, cat);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ShowMessage("Se Modifico", MessageType.Success);
                        CargarCategorias();
                    }
                    else
                    {
                        ShowMessage("Hubo un error...", MessageType.Error);
                    }
                }
            }
            
        }

        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            InsertarCategoria();
        }

        public enum MessageType { Success, Error, Info, Warning };

        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }

        private bool ValidarCampos() {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                ShowMessage("Ingrese el Id de la Categoria", MessageType.Error);
                return false;
            }
            else
            {
                if (string.IsNullOrEmpty(txtShort.Text))
                {
                    ShowMessage("Ingrese el nombre corto de la Categoria", MessageType.Error);
                    return false;
                }
                else
                {
                    if (string.IsNullOrEmpty(txtLong.Text))
                    {
                        ShowMessage("Ingrese el nombre largo de la Categoria", MessageType.Error);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                    
                }
            }
        }

        private void InsertarCategoria()
        {
            if (ValidarCampos())
            {
                Category cat = new Category();
                cat.CategoryID = txtId.Text;
                cat.ShortName = txtShort.Text;
                cat.LongName = txtLong.Text;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44325/api/Categories/");
                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<Category>("Categories", cat);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ShowMessage("Se inserto!!!", MessageType.Success);
                        CargarCategorias();
                        LimpiarCampos();
                        HabilitarInsertarID();
                        //return RedirectToAction("Index");
                    }
                }
            }

        }
    }
}
