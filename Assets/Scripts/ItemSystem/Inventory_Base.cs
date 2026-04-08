using System.Collections.Generic;
using UnityEngine;

public class Inventory_Base : MonoBehaviour
{
  public int maxInventorySize = 10;
  public List<Inventory_Item> inventory = new List<Inventory_Item>();

  public bool CanAddItem() => inventory.Count < maxInventorySize;

  public void AddItem(Inventory_Item item)
  {
    inventory.Add(item);
  }
}
