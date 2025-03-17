using UnityEngine;

public class pausarJuego : MonoBehaviour
{
    public GameObject menuPausa;
    public GameObject btnPausa;
    public bool juegoPausado = false;
    public AudioSource musica;

    void Start()
    {
       btnPausa = GameObject.Find("btnPause");
       musica =  GetComponent<AudioSource>();
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
        musica.Play();
        menuPausa.SetActive(false);
        btnPausa.SetActive(true);
        // velocidad de ejecucion del juego
        Time.timeScale =1;
        juegoPausado = false;
    }

    public void pausar()
    {
        musica.Pause();
        menuPausa.SetActive(true);
        btnPausa.SetActive(false);
        // congela la ejecucion
        Time.timeScale =0;
        juegoPausado = true;
    }
}
