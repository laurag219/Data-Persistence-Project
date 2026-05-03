using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;            // Necesario para InputField
using UnityEngine.SceneManagement;
using TMPro;


#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    // Cambio 6: Reemplazar ColorPicker por InputField
    public TMP_InputField NameInputField;   // antes: public ColorPicker ColorPicker;

    // Cambio 7: Método que se llama cuando el usuario cambia el texto
    public void OnNameChanged(string newName)
    {
        MainManagerMenu.Instance.PlayerName = newName;
    }

    private void Start()
    {
        // Cambio 8: Inicializar el InputField con el nombre guardado
        if (MainManagerMenu.Instance != null && NameInputField != null)
        {
            NameInputField.text = MainManagerMenu.Instance.PlayerName;
            NameInputField.onValueChanged.AddListener(OnNameChanged);
        }
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        MainManagerMenu.Instance.SaveGameData();    // Cambio 9: guardar el nombre al salir
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void SaveNameClicked()
    {
        MainManagerMenu.Instance.SaveGameData();
    }

    public void LoadNameClicked()
    {
        MainManagerMenu.Instance.LoadGameData();
        // Actualizar el campo de texto con el nombre recién cargado
        if (NameInputField != null)
            NameInputField.text = MainManagerMenu.Instance.PlayerName;
    }
}
