using UnityEngine;
using System.Collections;

public class ObjetoEscena : MonoBehaviour {

    public int id = -1;
    public int cantidad = 1;

    void OnTriggerEnter2D(Collider2D evento)
    {
        if (Inventario.inv.canAdd(id))
        {
            Inventario.inv.addObjeto(id);
            Destroy(gameObject);
        }
    }
}
