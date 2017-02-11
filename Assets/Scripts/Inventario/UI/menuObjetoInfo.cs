using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CommonTools;

public class menuObjetoInfo : MonoBehaviour {

    public int id;
    public int posicion;
    public GameObject objetoUI;
    public GameObject objetoEscenaPrefab;
    private Transform equipoUI = null;

    void Awake()
    {
        Transform child = GameManager.instancia.inventarioUI.transform.GetChild(0);
        equipoUI = CommonTools.GameObjectTools.getChildByName(child, "Equipo");
    }

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

    public void equiparObjeto()
    {
        if (personaje.pj != null)
        {
            personaje.pj.equipaObjeto(this.id);
            if (this.equipoUI != null)
            {
                Sprite sprite = new Sprite();
                GameManager.instancia.spritesObjeto.TryGetValue(id, out sprite);
                GameObject armaObjeto = CommonTools.GameObjectTools.getChildByName(equipoUI, "Arma").gameObject;
                armaObjeto.transform.GetChild(0).GetComponent<Image>().color = new Color(256f, 256f, 256f, 256f);
                armaObjeto.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
                Inventario.inv.borraObjeto(this.id);
                Inventario.inv.addObjeto(personaje.pj.armaPrevia);
                GameManager.instancia.inventarioUI.GetComponentInChildren<cargaObjetos>().addObjeto(personaje.pj.armaPrevia);
            }
            Destroy(objetoUI);
        }
        this.gameObject.SetActive(false);
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
