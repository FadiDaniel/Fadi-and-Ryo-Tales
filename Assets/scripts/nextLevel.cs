using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Caballero"))
        {
            controlPersonaje scriptCaballero = collision.gameObject.GetComponent<controlPersonaje>();
            scriptCaballero.meditar();
            StartCoroutine(cargarSigienteNivel());
        }
    } 
    IEnumerator cargarSigienteNivel()
    {
        yield return new WaitForSeconds(2.5f);
        // carga la escena siguiente a la que estamos
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
}
