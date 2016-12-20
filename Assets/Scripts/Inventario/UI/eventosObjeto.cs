using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class eventosObjeto : MonoBehaviour, IPointerClickHandler {

    public int posicion;
    public int id;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button.Equals(PointerEventData.InputButton.Right))
        {
            Inventario.inv.activarMenuObjeto(id, posicion, this.GetComponent<RectTransform>(), eventData.position, this.gameObject);
        }
    }
}
