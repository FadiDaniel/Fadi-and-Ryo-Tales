using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class controlCamara : MonoBehaviour
{
    public Transform objetivo;
    public float velocidadCamara = 0.025f;
    public Vector3 desplazamiento;

    private void LateUpdate()
    {
        if (objetivo.position.y <= -5)
        {
            // La camara se queda en su ultima posicion valida
            return;
        }

        Vector3 posicionDeseada = objetivo.position + desplazamiento;
        // efecto de suavizado en el movimiento de la camara 
        Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, velocidadCamara);
        
        // Mantenemos la posición Z fija
        transform.position = new Vector3(posicionSuavizada.x, posicionSuavizada.y, transform.position.z);
    } 
}
