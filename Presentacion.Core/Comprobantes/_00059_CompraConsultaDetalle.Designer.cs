namespace Presentacion.Core.Comprobantes
{
    partial class _00059_CompraConsultaDetalle
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
            this.dgvDetallesCompra = new System.Windows.Forms.DataGridView();
            this.lblRazonSocial = new System.Windows.Forms.Label();
            this.lblCuit = new System.Windows.Forms.Label();
            this.lblFechaDeEntrega = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetallesCompra)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDetallesCompra
            // 
            this.dgvDetallesCompra.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetallesCompra.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvDetallesCompra.Location = new System.Drawing.Point(0, 56);
            this.dgvDetallesCompra.Name = "dgvDetallesCompra";
            this.dgvDetallesCompra.RowHeadersVisible = false;
            this.dgvDetallesCompra.Size = new System.Drawing.Size(697, 198);
            this.dgvDetallesCompra.TabIndex = 0;
            // 
            // lblRazonSocial
            // 
            this.lblRazonSocial.AutoSize = true;
            this.lblRazonSocial.Location = new System.Drawing.Point(12, 9);
            this.lblRazonSocial.Name = "lblRazonSocial";
            this.lblRazonSocial.Size = new System.Drawing.Size(76, 13);
            this.lblRazonSocial.TabIndex = 1;
            this.lblRazonSocial.Text = "Razon Social: ";
            // 
            // lblCuit
            // 
            this.lblCuit.AutoSize = true;
            this.lblCuit.Location = new System.Drawing.Point(314, 9);
            this.lblCuit.Name = "lblCuit";
            this.lblCuit.Size = new System.Drawing.Size(38, 13);
            this.lblCuit.TabIndex = 2;
            this.lblCuit.Text = "CUIT: ";
            // 
            // lblFechaDeEntrega
            // 
            this.lblFechaDeEntrega.AutoSize = true;
            this.lblFechaDeEntrega.Location = new System.Drawing.Point(314, 33);
            this.lblFechaDeEntrega.Name = "lblFechaDeEntrega";
            this.lblFechaDeEntrega.Size = new System.Drawing.Size(95, 13);
            this.lblFechaDeEntrega.TabIndex = 3;
            this.lblFechaDeEntrega.Text = "Fecha de Entrega:";
            // 
            // _00059_CompraConsultaDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 254);
            this.Controls.Add(this.lblFechaDeEntrega);
            this.Controls.Add(this.lblCuit);
            this.Controls.Add(this.lblRazonSocial);
            this.Controls.Add(this.dgvDetallesCompra);
            this.Name = "_00059_CompraConsultaDetalle";
            this.Text = "Detalle";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetallesCompra)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDetallesCompra;
        private System.Windows.Forms.Label lblRazonSocial;
        private System.Windows.Forms.Label lblCuit;
        private System.Windows.Forms.Label lblFechaDeEntrega;
    }
}