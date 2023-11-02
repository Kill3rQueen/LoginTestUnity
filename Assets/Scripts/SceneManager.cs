using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SceneManager : MonoBehaviour
{
    //Definición de variables a usar, es este caso los campos de usuario
    [SerializeField] private InputField m_userNameInput     = null;
    [SerializeField] private InputField m_emailInput        = null;
    [SerializeField] private InputField m_password        = null;
    [SerializeField] private InputField m_reEnterPassword        = null;
    [SerializeField] private Text m_text           = null;

    //Referencias a los gameObject que componen el menú de registro y login
    [SerializeField] private GameObject m_registerUI = null;
    [SerializeField] private GameObject m_loginUI = null;

    //LLamamos al network manager
    private void Awake() 
    {
        m_networkManager = GameObject.FindObjectOfType<NetworkManager>();    
    }

    //Comprobamos los campos rellenos en el formulario
    public void SubmitRegister()
    {
        if (m_userNameInput.text == "" || m_emailInput.text == "" || m_password == "" || m_reEnterPassword.text == "")
        {
            m_text.text = "Por favor rellene todos los campos";
            return;
        }

        if (m_password.text == m_reEnterPassword.text)
        {
            m_text.text = "Procesando...";

            m_networkManager.CreateUser(m_userNameInput.text, m_emailInput.text, m_password.text, delegate(Response response)
            {
                m_text.text = response.message;
            });
        } else
        {
            m_text.text = "Contraseñas diferentes por favor verifique";
        }
    }

    //Mostrar y ocultar tanto el menú de login como del de register
    public void ShowLogin(){
        m_registerUI.SetActive(false);
        m_loginUI.SetActive(true);
    }

    public void ShowRegister(){
        m_registerUI.SetActive(true);
        m_loginUI.SetActive(false);
    }
}
