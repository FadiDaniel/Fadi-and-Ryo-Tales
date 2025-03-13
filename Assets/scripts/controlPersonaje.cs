using UnityEngine;

public class controlPersonaje : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto= 7f;
    public float fuerzaRebote= 5.5f;
    public float longitudRaycast = 0.80f; // linea invisible para detectar colision con el suelo 
    public LayerMask capaSuelo;
    private bool enSuelo;
    private bool recibiendoDanio;
    private bool sanando;
    private bool atacando;
    private Rigidbody2D rb;
    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!atacando && !sanando)
        {
            // actualiza posicion y animacion de movimiento
            movimiento();
            // revision para salto
            salto();
        }
        if(!recibiendoDanio && !sanando)
        {
            // revision para ataque
            ataque();
        }
        animaciones();
        
    }
    public void animaciones()
    {
        // de recibiendo daño
        animator.SetBool("herido",recibiendoDanio);
        // de recuperar vida
        animator.SetBool("sanando", sanando);
        // de ataque
        animator.SetBool("atacando", atacando);
    }

    public void atacar()
    {
        atacando = true;
    }

    public void desactivaAtacar() // se llama desde la animacion de ataque
    {
        atacando=false;
    }

    public void recibeSalud()
    {
        sanando = true; 
        // codigo para aumentar corazones
    }

    public void desactivaSanar() // llamado desde el fin de la animacion de sanar
    {
        sanando = false;
    }

    public void recibeDanio(Vector2 direccion, int cantidadDanio) // cuando el jugador es golpeado, el controlador del enemigo llama a esta funcion
    {
        if(!recibiendoDanio)
        {
            recibiendoDanio = true;
            Vector2 rebote = new Vector2(transform.position.x - direccion.x, 0.8f).normalized;
            rb.AddForce(rebote*fuerzaRebote,ForceMode2D.Impulse);
        }
    }

    public void desactivaDanio() // llamado desde el fin de la animacion de daño
    {
        recibiendoDanio = false;
        // para cancelar el impulso cuando se acabe el daño
        rb.linearVelocity = Vector2.zero;
    }

    void movimiento()
    {
        // obtiene el desplazamiento actual para pasarselo al animator y que haga la animacion correspondiente, aparte de actualizar la posicion del personaje 
        float velocidadX = Input.GetAxis("Horizontal") * Time.deltaTime*velocidad;
        animator.SetFloat("movimiento", velocidadX*velocidad);
        if (velocidadX > 0) // girar a la derecha
        {
            transform.localScale = new Vector3(1,1,1);
        }
        if(velocidadX < 0) // girar a la izquierda
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        Vector3 posicion = transform.position;
        // MOVIMIENTO actualiza posicion si no se esta recibiendo daño
        if(!recibiendoDanio)
        {
            transform.position = new Vector3(velocidadX + posicion.x, posicion.y, posicion.z);
        }
        // cambia animacion si se esta en el suelo o no
        animator.SetBool("enSuelo",enSuelo);
    }
    public void salto()
    {
        // detectar si esta colicionando con el suelo
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.down,longitudRaycast,capaSuelo);
        enSuelo = hit.collider != null;
        // SALTAR con la flecha de arriba si no se esta recibiendo daño y se toca el suelo
        if (enSuelo && Input.GetKeyDown(KeyCode.UpArrow) && !recibiendoDanio) 
        {
            rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
        }
    }
    public void ataque()
    {
        // ATAQUE   si no esta atacando y esta en el suelo
        if(Input.GetKeyDown(KeyCode.Space) && !atacando && enSuelo)
        {
            atacar();
        }
    }

    // dibuja la linea invisible que detecta la colicion con el suelos @Override
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position,transform.position + Vector3.down * longitudRaycast);       
    }
}
