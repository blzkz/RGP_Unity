using UnityEngine;
using System.Collections;
using System;

public class personaje : MonoBehaviour {

    enum Tipos
    {
        weapon,
        consumible
    }

    Rigidbody2D personajeRb;
    Animator personajeAnimator;

    public static personaje pj = null;

    [SerializeField]
    private int vidaTotal;
    [SerializeField]
    private int fuerza;

    public int danoTotal;

    private Objeto arma;

    void Awake()
    {
        this.iniciaEstado();
        if (pj == null)
        {
            pj = this;
        }
        else if (pj != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void iniciaEstado()
    {
        vidaTotal = 10;
        fuerza = 3;
        danoTotal = fuerza;
    }
	// Use this for initialization
	void Start () {
        personajeRb = GetComponent<Rigidbody2D>();
        personajeAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 moviento = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (moviento != Vector2.zero)
        {
            personajeAnimator.SetBool("enMovimiento", true);
            personajeAnimator.SetFloat("x", moviento.x);
            personajeAnimator.SetFloat("y", moviento.y);
        }
        else
        {
            personajeAnimator.SetBool("enMovimiento", false);
        }

        personajeRb.MovePosition(personajeRb.position + moviento * Time.deltaTime * 2);
	}

    public void equipaObjeto(int id)
    {
        BaseDatosHandler bbdd = GameManager.instancia.GetComponent<BaseDatosHandler>();
        Objeto obj = bbdd.buscarObjetoPorId(id);
        if (obj.id != -1)
        {
            Tipos tipo = (Tipos)Enum.Parse(typeof(Tipos), obj.type);
            switch (tipo)
            {
                case Tipos.weapon:
                    this.equipaArma(obj);
                    break;
                default:
                    Debug.Log(obj.type);
                    break;
            }
        }
    }

    private void equipaArma(Objeto nuevaArma)
    {
        Objeto armaPrevia = null;
        if (arma != null)
        {
            armaPrevia = arma;
            this.sueltaArma();
        }

        arma = nuevaArma;
        this.danoTotal = this.fuerza + arma.damage;
    }

    public void sueltaArma()
    {
        this.danoTotal -= arma.damage;
        arma = null;
    }
}
