using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using CommonTools;

public class Inventario : MonoBehaviour {

    public static Inventario inv = null;
    public List<instanciaObjeto> listaObjetos;

    internal void borraObjetoPorPosicion(int posicion)
    {
        if (posicion < listaObjetos.Count)
        {
            listaObjetos.Remove(listaObjetos[posicion]);
        }
    }

    private string savePath;
    private string sLlave = "loquequieras";

    [NonSerialized]
    public int maxNumObj = 5;
    [NonSerialized]
    public BaseDatosHandler handler;
    [NonSerialized]
    public GameObject menuObjeto;

    void Awake()
    {
        if (inv == null)
        {
            this.savePath = Application.dataPath + "/Resources/Inventario.sav";
            inv = this;
            if(GameManager.instancia != null)
            {
                handler = GameManager.instancia.GetComponent<BaseDatosHandler>();
            }
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
        CryptoTools.cifrar(inventarioStringified, this.sLlave, this.savePath);
    }

    public void cargarEstado()
    {
        try
        {
            if (File.Exists(this.savePath))
            {
                string datos = CryptoTools.descifrar(this.sLlave, this.savePath);
                JsonUtility.FromJsonOverwrite(datos, this);
            }
        }
        catch(ArgumentException e)
        {
            Debug.LogError(e.Message);
            Debug.LogError("Llave borrada/fichero erróneo");
        }
    }

    public bool canAdd(int id) {
        return this.listaObjetos.Count < this.maxNumObj;
    }

    public void activarMenuObjeto(int id, int posicion, RectTransform rectT, Vector2 eventPos, GameObject objetoUI)
    {
        if (menuObjeto == null)
        {
            menuObjeto = GameManager.instancia.inventarioUI.transform.GetChild(0).Find("MenuObjeto").gameObject;
        }
        menuObjetoInfo menu = menuObjeto.GetComponent<menuObjetoInfo>();
        menu.id = id;
        menu.posicion = posicion;
        menu.objetoUI = objetoUI;
        Rect rec = RectTransformUtility.PixelAdjustRect(rectT, GameManager.instancia.inventarioUI.GetComponent<Canvas>());
        float offsetX = this.menuObjeto.GetComponent<RectTransform>().rect.width / 2;
        float offsetY = this.menuObjeto.GetComponent<RectTransform>().rect.height / 2;
        Vector3 post = new Vector3(eventPos.x + offsetX, eventPos.y - offsetY);
        this.menuObjeto.GetComponent<RectTransform>().position = post;
        this.menuObjeto.SetActive(true);
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
