using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;

namespace Horas_Extras_y_Cambios.Models
{
    [FirestoreData]
 public class Cambios
    {
        [FirestoreProperty]
        public String persona
        { get; set; }

        [FirestoreProperty]
        public String tipoCambio
        { get; set; }

        [FirestoreProperty]
        public String turno
        { get; set; }

        [FirestoreProperty]
        public String fecha { get; set; }

        [FirestoreProperty]
        public String porcentHoras { get; set; }

        [FirestoreProperty]
        public short cantHoras{ get; set; }


        [FirestoreProperty]
        public String diaFranco { get; set; }

        [FirestoreProperty]
        public String masInformacion { get; set; }

        public static implicit operator Cambios(List<Cambios> v)
        {
            throw new NotImplementedException();
        }

        /*   public Cambios(String Persona, String TipoCambio, String Turno, String fecha, short cantHoras)
           {
               this.Persona = Persona;
               this.TipoCambio = TipoCambio;
               this.Turno = Turno;
               this.fecha = fecha;
               this.cantHoras = cantHoras;
           }
           */


    }
}
