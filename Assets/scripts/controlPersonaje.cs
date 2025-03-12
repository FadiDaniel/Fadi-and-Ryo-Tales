using UnityEngine;

public class controlPersonaje : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto= 6f;
    public float longitudRaycast = 0.80f; // linea invisible para detectar colision con el suelo 
    public LayerMask capaSuelo;
    private bool enSuelo;
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
        mover();
    }

    void mover()
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
        // actualiza posicion
        transform.position = new Vector3(velocidadX + posicion.x, posicion.y, posicion.z);
        // detectar si esta colicionando con el suelo
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.down,longitudRaycast,capaSuelo);
        enSuelo = hit.collider != null;
        if (enSuelo && Input.GetKeyDown(KeyCode.UpArrow)) // se salta con la flecha de arriba
        {
            rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
        }



    }
    // dibuja la linea invisible que detecta la colicion con el suelos @Override
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position,transform.position + Vector3.down * longitudRaycast);       
    }
}
