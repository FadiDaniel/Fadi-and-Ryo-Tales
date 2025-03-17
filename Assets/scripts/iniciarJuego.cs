using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class iniciarJuego : MonoBehaviour
{
    AudioSource musica;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musica = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            musica.Play();
            StartCoroutine(Comenzar());
        }
    }

    IEnumerator Comenzar()
    {
        // ## codigo para el sonido de inicio ##
        yield return new WaitForSeconds(1.5f);
        // cargar escena despues de un segundo 
        SceneManager.LoadScene("Nivel_1F");
    }
}
