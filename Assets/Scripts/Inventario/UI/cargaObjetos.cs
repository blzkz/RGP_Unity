using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cargaObjetos : MonoBehaviour {

    public GameObject objeto;
    private Inventario inventario;
    private BaseDatosHandler handler;

	// Use this for initialization
	void Start () {
        Debug.Log("start");
        inventario = Inventario.inv;
        handler = GameManager.instancia.GetComponent<BaseDatosHandler>();
        this.pintaObjetos();
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
	// Update is called once per frame
	void Update () {
	
	}
}
