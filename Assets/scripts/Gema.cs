using UnityEngine;

public class Gema : MonoBehaviour
{
    // detecta colisiones 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // solo con el jugador
        if(collision.CompareTag("Caballero"))
        {
            collision.gameObject.GetComponent<controlPersonaje>().recibeSalud();
            Destroy(gameObject);
        }
    }
}
