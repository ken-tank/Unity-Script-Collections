using UnityEngine;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New JSON Data", menuName = "JSON Data")]
public class JSON_Data : ScriptableObject
{
    public string fileName;
    public Data data;

    Data currentData;
    
    [HideInInspector] public string path => Application.persistentDataPath + "/Data/" + fileName + ".dat";

    string key = "AB452FE321FFAAEEAC425531236778F2";
    string iv = "3241563845125263";
    
    public void Create(Data target)
    {
        string json = JsonUtility.ToJson(target);

        json = Encrypt(json);

        FileInfo file = new FileInfo(path);
        file.Directory.Create();
        File.WriteAllText(file.FullName, json);
        Intialize();
    }

    public void Intialize() 
    {
        currentData = Load();
    }

    public void Delete() 
    {
        FileInfo file = new FileInfo(path);
        File.Delete(file.FullName);
    }

    public Data Load()
    {
        FileInfo file = new FileInfo(path);
        string json = Decrypt(File.ReadAllText(file.FullName));

        Data o = JsonUtility.FromJson<Data>(json);
        return o;
    }

    public Field GetField(string keyName, string defaultValue = "0")
    {
        bool found = false;
        Field a = null;
        Data d = currentData;
        foreach (var item in d.field)
        {
            if (item.key == keyName) 
            {
                a = item;
                found = true;
            }
        }

        if (!found)
        {
            SetField(keyName, defaultValue);
        }

        return found ? a : GetField(keyName);
    }

    public void SetField(string keyName, string value)
    {
        Data a = currentData;
        bool found = false;
        foreach (var item in a.field)
        {
            if (item.key == keyName)
            {
                item.value = value;
                found = true;
            }
        }
        if (!found)
        {
            Field newField = new Field();
            newField.key = keyName;
            newField.value = value;
            a.field.Add(newField);
        }

        //Create(a);
    }

    public void Save() 
    {
        Create(currentData);
    }

    public bool CheckFile()
    {
        bool exists = File.Exists(path);
        if (exists) Intialize();
        else Create(data);

        return exists;
    }

    string Encrypt(string text) 
    {
        Aes aes = Aes.Create();
        aes.BlockSize = 128;
        aes.KeySize = 256;
        aes.Key = ASCIIEncoding.ASCII.GetBytes(key);
        aes.IV = ASCIIEncoding.ASCII.GetBytes(iv);
        aes.Padding = PaddingMode.PKCS7;

        byte[] txByteData = ASCIIEncoding.ASCII.GetBytes(text);
        ICryptoTransform trnfm = aes.CreateEncryptor(aes.Key, aes.IV);

        byte[] result = trnfm.TransformFinalBlock(txByteData, 0, txByteData.Length);
        return Convert.ToBase64String(result);
    }

    string Decrypt(string text) 
    {
        Aes aes = Aes.Create();
        aes.BlockSize = 128;
        aes.KeySize = 256;
        aes.Key = ASCIIEncoding.ASCII.GetBytes(key);
        aes.IV = ASCIIEncoding.ASCII.GetBytes(iv);
        aes.Padding = PaddingMode.PKCS7;

        byte[] txByteData = Convert.FromBase64String(text);
        ICryptoTransform trnfm = aes.CreateDecryptor();

        byte[] result = trnfm.TransformFinalBlock(txByteData, 0, txByteData.Length);
        return ASCIIEncoding.ASCII.GetString(result);
    }
}

[System.Serializable]
public class Field
{
    public string key;
    public string value;
}

[System.Serializable]
public class Data
{
    public List<Field> field;
}