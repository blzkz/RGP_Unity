using UnityEngine;
using System.Collections;
using System.IO;

public class BaseDatosHandler : MonoBehaviour {

    public BaseDatosObjeto bd;

	// Use this for initialization
	void Start () {
        string datos = File.ReadAllText(Application.dataPath + "/Resources/objetos.json");
        bd = JsonUtility.FromJson<BaseDatosObjeto>(datos);
	}

    public Objeto buscarObjetoPorId(int id)
    {
        return bd.baseDatos.Find(objeto => objeto.id == id);
    }

    public bool existeObjeto(int id)
    {
        return bd.baseDatos.Exists(objeto => objeto.id == id);
    }

    public bool esAcumlable(int id)
    {
        Objeto obj = bd.baseDatos.Find(objeto => objeto.id == id);
        return obj.acumulable;
    }
}
