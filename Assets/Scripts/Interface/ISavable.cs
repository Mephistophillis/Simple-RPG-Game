using UnityEngine;

public interface ISavable
{
  public void LoadData(GameData data);
  public void SaveData(ref GameData data);
}
