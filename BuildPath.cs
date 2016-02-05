/*
 * Usuario: GNR092
 * Fecha: 04/02/2016
 * Hora: 11:09 p.m.
 * 
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace CreadorDeParches
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class BuildPath : Form
	{
		private readonly OpenFileDialog _FilePatch = new OpenFileDialog(){CheckFileExists = false,CheckPathExists = false,DereferenceLinks = true,Multiselect = false,Title = "Seleciona el archivo",Filter = "Text files (GameData.txt;GameData.dat;)|GameData.txt;GameData.dat"};
		private readonly OpenFileDialog _FilesPatch = new OpenFileDialog(){CheckFileExists = false,CheckPathExists = false,DereferenceLinks = true,Multiselect = false,Title = "Seleciona el archivo",Filter = "All files (*.*)|*.*"};
		public BuildPath()
		{
			
			InitializeComponent();
			
		}
		private void CMB_CargarParchesClick(object sender, EventArgs e)
		{
			ParchesdataGridView.Rows.Clear();
			if (_FilePatch.ShowDialog() != DialogResult.OK)
				return;
			TEXT_CargarParches.Text = _FilePatch.FileName;
			StreamReader _Stream = new StreamReader(_FilePatch.FileName);
			string str;
			while ((str = _Stream.ReadLine()) != null)
			       {
			       	string[] strArray = str.Split(char.Parse(","));
			       	int index = ParchesdataGridView.Rows.Add();
			       	ParchesdataGridView.Rows[index].Cells[0].Value= strArray[0];
			       	ParchesdataGridView.Rows[index].Cells[1].Value= strArray[1];
			       }
			_Stream.Close();
	
		}
		private void CMB_CargarArchivoClick(object sender, EventArgs e)
		{
			if (_FilesPatch.ShowDialog() != DialogResult.OK)
				return;
			TEXT_CargarArchivo.Text = _FilesPatch.FileName;
			FileStream _Stream = new FileStream(_FilesPatch.FileName,FileMode.Open,FileAccess.Read,FileShare.Read);
			HashAlgorithm hash = new MD5CryptoServiceProvider();
			byte[] hashmd5 = Calcular.CalcularMD5(hash,_Stream);
			TEXT_MD5.Text = BitConverter.ToString(hashmd5,0).Replace("-",string.Empty);
	
		}
		private void CMB_AgregarClick(object sender, EventArgs e)
		{
			if (!(TEXT_CargarArchivo.Text != string.Empty) || !(TEXT_MD5.Text != string.Empty))
				return;
			bool flag = false;
			for (int index = 0; index < ParchesdataGridView.Rows.Count; ++index) 
			{
				if (ParchesdataGridView.Rows[index].Cells[0].Value.ToString() == Path.GetFileName(TEXT_CargarArchivo.Text) || ParchesdataGridView.Rows[index].Cells[1].Value.ToString() == TEXT_MD5.Text) 
					flag = true;
			}
			if (!flag) 
			{
				int index = ParchesdataGridView.Rows.Add();
				this.ParchesdataGridView.Rows[index].Cells[0].Value = Path.GetFileName(TEXT_CargarArchivo.Text);
				this.ParchesdataGridView.Rows[index].Cells[1].Value = TEXT_MD5.Text;
				
			}
			else
				{
				int num = (int) MessageBox.Show("No se puede Agregar artículo duplicado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
	
		}
		private void CMB_RemoverClick(object sender, EventArgs e)
		{
			if (ParchesdataGridView.Rows.Count != 0)
				{
				if (ParchesdataGridView.SelectedRows.Count != 0)
					{
					if (ParchesdataGridView.Rows.Count == ParchesdataGridView.SelectedRows.Count)
						ParchesdataGridView.Rows.Clear();
					foreach (DataGridViewRow dataGridViewRow in (BaseCollection) ParchesdataGridView.SelectedRows)
						ParchesdataGridView.Rows.Remove(dataGridViewRow);
					}
				else
					{
					int num1 = (int) MessageBox.Show("No ha seleccionado una fila a eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
			else
				{
				int num2 = (int) MessageBox.Show("No hay ninguna fila a borrar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
		}
		private void CMB_LimpiarClick(object sender, EventArgs e)
		{
			ParchesdataGridView.Rows.Clear();
	
		}
		void CMB_GuardarClick(object sender, EventArgs e)
		{
			if (ParchesdataGridView.Rows.Count == 0 || savePatchesFileDialog.ShowDialog() != DialogResult.OK)
				return;
			StreamWriter streamWriter = new StreamWriter(savePatchesFileDialog.FileName);
			for (int index = 0; index < ParchesdataGridView.Rows.Count; ++index)
				streamWriter.WriteLine(string.Format("{0},{1}", ParchesdataGridView.Rows[index].Cells[0].Value, ParchesdataGridView.Rows[index].Cells[1].Value));
			streamWriter.Flush();
			streamWriter.Close();
	
		}
		
		void BuildPathLoad(object sender, EventArgs e)
		{
			notifyIcon1.Icon = Icon;
			notifyIcon1.Text = "BuildPath By GNR092";
	
		}
	}
}
