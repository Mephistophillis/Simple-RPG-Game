using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public class GameData
{
  public int gold;
  public List<Inventory_Item> itemList;
  public SerializedDictionary<string, int> inventory; // itemSaveId -> stackSize
  public SerializedDictionary<string, int> storageItems;
  public SerializedDictionary<string, int> storageMaterials;

  public SerializedDictionary<string, ItemType> equipedItems; // itemSaveId -> slotType

  public GameData()
  {
    inventory = new SerializedDictionary<string, int>();
    storageItems = new SerializedDictionary<string, int>();
    storageMaterials = new SerializedDictionary<string, int>();

    equipedItems = new SerializedDictionary<string, ItemType>();
  }
}
