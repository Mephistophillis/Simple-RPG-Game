using System;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
  private Inventory_Player inventory;
  private UI_ItemSlot[] uiItemsSlots;
  private UI_EquipSlot[] uiEquipSlots;

  [SerializeField] private Transform uiItemSlotParent;
  [SerializeField] private Transform uiEquipSlotParent;

  private void Awake()
  {
    uiItemsSlots = uiItemSlotParent.GetComponentsInChildren<UI_ItemSlot>();
    uiEquipSlots = uiEquipSlotParent.GetComponentsInChildren<UI_EquipSlot>();

    inventory = FindFirstObjectByType<Inventory_Player>();
    inventory.OnInventoryChange += UpdateUI;

    UpdateUI();
  }

  private void UpdateUI()
  {
    UpdateInventorySlots();
    UpdateEquipmentSlots();
  }

  private void UpdateEquipmentSlots()
  {
    List<Inventory_EquipmentSlot> playerEquipList = inventory.equipList;

    for (int i = 0; i < uiEquipSlots.Length; i++)
    {
      var playerEquipSlot = playerEquipList[i];

      if (!playerEquipSlot.HasItem())
        uiEquipSlots[i].UpdateSlot(null);
      else
        uiEquipSlots[i].UpdateSlot(playerEquipSlot.equipedItem);
    }
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
