using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CustomUIControls;
using System.Threading;
using NotificadorExistencias.GUIs;
using NotificadorExistencias.Negocio;

namespace NotificadorExistencias
{
    public partial class frmArticulos : Form
    {
        private TaskbarNotifier _tnNotificador;
        private List<string> _lista = new List<string>();
        private List<Modelos.Notificacion> _notifInsumos = new List<Modelos.Notificacion>();
        private string _inicaA;
        int _index;
        private IConsultasNegocio _consultasNegocio;
        private string _path;

        public frmArticulos()
        {
            InitializeComponent();

            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
        }

        private void frmArticulos_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;

            try
            {
                // valida si ya tiene alguna clave guardada para el archivo
                string cveActual = Properties.Settings.Default.accesoConfig;

                if (string.IsNullOrEmpty(cveActual))
                {
                    string acceso = Modelos.Utilerias.Transform("p4ssw0rd");

                    Properties.Settings.Default.accesoConfig = acceso;
                    Properties.Settings.Default.Save();
                }

                string cveActualNotif = Properties.Settings.Default.accesoNotif;

                if (string.IsNullOrEmpty(cveActualNotif))
                {
                    string acceso = Modelos.Utilerias.Transform("acceso");

                    Properties.Settings.Default.accesoNotif = acceso;
                    Properties.Settings.Default.Save();
                }

                string fileName = "config.dat";
                string pathConfigFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\NotifExis\";

                // si no existe el directorio, lo crea
                bool exists = System.IO.Directory.Exists(pathConfigFile);

                if (!exists) System.IO.Directory.CreateDirectory(pathConfigFile);

                // busca en el directorio si exite el archivo con el nombre dado
                var file = Directory.GetFiles(pathConfigFile, fileName, SearchOption.AllDirectories)
                        .FirstOrDefault();

                if (file == null)
                {
                    // no existe
                    // abrir el formulario para llenar la configuracion de conexion 
                    frmConfiguracion form = new frmConfiguracion();
                    var resultado = form.ShowDialog();

                    if (resultado != System.Windows.Forms.DialogResult.OK)
                    {
                        this._inicaA = string.Empty;
                        throw new Exception("No se ha definido la configuración");
                    }
                }

                file = Directory.GetFiles(pathConfigFile, fileName, SearchOption.AllDirectories)
                        .FirstOrDefault();

                // si existe
                // obtener la cadena de conexion del archivo
                FEncrypt.Respuesta result = FEncrypt.EncryptDncrypt.DecryptFile(file, "milagros");

                if (result.status == FEncrypt.Estatus.ERROR)
                    throw new Exception(result.error);

                if (result.status == FEncrypt.Estatus.OK)
                {
                    string[] list = result.resultado.Split(new string[] { "||" }, StringSplitOptions.None);

                    string servidor = list[0].Substring(2);     // servidor
                    string baseDatos = list[1].Substring(2);    // base de datos
                    string tipoAu = list[2].Substring(2);       // tipo de autenticacion
                    string usuario = list[3].Substring(2);      // usuario
                    string contra = list[4].Substring(2);       // contraseña
                    this._inicaA = list[5].Substring(2);        // inicia app

                    if (tipoAu.ToLower().Equals("windows"))
                    {
                        Modelos.ConectionString.conn = string.Format(
                        "Data Source={0};Initial Catalog={1};Integrated Security=True;",
                        servidor,
                        baseDatos);
                    }

                    if (tipoAu.ToLower().Equals("sql server"))
                    {
                        Modelos.ConectionString.conn = string.Format(
                        "Data Source={0};database={1};User Id={2};password={3};Trusted_Connection=yes;",
                        servidor,
                        baseDatos,
                        usuario,
                        contra);
                    }

                }


                // iniciando valores para el notificador
                System.Drawing.Bitmap skin = new System.Drawing.Bitmap(Path.GetFullPath(@"Images\skin.bmp"));
                System.Drawing.Bitmap close = new System.Drawing.Bitmap(Path.GetFullPath(@"Images\close.bmp"));

                this._tnNotificador = new TaskbarNotifier();
                this._tnNotificador.SetBackgroundBitmap(skin, Color.FromArgb(255, 0, 255));
                this._tnNotificador.SetCloseBitmap(close, Color.FromArgb(255, 0, 255), new Point(300, 7));
                this._tnNotificador.TitleRectangle = new Rectangle(10, 13, 330, 25);
                this._tnNotificador.ContentRectangle = new Rectangle(0, 8, 320, 150);

                this.cargaInfo();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Notificador de Existencias", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void cargaInfo()
        {
            this._consultasNegocio = new ConsultasNegocio();
            List<Modelos.Notificacion> insumos = new List<Modelos.Notificacion>();

            // carga los insumos de la base de datos
            List<Modelos.Notificacion> insumosDB = this._consultasNegocio.getInsumos();

            #region - obtiene la informacion del archivo guardado -

            List<Modelos.Notificacion> guardado = new List<Modelos.Notificacion>();

            string fileName = "datos.dat";
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
                    string[] renglones = result.resultado.Split(new string[] { "||" }, StringSplitOptions.None);

                    foreach (string renglon in renglones)
                    {
                        string[] columnas = renglon.Split(new string[] { "|" }, StringSplitOptions.None);

                        guardado.Add(new Modelos.Notificacion
                        {
                            mostrar = Convert.ToString(columnas[0]).ToLower().Equals("true") ? true : false,
                            idInsumo = Convert.ToString(columnas[1]),
                            insumo = Convert.ToString(columnas[2]),
                            falta = Convert.ToString(columnas[3]),
                            mostrarXAlmacen = Convert.ToString(columnas[4]).ToLower().Equals("true") ? true : false
                        });
                    }
                }
            }

