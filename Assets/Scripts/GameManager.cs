using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instancia = null;
    public GameObject instanciaPersonaje;
    public GameObject instanciaInventario;
    public GameObject prefabInvUI;
    private GameObject inventarioUI;

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

    void toggleInventario()
    {
        if (Inventario.inv == null)
        {
            iniciaInventario();
        }

        if (inventarioUI == null)
        {
            Time.timeScale = 0.0f;
            inventarioUI = Instantiate(prefabInvUI);
        }
        else
        {
            Destroy(inventarioUI);
            Time.timeScale = 1.0f;
        }
    }

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void LateUpdate ()
    {
        if (Input.GetButtonDown("Inventario"))
        {
            toggleInventario();
        }
    }

    void OnDestroy()
    {
        Inventario.inv.guardarEstado();
    }
}
