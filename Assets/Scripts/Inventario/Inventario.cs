using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Inventario : MonoBehaviour {

    public static Inventario inv = null;
    public List<instanciaObjeto> listaObjetos;
    public int maxNumObj = 5;
    public BaseDatosHandler handler = null;

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

    void Start()
    {
        if (GameManager.instancia != null)
        {
            handler = GameManager.instancia.GetComponent<BaseDatosHandler>();
        }
    }

    void cargarInventario()
    {
        listaObjetos = new List<instanciaObjeto>();
    }

    public void addObjeto(int id)
    {
        if (handler.existeObjeto(id))
        {
            if (handler.esAcumlable(id) && enInventario(id))
            {
                listaObjetos.Find(objeto => objeto.id == id).cantidad++;
            }
            else if (canAdd(id))
            {
                listaObjetos.Add(new instanciaObjeto(id, 1));
            }
            else
            {
                Debug.Log("Límite máximo de objetos alcanzado.");
            }
        }
        else
        {
            Debug.Log("Error al añadir el objeto");
        }
    }

    public void borraObjeto(int id)
    {
        if (enInventario(id))
        {
            instanciaObjeto obj = listaObjetos.Find(objeto => objeto.id == id);
            if (obj.cantidad > 1)
            {
                obj.cantidad--;
            }
            else
            {
                listaObjetos.Remove(listaObjetos.Find(x => x.id == id));
            }
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

    public bool canAdd(int id) {
        return this.listaObjetos.Count < this.maxNumObj;
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
