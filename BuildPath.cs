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
        private readonly OpenFileDialog _FilePatch = new OpenFileDialog() { CheckFileExists = false, CheckPathExists = false, DereferenceLinks = true, Multiselect = false, Title = "Seleciona el archivo", Filter = "GameData files |GameData.txt;GameData.dat|TerraData Files |TerraData.txt|All files |*.*" };
        private string Rootpatch;
        private bool primero = false;
        private int actualcelda = 0;
        private int actualizaron = 0;
        private string Fileupdatelist = null;
        private List<string> ArchivosActualizados = new List<string>();
        private List<string> Nuevos = new List<string>();
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
            Rootpatch = null;
            primero = false;
            actualizaron = 0;
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
                            
                            pb_data.Value = (total * 100) / totalmax;
                            string md5;
                            bool duplicado = false;
                            FileStream _Stream = new FileStream(files, FileMode.Open, FileAccess.Read, FileShare.Read);
                            HashAlgorithm hash = new MD5CryptoServiceProvider();
                            byte[] hashmd5 = Calcular.CalcularMD5(hash, _Stream);
                            md5 = BitConverter.ToString(hashmd5, 0).Replace("-", string.Empty);
                            _Stream.Close();
                            for (int index = 0; index < ParchesdataGridView.Rows.Count; ++index)
                            {
                                if (ParchesdataGridView.Rows[index].Cells[0].Value.ToString() == Path.GetFileName(files) || ParchesdataGridView.Rows[index].Cells[1].Value.ToString() == md5)
                                {
                                    actualcelda = index;
                                    duplicado = true;
                                }
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
                                    Nuevos.Add(Path.GetFileName(files) + ":" + md5);
                                    total++;
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
                                        Nuevos.Add(newdirectori + "\\" + Path.GetFileName(files) + ":" + md5);
                                        total++;
                                    }

                                }
                            }
                            else
                            {
                                string directorio = Path.GetDirectoryName(files);
                                Checkfirst(files);
                                if (ComparePath(files))
                                {
                                    if (ParchesdataGridView.Rows[actualcelda].Cells[1].Value.ToString() != md5)
                                    {
                                        Fileupdatelist = "Antes: " + ParchesdataGridView.Rows[actualcelda].Cells[0].Value.ToString() + ":" + ParchesdataGridView.Rows[actualcelda].Cells[1].Value.ToString() + " Despues: " + Path.GetFileName(files)+":"+md5;
                                        ArchivosActualizados.Add(Fileupdatelist);
                                        ParchesdataGridView.Rows[actualcelda].Cells[0].Value = Path.GetFileName(files);
                                        ParchesdataGridView.Rows[actualcelda].Cells[1].Value = md5;
                                        actualizaron++;
                                    }
                                }
                                else
                                {
                                    if (ParchesdataGridView.Rows[actualcelda].Cells[1].Value.ToString() != md5)
                                    {
                                        string path = Path.GetDirectoryName(files);
                                        if (path.Contains(Rootpatch))
                                        {
                                            int count = Rootpatch.Length;
                                            string newdirectori = Path.GetDirectoryName(files).Remove(0, count + 1);
                                            Fileupdatelist = "Antes: " + ParchesdataGridView.Rows[actualcelda].Cells[0].Value.ToString() + ":" + ParchesdataGridView.Rows[actualcelda].Cells[1].Value.ToString() + " Despues: " + newdirectori + "\\" + Path.GetFileName(files) + ":" + md5;
                                            ArchivosActualizados.Add(Fileupdatelist);
                                            ParchesdataGridView.Rows[actualcelda].Cells[0].Value = newdirectori + "\\" + Path.GetFileName(files);
                                            ParchesdataGridView.Rows[actualcelda].Cells[1].Value = md5;
                                            actualizaron++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        
                        pb_data.Value = (total * 100) / totalmax;
                        string md5;
                        bool duplicado = false;
                        FileStream _Stream = new FileStream(str, FileMode.Open, FileAccess.Read, FileShare.Read);
                        HashAlgorithm hash = new MD5CryptoServiceProvider();
                        byte[] hashmd5 = Calcular.CalcularMD5(hash, _Stream);
                        md5 = BitConverter.ToString(hashmd5, 0).Replace("-", string.Empty);
                        _Stream.Close();
                        for (int index = 0; index < ParchesdataGridView.Rows.Count; ++index)
                        {
                            if (ParchesdataGridView.Rows[index].Cells[0].Value.ToString() == Path.GetFileName(str) || ParchesdataGridView.Rows[index].Cells[1].Value.ToString() == md5)
                            {
                                actualcelda = index;
                                duplicado = true;
                            }
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
                                Nuevos.Add(Path.GetFileName(str) + ":" + md5);
                                total++;
                            }
                            else
                            {
                                int count = Rootpatch.Length;
                                string newdirectori = Path.GetDirectoryName(str).Remove(0, count + 1);
                                ParchesdataGridView.Rows[index].Cells[0].Value = newdirectori + "\\" + Path.GetFileName(str);
                                ParchesdataGridView.Rows[index].Cells[1].Value = md5;
                                Nuevos.Add(newdirectori + "\\" + Path.GetFileName(str) + ":" + md5);
                                total++;
                            }
                        }
                        else
                        {
                            if (ParchesdataGridView.Rows[actualcelda].Cells[1].Value.ToString() != md5)
                            {
                                string directorio = Path.GetDirectoryName(str);
                                Checkfirst(str);
                                if (ComparePath(str))
                                {
                                    Fileupdatelist = "Antes: " + ParchesdataGridView.Rows[actualcelda].Cells[0].Value.ToString() + ":" + ParchesdataGridView.Rows[actualcelda].Cells[1].Value.ToString() + " Despues: " + Path.GetFileName(str) + ":" + md5;
                                    ArchivosActualizados.Add(Fileupdatelist);
                                    ParchesdataGridView.Rows[actualcelda].Cells[0].Value = Path.GetFileName(str);
                                    ParchesdataGridView.Rows[actualcelda].Cells[1].Value = md5;
                                    actualizaron++;
                                }
                            }
                            else
                            {
                                if (ParchesdataGridView.Rows[actualcelda].Cells[1].Value.ToString() != md5)
                                {
                                    string path = Path.GetDirectoryName(str);
                                    if (path.Contains(Rootpatch))
                                    {
                                        int count = Rootpatch.Length;
                                        string newdirectori = Path.GetDirectoryName(str).Remove(0, count + 1);
                                        Fileupdatelist = "Antes: " + ParchesdataGridView.Rows[actualcelda].Cells[0].Value.ToString() + ":" + ParchesdataGridView.Rows[actualcelda].Cells[1].Value.ToString() + " Despues: " + newdirectori + "\\" + Path.GetFileName(str) + ":" + md5;
                                        ArchivosActualizados.Add(Fileupdatelist);
                                        ParchesdataGridView.Rows[actualcelda].Cells[0].Value = newdirectori + "\\" + Path.GetFileName(str);
                                        ParchesdataGridView.Rows[actualcelda].Cells[1].Value = md5;
                                        actualizaron++;
                                    }
                                }
                            }
                        }
                    }//
                }
                MessageBox.Show("Se agregaron: " + total + " y se actualizaron: " + actualizaron);
            }
            catch(Exception)
            {
                MessageBox.Show("Error al agregar archivo o carpeta El directorio \n"+Rootpatch+" \n no es el mismo que a agregado porfavor compruebe que este en la anterior direccion\n guarde el trabajo y reinicie el programa","ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            try
            {
                new Thread(() =>
                {
                    if (ArchivosActualizados.Count > 0)
                    {
                        var dir = Path.Combine(Application.StartupPath);
                        using (StreamWriter file = new StreamWriter(dir + "\\Actua_" + DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss tt") + ".log"))
                        {
                            foreach (string line in ArchivosActualizados)
                            {
                                file.WriteLine(line);
                            }
                        }
                        ArchivosActualizados = null;
                    }
                }) { IsBackground = true }.Start();

                if (Nuevos.Count > 0)
                {
                    var dir = Path.Combine(Application.StartupPath);
                    using (StreamWriter file = new StreamWriter(dir + "\\New_" + DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss tt") + ".log"))
                    {
                        foreach (string line in Nuevos)
                        {
                            file.WriteLine(line);
                        }
                    }
                    Nuevos = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        private void ParchesdataGridView_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
    }
}
