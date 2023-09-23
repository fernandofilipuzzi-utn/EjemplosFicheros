using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EjemploSerializacionDeserializacion
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        MiContenedor miObjectoContenedor;
                //otros
        string ficheroSistema = "ejemplo.dat";

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            string path = Path.Combine(Application.StartupPath, ficheroSistema);

            FileStream fs = null;
            try
            {
                if (File.Exists(path)==true)
                {
                    fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                    BinaryFormatter bf= new BinaryFormatter();
                    miObjectoContenedor = bf.Deserialize(fs) as MiContenedor;
                }
            }
            catch (SerializationException ex)
            {
                MessageBox.Show(ex.Message, 
                                "Error en la deserialización:",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, 
                                "Advertencia, error en la deserialización",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (fs != null) fs.Close();
            }

            #region preinicialización
            if (miObjectoContenedor == null) //cuando corre por primera vez
            {
                miObjectoContenedor = new MiContenedor();
                miObjectoContenedor.Agregar(new Persona() { DNI = 31323213, Nombre = "Graciela" });
                miObjectoContenedor.Agregar(new Persona() { DNI = 30323213, Nombre = "Norberto" });
            }
            #endregion

            #region pintar los controles con el estado actual
            for (int n = 0; n < miObjectoContenedor.Cantidad; n++)
                dataGridView1.Rows.Add(new string[] { miObjectoContenedor[n].DNI.ToString(),
                                                      miObjectoContenedor[n].Nombre});
            #endregion
        }

        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            string path = Path.Combine(Application.StartupPath, ficheroSistema);

            FileStream fs = null;
            try
            {
                fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, miObjectoContenedor);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                               "Advertencia, error en la deserialización",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }
    }
}
