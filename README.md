# Registro-de-Horas-Extras-y-Cambios-de-turno

<h3> Aplicación de Registro de Horas Extras y Cambios de turno. </br>
Programado en <b>C#</b>, dando uso a Cloud FireStore de Google. </h3>

<h6>Software Utilizado para el registro de horas extras y cambios de turno.<h6>

Para el almacenamiento de datos hace uso de Cloud Firestore(GOOGLE), para la utilizacion es necesario modificar dos constantes:</br>
Dentro de FORM1.cs</br>
<b> public  static String DB = ""; //DATABASE (Ej: name-34341df") </b></br>
<b>public  static String COLLECTION = ""; // COLLECTION NAME.</b></br>

Modificar los valores por los generados en Cloud Firestore.
</br>

Para modificar los compañeros, solamente agregar dentro del array nuevos valores:

String[] listaCompanieros = new String[] { "Compañero 1", "Compañero 2", "Compañero 3", <b>"NUEVO VALOR"</b>}; 



![alt text](https://raw.githubusercontent.com/sferreyr/Horas-Extras-y-Cambios/master/Registro%5B1%5D.png?token=AKQQTAM2CGN7IQYLQ3MQ57LBMHQHU)

