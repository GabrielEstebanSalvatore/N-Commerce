namespace Presentacion.Core.Comprobantes
{
    partial class _00056_Presupuestos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_00056_Presupuestos));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.btnModificar = new System.Windows.Forms.ToolStripButton();
            this.dgvGrillaPresupuestos = new System.Windows.Forms.DataGridView();
            this.dgvDetalles = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbTipoComprobante = new System.Windows.Forms.ComboBox();
            this.lblTipoComprobante = new System.Windows.Forms.Label();
            
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrillaPresupuestos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSalir,
            this.btnNuevo,
            this.btnEliminar,
            this.btnModificar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(3);
            this.toolStrip1.Size = new System.Drawing.Size(808, 58);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSalir
            // 
            this.btnSalir.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnSalir.Font = new System.Drawing.Font("EuropeExt", 10F);
            this.btnSalir.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(50, 49);
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Font = new System.Drawing.Font("EuropeExt", 10F);
            this.btnNuevo.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(62, 49);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Font = new System.Drawing.Font("EuropeExt", 10F);
            this.btnEliminar.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(76, 49);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnModificar
            // 
            this.btnModificar.Font = new System.Drawing.Font("EuropeExt", 10F);
            this.btnModificar.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnModificar.Image = ((System.Drawing.Image)(resources.GetObject("btnModificar.Image")));
            this.btnModificar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(88, 49);
            this.btnModificar.Text = "Modificar";
            this.btnModificar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // dgvGrillaPresupuestos
            // 
            this.dgvGrillaPresupuestos.AllowUserToAddRows = false;
            this.dgvGrillaPresupuestos.AllowUserToDeleteRows = false;
            this.dgvGrillaPresupuestos.BackgroundColor = System.Drawing.Color.White;
            this.dgvGrillaPresupuestos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGrillaPresupuestos.Location = new System.Drawing.Point(0, 60);
            this.dgvGrillaPresupuestos.MultiSelect = false;
            this.dgvGrillaPresupuestos.Name = "dgvGrillaPresupuestos";
            this.dgvGrillaPresupuestos.ReadOnly = true;
            this.dgvGrillaPresupuestos.RowHeadersVisible = false;
            this.dgvGrillaPresupuestos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGrillaPresupuestos.Size = new System.Drawing.Size(556, 410);
            this.dgvGrillaPresupuestos.TabIndex = 7;
            this.dgvGrillaPresupuestos.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGrillaPresupuestos_RowEnter);
            this.dgvGrillaPresupuestos.DoubleClick += new System.EventHandler(this.dgvGrillaPresupuestos_DoubleClick);
            // 
            // dgvDetalles
            // 
            this.dgvDetalles.AllowUserToAddRows = false;
            this.dgvDetalles.AllowUserToDeleteRows = false;
            this.dgvDetalles.BackgroundColor = System.Drawing.Color.White;
            this.dgvDetalles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalles.Location = new System.Drawing.Point(562, 60);
            this.dgvDetalles.MultiSelect = false;
            this.dgvDetalles.Name = "dgvDetalles";
            this.dgvDetalles.ReadOnly = true;
            this.dgvDetalles.RowHeadersVisible = false;
            this.dgvDetalles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalles.Size = new System.Drawing.Size(246, 410);
            this.dgvDetalles.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.panel1.Location = new System.Drawing.Point(555, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(10, 410);
            this.panel1.TabIndex = 11;
            // 
            // cmbTipoComprobante
            // 
            this.cmbTipoComprobante.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTipoComprobante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoComprobante.FormattingEnabled = true;
            this.cmbTipoComprobante.Location = new System.Drawing.Point(331, 33);
            this.cmbTipoComprobante.Name = "cmbTipoComprobante";
            this.cmbTipoComprobante.Size = new System.Drawing.Size(204, 21);
            this.cmbTipoComprobante.TabIndex = 98;
            // 
            // lblTipoComprobante
            // 
            this.lblTipoComprobante.AutoSize = true;
            this.lblTipoComprobante.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.lblTipoComprobante.Font = new System.Drawing.Font("EuropeExt", 12F, System.Drawing.FontStyle.Bold);
            this.lblTipoComprobante.ForeColor = System.Drawing.SystemColors.Control;
            this.lblTipoComprobante.Location = new System.Drawing.Point(328, 9);
            this.lblTipoComprobante.Name = "lblTipoComprobante";
            this.lblTipoComprobante.Size = new System.Drawing.Size(237, 17);
            this.lblTipoComprobante.TabIndex = 99;
            this.lblTipoComprobante.Text = "Tipo de Comprobante:";
            
            // 
            // _00056_Presupuestos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 470);
            this.Controls.Add(this.lblTipoComprobante);
            this.Controls.Add(this.cmbTipoComprobante);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvDetalles);
            this.Controls.Add(this.dgvGrillaPresupuestos);
            this.Controls.Add(this.toolStrip1);
            this.Name = "_00056_Presupuestos";
            this.Text = "_00056_Presupuestos";
            this.Load += new System.EventHandler(this._00056_Presupuestos_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrillaPresupuestos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        protected System.Windows.Forms.ToolStripButton btnSalir;
        protected System.Windows.Forms.DataGridView dgvGrillaPresupuestos;
        protected System.Windows.Forms.ToolStripButton btnNuevo;
        protected System.Windows.Forms.ToolStripButton btnEliminar;
        protected System.Windows.Forms.ToolStripButton btnModificar;
        protected System.Windows.Forms.DataGridView dgvDetalles;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbTipoComprobante;
        private System.Windows.Forms.Label lblTipoComprobante;
        
    }
}