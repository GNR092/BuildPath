/*
 * Usuario: GNR092
 * Fecha: 04/02/2016
 * Hora: 11:09 p.m.
 * 
 */
namespace CreadorDeParches
{
	partial class BuildPath
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label LB_CargarParches;
		private System.Windows.Forms.TextBox TEXT_CargarParches;
        private System.Windows.Forms.Button CMB_CargarParches;
		private System.Windows.Forms.DataGridView ParchesdataGridView;
		private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
		private System.Windows.Forms.DataGridViewTextBoxColumn MD5;
		private System.Windows.Forms.Button CMB_Limpiar;
        private System.Windows.Forms.Button CMB_Remover;
		private System.Windows.Forms.Button CMB_Guardar;
		private System.Windows.Forms.SaveFileDialog savePatchesFileDialog;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuildPath));
            this.LB_CargarParches = new System.Windows.Forms.Label();
            this.TEXT_CargarParches = new System.Windows.Forms.TextBox();
            this.CMB_CargarParches = new System.Windows.Forms.Button();
            this.ParchesdataGridView = new System.Windows.Forms.DataGridView();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MD5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMB_Limpiar = new System.Windows.Forms.Button();
            this.CMB_Remover = new System.Windows.Forms.Button();
            this.CMB_Guardar = new System.Windows.Forms.Button();
            this.savePatchesFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.pb_data = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.ParchesdataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // LB_CargarParches
            // 
            this.LB_CargarParches.AutoSize = true;
            this.LB_CargarParches.BackColor = System.Drawing.Color.Transparent;
            this.LB_CargarParches.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB_CargarParches.ForeColor = System.Drawing.Color.Yellow;
            this.LB_CargarParches.Location = new System.Drawing.Point(23, 17);
            this.LB_CargarParches.Name = "LB_CargarParches";
            this.LB_CargarParches.Size = new System.Drawing.Size(163, 13);
            this.LB_CargarParches.TabIndex = 0;
            this.LB_CargarParches.Text = "Cargar Archivo de Parches:";
            // 
            // TEXT_CargarParches
            // 
            this.TEXT_CargarParches.Location = new System.Drawing.Point(188, 16);
            this.TEXT_CargarParches.Name = "TEXT_CargarParches";
            this.TEXT_CargarParches.ReadOnly = true;
            this.TEXT_CargarParches.Size = new System.Drawing.Size(442, 20);
            this.TEXT_CargarParches.TabIndex = 2;
            // 
            // CMB_CargarParches
            // 
            this.CMB_CargarParches.Location = new System.Drawing.Point(636, 14);
            this.CMB_CargarParches.Name = "CMB_CargarParches";
            this.CMB_CargarParches.Size = new System.Drawing.Size(75, 23);
            this.CMB_CargarParches.TabIndex = 1;
            this.CMB_CargarParches.Text = "Cargar";
            this.CMB_CargarParches.UseVisualStyleBackColor = true;
            this.CMB_CargarParches.Click += new System.EventHandler(this.CMB_CargarParchesClick);
            // 
            // ParchesdataGridView
            // 
            this.ParchesdataGridView.AllowDrop = true;
            this.ParchesdataGridView.AllowUserToAddRows = false;
            this.ParchesdataGridView.AllowUserToDeleteRows = false;
            this.ParchesdataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ParchesdataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.ParchesdataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ParchesdataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nombre,
            this.MD5});
            this.ParchesdataGridView.Location = new System.Drawing.Point(12, 59);
            this.ParchesdataGridView.Name = "ParchesdataGridView";
            this.ParchesdataGridView.ReadOnly = true;
            this.ParchesdataGridView.Size = new System.Drawing.Size(721, 271);
            this.ParchesdataGridView.TabIndex = 8;
            this.ParchesdataGridView.DragDrop += new System.Windows.Forms.DragEventHandler(this.ParchesdataGridView_DragDrop);
            this.ParchesdataGridView.DragEnter += new System.Windows.Forms.DragEventHandler(this.ParchesdataGridView_DragEnter);
            this.ParchesdataGridView.DragOver += new System.Windows.Forms.DragEventHandler(this.ParchesdataGridView_DragOver);
            // 
            // Nombre
            // 
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            // 
            // MD5
            // 
            this.MD5.HeaderText = "MD5";
            this.MD5.Name = "MD5";
            this.MD5.ReadOnly = true;
            // 
            // CMB_Limpiar
            // 
            this.CMB_Limpiar.Location = new System.Drawing.Point(377, 365);
            this.CMB_Limpiar.Name = "CMB_Limpiar";
            this.CMB_Limpiar.Size = new System.Drawing.Size(75, 23);
            this.CMB_Limpiar.TabIndex = 9;
            this.CMB_Limpiar.Text = "Reiniciar";
            this.CMB_Limpiar.UseVisualStyleBackColor = true;
            this.CMB_Limpiar.Click += new System.EventHandler(this.CMB_LimpiarClick);
            // 
            // CMB_Remover
            // 
            this.CMB_Remover.Location = new System.Drawing.Point(296, 365);
            this.CMB_Remover.Name = "CMB_Remover";
            this.CMB_Remover.Size = new System.Drawing.Size(75, 23);
            this.CMB_Remover.TabIndex = 10;
            this.CMB_Remover.Text = "Remover";
            this.CMB_Remover.UseVisualStyleBackColor = true;
            this.CMB_Remover.Click += new System.EventHandler(this.CMB_RemoverClick);
            // 
            // CMB_Guardar
            // 
            this.CMB_Guardar.Location = new System.Drawing.Point(339, 394);
            this.CMB_Guardar.Name = "CMB_Guardar";
            this.CMB_Guardar.Size = new System.Drawing.Size(75, 23);
            this.CMB_Guardar.TabIndex = 12;
            this.CMB_Guardar.Text = "Guardar";
            this.CMB_Guardar.UseVisualStyleBackColor = true;
            this.CMB_Guardar.Click += new System.EventHandler(this.CMB_GuardarClick);
            // 
            // savePatchesFileDialog
            // 
            this.savePatchesFileDialog.DefaultExt = "txt,dat";
            this.savePatchesFileDialog.Filter = "GameData Files|GameData.txt|TerraData Files|TerraData.txt";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // pb_data
            // 
            this.pb_data.Location = new System.Drawing.Point(12, 336);
            this.pb_data.Name = "pb_data";
            this.pb_data.Size = new System.Drawing.Size(721, 23);
            this.pb_data.TabIndex = 14;
            // 
            // BuildPath
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(745, 426);
            this.Controls.Add(this.pb_data);
            this.Controls.Add(this.CMB_Guardar);
            this.Controls.Add(this.CMB_Remover);
            this.Controls.Add(this.CMB_Limpiar);
            this.Controls.Add(this.ParchesdataGridView);
            this.Controls.Add(this.CMB_CargarParches);
            this.Controls.Add(this.TEXT_CargarParches);
            this.Controls.Add(this.LB_CargarParches);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BuildPath";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CreadorDeParches";
            this.Load += new System.EventHandler(this.BuildPathLoad);
            ((System.ComponentModel.ISupportInitialize)(this.ParchesdataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.ProgressBar pb_data;
	}
}
