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
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace CreadorDeParches
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class BuildPath : Form
    {
        private readonly OpenFileDialog _FilePatch = new OpenFileDialog() { CheckFileExists = false, CheckPathExists = false, DereferenceLinks = true, Multiselect = false, Title = "Seleciona el archivo", Filter = "Text files (GameData.txt;GameData.dat;)|GameData.txt;GameData.dat" };
        private readonly OpenFileDialog _FilesPatch = new OpenFileDialog() { CheckFileExists = false, CheckPathExists = false, DereferenceLinks = true, Multiselect = false, Title = "Seleciona el archivo", Filter = "All files (*.*)|*.*" };
        private string Rootpatch;
        private bool primero = false;
        public BuildPath()
        {

            InitializeComponent();

        }
        #region CMB_CargarParchesClick
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
                string[] strArray = str.Split(char.Parse(":"));
                int index = ParchesdataGridView.Rows.Add();
                ParchesdataGridView.Rows[index].Cells[0].Value = strArray[0];
                ParchesdataGridView.Rows[index].Cells[1].Value = strArray[1];
            }
            _Stream.Close();

        }
        #endregion

        #region remover/limpiar
        private void CMB_RemoverClick(object sender, EventArgs e)
        {
            if (ParchesdataGridView.Rows.Count != 0)
            {
                if (ParchesdataGridView.SelectedRows.Count != 0)
                {
                    if (ParchesdataGridView.Rows.Count == ParchesdataGridView.SelectedRows.Count)
                        ParchesdataGridView.Rows.Clear();
                    foreach (DataGridViewRow dataGridViewRow in (BaseCollection)ParchesdataGridView.SelectedRows)
                        ParchesdataGridView.Rows.Remove(dataGridViewRow);
                }
                else
                {
                    int num1 = (int)MessageBox.Show("No ha seleccionado una fila a eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            else
            {
                int num2 = (int)MessageBox.Show("No hay ninguna fila a borrar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
                streamWriter.WriteLine(string.Format("{0}:{1}", ParchesdataGridView.Rows[index].Cells[0].Value, ParchesdataGridView.Rows[index].Cells[1].Value));
            streamWriter.Flush();
            streamWriter.Close();

        }
        #endregion

        private void Checkfirst(string data)
        {
            if (!primero)
            {
                primero = true;
                Rootpatch = Path.GetDirectoryName(data);
            }
        }
        private bool ComparePath(string data)
        {
            if (Rootpatch == Path.GetDirectoryName(data))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        void BuildPathLoad(object sender, EventArgs e)
        {
            notifyIcon1.Icon = Icon;
            notifyIcon1.Text = "BuildPath By GNR092";

        }
        #region D&D
        private void ParchesdataGridView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void ParchesdataGridView_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                int total = 0;
                int totalmax = 0;
                pb_data.Value = 0;
                foreach (string str in (string[])e.Data.GetData(DataFormats.FileDrop, false))
                {
                    if (Directory.Exists(str))
                    {
                        string[] f = Directory.GetFiles(str, "*.*", SearchOption.AllDirectories);
                        foreach (string files in f)
                        {
                            totalmax++;
                        }

                    }
                    else
                    {
                        totalmax++;
                    }
                }
                foreach (string str in (string[])e.Data.GetData(DataFormats.FileDrop, false))
                {
                    if (Directory.Exists(str))
                    {
                        string[] f = Directory.GetFiles(str, "*.*", SearchOption.AllDirectories);
                        foreach (string files in f)
                        {
                            total++;
                            pb_data.Value = (total * 100) / totalmax;
                            string md5;
                            bool duplicado = false;
                            FileStream _Stream = new FileStream(files, FileMode.Open, FileAccess.Read, FileShare.Read);
                            HashAlgorithm hash = new MD5CryptoServiceProvider();
                            byte[] hashmd5 = Calcular.CalcularMD5(hash, _Stream);
                            md5 = BitConverter.ToString(hashmd5, 0).Replace("-", string.Empty);

                            for (int index = 0; index < ParchesdataGridView.Rows.Count; ++index)
                            {
                                if (ParchesdataGridView.Rows[index].Cells[0].Value.ToString() == Path.GetFileName(files) || ParchesdataGridView.Rows[index].Cells[1].Value.ToString() == md5)
                                    duplicado = true;
                            }
                            if (!duplicado)
                            {
                                int index = ParchesdataGridView.Rows.Add();
                                string directorio = Path.GetDirectoryName(files);
                                Checkfirst(files);

                                if (ComparePath(files))
                                {
                                    ParchesdataGridView.Rows[index].Cells[0].Value = Path.GetFileName(files);
                                    ParchesdataGridView.Rows[index].Cells[1].Value = md5;
                                }
                                else
                                {
                                    string path = Path.GetDirectoryName(files);
                                    if (path.Contains(Rootpatch))
                                    {
                                        int count = Rootpatch.Length;
                                        string newdirectori = Path.GetDirectoryName(files).Remove(0, count + 1);
                                        ParchesdataGridView.Rows[index].Cells[0].Value = newdirectori + "\\" + Path.GetFileName(files);
                                        ParchesdataGridView.Rows[index].Cells[1].Value = md5;
                                    }
                                    
                                }
                            }
                        }
                    }
                    else
                    {
                        total++;
                        pb_data.Value = (total * 100) / totalmax;
                        string md5;
                        bool duplicado = false;
                        FileStream _Stream = new FileStream(str, FileMode.Open, FileAccess.Read, FileShare.Read);
                        HashAlgorithm hash = new MD5CryptoServiceProvider();
                        byte[] hashmd5 = Calcular.CalcularMD5(hash, _Stream);
                        md5 = BitConverter.ToString(hashmd5, 0).Replace("-", string.Empty);

                        for (int index = 0; index < ParchesdataGridView.Rows.Count; ++index)
                        {
                            if (ParchesdataGridView.Rows[index].Cells[0].Value.ToString() == Path.GetFileName(str) || ParchesdataGridView.Rows[index].Cells[1].Value.ToString() == md5)
                                duplicado = true;
                        }
                        if (!duplicado)
                        {
                            int index = ParchesdataGridView.Rows.Add();
                            string directorio = Path.GetDirectoryName(str);
                            Checkfirst(str);

                            if (ComparePath(str))
                            {
                                ParchesdataGridView.Rows[index].Cells[0].Value = Path.GetFileName(str);
                                ParchesdataGridView.Rows[index].Cells[1].Value = md5;
                            }
                            else
                            {
                                int count = Rootpatch.Length;
                                string newdirectori = Path.GetDirectoryName(str).Remove(0, count + 1);
                                ParchesdataGridView.Rows[index].Cells[0].Value = newdirectori + "\\" + Path.GetFileName(str);
                                ParchesdataGridView.Rows[index].Cells[1].Value = md5;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }//
                }

                MessageBox.Show("Se agregaron " + total + " de archivos con exito");
            }
            catch(Exception)
            {
                MessageBox.Show("Error al agregar archivo o carpeta El directorio \n"+Rootpatch+" \n no es el mismo que a agregado porfavor compruebe que este en la anterior direccion\n guarde el trabajo y reinicie el programa","ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        #endregion

        private void ParchesdataGridView_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
    }
}
