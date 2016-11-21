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
        foreach (instanciaObjeto obj in inventario.listaObjetos)
        {
            GameObject instanciaObjeto = Instantiate(objeto);
            Objeto detalleObjeto = handler.buscarObjetoPorId(obj.id);
            instanciaObjeto.transform.GetComponentInChildren<Text>().text = detalleObjeto.nombre;
            instanciaObjeto.transform.SetParent(this.transform);
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
