using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace TPWinForm_equipo_4B
{
    public partial class frmAgregarArticulo : Form
    {
        private Articulo articulo = null;
        public frmAgregarArticulo()
        {
            InitializeComponent();
        }

        public frmAgregarArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar Articulo";
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {   
                if (articulo == null) articulo = new Articulo();

                articulo.Codigo = txtCodigoArticulo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.IdMarca = (Marca)cbMarca.SelectedItem;
                articulo.IdCategoria = (Categoria)cbCategoria.SelectedItem;
                articulo.Precio = decimal.Parse(txtPrecio.Text);

                if (articulo.Id != 0){

                    negocio.Modificar(articulo);
                    MessageBox.Show("Modificado exitosamente.");

                }
                else{

                    negocio.Agregar(articulo);
                    MessageBox.Show("Agregado exitosamente.");

                }

                
                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void frmAgregarArticulo_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();

            try
            {
                cbCategoria.DataSource = categoriaNegocio.listar();
                cbCategoria.ValueMember = "id";
                cbCategoria.DisplayMember = "Descripcion";
                cbMarca.DataSource = marcaNegocio.listar();
                cbMarca.ValueMember = "id";
                cbMarca.DisplayMember = "Descripcion";

                if (articulo != null)
                {
                    txtNombre.Text = articulo.Nombre;
                    txtCodigoArticulo.Text = articulo.Codigo;
                    txtDescripcion.Text = articulo.Descripcion;
                    cbMarca.SelectedValue = articulo.IdMarca;
                    cbCategoria.SelectedValue= articulo.IdCategoria;
                    txtPrecio.Text = articulo.Precio.ToString();
                    cbMarca.SelectedValue = articulo.IdMarca.Id;
                    cbCategoria.SelectedValue = articulo.IdCategoria.Id;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
