using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Player : Inventory_Base
{
  public event Action<int> OnQuickSlotUsed;
  public int gold = 10000;
  public List<Inventory_EquipmentSlot> equipList;
  public Inventory_Storage storage { get; private set; }

  [Header("Quick Item Slots")]
  public Inventory_Item[] quickItems = new Inventory_Item[2];

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
}
