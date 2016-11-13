using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(Inventario))]
public class InventarioEditor : Editor {

    int indice = 0;

    public override void OnInspectorGUI()
    {
        Inventario contexto = (Inventario)this.target;
        if (GameManager.instancia != null)
        {
            BaseDatosHandler bbdd = GameManager.instancia.GetComponent<BaseDatosHandler>();
            string[] opciones = new string[bbdd.bd.baseDatos.Count];
            Dictionary<string, int> diccionario = new Dictionary<string, int>();

            foreach (Objeto obj in bbdd.bd.baseDatos)
            {
                diccionario.Add(obj.nombre, obj.id);
            }

            diccionario.Keys.CopyTo(opciones, 0);
            EditorGUILayout.LabelField("Numero objetos: ", contexto.listaObjetos.Count.ToString() + "/" + contexto.maxNumObj);
            EditorGUILayout.LabelField("Objeto", "Cantidad");
            foreach (instanciaObjeto obj in contexto.listaObjetos)
            {
                Objeto objActual = bbdd.buscarObjetoPorId(obj.id);
                EditorGUILayout.LabelField(objActual.nombre, obj.cantidad.ToString());
            }

            indice = EditorGUILayout.Popup(indice, opciones);

            if (GUILayout.Button("Añadir objeto"))
            {
                int idObjeto;
                if (diccionario.TryGetValue(opciones[indice], out idObjeto))
                {
                    contexto.addObjeto(idObjeto);
                }
            }

            if (GUILayout.Button("Borrar objeto"))
            {
                int idObjeto;
                if (diccionario.TryGetValue(opciones[indice], out idObjeto))
                {
                    contexto.borraObjeto(idObjeto);
                }
            }
        }
    }
}
