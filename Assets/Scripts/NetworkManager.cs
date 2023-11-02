using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Clase para la conexión con la base de datos y la creación de usuarios
public class NetworkManager : MonoBehaviour
{
    //Creacion de usuario
    public void CreateUser(string userName, string email, string pass, Action<response> response){
        StartCoroutine( CO_CreateUser( userName, email, pass, response) );

    }

    //Coroutina para enviar los datos de usuario por formulario
    private IEnumerator CO_CreateUser(string userName, string email, string pass, Action<response> response){

        WWWForm form = new WWWForm(); //Nuevo Formulario

        form.AddField("userName", userName); //user
        form.AddField("email", email); //email
        form.AddField("pass", pass); //password

        WWW w = new WWW("http://localhost/Game/createUser.php", form); //Referencia a archivo php para enviar información de registro

        yield return w; //Esperamos respuesta

        response(JsonUtility.FromJson<response>(w.text));
    }
}

//Mensajes para comunicarnos con el usuario
[SerializeField]
public class response
{
    public bool done = false; //Indicamos si esta activo el mensaje de error
    public string message = ""; //Pasamos el mensaje de error
}
