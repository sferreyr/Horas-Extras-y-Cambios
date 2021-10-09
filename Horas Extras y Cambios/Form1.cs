using Horas_Extras_y_Cambios.Models;
using Superpower;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using FireSharp;
using SpreadsheetLight;
using Google.Cloud.Firestore;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace Horas_Extras_y_Cambios
{
    public partial class Form1 : Form
    {
        public static float PorcentajeHoras = 0.0f; // Modifiable
        //Database name  From Firestone (Google Cloud)
        public  static String DB = ""; //DATABASE (Ej: name-34341df")
        public  static String COLLECTION = ""; // COLLECTION NAME.

        public Form1()
        {
            InitializeComponent();


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Lista de 3 turnos
            String[] turno = new String[3] { "Turno Mañana", "Turno Tarde", "Turno Noche" };

            //Lista de Compañeros Actuales
            String[] listaCompanieros = new String[] { "Compañero 1", "Compañero 2", "Compañero 3"};

            //Tipo de Cambio a realizar.
            String[] tipoCambio = new string[] { "Cambio de Reten", "Cambio de Francos", "Cambio de Guardia", "Horas Extras" };
           
            foreach (var item in listaCompanieros)
            {
                comboBox1.Items.Add(item);
            }

            foreach (var item in tipoCambio)
            {
                comboBox2.Items.Add(item);
            }

            foreach (var item in turno)
            {
                comboBox3.Items.Add(item);
            }
          
            //Porcentaje Horas Extras;
            comboBox4.Items.Add("Si");
            comboBox4.Items.Add("No");

            // Fecha y formato
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";

        }

   

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            //Seteamos todos los valores a las variables.
            var empleado = comboBox1.Text.ToString();
            var cambio = comboBox2.Text.ToString();
            var turno = comboBox3.Text.ToString();
            var cantHoras = short.Parse(textBox1.Text);
            var fecha = dateTimePicker1.Text;
            var dFranco = comboBox4.Text.ToString(); // Dia franco? Si o No
            var pporcenHoras = ""; // Porcentaje 100% o 50%
            if(dFranco == "Si")
            {
                pporcenHoras = "100%";
            }
            else if(dFranco == "No")
            {
                pporcenHoras = "50%";
            }
            String masInfo = textBox2.Text.ToString();

            FirestoreDb db = FirestoreDb.Create(DB); // Poner valor de Base de datos
            Cambios cambios = new Cambios
            {
                persona = empleado,
                tipoCambio = cambio,
                turno = turno,
                fecha = fecha,
                porcentHoras = pporcenHoras,
                cantHoras = cantHoras,
                diaFranco = dFranco,
                masInformacion = masInfo,
            };
            try
            {
                if (cambios.persona != "" )
                {
                    DocumentReference addedDocRef = await db.Collection(COLLECTION).AddAsync(cambios);
                    MessageBox.Show("Se guardo en la base de datos.");
                }
                else
                {
                    MessageBox.Show("No agregaste a ningun compañero.");
                }

            }
            catch (Exception)
            {
                throw new Exception("No se pudo Guardar, problema en la conexion hacia la base de datos");
            }




            /*  string pathFile = AppDomain.CurrentDomain.BaseDirectory + "DatosGuardados.xlsx";
              SLDocument oSLDocument = new SLDocument();

              System.Data.DataTable dt = new System.Data.DataTable();
              //Columnas
              dt.Columns.Add("Compañero", typeof(string));
              dt.Columns.Add("Tipo de Cambio", typeof(string));
              dt.Columns.Add("Turno", typeof(string));
              dt.Columns.Add("Fecha", typeof(DateTime));
              dt.Columns.Add("Cant. Horas", typeof(string));


              //Registros

              dt.Rows.Add(empleado, cambio, turno, fecha, cantHoras);

              oSLDocument.SetCellValue("A1", true);

              oSLDocument.SaveAs(pathFile);
            */

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem.ToString() != "Horas Extras")
            {
                textBox1.Enabled = false;
                comboBox4.Enabled = false;
            }
            else
            {
                textBox1.Enabled = true;
                comboBox4.Enabled = true;
            }

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex.ToString() == "Si")
            {
                PorcentajeHoras = 100; 
            }
            else
            {
                PorcentajeHoras = 50;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Form2 DatosGuardados = new Form2();
            DatosGuardados.Show();


        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/sferreyr");
        }
    }
}
