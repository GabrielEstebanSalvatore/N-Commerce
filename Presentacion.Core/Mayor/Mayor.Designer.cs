
namespace Presentacion.Core.Mayor
{
    partial class Mayor
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.pnlBotonera = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.chkFecha = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaDesde = new System.Windows.Forms.DateTimePicker();
            this.btnSalir = new FontAwesome.Sharp.IconButton();
            this.btnActualizar = new FontAwesome.Sharp.IconButton();
            this.btnBuscar = new FontAwesome.Sharp.IconButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnNuevo = new FontAwesome.Sharp.IconButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel15 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.panel16 = new System.Windows.Forms.Panel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnlBotonera.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel15.SuspendLayout();
            this.panel16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBotonera
            // 
            this.pnlBotonera.BackColor = System.Drawing.SystemColors.Control;
            this.pnlBotonera.Controls.Add(this.panel1);
            this.pnlBotonera.Controls.Add(this.chkFecha);
            this.pnlBotonera.Controls.Add(this.label2);
            this.pnlBotonera.Controls.Add(this.label1);
            this.pnlBotonera.Controls.Add(this.dtpFechaHasta);
            this.pnlBotonera.Controls.Add(this.dtpFechaDesde);
            this.pnlBotonera.Controls.Add(this.btnSalir);
            this.pnlBotonera.Controls.Add(this.btnActualizar);
            this.pnlBotonera.Controls.Add(this.btnBuscar);
            this.pnlBotonera.Controls.Add(this.panel5);
            this.pnlBotonera.Controls.Add(this.btnNuevo);
            this.pnlBotonera.Controls.Add(this.panel2);
            this.pnlBotonera.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBotonera.Location = new System.Drawing.Point(0, 0);
            this.pnlBotonera.Name = "pnlBotonera";
            this.pnlBotonera.Size = new System.Drawing.Size(1160, 64);
            this.pnlBotonera.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(46)))), ((int)(((byte)(37)))));
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Location = new System.Drawing.Point(129, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2, 38);
            this.panel1.TabIndex = 17;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(46)))), ((int)(((byte)(37)))));
            this.panel4.Location = new System.Drawing.Point(85, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(2, 38);
            this.panel4.TabIndex = 7;
            // 
            // chkFecha
            // 
            this.chkFecha.AutoSize = true;
            this.chkFecha.Location = new System.Drawing.Point(543, 26);
            this.chkFecha.Name = "chkFecha";
            this.chkFecha.Size = new System.Drawing.Size(15, 14);
            this.chkFecha.TabIndex = 31;
            this.chkFecha.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(344, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 15);
            this.label2.TabIndex = 30;
            this.label2.Text = "Fecha hasta";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(145, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 15);
            this.label1.TabIndex = 29;
            this.label1.Text = "Fecha desde";
            // 
            // dtpFechaHasta
            // 
            this.dtpFechaHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaHasta.Location = new System.Drawing.Point(424, 22);
            this.dtpFechaHasta.Name = "dtpFechaHasta";
            this.dtpFechaHasta.Size = new System.Drawing.Size(106, 21);
            this.dtpFechaHasta.TabIndex = 28;
            // 
            // dtpFechaDesde
            // 
            this.dtpFechaDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaDesde.Location = new System.Drawing.Point(229, 22);
            this.dtpFechaDesde.Name = "dtpFechaDesde";
            this.dtpFechaDesde.Size = new System.Drawing.Size(106, 21);
            this.dtpFechaDesde.TabIndex = 27;
            // 
            // btnSalir
            // 
            this.btnSalir.FlatAppearance.BorderSize = 0;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("EuropeExt", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.IconChar = FontAwesome.Sharp.IconChar.SignOutAlt;
            this.btnSalir.IconColor = System.Drawing.Color.Red;
            this.btnSalir.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnSalir.IconSize = 32;
            this.btnSalir.Location = new System.Drawing.Point(1069, 15);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(41, 39);
            this.btnSalir.TabIndex = 26;
            this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnSalir.UseVisualStyleBackColor = true;
            // 
            // btnActualizar
            // 
            this.btnActualizar.FlatAppearance.BorderSize = 0;
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizar.Font = new System.Drawing.Font("EuropeExt", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizar.IconChar = FontAwesome.Sharp.IconChar.Retweet;
            this.btnActualizar.IconColor = System.Drawing.Color.Black;
            this.btnActualizar.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnActualizar.IconSize = 32;
            this.btnActualizar.Location = new System.Drawing.Point(715, 14);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(140, 39);
            this.btnActualizar.TabIndex = 22;
            this.btnActualizar.Text = "ACTUALIZAR";
            this.btnActualizar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnActualizar.UseVisualStyleBackColor = true;
            // 
            // btnBuscar
            // 
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("EuropeExt", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnBuscar.IconColor = System.Drawing.Color.SteelBlue;
            this.btnBuscar.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnBuscar.IconSize = 32;
            this.btnBuscar.Location = new System.Drawing.Point(11, 14);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(116, 39);
            this.btnBuscar.TabIndex = 18;
            this.btnBuscar.Text = "BUSCAR";
            this.btnBuscar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(46)))), ((int)(((byte)(37)))));
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Location = new System.Drawing.Point(706, 14);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(2, 38);
            this.panel5.TabIndex = 19;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(46)))), ((int)(((byte)(37)))));
            this.panel6.Location = new System.Drawing.Point(85, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(2, 38);
            this.panel6.TabIndex = 7;
            // 
            // btnNuevo
            // 
            this.btnNuevo.FlatAppearance.BorderSize = 0;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("EuropeExt", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevo.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            this.btnNuevo.IconColor = System.Drawing.Color.Green;
            this.btnNuevo.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnNuevo.IconSize = 32;
            this.btnNuevo.Location = new System.Drawing.Point(580, 14);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(120, 39);
            this.btnNuevo.TabIndex = 17;
            this.btnNuevo.Text = "IMPRIMIR";
            this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnNuevo.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(46)))), ((int)(((byte)(37)))));
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(574, 14);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(2, 38);
            this.panel2.TabIndex = 16;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(46)))), ((int)(((byte)(37)))));
            this.panel3.Location = new System.Drawing.Point(85, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(2, 38);
            this.panel3.TabIndex = 7;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(60)))));
            this.panel7.Controls.Add(this.panel15);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 64);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1160, 444);
            this.panel7.TabIndex = 6;
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.panel15.Controls.Add(this.label17);
            this.panel15.Controls.Add(this.panel16);
            this.panel15.Location = new System.Drawing.Point(38, 48);
            this.panel15.Margin = new System.Windows.Forms.Padding(15, 3, 3, 3);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(982, 311);
            this.panel15.TabIndex = 11;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("EuropeExt", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(326, 8);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(244, 17);
            this.label17.TabIndex = 8;
            this.label17.Text = "INGRESOS - EGRESOS";
            // 
            // panel16
            // 
            this.panel16.Controls.Add(this.chart1);
            this.panel16.Location = new System.Drawing.Point(3, 28);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(967, 280);
            this.panel16.TabIndex = 0;
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.Transparent;
            this.chart1.BorderlineColor = System.Drawing.Color.Transparent;
            this.chart1.BorderlineWidth = 2;
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.HeaderSeparatorColor = System.Drawing.Color.Transparent;
            legend1.InterlacedRowsColor = System.Drawing.Color.White;
            legend1.ItemColumnSeparatorColor = System.Drawing.Color.Transparent;
            legend1.Name = "Legend1";
            legend1.TitleBackColor = System.Drawing.Color.White;
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "Ingresos";
            series2.BorderWidth = 3;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.Name = "Egresos";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(967, 280);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // Mayor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 508);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.pnlBotonera);
            this.Name = "Mayor";
            this.Text = "Mayor";
            this.pnlBotonera.ResumeLayout(false);
            this.pnlBotonera.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            this.panel16.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBotonera;
        private FontAwesome.Sharp.IconButton btnSalir;
        protected FontAwesome.Sharp.IconButton btnActualizar;
        protected FontAwesome.Sharp.IconButton btnBuscar;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        protected FontAwesome.Sharp.IconButton btnNuevo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaHasta;
        private System.Windows.Forms.DateTimePicker dtpFechaDesde;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox chkFecha;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}