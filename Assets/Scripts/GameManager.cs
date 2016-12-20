using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GameManager : MonoBehaviour {

    public static GameManager instancia = null;
    public GameObject instanciaPersonaje;
    public GameObject instanciaInventario;
    public GameObject prefabInvUI;
    public GameObject inventarioUI;
    public Dictionary<int, Sprite> spritesObjeto;

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

    void cargaSpritesObjeto()
    {
        spritesObjeto = new Dictionary<int, Sprite>();
        foreach(Objeto obj in Inventario.inv.handler.bd.baseDatos)
        {
            string ruta = Application.dataPath + "/Resources/" + obj.rutaSprite + ".png";
            if (File.Exists(ruta)) {
                Sprite sprite = Resources.Load<Sprite>(obj.rutaSprite);
                spritesObjeto.Add(obj.id, sprite);
            }
        }
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
        cargaSpritesObjeto();
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
