using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventario : MonoBehaviour {

    public static Inventario inv = null;
    public List<instanciaObjeto> listaObjetos;

    void Awake()
    {
        if (inv == null)
        {
            inv = this;
        }
        else if (inv != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void cargarInventario()
    {
        listaObjetos = new List<instanciaObjeto>();
    }

    public void addObjeto(int id)
    {
        if (GameManager.instancia.GetComponent<BaseDatosHandler>().existeObjeto(id))
        {
            listaObjetos.Add(new instanciaObjeto(id, 1));
        }
    }

    public void borraObjeto(int id)
    {
        if (enInventario(id))
        {
            listaObjetos.Remove(listaObjetos.Find(x => x.id == id));
        }
    }

    public bool enInventario(int id)
    {
        return listaObjetos.Exists(x => x.id == id);
    }

}

[System.Serializable]
public class instanciaObjeto
{
    public int id;
    public int cantidad;

    public instanciaObjeto(int objId = -1, int objCantidad = 0)
    {
        id = objId;
        cantidad = objCantidad;
    }
}
