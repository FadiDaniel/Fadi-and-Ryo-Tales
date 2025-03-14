using UnityEngine;

public class pausarJuego : MonoBehaviour
{
    public GameObject menuPausa;
    public GameObject btnPausa;
    public bool juegoPausado = false;

    void Start()
    {
       btnPausa = GameObject.Find("btnPause");
    }

    // Update is called once per frame
    void Update()
    {   // PAUSA con "esc"
           if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(juegoPausado)
            {
                reanudar();
            }
            else
            {
                pausar();
            }
        }
    }

    public void reanudar()
    {
        menuPausa.SetActive(false);
        btnPausa.SetActive(true);
        // velocidad de ejecucion del juego
        Time.timeScale =1;
        juegoPausado = false;
    }

    public void pausar()
    {
        menuPausa.SetActive(true);
        btnPausa.SetActive(false);
        // congela la ejecucion
        Time.timeScale =0;
        juegoPausado = true;
    }
}
