using UnityEngine;
using System.Collections;

namespace CommonTools
{
    enum Tipos
    {
        weapon,
        consumible
    }

    public static class GameObjectTools {
        public static Transform getChildByName(Transform trans, string name)
        {
            Transform res = null;
            foreach (Transform child in trans)
            {
                if (child.name == name)
                {
                    res = child;
                }
            }
            return res;
        }

    }

}
