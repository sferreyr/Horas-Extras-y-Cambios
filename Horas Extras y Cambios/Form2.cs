using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Google.Cloud.Firestore;
using Horas_Extras_y_Cambios.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Horas_Extras_y_Cambios
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private async void Form2_Load(object sender, EventArgs e)
        {
            //Columnas, tipo de cambio y compañeros
            String[] columnas = new String[] { "Compañero", "Tipo de Cambio", "Turno", "Fecha", "Cantidad de Horas", "Dia Franco", "Porcentaje Horas", "Mas Informacion" };

            //Definiciones de ListView y customizacion
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
          
            listView1.View = View.Details;
            listView1.GridLines = true;
            
            //Añadir columnas
            foreach (var item in columnas)
            {
                listView1.Columns.Add(item);
            }

            //Tamaño dinamico para las columnas
            foreach (ColumnHeader column in listView1.Columns)
            {
                column.Width = listView1.Width / listView1.Columns.Count;
            }

            try
            {
                FirestoreDb db = FirestoreDb.Create(Form1.DB); //  Valores obtenidos de variables estaticas Form1
                Query Query = db.Collection(Form1.COLLECTION).OrderByDescending("fecha").Limit(15);
                QuerySnapshot QuerySnapshot = await Query.GetSnapshotAsync();
                foreach (DocumentSnapshot documentSnapshot in QuerySnapshot.Documents)
                {
                    Dictionary<string, object> resultado = documentSnapshot.ToDictionary();

                    Console.WriteLine("Id del elemento" + documentSnapshot.Id);
                    var ID = documentSnapshot.Id;
                    var persona = resultado["persona"].ToString();
                    var turno = resultado["turno"].ToString();
                    var tipoCambio = resultado["tipoCambio"].ToString();
                    var fecha = resultado["fecha"].ToString();
                    var cantHoras = resultado["cantHoras"].ToString();
                    var porcentHoras = resultado["porcentHoras"].ToString();
                    var dFranco = resultado["diaFranco"].ToString();
                    String masInfo = resultado["masInformacion"].ToString();

                    //En orden se agregan los resultados, para coincidir con las columnas
                    listView1.Items.Add(new ListViewItem(new string[] {
                    persona,
                    tipoCambio,
                    turno,
                    fecha,
                    cantHoras,
                    dFranco,
                    porcentHoras,
                    masInfo,
                    ID,
                    }));

                }

            }
            catch (Exception)
            {
                throw new Exception("Problema en la conexion hacia la base de datos");
            }
           

        }

      
        private async void button1_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem eachItem in listView1.SelectedItems)
            {
                //Seteamos la posicion del Index 'ID'
                String idValue = eachItem.SubItems[8].Text;
                if(idValue != "")
                {
                    try
                    {
                        FirestoreDb db = FirestoreDb.Create(Form1.DB); //  Valores obtenidos de variables estaticas Form1
                        DocumentReference delRef = db.Collection(Form1.COLLECTION).Document(idValue);
                        await delRef.DeleteAsync();
                        MessageBox.Show($"Se elimino de la base de datos a {eachItem.Text}" );   
                    }
                    catch (Exception)
                    {
                        throw new Exception("No se pudo Eliminar, problema en la conexion hacia la base de datos");
                       
                    }
                    //Eliminamos visualmente el item
                    listView1.Items.Remove(eachItem);

                }
            }
        }
       


    }
}
