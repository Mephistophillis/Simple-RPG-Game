using System;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
  private string fullPath;
  private bool encryptData;
  private string codeWord = "Simple_First_PRG";

  public FileDataHandler(string dataDirPath, string dataFileName, bool encryptData)
  {
    fullPath = Path.Combine(dataDirPath, dataFileName);
    this.encryptData = encryptData;
  }

  public void SaveData(GameData gameData)
  {
    try
    {
      // 1. Create directiory if it doesn't exist
      Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

      // 2. Convert GameData to JSON string
      string dataToSave = JsonUtility.ToJson(gameData, true);

      if (encryptData)
        dataToSave = EncryptDecrypt(dataToSave);

      // 3. Open/Create a new file
      using (FileStream stream = new FileStream(fullPath, FileMode.Create))
      {
        // 4. Write JSON text to the file
        using (StreamWriter write = new StreamWriter(stream))
        {
          write.Write(dataToSave);
        }
      }
    }

    catch (Exception e)
    {
      Debug.LogError("Error on data save to:" + fullPath + "\n" + e);
    }
  }

  public GameData LoadData()
  {
    GameData loadData = null;

    // 1. Check if the save file exists
    if (File.Exists(fullPath))
    {
      try
      {
        string dataToLoad = "";

        // 2. Open the file
        using (FileStream stream = new FileStream(fullPath, FileMode.Open))
        {
          // 3. Read file's text content
          using (StreamReader reader = new StreamReader(stream))
          {
            dataToLoad = reader.ReadToEnd();
          }
        }

        if (encryptData)
          dataToLoad = EncryptDecrypt(dataToLoad);

        // 4. Convert JSON to GameData object
        loadData = JsonUtility.FromJson<GameData>(dataToLoad);
      }

      catch (Exception e)
      {
        Debug.LogError("Error on trying to load data from file: " + fullPath + "\n" + e);
      }
    }

    return loadData;
  }

  public void Delete()
  {
    if (File.Exists(fullPath))
    {
      try
      {
        File.Delete(fullPath);
      }

      catch (Exception e)
      {
        Debug.LogError("Error on trying to delete data from file: " + fullPath + "\n" + e);
      }
    }
  }

  private string EncryptDecrypt(string data)
  {
    string modifiedData = "";

    for (int i = 0; i < data.Length; i++)
    {
      modifiedData += (char)(data[i] ^ codeWord[i % codeWord.Length]);
    }

    return modifiedData;
  }
}
