using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using System;
using System.IO;
using System.Text;

namespace CommonTools
{
    enum Tipos
    {
        weapon,
        consumible
    }

    // DO NOT USE, use instead Find()
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

    public static class CryptoTools
    {
        public static void cifrar(string datosACifrar, string sLlave, string SRutaFichero)
        {
            byte[] stringParaCifrar = UTF8Encoding.UTF8.GetBytes(datosACifrar);
            byte[] keyArray;

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(sLlave));
            hashmd5.Clear();

            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform desencrypt = tdes.CreateEncryptor();
            byte[] cadenaCifrada = desencrypt.TransformFinalBlock(stringParaCifrar, 0, stringParaCifrar.Length);

            File.WriteAllText(SRutaFichero, Convert.ToBase64String(cadenaCifrada));
            tdes.Clear();
        }

        public static string descifrar(string sLlave, string SRutaFichero)
        {
            string resultado = "";
            if (File.Exists(SRutaFichero))
            {
                string contenidoCifrado = File.ReadAllText(SRutaFichero);

                byte[] keyArray;
                byte[] arrayCifrado = Convert.FromBase64String(contenidoCifrado);

                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(sLlave));
                hashmd5.Clear();

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform desencrypt = tdes.CreateDecryptor();
                
                resultado = UTF8Encoding.UTF8.GetString(desencrypt.TransformFinalBlock(arrayCifrado, 0, arrayCifrado.Length));
                tdes.Clear();
            }
            return resultado;
        }
    }

}
