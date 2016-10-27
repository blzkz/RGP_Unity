using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class Objeto : ISerializationCallbackReceiver {

    public int id;
    public string nombre;
    public int value;
    public int damage;
    public int weight;
    public string type;
    public string nombreSprite;
    public string rutaSprite;

    void ISerializationCallbackReceiver.OnBeforeSerialize() {}

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        this.rutaSprite = "Sprites/" + nombreSprite;
    }
}

[System.Serializable]
public class BaseDatosObjeto
{
    public List<Objeto> baseDatos;
}