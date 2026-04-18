using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Base : MonoBehaviour
{
  public event Action OnInventoryChange;
  public int maxInventorySize = 10;
  public List<Inventory_Item> itemList = new List<Inventory_Item>();

  protected virtual void Awake()
  {
    
  }

  public bool CanAddItem() => itemList.Count < maxInventorySize;
  public Inventory_Item CanAddToStack(Inventory_Item itemToAdd)
  {
    List<Inventory_Item> stackableItems =
      itemList.FindAll(item => item.itemData == itemToAdd.itemData);

    foreach (var item in stackableItems)
    {
      if (item.CanAddStack())
        return item;
    }

    return null;
  }

  public void AddItem(Inventory_Item item)
  {
    Inventory_Item itemInInventory = CanAddToStack(item);
    
    if (itemInInventory != null)
      itemInInventory.AddStack();
    else
      itemList.Add(item);

    OnInventoryChange?.Invoke();
  }

  public void RemoveItem(Inventory_Item itemToRemove)
  {
    itemList.Remove(FindItem(itemToRemove.itemData));
    OnInventoryChange?.Invoke();
  }

  public Inventory_Item FindItem(ItemDataSO itemData)
  {
    return itemList.Find(item => item.itemData == itemData);
  }
}
