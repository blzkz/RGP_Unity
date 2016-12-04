using UnityEngine;
using System.Collections;

public class menuObjetoInfo : MonoBehaviour {

    public int id;
    public int posicion;
    public GameObject objetoUI;

    public void soltarObjeto()
    {
        Inventario.inv.borraObjetoPorPosicion(posicion);
        Destroy(objetoUI);
        this.gameObject.SetActive(false);
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
