using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instancia = null;
    public GameObject instanciaPersonaje;

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
    }

    void iniciaPersonaje()
    {
        Instantiate(instanciaPersonaje);
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
