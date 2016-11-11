using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instancia = null;
    public GameObject instanciaPersonaje;
    public GameObject instanciaInventario;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else if (instancia != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        iniciaPersonaje();
        iniciaInventario();
    }

    void iniciaPersonaje()
    {
        if (personaje.pj == null)
        {
            Instantiate(instanciaPersonaje);
        }

    }

    void iniciaInventario()
    {
        if (Inventario.inv == null)
        {
            Instantiate(instanciaInventario);
        }
        Inventario.inv.cargarEstado();

    }
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDestroy()
    {
        Inventario.inv.guardarEstado();
    }
}
