using UnityEngine;

public class trampa : MonoBehaviour
{
       // COLISION CONTRA PERSONAJE
    private void OnCollisionEnter2D(Collision2D collision)
    {   // compara si la colision se hizo contra un objeto etiquetado como caballero
        if (collision.gameObject.CompareTag("Caballero"))
        {
            Vector2 direccionDanio = new Vector2(transform.position.x,0);
            // guarda el acceso al script del Caballero en una variable
            controlPersonaje scriptCaballero = collision.gameObject.GetComponent<controlPersonaje>();
            // llama a una funcion del personaje Caballero para hacerle daño, se le pasa la direccion y la cantidad de daño
            scriptCaballero.recibeDanio(direccionDanio,1);  
        }
    }
}
