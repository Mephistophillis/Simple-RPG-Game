using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Player : Inventory_Base
{
  public event Action<int> OnQuickSlotUsed;

  public Inventory_Storage storage { get; private set; }
  public List<Inventory_EquipmentSlot> equipList;

  [Header("Quick Item Slots")]
  public Inventory_Item[] quickItems = new Inventory_Item[2];

  [Header("Gold Info")]
  public int gold = 10000;

  protected override void Awake()
  {
    base.Awake();
    storage = FindFirstObjectByType<Inventory_Storage>();
  }

  public void SetQuickItemInSlot(int slotNumber, Inventory_Item itemToSlot)
  {
    quickItems[slotNumber - 1] = itemToSlot;
    TriggerUnpdateUI();
  }

  public void TryUseQuickItemInSlot(int passedSlotNumber)
  {
    int slotNumber = passedSlotNumber - 1;
    var itemToUse = quickItems[slotNumber];

    if (itemToUse == null) return;

    TryUseItem(itemToUse);

    if (FindItem(itemToUse) == null)
    {
      quickItems[slotNumber] = FindSameItem(itemToUse);
      TriggerUnpdateUI();

      OnQuickSlotUsed?.Invoke(slotNumber);
    }
  }

  public void TryEquipItem(Inventory_Item item)
  {
    Inventory_Item inventoryItem = FindItem(item);
    var matchingSlots = equipList.FindAll(slot => slot.slotType == item.itemData.itemType);

    // STEP 1: Try to find empty slot and equip item
    foreach (var slot in matchingSlots)
    {
      if (!slot.HasItem())
      {
        EquipItem(inventoryItem, slot);
        return;
      }
    }

    // STEP 2: No empty slots? Replace first one
    var slotToReplace = matchingSlots[0];
    var itemToUnequip = slotToReplace.equipedItem;

    UnequipItem(itemToUnequip, slotToReplace != null);
    EquipItem(inventoryItem, slotToReplace);
  }

  public void EquipItem(Inventory_Item itemToEquip, Inventory_EquipmentSlot slot)
  {
    float savedHelthPercent = player.health.GetHealthPercent();
    slot.equipedItem = itemToEquip;
    slot.equipedItem.AddModifiers(player.stats);
    slot.equipedItem.AddItemEffect(player);

    player.health.SetHealthToPercent(savedHelthPercent);
    RemoveOneItem(itemToEquip);
  }

  public void UnequipItem(Inventory_Item itemToUnequip, bool replacingItem = false)
  {
    if (!CanAddItem(itemToUnequip) && !replacingItem)
    {
      Debug.Log("Inventory is full!");
      return;
    }

    float savedHelthPercent = player.health.GetHealthPercent();

    var slotToUnequip = equipList.Find(slot => slot.equipedItem == itemToUnequip);

    if (slotToUnequip != null)
      slotToUnequip.equipedItem = null;

    itemToUnequip.RemoveModifiers(player.stats);
    itemToUnequip.RemoveItemEffect();

    player.health.SetHealthToPercent(savedHelthPercent);
    AddItem(itemToUnequip);
  }

  public override void SaveData(ref GameData data)
  {
    data.gold = gold;
    data.inventory.Clear();
    data.equipedItems.Clear();

    foreach (var item in itemList)
    {
      if (item != null && item.itemData != null)
      {
        string saveId = item.itemData.saveId;

        if (!data.inventory.ContainsKey(saveId))
          data.inventory[saveId] = 0;

        data.inventory[saveId] += item.stackSize;
      }
    }

    foreach (var slot in equipList)
    {
      if (slot.HasItem())
        data.equipedItems[slot.equipedItem.itemData.saveId] = slot.slotType;
    }
  }

  public override void LoadData(GameData data)
  {
    gold = data.gold;

    foreach (var entry in data.inventory)
    {
      string saveId = entry.Key;
      int stackSize = entry.Value;

      ItemDataSO itemData = itemDataBase.GetItemData(saveId);

      if (itemData == null)
      {
        Debug.LogWarning("Item not found: " + saveId);
        continue;
      }


      for (int i = 0; i < stackSize; i++)
      {
        Inventory_Item itemToLoad = new Inventory_Item(itemData);
        AddItem(itemToLoad);
      }
    }

    foreach (var entry in data.equipedItems)
    {
      string saveId = entry.Key;
      ItemType loadedSlotType = entry.Value;

      ItemDataSO itemData = itemDataBase.GetItemData(saveId);
      Inventory_Item itemToLoad = new Inventory_Item(itemData);

      var slot = equipList.Find(slot => slot.slotType == loadedSlotType && !slot.HasItem());

      slot.equipedItem = itemToLoad;
      slot.equipedItem.AddModifiers(player.stats);
      slot.equipedItem.AddItemEffect(player);
    }

    TriggerUnpdateUI();
  }
}
