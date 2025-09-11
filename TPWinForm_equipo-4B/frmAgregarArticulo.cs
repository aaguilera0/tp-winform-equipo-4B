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
        List<String> listUrlImg = new List<String>();
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
            ImagenNegocio negocioImg = new negocio.ImagenNegocio();
            int idArticulo = 0;

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
                    idArticulo = negocio.ObtenerIdArticulo(articulo);
                    negocioImg.Agregar(listUrlImg, idArticulo);
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

        private void btSavedImaged_Click(object sender, EventArgs e)
        {

            try {
                if (string.IsNullOrEmpty(txtUrlImagen.Text)) {
                    MessageBox.Show("Ingrese url de imagen para guardar");
                }
                else {

                    listUrlImg.Add(txtUrlImagen.Text);                                     
                    cargarDGV();
                    txtUrlImagen.Clear();

                }                    

            }catch (Exception ex){

                throw ex;

            }
        }

        private void cargarDGV()
        {
            int ultimoIndiceList = listUrlImg.Count - 1;
            dgvArticulo.DataSource = null;
            dgvArticulo.DataSource = listUrlImg;
            dgvArticulo.ClearSelection();
            dgvArticulo.Rows[ultimoIndiceList].Selected = true;
            dgvArticulo.CurrentCell = dgvArticulo.Rows[ultimoIndiceList].Cells[0];
            dgvArticulo.FirstDisplayedScrollingRowIndex = ultimoIndiceList;
            cargarImagen(listUrlImg[ultimoIndiceList]);
        }

        private void cargarImagen(String imagen)
        {
            try
            {
                pbArticulo.Load(imagen);
            }
            catch (Exception)
            {                
                pbArticulo.Load("https://e1.pngegg.com/pngimages/50/931/png-clipart-through-the-ages-empty-street-thumbnail.png");
            }
        }

        private void btDeletedImage_Click(object sender, EventArgs e)
        {
            try {

                if (listUrlImg.Count > 0) {

                    string urlImgBorrar = (string)dgvArticulo.CurrentRow.DataBoundItem; // o el índice correcto de columna
                    int auxInd = listUrlImg.IndexOf(urlImgBorrar);
                    listUrlImg.RemoveAt(auxInd);
                    cargarDGV();

                }
                else {

                    MessageBox.Show("No hay imagen para borrar");

                }

            } catch (Exception ex) {

                MessageBox.Show("Error al borrar la imagen: " + ex.Message);

            }
        }

        private void dgvArticulo_SelectionChanged(object sender, EventArgs e)
        {
            string urlImg = (string)dgvArticulo.CurrentRow.DataBoundItem;
            cargarImagen(urlImg);
        }
    }
}
