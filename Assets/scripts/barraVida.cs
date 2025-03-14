using UnityEngine;
using UnityEngine.UI;

public class barraVida : MonoBehaviour
{
    public Image rellenoVida;
    private controlPersonaje scriptCaballero;
    private float vidaMaxima;

    void Start()
    {
        scriptCaballero = GameObject.Find("Caballero").GetComponent<controlPersonaje>();    
        vidaMaxima = scriptCaballero.vida; // adaptar la barra de vida 
    }

    // Update is called once per frame
    void Update()
    {
        rellenoVida.fillAmount = scriptCaballero.vida / vidaMaxima;
    }
}
