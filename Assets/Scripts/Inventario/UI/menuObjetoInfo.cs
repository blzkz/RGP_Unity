using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class menuObjetoInfo : MonoBehaviour {

    public int id;
    public int posicion;
    public GameObject objetoUI;
    public GameObject objetoEscenaPrefab;

    public void soltarObjeto()
    {
        Inventario.inv.borraObjetoPorPosicion(posicion);
        GameObject objeto = Instantiate(objetoEscenaPrefab);
        objeto.name = "objeto_" + id.ToString();
        Sprite sprite = new Sprite();
        if (GameManager.instancia.spritesObjeto.TryGetValue(id, out sprite))
        {
            objeto.GetComponent<SpriteRenderer>().sprite = sprite;
        }
        else
        {
            Debug.Log("Sprite no existe en el diccionario");
        }
        objeto.GetComponent<ObjetoEscena>().id = id;
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
