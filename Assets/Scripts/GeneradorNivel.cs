using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorNivel : MonoBehaviour
{
    [SerializeField] private GameObject[] partesNivel;
    [SerializeField] private float distanciaMinima;
    [SerializeField] private Transform puntoFinal;
    [SerializeField] private int cantidadInicial;
    private Transform jugador;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;

        for(int i = 0; i < cantidadInicial; i++)
        {
            Debug.Log("Se ha generado el nivel en el start");
            GenenrarParteNivel();
        }

    }

    void Update()
    {
        //Método para generar las partes del nivel

        /*
        if(Vector2.Distance(jugador.position, puntoFinal.position) < distanciaMinima)
        {
            Debug.Log("Se ha generado el nivel en el update");
            GenenrarParteNivel();
        }
        */
    }

    public void GenenrarParteNivel()
    {
        int numeroAleatorio = Random.Range(0, partesNivel.Length);
        GameObject nivel = Instantiate(partesNivel[numeroAleatorio], puntoFinal.position, Quaternion.identity);
        puntoFinal = BuscarPuntoFinal(nivel, "PuntoFinal");
    }

    private Transform BuscarPuntoFinal(GameObject parteNivel, string etiqueta)
    {
        Transform punto = null;

        foreach(Transform ubicacion in parteNivel.transform)
        {
            if (ubicacion.CompareTag(etiqueta)) {
                punto = ubicacion;
                Debug.Log("Has encontrado el punto final");
                break;
            
            }
        }
        return punto;
    }
}
