using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

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

    public void guardarEstado()
    {
        string inventarioStringified = JsonUtility.ToJson(this);
        File.WriteAllText(Application.dataPath + "/Resources/Inventario.json", inventarioStringified);
    }

    public void cargarEstado()
    {
        if (File.Exists(Application.dataPath + "/Resources/Inventario.json")) {
            string datos = File.ReadAllText(Application.dataPath + "/Resources/Inventario.json");
            JsonUtility.FromJsonOverwrite(datos, this);
        }
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
