using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class iniciarJuego : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Comenzar());
        }
    }

    IEnumerator Comenzar()
    {
        // ## codigo para el sonido de inicio ##
        yield return new WaitForSeconds(1f);
        // cargar escena despues de un segundo 
        SceneManager.LoadScene("Pruebas");
    }
}
