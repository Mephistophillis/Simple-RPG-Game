using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
  private FileDataHandler dataHandler;
  private GameData gameData;
  private List<ISavable> allSavables;
  [SerializeField] private string fileName = "simple-rpg.json";
  [SerializeField] private bool encryptData = true;

  private IEnumerator Start()
  {
    Debug.Log("path: " + Application.persistentDataPath);
    dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encryptData);
    allSavables = FindAllSavables();

    yield return new WaitForSeconds(.01f);

    LoadGame();
  }

  public void LoadGame()
  {
    gameData = dataHandler.LoadData();

    if (gameData == null)
    {
      Debug.LogWarning("No save file found.");
      gameData = new GameData();
      return;
    }

    foreach (var savable in allSavables)
      savable.LoadData(gameData);
  }

  public void SaveGame()
  {
    foreach (var savable in allSavables)
      savable.SaveData(ref gameData);

    dataHandler.SaveData(gameData);
  }

  [ContextMenu("*** Delete save data ***")]
  public void DeleteSaveData()
  {
    dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encryptData);
    dataHandler.Delete();
  }

  private void OnApplicationQuit()
  {
    SaveGame();
  }

  private List<ISavable> FindAllSavables()
  {
    return FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None)
      .OfType<ISavable>()
      .ToList();
  }
}
