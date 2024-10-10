using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerControler : MonoBehaviour
{
    public float speed = 5.0f;
    public GameObject hasPerdido;

    public int puntos;
    public TextMeshProUGUI textoPuntos;

    public Vector2 posicionInicial = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        hasPerdido.SetActive(false);
        puntos = 0;
        transform.position = posicionInicial;
    }

    // Update is called once per frame
    void Update()
    {
        float tilt = Input.acceleration.x;
        transform.Translate(tilt * speed * Time.deltaTime, 0, 0);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trampa")
        {
            hasPerdido.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Punto"))
        {
            puntos++;
            textoPuntos.text = puntos.ToString();

            //textoRecord.text = "Record: " + record.ToString();
        }
    }

    public void Repetir()
    {
        hasPerdido.SetActive(false);
        Time.timeScale = 1;
        puntos = 0;
        textoPuntos.text = "00";
        transform.position = posicionInicial;
    }

    public void SalirJuego()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
