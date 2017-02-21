using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CommonTools;
using System;

public class statsUI : MonoBehaviour {

    private Transform damagePanel = null;

    void Awake()
    {
        if (damagePanel == null)
        {
            damagePanel = this.transform.Find("Damage");
        }
    }
	// Use this for initialization
	void Start () {
        updateStats();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void updateStats()
    {
        updatePanel(damagePanel, personaje.pj.danoTotal);
    }

    public void updateItem(int newObjId, int formerObjectId)
    {
        BaseDatosHandler bbdd = GameManager.instancia.GetComponent<BaseDatosHandler>();
        Objeto newObj = bbdd.buscarObjetoPorId(newObjId);
        Objeto formerObject = bbdd.buscarObjetoPorId(formerObjectId);

        Tipos tipo = (Tipos)Enum.Parse(typeof(Tipos), newObj.type);

        switch (tipo)
        {
            case Tipos.weapon:
                int delta = 0;
                if (formerObjectId > -1)
                {
                    delta = newObj.damage - formerObject.damage;
                } else
                {
                    delta = newObj.damage;
                }
                updatePanel(damagePanel, personaje.pj.danoTotal, delta);
                break;
            default:
                Debug.Log(newObj.type);
                break;
        }
    }

    public void updatePanel(Transform parent, int newValue, int delta = 0)
    {
        damagePanel.Find("value").GetComponent<Text>().text = newValue.ToString();
        if (delta > 0)
        {
            Text deltaComp = damagePanel.Find("delta").GetComponent<Text>();
            deltaComp.color = Color.green;
            deltaComp.text = "▲ " + delta.ToString();
        } else if (delta < 0)
        {
            Text deltaComp = damagePanel.Find("delta").GetComponent<Text>();
            deltaComp.color = Color.red;
            deltaComp.text = "▼ " + delta.ToString();
        }
    }
}
