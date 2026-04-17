using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
  private UI_ItemSlot[] uiItemsSlots;
  private Inventory_Base inventory;

  private void Awake()
  {
    uiItemsSlots = GetComponentsInChildren<UI_ItemSlot>();

    inventory = FindFirstObjectByType<Inventory_Base>();
    inventory.OnInventoryChange += UpdateInventorySlots;

    UpdateInventorySlots();
  }

  private void UpdateInventorySlots()
  {
    List<Inventory_Item> itemList = inventory.itemList;

    for (int i = 0; i < uiItemsSlots.Length; i++) // 10 ui slots
    {
      if (i < itemList.Count)
      {
        uiItemsSlots[i].UpdateSlot(itemList[i]);
      }
      else
      {
        uiItemsSlots[i].UpdateSlot(null);
      }
    }
  }
}