            #endregion

            foreach (Modelos.Notificacion ins in insumosDB)
            {
                Modelos.Notificacion insum = guardado.Where(w => w.idInsumo.Equals(ins.idInsumo)).Select(s => s).FirstOrDefault();

                if (insum != null)
                    insumos.Add(new Modelos.Notificacion
                    {
                        mostrar = insum.mostrar,
                        idInsumo = ins.idInsumo,
                        insumo = ins.insumo,
                        falta = insum.falta,
                        mostrarXAlmacen = insum.mostrarXAlmacen
                    });
                else
                {
                    insumos.Add(new Modelos.Notificacion
                    {
                        mostrar = ins.mostrar,
                        idInsumo = ins.idInsumo,
                        insumo = ins.insumo,
                        falta = ins.falta,
                        mostrarXAlmacen = ins.mostrarXAlmacen
                    });
                }
            }

            this.dgvInsumos.DataSource = insumos;
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this._inicaA))
                    throw new Exception("No se ha definido la configuración");

                this.guardarDatos();
                this.tAccion.Start();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Notificador de Existencias", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        void CloseClick(object obj, EventArgs ea)
        {
            MessageBox.Show("Closed was Clicked");
        }

        void TitleClick(object obj, EventArgs ea)
        {
            MessageBox.Show("Title was Clicked");
        }

        void ContentClick(object obj, EventArgs ea)
        {
            MessageBox.Show("Content was Clicked");
        }

        protected void Displaynotify()
        {
            try
            {

                this.niIconoNotif.Icon = new System.Drawing.Icon(Path.GetFullPath(@"Images\1489520377_Bell.ico"));

                this.niIconoNotif.Visible = true;
                this.niIconoNotif.ShowBalloonTip(100);
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message, "Notificador de Existencias", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void tAccion_Tick(object sender, EventArgs e)
        {
            // oculta el formulario
            this.Hide();
            
            // comienza proceso de notificacion
            Displaynotify();
            
            if (this._lista.Count > 0)
            {
                if (!this.bwAccion.IsBusy)
                {
                    this.bwAccion.RunWorkerAsync();
                    this._index++;

                }
            }
            else
            {
                var insumosMost = this._notifInsumos.Where(w => w.mostrar).Select(s => s).ToList();

                foreach(Modelos.Notificacion not in insumosMost)
                {
                    string leyenda = this._consultasNegocio.getExisInsumo(not.idInsumo, not.mostrarXAlmacen, Convert.ToDecimal(not.falta));

                    if(!string.IsNullOrEmpty(leyenda))
                        this._lista.Add(not.insumo + ":" +Environment.NewLine + leyenda);
                    
                }

                // busca los insumos agotados segun lo definido en la lista por el usuario
                /*
                this._lista.Add("uno");
                this._lista.Add("dos");
                this._lista.Add("tres");
                this._lista.Add("cuatro");
                this._lista.Add("cinco");
                this._lista.Add("seis");
                this._lista.Add("siete");
                */
                //this._lista = new List<string>();

                if (!this.bwAccion.IsBusy)
                    this.bwAccion.RunWorkerAsync();

                this._index = 0;
            }





            /*
            // buscar en la bd cuales articulos faltan y los va a vaciar en una lista
            this._lista.Add("uno");
            this._lista.Add("dos");
            this._lista.Add("tres");
            this._lista.Add("cuatro");
            this._lista.Add("cinco");
            this._lista.Add("seis");
            this._lista.Add("siete");

            int count = this._lista.Count;

            int i = 0;

            while (count > i)
            {
                
                if (!this.bwAccion.IsBusy)
                    this.bwAccion.RunWorkerAsync(4);
                
                //mandar(this._lista[i]);
            /*
                i++;
            }


            this._lista.Clear();
            */
        }

        void mandar(string leyenda)
        {
            this._tnNotificador.CloseClickable = true;
            this._tnNotificador.TitleClickable = false;
            this._tnNotificador.ContentClickable = false;
            this._tnNotificador.EnableSelectionRectangle = false;
            this._tnNotificador.KeepVisibleOnMousOver = true;	// Added Rev 002
            this._tnNotificador.ReShowOnMouseOver = true;			// Added Rev 002

            this._tnNotificador.Show("EXISTENCIA ACTUAL",
                leyenda,
                500, 5000, 500);
        }

        private void bwAccion_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(6000);
            
            /*
            this._tnNotificador.CloseClickable = true;
            this._tnNotificador.TitleClickable = true;
            this._tnNotificador.ContentClickable = true;
            this._tnNotificador.EnableSelectionRectangle = true;
            this._tnNotificador.KeepVisibleOnMousOver = true;	// Added Rev 002
            this._tnNotificador.ReShowOnMouseOver = true;			// Added Rev 002

            this._tnNotificador.Show("SIN EXISTENCIA",
                "tsyrdtfjyvghkbjk",
                500, 5000, 500);
            */
        }

        private void bwAccion_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //this.tAccion.Stop();

            //foreach (string s in this._lista)
            //{

            if (this._index < this._lista.Count)
                mandar(this._lista[this._index]);
            else
                this._lista.Clear();
                
            //}

            //this.tAccion.Start();
        }

        private void niIconoNotif_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            frmAccesoNotif formA = new frmAccesoNotif();

            var respuesta = formA.ShowDialog();

            if (respuesta == System.Windows.Forms.DialogResult.OK)
            {
                this.Show();
                this.tAccion.Stop();   
            }
        }

        private void frmArticulos_Shown(object sender, EventArgs e)
        {
            if (this._inicaA.ToLower().Equals("true"))
            {
                this.guardarDatos();
                this.Hide();
                this.tAccion.Start();
            }
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            frmAcceso formA = new frmAcceso();

            var respuesta = formA.ShowDialog();

            if (respuesta == System.Windows.Forms.DialogResult.OK)
            {
                frmConfiguracion form = new frmConfiguracion();
                var resultado = form.ShowDialog();

                if(resultado == System.Windows.Forms.DialogResult.OK)
                    this.frmArticulos_Load(null, null);
            }
        }

        private void btnGuardaCambios_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvInsumos.Rows.Count == 0)
                    throw new Exception("No hay nada que guardar");

                this.guardarDatos();

                MessageBox.Show("Proceso Concluido");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Notificador de Existencias", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void guardarDatos()
        {
            StringBuilder sb = new StringBuilder();

            this._notifInsumos = new List<Modelos.Notificacion>();

            // leer datos del grid
            for (int rows = 0; rows < this.dgvInsumos.Rows.Count; rows++)
            {
                string mostrar = this.dgvInsumos.Rows[rows].Cells[0].Value.ToString();
                string clave = this.dgvInsumos.Rows[rows].Cells[1].Value.ToString();
                string insumo = this.dgvInsumos.Rows[rows].Cells[2].Value.ToString();
                string menosDe = this.dgvInsumos.Rows[rows].Cells[3].Value == null ? "1" : this.dgvInsumos.Rows[rows].Cells[3].Value.ToString();
                string mostrarx = this.dgvInsumos.Rows[rows].Cells[4].Value.ToString();

                this._notifInsumos.Add(new Modelos.Notificacion
                {
                    mostrar = mostrar.ToLower().Equals("false") ? false : true,
                    idInsumo = clave,
                    insumo = insumo,
                    falta = menosDe,
                    mostrarXAlmacen = mostrarx.ToLower().Equals("false") ? false : true
                });

                sb.Append(mostrar.ToLower().Equals("false") ? "false" : "true").Append("|")
                    .Append(clave).Append("|")
                    .Append(insumo).Append("|")
                    .Append(menosDe).Append("|")
                    .Append(mostrarx.ToLower().Equals("false") ? "false" : "true").Append("||");

            }

            FEncrypt.Respuesta result =
                FEncrypt.EncryptDncrypt.EncryptFile("milagros", sb.ToString().Substring(0, sb.ToString().Trim().Length - 2), this._path);

            if (result.status == FEncrypt.Estatus.ERROR)
                throw new Exception(result.error);

        }

        private void dgvInsumos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvInsumos.CurrentCell.ColumnIndex == 3)
            {
                string texto = 
                    this.dgvInsumos[e.ColumnIndex, e.RowIndex].Value == null ?
                    "0" : this.dgvInsumos[e.ColumnIndex, e.RowIndex].Value.ToString();
                decimal num = 0;

                if (decimal.TryParse(texto, out num))
                    this.dgvInsumos[e.ColumnIndex, e.RowIndex].Value = num;
                else
                    this.dgvInsumos[e.ColumnIndex, e.RowIndex].Value = (decimal)1;

                if(num == 0)
                    this.dgvInsumos[e.ColumnIndex, e.RowIndex].Value = (decimal)1;
            }
        }

        private void btnActLista_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this._inicaA))
                    throw new Exception("No se ha definido la configuración");

                this.cargaInfo();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Notificador de Existencias", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dgvInsumos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
