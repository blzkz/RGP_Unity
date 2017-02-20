using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cargaObjetos : MonoBehaviour {

    public GameObject objeto;
    private Inventario inventario;
    private BaseDatosHandler handler;
    private Transform equipoUI = null;

    void Awake()
    {
        Transform child = this.transform.parent.parent;
        equipoUI = child.Find("Equipo");
    }

    // Use this for initialization
    void Start () {
        Debug.Log("start");
        inventario = Inventario.inv;
        handler = GameManager.instancia.GetComponent<BaseDatosHandler>();
        this.pintaObjetos();
        this.pintaArma();
	}
	
    void pintaObjetos()
    {
        int posicion = 0;
        foreach (instanciaObjeto obj in inventario.listaObjetos)
        {
            GameObject instanciaObjeto = Instantiate(objeto);
            eventosObjeto ev = instanciaObjeto.GetComponent<eventosObjeto>();
            ev.id = obj.id;
            ev.posicion = posicion;
            Objeto detalleObjeto = handler.buscarObjetoPorId(obj.id);
            instanciaObjeto.transform.GetComponentInChildren<Text>().text = detalleObjeto.nombre;
            instanciaObjeto.transform.SetParent(this.transform);

            posicion++;
        }
    }

    public void addObjeto(int id)
    {
        if (id > -1)
        {
            GameObject instanciaObjeto = Instantiate(objeto);
            eventosObjeto ev = instanciaObjeto.GetComponent<eventosObjeto>();
            ev.id = id;
            ev.posicion = inventario.handler.bd.baseDatos.Count == 0 ? 0 : inventario.handler.bd.baseDatos.Count - 1;
            Objeto detalleObjeto = handler.buscarObjetoPorId(id);
            instanciaObjeto.transform.GetComponentInChildren<Text>().text = detalleObjeto.nombre;
            instanciaObjeto.transform.SetParent(this.transform);
        }
    }

    private void pintaArma()
    {
        if (personaje.pj != null && personaje.pj.arma != null && personaje.pj.arma.id > -1)
        {
            int id = personaje.pj.arma.id;
            Sprite sprite = new Sprite();
            GameManager.instancia.spritesObjeto.TryGetValue(id, out sprite);
            GameObject armaObjeto = equipoUI.Find("Arma").gameObject;
            armaObjeto.transform.GetChild(0).GetComponent<Image>().color = new Color(256f, 256f, 256f, 256f);
            armaObjeto.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
