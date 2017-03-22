namespace NotificadorExistencias
{
    partial class frmArticulos
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmArticulos));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.tAccion = new System.Windows.Forms.Timer(this.components);
            this.bwAccion = new System.ComponentModel.BackgroundWorker();
            this.niIconoNotif = new System.Windows.Forms.NotifyIcon(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.btnConfig = new System.Windows.Forms.Button();
            this.dgvInsumos = new System.Windows.Forms.DataGridView();
            this.notificacionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnGuardaCambios = new System.Windows.Forms.Button();
            this.btnActLista = new System.Windows.Forms.Button();
            this.mostrarDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.idInsumoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.insumoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.faltaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mostrarXAlmacenDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInsumos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.notificacionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnIniciar
            // 
            this.btnIniciar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnIniciar.Location = new System.Drawing.Point(457, 369);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(134, 32);
            this.btnIniciar.TabIndex = 0;
            this.btnIniciar.Text = "Iniciar Notificador";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // tAccion
            // 
            this.tAccion.Interval = 1000;
            this.tAccion.Tick += new System.EventHandler(this.tAccion_Tick);
            // 
            // bwAccion
            // 
            this.bwAccion.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwAccion_DoWork);
            this.bwAccion.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwAccion_RunWorkerCompleted);
            // 
            // niIconoNotif
            // 
            this.niIconoNotif.Text = "Doble click para abrir configuración";
            this.niIconoNotif.Visible = true;
            this.niIconoNotif.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.niIconoNotif_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 18F);
            this.label1.Location = new System.Drawing.Point(78, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(284, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "Notificador de Existencias";
            // 
            // btnConfig
            // 
            this.btnConfig.Image = ((System.Drawing.Image)(resources.GetObject("btnConfig.Image")));
            this.btnConfig.Location = new System.Drawing.Point(12, 12);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(60, 60);
            this.btnConfig.TabIndex = 2;
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // dgvInsumos
            // 
            this.dgvInsumos.AllowUserToAddRows = false;
            this.dgvInsumos.AllowUserToDeleteRows = false;
            this.dgvInsumos.AllowUserToResizeRows = false;
            this.dgvInsumos.AutoGenerateColumns = false;
            this.dgvInsumos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInsumos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mostrarDataGridViewCheckBoxColumn,
            this.idInsumoDataGridViewTextBoxColumn,
            this.insumoDataGridViewTextBoxColumn,
            this.faltaDataGridViewTextBoxColumn,
            this.mostrarXAlmacenDataGridViewCheckBoxColumn});
            this.dgvInsumos.DataSource = this.notificacionBindingSource;
            this.dgvInsumos.Location = new System.Drawing.Point(12, 78);
            this.dgvInsumos.MultiSelect = false;
            this.dgvInsumos.Name = "dgvInsumos";
            this.dgvInsumos.Size = new System.Drawing.Size(598, 280);
            this.dgvInsumos.TabIndex = 3;
            this.dgvInsumos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInsumos_CellContentClick);
            this.dgvInsumos.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInsumos_CellEndEdit);
            // 
            // notificacionBindingSource
            // 
            this.notificacionBindingSource.DataSource = typeof(NotificadorExistencias.Modelos.Notificacion);
            // 
            // btnGuardaCambios
            // 
            this.btnGuardaCambios.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnGuardaCambios.Location = new System.Drawing.Point(299, 369);
            this.btnGuardaCambios.Name = "btnGuardaCambios";
            this.btnGuardaCambios.Size = new System.Drawing.Size(139, 32);
            this.btnGuardaCambios.TabIndex = 4;
            this.btnGuardaCambios.Text = "Guardar cambios";
            this.btnGuardaCambios.UseVisualStyleBackColor = true;
            this.btnGuardaCambios.Click += new System.EventHandler(this.btnGuardaCambios_Click);
            // 
            // btnActLista
            // 
            this.btnActLista.Image = ((System.Drawing.Image)(resources.GetObject("btnActLista.Image")));
            this.btnActLista.Location = new System.Drawing.Point(550, 12);
            this.btnActLista.Name = "btnActLista";
            this.btnActLista.Size = new System.Drawing.Size(60, 60);
            this.btnActLista.TabIndex = 5;
            this.btnActLista.UseVisualStyleBackColor = true;
            this.btnActLista.Click += new System.EventHandler(this.btnActLista_Click);
            // 
            // mostrarDataGridViewCheckBoxColumn
            // 
            this.mostrarDataGridViewCheckBoxColumn.DataPropertyName = "mostrar";
            this.mostrarDataGridViewCheckBoxColumn.HeaderText = "Mostrar";
            this.mostrarDataGridViewCheckBoxColumn.Name = "mostrarDataGridViewCheckBoxColumn";
            this.mostrarDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.mostrarDataGridViewCheckBoxColumn.ToolTipText = "¿Mostrar Notificación?";
            this.mostrarDataGridViewCheckBoxColumn.Width = 48;
            // 
            // idInsumoDataGridViewTextBoxColumn
            // 
            this.idInsumoDataGridViewTextBoxColumn.DataPropertyName = "idInsumo";
            this.idInsumoDataGridViewTextBoxColumn.HeaderText = "Clave";
            this.idInsumoDataGridViewTextBoxColumn.Name = "idInsumoDataGridViewTextBoxColumn";
            this.idInsumoDataGridViewTextBoxColumn.ReadOnly = true;
            this.idInsumoDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.idInsumoDataGridViewTextBoxColumn.Width = 50;
            // 
            // insumoDataGridViewTextBoxColumn
            // 
            this.insumoDataGridViewTextBoxColumn.DataPropertyName = "insumo";
            this.insumoDataGridViewTextBoxColumn.HeaderText = "Insumo";
            this.insumoDataGridViewTextBoxColumn.MinimumWidth = 250;
            this.insumoDataGridViewTextBoxColumn.Name = "insumoDataGridViewTextBoxColumn";
            this.insumoDataGridViewTextBoxColumn.ReadOnly = true;
            this.insumoDataGridViewTextBoxColumn.Width = 260;
            // 
            // faltaDataGridViewTextBoxColumn
            // 
            this.faltaDataGridViewTextBoxColumn.DataPropertyName = "falta";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = "1";
            this.faltaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.faltaDataGridViewTextBoxColumn.HeaderText = "Menos de:";
            this.faltaDataGridViewTextBoxColumn.Name = "faltaDataGridViewTextBoxColumn";
            this.faltaDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.faltaDataGridViewTextBoxColumn.ToolTipText = "Mostrar Notificación cuando la exisistencia sea Menor de:";
            this.faltaDataGridViewTextBoxColumn.Width = 80;
            // 
            // mostrarXAlmacenDataGridViewCheckBoxColumn
            // 
            this.mostrarXAlmacenDataGridViewCheckBoxColumn.DataPropertyName = "mostrarXAlmacen";
            this.mostrarXAlmacenDataGridViewCheckBoxColumn.HeaderText = "Mostrar por Almacén";
            this.mostrarXAlmacenDataGridViewCheckBoxColumn.Name = "mostrarXAlmacenDataGridViewCheckBoxColumn";
            this.mostrarXAlmacenDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.mostrarXAlmacenDataGridViewCheckBoxColumn.Width = 110;
            // 
            // frmArticulos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 415);
            this.Controls.Add(this.btnActLista);
            this.Controls.Add(this.btnGuardaCambios);
            this.Controls.Add(this.dgvInsumos);
            this.Controls.Add(this.btnConfig);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnIniciar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmArticulos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Notificador";
            this.Load += new System.EventHandler(this.frmArticulos_Load);
            this.Shown += new System.EventHandler(this.frmArticulos_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInsumos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.notificacionBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnIniciar;
        public System.Windows.Forms.Timer tAccion;
        private System.ComponentModel.BackgroundWorker bwAccion;
        private System.Windows.Forms.NotifyIcon niIconoNotif;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.DataGridView dgvInsumos;
        private System.Windows.Forms.BindingSource notificacionBindingSource;
        private System.Windows.Forms.Button btnGuardaCambios;
        private System.Windows.Forms.Button btnActLista;
        private System.Windows.Forms.DataGridViewCheckBoxColumn mostrarDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idInsumoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn insumoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn faltaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn mostrarXAlmacenDataGridViewCheckBoxColumn;
    }
}

