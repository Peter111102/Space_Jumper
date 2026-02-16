using UnityEngine;

public class test : MonoBehaviour
{
    // Viene eseguito una volta all'inizio
    void Start()
    {
        // Posiziona l'oggetto al centro del mondo all'avvio
        transform.position = new Vector3(0, 0, 0);
        Debug.Log("Oggetto posizionato. Premi SPAZIO per saltare!");
    }

    // Viene eseguito ogni frame
    void Update()
    { 
        // Unity controlla qui se premi il tasto, 60+ volte al secondo
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Salto! Il tasto funziona.");
            
            // Per farlo "saltare" davvero fisicamente, aggiungiamo una spinta:
            transform.Translate(Vector3.up * 2f); 
        }
    }
}