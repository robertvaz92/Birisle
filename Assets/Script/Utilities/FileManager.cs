using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class FileManager
{
    private string m_filePath;
    private int m_encryptHash;

    public FileManager()
    {
        m_encryptHash = 20251006;
    }

    public string readFile(string fileName)
    {
        string retVal = "";
        m_filePath = Path.Combine(Application.persistentDataPath, fileName);

        // Does the file exist?
        if (File.Exists(m_filePath))
        {
            // Read the entire file and save its contents.
            retVal = File.ReadAllText(m_filePath);
            retVal = EncryptDecrypt(retVal);
        }
        return retVal;
    }

    public void writeFile(string fileName, string jsonString)
    {
        jsonString = EncryptDecrypt(jsonString);
        m_filePath = Path.Combine(Application.persistentDataPath, fileName);
        // Write JSON to file.
        File.WriteAllText(m_filePath, jsonString);
    }


    private string EncryptDecrypt(string textToRobs)
    {
        StringBuilder inSb = new StringBuilder(textToRobs);
        StringBuilder outSb = new StringBuilder(textToRobs.Length);
        char c;
        for (int i = 0; i < textToRobs.Length; i++)
        {
            c = inSb[i];
            c = (char)(c ^ m_encryptHash);
            outSb.Append(c);
        }

        return outSb.ToString();
    }
}
