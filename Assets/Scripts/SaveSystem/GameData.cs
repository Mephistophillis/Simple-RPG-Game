using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public class GameData
{
  public int gold;
  public List<Inventory_Item> itemList;
  public SerializedDictionary<string, int> inventory;

  public GameData()
  {
    inventory = new SerializedDictionary<string, int>();
  }
}
