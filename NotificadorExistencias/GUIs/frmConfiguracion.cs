using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using NotificadorExistencias.Negocio;

namespace NotificadorExistencias.GUIs
{
    public partial class frmConfiguracion : Form
    {
        IConsultasNegocio _consultasNegocio;
        private string _path = string.Empty;

        public frmConfiguracion()
        {
            InitializeComponent();
        }

        private void frmConfiguracion_Load(object sender, EventArgs e)
        {
            try
            {
                string fileName = "config.dat";
                string pathConfigFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\NotifExis\";

                // si no existe el directorio, lo crea
                bool exists = System.IO.Directory.Exists(pathConfigFile);

                if (!exists) System.IO.Directory.CreateDirectory(pathConfigFile);

                // busca en el directorio si exite el archivo con el nombre dado
                var file = Directory.GetFiles(pathConfigFile, fileName, SearchOption.AllDirectories)
                        .FirstOrDefault();

                this._path = pathConfigFile + fileName;

                if (file != null)
                {
                    // si existe
                    // cargar los datos en los campos
                    FEncrypt.Respuesta result = FEncrypt.EncryptDncrypt.DecryptFile(this._path, "milagros");

                    if (result.status == FEncrypt.Estatus.ERROR)
                        throw new Exception(result.error);

                    if (result.status == FEncrypt.Estatus.OK)
                    {
                        string[] list = result.resultado.Split(new string[] { "||" }, StringSplitOptions.None);

                        this.tbServidor.Text = list[0].Substring(2);                                    // servidor
                        this.tbBaseDeDatos.Text = list[1].Substring(2);                                 // base de datos
                        this.cmbAutenticacion.SelectedItem = list[2].Substring(2);                      // tipo del autenticacion
                        this.tbUsuario.Text = list[3].Substring(2);                                     // usuario
                        this.tbContrasenia.Text = list[4].Substring(2);                                 // contraseña
                        this.cbIniciarApp.Checked = list[5].Substring(2).Equals("true") ? true : false; // inicia la app
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // validaciones
                if (string.IsNullOrEmpty(this.tbServidor.Text))
                    throw new Exception("Llene el campo Servidor");

                if (string.IsNullOrEmpty(this.tbBaseDeDatos.Text))
                    throw new Exception("Llene el campo Base de Datos");

                if (this.cmbAutenticacion.SelectedIndex == -1)
                    throw new Exception("Seleccione el tipo Autenticación");

                string tipo = (string)this.cmbAutenticacion.SelectedItem;

                if (tipo.ToLower().Equals("sql server"))
                {

                    if (string.IsNullOrEmpty(this.tbUsuario.Text))
                        throw new Exception("Llene el campo Usuario");

                    if (string.IsNullOrEmpty(this.tbServidor.Text))
                        throw new Exception("Llene el campo Contraseña");
                }

                // define texto del archivo
                string cadena = string.Empty;

                cadena += "S_" + this.tbServidor.Text + "||";
                cadena += "B_" + this.tbBaseDeDatos.Text + "||";
                cadena += "A_" + this.cmbAutenticacion.SelectedItem + "||";
                cadena += "U_" + this.tbUsuario.Text + "||";
                cadena += "C_" + this.tbContrasenia.Text + "||";
                cadena += "I_" + (this.cbIniciarApp.Checked ? "true" : "false");

                // prosigue con la creación del archivo
                FEncrypt.Respuesta result = FEncrypt.EncryptDncrypt.EncryptFile("milagros", cadena, this._path);

                if (result.status == FEncrypt.Estatus.ERROR)
                    throw new Exception(result.error);

                if (result.status == FEncrypt.Estatus.OK)
                {
                    string tipoAu = (string)this.cmbAutenticacion.SelectedItem;

                    if (tipoAu.ToLower().Equals("windows"))
                    {
                        Modelos.ConectionString.conn = string.Format(
                        "Data Source={0};Initial Catalog={1};Integrated Security=True;",
                        this.tbServidor.Text,
                        this.tbBaseDeDatos.Text);
                    }

                    if (tipoAu.ToLower().Equals("sql server"))
                    {
                        Modelos.ConectionString.conn = string.Format(
                        "Data Source={0};database={1};User Id={2};password={3};Trusted_Connection=yes;",
                        this.tbServidor.Text,
                        this.tbBaseDeDatos.Text,
                        this.tbUsuario.Text,
                        this.tbContrasenia.Text);
                    }

                    this._bloqueo(tipoAu);

                    MessageBox.Show("Se cargó correctamente la información", "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = System.Windows.Forms.DialogResult.OK;

                    this.Close();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnTestConn_Click(object sender, EventArgs e)
        {
            try
            {
                string tipoAu = (string)this.cmbAutenticacion.SelectedItem;

                Modelos.ConectionString.conn = string.Empty;

                if (tipoAu.ToLower().Equals("windows"))
                {
                    Modelos.ConectionString.conn = string.Format(
                    "Data Source={0};Initial Catalog={1};Integrated Security=True;",
                    this.tbServidor.Text,
                    this.tbBaseDeDatos.Text);
                }

                if (tipoAu.ToLower().Equals("sql server"))
                {
                    Modelos.ConectionString.conn = string.Format(
                    "Data Source={0};database={1};User Id={2};password={3};",
                    this.tbServidor.Text,
                    this.tbBaseDeDatos.Text,
                    this.tbUsuario.Text,
                    this.tbContrasenia.Text);
                }

                this._consultasNegocio = new ConsultasNegocio();

                bool result = this._consultasNegocio.pruebaConn();

                if (!result)
                    throw new Exception("Error al conectar");

                MessageBox.Show("Conexión Exitosa!!!", "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Falló la conexión a la base de datos", "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cmbAutenticacion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                string tipoAu = (string)this.cmbAutenticacion.SelectedItem;

                this._bloqueo(tipoAu);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void _bloqueo(string tipoAu)
        {

            if (tipoAu.ToLower().Equals("windows"))
            {
                this.tbUsuario.Text = string.Empty;
                this.tbContrasenia.Text = string.Empty;

                this.tbUsuario.Enabled = false;
                this.tbContrasenia.Enabled = false;
            }

            if (tipoAu.ToLower().Equals("sql server"))
            {
                this.tbUsuario.Text = string.Empty;
                this.tbContrasenia.Text = string.Empty;

                this.tbUsuario.Enabled = true;
                this.tbContrasenia.Enabled = true;
            }
        }
    }
}
