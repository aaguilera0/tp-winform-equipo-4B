using dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using negocio;
namespace TPWinForm_equipo_4B
{
    public partial class Form1 : Form
    {
        private List<Articulo> listaArticulo;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            listaArticulo= negocio.Listar();
            dgvArticulo.DataSource = listaArticulo;
            cargarImagen(listaArticulo[0].imagen.ImagenUrl);
            //dgvArticulo.Columns["IdCategoria"].Visible = false;
            //dgvArticulo.Columns["IdMarca"].Visible = false;
            dgvArticulo.Columns["imagen"].Visible = false;

        }

        private void dgvArticulo_SelectionChanged(object sender, EventArgs e)
        {

            Articulo Selecionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
            cargarImagen(Selecionado.imagen.ImagenUrl);
        }

        private void cargarImagen(String imagen)
        {
            try
            {
                pcbArticulo.Load(imagen);
            }
            catch (Exception)
            {

                pcbArticulo.Load("https://e1.pngegg.com/pngimages/50/931/png-clipart-through-the-ages-empty-street-thumbnail.png");
            }
        }

        private void bttAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarArticulo alta = new frmAgregarArticulo();
            alta.ShowDialog();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
            frmAgregarArticulo modificar = new frmAgregarArticulo(seleccionado);
            modificar.ShowDialog();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            Articulo seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("CONFIRMAR ELIMINACION", "Eliminado", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                { 
                    seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
                    negocio.Eliminar(seleccionado.Id);
                    MessageBox.Show("Eliminado exitosamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }

        
    }
}


