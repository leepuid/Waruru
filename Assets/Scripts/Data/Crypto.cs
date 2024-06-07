using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class Crypto : MonoBehaviour
{
    // 암호화 key
    private static readonly byte[] aesKey = Encoding.UTF8.GetBytes("ehalshrkdhkfmfmb");
    // 암호화 IV
    private static readonly byte[] aesIVKey = Encoding.UTF8.GetBytes("wltmdwhdgncjfdns");

    // 암호화 후, 암호화 데이터를 저장하는 함수.
    public static void SaveEncryptedData(string keyName, string data)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = aesKey;
            aesAlg.IV = aesIVKey;

            // 암호화 Key와 IV를 이용하여 암호화를 진행할 encryptor 생성.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            byte[] encryptedData = null;

            // 일반 데이터 암호화.
            byte[] bytesToEncrypt = Encoding.UTF8.GetBytes(data);
            encryptedData = encryptor.TransformFinalBlock(bytesToEncrypt, 0, bytesToEncrypt.Length);

            // 암호화 데이터를 문자열로 변환하여 저장 .
            string encryptedString = Convert.ToBase64String(encryptedData);
            PlayerPrefs.SetString(keyName, encryptedString);
            PlayerPrefs.Save();
        }
    }

    // 복호화 후, 복호화 데이터를 반환하는 함수.
    public static string LoadEncryptedData(string keyName)
    {
        string encryptedString = PlayerPrefs.GetString(keyName);
        if (!string.IsNullOrEmpty(encryptedString))
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = aesKey;
                aesAlg.IV = aesIVKey;

                //암호화 key와 IV를 이용하여 복호화를 진행할 decryptor 생성.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // 데이터 복호화.
                byte[] encryptedData = Convert.FromBase64String(encryptedString);
                byte[] decryptedData = decryptor.TransformFinalBlock(encryptedData, 0, encryptedData.Length);

                // 복호화 데이터를 이용하여 저장된 데이터 반환.
                return Encoding.UTF8.GetString(decryptedData);
            }
        }
        else
        {
            return null;
        }
    }
}
