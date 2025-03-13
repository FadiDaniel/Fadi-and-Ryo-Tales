using System.Collections;
using UnityEngine;

public class controlEnemigo : MonoBehaviour
{
    public Transform jugador;
    public float radioDeteccion = 7.7f;
    public float velocidad = 13f;
    public float fuerzaRebote = 8f;
    private Rigidbody2D rb;
    private Vector2 movimiento;
    private bool enMovimiento;
    private bool recibiendoDanio;
    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       DetectarYSeguir();
       animaciones();
    }

    void animaciones()
    {
        animator.SetBool("herido",recibiendoDanio);
    }

    // COLISION CONTRA PERSONAJE
    private void OnCollisionEnter2D(Collision2D collision)
    {   // compara si la colision se hizo contra un objeto etiquetado como caballero
        if (collision.gameObject.CompareTag("Caballero"))
        {
            Vector2 direccionDanio = new Vector2(transform.position.x,0);
            // llama a una funcion del personaje Caballero para hacerle daño  
            collision.gameObject.GetComponent<controlPersonaje>().recibeDanio(direccionDanio,1); // se le pasa la direccion y la cantidad de daño
        }
    }

    // COLISION CONTRA ESPADA
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Espada"))
        {
            Vector2 direccionDanio = new Vector2(collision.gameObject.transform.position.x,0);
            recibeDanio(direccionDanio,1); 
        }
    }

     public void recibeDanio(Vector2 direccion, int cantidadDanio) 
    {
        if(!recibiendoDanio)
        {
            recibiendoDanio = true;
            Vector2 rebote = new Vector2(transform.position.x - direccion.x, 0.8f).normalized;
            rb.AddForce(rebote*fuerzaRebote,ForceMode2D.Impulse);
            StartCoroutine(desactivarDanio());
        }
    }
    
    // Corutina para desactivar el daño (pudimos llamar a una funcion al final de la animacion de daño pero queriamos intentar con esto)
    IEnumerator desactivarDanio()
    {
        yield return new WaitForSeconds(0.8f);
        recibiendoDanio = false;
        rb.linearVelocity = Vector2.zero;
    }

    void DetectarYSeguir() // calcula la diostancia del jugador y compara si esta dentro del rango de deteccion para seguirlo 
    {
    float distanciaDeJugador = Vector2.Distance(transform.position, jugador.position);
        if(distanciaDeJugador < radioDeteccion)
        {
            // sigue al jugador
            Vector2 direction = (jugador.position - transform.position).normalized;

            // gira en la direccion a la que camina
            if (direction.x > 0) // a la derecha
            {
                transform.localScale = new Vector3(1,1,1);
            }
            if(direction.x < 0) // a la izquierda
            {
                transform.localScale = new Vector3(-1,1,1);
            }

            movimiento = new Vector2(direction.x,0);
            // activar animacion animacion
            enMovimiento = true;
        }
        else
        {
            movimiento = Vector2.zero;
            enMovimiento = false;
        }
        if(!recibiendoDanio)
        {
            rb.MovePosition(rb.position + movimiento * velocidad * Time.deltaTime);
        }
        animator.SetBool("enMovimiento",enMovimiento);
    }

    // funcion que ayuda a ver el rango de deteccion del enemigo 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,radioDeteccion);  
    }
}
